namespace CodeComposerLib.Languages.GMacDSL
{
    public static class GMacDslUtils
    {
        public static LanguageInfo GMacDslInfo { get; private set; }


        static GMacDslUtils()
        {
            GMacDslInfo = new LanguageInfo("GMacDSL", "1.0", "GMacDSL 1.0");
        }


        public static GMacDslCodeGenerator GMacDslCodeGenerator()
        {
            return new GMacDslCodeGenerator();
        }

        public static GMacDslSyntaxFactory GMacDslSyntaxFactory()
        {
            return new GMacDslSyntaxFactory();
        }

    }
}
