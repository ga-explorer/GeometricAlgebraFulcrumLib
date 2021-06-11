using System;
using System.Drawing;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.GraphicsGeometry.Vertices
{
    public sealed class GraphicsNormalTexturedVertex3D 
        : IGraphicsVertex3D
    {
        public int Index { get; }

        public ITuple3D Point { get; }

        public Color Color
        {
            get => Color.Black;
            set => throw new InvalidOperationException();
        }

        public ITuple2D TextureUv { get; set; }

        public IGraphicsNormal3D Normal { get; }
            = new GraphicsNormal3D(0, 0, 0);

        public double X 
            => Point.X;

        public double Y 
            => Point.Y;

        public double Z 
            => Point.Z;

        public double Item1
            => X;

        public double Item2
            => Y;

        public double Item3
            => Z;

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
            => GraphicsVertexDataKind3D.PositionNormalTextureVertex;

        public bool IsValid
            => Point.IsValid ||
               TextureUv.IsValid ||
               Normal.IsValid;

        public bool IsInvalid
            => Point.IsInvalid ||
               TextureUv.IsInvalid ||
               Normal.IsInvalid;


        public GraphicsNormalTexturedVertex3D(int index, ITuple3D point)
        {
            Index = index;
            Point = point;
            TextureUv = new Tuple2D();
        }

        public GraphicsNormalTexturedVertex3D(int index, ITuple3D point, ITuple2D textureUv)
        {
            Index = index;
            Point = point;
            TextureUv = textureUv;
        }

        public GraphicsNormalTexturedVertex3D(int index, ITuple3D point, ITuple2D textureUv, ITuple3D normal)
        {
            Index = index;
            Point = point;
            TextureUv = textureUv;
            Normal.Set(normal);
        }

        public GraphicsNormalTexturedVertex3D(int index, IGraphicsVertex3D vertex)
        {
            Index = index;
            Point = vertex.Point;
            TextureUv = vertex.TextureUv;
            Normal.Set(vertex.Normal);
        }

    }
}
