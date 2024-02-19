namespace CodeComposerLib.Languages.GMacDSL;

public static class CclGMacDslUtils
{
    public static CclLanguageInfo GMacDslInfo { get; private set; }


    static CclGMacDslUtils()
    {
        GMacDslInfo = new CclLanguageInfo("GMacDSL", "1.0", "GMacDSL 1.0");
    }


    public static CclGMacDslCodeGenerator GMacDslCodeComposer()
    {
        return new CclGMacDslCodeGenerator();
    }

    public static CclGMacDslSyntaxFactory GMacDslSyntaxFactory()
    {
        return new CclGMacDslSyntaxFactory();
    }

}