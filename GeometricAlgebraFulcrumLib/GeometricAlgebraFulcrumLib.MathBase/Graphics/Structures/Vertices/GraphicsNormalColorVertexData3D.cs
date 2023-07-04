using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Structures.Vertices
{
    public sealed class GraphicsNormalColorVertexData3D 
        : IGraphicsVertexData3D
    {
        public Color Color { get; set; }
            = Color.Black;

        public IFloat64Vector2D TextureUv
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
            => true;

        public bool HasTextureUv 
            => false;

        public bool HasNormal 
            => true;

        public GraphicsVertexDataKind3D DataKind
            => GraphicsVertexDataKind3D.NormalColorData;
        

        public GraphicsNormalColorVertexData3D()
        {
        }

        public GraphicsNormalColorVertexData3D(Color color)
        {
            Color = color;
        }

        public GraphicsNormalColorVertexData3D(Color color, IFloat64Vector3D normal)
        {
            Color = color;
            Normal.Set(normal);
        }

        public GraphicsNormalColorVertexData3D(IGraphicsVertexData3D point)
        {
            Color = point.Color;
        }


        public Float64Vector3D GetDisplacedPoint(IFloat64Vector3D point, double d)
        {
            return Float64Vector3D.Create(point.X + d * Normal.X,
                point.Y + d * Normal.Y,
                point.Z + d * Normal.Z);
        }
    }
}