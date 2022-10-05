using System.Diagnostics;

using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using GraphicsComposerLib.Geometry.Primitives.Vertices;
using GraphicsComposerLib.Geometry.Structures.Vertices;
using SixLabors.ImageSharp;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Surfaces
{
    public sealed record GrParametricSurfaceLocalFrame3D :
        IGraphicsVertex3D
    {
        public double Item1
            => Point.X;
        
        public double Item2
            => Point.Y;
        
        public double Item3
            => Point.Z;

        public double X 
            => Point.X;

        public double Y 
            => Point.Y;

        public double Z 
            => Point.Z;

        public int Index { get; internal set; } = -1;
        
        public Tuple3D Point { get; }
        
        public Color Color { get; set; }

        public Pair<double> ParameterValue { get; }
        
        public GrNormal3D Normal { get; }

        public bool HasParameterValue 
            => true;

        public bool HasNormal 
            => true;

        public bool HasColor 
            => true;
        
        public GraphicsVertexDataKind3D DataKind 
            => GraphicsVertexDataKind3D.NormalTextureColorData;


        internal GrParametricSurfaceLocalFrame3D(double parameterValue1, double parameterValue2, ITuple3D point, ITuple3D normal)
        {
            ParameterValue = new Pair<double>(parameterValue1, parameterValue2);
            Point = point.ToTuple3D();
            Normal = new GrNormal3D(normal);

            Debug.Assert(IsValid());
        }

        internal GrParametricSurfaceLocalFrame3D(IGraphicsParametricSurface3D surface, double parameterValue1, double parameterValue2)
        {
            ParameterValue = new Pair<double>(parameterValue1, parameterValue2);
            Point = surface.GetPoint(parameterValue1, parameterValue2);
            Normal = new GrNormal3D(
                surface.GetNormal(parameterValue1, parameterValue2).ToUnitVector()
            );

            Debug.Assert(IsValid());
        }


        public bool IsValid()
        {
            return ParameterValue.Item1.IsValid() &&
                   ParameterValue.Item2.IsValid() &&
                   Point.IsValid() &&
                   Normal.IsValid();
        }
    }
}