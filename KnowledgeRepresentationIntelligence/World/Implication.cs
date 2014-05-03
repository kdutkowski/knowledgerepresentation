namespace KnowledgeRepresentationReasoning.World
{
    using System.Collections.Generic;

    public class Implication
    {
        // Jakie akcje do wykonania
        public List<Action> TriggeredActions { get; set; }

        // Jakie fluenty do uwolnienia
        public List<Fluent> FluentsToRelease { get; set; }

        // Przyszły stan bez uwzględnienia uwolnionych fluentów
        public State State { get; set; }

        // Czas w którym się znajdziemy
        public int Time { get; set; }
    }
}
