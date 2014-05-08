namespace KnowledgeRepresentationReasoning.World.Interfaces
{
    using System.Collections.Generic;

    using KnowledgeRepresentationReasoning.Logic;

    public interface IWorldDescription
    {
        IEnumerable<State> GetInitialStates();

        IEnumerable<string> GetFluentNames();

        List<Implication> GetImplications(Vertex leaf, int nextTime);

        bool Validate(Logic.Vertex leaf);
    }
}
