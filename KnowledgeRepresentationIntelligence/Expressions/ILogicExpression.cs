namespace KnowledgeRepresentationReasoning.Expressions
{
    using System;
    using System.Collections.Generic;

    using KnowledgeRepresentationReasoning.World;

    public interface ILogicExpression
    {
        bool Evaluate();
        bool Evaluate(IEnumerable<Tuple<string, bool>> values);
        void SetExpression(string expression);
        string[] GetFluentNames();
        List<Fluent[]> CalculatePossibleFluents();

        void AddExpression(ILogicExpression logicExpression);
    }
}
