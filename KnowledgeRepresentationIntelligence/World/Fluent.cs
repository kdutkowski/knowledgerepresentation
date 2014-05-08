namespace KnowledgeRepresentationReasoning.World
{
    public class Fluent
    {
        public string Name { get; set; }
        public bool Value { get; set; }

        public Fluent(string name, bool p)
        {
            this.Name = name;
            Value = p;
        }

        public Fluent()
        {
            // TODO: Complete member initialization
        }

        public override string ToString()
        {
            string description = "Fluent: " + Name + " , value: " + Value + " ";

            return description;
        }

        public override bool Equals(object obj)
        {
            if (obj is Fluent)
            {
                var fluent = obj as Fluent;
                if (Name.Equals(fluent.Name)) return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
