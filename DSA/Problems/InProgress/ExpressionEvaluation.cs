using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Problems.InProgress
{
    /// <summary>
    ///     Evaluate the validity of the passed in mathematical expression.
    /// 
    ///     The expression may contain the following types of characters:
    ///     Numeric: [0 ... 9]
    ///     Operators: [+,-,*,/]
    ///     Signs: [+,-]
    ///     Brackets: [(,)]
    /// </summary>
    /// <mutation>
    ///     Brackets: [(,),{,}]
    /// </mutation>
    /// <input>
    ///     "-((10/3)*4)+-1"
    /// </input>
    /// <output>
    ///     true
    /// </output>
    public class ExpressionEvaluation
    {
        public static bool StateMachine(string expression)
        {
            // Time:    O(n).   One pass over the entire string from left to right. Evaluates the current state of the expression and compares it to potential valid states.
            // Memory:  O(1).   A constant amount of memory is used to keep track of potential valid states.

            //TODO: Implement the function
            return true;
        }
    }

    [TestClass]
    public class TestExpressionEvaluation
    {
        [TestMethod]
        public void StateMachineOperators()
        {
            string input = "1*2/3+5-6";
            const bool expected = true;

            var actual = ExpressionEvaluation.StateMachine(input);

            Assert.AreEqual(expected, actual);
        }
    }

    // TODO: Add more test cases
}