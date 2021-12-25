using System;
using System.Drawing;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Geometry.Primitives.Vertices;

namespace GraphicsComposerLib.Geometry.Structures.Vertices
{
    public sealed class GraphicsNormalTextureVertexData3D 
        : IGraphicsVertexData3D
    {
        public Color Color
        {
            get => Color.Black;
            set => throw new InvalidOperationException();
        }

        public ITuple2D TextureUv { get; set; }

        public GrNormal3D Normal { get; }
            = new GrNormal3D();
        
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
            TextureUv = new Tuple2D(0, 0);
        }

        public GraphicsNormalTextureVertexData3D(ITuple2D textureUv)
        {
            TextureUv = textureUv;
        }

        public GraphicsNormalTextureVertexData3D(ITuple2D textureUv, ITuple3D normal)
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
