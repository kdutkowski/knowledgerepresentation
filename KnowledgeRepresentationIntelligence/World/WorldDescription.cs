namespace KnowledgeRepresentationReasoning.World
{
    using System;
    using System.Collections.Generic;

    using KnowledgeRepresentationReasoning.World.Interfaces;
    using KnowledgeRepresentationReasoning.World.Records;

    public class WorldDescription : IWorldDescription
    {
        public List<Tuple<WorldDescriptionRecordType, WorldDescriptionRecord>> Descriptions;

        public State GetInitialState()
        {
            throw new NotImplementedException();
        }

        public WorldDescriptionImplication Verify(Action action, State state, int time)
        {
            throw new NotImplementedException();
        }
    }
}
