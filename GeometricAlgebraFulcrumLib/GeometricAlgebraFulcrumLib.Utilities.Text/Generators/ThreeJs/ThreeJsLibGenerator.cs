using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Utilities.Text.Generators.ThreeJs;

public sealed class ThreeJsLibGenerator
{



    private readonly Dictionary<string, string> _typeMappingDictionary;

    public string NameSpace { get; }
        = "namespace GraphicsComposerLib.WebGl.ThreeJs.CodeComposer";

    public LinearTextComposer TextComposer { get; }
        = new LinearTextComposer();

    public List<ThreeJsComponentData> ComponentsList { get; }
        = new List<ThreeJsComponentData>();


    public ThreeJsLibGenerator()
    {
        _typeMappingDictionary = new Dictionary<string, string>()
        {
            {"Number", "TjNumber"},
            {"Float", "TjNumber"},
            {"Integer", "TjNumber"},
            {"Boolean", "TjBoolean"},
            {"String", "TjString"},
        };


    }

    private void InitializeComponentsData()
    {
        var nameSpace = "Math";

        AddComponent(nameSpace, "TjComponent", "TjVector3")
            .AddConstructorArgument("x", "TjNumber")
            .AddConstructorArgument("y", "TjNumber")
            .AddConstructorArgument("z", "TjNumber");

        nameSpace = "Cameras";

        //AddComponent(nameSpace, "TjCamera", "PerspectiveCamera")
        //    .AddConstructorArgument("fov", "VerticalFieldOfView", "TjNumber");
    }


    public ThreeJsComponentData AddComponent(string nameSpace, string baseClassName, string className)
    {
        var component = new ThreeJsComponentData()
        {
            CSharpNameSpace = nameSpace,
            CSharpBaseClassName = baseClassName,
            CSharpClassName = "Tj" + className,
            JavaScriptClassName = className
        };

        ComponentsList.Add(component);

        return component;
    }


}