using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.Json;
using DSA.Problems.Done;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(JsonSerializer))]

namespace AWSLambda
{
    public class ExpressionEvaluationFunction
    {
        /// <summary>
        ///     Evaluates the passed in arithmetic expression's validity.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns>
        ///     bool
        /// </returns>
        public bool FunctionHandler(string input, ILambdaContext context)
        {
            return ExpressionEvaluation.StateMachine(input);
        }
    }
}