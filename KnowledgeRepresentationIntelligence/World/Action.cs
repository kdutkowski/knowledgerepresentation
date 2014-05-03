namespace KnowledgeRepresentationReasoning.World
{
    public class Action
    {
        public string Id { get; set; }
        public int? Duration { get; set; }
        public int? TriggeredAfter { get; set; }
        public int? StartAt { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Action)
            {
                var action = obj as Action;
                if (action.Id.Equals(this.Id) && action.Duration.Equals(this.Duration))
                    return true;
            }
            return false;
        }
    }
}
