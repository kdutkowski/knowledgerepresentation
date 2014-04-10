namespace KnowledgeRepresentationReasoning.Expressions
{
    using System;
    using System.Collections.Generic;

    internal interface ILogicExpression
    {
        bool Evaluate(IEnumerable<Tuple<string, bool>> values);

        void SetExpression(string expression);
    }
}
