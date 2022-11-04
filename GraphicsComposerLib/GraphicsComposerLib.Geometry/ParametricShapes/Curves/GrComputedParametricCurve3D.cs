using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using MathNet.Numerics;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Calculus;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves
{
    public class GrComputedParametricCurve3D :
        IGraphicsC1ParametricCurve3D
    {
        public static GrComputedParametricCurve3D Create(IScalarD1Function xFunc, IScalarD1Function yFunc, IScalarD1Function zFunc)
        {
            return new GrComputedParametricCurve3D(
                t => new Tuple3D(
                    xFunc.GetValue(t), 
                    yFunc.GetValue(t), 
                    zFunc.GetValue(t)
                ),
                t => new Tuple3D(
                    xFunc.GetFirstDerivative(t), 
                    yFunc.GetFirstDerivative(t), 
                    zFunc.GetFirstDerivative(t)
                )
            );
        }

        public static GrComputedParametricCurve3D Create(Func<double, double> xFunc, Func<double, double> yFunc, Func<double, double> zFunc)
        {
            return new GrComputedParametricCurve3D(
                t => new Tuple3D(
                    xFunc(t), 
                    yFunc(t), 
                    zFunc(t)
                ),
                t => new Tuple3D(
                    Differentiate.FirstDerivative(xFunc, t), 
                    Differentiate.FirstDerivative(yFunc, t), 
                    Differentiate.FirstDerivative(zFunc, t)
                )
            );
        }

        public Func<double, Tuple3D> GetPointFunc { get; }

        public Func<double, Tuple3D> GetTangentFunc { get; }


        public GrComputedParametricCurve3D([NotNull] Func<double, Tuple3D> getPointFunc)
        {
            GetPointFunc = getPointFunc;
            GetTangentFunc = null;
        }
        
        public GrComputedParametricCurve3D([NotNull] Func<double, Tuple3D> getPointFunc, [NotNull] Func<double, Tuple3D> getTangentFunc)
        {
            GetPointFunc = getPointFunc;
            GetTangentFunc = getTangentFunc;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D GetPoint(double parameterValue)
        {
            return GetPointFunc(parameterValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D GetTangent(double parameterValue)
        {
            if (GetTangentFunc is not null)
                return GetTangentFunc(parameterValue);

            const double epsilon = 1e-7;

            var p1 = GetPointFunc(parameterValue - epsilon);
            var p2 = GetPointFunc(parameterValue + epsilon);

            return (p2 - p1) / (2 * epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D GetUnitTangent(double parameterValue)
        {
            return GetTangent(parameterValue).ToUnitVector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricCurveLocalFrame3D GetFrame(double parameterValue)
        {
            return GrParametricCurveLocalFrame3D.Create(
                parameterValue,
                GetPoint(parameterValue),
                GetTangent(parameterValue)
            );
        }
    }
}