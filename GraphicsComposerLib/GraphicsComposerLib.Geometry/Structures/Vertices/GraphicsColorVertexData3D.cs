using System;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Frames.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using SixLabors.ImageSharp;

namespace GraphicsComposerLib.Geometry.Structures.Vertices
{
    public sealed class GraphicsColorVertexData3D 
        : IGraphicsVertexData3D
    {
        public Color Color { get; set; }
            = Color.Black;

        public IFloat64Tuple2D TextureUv
        {
            get => Float64Tuple2D.Zero;
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