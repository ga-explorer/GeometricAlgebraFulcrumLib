using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Differential.Functions;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Trivectors
{
    public class ComputedParametricTrivector3D :
        IParametricTrivector3D
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricTrivector3D Create(Func<double, Float64Trivector3D> getTrivectorFunc)
        {
            return new ComputedParametricTrivector3D(
                Float64ScalarRange.Infinite, 
                getTrivectorFunc
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricTrivector3D Create(Float64ScalarRange parameterRange, Func<double, Float64Trivector3D> getTrivectorFunc)
        {
            return new ComputedParametricTrivector3D(
                parameterRange, 
                getTrivectorFunc
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricTrivector3D Create(Func<double, Float64Trivector3D> getTrivectorFunc, Func<double, Float64Trivector3D> getTangentFunc)
        {
            return new ComputedParametricTrivector3D(
                Float64ScalarRange.Infinite, 
                getTrivectorFunc, 
                getTangentFunc
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricTrivector3D Create(Float64ScalarRange parameterRange, Func<double, Float64Trivector3D> getTrivectorFunc, Func<double, Float64Trivector3D> getTangentFunc)
        {
            return new ComputedParametricTrivector3D(
                parameterRange, 
                getTrivectorFunc, 
                getTangentFunc
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricTrivector3D Create(DifferentialFunction xyzFunc)
        {
            return Create(
                Float64ScalarRange.Infinite, 
                xyzFunc
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricTrivector3D Create(Float64ScalarRange parameterRange, DifferentialFunction xyzFunc)
        {
            var xyzDtFunc = xyzFunc.GetDerivative1();
            
            return new ComputedParametricTrivector3D(
                parameterRange, 
                t => 
                    Float64Trivector3D.Create(
                        xyzFunc.GetValue(t)
                    ),
                t => 
                    Float64Trivector3D.Create(
                        xyzDtFunc.GetValue(t)
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricTrivector3D Create(Func<double, double> xyzFunc)
        {
            return Create(
                Float64ScalarRange.Infinite, 
                xyzFunc
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComputedParametricTrivector3D Create(Float64ScalarRange parameterRange, Func<double, double> xyzFunc)
        {
            return new ComputedParametricTrivector3D(
                parameterRange, 
                t => 
                    Float64Trivector3D.Create(xyzFunc(t)),
                t => 
                    Float64Trivector3D.Create(
                        Differentiate.FirstDerivative(xyzFunc, t)
                    )
                );
        }


        public Float64ScalarRange ParameterRange { get; }
        
        public Func<double, Float64Trivector3D> GetTrivectorFunc { get; }

        public Func<double, Float64Trivector3D> GetTangentFunc { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ComputedParametricTrivector3D(Float64ScalarRange parameterRange, Func<double, Float64Trivector3D> getTrivectorFunc)
        {
            ParameterRange = parameterRange;
            GetTrivectorFunc = getTrivectorFunc;
            GetTangentFunc = 
                t =>
                {
                    const double epsilon = 1e-7;

                    var p1 = getTrivectorFunc(t - epsilon);
                    var p2 = getTrivectorFunc(t + epsilon);

                    return (p2 - p1) / (2 * epsilon);
                };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ComputedParametricTrivector3D(Float64ScalarRange parameterRange, Func<double, Float64Trivector3D> getTrivectorFunc, Func<double, Float64Trivector3D> getTangentFunc)
        {
            ParameterRange = parameterRange;
            GetTrivectorFunc = getTrivectorFunc;
            GetTangentFunc = getTangentFunc;
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Trivector3D GetTrivector(double parameterValue)
        {
            return GetTrivectorFunc(parameterValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Trivector3D GetDerivative1Trivector(double parameterValue)
        {
            return GetTangentFunc(parameterValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IParametricScalar GetDualScalarCurve()
        {
            return ComputedParametricScalar.Create(
                ParameterRange,
                t => GetTrivector(t).Dual3D().Scalar.Value
            );
        }
    }
}