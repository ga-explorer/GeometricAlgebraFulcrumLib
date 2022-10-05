using GraphicsComposerLib.Rendering.LaTeX.KaTeX.Expressions;

namespace GraphicsComposerLib.Rendering.LaTeX.KaTeX
{
    public static class KaTeXAccents
    {
        public static KaTeXOneArgFunction Tilde()
        {
            return new KaTeXOneArgFunction(
                @"\tilde{texArg1}"
            );
        }

        public static KaTeXOneArgFunction Tilde(this IKaTeXExpression arg)
        {
            return new KaTeXOneArgFunction(
                @"\tilde{texArg1}"
            ){ Argument = arg };
        }


    }
}
