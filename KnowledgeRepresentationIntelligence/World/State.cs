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

        public override string ToString()
        {
            string description = "State with " + Fluents.Count + " fluents:";

            foreach (var fluent in Fluents)
            {
                description += "\n" + fluent.ToString();
            }

            return description;
        }

        internal void AddFluents(List<string> fluentsName)
        {
            foreach (var name  in fluentsName)
            {
                Fluent newFluent = new Fluent(name, false);
                Fluents.Add(newFluent);
            }
        }
    }
}
