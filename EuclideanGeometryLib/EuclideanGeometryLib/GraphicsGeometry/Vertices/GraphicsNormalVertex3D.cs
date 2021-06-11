using System;
using System.Drawing;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.GraphicsGeometry.Vertices
{
    public sealed class GraphicsNormalVertex3D 
        : IGraphicsVertex3D
    {
        public int Index { get; }

        public ITuple3D Point { get; }

        public Color Color
        {
            get => Color.Black;
            set => throw new InvalidOperationException();
        }

        public ITuple2D TextureUv
        {
            get => Tuple2D.Zero;
            set => throw new InvalidOperationException();
        }

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
            => GraphicsVertexDataKind3D.PositionNormalVertex;

        public bool IsValid 
            => Point.IsValid &&
               !double.IsNaN(NormalX) &&
               !double.IsNaN(NormalY) &&
               !double.IsNaN(NormalZ);

        public bool IsInvalid 
            => Point.IsInvalid || 
               double.IsNaN(NormalX) ||
               double.IsNaN(NormalY) ||
               double.IsNaN(NormalZ);


        public GraphicsNormalVertex3D(int index, ITuple3D point)
        {
            Index = index;
            Point = point;
        }

        public GraphicsNormalVertex3D(int index, ITuple3D point, ITuple3D normal)
        {
            Index = index;
            Point = point;
            Normal.Set(normal);
        }

        public GraphicsNormalVertex3D(int index, IGraphicsVertex3D vertex)
        {
            Index = index;
            Point = vertex.Point;
            Normal.Set(vertex.Normal);
        }


        
        public Tuple3D GetDisplacedPoint(double d)
        {
            return new Tuple3D(
                Point.X + d * Normal.X,
                Point.Y + d * Normal.Y,
                Point.Z + d * Normal.Z
            );
        }
    }
}