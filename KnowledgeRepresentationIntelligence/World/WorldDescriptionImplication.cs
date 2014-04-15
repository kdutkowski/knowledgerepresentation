namespace KnowledgeRepresentationReasoning.World
{
    using System.Collections.Generic;

    public class WorldDescriptionImplication
    {
        // Jakie akcje zostaną wykonane
        public List<Action> TriggeredActions { get; set; }

        // Jakie fluenty zostały uwolnione
        public List<Fluent> ReleasedFluents { get; set; }

        // Jakie akcje są niewykonalne
        public List<Action> ImpossibleActions { get; set; }

        // Czy stan spełnia warunki
        public bool IsPossible { get; set; }
    }
}
