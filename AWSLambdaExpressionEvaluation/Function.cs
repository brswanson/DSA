using Amazon.Lambda.Core;
using DSA.Problems.Done;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWSLambdaExpressionEvaluation
{
    public class Function
    {

        /// <summary>
        ///     Evaluates the passed in arithmetic expression's validity.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>
        ///     bool
        /// </returns>
        public bool FunctionHandler(string input)
        {
            return ExpressionEvaluation.StateMachine(input);
        }
    }
}
