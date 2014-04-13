namespace KnowledgeRepresentationReasoning.World.Interfaces
{
    using System.Collections.Generic;

    public interface IWorldDescription
    {
        IEnumerable<State> GetInitialStates();

        WorldDescriptionImplication Verify(Action action, State state, int time);
    }
}
