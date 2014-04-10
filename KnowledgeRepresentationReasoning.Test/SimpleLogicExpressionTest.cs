namespace KnowledgeRepresentationReasoning.Test
{
    using System;
    using KnowledgeRepresentationReasoning.Expressions;
    using NUnit.Framework;

    [TestFixture]
    public class SimpleLogicExpressionTest
    {
        private readonly ILogicExpression _expression;

        public SimpleLogicExpressionTest()
        {
            _expression = new SimpleLogicExpression("");
        }

        [Test]
        public void OneValue_Test()
        {
            var values = new[]
                         {
                             new Tuple<string, bool>("v1", true)
                         };
            _expression.SetExpression("v1");

            bool result = _expression.Evaluate(values);

            Assert.True(result);
        }

        [Test]
        public void OneValue_Negation_Test()
        {
            var values = new[]
                         {
                             new Tuple<string, bool>("v1", true)
                         };
            _expression.SetExpression("!v1");

            bool result = _expression.Evaluate(values);

            Assert.False(result);
        }

        [Test]
        public void TwoValues_OR_Test_ResultTrue()
        {
            var values = new[]
                         {
                             new Tuple<string, bool>("v1", true),
                             new Tuple<string, bool>("v2", false), 
                         };
            _expression.SetExpression("v1 || v2");

            bool result = _expression.Evaluate(values);

            Assert.True(result);
        }

        [Test]
        public void TwoValues_OR_Test_ResultFalse()
        {
            var values = new[]
                         {
                             new Tuple<string, bool>("v1", false),
                             new Tuple<string, bool>("v2", false), 
                         };
            _expression.SetExpression("v1 || v2");

            bool result = _expression.Evaluate(values);

            Assert.False(result);
        }

        [Test]
        public void TwoValues_AND_Test_ResultFalse()
        {
            var values = new[]
                         {
                             new Tuple<string, bool>("v1", true),
                             new Tuple<string, bool>("v2", false), 
                         };
            _expression.SetExpression("v1 && v2");

            bool result = _expression.Evaluate(values);

            Assert.False(result);
        }

        [Test]
        public void TwoValues_AND_Test_ResultTrue()
        {
            var values = new[]
                         {
                             new Tuple<string, bool>("v1", true),
                             new Tuple<string, bool>("v2", true), 
                         };
            _expression.SetExpression("v1 && v2");

            bool result = _expression.Evaluate(values);

            Assert.True(result);
        }

        [Test]
        public void ManyValues_OR_AND_NEG_WithBrackets_Test()
        {
            var values = new[]
                         {
                             new Tuple<string, bool>("v1", false),
                             new Tuple<string, bool>("v2", true), 
                             new Tuple<string, bool>("v3", false),
                             new Tuple<string, bool>("v4", false),
                             new Tuple<string, bool>("v5", true),
                         };
            _expression.SetExpression("(v1 || v2) && (!v3 || v4 && v5)");

            bool result = _expression.Evaluate(values);

            Assert.True(result);
        }
    }
}
