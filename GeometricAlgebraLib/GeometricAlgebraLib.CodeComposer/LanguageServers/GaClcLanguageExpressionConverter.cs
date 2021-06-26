using CodeComposerLib.Languages;
using GeometricAlgebraLib.SymbolicExpressions.Context;

namespace GeometricAlgebraLib.CodeComposer.LanguageServers
{
    public abstract class GaClcLanguageExpressionConverter : 
        LanguageExpressionConverter
    {
        /// <summary>
        /// The code block containing a dictionary used to convert low-level variables names into 
        /// target code variables names to be used during expression conversion to target code
        /// </summary>
        public SymbolicContext ActiveContext { get; set; }


        protected GaClcLanguageExpressionConverter(LanguageInfo sourceLanguageInfo, LanguageInfo targetLanguageInfo)
            : base(sourceLanguageInfo, targetLanguageInfo)
        {
            
        }
    }
}