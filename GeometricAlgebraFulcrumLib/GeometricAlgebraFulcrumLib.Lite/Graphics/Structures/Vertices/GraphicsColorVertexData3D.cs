using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Structures.Vertices
{
    public sealed class GraphicsColorVertexData3D 
        : IGraphicsVertexData3D
    {
        public Color Color { get; set; }
            = Color.Black;

        public IFloat64Vector2D TextureUv
        {
            get => Float64Vector2D.Zero;
            set => throw new InvalidOperationException();
        }

        public Normal3D Normal
            => null;

        public bool HasColor 
            => true;

        public bool HasTextureUv 
            => false;

        public bool HasNormal 
            => false;

        public GraphicsVertexDataKind3D DataKind
            => GraphicsVertexDataKind3D.ColorData;
        
        public double TextureU 
            => 0;

        public double TextureV 
            => 0;

        public double NormalX 
            => 0;

        public double NormalY 
            => 0;

        public double NormalZ 
            => 0;
        

        public GraphicsColorVertexData3D()
        {
        }

        public GraphicsColorVertexData3D(Color color)
        {
            Color = color;
        }

        public GraphicsColorVertexData3D(IGraphicsVertexData3D vertex)
        {
            Color = vertex.Color;
        }
    }
}