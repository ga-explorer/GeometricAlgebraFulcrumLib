using System.Collections;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Signals.Filters;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Samplers;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot.SkiaSharp;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using PngEncoder = SixLabors.ImageSharp.Formats.Png.PngEncoder;
// ReSharper disable InconsistentNaming

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier
{
    /// <summary>
    /// Represents a multi-segment PH curve (chain of PhBernsteinCurve2D).
    /// Fixed cumulative length handling, parameter mapping and robustness.
    /// </summary>
    public sealed class PhBernsteinPolyCurve2D :
        Float64Path2D,
        IReadOnlyList<PhBernsteinCurve2D>
    {
        private readonly IReadOnlyList<PhBernsteinCurve2D> _curveList;

        // segmentLengths[i] is length of segment i (0..N-1)
        private readonly double[] _segmentLengthArray;

        // cumLengths length is N+1: cumLengths[0] = 0, cumLengths[i+1] = sum of lengths up to i
        private readonly double[] _cumLengthArray;


        public double TotalLength { get; }

        public double Energy { get; }

        public double MaxCurvature { get; }

        public double PositionError { get; }

        public double TangentError { get; }

        public double AccelerationError { get; }

        public double PhysicalSpeed { get; private set; } = 1;

        public double PhysicalLengthScale { get; private set; } = 1;

        public double PhysicalTotalLength
            => TotalLength * PhysicalLengthScale;

        /// <summary>
        /// Total physical time to traverse the curve at constant physical speed (m/s),
        /// when curve-length units are scaled by PhysicalLengthScale (curveUnits -> meters).
        /// </summary>
        public double PhysicalTotalTime
            => (TotalLength * PhysicalLengthScale) / PhysicalSpeed;

        public int Count
            => _curveList.Count;

        public PhBernsteinCurve2D this[int index]
            => _curveList[index];


        public PhBernsteinPolyCurve2D(IReadOnlyList<PhBernsteinCurve2D> curveList, bool isPeriodic)
            : base(CreateRange(curveList), isPeriodic)
        {
            if (curveList == null) throw new ArgumentNullException(nameof(curveList));
            if (curveList.Count == 0) throw new ArgumentException("curveList must contain at least one segment", nameof(curveList));

            _curveList = curveList;
            var n = curveList.Count;

            _segmentLengthArray = new double[n];
            _cumLengthArray = new double[n + 1];
            _cumLengthArray[0] = 0.0;

            var length = 0.0;
            var energy = 0.0;
            var maxCurvature = 0.0;

            for (var i = 0; i < n; i++)
            {
                var seg = curveList[i];

                // Use GetArcLength(1) as length from 0 to 1 for segment (PhBernsteinCurve2D returns double)
                var segLength = seg.GetArcLength(1.0);

                if (!double.IsFinite(segLength) || segLength < 0.0) segLength = 0.0;

                _segmentLengthArray[i] = segLength;
                length += segLength;
                _cumLengthArray[i + 1] = length;

                // accumulate stats
                energy += seg.GetMaxEnergy();
                maxCurvature = Math.Max(maxCurvature, seg.GetMaxCurvature());
            }

            TotalLength = length;
            Energy = energy;
            MaxCurvature = maxCurvature;

            // compute join errors between consecutive curves
            var pointError = 0.0;
            var tangentError = 0.0;
            var accelerationError = 0.0;

            for (var i = 0; i < n - 1; i++)
            {
                var c1 = curveList[i];
                var c2 = curveList[i + 1];

                var pDist = c1.GetPoint(1).GetDistanceToPoint(c2.GetPoint(0));
                var tDist = c1.GetTangentVector(1).GetDistanceToPoint(c2.GetTangentVector(0));
                var aDist = c1.GetAccelerationVector(1).GetDistanceToPoint(c2.GetAccelerationVector(0));

                pointError += pDist;
                tangentError += tDist;
                accelerationError += aDist;
            }

            PositionError = pointError;
            TangentError = tangentError;
            AccelerationError = accelerationError;
        }

        private static Float64ScalarRange CreateRange(IReadOnlyList<PhBernsteinCurve2D> curveList)
        {
            if (curveList == null)
                throw new ArgumentNullException(nameof(curveList));

            // total length = sum of segment lengths
            var total = 0.0;
            foreach (var c in curveList)
            {
                var segLen = c.GetArcLength(1.0);
                if (!double.IsFinite(segLen) || segLen < 0.0) segLen = 0.0;
                total += segLen;
            }
            return Float64ScalarRange.Create(0.0, total);
        }

        
        public static PhBernsteinPolyCurve2D FromPolyline(Float64Polyline2D polyline, PhBernsteinCurveC2Cache2D c2Cache, Random? randGen = null)
        {
            if (polyline == null) 
                throw new ArgumentNullException(nameof(polyline));
            
            randGen ??= new Random();

            var segCount = polyline.SegmentCount;
            if (segCount < 2) 
                throw new ArgumentException("polyline must have at least two segments", nameof(polyline));

            var cornerCurveList = new List<PhBernsteinCurve2D>(segCount - 1);

            for (var segmentIndex = 0; segmentIndex < segCount - 1; segmentIndex++)
            {
                var s1 = polyline.GetSegment(segmentIndex);
                var s2 = polyline.GetSegment(segmentIndex + 1);

                var p1 = 0.75.Lerp(s1.Point1, s1.Point2);
                var p2 = s1.Point2;
                var p3 = 0.25.Lerp(s2.Point1, s2.Point2);

                var phCurve =
                    c2Cache.JoinLines(p1, p2, p3);

                cornerCurveList.Add(phCurve);
            }

            var curveList = new List<PhBernsteinCurve2D>(cornerCurveList.Count * 2 + 2);

            var cornerCurve1 = cornerCurveList[0];

            {
                var (foundCurve, phCurve) =
                    PhBernsteinCurve2D.TryCreateC2(
                        randGen,
                        3,
                        polyline.GetVertexPoint(0),
                        cornerCurve1.GetPoint(0),
                        polyline.GetVertexTangentVector(0),
                        cornerCurve1.GetTangentVector(0),
                        LinFloat64Vector2D.Zero,
                        cornerCurve1.GetAccelerationVector(0)
                    );

                if (foundCurve)
                {
                    curveList.Add(phCurve);
                    curveList.Add(cornerCurve1);
                }
                else
                {
                    throw new InvalidOperationException("Failed to construct initial join segment");
                }
            }

            for (var i = 1; i < cornerCurveList.Count; i++)
            {
                var cornerCurve2 = cornerCurveList[i];

                var (foundCurve, phCurve) =
                    PhBernsteinCurve2D.TryCreateC2(
                        randGen,
                        4,
                        cornerCurve1.GetPoint(1),
                        cornerCurve2.GetPoint(0),
                        cornerCurve1.GetTangentVector(1),
                        cornerCurve2.GetTangentVector(0),
                        cornerCurve1.GetAccelerationVector(1),
                        cornerCurve2.GetAccelerationVector(0)
                    );

                if (foundCurve)
                {
                    curveList.Add(phCurve);
                    curveList.Add(cornerCurve2);

                    cornerCurve1 = cornerCurve2;
                }
                else
                {
                    throw new InvalidOperationException("Failed to construct middle join segment");
                }
            }

            {
                var (foundCurve, phCurve) =
                    PhBernsteinCurve2D.TryCreateC2(
                        randGen,
                        3,
                        cornerCurve1.GetPoint(1),
                        polyline.GetVertexPoint(polyline.Count - 1),
                        cornerCurve1.GetTangentVector(1),
                        polyline.GetVertexTangentVector(polyline.Count - 1),
                        cornerCurve1.GetAccelerationVector(1),
                        LinFloat64Vector2D.Zero
                    );

                if (foundCurve)
                    curveList.Add(phCurve);
                else
                {
                    throw new InvalidOperationException("Failed to construct final join segment");
                }
            }

            return new PhBernsteinPolyCurve2D(curveList, false);
        }

        public static PhBernsteinPolyCurve2D FromPolyline(Float64Polyline2D polyline, Random? randGen = null)
        {
            if (polyline == null) 
                throw new ArgumentNullException(nameof(polyline));
            
            randGen ??= new Random();

            var segCount = polyline.SegmentCount;
            if (segCount < 2) 
                throw new ArgumentException("polyline must have at least two segments", nameof(polyline));

            var cornerCurveList = new List<PhBernsteinCurve2D>(segCount - 1);

            for (var segmentIndex = 0; segmentIndex < segCount - 1; segmentIndex++)
            {
                var s1 = polyline.GetSegment(segmentIndex);
                var s2 = polyline.GetSegment(segmentIndex + 1);

                var p1 = 0.75.Lerp(s1.Point1, s1.Point2);
                var p2 = s1.Point2;
                var p3 = 0.25.Lerp(s2.Point1, s2.Point2);

                var (foundCurve, phCurve) =
                    PhBernsteinCurve2D.TryJoinLinesC2(
                        randGen, 5, p1, p2, p3
                    );

                if (!foundCurve || phCurve == null)
                    throw new InvalidOperationException("Failed to construct corner PH curve");

                cornerCurveList.Add(phCurve);
            }

            var curveList = new List<PhBernsteinCurve2D>(cornerCurveList.Count * 2 + 2);

            var cornerCurve1 = cornerCurveList[0];

            {
                var (foundCurve, phCurve) =
                    PhBernsteinCurve2D.TryCreateC2(
                        randGen,
                        3,
                        polyline.GetVertexPoint(0),
                        cornerCurve1.GetPoint(0),
                        polyline.GetVertexTangentVector(0),
                        cornerCurve1.GetTangentVector(0),
                        LinFloat64Vector2D.Zero,
                        cornerCurve1.GetAccelerationVector(0)
                    );

                if (foundCurve)
                {
                    curveList.Add(phCurve);
                    curveList.Add(cornerCurve1);
                }
                else
                {
                    throw new InvalidOperationException("Failed to construct initial join segment");
                }
            }

            for (var i = 1; i < cornerCurveList.Count; i++)
            {
                var cornerCurve2 = cornerCurveList[i];

                var (foundCurve, phCurve) =
                    PhBernsteinCurve2D.TryCreateC2(
                        randGen,
                        4,
                        cornerCurve1.GetPoint(1),
                        cornerCurve2.GetPoint(0),
                        cornerCurve1.GetTangentVector(1),
                        cornerCurve2.GetTangentVector(0),
                        cornerCurve1.GetAccelerationVector(1),
                        cornerCurve2.GetAccelerationVector(0)
                    );

                if (foundCurve)
                {
                    curveList.Add(phCurve);
                    curveList.Add(cornerCurve2);

                    cornerCurve1 = cornerCurve2;
                }
                else
                {
                    throw new InvalidOperationException("Failed to construct middle join segment");
                }
            }

            {
                var (foundCurve, phCurve) =
                    PhBernsteinCurve2D.TryCreateC2(
                        randGen,
                        3,
                        cornerCurve1.GetPoint(1),
                        polyline.GetVertexPoint(polyline.Count - 1),
                        cornerCurve1.GetTangentVector(1),
                        polyline.GetVertexTangentVector(polyline.Count - 1),
                        cornerCurve1.GetAccelerationVector(1),
                        LinFloat64Vector2D.Zero
                    );

                if (foundCurve)
                    curveList.Add(phCurve);
                else
                {
                    throw new InvalidOperationException("Failed to construct final join segment");
                }
            }

            return new PhBernsteinPolyCurve2D(curveList, false);
        }

        public override Float64Path2D ToFinitePath() =>
            IsFinite ? this : new PhBernsteinPolyCurve2D(_curveList, false);

        public override Float64Path2D ToPeriodicPath() =>
            IsPeriodic ? this : new PhBernsteinPolyCurve2D(_curveList, true);

        public override bool IsValid()
        {
            return _curveList.Count > 0;
        }


        /// <summary>
        /// Binary search: find segment index i such that cum[i] &lt;= distance &lt; cum[i+1]
        /// Assumes distance is already in [0, Length].
        /// Returns last segment index (n-1) when distance == Length.
        /// </summary>
        private int FindSegmentIndex(double distance)
        {
            var nSegments = _segmentLengthArray.Length;
            if (nSegments == 1) return 0;

            // clamp distance to [0, Length]
            distance = Math.Max(0.0, Math.Min(distance, TotalLength));

            var low = 0;
            var high = nSegments; // we search in cum array positions [0..nSegments]
            // We want i such that cum[i] <= distance < cum[i+1], with i in [0..nSegments-1]

            while (low <= high)
            {
                var mid = (low + high) >> 1;
                // mid corresponds to cum index; valid mid in [0..nSegments]
                if (mid == nSegments) // rightmost cum index (== Length)
                {
                    // if distance == Length -> last segment
                    if (distance >= _cumLengthArray[nSegments]) return nSegments - 1;
                    high = mid - 1;
                    continue;
                }

                var a = _cumLengthArray[mid];
                var b = _cumLengthArray[mid + 1];

                if (distance < a)
                {
                    high = mid - 1;
                }
                else if (distance >= b)
                {
                    low = mid + 1;
                }
                else
                {
                    return Math.Min(mid, nSegments - 1);
                }
            }

            // If loop fails, clamp result
            var idx = Math.Max(0, Math.Min(_segmentLengthArray.Length - 1, low));
            return idx;
        }

        /// <summary>
        /// Clamp distance to [0, Length] and return segment index + local parameter u in [0,1].
        /// </summary>
        private int ClampSegmentIndexForS(double distance, out double u)
        {
            if (_segmentLengthArray.Length == 0)
            {
                u = 0.0;
                return 0;
            }

            if (double.IsNaN(distance)) distance = 0.0;
            if (distance <= 0.0)
            {
                u = 0.0;
                return 0;
            }

            if (distance >= TotalLength)
            {
                u = 1.0;
                return _segmentLengthArray.Length - 1;
            }

            var seg = FindSegmentIndex(distance);
            var s0 = _cumLengthArray[seg];
            var segLen = _segmentLengthArray[seg];

            var local = distance - s0;
            u = segLen > 0.0 ? local / segLen : 0.0;
            // numeric safety
            u = Math.Max(0.0, Math.Min(1.0, u));
            return seg;
        }
        
        private int GetSampleCount(double timeResolution)
        {
            return 1 + (PhysicalTotalLength / (timeResolution * PhysicalSpeed)).CeilingToInt32();
        }


        public override LinFloat64Vector2D GetValue(double distance)
        {
            if (_curveList.Count == 0)
                throw new InvalidOperationException("The curve list is empty.");

            if (distance <= 0)
                return _curveList[0].GetPoint(0);

            if (distance >= TotalLength)
                return _curveList[^1].GetPoint(1);

            // 1. Find which segment this global distance lies in.
            var segIndex = FindSegmentIndex(distance);

            // 2. Get cumulative length at the start of the segment.
            var segStart = _cumLengthArray[segIndex];

            // 3. Compute local distance within the segment.
            var localDistance = distance - segStart;

            // 4. Convert local distance to local parameter (0–1) using LengthToTime().
            var curve = _curveList[segIndex];
            var localParam = curve.LengthToTime(localDistance);

            // 5. Evaluate the curve at that parameter to get the position.
            return curve.GetValue(localParam);

            //if (_curveList.Count == 0) 
            //    throw new InvalidOperationException("No segments.");

            //// clamp and find segment + local parameter
            //var seg = ClampSegmentIndexForS(distance, out var u);
            //var segment = _curveList[seg];

            //// Convert arc-length u (fraction of this segment) into segment time using its LengthToTime
            //// segment.LengthToTime expects a length (distance from segment start), but the PhBernsteinCurve2D API
            //// in your code provides LengthToTime(length) that maps [0, segLength] -> [0,1].
            //var segLen = _segmentLengthArray[seg];
            //var localLength = u * segLen;

            //var t = segment.LengthToTime(localLength);

            //// ensure within valid time bounds
            //t = Math.Max(segment.MinTime, Math.Min(segment.MaxTime, t));

            //return segment.GetValue(t);
        }

        public override LinFloat64Vector2D GetDerivative1Value(double distance)
        {
            if (_curveList.Count == 0) throw new InvalidOperationException("No segments.");

            var seg = ClampSegmentIndexForS(distance, out var u);
            var segment = _curveList[seg];
            var segLen = _segmentLengthArray[seg];
            var localLength = u * segLen;
            var t = segment.LengthToTime(localLength);
            t = Math.Max(segment.MinTime, Math.Min(segment.MaxTime, t));

            return segment.GetDerivative1Value(t);
        }

        public override LinFloat64Vector2D GetDerivative2Value(double distance)
        {
            if (_curveList.Count == 0) throw new InvalidOperationException("No segments.");

            var seg = ClampSegmentIndexForS(distance, out var u);
            var segment = _curveList[seg];
            var segLen = _segmentLengthArray[seg];
            var localLength = u * segLen;
            var t = segment.LengthToTime(localLength);
            t = Math.Max(segment.MinTime, Math.Min(segment.MaxTime, t));

            return segment.GetDerivative2Value(t);
        }

        /// <summary>
        /// Compute curvature at a given arc length s.
        /// </summary>
        public double GetCurvature(double distance)
        {
            var seg = ClampSegmentIndexForS(distance, out var u);
            var curve = _curveList[seg];
            var segLen = _segmentLengthArray[seg];
            var localS = u * segLen;
            var t = curve.LengthToTime(localS);

            return curve.GetCurvature(t);
        }

        public double GetArcLength()
        {
            return TotalLength;
        }

        public override Float64Scalar GetArcLength(double a, double b)
        {
            if (_curveList.Count == 0)
                return 0d;

            // Handle reversed input order
            if (b < a)
                return -GetArcLength(b, a);

            // Clamp to valid range
            a = Math.Max(0d, Math.Min(a, TotalLength));
            b = Math.Max(0d, Math.Min(b, TotalLength));

            if (Math.Abs(b - a) < 1e-15)
                return 0d;

            var sa = CumLengthAt(a);
            var sb = CumLengthAt(b);

            return sb - sa;

            // --- Helper to compute cumulative arc length S(x) ---
            double CumLengthAt(double x)
            {
                // clamp x
                x = Math.Max(0d, Math.Min(x, TotalLength));

                // last segment edge case
                if (x >= TotalLength)
                    return TotalLength;

                // find segment i where cum[i] ≤ x < cum[i+1]
                var i = FindSegmentIndex(x);
                var segLen = _segmentLengthArray[i];
                if (segLen <= 0d)
                    return _cumLengthArray[i];

                var local = x - _cumLengthArray[i];
                var u = local / segLen;
                u = Math.Max(0d, Math.Min(1d, u));

                // partial arc length within this segment
                var segArc = _curveList[i].GetArcLength(u);
                return _cumLengthArray[i] + segArc;
            }
        }


        /// <summary>
        /// Given a global distance in curve units, find the corresponding segment,
        /// local distance within that segment, and local parameter value.
        /// </summary>
        private (PhBernsteinCurve2D Segment, int SegmentIndex, double LocalDistance, double LocalParameter) GetLocalSegmentData(double curveDistance)
        {
            if (_curveList.Count == 0)
                throw new InvalidOperationException("The curve list is empty.");

            if (curveDistance <= 0)
                return (_curveList[0], 0, 0, 0);

            if (curveDistance >= TotalLength)
            {
                var last = _curveList[^1];
                return (last, _curveList.Count - 1, _segmentLengthArray[^1], 1);
            }

            // 1. Find the segment index
            var segIndex = FindSegmentIndex(curveDistance);

            // 2. Compute local distance within segment
            var segStart = _cumLengthArray[segIndex];
            //var segLength = _segmentLengthArray[segIndex];
            var localDistance = curveDistance - segStart;

            // 3. Convert local distance to local parameter
            var seg = _curveList[segIndex];
            var localParam = seg.LengthToTime(localDistance);

            return (seg, segIndex, localDistance, localParam);
        }



        /// <summary>
        /// Set physical length scaling factor and fixed physical travel speed of curve
        /// </summary>
        /// <param name="physicalLengthScale"></param>
        /// <param name="physicalSpeed"></param>
        /// <exception cref="ArgumentException"></exception>
        public void SetPhysicalProperties(double physicalLengthScale, double physicalSpeed)
        {
            if (!physicalLengthScale.IsFinite() || physicalLengthScale < 1)
                throw new ArgumentException(nameof(physicalLengthScale));

            if (!physicalSpeed.IsFinite() || physicalSpeed <= 0)
                throw new ArgumentException(nameof(physicalSpeed));

            PhysicalLengthScale = physicalLengthScale;
            PhysicalSpeed = physicalSpeed;
        }


        /// <summary>
        /// Get the total distance traveled in curve units at time t, given physical speed (m/s)
        /// and PhysicalLengthScale (meters per curve unit).
        /// </summary>
        public double GetCurveDistanceAtPhysicalTime(double time)
        {
            var totalTime = PhysicalTotalTime;
            if (time <= 0) return 0.0;
            if (time >= totalTime) return TotalLength;

            // s_phys = speed * time (meters)
            // s_curve = s_phys / lengthScale (curve units)
            return (PhysicalSpeed * time) / PhysicalLengthScale;
        }

        /// <summary>
        /// Get the total distance traveled in curve units at time t, given physical speed (m/s)
        /// and PhysicalLengthScale (meters per curve unit).
        /// </summary>
        public double GetCurveDistanceAtPhysicalDistance(double distance)
        {
            if (distance <= 0) return 0.0;
            if (distance >= PhysicalTotalLength) return TotalLength;

            return distance / PhysicalLengthScale;
        }


        /// <summary>
        /// Get the total distance traveled in physical units at time t, given physical speed (m/s)
        /// and PhysicalLengthScale (meters per curve unit).
        /// </summary>
        public double GetPhysicalDistanceAtTime(double time)
        {
            var totalTime = PhysicalTotalTime;
            if (time <= 0) return 0.0;
            if (time >= totalTime) return PhysicalTotalLength;

            // s_phys = speed * time (meters)
            return PhysicalSpeed * time;
        }

        /// <summary>
        /// Get the total time required in seconds to move on curve for a
        /// given distance in meters
        /// </summary>
        public double GetPhysicalTimeAtDistance(double distance)
        {
            var totalLength = PhysicalTotalLength;
            if (distance <= 0) return 0.0;
            if (distance >= totalLength) return PhysicalTotalTime;

            // t_phys = distance / speed (seconds)
            return distance / PhysicalSpeed;
        }
        
        public double[] GetPhysicalTimeSamples(double timeResolution = 0.1)
        {
            var sampleCount = GetSampleCount(timeResolution);
            var signalArray = new double[sampleCount];
            var distanceResolution = PhysicalTotalLength / (sampleCount - 1d);

            for (var i = 0; i < sampleCount; i++) 
                signalArray[i] = GetPhysicalTimeAtDistance(i * distanceResolution);

            return signalArray;
        }


        /// <summary>
        /// Get the 2D physical position vector at time t (meters). lengthScale converts curve units -> meters.
        /// </summary>
        public LinFloat64Vector2D GetPhysicalPositionAtTime(double time)
        {
            var curveDistance = GetCurveDistanceAtPhysicalTime(time);

            // GetValue expects distance in curve units; returns coordinates in curve
            // coordinate units.
            //
            // If curve coordinates are already in meters, no additional scaling required;
            // otherwise if your curve geometry is unitless, and you want physical
            // coordinates scaled by PhysicalLengthScale, you can multiply result by
            // PhysicalLengthScale here.
            //
            // By default, we assume coordinates are in same units as arc-length,
            // so convert position from curve units to meters by scaling by PhysicalLengthScale.

            // Convert coordinates (curve units) -> physical meters:
            return GetValue(curveDistance) * PhysicalLengthScale;
        }

        /// <summary>
        /// Get physical position (in meters) corresponding to a given physical distance (in meters)
        /// along the poly-curve. Converts to curve units internally.
        /// </summary>
        public LinFloat64Vector2D GetPhysicalPositionAtDistance(double distance)
        {
            // Convert physical distance (meters) -> curve distance (curve units)
            var curveDistance = distance / PhysicalLengthScale;

            // Evaluate in curve units, then scale coordinates to meters
            return GetValue(curveDistance) * PhysicalLengthScale;
        }
        
        public LinFloat64Vector2D[] GetPhysicalPositionSamples(double timeResolution = 0.1)
        {
            var sampleCount = GetSampleCount(timeResolution);
            var signalArray = new LinFloat64Vector2D[sampleCount];
            var distanceResolution = PhysicalTotalLength / (sampleCount - 1d);

            for (var i = 0; i < sampleCount; i++) 
                signalArray[i] = GetPhysicalPositionAtDistance(i * distanceResolution);

            return signalArray;
        }


        /// <summary>
        /// Get the physical velocity vector at time t (m/s).
        /// Uses: v_vec = (dr/ds_curve) * (ds_curve/dt) where ds_curve/dt = speed / lengthScale
        /// and then converts coordinates to meters by multiplying dr/ds_curve (curve units -> curve units)
        /// and scaling by (1) lengthScale in dr and (2) ds/dt accordingly. The net factor is (speed / lengthScale) * lengthScale = speed.
        /// However, to be safe for arbitrary coordinate unit conventions we do the explicit math.
        /// </summary>
        public LinFloat64Vector2D GetPhysicalVelocityAtTime(double time)
        {
            var curveDistance = GetCurveDistanceAtPhysicalTime(time);

            // derivative w.r.t curve arc-length units
            var dr_ds = GetDerivative1Value(curveDistance); // units: curve-units / curve-unit-distance

            // Convert dr_ds from curve-units per curve-length to meters per curve-length:
            // multiply spatial units by lengthScale
            var dr_ds_m = dr_ds * PhysicalLengthScale;

            // ds_curve/dt = speed (m/s) / lengthScale (m per curve unit) = speed / lengthScale (curve units per second)
            var ds_dt = PhysicalSpeed / PhysicalLengthScale;

            // v = dr_ds_m * ds_dt => units: meters * (curve-units per second / curve-unit) => m/s
            return dr_ds_m * ds_dt;
        }

        /// <summary>
        /// Get physical tangent vector (direction) at a given physical distance.
        /// Scales derivatives correctly to express components in meters.
        /// </summary>
        public LinFloat64Vector2D GetPhysicalVelocityAtDistance(double distance)
        {
            var curveDistance = distance / PhysicalLengthScale;

            // Tangent direction (dimensionless)
            var tangent = GetTangentVector(curveDistance);

            // Scale spatial coordinates from curve units to meters
            return tangent * PhysicalLengthScale;
        }
        
        public LinFloat64Vector2D[] GetPhysicalVelocitySamples(double timeResolution = 0.1)
        {
            var sampleCount = GetSampleCount(timeResolution);
            var signalArray = new LinFloat64Vector2D[sampleCount];
            var distanceResolution = PhysicalTotalLength / (sampleCount - 1d);

            for (var i = 0; i < sampleCount; i++) 
                signalArray[i] = GetPhysicalVelocityAtDistance(i * distanceResolution);

            return signalArray;
        }


        /// <summary>
        /// Get physical scalar speed at time t (m/s) — with constant-speed profile it equals input speed.
        /// Provided for completeness; remains constant.
        /// </summary>
        public double GetPhysicalSpeedAtTime(double time)
        {
            return PhysicalSpeed;
        }

        /// <summary>
        /// Get physical scalar speed at distance s (m) — with constant-speed profile it equals input speed.
        /// Provided for completeness; remains constant.
        /// </summary>
        public double GetPhysicalSpeedAtDistance(double distance)
        {
            return PhysicalSpeed;
        }
        
        public double[] GetPhysicalSpeedSamples(double timeResolution = 0.1, int medianFilterSamples = 0)
        {
            var sampleCount = GetSampleCount(timeResolution);
            var signalArray = new double[sampleCount];
            var distanceResolution = PhysicalTotalLength / (sampleCount - 1d);

            for (var i = 0; i < sampleCount; i++) 
                signalArray[i] = GetPhysicalSpeedAtDistance(i * distanceResolution);

            return medianFilterSamples < 1 
                ? signalArray 
                : MedianFilter.Filter(signalArray, medianFilterSamples).ToArray();
        }


        /// <summary>
        /// Get the physical acceleration vector at time t (m/s^2).
        /// a = d^2r/dt^2 = d^2r/ds^2 * (ds/dt)^2, and convert coordinates to meters.
        /// </summary>
        public LinFloat64Vector2D GetPhysicalAccelerationAtTime(double time)
        {
            var sCurve = GetCurveDistanceAtPhysicalTime(time);

            // second derivative wrt curve arc-length
            // units: curve-units / (curve-unit)^2
            var d2r_ds2 = GetDerivative2Value(sCurve);

            // Convert spatial units to meters: multiply spatial components by PhysicalLengthScale
            // After conversion, units become meters / (curve-unit)^2
            var d2r_ds2_m = d2r_ds2 * PhysicalLengthScale;

            // ds/dt = PhysicalSpeed / PhysicalLengthScale (curve-units per second)
            var ds_dt = PhysicalSpeed / PhysicalLengthScale;

            // a = d2r_ds2_m * (ds_dt)^2 => (m / curveUnit^2) * (curveUnit^2 / s^2) = m/s^2
            var factor = ds_dt * ds_dt;

            return d2r_ds2_m * factor;
        }

        /// <summary>
        /// Get physical acceleration (m/s²) at a given physical distance, assuming constant UAV speed (m/s).
        /// a = v² * curvature * normal, derived from the curvature vector.
        /// </summary>
        public LinFloat64Vector2D GetPhysicalAccelerationAtDistance(double distance)
        {
            // d²r/ds² (curve units⁻¹)
            // Convert to meters: multiply spatial by lengthScale, divide by lengthScale² → 1/lengthScale
            var curveDistance = distance / PhysicalLengthScale;
            var curvatureVec =
                GetDerivative2Value(curveDistance);

            var curvatureVec_m = curvatureVec / PhysicalLengthScale;
            return curvatureVec_m * (PhysicalSpeed * PhysicalSpeed);
        }
        
        public LinFloat64Vector2D[] GetPhysicalAccelerationSamples(double timeResolution = 0.1)
        {
            var sampleCount = GetSampleCount(timeResolution);
            var signalArray = new LinFloat64Vector2D[sampleCount];
            var distanceResolution = PhysicalTotalLength / (sampleCount - 1d);

            for (var i = 0; i < sampleCount; i++) 
                signalArray[i] = GetPhysicalAccelerationAtDistance(i * distanceResolution);

            return signalArray;
        }


        /// <summary>
        /// Compute the UAV’s physical lateral (centripetal) acceleration magnitude [m/s²]
        /// at a given physical time. Uses a_lat = v² * κ, where κ is curvature [1/m].
        /// </summary>
        public double GetPhysicalLateralAccelerationAtTime(double time)
        {
            // Get curvature in 1/m at the current time
            var curvature = GetPhysicalCurvatureAtTime(time);

            // Lateral (centripetal) acceleration magnitude = v² * κ
            return PhysicalSpeed * PhysicalSpeed * Math.Abs(curvature);
        }

        /// <summary>
        /// Compute the UAV’s physical lateral (centripetal) acceleration magnitude [m/s²]
        /// at a given physical distance. Uses a_lat = v² * κ, where κ is curvature [1/m].
        /// </summary>
        public double GetPhysicalLateralAccelerationAtDistance(double distance)
        {
            var curvature = GetPhysicalCurvatureAtDistance(distance);

            return PhysicalSpeed * PhysicalSpeed * Math.Abs(curvature);
        }
        
        public double[] GetPhysicalLateralAccelerationSamples(double timeResolution = 0.1, int medianFilterSamples = 0)
        {
            var sampleCount = GetSampleCount(timeResolution);
            var signalArray = new double[sampleCount];
            var distanceResolution = PhysicalTotalLength / (sampleCount - 1d);

            for (var i = 0; i < sampleCount; i++) 
                signalArray[i] = GetPhysicalLateralAccelerationAtDistance(i * distanceResolution);
            
            return medianFilterSamples < 1 
                ? signalArray 
                : MedianFilter.Filter(signalArray, medianFilterSamples).ToArray();
        }


        /// <summary>
        /// Get physical curvature (1/m) at a given physical distance.
        /// </summary>
        public double GetPhysicalCurvatureAtTime(double time)
        {
            var curveDistance =
                GetPhysicalDistanceAtTime(time) / PhysicalLengthScale;

            // curvature in 1/curve-units -> 1/m
            return GetCurvature(curveDistance) / PhysicalLengthScale;
        }

        /// <summary>
        /// Get physical curvature (1/m) at a given physical distance.
        /// </summary>
        public double GetPhysicalCurvatureAtDistance(double distance)
        {
            var curveDistance = distance / PhysicalLengthScale;

            // curvature in 1/curve-units -> 1/m
            return GetCurvature(curveDistance) / PhysicalLengthScale;
        }

        public double[] GetPhysicalCurvatureSamples(double timeResolution = 0.1, int medianFilterSamples = 0)
        {
            var sampleCount = GetSampleCount(timeResolution);
            var signalArray = new double[sampleCount];
            var distanceResolution = PhysicalTotalLength / (sampleCount - 1d);

            for (var i = 0; i < sampleCount; i++) 
                signalArray[i] = GetPhysicalCurvatureAtDistance(i * distanceResolution);

            return medianFilterSamples < 1 
                ? signalArray 
                : MedianFilter.Filter(signalArray, medianFilterSamples).ToArray();
        }

        
        /// <summary>
        /// Compute physical turn radius [m] at a given physical time.
        /// </summary>
        public double GetPhysicalTurnRadiusAtTime(double time)
        {
            var curvature = GetPhysicalCurvatureAtTime(time);
            if (Math.Abs(curvature) < 1e-12) return double.PositiveInfinity;

            return 1.0 / Math.Abs(curvature);
        }

        /// <summary>
        /// Compute physical turn radius [m] at a given physical distance.
        /// </summary>
        public double GetPhysicalTurnRadiusAtDistance(double distance)
        {
            var curvature = GetPhysicalCurvatureAtDistance(distance);
            if (Math.Abs(curvature) < 1e-12) return double.PositiveInfinity;

            return 1.0 / Math.Abs(curvature);
        }

        public double[] GetPhysicalTurnRadiusSamples(double timeResolution = 0.1, int medianFilterSamples = 0)
        {
            var sampleCount = GetSampleCount(timeResolution);
            var signalArray = new double[sampleCount];
            var distanceResolution = PhysicalTotalLength / (sampleCount - 1d);

            for (var i = 0; i < sampleCount; i++) 
                signalArray[i] = GetPhysicalTurnRadiusAtDistance(i * distanceResolution);

            return medianFilterSamples < 1 
                ? signalArray 
                : MedianFilter.Filter(signalArray, medianFilterSamples).ToArray();
        }

        
        /// <summary>
        /// Compute required physical bank angle [rad] at a given physical time,
        /// assuming coordinated level flight.
        /// </summary>
        public double GetPhysicalBankAngleAtTime(double time)
        {
            var curvature = GetPhysicalCurvatureAtTime(time);
            const double g = 9.80665;
            var aLat = PhysicalSpeed * PhysicalSpeed * curvature;
            return Math.Atan(aLat / g);
        }

        /// <summary>
        /// Compute required physical bank angle [rad] at a given physical distance,
        /// assuming coordinated level flight.
        /// </summary>
        public double GetPhysicalBankAngleAtDistance(double distance)
        {
            var curvature = GetPhysicalCurvatureAtDistance(distance);
            const double g = 9.80665;
            var aLat = PhysicalSpeed * PhysicalSpeed * curvature;
            return Math.Atan(aLat / g);
        }

        public double[] GetPhysicalBankAngleSamples(double timeResolution = 0.1, int medianFilterSamples = 0)
        {
            var sampleCount = GetSampleCount(timeResolution);
            var signalArray = new double[sampleCount];
            var distanceResolution = PhysicalTotalLength / (sampleCount - 1d);

            for (var i = 0; i < sampleCount; i++) 
                signalArray[i] = GetPhysicalBankAngleAtDistance(i * distanceResolution);

            return medianFilterSamples < 1 
                ? signalArray 
                : MedianFilter.Filter(signalArray, medianFilterSamples).ToArray();
        }


        /// <summary>
        /// Get full physical geometry (position, tangent, curvature, acceleration) at a given physical time.
        /// </summary>
        public (LinFloat64Vector2D Position, LinFloat64Vector2D Velocity, double Curvature, LinFloat64Vector2D Acceleration) GetPhysicalGeometryAtPhysicalTime(double time)
        {
            var position = GetPhysicalPositionAtTime(time);
            var velocity = GetPhysicalVelocityAtTime(time);
            var curvature = GetPhysicalCurvatureAtTime(time);
            var acceleration = GetPhysicalAccelerationAtTime(time);

            return (position, velocity, curvature, acceleration);
        }

        /// <summary>
        /// Get full physical geometry (position, tangent, curvature, acceleration) at a given physical distance.
        /// </summary>
        public (LinFloat64Vector2D Position, LinFloat64Vector2D Velocity, double Curvature, LinFloat64Vector2D Acceleration) GetPhysicalGeometryAtPhysicalDistance(double distance)
        {
            var position = GetPhysicalPositionAtDistance(distance);
            var velocity = GetPhysicalVelocityAtDistance(distance);
            var curvature = GetPhysicalCurvatureAtDistance(distance);
            var acceleration = GetPhysicalAccelerationAtDistance(distance);

            return (position, velocity, curvature, acceleration);
        }


        /// <summary>
        /// Represents the numerical range (min, max, mean) of a physical quantity.
        /// </summary>
        public readonly record struct PhysicalQuantityRange
        {
            public static PhysicalQuantityRange Create(IEnumerable<double> signalData)
            {
                var count = 0;
                var sum = 0.0;
                var min = double.PositiveInfinity;
                var max = double.NegativeInfinity;

                foreach (var value in signalData)
                {
                    if (!double.IsFinite(value)) continue;

                    min = Math.Min(min, value);
                    max = Math.Max(max, value);
                    sum += value;
                    count++;
                }

                return count > 0
                    ? new PhysicalQuantityRange(min, max, sum / (count - 1d))
                    : new PhysicalQuantityRange(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity);
            }

            
            public double Min { get; }
            
            public double Max { get; }
            
            public double Mean { get; }


            /// <summary>
            /// Represents the numerical range (min, max, mean) of a physical quantity.
            /// </summary>
            private PhysicalQuantityRange(double min, double max, double mean)
            {
                Min = min;
                Max = max;
                Mean = mean;
            }
            
            public void Deconstruct(out double min, out double max, out double mean)
            {
                min = Min;
                max = Max;
                mean = Mean;
            }

            public override string ToString()
            {
                return $"Min={Min:F4}, Max={Max:F4}, Mean={Mean:F4}";
            }
        }

        /// <summary>
        /// Compute min, max, and mean values of physical curvature (1/m).
        /// </summary>
        public PhysicalQuantityRange GetPhysicalCurvatureRange(double timeResolution = 0.1, int medianFilterSamples = 0)
        {
            var signalArray = 
                GetPhysicalCurvatureSamples(timeResolution, medianFilterSamples).Select(Math.Abs);

            return PhysicalQuantityRange.Create(signalArray);
        }

        /// <summary>
        /// Compute min, max, and mean of physical lateral acceleration (m/s²).
        /// </summary>
        public PhysicalQuantityRange GetPhysicalLateralAccelerationRange(double timeResolution = 0.1, int medianFilterSamples = 0)
        {
            var signalArray = 
                GetPhysicalLateralAccelerationSamples(timeResolution, medianFilterSamples);

            return PhysicalQuantityRange.Create(signalArray);
        }

        /// <summary>
        /// Compute min, max, and mean of required physical bank angle (radians).
        /// </summary>
        public PhysicalQuantityRange GetPhysicalBankAngleRange(double timeResolution = 0.1, int medianFilterSamples = 0)
        {
            var signalArray = 
                GetPhysicalBankAngleSamples(timeResolution, medianFilterSamples);

            return PhysicalQuantityRange.Create(signalArray);
        }

        /// <summary>
        /// Compute min, max, and mean of physical turn radius (m).
        /// </summary>
        public PhysicalQuantityRange GetPhysicalTurnRadiusRange(double timeResolution = 0.1, int medianFilterSamples = 0)
        {
            var signalArray = 
                GetPhysicalTurnRadiusSamples(timeResolution, medianFilterSamples);

            return PhysicalQuantityRange.Create(signalArray);
        }

        /// <summary>
        /// Summarize all key physical ranges (for UAV trajectory optimization).
        /// </summary>
        public Dictionary<string, PhysicalQuantityRange> GetPhysicalQuantitiesSummary(double timeResolution = 0.1, int medianFilterSamples = 0)
        {
            return new Dictionary<string, PhysicalQuantityRange>
            {
                ["Curvature (1/m)"] = GetPhysicalCurvatureRange(timeResolution, medianFilterSamples),
                ["Lateral Accel (m/s²)"] = GetPhysicalLateralAccelerationRange(timeResolution, medianFilterSamples),
                ["Bank Angle (rad)"] = GetPhysicalBankAngleRange(timeResolution, medianFilterSamples),
                ["Turn Radius (m)"] = GetPhysicalTurnRadiusRange(timeResolution, medianFilterSamples)
            };
        }


        public double GetMinObstacleDistance(IReadOnlyList<Float64LineSegment2D> obstacleList)
        {
            if (obstacleList == null) throw new ArgumentNullException(nameof(obstacleList));

            var min = double.PositiveInfinity;
            foreach (var curve in _curveList)
            {
                var d = curve.ToAdaptivePolyline().GetShortestDistanceTo(obstacleList);
                if (d < min) min = d;
            }
            return min;
        }

        /// <summary>
        /// Sample points across all segments (concatenated).
        /// </summary>
        public List<LinFloat64Vector2D> SamplePoints(SampledPhBernsteinCurve2D.SamplingOptions samplingOptions)
        {
            if (samplingOptions == null) throw new ArgumentNullException(nameof(samplingOptions));

            var pts = new List<LinFloat64Vector2D>();
            // reuse a sampler instance per segment to avoid repeated allocations (sampler currently stateless per Generate call)
            foreach (var seg in _curveList)
            {
                var sampler = new SampledPhBernsteinCurve2D(false, samplingOptions);
                sampler.Generate(seg);
                foreach (var p in sampler.Points)
                    pts.Add(LinFloat64Vector2D.Create(p.X, p.Y));
            }
            return pts;
        }

        public Float64Polyline2D ToAdaptivePolyline(PhBernsteinCurve2D.SamplingOptions? samplingOptions = null)
        {
            samplingOptions ??= new PhBernsteinCurve2D.SamplingOptions();

            // Concatenate adaptive polylines from each segment into one polyline, without duplicating shared join points.
            var points = new List<LinFloat64Vector2D>();
            for (var i = 0; i < _curveList.Count; i++)
            {
                var poly = _curveList[i].ToAdaptivePolyline(samplingOptions);

                // avoid duplicate first point (shared with previous)
                points.AddRange(i == 0 ? poly : poly.Skip(1));
            }

            return new Float64Polyline2D(points).RemoveShortSegments(1e-5);
        }


        public PhBernsteinPolyCurve2D ReflectOnX()
        {
            return new PhBernsteinPolyCurve2D(
                _curveList.Select(c => c.ReflectOnX()).ToArray(),
                IsPeriodic
            );
        }

        public PhBernsteinPolyCurve2D ScaleBy(double scalingFactor)
        {
            return new PhBernsteinPolyCurve2D(
                _curveList.Select(c => c.ScaleBy(scalingFactor)).ToArray(),
                IsPeriodic
            );
        }

        public PhBernsteinPolyCurve2D MapUsing(Float64SimilarityAffineMap2D map)
        {
            return new PhBernsteinPolyCurve2D(
                _curveList.Select(c => c.MapUsing(map)).ToArray(),
                IsPeriodic
            );
        }


        private static Image<Rgba32> RenderPlotToImage(PlotModel model, int width = 2049, int height = 600)
        {
            using var stream = new MemoryStream();

            // Use SkiaSharp renderer for OxyPlot to a PNG stream
            var exporter = new PngExporter
            {
                Width = width,
                Height = height,
            };
            exporter.Export(model, stream);
            stream.Position = 0;

            // Load back into ImageSharp Image
            return Image.Load<Rgba32>(stream);
        }

        public Image<Rgba32> PlotCurvePositionVsLength(int sampleCount = 2049)
        {
            if (sampleCount < 2)
                throw new ArgumentOutOfRangeException(nameof(sampleCount));

            var model = new PlotModel
            {
                Title = "Position Components vs Arc Length",
                Background = OxyColors.White
            };

            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Arc Length (s)" });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Position (units)" });

            var xSeries = new LineSeries { Title = "X(s)", Color = OxyColors.Blue, StrokeThickness = 2 };
            var ySeries = new LineSeries { Title = "Y(s)", Color = OxyColors.Red, StrokeThickness = 2 };
            var normSeries = new LineSeries { Title = "|r(s)|", Color = OxyColors.DarkGreen, StrokeThickness = 2, LineStyle = LineStyle.Dash };

            for (var i = 0; i <= sampleCount; i++)
            {
                var s = i / (double)sampleCount * TotalLength;
                var p = GetValue(s);
                var norm = Math.Sqrt(p.X * p.X + p.Y * p.Y);

                xSeries.Points.Add(new DataPoint(s, p.X));
                ySeries.Points.Add(new DataPoint(s, p.Y));
                normSeries.Points.Add(new DataPoint(s, norm));
            }

            model.Series.Add(xSeries);
            model.Series.Add(ySeries);
            model.Series.Add(normSeries);

            return RenderPlotToImage(model);
        }

        public Image<Rgba32> PlotCurveTangentVsLength(int sampleCount = 2049)
        {
            if (sampleCount < 2)
                throw new ArgumentOutOfRangeException(nameof(sampleCount));

            var model = new PlotModel
            {
                Title = "Tangent Components vs Arc Length",
                Background = OxyColors.White
            };

            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Arc Length (s)" });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Tangent Components" });

            var txSeries = new LineSeries { Title = "Tx(s)", Color = OxyColors.Blue, StrokeThickness = 2 };
            var tySeries = new LineSeries { Title = "Ty(s)", Color = OxyColors.Red, StrokeThickness = 2 };
            var normSeries = new LineSeries { Title = "|T(s)|", Color = OxyColors.DarkGreen, StrokeThickness = 2, LineStyle = LineStyle.Dash };

            for (var i = 0; i <= sampleCount; i++)
            {
                var s = i / (double)sampleCount * TotalLength;
                var tVec = GetDerivative1Value(s);
                var norm = Math.Sqrt(tVec.X * tVec.X + tVec.Y * tVec.Y);

                txSeries.Points.Add(new DataPoint(s, tVec.X));
                tySeries.Points.Add(new DataPoint(s, tVec.Y));
                normSeries.Points.Add(new DataPoint(s, norm));
            }

            model.Series.Add(txSeries);
            model.Series.Add(tySeries);
            model.Series.Add(normSeries);

            return RenderPlotToImage(model);
        }

        public Image<Rgba32> PlotCurveAccelerationVsLength(int sampleCount = 2049)
        {
            if (sampleCount < 2)
                throw new ArgumentOutOfRangeException(nameof(sampleCount));

            var model = new PlotModel
            {
                Title = "Acceleration Components vs Arc Length",
                Background = OxyColors.White
            };

            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Arc Length (s)" });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Acceleration Components" });

            var axSeries = new LineSeries { Title = "Ax(s)", Color = OxyColors.Blue, StrokeThickness = 2 };
            var aySeries = new LineSeries { Title = "Ay(s)", Color = OxyColors.Red, StrokeThickness = 2 };
            var normSeries = new LineSeries { Title = "|A(s)|", Color = OxyColors.DarkGreen, StrokeThickness = 2, LineStyle = LineStyle.Dash };

            for (var i = 0; i <= sampleCount; i++)
            {
                var s = i / (double)sampleCount * TotalLength;
                var aVec = GetDerivative2Value(s);
                var norm = Math.Sqrt(aVec.X * aVec.X + aVec.Y * aVec.Y);

                axSeries.Points.Add(new DataPoint(s, aVec.X));
                aySeries.Points.Add(new DataPoint(s, aVec.Y));
                normSeries.Points.Add(new DataPoint(s, norm));
            }

            model.Series.Add(axSeries);
            model.Series.Add(aySeries);
            model.Series.Add(normSeries);

            return RenderPlotToImage(model);
        }

        public Image<Rgba32> PlotCurveCurvatureVsLength(int sampleCount = 2049)
        {
            if (sampleCount < 2)
                throw new ArgumentOutOfRangeException(nameof(sampleCount));

            var model = new PlotModel
            {
                Title = "Curvature vs Arc Length",
                Background = OxyColors.White
            };

            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Arc Length (s)" });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Curvature κ(s)" });

            var series = new LineSeries { Color = OxyColors.Blue, StrokeThickness = 2 };

            for (var i = 0; i <= sampleCount; i++)
            {
                var s = i / (double)sampleCount * TotalLength;

                var k = GetCurvature(s);
                series.Points.Add(new DataPoint(s, k));
            }

            model.Series.Add(series);
            return RenderPlotToImage(model);
        }

        public Image<Rgba32> PlotCurveEnergyDensityVsLength(int sampleCount = 2049)
        {
            if (sampleCount < 2)
                throw new ArgumentOutOfRangeException(nameof(sampleCount));

            var model = new PlotModel
            {
                Title = "Energy Density vs Arc Length",
                Background = OxyColors.White
            };

            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Arc Length (s)" });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Energy Density e(s)" });

            var series = new LineSeries { Color = OxyColors.Red, StrokeThickness = 2 };

            var totalLength = TotalLength;
            for (var i = 0; i <= sampleCount; i++)
            {
                var s = i / (double)sampleCount * totalLength;
                var k = GetCurvature(s);
                var e = k * k; // simple elastic bending energy density ∝ κ²
                series.Points.Add(new DataPoint(s, e));
            }

            model.Series.Add(series);
            return RenderPlotToImage(model);
        }

        public Image<Rgba32> PlotCurveEnergyVsLength(int sampleCount = 2049)
        {
            if (sampleCount < 2)
                throw new ArgumentOutOfRangeException(nameof(sampleCount));

            var model = new PlotModel
            {
                Title = "Cumulative Energy vs Arc Length",
                Background = OxyColors.White
            };

            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Arc Length (s)" });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Cumulative Energy E(s)" });

            var series = new LineSeries { Color = OxyColors.DarkGreen, StrokeThickness = 2 };

            var cumulativeEnergy = 0d;
            var prevS = 0d;
            var prevE = 0d;

            for (var i = 0; i <= sampleCount; i++)
            {
                var s = i / (double)sampleCount * TotalLength;
                var k = GetCurvature(s);
                var e = k * k; // energy density
                var ds = s - prevS;
                cumulativeEnergy += 0.5 * (e + prevE) * ds; // trapezoidal integration

                series.Points.Add(new DataPoint(s, cumulativeEnergy));

                prevS = s;
                prevE = e;
            }

            model.Series.Add(series);
            return RenderPlotToImage(model);
        }

        public void PlotCurveMetrics(string filePath, int sampleCount = 2049)
        {
            var encoder = new PngEncoder()
            {
                CompressionLevel = PngCompressionLevel.NoCompression,
                TransparentColorMode = PngTransparentColorMode.Preserve,
                ColorType = PngColorType.RgbWithAlpha
            };

            PlotCurvePositionVsLength(sampleCount).SaveAsPng(
                Path.Combine(filePath, "Curve Position vs arclength.png"),
                encoder
            );

            PlotCurveTangentVsLength(sampleCount).SaveAsPng(
                Path.Combine(filePath, "Curve Tangent vs arclength.png"),
                encoder
            );

            PlotCurveAccelerationVsLength(sampleCount).SaveAsPng(
                Path.Combine(filePath, "Curve Acceleration vs arclength.png"),
                encoder
            );

            PlotCurveCurvatureVsLength(sampleCount).SaveAsPng(
                Path.Combine(filePath, "Curve Curvature vs arclength.png"),
                encoder
            );

            PlotCurveEnergyDensityVsLength(sampleCount).SaveAsPng(
                Path.Combine(filePath, "Curve Energy Density vs arclength.png"),
                encoder
            );

            PlotCurveEnergyVsLength(sampleCount).SaveAsPng(
                Path.Combine(filePath, "Curve Energy vs arclength.png"),
                encoder
            );
        }


        public Image<Rgba32> PlotPhysicalPositionVsTime(int sampleCount = 2049)
        {
            if (sampleCount < 2) throw new ArgumentOutOfRangeException(nameof(sampleCount));

            var totalTime = PhysicalTotalTime;
            var model = new PlotModel
            {
                Title = $"UAV Position vs Time (v = {PhysicalSpeed:F2} m/s)",
                Background = OxyColors.White
            };

            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Time (s)" });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Position (m)" });

            var xSeries = new LineSeries { Title = "X(t)", Color = OxyColors.Blue, StrokeThickness = 2 };
            var ySeries = new LineSeries { Title = "Y(t)", Color = OxyColors.Red, StrokeThickness = 2 };
            var normSeries = new LineSeries { Title = "|r(t)|", Color = OxyColors.DarkGreen, StrokeThickness = 2, LineStyle = LineStyle.Dash };

            for (var i = 0; i <= sampleCount; i++)
            {
                var t = i / (double)sampleCount * totalTime;
                var pos = GetPhysicalPositionAtTime(t);
                var norm = Math.Sqrt(pos.X * pos.X + pos.Y * pos.Y);

                xSeries.Points.Add(new DataPoint(t, pos.X));
                ySeries.Points.Add(new DataPoint(t, pos.Y));
                normSeries.Points.Add(new DataPoint(t, norm));
            }

            model.Series.Add(xSeries);
            model.Series.Add(ySeries);
            model.Series.Add(normSeries);

            return RenderPlotToImage(model);
        }

        /// <summary>
        /// Plot physical X, Y, and |r| vs physical distance.
        /// </summary>
        public Image<Rgba32> PlotPhysicalPositionVsDistance(int sampleCount = 2049)
        {
            if (sampleCount < 2) throw new ArgumentOutOfRangeException(nameof(sampleCount));

            var totalDistance = PhysicalTotalLength;
            var model = new PlotModel
            {
                Title = "Physical Position Components vs Distance",
                Background = OxyColors.White
            };

            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Distance (m)" });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Position (m)" });

            var xSeries = new LineSeries { Title = "X(s)", Color = OxyColors.Blue, StrokeThickness = 2 };
            var ySeries = new LineSeries { Title = "Y(s)", Color = OxyColors.Red, StrokeThickness = 2 };
            var normSeries = new LineSeries { Title = "|r(s)|", Color = OxyColors.DarkGreen, LineStyle = LineStyle.Dash, StrokeThickness = 2 };

            for (var i = 0; i <= sampleCount; i++)
            {
                var d = i / (double)sampleCount * totalDistance;
                var pos = GetPhysicalPositionAtDistance(d);
                var norm = Math.Sqrt(pos.X * pos.X + pos.Y * pos.Y);

                xSeries.Points.Add(new DataPoint(d, pos.X));
                ySeries.Points.Add(new DataPoint(d, pos.Y));
                normSeries.Points.Add(new DataPoint(d, norm));
            }

            model.Series.Add(xSeries);
            model.Series.Add(ySeries);
            model.Series.Add(normSeries);

            return RenderPlotToImage(model);
        }


        public Image<Rgba32> PlotPhysicalVelocityVsTime(int sampleCount = 2049)
        {
            if (sampleCount < 2) throw new ArgumentOutOfRangeException(nameof(sampleCount));

            var totalTime = PhysicalTotalTime;
            var model = new PlotModel
            {
                Title = $"UAV Velocity Components vs Time (v = {PhysicalSpeed:F2} m/s)",
                Background = OxyColors.White
            };

            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Time (s)" });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Velocity (m/s)" });

            var vxSeries = new LineSeries { Title = "Vx(t)", Color = OxyColors.Blue, StrokeThickness = 2 };
            var vySeries = new LineSeries { Title = "Vy(t)", Color = OxyColors.Red, StrokeThickness = 2 };
            var normSeries = new LineSeries { Title = "|v(t)|", Color = OxyColors.DarkGreen, StrokeThickness = 2, LineStyle = LineStyle.Dash };

            for (var i = 0; i <= sampleCount; i++)
            {
                var t = i / (double)sampleCount * totalTime;
                var vel = GetPhysicalVelocityAtTime(t);
                var norm = Math.Sqrt(vel.X * vel.X + vel.Y * vel.Y);

                vxSeries.Points.Add(new DataPoint(t, vel.X));
                vySeries.Points.Add(new DataPoint(t, vel.Y));
                normSeries.Points.Add(new DataPoint(t, norm));
            }

            model.Series.Add(vxSeries);
            model.Series.Add(vySeries);
            model.Series.Add(normSeries);

            return RenderPlotToImage(model);
        }

        /// <summary>
        /// Plot physical velocity components vs physical distance.
        /// </summary>
        public Image<Rgba32> PlotPhysicalVelocityVsDistance(int sampleCount = 2049)
        {
            if (sampleCount < 2)
                throw new ArgumentOutOfRangeException(nameof(sampleCount));

            var totalDistance = PhysicalTotalLength;
            var model = new PlotModel
            {
                Title = "Physical Velocity Components vs Distance",
                Background = OxyColors.White
            };

            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Distance (m)" });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Velocity (m/s)" });

            var vxSeries = new LineSeries { Title = "Vx(s)", Color = OxyColors.Blue, StrokeThickness = 2 };
            var vySeries = new LineSeries { Title = "Vy(s)", Color = OxyColors.Red, StrokeThickness = 2 };
            var normSeries = new LineSeries { Title = "|v(s)|", Color = OxyColors.DarkGreen, LineStyle = LineStyle.Dash, StrokeThickness = 2 };

            for (var i = 0; i <= sampleCount; i++)
            {
                var d = i / (double)sampleCount * totalDistance;
                var geo = GetPhysicalGeometryAtPhysicalDistance(d);
                var vel = GetPhysicalVelocityAtTime(GetPhysicalTimeAtDistance(d));
                var norm = Math.Sqrt(vel.X * vel.X + vel.Y * vel.Y);

                vxSeries.Points.Add(new DataPoint(d, vel.X));
                vySeries.Points.Add(new DataPoint(d, vel.Y));
                normSeries.Points.Add(new DataPoint(d, norm));
            }

            model.Series.Add(vxSeries);
            model.Series.Add(vySeries);
            model.Series.Add(normSeries);

            return RenderPlotToImage(model);
        }


        public Image<Rgba32> PlotPhysicalAccelerationVsTime(int sampleCount = 2049)
        {
            if (sampleCount < 2) throw new ArgumentOutOfRangeException(nameof(sampleCount));

            var totalTime = PhysicalTotalTime;
            var model = new PlotModel
            {
                Title = $"UAV Acceleration Components vs Time (v = {PhysicalSpeed:F2} m/s)",
                Background = OxyColors.White
            };

            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Time (s)" });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Acceleration (m/s²)" });

            var axSeries = new LineSeries { Title = "Ax(t)", Color = OxyColors.Blue, StrokeThickness = 2 };
            var aySeries = new LineSeries { Title = "Ay(t)", Color = OxyColors.Red, StrokeThickness = 2 };
            var normSeries = new LineSeries { Title = "|a(t)|", Color = OxyColors.DarkGreen, StrokeThickness = 2, LineStyle = LineStyle.Dash };

            for (var i = 0; i <= sampleCount; i++)
            {
                var t = i / (double)sampleCount * totalTime;
                var acc = GetPhysicalAccelerationAtTime(t);
                var norm = Math.Sqrt(acc.X * acc.X + acc.Y * acc.Y);

                axSeries.Points.Add(new DataPoint(t, acc.X));
                aySeries.Points.Add(new DataPoint(t, acc.Y));
                normSeries.Points.Add(new DataPoint(t, norm));
            }

            model.Series.Add(axSeries);
            model.Series.Add(aySeries);
            model.Series.Add(normSeries);

            return RenderPlotToImage(model);
        }

        /// <summary>
        /// Plot physical acceleration components vs physical distance.
        /// </summary>
        public Image<Rgba32> PlotPhysicalAccelerationVsDistance(int sampleCount = 2049)
        {
            if (sampleCount < 2)
                throw new ArgumentOutOfRangeException(nameof(sampleCount));

            var totalDistance = PhysicalTotalLength;
            var model = new PlotModel
            {
                Title = "Physical Acceleration Components vs Distance",
                Background = OxyColors.White
            };

            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Distance (m)" });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Acceleration (m/s²)" });

            var axSeries = new LineSeries { Title = "Ax(s)", Color = OxyColors.Blue, StrokeThickness = 2 };
            var aySeries = new LineSeries { Title = "Ay(s)", Color = OxyColors.Red, StrokeThickness = 2 };
            var normSeries = new LineSeries { Title = "|a(s)|", Color = OxyColors.DarkGreen, LineStyle = LineStyle.Dash, StrokeThickness = 2 };

            for (var i = 0; i <= sampleCount; i++)
            {
                var d = i / (double)sampleCount * totalDistance;
                var acc = GetPhysicalAccelerationAtDistance(d);
                var norm = Math.Sqrt(acc.X * acc.X + acc.Y * acc.Y);

                axSeries.Points.Add(new DataPoint(d, acc.X));
                aySeries.Points.Add(new DataPoint(d, acc.Y));
                normSeries.Points.Add(new DataPoint(d, norm));
            }

            model.Series.Add(axSeries);
            model.Series.Add(aySeries);
            model.Series.Add(normSeries);

            return RenderPlotToImage(model);
        }


        /// <summary>
        /// Plot the physical lateral (centripetal) acceleration magnitude [m/s²]
        /// versus physical time [s], using the GetPhysicalLateralAccelerationAtPhysicalTime()
        /// method for computation.
        /// </summary>
        public Image<Rgba32> PlotPhysicalLateralAccelerationVsTime(int sampleCount = 2049)
        {
            if (sampleCount < 2)
                throw new ArgumentOutOfRangeException(nameof(sampleCount));

            // Determine total physical time (already set when PhysicalSpeed and PhysicalLengthScale are defined)
            var totalTime = PhysicalTotalTime;

            var model = new PlotModel
            {
                Title = $"Physical Lateral Acceleration vs Time (v = {PhysicalSpeed:F2} m/s)",
                Background = OxyColors.White
            };

            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Time (s)"
            });

            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Lateral Acceleration (m/s²)"
            });

            var series = new LineSeries
            {
                Title = "a_lat(t) = v²·κ(t)",
                Color = OxyColors.Purple,
                StrokeThickness = 2
            };

            for (var i = 0; i <= sampleCount; i++)
            {
                var t = i / (double)sampleCount * totalTime;

                // Use the dedicated method for consistency and maintainability
                var aLat = GetPhysicalLateralAccelerationAtTime(t);

                series.Points.Add(new DataPoint(t, aLat));
            }

            model.Series.Add(series);

            return RenderPlotToImage(model);
        }

        public Image<Rgba32> PlotPhysicalLateralAccelerationVsDistance(int sampleCount = 2049)
        {
            if (sampleCount < 2)
                throw new ArgumentOutOfRangeException(nameof(sampleCount));

            var totalDistance = PhysicalTotalLength;
            var model = new PlotModel
            {
                Title = "Lateral (Centripetal) Acceleration vs Physical Distance",
                Background = OxyColors.White
            };

            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Distance (m)" });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Lateral Accel (m/s²)" });

            var series = new LineSeries { Color = OxyColors.Purple, StrokeThickness = 2 };

            for (var i = 0; i <= sampleCount; i++)
            {
                var d = i / (double)sampleCount * totalDistance;
                var aLat = GetPhysicalLateralAccelerationAtDistance(d);
                series.Points.Add(new DataPoint(d, aLat));
            }

            model.Series.Add(series);
            return RenderPlotToImage(model);
        }


        /// <summary>
        /// Plot physical curvature (1/m) vs physical distance.
        /// </summary>
        public Image<Rgba32> PlotPhysicalCurvatureVsDistance(int sampleCount = 2049)
        {
            if (sampleCount < 2) throw new ArgumentOutOfRangeException(nameof(sampleCount));

            var totalDistance = PhysicalTotalLength;
            var model = new PlotModel
            {
                Title = "Physical Curvature vs Distance",
                Background = OxyColors.White
            };

            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Distance (m)" });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Curvature (1/m)" });

            var series = new LineSeries { Title = "κ(s)", Color = OxyColors.Purple, StrokeThickness = 2 };

            for (var i = 0; i <= sampleCount; i++)
            {
                var d = i / (double)sampleCount * totalDistance;
                var kappa = GetPhysicalCurvatureAtDistance(d);
                series.Points.Add(new DataPoint(d, kappa));
            }

            model.Series.Add(series);
            return RenderPlotToImage(model);
        }

        /// <summary>
        /// Plot physical curvature (1/m) vs physical time.
        /// </summary>
        public Image<Rgba32> PlotPhysicalCurvatureVsTime(int sampleCount = 2049)
        {
            if (sampleCount < 2) throw new ArgumentOutOfRangeException(nameof(sampleCount));

            var totalTime = PhysicalTotalTime;
            var model = new PlotModel
            {
                Title = "Physical Curvature vs Time",
                Background = OxyColors.White
            };

            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Time (s)" });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Curvature (1/m)" });

            var series = new LineSeries { Title = "κ(t)", Color = OxyColors.Purple, StrokeThickness = 2 };

            for (var i = 0; i <= sampleCount; i++)
            {
                var t = i / (double)sampleCount * totalTime;
                var kappa = GetPhysicalCurvatureAtTime(t);
                series.Points.Add(new DataPoint(t, kappa));
            }

            model.Series.Add(series);
            return RenderPlotToImage(model);
        }


        /// <summary>
        /// Plot required bank angle (deg) vs physical time (s).
        /// </summary>
        public Image<Rgba32> PlotPhysicalBankAngleVsTime(int sampleCount = 2049)
        {
            if (sampleCount < 2) throw new ArgumentOutOfRangeException(nameof(sampleCount));

            var totalTime = PhysicalTotalTime;
            var model = new PlotModel
            {
                Title = "Required Bank Angle vs Time",
                Background = OxyColors.White
            };

            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Time (s)" });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Bank Angle (deg)" });

            var series = new LineSeries { Title = "φ(t)", Color = OxyColors.SteelBlue, StrokeThickness = 2 };

            for (var i = 0; i <= sampleCount; i++)
            {
                var t = i / (double)sampleCount * totalTime;
                var phiRad = GetPhysicalBankAngleAtTime(t);
                series.Points.Add(new DataPoint(t, phiRad * 180.0 / Math.PI));
            }

            model.Series.Add(series);
            return RenderPlotToImage(model);
        }

        /// <summary>
        /// Plot required bank angle (deg) vs physical distance (m).
        /// </summary>
        public Image<Rgba32> PlotPhysicalBankAngleVsDistance(int sampleCount = 2049)
        {
            if (sampleCount < 2) throw new ArgumentOutOfRangeException(nameof(sampleCount));

            var totalDistance = PhysicalTotalLength;
            var model = new PlotModel
            {
                Title = "Required Bank Angle vs Distance",
                Background = OxyColors.White
            };

            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Distance (m)" });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Bank Angle (deg)" });

            var series = new LineSeries { Title = "φ(s)", Color = OxyColors.SteelBlue, StrokeThickness = 2 };

            for (var i = 0; i <= sampleCount; i++)
            {
                var d = i / (double)sampleCount * totalDistance;
                var phiRad = GetPhysicalBankAngleAtDistance(d);
                series.Points.Add(new DataPoint(d, phiRad * 180.0 / Math.PI));
            }

            model.Series.Add(series);
            return RenderPlotToImage(model);
        }


        /// <summary>
        /// Plot turn radius (m) vs physical time (s).
        /// </summary>
        public Image<Rgba32> PlotPhysicalTurnRadiusVsTime(int sampleCount = 2049)
        {
            if (sampleCount < 2) throw new ArgumentOutOfRangeException(nameof(sampleCount));

            var totalTime = PhysicalTotalTime;
            var model = new PlotModel
            {
                Title = "Turn Radius vs Time",
                Background = OxyColors.White
            };

            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Time (s)" });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Turn Radius (m)" });

            var series = new LineSeries { Title = "R(t)", Color = OxyColors.DarkOrange, StrokeThickness = 2 };

            for (var i = 0; i <= sampleCount; i++)
            {
                var t = i / (double)sampleCount * totalTime;
                var R = GetPhysicalTurnRadiusAtTime(t);
                series.Points.Add(new DataPoint(t, R));
            }

            model.Series.Add(series);
            return RenderPlotToImage(model);
        }

        /// <summary>
        /// Plot turn radius (m) vs physical distance (m).
        /// </summary>
        public Image<Rgba32> PlotPhysicalTurnRadiusVsDistance(int sampleCount = 2049)
        {
            if (sampleCount < 2) throw new ArgumentOutOfRangeException(nameof(sampleCount));

            var totalDistance = PhysicalTotalLength;
            var model = new PlotModel
            {
                Title = "Turn Radius vs Distance",
                Background = OxyColors.White
            };

            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Distance (m)" });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Turn Radius (m)" });

            var series = new LineSeries { Title = "R(s)", Color = OxyColors.DarkOrange, StrokeThickness = 2 };

            for (var i = 0; i <= sampleCount; i++)
            {
                var d = i / (double)sampleCount * totalDistance;
                var R = GetPhysicalTurnRadiusAtDistance(d);
                series.Points.Add(new DataPoint(d, R));
            }

            model.Series.Add(series);
            return RenderPlotToImage(model);
        }


        /// <summary>
        /// Plot curvature (1/m), lateral acceleration (m/s²), and bank angle (°) versus physical time (s).
        /// </summary>
        public Image<Rgba32> PlotPhysicalFlightEnvelopeVsTime(int sampleCount = 2049)
        {
            if (sampleCount < 2)
                throw new ArgumentOutOfRangeException(nameof(sampleCount));

            var totalTime = PhysicalTotalTime;
            const double g = 9.80665;

            var model = new PlotModel
            {
                Title = "UAV Flight Envelope vs Time",
                Background = OxyColors.White
            };

            // --- Axes ---
            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Time (s)",
                Key = "TimeAxis"
            });

            // Curvature axis (left)
            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Curvature (1/m)",
                Key = "CurvatureAxis",
                AxislineColor = OxyColors.Purple,
                TextColor = OxyColors.Purple,
                TitleColor = OxyColors.Purple
            });

            // Lateral acceleration axis (right)
            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Right,
                Title = "Lateral Accel (m/s²)",
                Key = "AccelAxis",
                AxislineColor = OxyColors.Red,
                TextColor = OxyColors.Red,
                TitleColor = OxyColors.Red
            });

            // Secondary right axis for bank angle
            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Right,
                StartPosition = 0,
                EndPosition = 0.5,
                Title = "Bank Angle (°)",
                Key = "BankAxis",
                AxislineColor = OxyColors.Blue,
                TextColor = OxyColors.Blue,
                TitleColor = OxyColors.Blue
            });

            // --- Data series ---
            var curvatureSeries = new LineSeries
            {
                Title = "κ(t)",
                Color = OxyColors.Purple,
                StrokeThickness = 2,
                YAxisKey = "CurvatureAxis"
            };

            var accelSeries = new LineSeries
            {
                Title = "a_lat(t)",
                Color = OxyColors.Red,
                StrokeThickness = 2,
                YAxisKey = "AccelAxis"
            };

            var bankSeries = new LineSeries
            {
                Title = "φ(t)",
                Color = OxyColors.Blue,
                StrokeThickness = 2,
                LineStyle = LineStyle.Dash,
                YAxisKey = "BankAxis"
            };

            for (var i = 0; i <= sampleCount; i++)
            {
                var t = i / (double)sampleCount * totalTime;

                var kappa = GetPhysicalCurvatureAtTime(t);
                var aLat = PhysicalSpeed * PhysicalSpeed * kappa;
                var phi = Math.Atan(aLat / g) * 180.0 / Math.PI;

                curvatureSeries.Points.Add(new DataPoint(t, kappa));
                accelSeries.Points.Add(new DataPoint(t, aLat));
                bankSeries.Points.Add(new DataPoint(t, phi));
            }

            model.Series.Add(curvatureSeries);
            model.Series.Add(accelSeries);
            model.Series.Add(bankSeries);

            return RenderPlotToImage(model);
        }

        /// <summary>
        /// Plot curvature (1/m), lateral acceleration (m/s²), and bank angle (°) versus physical distance (m).
        /// </summary>
        public Image<Rgba32> PlotPhysicalFlightEnvelopeVsDistance(int sampleCount = 2049)
        {
            if (sampleCount < 2)
                throw new ArgumentOutOfRangeException(nameof(sampleCount));

            var totalDistance = PhysicalTotalLength;
            const double g = 9.80665;

            var model = new PlotModel
            {
                Title = "UAV Flight Envelope vs Distance",
                Background = OxyColors.White
            };

            // --- Axes ---
            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Distance (m)",
                Key = "DistanceAxis"
            });

            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Curvature (1/m)",
                Key = "CurvatureAxis",
                AxislineColor = OxyColors.Purple,
                TextColor = OxyColors.Purple,
                TitleColor = OxyColors.Purple
            });

            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Right,
                Title = "Lateral Accel (m/s²)",
                Key = "AccelAxis",
                AxislineColor = OxyColors.Red,
                TextColor = OxyColors.Red,
                TitleColor = OxyColors.Red
            });

            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Right,
                StartPosition = 0,
                EndPosition = 0.5,
                Title = "Bank Angle (°)",
                Key = "BankAxis",
                AxislineColor = OxyColors.Blue,
                TextColor = OxyColors.Blue,
                TitleColor = OxyColors.Blue
            });

            // --- Data series ---
            var curvatureSeries = new LineSeries
            {
                Title = "κ(s)",
                Color = OxyColors.Purple,
                StrokeThickness = 2,
                YAxisKey = "CurvatureAxis"
            };

            var accelSeries = new LineSeries
            {
                Title = "a_lat(s)",
                Color = OxyColors.Red,
                StrokeThickness = 2,
                YAxisKey = "AccelAxis"
            };

            var bankSeries = new LineSeries
            {
                Title = "φ(s)",
                Color = OxyColors.Blue,
                StrokeThickness = 2,
                LineStyle = LineStyle.Dash,
                YAxisKey = "BankAxis"
            };

            for (var i = 0; i <= sampleCount; i++)
            {
                var d = i / (double)sampleCount * totalDistance;

                var kappa = GetPhysicalCurvatureAtDistance(d);
                var aLat = PhysicalSpeed * PhysicalSpeed * kappa;
                var phi = Math.Atan(aLat / g) * 180.0 / Math.PI;

                curvatureSeries.Points.Add(new DataPoint(d, kappa));
                accelSeries.Points.Add(new DataPoint(d, aLat));
                bankSeries.Points.Add(new DataPoint(d, phi));
            }

            model.Series.Add(curvatureSeries);
            model.Series.Add(accelSeries);
            model.Series.Add(bankSeries);

            return RenderPlotToImage(model);
        }


        public void PlotPhysicalMetricsVsTime(string filePath, int sampleCount = 2049)
        {
            var encoder = new PngEncoder()
            {
                CompressionLevel = PngCompressionLevel.NoCompression,
                TransparentColorMode = PngTransparentColorMode.Preserve,
                ColorType = PngColorType.RgbWithAlpha
            };

            PlotPhysicalPositionVsTime(sampleCount).SaveAsPng(
                Path.Combine(filePath, "Physical Position vs time.png"),
                encoder
            );

            PlotPhysicalVelocityVsTime(sampleCount).SaveAsPng(
                Path.Combine(filePath, "Physical Velocity vs time.png"),
                encoder
            );

            PlotPhysicalAccelerationVsTime(sampleCount).SaveAsPng(
                Path.Combine(filePath, "Physical Acceleration vs time.png"),
                encoder
            );

            PlotPhysicalLateralAccelerationVsTime(sampleCount).SaveAsPng(
                Path.Combine(filePath, "Physical Lateral Acceleration vs time.png"),
                encoder
            );

            PlotPhysicalCurvatureVsTime(sampleCount).SaveAsPng(
                Path.Combine(filePath, "Physical Curvature vs time.png"),
                encoder
            );

            PlotPhysicalFlightEnvelopeVsTime(sampleCount).SaveAsPng(
                Path.Combine(filePath, "Physical Flight Envelope vs time.png"),
                encoder
            );
        }

        public void PlotPhysicalMetricsVsDistance(string filePath, int sampleCount = 2049)
        {
            var encoder = new PngEncoder()
            {
                CompressionLevel = PngCompressionLevel.NoCompression,
                TransparentColorMode = PngTransparentColorMode.Preserve,
                ColorType = PngColorType.RgbWithAlpha
            };

            PlotPhysicalPositionVsDistance(sampleCount).SaveAsPng(
                Path.Combine(filePath, "Physical Position vs distance.png"),
                encoder
            );

            PlotPhysicalVelocityVsDistance(sampleCount).SaveAsPng(
                Path.Combine(filePath, "Physical Velocity vs distance.png"),
                encoder
            );

            PlotPhysicalAccelerationVsDistance(sampleCount).SaveAsPng(
                Path.Combine(filePath, "Physical Acceleration vs distance.png"),
                encoder
            );

            PlotPhysicalLateralAccelerationVsDistance(sampleCount).SaveAsPng(
                Path.Combine(filePath, "Physical Lateral Acceleration vs distance.png"),
                encoder
            );

            PlotPhysicalCurvatureVsDistance(sampleCount).SaveAsPng(
                Path.Combine(filePath, "Physical Curvature vs distance.png"),
                encoder
            );

            PlotPhysicalFlightEnvelopeVsDistance(sampleCount).SaveAsPng(
                Path.Combine(filePath, "Physical Flight Envelope vs distance.png"),
                encoder
            );
        }

        /// <summary>
        /// One row in the flight-envelope table (for a given curvature κ).
        /// </summary>
        public sealed class FlightEnvelopeRow
        {
            public double Curvature { get; set; }            // 1/m

            public double TurnRadius { get; set; }           // m (1/|κ|)

            public double LateralAcceleration { get; set; }  // m/s^2 (v^2 * κ)

            public double BankAngleDeg { get; set; }         // degrees (atan(a_lat / g))

            public double LoadFactor { get; set; }           // g (dimensionless)

            public double YawRate { get; set; }              // rad/s (v * κ)

            public double TurnPeriod { get; set; }           // s (2π / yawRate) or +Inf if yawRate==0
        }

        /// <summary>
        /// Generate a flight-envelope table for the provided speed and g-limit.
        /// Returns: (rows, recommendedMaxCurvature, minTurnRadius)
        /// - rows: one FlightEnvelopeRow per curvature value
        /// - recommendedMaxCurvature: curvature limit (1/m) derived from maxLateralG
        /// - minTurnRadius: corresponding minimum turn radius (m)
        /// </summary>
        public (List<FlightEnvelopeRow> Rows, double RecommendedMaxCurvature, double MinTurnRadius) GenerateFlightEnvelopeTable(double speed, double maxLateralG, IEnumerable<double>? curvatureValues = null)
        {
            if (!double.IsFinite(speed) || speed <= 0)
                throw new ArgumentOutOfRangeException(nameof(speed));

            if (!double.IsFinite(maxLateralG) || maxLateralG <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxLateralG));

            // Default representative curvature values (1/m) if none provided
            var defaultCurvatures = new double[] { 0.005, 0.01, 0.02, 0.03, 0.04, 0.05 };

            var curvList = (curvatureValues == null) ? defaultCurvatures : curvatureValues.ToArray();

            const double g = 9.80665; // m/s^2
            var aMax = maxLateralG * g;

            // Recommended maximum curvature so that v^2 * kappa <= aMax --> kappa <= aMax / v^2
            var recommendedMaxCurvature = aMax / (speed * speed);
            var minTurnRadius = recommendedMaxCurvature > 0.0 ? 1.0 / recommendedMaxCurvature : double.PositiveInfinity;

            var rows = new List<FlightEnvelopeRow>(curvList.Length);

            foreach (var k in curvList)
            {
                var absK = Math.Abs(k);
                var row = new FlightEnvelopeRow
                {
                    Curvature = k,
                    TurnRadius = absK > 0 ? 1.0 / absK : double.PositiveInfinity,
                    LateralAcceleration = speed * speed * k,
                    BankAngleDeg = Math.Atan((speed * speed * k) / g) * 180.0 / Math.PI,
                    LoadFactor = Math.Sqrt(1.0 + Math.Pow((speed * speed * k) / g, 2.0)),
                    YawRate = speed * k,
                    TurnPeriod = Math.Abs(speed * k) > 0.0 ? 2.0 * Math.PI / Math.Abs(speed * k) : double.PositiveInfinity
                };

                rows.Add(row);
            }

            return (rows, recommendedMaxCurvature, minTurnRadius);
        }

        public IEnumerator<PhBernsteinCurve2D> GetEnumerator()
            => _curveList.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}


