namespace KnowledgeRepresentationReasoning.World.Interfaces
{
    public interface IWorldDescription
    {
        State GetInitialState();

        WorldDescriptionImplication Verify(Action action, State state, int time);
    }
}
