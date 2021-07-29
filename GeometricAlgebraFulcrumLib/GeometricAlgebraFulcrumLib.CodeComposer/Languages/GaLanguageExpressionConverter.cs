using CodeComposerLib.Languages;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Languages
{
    public abstract class GaLanguageExpressionConverter : 
        CclLanguageExpressionConverterBase
    {
        /// <summary>
        /// The code block containing a dictionary used to convert low-level variables names into 
        /// target code variables names to be used during expression conversion to target code
        /// </summary>
        public SymbolicContext ActiveContext { get; set; }


        protected GaLanguageExpressionConverter(CclLanguageInfo sourceLanguageInfo, CclLanguageInfo targetLanguageInfo)
            : base(sourceLanguageInfo, targetLanguageInfo)
        {
            
        }
    }
}