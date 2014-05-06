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

        // TODO: Zaimplementować metodę zwracającą rezultat przejścia przez dany węzeł w drzewie (czyli co się stanie jak w danym stanie
        // wykonamy daną akcję w jakimś czasie (według opisu świata)
        // TODO: Niech ta metoda zwraca jedno Implication (trzeba poprawic w dalszym kodzie)
        public List<Implication> GetImplications(Vertex leaf)
        {
            throw new NotImplementedException();
        }

        // TODO: Zaimplementować walidację czy dany wezel jest prawidlowy wzgledem opisu swiata czyli czy w danym czasie
        // wykonanie akcji jest możliwe, czy stan taki w danym czasie i przy danej akcji jest mozliwy
        public bool Validate(Logic.Vertex leaf)
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

        private IEnumerable<WorldAction> GetTriggeredActions(WorldAction worldAction, State state, int time)
        {
            var actionInvokesRecords = Descriptions.Where(t => t.Item1 == WorldDescriptionRecordType.ActionInvokesAfterIf)
                .Select(t => t.Item2 as ActionInvokesAfterIfRecord).ToList();
            var expressionTriggersRecords = Descriptions.Where(t => t.Item1 == WorldDescriptionRecordType.ExpressionTriggersAction)
                .Select(t => t.Item2 as ExpressionTriggersActionRecord).ToList();

            var triggeredActions = actionInvokesRecords.Where(t => t.IsFulfilled(state, worldAction)).Select(t => t.GetResult(time)).ToList();
            triggeredActions.AddRange(expressionTriggersRecords.Where(t => t.IsFulfilled(state)).Select(t => t.GetResult(time)));

            return triggeredActions;
        }

        private IEnumerable<Fluent> GetReleasedFluents(WorldAction worldAction, State state, int time)
        {
            var actionReleaseRecords = Descriptions.Where(t => t.Item1 == WorldDescriptionRecordType.ActionReleasesIf)
                                                   .Select(t => t.Item2 as ActionReleasesIfRecord).ToList();

            var releasedFluents = actionReleaseRecords.Where(t => t.IsFulfilled(state, worldAction)).Select(t => t.GetResult(time));
            return releasedFluents;
        }

        // TODO: Zaimplementować za pomocą GetReleasedFluents oraz pobierając odpowiednie fluenty zmieniane przez
        // akcję poprzez rekordy ActionCausesIfRecord, metodę zwracającą możliwe stany po wykonaniu akcji
        // (uwzględnić to ze pewne fluenty zostaną uwolnione (wtedy stan rozdziela się na dwa możliwe z 0 i 1 jako
        // wartością fluenta
        private List<State> GetPossibleFutureStates(WorldAction worldAction, State state, int time)
        {
            throw new NotImplementedException();
        }
    }
}
