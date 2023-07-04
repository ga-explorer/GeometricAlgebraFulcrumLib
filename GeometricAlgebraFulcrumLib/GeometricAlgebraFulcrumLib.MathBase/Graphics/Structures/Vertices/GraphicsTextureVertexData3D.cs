using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Structures.Vertices
{
    public sealed class GraphicsTextureVertexData3D 
        : IGraphicsVertexData3D
    {
        public Color Color
        {
            get => Color.Black;
            set => throw new InvalidOperationException();
        }

        public IFloat64Vector2D TextureUv { get; set; }

        public Normal3D Normal
            => null;

        public bool HasColor 
            => false;

        public bool HasTextureUv 
            => true;

        public bool HasNormal 
            => false;

        public GraphicsVertexDataKind3D DataKind
            => GraphicsVertexDataKind3D.TextureData;
        
        public double TextureU 
            => TextureUv.X;

        public double TextureV 
            => TextureUv.Y;

        public double NormalX 
            => 0;

        public double NormalY 
            => 0;

        public double NormalZ 
            => 0;
        

        public GraphicsTextureVertexData3D()
        {
            TextureUv = Float64Vector2D.Create((Float64Scalar)0, 0);
        }

        public GraphicsTextureVertexData3D(IFloat64Vector2D textureUv)
        {
            TextureUv = textureUv;
        }

        public GraphicsTextureVertexData3D(IGraphicsVertexData3D vertex)
        {
            TextureUv = vertex.TextureUv;
        }
    }
}