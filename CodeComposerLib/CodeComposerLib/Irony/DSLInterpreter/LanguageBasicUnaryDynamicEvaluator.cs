using CodeComposerLib.Irony.Semantic.Expression.Value;
using Microsoft.CSharp.RuntimeBinder;

namespace CodeComposerLib.Irony.DSLInterpreter;

public abstract class LanguageBasicUnaryDynamicEvaluator
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
    /// <param name="excException"></param>
    public abstract ILanguageValue Fallback(ILanguageValue value1, RuntimeBinderException excException);
}

public static class LanguageBasicUnaryDynamicEvaluatorExtension
{
    /// <summary>
    /// Dynamic evaluation extension method
    /// </summary>
    /// <param name="value1">The first operand</param>
    /// <param name="unaryOp">The binaty operation</param>
    /// <returns>The value of evaluation the operations on the two operands</returns>
    public static ILanguageValue AcceptOperation(this ILanguageValue value1, LanguageBasicUnaryDynamicEvaluator unaryOp)
    {
        try
        {
            //Polymorphic Visit call
            return ((dynamic)unaryOp).Evaluate((dynamic)value1);
        }
        catch (RuntimeBinderException excException)
        {
            //Do not use the fallback method
            if (unaryOp.UseExceptions)
                throw;

            //Use the fallback method
            return unaryOp.Fallback(value1, excException);
        }
    }
}