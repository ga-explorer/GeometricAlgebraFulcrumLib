using System.Linq;
using EuclideanGeometryLib.GraphicsGeometry.Lines;
using TextComposerLib.Text;

namespace GraphicsComposerLib.WebGl.Xeogl.Generators
{
    public sealed class XeoglLinesMeshGenerator : XeoglMeshGenerator
    {
        public IGraphicsLinesGeometry3D BaseGeometry { get; }


        public XeoglLinesMeshGenerator(IGraphicsLinesGeometry3D baseGeometry, string material)
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
            foreach (var vertex in BaseGeometry.VertexPoints)
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
                BaseGeometry.LineVerticesIndices.Select(i => i.ToString()).Concatenate(", ")
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
}
