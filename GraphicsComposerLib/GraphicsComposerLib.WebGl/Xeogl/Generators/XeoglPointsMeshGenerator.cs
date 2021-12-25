using GraphicsComposerLib.Geometry.Primitives.Points;

namespace GraphicsComposerLib.WebGl.Xeogl.Generators
{
    public sealed class XeoglPointsMeshGenerator : XeoglMeshGenerator
    {
        public IGraphicsPointGeometry3D BaseGeometry { get; }


        public XeoglPointsMeshGenerator(IGraphicsPointGeometry3D baseGeometry, string material)
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