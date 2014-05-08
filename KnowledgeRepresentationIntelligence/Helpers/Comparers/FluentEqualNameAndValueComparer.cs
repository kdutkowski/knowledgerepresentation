namespace KnowledgeRepresentationReasoning.Helpers.Comparers
{
    using System.Collections.Generic;

    using KnowledgeRepresentationReasoning.World;

    public class FluentEqualNameAndValueComparer : IEqualityComparer<Fluent>
    {
        public bool Equals(Fluent x, Fluent y)
        {
            return x.Name == y.Name && x.Value == y.Value;
        }

        public int GetHashCode(Fluent obj)
        {
            int hashCode = obj.Name.GetHashCode();
            hashCode = (hashCode * 397) ^ obj.Value.GetHashCode();
            return hashCode;
        }
    }
}
