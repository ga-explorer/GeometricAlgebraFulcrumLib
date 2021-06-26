using CodeComposerLib.Languages;
using CodeComposerLib.Languages.Mathematica;

namespace GeometricAlgebraLib.CodeComposer.LanguageServers
{
    public abstract class GaClcMathematicaExprConverter : 
        GaClcLanguageExpressionConverter
    {
        protected GaClcMathematicaExprConverter(LanguageInfo targetLanguageInfo)
            : base(MathematicaUtils.Mathematica7Info, targetLanguageInfo)
        {
            
        }
    }
}
