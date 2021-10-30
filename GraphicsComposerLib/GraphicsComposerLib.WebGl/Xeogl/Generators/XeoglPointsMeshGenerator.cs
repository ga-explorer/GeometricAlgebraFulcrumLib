using EuclideanGeometryLib.GraphicsGeometry.Points;

namespace GraphicsComposerLib.WebGl.Xeogl.Generators
{
    public sealed class XeoglPointsMeshGenerator : XeoglMeshGenerator
    {
        public IGraphicsPointsGeometry3D BaseGeometry { get; }


        public XeoglPointsMeshGenerator(IGraphicsPointsGeometry3D baseGeometry, string material)
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

            ScriptComposer
                .DecreaseIndentation()
                .AppendLineAtNewLine(@"}),");
        }
    }
}