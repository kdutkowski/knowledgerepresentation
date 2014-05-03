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
            return this.GetSummarizedInitialRecord().GetFluentNames();
        }

        public IEnumerable<State> GetInitialStates()
        {
            return this.GetSummarizedInitialRecord().PossibleFluents.Select(t => new State { Fluents = t.ToList() });
        }

        public WorldDescriptionImplication Verify(Action action, State state, int time)
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
    }
}
