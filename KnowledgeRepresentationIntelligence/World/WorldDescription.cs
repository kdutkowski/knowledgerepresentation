namespace KnowledgeRepresentationReasoning.World
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using KnowledgeRepresentationReasoning.World.Interfaces;
    using KnowledgeRepresentationReasoning.World.Records;

    public class WorldDescription : IWorldDescription
    {
        public List<Tuple<WorldDescriptionRecordType, WorldDescriptionRecord>> Descriptions;

        public IEnumerable<string> GetFluentNames()
        {
            return this.GetSummarizedInitialRecord().GetResult();
        }

        public IEnumerable<State> GetInitialStates()
        {
            return this.GetSummarizedInitialRecord().PossibleFluents.Select(t => new State { Fluents = t.ToList() });
        }

        public IEnumerable<Implication> GetImplications(Action action, State state, int time)
        {
            throw new NotImplementedException();
        }

        public bool Validate(Action action, State state, int time)
        {
            throw new NotImplementedException();
        }

        private InitialRecord GetSummarizedInitialRecord()
        {
            var initialRecords = Descriptions.Where(t => t.Item1 == WorldDescriptionRecordType.Initially).ToList();
            if (!initialRecords.Any())
                throw new TypeInitializationException("Brak warunków początkowych!", null);

            return initialRecords.Select(t => (t.Item2 as InitialRecord)).Aggregate((x, y) => x.ConcatOr(y));
        }

        private IEnumerable<Action> GetTriggeredActions(Action action, State state, int time)
        {
            var actionInvokesRecords = Descriptions.Where(t => t.Item1 == WorldDescriptionRecordType.ActionInvokesAfterIf)
                .Select(t => t.Item2 as ActionInvokesAfterIfRecord).ToList();
            var expressionTriggersRecords = Descriptions.Where(t => t.Item1 == WorldDescriptionRecordType.ExpressionTriggersAction)
                .Select(t => t.Item2 as ExpressionTriggersActionRecord).ToList();

            var triggeredActions = actionInvokesRecords.Where(t => t.IsFulfilled(state, action)).Select(t => t.GetResult(time)).ToList();
            triggeredActions.AddRange(expressionTriggersRecords.Where(t => t.IsFulfilled(state)).Select(t => t.GetResult(time)));

            return triggeredActions;
        }

        private IEnumerable<Fluent> GetReleasedFluents(Action action, State state, int time)
        {
            var actionReleaseRecords = Descriptions.Where(t => t.Item1 == WorldDescriptionRecordType.ActionReleasesIf)
                                                   .Select(t => t.Item2 as ActionReleasesIfRecord).ToList();

            var releasedFluents = actionReleaseRecords.Where(t => t.IsFulfilled(state, action)).Select(t => t.GetResult(time));
            return releasedFluents;
        }

        internal bool CheckIfLeafIsPossible(Logic.Vertex leaf)
        {
            throw new NotImplementedException();
        }
    }
}
