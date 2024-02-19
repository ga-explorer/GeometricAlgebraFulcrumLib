using CodeComposerLib.Irony.Semantic.Expression.Value;
using Microsoft.CSharp.RuntimeBinder;

namespace CodeComposerLib.Irony.DSLInterpreter;

/// <summary>
/// This class is used to evaluate binary operations on two polymorphic ILanguageValue operands to return a suitable
/// ILanguageValue object using multi dispatch function calling. 
/// For example an addition operation may take two integers, two floats, or two strings then return a suitable value
/// </summary>
public abstract class LanguageBasicBinaryDynamicEvaluator
{
    /// <summary>
    /// The UseExceptions flag controls if a missing Visit method throws an exception (true) 
    /// or if it executes the Fallback method (false).
    /// </summary>
    public abstract bool UseExceptions { get; }

    /// <summary>
    /// The fall back method that is called if no matching visit method is found and UseExceptions flag is false
    /// </summary>
    /// <param name="value1"></param>
    /// <param name="value2"></param>
    /// <param name="excException"></param>
    /// <returns></returns>
    public abstract ILanguageValue Fallback(ILanguageValue value1, ILanguageValue value2, RuntimeBinderException excException);
}

public static class LanguageBasicBinaryDynamicEvaluatorExtension
{
    /// <summary>
    /// Dynamic evaluation extension method
    /// </summary>
    /// <param name="value1">The first operand</param>
    /// <param name="binaryOp">The binaty operation</param>
    /// <param name="value2">The second operand</param>
    /// <returns>The value of evaluation the operations on the two operands</returns>
    public static ILanguageValue AcceptOperation(this ILanguageValue value1, LanguageBasicBinaryDynamicEvaluator binaryOp, ILanguageValue value2)
    {
        try
        {
            //Polymorphic Visit call
            return ((dynamic)binaryOp).Evaluate((dynamic)value1, (dynamic)value2);
        }
        catch (RuntimeBinderException excException)
        {
            //Do not use the fallback method
            if (binaryOp.UseExceptions)
                throw;

            //Use the fallback method
            return binaryOp.Fallback(value1, value2, excException);
        }
    }

}