namespace KnowledgeRepresentationReasoning.World.Interfaces
{
    using System.Collections.Generic;

    public interface IWorldDescription
    {
        IEnumerable<State> GetInitialStates();

        IEnumerable<string> GetFluentNames();

        IEnumerable<Implication> GetImplications(Action action, State state, int time);
    }
}
