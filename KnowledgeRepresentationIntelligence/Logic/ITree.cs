namespace KnowledgeRepresentationReasoning.Logic
{
    internal interface ITree
    {
        bool AddFirstLevel(World.WorldDescription WorldDescription, Scenario.ScenarioDescription ScenarioDescription, out int numberOfImpossibleLeaf);
    }
}
