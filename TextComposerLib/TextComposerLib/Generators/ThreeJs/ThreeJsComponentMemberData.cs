namespace TextComposerLib.Generators.ThreeJs;

public sealed class ThreeJsComponentMemberData
{
    public string JavaScriptName { get; init; }

    public string CSharpName { get; init; }

    public string CSharpTypeName { get; init; }

    public bool GenerateGet { get; init; }

    public bool GenerateSet { get; init; }

    public int ConstructorArgumentIndex { get; init; }

    public bool IsConstructorArgument 
        => ConstructorArgumentIndex >= 0;

}