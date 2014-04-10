using System.Collections.Generic;

namespace KnowledgeRepresentationReasoning.World.Records
{
    public class InitiallyRecord : WorldDescriptionRecord
    {
        public InitiallyRecord() : base(WorldDescriptionRecordType.Initially)
        {
            
        }

        public List<Fluent> InitialFluents { get; set; }
    }
}
