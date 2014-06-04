using System.CodeDom;

namespace KnowledgeRepresentationReasoning.World
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using KnowledgeRepresentationReasoning.Logic;
    using KnowledgeRepresentationReasoning.World.Interfaces;
    using KnowledgeRepresentationReasoning.World.Records;

    public class WorldDescription : IWorldDescription
    {
        public List<Tuple<WorldDescriptionRecordType, WorldDescriptionRecord>> Descriptions;

        public WorldDescription()
        {
            Descriptions = new List<Tuple<WorldDescriptionRecordType, WorldDescriptionRecord>>();
        }

        public IEnumerable<string> GetFluentNames()
        {
            return this.GetSummarizedInitialRecord().GetResult();
        }

        public IEnumerable<State> GetInitialStates()
        {
            return this.GetSummarizedInitialRecord().PossibleFluents.Select(t => new State { Fluents = t.ToList() });
        }

        public List<Implication> GetImplications(Vertex leaf)
        {
            var triggeredActions = this.GetTriggeredActions(leaf.ActualWorldAction, leaf.ActualState, leaf.Time);
            var possibleFutureStates = this.GetPossibleFutureStates(leaf.ActualWorldAction, leaf.ActualState, leaf.Time);
            return possibleFutureStates.Select(possibleFutureState => 
                new Implication { FutureState = possibleFutureState, TriggeredActions = triggeredActions.ToList() }).ToList();
        }

        public bool Validate(Vertex leaf)
        {
            var impossibleActionAtRecords = Descriptions.Where(t => t.Item1 == WorldDescriptionRecordType.ImpossibleActionAt)
                                                   .Select(t => t.Item2 as ImpossibleActionAtRecord).ToList();
            var impossibleActionIfRecords = Descriptions.Where(t => t.Item1 == WorldDescriptionRecordType.ImpossibleActionIf)
                                                   .Select(t => t.Item2 as ImpossibleActionIfRecord).ToList();
            foreach (var IAAR in impossibleActionAtRecords)
            {
                if (IAAR.IsFulfilled(leaf.Time) && IAAR.GetResult() == leaf.ActualWorldAction)
                {
                    return false;
                }
            }
            foreach (var IAIR in impossibleActionIfRecords)
            {
                if (IAIR.IsFulfilled(leaf.ActualState) && IAIR.GetResult() == leaf.ActualWorldAction)
                {
                    return false;
                }
            }
            return true;
        }

        private InitialRecord GetSummarizedInitialRecord()
        {
            var initialRecords = Descriptions.Where(t => t.Item1 == WorldDescriptionRecordType.Initially).ToList();
            if (!initialRecords.Any())
            {
                return new InitialRecord("");
            }

            return initialRecords.Select(t => (t.Item2 as InitialRecord)).Aggregate((x, y) => x.ConcatOr(y));
        }

        private IEnumerable<WorldAction> GetTriggeredActions(WorldAction worldAction, State state, int time)
        {
            var actionInvokesRecords = Descriptions.Where(t => t.Item1 == WorldDescriptionRecordType.ActionInvokesAfterIf)
                .Select(t => t.Item2 as ActionInvokesAfterIfRecord).ToList();
            
            var expressionTriggersRecords = Descriptions.Where(t => t.Item1 == WorldDescriptionRecordType.ExpressionTriggersAction)
                .Select(t => t.Item2 as ExpressionTriggersActionRecord).ToList();

            if(worldAction != null) expressionTriggersRecords = new List<ExpressionTriggersActionRecord>();

            var triggeredActions = new List<WorldAction>();
            if(worldAction != null)
                triggeredActions.AddRange(actionInvokesRecords.Where(t => t.IsFulfilled(state, worldAction)).Select(t => t.GetResult(time)).ToList());
            triggeredActions.AddRange(expressionTriggersRecords.Where(t => t.IsFulfilled(state)).Select(t => t.GetResult(time)));

            return triggeredActions;
        }

        private IEnumerable<Fluent> GetReleasedFluents(WorldAction worldAction, State state, int time)
        {
            if(worldAction == null) return new List<Fluent>();
            var actionReleaseRecords = Descriptions.Where(t => t.Item1 == WorldDescriptionRecordType.ActionReleasesIf)
                                                   .Select(t => t.Item2 as ActionReleasesIfRecord).ToList();

            var releasedFluents = actionReleaseRecords.Where(t => t.IsFulfilled(state, worldAction)).Select(t => t.GetResult(time));
            return releasedFluents;
        }

        private IEnumerable<State> GetPossibleFutureStates(WorldAction worldAction, State state, int time)
        {
            var possibleFutureStates = new List<State>();
            var possibleStateChanges = new List<State>{ state };

            // Get released fluents
            var releasedFluents = GetReleasedFluents(worldAction, state, time);

            // Get possible state changes from ActionCausesIf records
            var actionCausesRec = Descriptions.Where(t => t.Item1 == WorldDescriptionRecordType.ActionCausesIf)
                            .Select(t => t.Item2 as ActionCausesIfRecord).ToList();

            if (actionCausesRec.Any())
            {
                var causedStatesX = actionCausesRec.Where(t => t.IsFulfilled(state, worldAction));
                if (causedStatesX.Any())
                {
                    var causedStates = causedStatesX.Aggregate((x, y) => x.Concat(y)).GetResult();
                    possibleStateChanges = new List<State>(causedStates.Select(t => new State {Fluents = t.ToList()}));
                }
            }

            // Get all future states excluding released fluents changes
            foreach (var stateChange in possibleStateChanges)
            {
                var template = (State)state.Clone();
                template.Fluents.RemoveAll(t => stateChange.Fluents.Any(x => x.Name == t.Name));
                template.Fluents.AddRange(stateChange.Fluents);
                possibleFutureStates.Add(template);
            }

            // Include released fluents
            foreach (var fluent in releasedFluents)
            {
                var statesToAdd = new List<State>();
                foreach (var futureState in possibleFutureStates)
                {
                    var copy = (State)futureState.Clone();
                    var fluentToRelease = copy.Fluents.First(t => t.Name == fluent.Name);
                    fluentToRelease.Value = !fluentToRelease.Value;
                    statesToAdd.Add(copy);
                }
                possibleFutureStates.AddRange(statesToAdd);
            }

            return possibleFutureStates;
        }
    }
}