//using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
//using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
//using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
//using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
//using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Samplers;
//using System.Collections;
//using OpenQA.Selenium;

//namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Bezier;

///// <summary>
///// Represents a multi-segment PH curve
///// </summary>
//public class PhBernsteinPolyCurve2D :
//    Float64Path2D,
//    IReadOnlyList<PhBernsteinCurve2D>
//{
//    public static PhBernsteinPolyCurve2D FromPolyline(Float64Polyline2D polyline, Random? randGen = null)
//    {
//        randGen ??= new Random(10);

//        var cornerCurveList = new List<PhBernsteinCurve2D>(polyline.SegmentCount - 1);

//        for (var segmentIndex = 0; segmentIndex < polyline.SegmentCount - 1; segmentIndex++)
//        {
//            var s1 = polyline.GetSegment(segmentIndex);
//            var s2 = polyline.GetSegment(segmentIndex + 1);

//            var p1 = 0.75.Lerp(s1.Point1, s1.Point2);
//            var p2 = s1.Point2;
//            var p3 = 0.25.Lerp(s2.Point1, s2.Point2);

//            var (foundCurve, phCurve) =
//                PhBernsteinCurve2D.TryJoinLinesC2(
//                    randGen, 5, p1, p2, p3
//                );

//            if (!foundCurve)
//                throw new NotFoundException();

