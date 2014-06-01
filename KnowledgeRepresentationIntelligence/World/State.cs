namespace KnowledgeRepresentationReasoning.World
{
    using System.Collections.Generic;
    using System.Linq;

    using KnowledgeRepresentationReasoning.Helpers.Comparers;

    public class State
    {
        public List<Fluent> Fluents { get; set; }

        public State()
        {
            Fluents = new List<Fluent>();
        }

        public object Clone()
        {
            var res = new State { Fluents = new List<Fluent>() };
            foreach (var fluent in Fluents)
                res.Fluents.Add(new Fluent{ Name = fluent.Name, Value = fluent.Value });
            return res;
        }


        public override string ToString()
        {
            string description = "State with " + Fluents.Count + " fluents: ";

            foreach (var fluent in Fluents)
            {
                description += "\n" + fluent + "(" + (fluent.Value?"1":"0") + ")";
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

        public override bool Equals(object obj)
        {
            if (obj is State)
            {
                var state = obj as State;
                return state.Fluents.All(fluent => Fluents.Contains(fluent, new FluentEqualNameAndValueComparer()));
            }
            return false;
        }
    }
}
