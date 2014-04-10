namespace KnowledgeRepresentationReasoning.World
{
    using System.Collections.Generic;

    public class State
    {
        // Dictionary of pairs (id, fluent)
        public Dictionary<string, Fluent> Fluents { get; set; }
    }
}