//            cornerCurveList.Add(phCurve);
//        }

//        var curveList = new List<PhBernsteinCurve2D>(2 * polyline.Count);

//        var cornerCurve1 = cornerCurveList[0];

//        {
//            var (foundCurve, phCurve) = 
//                PhBernsteinCurve2D.TryCreateC2(
//                    randGen, 
//                    3,
//                    polyline.GetVertexPoint(0),
//                    cornerCurve1.GetPoint(0),
//                    polyline.GetVertexTangentVector(0), 
//                    cornerCurve1.GetTangentVector(0),
//                    LinFloat64Vector2D.Zero, 
//                    cornerCurve1.GetAccelerationVector(0)
//                );

//            if (!foundCurve)
//                throw new NotFoundException();

//            curveList.Add(phCurve);
//            curveList.Add(cornerCurve1);
//        }

//        for (var i = 1; i < cornerCurveList.Count; i++)
//        {
//            var cornerCurve2 = cornerCurveList[i];

//            var (foundCurve, phCurve) = 
//                PhBernsteinCurve2D.TryCreateC2(
//                    randGen, 
//                    3,
//                    cornerCurve1.GetPoint(1),
//                    cornerCurve2.GetPoint(0),
//                    cornerCurve1.GetTangentVector(1),
//                    cornerCurve2.GetTangentVector(0),
//                    cornerCurve1.GetAccelerationVector(1),
//                    cornerCurve2.GetAccelerationVector(0)
//                );

