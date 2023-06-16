using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Structures.Vertices
{
    public sealed class GraphicsNormalVertexData3D 
        : IGraphicsVertexData3D
    {
        public Color Color
        {
            get => Color.Black;
            set => throw new InvalidOperationException();
        }

        public IFloat64Tuple2D TextureUv
        {
            get => Float64Vector2D.Zero;
            set => throw new InvalidOperationException();
        }

        public Normal3D Normal { get; }
            = new Normal3D();
        
        public double TextureU 
            => 0;

        public double TextureV 
            => 0;

        public double NormalX 
            => Normal.X;

        public double NormalY 
            => Normal.Y;

        public double NormalZ 
            => Normal.Z;

        public bool HasColor 
            => false;

        public bool HasTextureUv 
            => false;

        public bool HasNormal 
            => true;

        public GraphicsVertexDataKind3D DataKind
            => GraphicsVertexDataKind3D.NormalData;
        

        public GraphicsNormalVertexData3D()
        {
        }

        public GraphicsNormalVertexData3D(IFloat64Tuple3D normal)
        {
            Normal.Set(normal);
        }

        public GraphicsNormalVertexData3D(IGraphicsVertexData3D vertex)
        {
            Normal.Set(vertex.Normal);
        }

        
        public Float64Vector3D GetDisplacedPoint(IFloat64Tuple3D point, double d)
        {
            return Float64Vector3D.Create(point.X + d * Normal.X,
                point.Y + d * Normal.Y,
                point.Z + d * Normal.Z);
        }
    }
}