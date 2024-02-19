using System.Text;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Primitives;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Primitives.Triangles;
using TextComposerLib.Code.JavaScript;
using TextComposerLib.Text;
using WebComposerLib.Colors;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Generators;

public sealed class XeoglTrianglesMeshGenerator 
    : XeoglMeshGenerator
{
    private readonly string _itemsSeparator = 
        "," + Environment.NewLine + Environment.NewLine;


    public IGraphicsTriangleGeometry3D BaseGeometry { get; }


    public XeoglTrianglesMeshGenerator(IGraphicsTriangleGeometry3D geometry, string material)
        : base(material)
    {
        BaseGeometry = geometry;
    }


    private static string VertexPositionString(IGraphicsSurfaceLocalFrame3D vertex)
    {
        var point = vertex.Point;

        return new StringBuilder()
            .Append("// Vertex ")
            .AppendLine(vertex.Index.ToString())
            .Append(point.X.ToString("G"))
            .Append(", ")
            .Append(point.Y.ToString("G"))
            .Append(", ")
            .Append(point.Z.ToString("G"))
            .ToString();
    }

    private static string VertexNormalString(IGraphicsSurfaceLocalFrame3D vertex)
    {
        var normal = vertex.Normal;

        return new StringBuilder()
            .Append("// Vertex ")
            .AppendLine(vertex.Index.ToString())
            .Append(normal.X.ToString("G"))
            .Append(", ")
            .Append(normal.Y.ToString("G"))
            .Append(", ")
            .Append(normal.Z.ToString("G"))
            .ToString();
    }

    private static string VertexUvString(IGraphicsSurfaceLocalFrame3D vertex)
    {
        var uv = vertex.ParameterValue;

        return new StringBuilder()
            .Append("// Vertex ")
            .AppendLine(vertex.Index.ToString())
            .Append(uv.Item1.ToString("G"))
            .Append(", ")
            .Append(uv.Item2.ToString("G"))
            .ToString();
    }

    private static string VertexColorString(IGraphicsSurfaceLocalFrame3D vertex)
    {
        var color = vertex.Color.ToSystemDrawingColor().ToJavaScriptRgbaNumbersArrayText();

        return new StringBuilder()
            .Append("// Vertex ")
            .AppendLine(vertex.Index.ToString())
            .Append(color)
            .ToString();
    }


    private void GeneratePositions()
    {
        var positions =
            BaseGeometry
                .GeometryVertices
                .Select(VertexPositionString)
                .Concatenate(_itemsSeparator);

        ScriptComposer
            .AppendLineAtNewLine(@"positions: [")
            .IncreaseIndentation()
            .Append(positions)
            .DecreaseIndentation()
            .AppendLineAtNewLine("],");
    }

    private void GenerateNormals()
    {
        var normals =
            BaseGeometry
                .GeometryVertices
                .Select(VertexNormalString)
                .Concatenate(_itemsSeparator);

        ScriptComposer
            .AppendLineAtNewLine(@"normals: [")
            .IncreaseIndentation()
            .Append(normals)
            .DecreaseIndentation()
            .AppendLineAtNewLine("],");
    }

    private new void GenerateTextureUVs()
    {
        var textureUVs =
            BaseGeometry
                .GeometryVertices
                .Select(VertexUvString)
                .Concatenate(_itemsSeparator);

        ScriptComposer
            .AppendLineAtNewLine(@"uv: [")
            .IncreaseIndentation()
            .Append(textureUVs)
            .DecreaseIndentation()
            .AppendLineAtNewLine("],");
    }

    private void GenerateColors()
    {
        var colors =
            BaseGeometry
                .GeometryVertices
                .Select(VertexColorString)
                .Concatenate(_itemsSeparator);

        ScriptComposer
            .AppendLineAtNewLine(@"colors: [")
            .IncreaseIndentation()
            .Append(colors)
            .DecreaseIndentation()
            .AppendLineAtNewLine("],");
    }

    private void GenerateIndices()
    {
        ScriptComposer
            .AppendLineAtNewLine(@"indices: [")
            .IncreaseIndentation()
            .AppendLineAtNewLine(
                BaseGeometry
                    .GeometryIndices
                    .Select(i => i.ToString())
                    .Concatenate(", ")
            )
            .DecreaseIndentation()
            .AppendLineAtNewLine("]");
    }

    private void GenerateTriangles()
    {
        var separator = "," + Environment.NewLine + Environment.NewLine;

        ScriptComposer
            .AppendAtNewLine(@"primitive: '")
            .Append(PrimitiveTypeNames[(int)BaseGeometry.PrimitiveType])
            .AppendLine("',");

        GeneratePositions();

        if (BaseGeometry.VertexNormalsEnabled)
            GenerateNormals();

        if (BaseGeometry.VertexTextureUVsEnabled)
            GenerateTextureUVs();

        if (BaseGeometry.VertexColorsEnabled)
            GenerateColors();

        GenerateIndices();
    }
        

    protected override void GenerateMeshGeometry()
    {
        ScriptComposer
            .AppendLineAtNewLine(@"geometry: new xeogl.Geometry({")
            .IncreaseIndentation();

        GenerateTriangles();

        ScriptComposer
            .DecreaseIndentation()
            .AppendLineAtNewLine(@"}),");
    }
}