//            if (!foundCurve)
//                throw new NotFoundException();

//            curveList.Add(phCurve);
//            curveList.Add(cornerCurve2);

//            cornerCurve1 = cornerCurve2;
//        }

//        {
//            var (foundCurve, phCurve) = 
//                PhBernsteinCurve2D.TryCreateC2(
//                    randGen, 
//                    3,
//                    cornerCurve1.GetPoint(1),
//                    polyline.GetVertexPoint(polyline.Count - 1),
//                    cornerCurve1.GetTangentVector(1),
//                    polyline.GetVertexTangentVector(polyline.Count - 1), 
//                    cornerCurve1.GetAccelerationVector(1),
//                    LinFloat64Vector2D.Zero
//                );

//            if (!foundCurve)
//                throw new NotFoundException();

//            curveList.Add(phCurve);
//        }

//        return new PhBernsteinPolyCurve2D(curveList, false);
//    }


//    private readonly IReadOnlyList<PhBernsteinCurve2D> _curveList;
//    private readonly double[] _segmentLengthArray;
//    private readonly double[] _cumLengthArray;


//    public double Length { get; }

//    public double Energy { get; }

//    public double MaxCurvature { get; }

//    public double PointError { get; }

//    public double TangentError { get; }

//    public double AccelerationError { get; }

