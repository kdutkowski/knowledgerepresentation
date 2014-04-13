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
            var subResults = new List<State>[initialRecords.Count];

            for(int i = 0; i < initialRecords.Count; i++)
            {
                subResults[i].AddRange(
                    (initialRecords[i].Item2 as InitialRecord)
                        .PossibleFluents
                        .Select(t => new State(){Fluents = t.ToList()}));
            }

            var result = subResults.Aggregate((x, y) => (
                from stateX in x 
                from stateY in y 
                select new State { Fluents = stateX.Fluents.Concat(stateY.Fluents).ToList() }).ToList());
            return result;
        }

        public WorldDescriptionImplication Verify(Action action, State state, int time)
        {
            throw new NotImplementedException();
        }
    }
}
