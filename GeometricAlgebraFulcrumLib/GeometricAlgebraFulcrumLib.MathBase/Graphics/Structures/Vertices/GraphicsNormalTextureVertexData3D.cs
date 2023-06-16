using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Structures.Vertices
{
    public sealed class GraphicsNormalTextureVertexData3D 
        : IGraphicsVertexData3D
    {
        public Color Color
        {
            get => Color.Black;
            set => throw new InvalidOperationException();
        }

        public IFloat64Tuple2D TextureUv { get; set; }

        public Normal3D Normal { get; }
            = new Normal3D();
        
        public double TextureU 
            => TextureUv.X;

        public double TextureV 
            => TextureUv.Y;

        public double NormalX 
            => Normal.X;

        public double NormalY 
            => Normal.Y;

        public double NormalZ 
            => Normal.Z;

        public bool HasColor 
            => false;

        public bool HasTextureUv 
            => true;

        public bool HasNormal 
            => true;

        public GraphicsVertexDataKind3D DataKind
            => GraphicsVertexDataKind3D.NormalTextureData;
        

        public GraphicsNormalTextureVertexData3D()
        {
            TextureUv = new Float64Vector2D(0, 0);
        }

        public GraphicsNormalTextureVertexData3D(IFloat64Tuple2D textureUv)
        {
            TextureUv = textureUv;
        }

        public GraphicsNormalTextureVertexData3D(IFloat64Tuple2D textureUv, IFloat64Tuple3D normal)
        {
            TextureUv = textureUv;
            Normal.Set(normal);
        }

        public GraphicsNormalTextureVertexData3D(IGraphicsVertexData3D vertex)
        {
            TextureUv = vertex.TextureUv;
            Normal.Set(vertex.Normal);
        }
    }
}