//    public double AccelerationDirectionError { get; }

//    public int Count
//        => _curveList.Count;

//    public PhBernsteinCurve2D this[int index]
//        => _curveList[index];


//    public PhBernsteinPolyCurve2D(IReadOnlyList<PhBernsteinCurve2D> curveList, bool isPeriodic)
//        : base(
//            Float64ScalarRange.Create(0, curveList.Sum(c => c.GetArcLength(1))), 
//            isPeriodic
//        )
//    {
//        _curveList = curveList;

//        _segmentLengthArray = new double[curveList.Count];
//        _cumLengthArray = new double[curveList.Count];
//        _cumLengthArray[0] = 0;

//        var length = 0d;
//        var energy = 0d;
//        var maxCurvature = 0d;

//        for (var i = 0; i < curveList.Count; i++)
//        {
//            var curve = curveList[i];
//            var segmentLength = curve.GetArcLength(1);
//            length += segmentLength;
//            //energy += curve.GetMaxEnergy();
//            maxCurvature = Math.Max(maxCurvature, curve.GetMaxCurvature());

//            _segmentLengthArray[i] = segmentLength;
//            _cumLengthArray[i] = length;
//        }

//        Length = length;
//        Energy = energy;
//        MaxCurvature = maxCurvature;

//        var pointError = 0d;
//        var tangentError = 0d;
//        var accelerationError = 0d;
//        var accelerationDirectionError = 0d;

