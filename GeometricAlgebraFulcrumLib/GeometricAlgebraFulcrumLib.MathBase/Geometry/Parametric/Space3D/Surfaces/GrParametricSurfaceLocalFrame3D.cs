using System.Diagnostics;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Primitives.Vertices;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Structures.Vertices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space3D.Surfaces
{
    public sealed record GrParametricSurfaceLocalFrame3D :
        IGraphicsVertex3D
    {
        
        public int VSpaceDimensions 
            => 3;

        public double Item1
            => Point.X;
        
        public double Item2
            => Point.Y;
        
        public double Item3
            => Point.Z;

        public Float64Scalar X 
            => Point.X;

        public Float64Scalar Y 
            => Point.Y;

        public Float64Scalar Z 
            => Point.Z;

        public int Index { get; internal set; } = -1;
        
        public Float64Vector3D Point { get; }
        
        public Color Color { get; set; }

        public Pair<double> ParameterValue { get; }
        
        public Normal3D Normal { get; }

        public bool HasParameterValue 
            => true;

        public bool HasNormal 
            => true;

        public bool HasColor 
            => true;
        
        public GraphicsVertexDataKind3D DataKind 
            => GraphicsVertexDataKind3D.NormalTextureColorData;


        internal GrParametricSurfaceLocalFrame3D(double parameterValue1, double parameterValue2, IFloat64Tuple3D point, IFloat64Tuple3D normal)
        {
            ParameterValue = new Pair<double>(parameterValue1, parameterValue2);
            Point = point.ToVector3D();
            Normal = new Normal3D(normal);

            Debug.Assert(IsValid());
        }

        internal GrParametricSurfaceLocalFrame3D(IGraphicsParametricSurface3D surface, double parameterValue1, double parameterValue2)
        {
            ParameterValue = new Pair<double>(parameterValue1, parameterValue2);
            Point = surface.GetPoint(parameterValue1, parameterValue2);
            Normal = new Normal3D(
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