using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier;
using System.Numerics;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Samplers
{
    /// <summary>
    /// Specialized adaptive sampler for Pythagorean Hodograph
    /// Bernstein curves
    /// </summary>
    public sealed class SampledPhBernsteinCurve2D(
        bool isPeriodic,
        SampledPhBernsteinCurve2D.SamplingOptions? options = null
        ) : Float64SampledPath2D(Float64ScalarRange.ZeroToOne, isPeriodic, options ?? new SamplingOptions())
    {
        /// <summary>
        /// Reuse base SamplingOptions or accept a specialized options if wanted
        /// </summary>
        public sealed class SamplingOptions : 
            Float64AdaptiveSamplingOptions
        {
            // keep defaults; user can set MinLevel/MaxLevel/MaxChordError/MaxAngleRadians etc.
            // Add any PH-specific options here if needed later
        }

        
        /// <summary>
        /// For PH curves the original implementation indexed corners by
        /// (level,segmentCount ints), // here we expose a compatible
        /// recursive signature that uses integer segment counts directly.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="segmentCount"></param>
        /// <returns></returns>
        private static ulong EncodeCornerKeyInteger(int level, int segmentCount)
        {
            if (segmentCount == 0) return 0UL;
            
            var tz = BitOperations.TrailingZeroCount((uint)segmentCount);
            
            if (tz > level) tz = level;
            
            var segNorm = (uint)(segmentCount >> tz);
            var levelNorm = (uint)(level - tz);
            
            return (ulong)levelNorm << 32 | segNorm;
        }


        public new SamplingOptions Options { get; } = options ?? new SamplingOptions();

        
        private int GetOrAddCornerInteger(PhBernsteinCurve2D curve, int level, int segmentCount)
        {
            var key = EncodeCornerKeyInteger(level, segmentCount);
            if (CornerIndex.TryGetValue(key, out var idx)) 
                return idx;

            var t = segmentCount / (double)(1L << level);
            var p = curve.GetPoint(t);
            var d = curve.GetTangentVector(t);
            var u = 
                d.L2Norm() > 1e-14 
                    ? d.VectorDivideByENorm() 
                    : LinFloat64Vector2D.Create(1.0, 0.0);

            idx = CornerList.Count;
            CornerList.Add(new Corner(idx, t, new CornerPosition(level, segmentCount), p, u));
            CornerIndex[key] = idx;
            
            return idx;
        }

        private void SubdivideRecursive(
            PhBernsteinCurve2D curve,
            int level,
            int segmentCount0,
            int segmentCount1,
            double t0,
            double t1,
            double s0,
            double parentCumulativeLength)
        {
            var c0 = GetOrAddCornerInteger(curve, level, segmentCount0);
            var c1 = GetOrAddCornerInteger(curve, level, segmentCount1);
            var corner0 = CornerList[c0];
            var corner1 = CornerList[c1];

            var s1 = curve.GetArcLength(t1);
            var segLength = s1 - s0;

            var dx = corner1.Point.X - corner0.Point.X;
            var dy = corner1.Point.Y - corner0.Point.Y;
            var chord = Math.Sqrt(dx * dx + dy * dy);

            var dotEnd = Math.Clamp(corner0.Tangent.X * corner1.Tangent.X + corner0.Tangent.Y * corner1.Tangent.Y, -1.0, 1.0);
            var angleEnd = Math.Acos(dotEnd);

            var tooSmallParam = Math.Abs(t1 - t0) <= Options.ParameterSpan;
            var levelReached = level >= Options.MaxDepth;
            var minLevelNeeded = level < Options.MinDepth;
            var arcChordGood = chord < 1e-14 || (segLength - chord) / chord <= Options.RelativeChordError;
            var angleGood = angleEnd <= Options.FrameAngleDifference.RadiansValue;

            if (minLevelNeeded || !levelReached && !(arcChordGood && angleGood) && !tooSmallParam)
            {
                if (level < Options.MaxDepth)
                {
                    var childLevel = level + 1;
                    var leftSeg0 = segmentCount0 * 2;
                    var leftSeg1 = leftSeg0 + 1;
                    var tm = 0.5 * (t0 + t1);
                    var sm = curve.S.GetValue(tm);

                    // Midpoint point and tangent
                    var pm = LinFloat64Vector2D.Create(curve.X.GetValue(tm), curve.Y.GetValue(tm));
                    var um = LinFloat64Vector2D.Create(curve.XDt.GetValue(tm), curve.YDt.GetValue(tm)).VectorDivideByENorm();

                    // Midpoint tangent angle checks
                    var dotMid = Math.Clamp(corner0.Tangent.X * um.X + corner0.Tangent.Y * um.Y, -1.0, 1.0);
                    var midAngle0 = Math.Acos(dotMid);
                    
                    dotMid = Math.Clamp(um.X * corner1.Tangent.X + um.Y * corner1.Tangent.Y, -1.0, 1.0);
                    var midAngle1 = Math.Acos(dotMid);
                    var angleBad = midAngle0 > Options.FrameAngleDifference.RadiansValue || midAngle1 > Options.FrameAngleDifference.RadiansValue;

                    // Chord improvement test
                    var leftChord = (pm - corner0.Point).L2Norm();
                    var rightChord = (corner1.Point - pm).L2Norm();
                    var totalChildChord = leftChord + rightChord;
                    double improvement = totalChildChord - chord;
                    var relativeImprovement = chord > 1e-14 ? improvement / chord : 0.0;

                    if (!angleBad && relativeImprovement < Options.RelativeChordImprovement)
                    {
                        AddLeaf(t0, t1, c0, c1, parentCumulativeLength, segLength);
                        return;
                    }

                    // left child
                    SubdivideRecursive(curve, childLevel, leftSeg0, leftSeg1, t0, tm, s0, parentCumulativeLength);

                    var afterLeftCum = TotalLength;

                    // right child
                    var rightSeg0 = leftSeg1;
                    var rightSeg1 = rightSeg0 + 1;
                    SubdivideRecursive(curve, childLevel, rightSeg0, rightSeg1, tm, t1, sm, afterLeftCum);
                    return;
                }
            }

            AddLeaf(t0, t1, c0, c1, parentCumulativeLength, segLength);
        }


        /// <summary>
        /// Generation similar to original PhBernstein implementation 
        /// </summary>
        public SampledPhBernsteinCurve2D Generate(PhBernsteinCurve2D curve)
        {
            ResetStorage();

            var estimatedLeaves = Math.Min(Options.ExpectedLeaves, 1 << Math.Min(16, Options.MaxDepth));
            LeafList.Capacity = Math.Max(LeafList.Capacity, estimatedLeaves);
            CornerList.Capacity = Math.Max(CornerList.Capacity, 2 * LeafList.Capacity + 8);

            var s0 = curve.S.GetValue(0.0);
            SubdivideRecursive(curve, 0, 0, 1, 0.0, 1.0, s0, 0.0);

            IsReady = true;

            return this;
        }
        
        public override bool IsValid()
        {
            throw new NotImplementedException();
        }

        public override Float64ArcLengthPath2D ToFiniteArcLengthPath()
        {
            throw new NotImplementedException();
        }

        public override Float64ArcLengthPath2D ToPeriodicArcLengthPath()
        {
            throw new NotImplementedException();
        }
        
        public override Float64Scalar TimeToLength(double t)
        {
            t = Math.Clamp(t, 0.0, 1.0);
            
            return GetSampleByParameter(t).GetLength();
        }

        public override Float64Scalar LengthToTime(double length)
        {
            length = Math.Clamp(length, 0.0, TotalLength);
            
            return GetSampleByLength(length).ParameterValue;
        }

        public override LinFloat64Vector2D GetValue(double t)
        {
            t = Math.Clamp(t, 0.0, 1.0);
            
            return GetSampleByParameter(t).GetPoint();
        }

        public override LinFloat64Vector2D GetDerivative1Value(double t)
        {
            t = Math.Clamp(t, 0.0, 1.0);
            
            return GetSampleByParameter(t).GetTangent();
        }
    }
}
