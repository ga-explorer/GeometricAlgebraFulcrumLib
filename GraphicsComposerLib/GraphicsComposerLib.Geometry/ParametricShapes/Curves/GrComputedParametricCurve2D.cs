using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves
{
    public class GrComputedParametricCurve2D :
        IGraphicsParametricCurve2D
    {
        public Func<double, Tuple2D> GetPointFunc { get; }

        public Func<double, Tuple2D> GetTangentFunc { get; }


        public GrComputedParametricCurve2D([NotNull] Func<double, Tuple2D> getPointFunc)
        {
            GetPointFunc = getPointFunc;
            GetTangentFunc = null;
        }
        
        public GrComputedParametricCurve2D([NotNull] Func<double, Tuple2D> getPointFunc, [NotNull] Func<double, Tuple2D> getTangentFunc)
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
        public Tuple2D GetPoint(double parameterValue)
        {
            return GetPointFunc(parameterValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple2D GetTangent(double parameterValue)
        {
            if (GetTangentFunc is not null)
                return GetTangentFunc(parameterValue);

            const double epsilon = 1e-7;

            var p1 = GetPointFunc(parameterValue - epsilon);
            var p2 = GetPointFunc(parameterValue + epsilon);

            return (p2 - p1) / (2 * epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple2D GetUnitTangent(double parameterValue)
        {
            return GetTangent(parameterValue).ToUnitVector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricCurveLocalFrame2D GetFrame(double parameterValue)
        {
            return GrParametricCurveLocalFrame2D.CreateFrame(
                parameterValue,
                GetPoint(parameterValue),
                GetTangent(parameterValue)
            );
        }
    }
}