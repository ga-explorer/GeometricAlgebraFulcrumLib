
using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Geometry.Structures.Vertices;
using SixLabors.ImageSharp;

namespace GraphicsComposerLib.Geometry.Primitives.Vertices
{
    public sealed class GrColorVertex3D 
        : IGraphicsVertex3D
    {
        public int Index { get; }

        public Float64Tuple3D Point { get; }

        public Color Color { get; set; }
            = Color.Black;

        public Pair<double> ParameterValue 
            => new Pair<double>(0, 0);

        public GrNormal3D Normal
            => null;

        public bool HasColor 
            => true;

        public bool HasParameterValue 
            => false;

        public bool HasNormal 
            => false;

        public GraphicsVertexDataKind3D DataKind
            => GraphicsVertexDataKind3D.ColorData;

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

        public bool IsValid() => Point.IsValid();


        public GrColorVertex3D(int index, ITriplet<double> point)
        {
            Index = index;
            Point = point.ToTuple3D();
        }

        public GrColorVertex3D(int index, ITriplet<double> point, Color color)
        {
            Index = index;
            Point = point.ToTuple3D();
            Color = color;
        }

        public GrColorVertex3D(int index, IGraphicsSurfaceLocalFrame3D vertex)
        {
            Index = index;
            Point = vertex.Point;
            Color = vertex.Color;
        }
    }
}