namespace KnowledgeRepresentationReasoning.World.Interfaces
{
    using System.Collections.Generic;

    public interface IWorldDescription
    {
        IEnumerable<State> GetInitialStates();

        IEnumerable<string> GetFluentNames();

        IEnumerable<Implication> GetImplications(Logic.Vertex leaf);

        bool Validate(Logic.Vertex leaf);
    }
}
