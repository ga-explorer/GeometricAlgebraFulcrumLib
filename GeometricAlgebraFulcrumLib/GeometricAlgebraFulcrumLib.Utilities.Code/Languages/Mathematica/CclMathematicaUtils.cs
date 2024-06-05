namespace GeometricAlgebraFulcrumLib.Utilities.Code.Languages.Mathematica;

public static class CclMathematicaUtils
{
    public static CclLanguageInfo Mathematica7Info { get; private set; }


    static CclMathematicaUtils()
    {
        Mathematica7Info = new CclLanguageInfo("Mathematica", "7.0", "Mathematica 7.0");
    }


}