//        var c1 = curveList[0];
//        for (var i = 1; i < curveList.Count; i++)
//        {
//            var c2 = curveList[i];

//            pointError += c1.GetPoint(1).GetDistanceToPoint(c2.GetPoint(0));
//            tangentError += c1.GetTangentVector(1).GetDistanceToPoint(c2.GetTangentVector(0));
//            accelerationError += c1.GetAccelerationVector(1).GetDistanceToPoint(c2.GetAccelerationVector(0));
//            accelerationDirectionError += 1 - c1.GetAccelerationVector(1).GetAngleCos(c1.GetAccelerationVector(0));

//            c1 = c2;
//        }

//        PointError = pointError;
//        TangentError = tangentError;
//        AccelerationError = accelerationError;
//        AccelerationDirectionError = accelerationDirectionError;
//    }


//    public override Float64Path2D ToFinitePath()
//    {
//        return IsFinite
//            ? this
//            : new PhBernsteinPolyCurve2D(_curveList, false);
//    }

//    public override Float64Path2D ToPeriodicPath()
//    {
//        return IsPeriodic
//            ? this
//            : new PhBernsteinPolyCurve2D(_curveList, true);
//    }

//    public override bool IsValid()
//    {
//        return true;
//    }

//    /// <summary>
//    /// Find segment index where distance in [s_i, s_{i+1})
//    /// </summary>
//    /// <param name="distance"></param>
//    /// <returns></returns>
//    private int FindSegmentIndex(double distance)
//    {
//        // binary search on _cumLengths
//        var low = 0;
//        var high = _cumLengthArray.Length - 2;
//        while (low <= high)
//        {
//            var mid = (low + high) >> 1;

