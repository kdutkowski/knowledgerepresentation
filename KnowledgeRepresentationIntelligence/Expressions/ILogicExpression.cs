namespace KnowledgeRepresentationReasoning.Expressions
{
    using System;
    using System.Collections.Generic;

    using KnowledgeRepresentationReasoning.World;

    internal interface ILogicExpression
    {
        bool Evaluate();
        bool Evaluate(IEnumerable<Tuple<string, bool>> values);
        void SetExpression(string expression);
        string[] GetFluentNames();
        List<Fluent[]> CalculatePossibleFluents();
    }
}
