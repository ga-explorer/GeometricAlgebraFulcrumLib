namespace GeometricAlgebraFulcrumLib.Utilities.Text.Generators.ThreeJs;

public sealed class ThreeJsComponentData
{
    public int ConstructorArgumentsCount { get; private set; }

    public List<ThreeJsComponentMemberData> MembersList { get; }
        = new List<ThreeJsComponentMemberData>();

    public string JavaScriptClassName { get; init; }

    public string CSharpClassName { get; init; }

    public string CSharpBaseClassName { get; init; }

    public string CSharpNameSpace { get; init; }


    public ThreeJsComponentData AddConstructorArgument(string memberName, string typeName)
    {
        var memberData = new ThreeJsComponentMemberData()
        {
            CSharpName = memberName,
            CSharpTypeName = typeName,
            GenerateGet = true,
            GenerateSet = true,
            ConstructorArgumentIndex = ConstructorArgumentsCount++
        };

        MembersList.Add(memberData);

        return this;
    }
        
    public ThreeJsComponentData AddConstructorArgumentGetOnly(string memberName, string typeName)
    {
        var memberData = new ThreeJsComponentMemberData()
        {
            CSharpName = memberName,
            CSharpTypeName = typeName,
            GenerateGet = true,
            GenerateSet = false,
            ConstructorArgumentIndex = ConstructorArgumentsCount++
        };

        MembersList.Add(memberData);

        return this;
    }

    public ThreeJsComponentData AddMember(string memberName, string typeName)
    {
        var memberData = new ThreeJsComponentMemberData()
        {
            CSharpName = memberName,
            CSharpTypeName = typeName,
            GenerateGet = true,
            GenerateSet = true,
            ConstructorArgumentIndex = -1
        };

        MembersList.Add(memberData);

        return this;
    }

    public ThreeJsComponentData AddMemberGetOnly(string memberName, string typeName)
    {
        var memberData = new ThreeJsComponentMemberData()
        {
            CSharpName = memberName,
            CSharpTypeName = typeName,
            GenerateGet = true,
            GenerateSet = false,
            ConstructorArgumentIndex = -1
        };

        MembersList.Add(memberData);

        return this;
    }
}