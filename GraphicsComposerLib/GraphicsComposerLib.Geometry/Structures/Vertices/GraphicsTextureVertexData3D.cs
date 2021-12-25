using System;
using System.Drawing;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Geometry.Primitives.Vertices;

namespace GraphicsComposerLib.Geometry.Structures.Vertices
{
    public sealed class GraphicsTextureVertexData3D 
        : IGraphicsVertexData3D
    {
        public Color Color
        {
            get => Color.Black;
            set => throw new InvalidOperationException();
        }

        public ITuple2D TextureUv { get; set; }

        public GrNormal3D Normal
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
            TextureUv = new Tuple2D(0, 0);
        }

        public GraphicsTextureVertexData3D(ITuple2D textureUv)
        {
            TextureUv = textureUv;
        }

        public GraphicsTextureVertexData3D(IGraphicsVertexData3D vertex)
        {
            TextureUv = vertex.TextureUv;
        }
    }
}