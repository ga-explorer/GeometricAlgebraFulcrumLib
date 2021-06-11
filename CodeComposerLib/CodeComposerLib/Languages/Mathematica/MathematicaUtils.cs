namespace CodeComposerLib.Languages.Mathematica
{
    public static class MathematicaUtils
    {
        public static LanguageInfo Mathematica7Info { get; private set; }


        static MathematicaUtils()
        {
            Mathematica7Info = new LanguageInfo("Mathematica", "7.0", "Mathematica 7.0");
        }


    }
}
