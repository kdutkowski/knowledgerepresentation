namespace KnowledgeRepresentationReasoning.Expressions
{
    using System;
    using System.Collections.Generic;

    using KnowledgeRepresentationReasoning.World;

    public interface ILogicExpression
    {
        bool Evaluate(IEnumerable<Tuple<string, bool>> values);
        bool Evaluate(State state);
        void SetExpression(string expression);
        string[] GetFluentNames();
        List<Fluent[]> CalculatePossibleFluents();

        void AddExpression(ILogicExpression logicExpression);
    }
}
