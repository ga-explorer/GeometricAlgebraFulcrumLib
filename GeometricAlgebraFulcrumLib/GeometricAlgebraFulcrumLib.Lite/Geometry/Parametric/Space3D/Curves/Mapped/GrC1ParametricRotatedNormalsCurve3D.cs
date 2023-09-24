using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Curves.Mapped
{
    public class GrParametricRotatedNormalsCurve3D :
        IParametricCurve3D
    {
        public IParametricCurve3D BaseCurve { get; }

        public Func<double, Float64PlanarAngle> AngleFunction { get; }

        public Float64ScalarRange ParameterRange 
            => BaseCurve.ParameterRange;


        public GrParametricRotatedNormalsCurve3D(IParametricCurve3D baseCurve, Func<double, Float64PlanarAngle> angleFunction)
        {
            BaseCurve = baseCurve;
            AngleFunction = angleFunction;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return BaseCurve.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D GetPoint(double parameterValue)
        {
            return BaseCurve.GetPoint(parameterValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D GetDerivative1Point(double parameterValue)
        {
            return BaseCurve.GetDerivative1Point(parameterValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ParametricCurveLocalFrame3D GetFrame(double parameterValue)
        {
            return BaseCurve
                .GetFrame(parameterValue)
                .RotateNormalsBy(
                    AngleFunction(parameterValue)
                );
        }
    }
}