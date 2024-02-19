using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Primitives.Lines;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Generators;

public sealed class XeoglLinesMeshGenerator : XeoglMeshGenerator
{
    public IGraphicsLineGeometry3D BaseGeometry { get; }


    public XeoglLinesMeshGenerator(IGraphicsLineGeometry3D baseGeometry, string material)
        : base(material)
    {
        BaseGeometry = baseGeometry;
    }

        
    private void GeneratePositions()
    {
        ScriptComposer
            .AppendLineAtNewLine("positions: [")
            .IncreaseIndentation();

        var isFirstFlag = true;
        foreach (var vertex in BaseGeometry.GeometryPoints)
        {
            if (!isFirstFlag)
                ScriptComposer.AppendLine(", ");
            else
                isFirstFlag = false;

            ScriptComposer
                .AppendAtNewLine(vertex.X.ToString("G"))
                .Append(", ")
                .Append(vertex.Y.ToString("G"))
                .Append(", ")
                .Append(vertex.Z.ToString("G"));
        }

        ScriptComposer
            .DecreaseIndentation()
            .AppendLineAtNewLine(@"],");
    }

    private void GenerateIndices()
    {
        ScriptComposer
            .AppendLineAtNewLine("indices: [")
            .IncreaseIndentation();

        ScriptComposer.AppendLineAtNewLine(
            BaseGeometry
                .LineVertexIndices
                .SelectMany(i => i.GetItems())
                .Concatenate(", ")
        );

        ScriptComposer
            .DecreaseIndentation()
            .AppendLineAtNewLine("]");
    }


    protected override void GenerateMeshGeometry()
    {
        ScriptComposer
            .AppendLineAtNewLine(@"geometry: new xeogl.Geometry({")
            .IncreaseIndentation();

        ScriptComposer
            .AppendAtNewLine(@"primitive: '")
            .Append(PrimitiveTypeNames[(int)BaseGeometry.PrimitiveType])
            .AppendLine("',");

        GeneratePositions();

        GenerateIndices();

        ScriptComposer
            .DecreaseIndentation()
            .AppendLineAtNewLine(@"}),");
    }
}