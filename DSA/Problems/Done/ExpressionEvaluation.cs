using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSA.Problems.Done
{
    /// <summary>
    ///     Evaluate the validity of the passed in mathematical expression.
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
            // Time:    O(n).   One pass over the entire string from left to right. Evaluates the state of each character against the previous character's valid state transitions.
            // Memory:  O(1).   A constant amount of memory is used to keep track of valid states and bracket counts.

            // Treating null and empty string expressions as true
            if (string.IsNullOrEmpty(expression)) return true;

            var bracketStack = new Stack<char>();
            var validStateTransitions = ExpressionStateMachine.GetStateTransitions();

            foreach (var currentChar in expression)
            {
                // Check brackets first for possible early return
                if (BracketsInvalid(bracketStack, currentChar)) return false;

                // Get all possible states for the current character
                var currentStates = ExpressionStateMachine.GetStates(currentChar);

                // Get the next valid state, if it exists
                var nextState = GetNextValidState(validStateTransitions, currentStates);
                if (nextState == ExpressionStateMachine.ExpressionStateEnum.Invalid) return false;

                // Set the collection of valid state transitions for the current state
                validStateTransitions = ExpressionStateMachine.GetStateTransitions(nextState);
            }

            // Check to see if all brackets have a matching pair before returning true
            if (bracketStack.Count != 0) return false;

            // Check for the Exit state on return in case the expression ends with an invalid state
            return validStateTransitions.Contains(ExpressionStateMachine.ExpressionStateEnum.Exit);
        }

        private static ExpressionStateMachine.ExpressionStateEnum GetNextValidState(
            List<ExpressionStateMachine.ExpressionStateEnum> validStates,
            List<ExpressionStateMachine.ExpressionStateEnum> currentStates)
        {
            var nextStatePossabilities = validStates.Intersect(currentStates).ToList();

            return nextStatePossabilities.Count > 0
                ? nextStatePossabilities.First()
                : ExpressionStateMachine.ExpressionStateEnum.Invalid;
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
        public static bool IsOperator(char c) => new List<char> {'+', '-', '/', '*'}.Contains(c);
        public static bool IsSign(char c) => new List<char> {'+', '-'}.Contains(c);
        public static bool IsOpeningBracket(char c) => new List<char> {'(', '{'}.Contains(c);
        public static bool IsClosingBracket(char c) => new List<char> {')', '}'}.Contains(c);

        public enum ExpressionStateEnum
        {
            Start,
            Numeric,
            Operator,
            Sign,
            Bracket,
            Exit,
            Invalid
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

        public static List<ExpressionStateEnum> GetStateTransitions(ExpressionStateEnum state = ExpressionStateEnum.Start)
        {
            var possibleStates = new List<ExpressionStateEnum>();

            switch (state)
            {
                case ExpressionStateEnum.Start:
                    possibleStates.Add(ExpressionStateEnum.Numeric);
                    possibleStates.Add(ExpressionStateEnum.Sign);
                    possibleStates.Add(ExpressionStateEnum.Bracket);
                    possibleStates.Add(ExpressionStateEnum.Exit);
                    break;

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

            return possibleStates;
        }
    }

    [TestClass]
    public class TestExpressionEvaluation
    {
        #region  Number

        [TestMethod]
        public void Number()
        {
            string input = "5"; // Valid, "five"
            const bool expected = true;

            var actual = ExpressionEvaluation.StateMachine(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NumberNumber()
        {
            string input = "55"; // Valid, "fifty five"
            const bool expected = true;

            var actual = ExpressionEvaluation.StateMachine(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NumberBracket()
        {
            string input = "55(5)"; // Valid, "fifty five times five"
            const bool expected = true;

            var actual = ExpressionEvaluation.StateMachine(input);

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Operator

        [TestMethod]
        public void OperatorsAll()
        {
            string input = "1*2/3+5-6"; // Valid, "one times two divided by three plus five minus six"
            const bool expected = true;

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

        #endregion

        #region Bracket

        [TestMethod]
        public void BracketsCountPositive()
        {
            string input = "(2)"; // Valid, "two"
            const bool expected = true;

            var actual = ExpressionEvaluation.StateMachine(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BracketsCountNegative()
        {
            string input = "((((1)"; // Invalid, too many parens
            const bool expected = false;

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

        #endregion

        #region Sign

        [TestMethod]
        public void SignsPositive()
        {
            string input = "-5"; // Valid, "negative five"
            const bool expected = true;

            var actual = ExpressionEvaluation.StateMachine(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SignsNegative()
        {
            string input = "--5"; // Invalid, "negative negative five"
            const bool expected = false;

            var actual = ExpressionEvaluation.StateMachine(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SignSignNumber()
        {
            string input = "--5"; // Invalid, "minus negative five"
            const bool expected = false;

            var actual = ExpressionEvaluation.StateMachine(input);

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Exit

        [TestMethod]
        public void Exit()
        {
            string input = ""; // Valid, ""
            const bool expected = true;

            var actual = ExpressionEvaluation.StateMachine(input);

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region General

        [TestMethod]
        public void AllTypes()
        {
            string input = "-(((10/3)*4)+-1)"; // Valid, "negative ten divided by three times four plus negative one"
            const bool expected = true;

            var actual = ExpressionEvaluation.StateMachine(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AllTypesTwo()
        {
            string input = "-5(5*-5+-5)"; // Valid, "negative five times five times negative five plus negative five"
            const bool expected = true;

            var actual = ExpressionEvaluation.StateMachine(input);

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}