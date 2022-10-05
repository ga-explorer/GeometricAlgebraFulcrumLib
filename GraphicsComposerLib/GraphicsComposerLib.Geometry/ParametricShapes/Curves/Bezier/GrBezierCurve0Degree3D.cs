using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves.Bezier
{
    public class GrBezierCurve0Degree3D :
        IGraphicsC1ParametricCurve3D
    {
        public Tuple3D Point1 { get; }


        public GrBezierCurve0Degree3D([NotNull] ITuple3D point1)
        {
            Point1 = point1.ToTuple3D();

            Debug.Assert(IsValid());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return Point1.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D GetPoint(double t)
        {
            return Point1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D GetTangent(double t)
        {
            return Tuple3D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D GetUnitTangent(double t)
        {
            return Tuple3D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GrParametricCurveLocalFrame3D GetFrame(double parameterValue)
        {
            return GrParametricCurveLocalFrame3D.CreateFrame(
                parameterValue,
                Point1,
                Tuple3D.Zero,
                Tuple3D.Zero,
                Tuple3D.Zero
            );
        }
    }
}