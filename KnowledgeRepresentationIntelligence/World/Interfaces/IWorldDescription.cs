namespace KnowledgeRepresentationReasoning.World.Interfaces
{
    using System.Collections.Generic;

    using KnowledgeRepresentationReasoning.Logic;

    public interface IWorldDescription
    {
        IEnumerable<State> GetInitialStates();

        IEnumerable<string> GetFluentNames();

        List<Implication> GetImplications(Vertex leaf);

        bool Validate(Vertex leaf);
    }
}
