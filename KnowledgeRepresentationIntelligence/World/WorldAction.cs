using System;

namespace KnowledgeRepresentationReasoning.World
{
    public class WorldAction : ICloneable, IEquatable<WorldAction>
    {
        public string Id { get; set; }
        public int? Duration { get; set; }
        public int? StartAt { get; set; }

        public WorldAction() { }

        public WorldAction(string id, int startTime, int durationTime)
        {
            Id = id;
            StartAt = startTime;
            Duration = durationTime;
        }

        public override bool Equals(object obj)
        {
            if (obj is WorldAction)
            {
                var action = obj as WorldAction;
                if (action.Id.Equals(Id) && action.Duration.Equals(Duration))
                    return true;
            }
            return false;
        }

        public bool Equals(WorldAction other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Id, other.Id) && Duration == other.Duration;
        }

        public bool EqualsIncludingStartAt(WorldAction other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Id, other.Id) && Duration == other.Duration && StartAt == other.StartAt;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Id != null ? Id.GetHashCode() : 0) * 397) ^ Duration.GetHashCode();
            }
        }

        public object Clone()
        {
            var worldAction = new WorldAction();
            worldAction.Id = Id;
            worldAction.Duration = Duration;
            worldAction.StartAt = StartAt;

            return worldAction;
        }

        public int GetEndTime()
        {
            int? endTime = StartAt + Duration;
            return endTime??-1;
        }

        public override string ToString()
        {
            var result = "(" + Id;
            if (Duration > 0)
                result += "," + Duration;
            result += ")";
            return result;
        }

        public bool Overlap(WorldAction other)
        {
            var otherEndTime = other.GetEndTime();
            var thisEndTime = GetEndTime();
            if (thisEndTime != -1 && otherEndTime != -1 && !EqualsIncludingStartAt(other))
            {
                if (StartAt >= other.StartAt && StartAt < otherEndTime)
                    return true;
                if (thisEndTime > other.StartAt && thisEndTime <= otherEndTime)
                    return true;
                if (StartAt < other.StartAt && thisEndTime > otherEndTime)
                    return true;
            }
            return false;
        }
    }
}
