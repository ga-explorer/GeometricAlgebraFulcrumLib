using GeometricAlgebraFulcrumLib.Utilities.Code.SyntaxTree.Expressions;
using GeometricAlgebraFulcrumLib.Utilities.Structures;
using Microsoft.CSharp.RuntimeBinder;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.Languages;

public abstract class CclExpressionToLanguageConverterBase<T> :
    ICclExpressionToLanguageConverter<T> where T : class
{
    public CclLanguageInfo TargetLanguageInfo { get; }
        
    public bool UseExceptions { get; set; }

    public bool IgnoreNullElements { get; set; }


    protected CclExpressionToLanguageConverterBase(CclLanguageInfo targetLanguageInfo)
    {
        TargetLanguageInfo = targetLanguageInfo;
    }


    public abstract SteExpression Fallback(T item, RuntimeBinderException excException);
        
    public IEnumerable<SteExpression> Convert(IEnumerable<T> exprList)
    {
        return exprList.Select(expr => expr.AcceptVisitor(this));
    }

    public SteExpression Convert(T expr)
    {
        return expr.AcceptVisitor(this);
    }
}