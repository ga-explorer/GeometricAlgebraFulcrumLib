using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.SyntaxTree;
using CodeComposerLib.SyntaxTree.Expressions;
using Microsoft.CSharp.RuntimeBinder;

namespace CodeComposerLib.Languages
{
    public abstract class CclLanguageExpressionConverterBase : 
        ICclLanguageSyntaxConverter
    {
        public CclLanguageInfo SourceLanguageInfo { get; }

        public CclLanguageInfo TargetLanguageInfo { get; }

        public bool UseExceptions { get; set; }

        public bool IgnoreNullElements { get; set; }


        protected CclLanguageExpressionConverterBase(CclLanguageInfo srcInfo, CclLanguageInfo trgtInfo)
        {
            SourceLanguageInfo = srcInfo;
            TargetLanguageInfo = trgtInfo;
        }


        public virtual ISyntaxTreeElement Fallback(ISyntaxTreeElement objItem, RuntimeBinderException excException)
        {
            return objItem;
        }


        public virtual ISyntaxTreeElement Visit(SteExpression expr)
        {
            return Convert(expr);
        }

        public IEnumerable<SteExpression> Convert(IEnumerable<SteExpression> exprList)
        {
            return exprList.Select(Convert);
        }

        public abstract SteExpression Convert(SteExpression expr);

        public abstract SteExpression Convert(SteExpression expr, IDictionary<string, string> targetVarsDictionary);
    }
}
