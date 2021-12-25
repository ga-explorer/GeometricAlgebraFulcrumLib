using System;
using System.Drawing;
using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Geometry.Structures.Vertices;

namespace GraphicsComposerLib.Geometry.Primitives.Vertices
{
    public sealed class GrTextureVertex3D 
        : IGraphicsVertex3D
    {
        public int Index { get; }

        public Tuple3D Point { get; }

        public Color Color
        {
            get => Color.Black;
            set => throw new InvalidOperationException();
        }

        public Pair<double> ParameterValue { get; set; }

        public GrNormal3D Normal
            => null;

        public bool HasColor 
            => false;

        public bool HasParameterValue 
            => true;

        public bool HasNormal 
            => false;

        public GraphicsVertexDataKind3D DataKind
            => GraphicsVertexDataKind3D.TextureData;

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
            => ParameterValue.Item1;

        public bool IsValid() =>
            Point.IsValid() &&
            ParameterValue.Item1.IsValid() &&
            ParameterValue.Item2.IsValid();


        public GrTextureVertex3D(int index, ITriplet<double> point)
        {
            Index = index;
            Point = point.ToTuple3D();
            ParameterValue = new Pair<double>(0, 0);
        }

        public GrTextureVertex3D(int index, ITriplet<double> point, IPair<double> textureUv)
        {
            Index = index;
            Point = point.ToTuple3D();
            ParameterValue = textureUv.ToPair();
        }

        public GrTextureVertex3D(int index, IGraphicsSurfaceLocalFrame3D vertex)
        {
            Index = index;
            Point = vertex.Point;
            ParameterValue = vertex.ParameterValue;
        }

    }
}