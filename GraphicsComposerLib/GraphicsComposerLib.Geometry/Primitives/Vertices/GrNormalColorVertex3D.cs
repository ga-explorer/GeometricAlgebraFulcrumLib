using System.Drawing;
using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Geometry.Structures.Vertices;

namespace GraphicsComposerLib.Geometry.Primitives.Vertices
{
    public sealed class GrNormalColorVertex3D 
        : IGraphicsVertex3D
    {
        public int Index { get; }

        public Tuple3D Point { get; }

        public Color Color { get; set; }
            = Color.Black;

        public Pair<double> ParameterValue 
            => new Pair<double>(0, 0);

        public GrNormal3D Normal { get; }
            = new GrNormal3D();

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

        public bool HasColor 
            => true;

        public bool HasParameterValue 
            => false;

        public bool HasNormal 
            => true;

        public GraphicsVertexDataKind3D DataKind
            => GraphicsVertexDataKind3D.NormalColorData;

        public bool IsValid() => Point.IsValid() && Normal.IsValid();


        public GrNormalColorVertex3D(int index, ITriplet<double> point)
        {
            Index = index;
            Point = point.ToTuple3D();
        }

        public GrNormalColorVertex3D(int index, ITriplet<double> point, Color color)
        {
            Index = index;
            Point = point.ToTuple3D();
            Color = color;
        }

        public GrNormalColorVertex3D(int index, ITriplet<double> point, Color color, ITriplet<double> normal)
        {
            Index = index;
            Point = point.ToTuple3D();
            Color = color;
            Normal.Set(normal);
        }

        public GrNormalColorVertex3D(int index, IGraphicsSurfaceLocalFrame3D point)
        {
            Index = index;
            Point = point.Point;
            Color = point.Color;
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