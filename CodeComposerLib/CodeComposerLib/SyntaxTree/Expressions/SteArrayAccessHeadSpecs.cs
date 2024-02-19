namespace CodeComposerLib.SyntaxTree.Expressions;

public class SteArrayAccessHeadSpecs : ISteCompositeHeadSpecs
{
    public string ArrayName { get; }

    public string HeadText => ArrayName;


    public SteArrayAccessHeadSpecs(string arrayName)
    {
        ArrayName = arrayName;
    }


    public override string ToString()
    {
        return ArrayName;
    }
}