//            if (distance < _cumLengthArray[mid])
//                high = mid - 1;
//            else if (distance >= _cumLengthArray[mid + 1])
//                low = mid + 1;
//            else
//                return mid;
//        }

//        return Math.Max(0, Math.Min(_cumLengthArray.Length - 2, low));
//    }

//    /// <summary>
//    /// Clamp s and return segment and local u
//    /// </summary>
//    /// <param name="distance"></param>
//    /// <param name="u"></param>
//    /// <returns></returns>
//    private int ClampSegmentIndexForS(double distance, out double u)
//    {
//        if (distance <= 0)
//        {
//            u = 0;
//            return 0;
//        }

//        if (distance >= Length)
//        {
//            u = 1;
//            return _cumLengthArray.Length - 2;
//        }

//        var seg = FindSegmentIndex(distance);
//        var s0 = _cumLengthArray[seg];
//        var length = _segmentLengthArray[seg];

//        u = length > 0 ? (distance - s0) / length : 0f;
//        return seg;
//    }

//    public override LinFloat64Vector2D GetValue(double distance)
//    {
//        if (_curveList.Count == 0)
//            throw new InvalidOperationException("No segments.");

//        var segmentIndex = FindSegmentIndex(distance);
//        var segment = _curveList[segmentIndex];
//        var length = _segmentLengthArray[segmentIndex];
//        var u = length > 0 ? distance / length : 0f;

