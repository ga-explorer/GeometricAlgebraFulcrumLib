using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Tuples;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.BSpline
{
    /// <summary>
    /// Knot vector class for B-splines. Provides efficient index access and span lookup.
    /// </summary>
    public sealed class BSplineKnotVector :
        IReadOnlyList<double>
    {
        /// <summary>
        /// Create an empty knot vector to be filled later.
        /// </summary>
        public static BSplineKnotVector Create() => new();

        /// <summary>
        /// Create uniform knot values spanning [t0, t1] with multiplicities as provided.
        /// </summary>
        public static BSplineKnotVector Create(double t0, double t1, int knotCount, int outerMultiplicity, int innerMultiplicity)
        {
            if (knotCount < 2) throw new ArgumentOutOfRangeException(nameof(knotCount));
            if (outerMultiplicity < 1) throw new ArgumentOutOfRangeException(nameof(outerMultiplicity));
            if (innerMultiplicity < 1) throw new ArgumentOutOfRangeException(nameof(innerMultiplicity));

            var kv = new BSplineKnotVector();
            var tList = t0.GetLinearRange(t1, knotCount).ToArray();

            for (var i = 0; i < tList.Length; i++)
            {
                var multiplicity = (i == 0 || i == tList.Length - 1) ? outerMultiplicity : innerMultiplicity;
                kv.AppendKnot(tList[i], multiplicity);
            }

            return kv;
        }

        /// <summary>
        /// Create uniform knot values spanning [t0, t1] with simple multiplicities.
        /// </summary>
        public static BSplineKnotVector CreateUniform(double t0, double t1, int knotCount)
        {
            if (knotCount < 2) throw new ArgumentOutOfRangeException(nameof(knotCount));

            var kv = new BSplineKnotVector();
            var tList = t0.GetLinearRange(t1, knotCount).ToArray();

            foreach (var t in tList) kv.AppendKnot(t);
            return kv;
        }

        /// <summary>
        /// Create a simple clamped knot vector: multiplicity degree+1 at ends, inner multiplicity 1.
        /// </summary>
        public static BSplineKnotVector CreateSimpleClamped(double t0, double t1, int knotCount, int degree)
        {
            if (knotCount < 2) throw new ArgumentOutOfRangeException(nameof(knotCount));
            if (degree < 0) throw new ArgumentOutOfRangeException(nameof(degree));

            var kv = new BSplineKnotVector();
            kv.AppendKnot(t0, degree + 1);

            var tList = t0.GetLinearRange(t1, knotCount).ToArray();
            for (var i = 1; i < tList.Length - 1; i++) kv.AppendKnot(tList[i]);

            kv.AppendKnot(t1, degree + 1);
            return kv;
        }

        /// <summary>
        /// Create a knot vector suitable for representing products of B-spline basis functions.
        /// </summary>
        public static BSplineKnotVector CreateProductKnotVector(BSplineKnotVector original, int targetDegree)
        {
            if (original == null) throw new ArgumentNullException(nameof(original));
            if (targetDegree < 0) throw new ArgumentOutOfRangeException(nameof(targetDegree));

            var result = Create();

            foreach (var knot in original._knotList)
            {
                // For product space, we need higher multiplicity to maintain continuity
                var newMultiplicity = Math.Min(knot.Multiplicity * 2, targetDegree + 1);
                result.AppendKnot(knot.Value, newMultiplicity);
            }

            return result;
        }

        /// <summary>
        /// Create a refined knot vector by inserting additional knots to improve numerical stability.
        /// </summary>
        public static BSplineKnotVector CreateRefinedKnotVector(BSplineKnotVector original, int refinementLevel = 1)
        {
            if (original == null) throw new ArgumentNullException(nameof(original));
            if (refinementLevel < 0) throw new ArgumentOutOfRangeException(nameof(refinementLevel));

            var result = Create();
            var distinctKnots = original.GetDistinctKnotValues();

            for (var i = 0; i < distinctKnots.Length - 1; i++)
            {
                var currentKnot = distinctKnots[i];
                var nextKnot = distinctKnots[i + 1];
                var multiplicity = original.GetMultiplicity(currentKnot);

                result.AppendKnot(currentKnot, multiplicity);

                // Insert refined knots
                for (var j = 1; j <= refinementLevel; j++)
                {
                    var refinedKnot = currentKnot + (nextKnot - currentKnot) * j / (refinementLevel + 1);
                    result.AppendKnot(refinedKnot, 1);
                }
            }

            var lastKnot = distinctKnots[^1];
            result.AppendKnot(lastKnot, original.GetMultiplicity(lastKnot));

            return result;
        }

        // internal representation
        private readonly List<BSplineKnot> _knotList = new();
        private readonly List<double> _valuesFlat = new(); // repeated knot values
        private double[]? _cachedDistinctKnots;

        private BSplineKnotVector() { }

        /// <summary>
        /// Number of *values* in the vector (including repeats).
        /// </summary>
        public int Count => _valuesFlat.Count;

        /// <summary>
        /// Number of distinct knots.
        /// </summary>
        public int KnotCount => _knotList.Count;

        /// <summary>
        /// Enumerates distinct knot objects (value + multiplicity).
        /// </summary>
        public IEnumerable<BSplineKnot> Knots => _knotList;

        /// <summary>
        /// Maximum allowed polynomial degree for this knot vector.
        /// Explicit: (Count - 1) / 2
        /// </summary>
        public int MaxDegree => (Count - 1) / 2;

        /// <summary>
        /// Return the knot value for the given (flat) index.
        /// O(1) with flat cache.
        /// </summary>
        public double this[int index]
        {
            get
            {
                if (index < 0 || index >= _valuesFlat.Count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                return _valuesFlat[index];
            }
        }

        /// <summary>Multiplicity of first knot.</summary>
        public int FirstMultiplicity => _knotList.Count == 0 ? 0 : _knotList[0].Multiplicity;

        /// <summary>Value of first knot.</summary>
        public double FirstValue => _knotList.Count == 0 ? double.NaN : _knotList[0].Value;

        /// <summary>Multiplicity of last knot.</summary>
        public int LastMultiplicity => _knotList.Count == 0 ? 0 : _knotList[^1].Multiplicity;

        /// <summary>Value of last knot.</summary>
        public double LastValue => _knotList.Count == 0 ? double.NaN : _knotList[^1].Value;

        /// <summary>
        /// Get the multiplicity of a specific knot value.
        /// </summary>
        public int GetMultiplicity(double knotValue)
        {
            var knot = _knotList.FirstOrDefault(k => Math.Abs(k.Value - knotValue) < 1e-14);
            return knot?.Multiplicity ?? 0;
        }

        /// <summary>
        /// Get distinct knot values in ascending order (cached for performance).
        /// </summary>
        public double[] GetDistinctKnotValues()
        {
            return _cachedDistinctKnots ??= _knotList.Select(k => k.Value).Distinct().OrderBy(v => v).ToArray();
        }

        /// <summary>
        /// Check if the knot vector is valid for the given degree.
        /// </summary>
        public bool IsValidForDegree(int degree)
        {
            if (degree < 0) return false;
            if (Count < 2 * degree + 2) return false;
            if (FirstMultiplicity < degree + 1 || LastMultiplicity < degree + 1) return false;

            // Check non-decreasing property
            for (var i = 1; i < _knotList.Count; i++)
            {
                if (_knotList[i].Value < _knotList[i - 1].Value)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Number of basis functions for a given degree.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetBasisCount(int degree)
        {
            if (degree < 0 || degree > MaxDegree)
                throw new ArgumentOutOfRangeException(nameof(degree));

            return Count - degree - 1;
        }

        /// <summary>Get distinct knot by index (distinct index, not flat index).</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BSplineKnot GetKnot(int knotIndex)
        {
            if (knotIndex < 0 || knotIndex >= _knotList.Count)
                throw new ArgumentOutOfRangeException(nameof(knotIndex));
            return _knotList[knotIndex];
        }

        /// <summary>Set distinct knot at index.</summary>
        public void SetKnot(int knotIndex, BSplineKnot knot)
        {
            if (knotIndex < 0 || knotIndex >= _knotList.Count)
                throw new ArgumentOutOfRangeException(nameof(knotIndex));
            if (knot is null) throw new ArgumentNullException(nameof(knot));

            _knotList[knotIndex] = knot;
            RebuildFlatValues();
        }

        /// <summary>Get sequence of (value, multiplicity) for distinct knots.</summary>
        public IEnumerable<Tuple<double, int>> GetKnotValueMultiplicityList()
            => _knotList.Select(k => new Tuple<double, int>(k.Value, k.Multiplicity));

        /// <summary>
        /// Append a knot value (with multiplicity). If value equals last, multiplicities are merged.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BSplineKnotVector AppendKnot(double value, int multiplicity = 1)
        {
            if (multiplicity < 1) throw new ArgumentOutOfRangeException(nameof(multiplicity));
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Invalid knot value.", nameof(value));

            if (_knotList.Count > 0)
            {
                var lastValue = LastValue;
                if (value < lastValue - 1e-14)
                    throw new InvalidOperationException("Knots must be non-decreasing.");

                if (Math.Abs(value - lastValue) < 1e-14)
                {
                    var lastK = _knotList[^1];
                    var newK = new BSplineKnot(lastK.Index1, value, lastK.Multiplicity + multiplicity);
                    _knotList[^1] = newK;
                    // append multiplicity times to flat array
                    for (var i = 0; i < multiplicity; i++) _valuesFlat.Add(value);

                    _cachedDistinctKnots = null; // Invalidate cache
                    return this;
                }
            }

            var newKnot = new BSplineKnot(Count, value, multiplicity);
            _knotList.Add(newKnot);
            for (var i = 0; i < multiplicity; i++) _valuesFlat.Add(value);

            _cachedDistinctKnots = null; // Invalidate cache
            return this;
        }

        /// <summary>
        /// Multiply multiplicities by scalingFactor and return a new knot vector.
        /// </summary>
        public BSplineKnotVector ScaleMultiplicity(int scalingFactor)
        {
            if (scalingFactor < 0) throw new ArgumentOutOfRangeException(nameof(scalingFactor));
            var kv = new BSplineKnotVector();
            foreach (var k in _knotList)
                kv.AppendKnot(k.Value, k.Multiplicity * scalingFactor);
            return kv;
        }

        /// <summary>
        /// Remove multiplicities (set all multiplicities to 1).
        /// </summary>
        public BSplineKnotVector RemoveMultiplicity()
        {
            var kv = new BSplineKnotVector();
            foreach (var k in _knotList) kv.AppendKnot(k.Value, 1);
            return kv;
        }

        /// <summary>
        /// Create a B-spline basis set from this vector.
        /// </summary>
        public BSplineBasisSet CreateBSplineBasisSet(int degree)
        {
            if (degree < 0 || degree > MaxDegree)
                throw new ArgumentOutOfRangeException(nameof(degree));
            if (!IsValidForDegree(degree))
                throw new InvalidOperationException("Knot vector is not valid for the specified degree.");

            return BSplineBasisSet.Create(this, degree);
        }

        #region Parameter Range Methods

        /// <summary>
        /// Get the valid parameter range for a B-spline curve of the specified degree.
        /// For a degree-p B-spline, the curve is defined on the interval [U[p], U[Count - p - 1]).
        /// </summary>
        /// <param name="degree">The degree of the B-spline curve</param>
        /// <returns>The valid parameter range as a Float64Interval</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when degree is invalid</exception>
        /// <exception cref="InvalidOperationException">Thrown when the knot vector is not valid for the given degree</exception>
        public Float64Interval GetParameterRange(int degree)
        {
            if (degree < 0 || degree > MaxDegree)
                throw new ArgumentOutOfRangeException(nameof(degree),
                    $"Degree must be between 0 and {MaxDegree}.");

            if (!IsValidForDegree(degree))
                throw new InvalidOperationException(
                    "Knot vector is not valid for the specified degree.");

            // For a degree-p B-spline with n+1 control points (where n = BasisCount - 1),
            // the curve is defined on the interval [U[p], U[n+1])
            // where n+1 = Count - degree - 1
            var n = GetBasisCount(degree) - 1;
            var startParam = this[degree];
            var endParam = this[n + 1];

            return Float64Interval.Create(startParam, endParam, false, false);
        }

        /// <summary>
        /// Get the valid parameter range for a B-spline curve of the specified degree,
        /// with an option to include the endpoints.
        /// </summary>
        /// <param name="degree">The degree of the B-spline curve</param>
        /// <param name="includeStart">Whether to include the start of the interval</param>
        /// <param name="includeEnd">Whether to include the end of the interval</param>
        /// <returns>The valid parameter range as a Float64Interval</returns>
        public Float64Interval GetParameterRange(int degree, bool includeStart, bool includeEnd)
        {
            var baseRange = GetParameterRange(degree);
            return Float64Interval.Create(baseRange.MinValue, baseRange.MaxValue, includeStart, includeEnd);
        }

        /// <summary>
        /// Check if a parameter value is within the valid range for a B-spline curve of the specified degree.
        /// </summary>
        /// <param name="degree">The degree of the B-spline curve</param>
        /// <param name="parameterValue">The parameter value to check</param>
        /// <returns>True if the parameter is within the valid range</returns>
        public bool IsParameterInRange(int degree, double parameterValue)
        {
            try
            {
                var range = GetParameterRange(degree);
                return range.Contains(parameterValue);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Clamp a parameter value to the valid range for a B-spline curve of the specified degree.
        /// </summary>
        /// <param name="degree">The degree of the B-spline curve</param>
        /// <param name="parameterValue">The parameter value to clamp</param>
        /// <returns>The clamped parameter value</returns>
        public double ClampParameterToRange(int degree, double parameterValue)
        {
            try
            {
                var range = GetParameterRange(degree);
                return Math.Max(range.MinValue, Math.Min(range.MaxValue, parameterValue));
            }
            catch
            {
                // Fallback to first and last knot values if degree validation fails
                return Math.Max(FirstValue, Math.Min(LastValue, parameterValue));
            }
        }

        /// <summary>
        /// Get the normalized parameter value in [0,1] relative to the valid parameter range.
        /// </summary>
        /// <param name="degree">The degree of the B-spline curve</param>
        /// <param name="parameterValue">The parameter value to normalize</param>
        /// <returns>The normalized parameter value in [0,1]</returns>
        public double GetNormalizedParameter(int degree, double parameterValue)
        {
            var range = GetParameterRange(degree);
            var clampedValue = ClampParameterToRange(degree, parameterValue);

            if (Math.Abs(range.MaxValue - range.MinValue) < 1e-14)
                return 0.0;

            return (clampedValue - range.MinValue) / (range.MaxValue - range.MinValue);
        }

        /// <summary>
        /// Convert a normalized parameter value in [0,1] to the actual parameter range.
        /// </summary>
        /// <param name="degree">The degree of the B-spline curve</param>
        /// <param name="normalizedParameter">The normalized parameter value in [0,1]</param>
        /// <returns>The actual parameter value in the valid range</returns>
        public double GetParameterFromNormalized(int degree, double normalizedParameter)
        {
            if (normalizedParameter < 0.0 || normalizedParameter > 1.0)
                throw new ArgumentOutOfRangeException(nameof(normalizedParameter),
                    "Normalized parameter must be in [0,1].");

            var range = GetParameterRange(degree);
            return range.MinValue + normalizedParameter * (range.MaxValue - range.MinValue);
        }

        #endregion


        ///// <summary>
        ///// Determine parameter limits
        ///// </summary>
        ///// <param name="degree"></param>
        ///// <returns></returns>
        ///// <exception cref="InvalidOperationException"></exception>
        //public Float64ScalarRange GetParameterRange(int degree)
        //{
        //    if (Count < 2 * (degree + 1))
        //        throw new InvalidOperationException("Invalid knot vector for B-spline parameter limits.");

        //    var tMin = _valuesFlat[degree];
        //    var tMax = _valuesFlat[Count - degree - 1];

        //    return Float64ScalarRange.Create(tMin, tMax);
        //}


        #region Span lookup (binary search) and helpers

        /// <summary>
        /// Find the span index for parameter value u given polynomial degree p.
        /// (Returns span index 'i' such that u in [U[i], U[i+1])). Special-cases u == U[n+1] to n.
        /// </summary>
        public int FindSpan(int degree, double u)
        {
            var n = GetBasisCount(degree) - 1;
            if (n < 0) throw new InvalidOperationException("No basis functions defined for given degree.");

            // Special-case when u is at last knot value (clamped)
            var last = LastValue;
            if (u >= last - 1e-14) return n;

            // Binary search on flat knots
            var low = degree;
            var high = n + 1;

            while (low <= high)
            {
                var mid = (low + high) >> 1;
                var uMid = _valuesFlat[mid];

                if (u < uMid - 1e-14)
                {
                    high = mid - 1;
                }
                else
                {
                    var uMid1 = _valuesFlat[mid + 1];
                    if (u < uMid1 - 1e-14) return mid;
                    low = mid + 1;
                }
            }

            return Math.Max(degree, Math.Min(n, low));
        }

        /// <summary>Return pair of knot values at flat indices index1,index2.</summary>
        public Pair<double> GetKnotValueRange(int index1, int index2) => new(this[index1], this[index2]);

        /// <summary>Difference of knot values at flat indices index1 and index2.</summary>
        public double GetKnotValueDifference(int index1, int index2) => this[index2] - this[index1];

        #endregion

        #region Enumeration

        public IEnumerator<double> GetEnumerator() => _valuesFlat.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        #region Internal helpers

        private void RebuildFlatValues()
        {
            _valuesFlat.Clear();
            var index = 0;
            foreach (var k in _knotList)
            {
                // update Index1 to current running flat index
                var newK = new BSplineKnot(index, k.Value, k.Multiplicity);
                index += k.Multiplicity;
                _valuesFlat.AddRange(Enumerable.Repeat(k.Value, k.Multiplicity));
            }

            // rebuild _knotList with corrected Index1
            var rebuilt = new List<BSplineKnot>(_knotList.Count);
            var running = 0;
            foreach (var k in _knotList)
            {
                rebuilt.Add(new BSplineKnot(running, k.Value, k.Multiplicity));
                running += k.Multiplicity;
            }

            _knotList.Clear();
            _knotList.AddRange(rebuilt);

            _cachedDistinctKnots = null; // Invalidate cache
        }

        #endregion
    }
}