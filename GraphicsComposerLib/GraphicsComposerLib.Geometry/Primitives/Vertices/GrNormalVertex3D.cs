using System;

using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Frames.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Geometry.Structures.Vertices;
using SixLabors.ImageSharp;

namespace GraphicsComposerLib.Geometry.Primitives.Vertices
{
    public sealed class GrNormalVertex3D 
        : IGraphicsVertex3D
    {
        public int Index { get; }

        public Float64Tuple3D Point { get; }

        public Color Color
        {
            get => Color.Black;
            set => throw new InvalidOperationException();
        }

        public Pair<double> ParameterValue 
            => new Pair<double>(0, 0);

        public Normal3D Normal { get; }
            = new Normal3D();

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

        public bool HasColor 
            => false;

        public bool HasParameterValue 
            => false;

        public bool HasNormal 
            => true;

        public GraphicsVertexDataKind3D DataKind
            => GraphicsVertexDataKind3D.NormalData;

        public bool IsValid() => Point.IsValid() && Normal.IsValid();


        public GrNormalVertex3D(int index, ITriplet<double> point)
        {
            Index = index;
            Point = point.ToTuple3D();
        }

        public GrNormalVertex3D(int index, ITriplet<double> point, ITriplet<double> normal)
        {
            Index = index;
            Point = point.ToTuple3D();
            Normal.Set(normal);
        }

        public GrNormalVertex3D(int index, IGraphicsSurfaceLocalFrame3D vertex)
        {
            Index = index;
            Point = vertex.Point;
            Normal.Set(vertex.Normal);
        }


        
        public Float64Tuple3D GetDisplacedPoint(double d)
        {
            return new Float64Tuple3D(
                Point.X + d * Normal.X,
                Point.Y + d * Normal.Y,
                Point.Z + d * Normal.Z
            );
        }
    }
}