//        return segment.GetValue(segment.LengthToTime(u));
//    }

//    public override LinFloat64Vector2D GetDerivative1Value(double distance)
//    {
//        if (_curveList.Count == 0)
//            throw new InvalidOperationException("No segments.");

//        var segmentIndex = FindSegmentIndex(distance);
//        var segment = _curveList[segmentIndex];
//        var length = _segmentLengthArray[segmentIndex];
//        var u = length > 0 ? distance / length : 0f;

//        return segment.GetDerivative1Value(segment.LengthToTime(u));
//    }

//    public override LinFloat64Vector2D GetDerivative2Value(double distance)
//    {
//        if (_curveList.Count == 0)
//            throw new InvalidOperationException("No segments.");

//        var segmentIndex = FindSegmentIndex(distance);
//        var segment = _curveList[segmentIndex];
//        var length = _segmentLengthArray[segmentIndex];
//        var u = length > 0 ? distance / length : 0f;

//        return segment.GetDerivative2Value(segment.LengthToTime(u));
//    }

//    public override Float64Scalar GetArcLength(double a, double b)
//    {
//        return base.GetArcLength(a, b);
//    }

//    public double GetArcLength()
//    {
//        return Length;
//    }

//    public double GetMinObstacleDistance(IReadOnlyList<Float64LineSegment2D> obstacleList)
//    {
//        var minObstacleDistance = double.PositiveInfinity;

//        foreach (var curve in _curveList)
//        {
//            minObstacleDistance = Math.Min(
//                minObstacleDistance,
//                curve.ToAdaptivePolyline().GetShortestDistanceTo(obstacleList)
//            );
//        }

//        return minObstacleDistance;
//    }

//    /// <summary>
//    /// Sample points evenly across all segments.
//    /// </summary>
//    public List<LinFloat64Vector2D> SamplePoints(SampledPhBernsteinCurve2D.SamplingOptions samplingOptions)
//    {
//        var pts = new List<LinFloat64Vector2D>();

//        foreach (var seg in _curveList)
//        {
//            var sampler = new SampledPhBernsteinCurve2D(false, samplingOptions);

//            sampler.Generate(seg);

//            pts.AddRange(
//                sampler.Points.Select(p =>
//                    LinFloat64Vector2D.Create(p.X, p.Y)
//                )
//            );
//        }

//        return pts;
//    }

//    public Float64Polyline2D ToAdaptivePolyline(PhBernsteinCurve2D.SamplingOptions? samplingOptions = null)
//    {
//        samplingOptions ??= new PhBernsteinCurve2D.SamplingOptions();

//        return _curveList.SelectMany(c => 
//            c.ToAdaptivePolyline(samplingOptions)
//        ).ToPolyline().RemoveShortSegments(1e-5);
//    }


//    public IEnumerator<PhBernsteinCurve2D> GetEnumerator()
//    {
//        return _curveList.GetEnumerator();
//    }

//    IEnumerator IEnumerable.GetEnumerator()
//    {
//        return GetEnumerator();
//    }

//}