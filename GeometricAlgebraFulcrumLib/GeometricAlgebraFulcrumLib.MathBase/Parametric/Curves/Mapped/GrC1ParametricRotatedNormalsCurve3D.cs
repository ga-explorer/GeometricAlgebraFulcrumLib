using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Parametric.Frames;

namespace GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves.Mapped
{
    public class GrParametricRotatedNormalsCurve3D :
        IParametricCurve3D
    {
        public IParametricCurve3D BaseCurve { get; }

        public Func<double, Float64PlanarAngle> AngleFunction { get; }


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
        public Float64Tuple3D GetPoint(double parameterValue)
        {
            return BaseCurve.GetPoint(parameterValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D GetDerivative1Point(double parameterValue)
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