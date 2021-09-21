using System;
using System.Linq;
using System.Text;
using EuclideanGeometryLib.GraphicsGeometry.Triangles;
using EuclideanGeometryLib.GraphicsGeometry.Vertices;
using TextComposerLib.Text;

namespace GraphicsComposerLib.Xeogl.Generators
{
    public sealed class XeoglTrianglesMeshGenerator 
        : XeoglMeshGenerator
    {
        private readonly string _itemsSeparator = 
            "," + Environment.NewLine + Environment.NewLine;


        public IGraphicsTrianglesGeometry3D BaseGeometry { get; }


        public XeoglTrianglesMeshGenerator(IGraphicsTrianglesGeometry3D geometry, string material)
            : base(material)
        {
            BaseGeometry = geometry;
        }


        private static string VertexPositionString(IGraphicsVertex3D vertex)
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

        private static string VertexNormalString(IGraphicsVertex3D vertex)
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

        private static string VertexUvString(IGraphicsVertex3D vertex)
        {
            var uv = vertex.TextureUv;

            return new StringBuilder()
                .Append("// Vertex ")
                .AppendLine(vertex.Index.ToString())
                .Append(uv.Item1.ToString("G"))
                .Append(", ")
                .Append(uv.Item2.ToString("G"))
                .ToString();
        }

        private static string VertexColorString(IGraphicsVertex3D vertex)
        {
            var color = vertex.Color.ToXeoglRgbaNumbersArrayText();

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
                    .Vertices
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
                    .Vertices
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
                    .Vertices
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
                    .Vertices
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
                        .VertexIndices
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

            if (BaseGeometry.ContainsVertexNormals)
                GenerateNormals();

            if (BaseGeometry.ContainsVertexUVs)
                GenerateTextureUVs();

            if (BaseGeometry.ContainsVertexColors)
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
}
