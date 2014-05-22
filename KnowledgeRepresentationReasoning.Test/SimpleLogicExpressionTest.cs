namespace KnowledgeRepresentationReasoning.Test
{
    using System;

    using KnowledgeRepresentationReasoning.Expressions;
    using KnowledgeRepresentationReasoning.World;

    using NUnit.Framework;

    [TestFixture]
    public class SimpleLogicExpressionTest
    {
        private readonly ILogicExpression _expression;

        public SimpleLogicExpressionTest()
        {
            _expression = new SimpleLogicExpression();
        }

        [Test]
        public void EmptyExpressionShouldReturnTrue_Test()
        {
            State st = new State();
            bool result = _expression.Evaluate(st);

            Assert.True(result);
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

        [Test]
        public void TwoValues_IMPL_Test_ResultTrue()
        {
            var values = new[]
                         {
                             new Tuple<string, bool>("v1", false),
                             new Tuple<string, bool>("v2", false),
                         };
            _expression.SetExpression("h.impl(v1, v2)");

            bool result = _expression.Evaluate(values);

            Assert.True(result);
        }

        [Test]
        public void TwoValues_IMPL_Test_ResultFalse()
        {
            var values = new[]
                         {
                             new Tuple<string, bool>("v1", true),
                             new Tuple<string, bool>("v2", false),
                         };
            _expression.SetExpression("h.impl(v1, v2)");

            bool result = _expression.Evaluate(values);

            Assert.False(result);
        }

        [Test]
        public void AddExpression_FirstEmpty_Test()
        {
            var firstExpression = new SimpleLogicExpression(string.Empty);
            var secondExpression = new SimpleLogicExpression("a || b");
            firstExpression.AddExpression(secondExpression);

            Assert.AreEqual("a || b", firstExpression.ToString());
            Assert.AreEqual(secondExpression.ToString(), firstExpression.ToString());
        }

        [Test]
        public void AddExpression_SecondEmpty_Test()
        {
            var firstExpression = new SimpleLogicExpression("a || b");
            var secondExpression = new SimpleLogicExpression(string.Empty);
            firstExpression.AddExpression(secondExpression);

            Assert.AreEqual("a || b", firstExpression.ToString());
            Assert.AreEqual(string.Empty, secondExpression.ToString());
        }

        [Test]
        public void AddExpression_AllEmpty_Test()
        {
            var firstExpression = new SimpleLogicExpression(string.Empty);
            var secondExpression = new SimpleLogicExpression(string.Empty);
            firstExpression.AddExpression(secondExpression);

            Assert.AreEqual(string.Empty, firstExpression.ToString());
            Assert.AreEqual(string.Empty, secondExpression.ToString());
        }

        [Test]
        public void AddExpression_Simple_Test()
        {
            var firstExpression = new SimpleLogicExpression("a || b");
            var secondExpression = new SimpleLogicExpression("a && c");
            firstExpression.AddExpression(secondExpression);

            Assert.AreEqual("(a || b) && (a && c)", firstExpression.ToString());
            Assert.AreEqual("a && c", secondExpression.ToString());
        }

        [Test]
        public void AddExpression_Complex_Test()
        {
            var firstExpression = new SimpleLogicExpression("(!a || b) && c");
            var secondExpression = new SimpleLogicExpression("a && c || !(b && !a)");
            var thirdExpression = new SimpleLogicExpression("!a && !b || (!a)");
            secondExpression.AddExpression(thirdExpression);

            var expected = "(a && c || !(b && !a)) && (!a && !b || (!a))".Trim().Normalize();
            var actual = secondExpression.ToString().Trim().Normalize();
            Assert.AreEqual(expected, actual);

            firstExpression.AddExpression(secondExpression);

            var expected2 = "((!a || b) && c) && ((a && c || !(b && !a)) && (!a && !b || (!a)))".Trim().Normalize();
            var actual2 = firstExpression.ToString().Trim().Normalize();
            Assert.AreEqual(expected2, actual2);
        }
    }
}