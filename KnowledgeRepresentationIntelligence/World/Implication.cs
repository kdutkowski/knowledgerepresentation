namespace KnowledgeRepresentationReasoning.World
{
    using System.Collections.Generic;

    public class Implication
    {
        // Przyszłe akcje
        public List<Action> TriggeredActions { get; set; }

        // Możliwe przyszłe stany
        public List<State> FutureStates { get; set; }
    }
}
