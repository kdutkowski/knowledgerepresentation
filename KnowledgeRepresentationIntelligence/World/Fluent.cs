namespace KnowledgeRepresentationReasoning.World
{
    public class Fluent
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Value { get; set; }
        public int? ReleaseAt { get; set; }

        public override string ToString()
        {
            string description = "Fluent: " + Id + ": name: " + Name + " , value: " + Value;

            return description;
        }
    }
}
