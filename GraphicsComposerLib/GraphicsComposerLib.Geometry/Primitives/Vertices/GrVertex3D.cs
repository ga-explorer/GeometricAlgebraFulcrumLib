using System;

using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Geometry.Structures.Vertices;
using SixLabors.ImageSharp;

namespace GraphicsComposerLib.Geometry.Primitives.Vertices
{
    public sealed class GrVertex3D 
        : IGraphicsVertex3D
    {
        public int Index { get; }

        public Tuple3D Point { get; }

        public Pair<double> ParameterValue 
            => new Pair<double>(0, 0);

        public GrNormal3D Normal
            => null;

        public Color Color
        {
            get => Color.Black;
            set => throw new InvalidOperationException();
        }

        public bool HasParameterValue 
            => false;

        public bool HasNormal 
            => false;

        public bool HasColor 
            => false;

        public GraphicsVertexDataKind3D DataKind
            => GraphicsVertexDataKind3D.VoidData;

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

        public bool IsValid() => Point.IsValid();


        public GrVertex3D(int index, double x, double y, double z)
        {
            Index = index;
            Point = new Tuple3D(x, y, z);
        }

        public GrVertex3D(int index, ITriplet<double> point)
        {
            Index = index;
            Point = point.ToTuple3D();
        }

        public GrVertex3D(int index, IGraphicsSurfaceLocalFrame3D vertex)
        {
            Index = index;
            Point = vertex.Point;
        }

    }
}