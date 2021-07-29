using CodeComposerLib.Languages;
using CodeComposerLib.Languages.Mathematica;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Languages
{
    public abstract class GaMathematicaExprConverter : 
        GaLanguageExpressionConverter
    {
        protected GaMathematicaExprConverter(CclLanguageInfo targetLanguageInfo)
            : base(CclMathematicaUtils.Mathematica7Info, targetLanguageInfo)
        {
            
        }
    }
}
