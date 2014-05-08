namespace KnowledgeRepresentationReasoning.World
{
    using System.Collections.Generic;

    public class Implication
    {
        // Przyszłe akcje
        public List<WorldAction> TriggeredActions { get; set; }

        // Możliwe przyszły stan
        public State FutureState { get; set; }
    }
}
