using System.Collections.Generic;
using System.Linq;
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
    ///     "-(((10/3)*4)+-1)"
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

            // Treating null and empty string expressions as true
            if (string.IsNullOrEmpty(expression)) return true;

            int bracketCount = 0;
            var validNextStates = ExpressionStateMachine.GetAllStates();

            for (var i = 0; i < expression.Length; i++)
            {
                var currChar = expression[i];
                var currentPossibleStates = ExpressionStateMachine.GetStates(currChar);

                // Check current state validity against previous valid states
                if (StateIsInvalid(validNextStates, currentPossibleStates)) return false;
                // Use the possible states to get a list of all valid states
                validNextStates = ExpressionStateMachine.GetValidStates(currentPossibleStates);

                // TODO: Keep a Stack of brackets and compare the types on Pop instead of a count
                UpdateBracketCount(ref bracketCount, currChar);
                // Return early if the bracket count is off
                if (bracketCount < 0) return false;
            }

            // Check to see if all brackets have a matching pair before returning true
            if (bracketCount != 0) return false;

            return validNextStates.Contains(ExpressionStateMachine.ExpressionStateEnum.Exit);
        }

        private static bool StateIsInvalid(List<ExpressionStateMachine.ExpressionStateEnum> validStates, List<ExpressionStateMachine.ExpressionStateEnum> currentStates)
        {
            // If any of the current states is valid for the previous possible states, continue
            return !validStates.Intersect(currentStates).Any();
        }

        private static void UpdateBracketCount(ref int bracketCount, char bracket)
        {
            if (ExpressionStateMachine.IsOpeningBracket(bracket)) bracketCount++;
            if (ExpressionStateMachine.IsClosingBracket(bracket)) bracketCount--;
        }
    }

    public static class ExpressionStateMachine
    {
        public static bool IsNumeric(char c) => char.IsDigit(c);
        public static bool IsOperator(char c) => new List<char> { '+', '-', '/', '*' }.Contains(c);
        public static bool IsSign(char c) => new List<char> { '+', '-' }.Contains(c);
        public static bool IsOpeningBracket(char c) => new List<char> { '(', '{' }.Contains(c);
        public static bool IsClosingBracket(char c) => new List<char> { ')', '}' }.Contains(c);

        public enum ExpressionStateEnum
        {
            Numeric,
            Operator,
            Sign,
            Bracket,
            Exit
        }

        public static List<ExpressionStateEnum> GetStates(char c)
        {
            var states = new List<ExpressionStateEnum>();

            // Numeric
            if (IsNumeric(c)) states.Add(ExpressionStateEnum.Numeric);

            // Operator
            if (IsOperator(c)) states.Add(ExpressionStateEnum.Operator);

            // Signs
            if (IsSign(c)) states.Add(ExpressionStateEnum.Sign);

            // Brackets
            if (IsOpeningBracket(c) || IsClosingBracket(c)) states.Add(ExpressionStateEnum.Bracket);

            return states;
        }

        public static List<ExpressionStateEnum> GetAllStates()
        {
            return new List<ExpressionStateEnum>
            {
                ExpressionStateEnum.Numeric,
                ExpressionStateEnum.Operator,
                ExpressionStateEnum.Sign,
                ExpressionStateEnum.Bracket,
                ExpressionStateEnum.Exit
            };
        }

        public static List<ExpressionStateEnum> GetValidStates(List<ExpressionStateEnum> currentStates)
        {
            var possibleStates = new List<ExpressionStateEnum>();

            foreach (var currentState in currentStates)
            {
                switch (currentState)
                {
                    case ExpressionStateEnum.Numeric:
                        possibleStates.Add(ExpressionStateEnum.Numeric);
                        possibleStates.Add(ExpressionStateEnum.Operator);
                        possibleStates.Add(ExpressionStateEnum.Bracket);
                        possibleStates.Add(ExpressionStateEnum.Exit);
                        break;

                    case ExpressionStateEnum.Operator:
                        possibleStates.Add(ExpressionStateEnum.Numeric);
                        possibleStates.Add(ExpressionStateEnum.Bracket);
                        possibleStates.Add(ExpressionStateEnum.Sign);
                        break;

                    case ExpressionStateEnum.Sign:
                        possibleStates.Add(ExpressionStateEnum.Numeric);
                        possibleStates.Add(ExpressionStateEnum.Bracket);
                        break;

                    case ExpressionStateEnum.Bracket:
                        possibleStates.Add(ExpressionStateEnum.Bracket);
                        possibleStates.Add(ExpressionStateEnum.Numeric);
                        possibleStates.Add(ExpressionStateEnum.Operator);
                        possibleStates.Add(ExpressionStateEnum.Sign);
                        possibleStates.Add(ExpressionStateEnum.Exit);
                        break;

                    case ExpressionStateEnum.Exit:
                        break;
                }
            }

            return possibleStates.Distinct().ToList();
        }
    }

    [TestClass]
    public class TestExpressionEvaluation
    {
        [TestMethod]
        public void StateMachineInvalidExpressionBrackets()
        {
            string input = "((((1)"; // Invalid, too many parens
            const bool expected = false;

            var actual = ExpressionEvaluation.StateMachine(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void StateMachineOperatorSign()
        {
            string input = "1++2"; // Valid, "one plus positive one"
            const bool expected = true;

            var actual = ExpressionEvaluation.StateMachine(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void StateMachineOperatorOperatorSign()
        {
            string input = "1++-2"; // Invalid, "one plus plus negative one"
            const bool expected = false;

            var actual = ExpressionEvaluation.StateMachine(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void StateMachineOperators()
        {
            string input = "1*2/3+5-6"; // Valid, "one times two divided by three plus five minus six"
            const bool expected = true;

            var actual = ExpressionEvaluation.StateMachine(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void StateMachineAllTypes()
        {
            string input = "-(((10/3)*4)+-1)";
            const bool expected = true;

            var actual = ExpressionEvaluation.StateMachine(input);

            Assert.AreEqual(expected, actual);
        }
    }

    // TODO: Add more test cases
}