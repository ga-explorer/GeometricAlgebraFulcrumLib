using System;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Frames.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using SixLabors.ImageSharp;

namespace GraphicsComposerLib.Geometry.Structures.Vertices
{
    public sealed class GraphicsNormalColorVertexData3D 
        : IGraphicsVertexData3D
    {
        public Color Color { get; set; }
            = Color.Black;

        public IFloat64Tuple2D TextureUv
        {
            get => Float64Tuple2D.Zero;
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

        public GraphicsNormalColorVertexData3D(Color color, IFloat64Tuple3D normal)
        {
            Color = color;
            Normal.Set(normal);
        }

        public GraphicsNormalColorVertexData3D(IGraphicsVertexData3D point)
        {
            Color = point.Color;
        }


        public Float64Tuple3D GetDisplacedPoint(IFloat64Tuple3D point, double d)
        {
            return new Float64Tuple3D(
                point.X + d * Normal.X,
                point.Y + d * Normal.Y,
                point.Z + d * Normal.Z
            );
        }
    }
}