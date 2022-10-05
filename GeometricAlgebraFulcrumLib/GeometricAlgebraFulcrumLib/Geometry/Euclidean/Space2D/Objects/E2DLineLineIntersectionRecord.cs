using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean.Space2D.Objects
{
    public sealed record E2DLineLineIntersectionRecord<T>
    {
        public IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        /// <summary>
        /// Signed distance from the 1st line's point 1 to the 2nd line
        /// </summary>
        public T Line1Point1Distance { get; init; }

        /// <summary>
        /// Signed distance from the 1st line's point 2 to the 2nd line
        /// </summary>
        public T Line1Point2Distance { get; init; }

        /// <summary>
        /// Signed distance from the 2nd line's point 1 to the 1st line
        /// </summary>
        public T Line2Point1Distance { get; init; }

        /// <summary>
        /// Signed distance from the 2nd line's point 2 to the 1st line
        /// </summary>
        public T Line2Point2Distance { get; init; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal E2DLineLineIntersectionRecord([NotNull] IScalarAlgebraProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;
        }

        
        /// <summary>
        /// Parameter value of 1st line at intersection point
        /// </summary>
        public T GetLine1ParameterValue12()
        {
            return ScalarProcessor.Divide(
                Line1Point1Distance,
                ScalarProcessor.Subtract(Line1Point1Distance, Line1Point2Distance)
            );
        }

        /// <summary>
        /// Parameter value of 1st line at intersection point
        /// </summary>
        public T GetLine1ParameterValue21()
        {
            return ScalarProcessor.Divide(
                Line1Point2Distance,
                ScalarProcessor.Subtract(Line1Point2Distance, Line1Point1Distance)
            );
        }

        /// <summary>
        /// Parameter value of 2nd line at intersection point
        /// </summary>
        public T GetLine2ParameterValue12()
        {
            return ScalarProcessor.Divide(
                Line2Point1Distance,
                ScalarProcessor.Subtract(Line2Point1Distance, Line2Point2Distance)
            );
        }

        /// <summary>
        /// Parameter value of 2nd line at intersection point
        /// </summary>
        public T GetLine2ParameterValue21()
        {
            return ScalarProcessor.Divide(
                Line2Point2Distance,
                ScalarProcessor.Subtract(Line2Point2Distance, Line2Point1Distance)
            );
        }

        /// <summary>
        /// Intersection of a line and a line exists
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasLineLineIntersection()
        {
            var t1 = GetLine1ParameterValue12();
            var t2 = GetLine2ParameterValue12();

            return
                ScalarProcessor.IsFiniteNumber(t1) &&
                ScalarProcessor.IsFiniteNumber(t2);
        }

        /// <summary>
        /// Intersection of a line and a ray exists
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasLineRayIntersection()
        {
            var t1 = GetLine1ParameterValue12();
            var t2 = GetLine2ParameterValue12();

            return
                ScalarProcessor.IsFiniteNumber(t1) &&
                ScalarProcessor.IsFiniteNumber(t2) &&
                ScalarProcessor.IsPositive(t2);
        }

        /// <summary>
        /// Intersection of a line and a segment exists
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasLineSegmentIntersection()
        {
            var t1 = GetLine1ParameterValue12();
            var t2 = GetLine2ParameterValue12();

            var s2 = ScalarProcessor.Subtract(ScalarProcessor.ScalarOne, t2);

            return
                ScalarProcessor.IsFiniteNumber(t1) &&
                ScalarProcessor.IsFiniteNumber(t2) &&
                ScalarProcessor.IsPositive(t2) &&
                ScalarProcessor.IsPositive(s2);
        }

        /// <summary>
        /// Intersection of a ray and a line exists
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasRayLineIntersection()
        {
            var t1 = GetLine1ParameterValue12();
            var t2 = GetLine2ParameterValue12();

            return
                ScalarProcessor.IsFiniteNumber(t1) &&
                ScalarProcessor.IsFiniteNumber(t2) &&
                ScalarProcessor.IsPositive(t1);
        }

        /// <summary>
        /// Intersection of a ray and a ray exists
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasRayRayIntersection()
        {
            var t1 = GetLine1ParameterValue12();
            var t2 = GetLine2ParameterValue12();

            return
                ScalarProcessor.IsFiniteNumber(t1) &&
                ScalarProcessor.IsFiniteNumber(t2) &&
                ScalarProcessor.IsPositive(t1) &&
                ScalarProcessor.IsPositive(t2);
        }

        /// <summary>
        /// Intersection of a ray and a segment exists
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasRaySegmentIntersection()
        {
            var t1 = GetLine1ParameterValue12();
            var t2 = GetLine2ParameterValue12();

            var s2 = ScalarProcessor.Subtract(ScalarProcessor.ScalarOne, t2);

            return
                ScalarProcessor.IsFiniteNumber(t1) &&
                ScalarProcessor.IsFiniteNumber(t2) &&
                ScalarProcessor.IsPositive(t1) &&
                ScalarProcessor.IsPositive(t2) &&
                ScalarProcessor.IsPositive(s2);
        }

        /// <summary>
        /// Intersection of a segment and a line exists
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasSegmentLineIntersection()
        {
            var t1 = GetLine1ParameterValue12();
            var t2 = GetLine2ParameterValue12();

            var s1 = ScalarProcessor.Subtract(ScalarProcessor.ScalarOne, t1);

            return
                ScalarProcessor.IsFiniteNumber(t1) &&
                ScalarProcessor.IsFiniteNumber(t2) &&
                ScalarProcessor.IsPositive(t1) &&
                ScalarProcessor.IsPositive(s1);
        }

        /// <summary>
        /// Intersection of a segment and a ray exists
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasSegmentRayIntersection()
        {
            var t1 = GetLine1ParameterValue12();
            var t2 = GetLine2ParameterValue12();

            var s1 = ScalarProcessor.Subtract(ScalarProcessor.ScalarOne, t1);

            return
                ScalarProcessor.IsFiniteNumber(t1) &&
                ScalarProcessor.IsFiniteNumber(t2) &&
                ScalarProcessor.IsPositive(t1) &&
                ScalarProcessor.IsPositive(t2) &&
                ScalarProcessor.IsPositive(s1);

        }

        /// <summary>
        /// Intersection of a segment and a segment exists
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasSegmentSegmentIntersection()
        {
            var t1 = GetLine1ParameterValue12();
            var t2 = GetLine2ParameterValue12();

            var s1 = ScalarProcessor.Subtract(ScalarProcessor.ScalarOne, t1);
            var s2 = ScalarProcessor.Subtract(ScalarProcessor.ScalarOne, t2);

            return
                ScalarProcessor.IsFiniteNumber(t1) &&
                ScalarProcessor.IsFiniteNumber(t2) &&
                ScalarProcessor.IsPositive(t1) &&
                ScalarProcessor.IsPositive(t2) &&
                ScalarProcessor.IsPositive(s1) &&
                ScalarProcessor.IsPositive(s2);
        }
    }
}
