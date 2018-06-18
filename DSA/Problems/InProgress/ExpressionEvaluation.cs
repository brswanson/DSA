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
            // Memory:  O(1).   A constant amount of memory is used to keep track of potential valid states and bracket counts.

            // Treating null and empty string expressions as true
            if (string.IsNullOrEmpty(expression)) return true;

            var bracketStack = new Stack<char>();
            var validNextStates = ExpressionStateMachine.GetAllStates();

            for (var i = 0; i < expression.Length; i++)
            {
                // Check brackets first for possible early return
                if (BracketsInvalid(bracketStack, expression[i])) return false;

                var currentPossibleStates = ExpressionStateMachine.GetStates(expression[i]);

                // Check current state validity against previous valid states
                if (StateInvalid(validNextStates, currentPossibleStates)) return false;
                // Use the possible states to get a list of all valid states
                validNextStates = ExpressionStateMachine.GetValidStates(currentPossibleStates);
            }

            // Check to see if all brackets have a matching pair before returning true
            if (bracketStack.Count != 0) return false;

            return validNextStates.Contains(ExpressionStateMachine.ExpressionStateEnum.Exit);
        }

        private static bool StateInvalid(List<ExpressionStateMachine.ExpressionStateEnum> validStates, List<ExpressionStateMachine.ExpressionStateEnum> currentStates)
        {
            // If any of the current states is valid for the previous possible states, continue
            return !validStates.Intersect(currentStates).Any();
        }

        private static bool BracketsInvalid(Stack<char> bracketCount, char bracket)
        {
            if (ExpressionStateMachine.IsOpeningBracket(bracket)) bracketCount.Push(bracket);
            if (ExpressionStateMachine.IsClosingBracket(bracket))
            {
                var lastBracket = bracketCount.Pop();

                if (bracket == ')' && lastBracket != '(') return true;
                if (bracket == '}' && lastBracket != '{') return true;
            }

            return false;
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
        public void BracketsNegative()
        {
            string input = "((((1)"; // Invalid, too many parens
            const bool expected = false;

            var actual = ExpressionEvaluation.StateMachine(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BracketsPositive()
        {
            string input = "(2)"; // Valid, "two"
            const bool expected = true;

            var actual = ExpressionEvaluation.StateMachine(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BracketsMultipleTypesPositive()
        {
            string input = "{(2)}"; // Valid, "two"
            const bool expected = true;

            var actual = ExpressionEvaluation.StateMachine(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BracketsMultipleTypesNegative()
        {
            string input = "{(2))"; // Invalid, "two"
            const bool expected = false;

            var actual = ExpressionEvaluation.StateMachine(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void OperatorSign()
        {
            string input = "1++2"; // Valid, "one plus positive one"
            const bool expected = true;

            var actual = ExpressionEvaluation.StateMachine(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void OperatorOperatorSign()
        {
            string input = "1---2"; // Invalid, "one minus minus negative one"
            const bool expected = false;

            var actual = ExpressionEvaluation.StateMachine(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Operators()
        {
            string input = "1*2/3+5-6"; // Valid, "one times two divided by three plus five minus six"
            const bool expected = true;

            var actual = ExpressionEvaluation.StateMachine(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AllTypes()
        {
            string input = "-(((10/3)*4)+-1)";
            const bool expected = true;

            var actual = ExpressionEvaluation.StateMachine(input);

            Assert.AreEqual(expected, actual);
        }
    }

    // TODO: Add more test cases
}