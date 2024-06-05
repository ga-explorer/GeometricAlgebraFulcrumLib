using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.Xeogl.Generators;

public abstract class XeoglMeshGenerator
{
    protected static readonly string[] PrimitiveTypeNames =
    {
        "points",
        "lines", 
        "line-loop", 
        "line-strip",
        "triangles", 
        "triangle-strip", 
        "triangle-fan"
    };


    protected LinearTextComposer ScriptComposer { get; }


    public string MeshVariableName { get; set; }

    public bool GenerateTextureUVs { get; set; }

    public string Material { get; }


    protected XeoglMeshGenerator(string material)
    {
        ScriptComposer = new LinearTextComposer();
        Material = material;
    }


    protected abstract void GenerateMeshGeometry();


    protected virtual void GenerateMaterial()
    {
        ScriptComposer
            .AppendAtNewLine("material: ")
            .AppendLine(Material);
    }


    public virtual string Generate()
    {
        ScriptComposer.Clear();

        if (!string.IsNullOrEmpty(MeshVariableName))
            ScriptComposer.AppendAtNewLine("const " + MeshVariableName + " = ");
        else
            ScriptComposer.AppendAtNewLine();

        ScriptComposer
            .Append(@"new xeogl.Mesh({")
            .IncreaseIndentation();

        GenerateMeshGeometry();

        GenerateMaterial();

        ScriptComposer
            .DecreaseIndentation()
            .AppendLineAtNewLine(@"});")
            .AppendLineAtNewLine();

        return ScriptComposer.ToString();
    }
}