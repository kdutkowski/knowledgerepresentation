namespace KnowledgeRepresentationReasoning.World
{
    using System.Collections.Generic;

    public class State
    {
        public List<Fluent> Fluents { get; set; }

        public State()
        {
            Fluents = new List<Fluent>();
        }
    }
}
