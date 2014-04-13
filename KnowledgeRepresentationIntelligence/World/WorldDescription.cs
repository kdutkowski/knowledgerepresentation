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

        public IEnumerable<State> GetInitialStates()
        {
            var initialRecords = Descriptions.Where(t => t.Item1 == WorldDescriptionRecordType.Initially).ToList();
            if (!initialRecords.Any())
                throw new TypeInitializationException("Brak warunków początkowych!", null);

            var initialRecordsSum = initialRecords.Select(t => (t.Item2 as InitialRecord)).Aggregate((x, y) => x.Concat(y));
            return initialRecordsSum.PossibleFluents.Select(t => new State { Fluents = t.ToList() });
        }

        public WorldDescriptionImplication Verify(Action action, State state, int time)
        {
            throw new NotImplementedException();
        }
    }
}
