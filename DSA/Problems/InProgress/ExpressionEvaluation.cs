using System;
using System.Collections.Generic;
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
        private static bool IsOpeningBracket(char c) => new List<char> { '(', '{' }.Contains(c);
        private static bool IsClosingBracket(char c) => new List<char> { ')', '}' }.Contains(c);

        public static bool StateMachine(string expression)
        {
            // Time:    O(n).   One pass over the entire string from left to right. Evaluates the current state of the expression and compares it to potential valid states.
            // Memory:  O(1).   A constant amount of memory is used to keep track of potential valid states.

            int bracketCount = 0;

            foreach (var c in expression)
            {
                // TODO: Implement the state machine logic for evaluation the expression

                // TODO: Keep a Stack of brackets and compare the types on Pop instead of a count
                UpdateBracketCount(ref bracketCount, c);
                // Return early if the bracket count is off
                if (bracketCount < 0) return false;
            }

            // Check to see if all brackets have a matching pair before returning true
            if (bracketCount != 0) return false;

            return true;
        }

        private static void UpdateBracketCount(ref int bracketCount, char bracket)
        {
            if (IsOpeningBracket(bracket)) bracketCount++;
            if (IsClosingBracket(bracket)) bracketCount--;
        }
    }

    public static class ExpressionStateMachine
    {
        public static List<ExpressionStateEnum> GetPotentialStates(ExpressionStateEnum currentState)
        {
            var possibleStates = new List<ExpressionStateEnum>();

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

            return possibleStates;
        }
    }

    public enum ExpressionStateEnum
    {
        Numeric,
        Operator,
        Sign,
        Bracket,
        Exit
    }

    [TestClass]
    public class TestExpressionEvaluation
    {
        [TestMethod]
        public void StateMachineInvalidExpressionBrackets()
        {
            string input = "((((1)";
            const bool expected = false;

            var actual = ExpressionEvaluation.StateMachine(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void StateMachineInvalidExpressionOperators()
        {
            string input = "1++2";
            const bool expected = false;

            var actual = ExpressionEvaluation.StateMachine(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void StateMachineOperators()
        {
            string input = "1*2/3+5-6";
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