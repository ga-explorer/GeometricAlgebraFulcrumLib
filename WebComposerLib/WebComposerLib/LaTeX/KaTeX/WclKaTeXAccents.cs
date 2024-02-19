using WebComposerLib.LaTeX.KaTeX.Expressions;

namespace WebComposerLib.LaTeX.KaTeX;

public static class WclKaTeXAccents
{
    public static WclKaTeXOneArgFunction Tilde()
    {
        return new WclKaTeXOneArgFunction(
            @"\tilde{texArg1}"
        );
    }

    public static WclKaTeXOneArgFunction Tilde(this IWclKaTeXExpression arg)
    {
        return new WclKaTeXOneArgFunction(
            @"\tilde{texArg1}"
        ){ Argument = arg };
    }


}