using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Tuples;
using System.Collections.Concurrent;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.BSpline
{
    /// <summary>
    /// B-spline basis set: evaluation of basis functions and derivatives.
    /// Uses standard algorithms from The NURBS Book (FindSpan, BasisFunctions, DersBasisFuns).
    /// </summary>
    public class BSplineBasisSet : IBSplineBasisSet
    {
        private readonly ConcurrentDictionary<(int span, double t, int derivativeOrder), double[][]> _basisDerivativesCache = new();
        private readonly ConcurrentDictionary<(int span, double t), double[]> _basisFunctionsCache = new();

        public int Degree { get; }
        public BSplineKnotVector KnotVector { get; }
        public int BasisCount => KnotVector.GetBasisCount(Degree);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static BSplineBasisSet Create(BSplineKnotVector knotVector, int degree)
            => new BSplineBasisSet(knotVector, degree);

        private BSplineBasisSet(BSplineKnotVector knotVector, int degree)
        {
            if (degree < 0 || degree > knotVector.MaxDegree)
                throw new ArgumentOutOfRangeException(nameof(degree));

            if (!knotVector.IsValidForDegree(degree))
                throw new ArgumentException("Knot vector is not valid for the specified degree.");

            Degree = degree;
            KnotVector = knotVector;
        }

        #region Basis evaluation (N_i,p(u))

        /// <summary>
        /// Evaluate a single basis function N_{i,p}(u).
        /// Uses FindSpan + BasisFunctions for efficiency and numerical stability.
        /// </summary>
        public double GetValue(int index, double u)
        {
            var n = BasisCount - 1;
            if (n < 0) return 0.0;

            if (index < 0 || index > n) 
                throw new ArgumentOutOfRangeException(nameof(index));

            // handle boundary: u == last knot value => basis function associated with last control point should be 1 at the end
            if (Math.Abs(u - KnotVector.LastValue) < 1e-14)
            {
                // Last basis index is n
                return index == n ? 1.0 : 0.0;
            }

            var span = KnotVector.FindSpan(Degree, u);
            var local = GetBasisFunctions(span, u); // N[0..p] corresponds to N_{span-p}..N_{span}
            var firstIndex = span - Degree;
            var offset = index - firstIndex;
            if (offset < 0 || offset > Degree) return 0.0;
            return local[offset];
        }

        /// <summary>
        /// Evaluate basis function N_i,p(t) multiplied by a scalar factor.
        /// </summary>
        public double GetValue(int index, double parameterValue, double termScalar)
        {
            if (index < 0 || index >= BasisCount)
                throw new ArgumentOutOfRangeException(nameof(index));

            return termScalar * GetValue(index, parameterValue);
        }

        /// <summary>
        /// Compute a weighted sum over all basis functions:
        /// Σ_i termScalarsList[i] * N_i,p(t)
        /// </summary>
        public double GetValue(double parameterValue, params double[] termScalarsList)
        {
            if (termScalarsList == null)
                throw new ArgumentNullException(nameof(termScalarsList));

            if (termScalarsList.Length != BasisCount)
                throw new ArgumentException("termScalarsList length must equal BasisCount.");

            var sum = 0.0;
            for (var i = 0; i < BasisCount; i++)
                sum += termScalarsList[i] * GetValue(i, parameterValue);

            return sum;
        }

        /// <summary>
        /// Return the values of all basis functions N_i,p(t) at the given parameter.
        /// </summary>
        public IReadOnlyList<double> GetValues(double parameterValue)
        {
            var values = new double[BasisCount];
            for (var i = 0; i < BasisCount; i++)
                values[i] = GetValue(i, parameterValue);

            return values;
        }

        /// <summary>
        /// Compute the basis functions N_{span-p, p}(u) ... N_{span, p}(u) with caching.
        /// Algorithm A2.2 (The NURBS Book).
        /// </summary>
        private double[] GetBasisFunctions(int span, double u)
        {
            return _basisFunctionsCache.GetOrAdd((span, u), key => CalculateBasisFunctions(key.span, key.t));
        }

        private double[] CalculateBasisFunctions(int span, double u)
        {
            var N = new double[Degree + 1];
            var left = new double[Degree + 1];
            var right = new double[Degree + 1];

            N[0] = 1.0;
            for (var j = 1; j <= Degree; j++)
            {
                left[j] = u - KnotVector[span + 1 - j];
                right[j] = KnotVector[span + j] - u;
                var saved = 0.0;
                for (var r = 0; r < j; r++)
                {
                    var denominator = right[r + 1] + left[j - r];
                    if (Math.Abs(denominator) < 1e-14)
                    {
                        // Handle zero denominator case
                        N[r] = saved;
                        saved = 0.0;
                    }
                    else
                    {
                        var temp = N[r] / denominator;
                        N[r] = saved + right[r + 1] * temp;
                        saved = left[j - r] * temp;
                    }
                }
                N[j] = saved;
            }

            return N;
        }

        #endregion

        #region Derivatives (up to specified order)

        /// <summary>
        /// Compute derivatives up to specified order for basis functions (Algorithm A2.3 from The NURBS Book).
        /// Returns ders[k][j] where k is derivative order (0..maxDerivativeOrder) and j is local basis index (0..p).
        /// </summary>
        private double[][] GetBasisDerivatives(int span, double u, int maxDerivativeOrder)
        {
            if (maxDerivativeOrder < 0) 
                throw new ArgumentOutOfRangeException(nameof(maxDerivativeOrder));
                
            maxDerivativeOrder = Math.Min(maxDerivativeOrder, Degree);
            
            return _basisDerivativesCache.GetOrAdd(
                (span, u, maxDerivativeOrder), 
                key => CalculateBasisDerivatives(key.span, key.t, key.derivativeOrder)
            );
        }

        /// <summary>
        /// Algorithm A2.3 from The NURBS Book - Compute nonzero basis functions and their derivatives.
        /// Corrected implementation with proper indexing and numerical stability.
        /// </summary>
        private double[][] CalculateBasisDerivatives(int span, double u, int maxDerivativeOrder)
        {
            if (maxDerivativeOrder > Degree) 
                maxDerivativeOrder = Degree;

            var p = Degree;
            var ndu = new double[p + 1, p + 1];
            var left = new double[p + 1];
            var right = new double[p + 1];

            // Initialize the basis functions table
            ndu[0, 0] = 1.0;

            for (var j = 1; j <= p; j++)
            {
                left[j] = u - KnotVector[span + 1 - j];
                right[j] = KnotVector[span + j] - u;
                
                var saved = 0.0;

                for (var r = 0; r < j; r++)
                {
                    // Compute denominator with numerical stability check
                    var denominator = right[r + 1] + left[j - r];
                    
                    if (Math.Abs(denominator) < 1e-14)
                    {
                        // Handle zero denominator case
                        ndu[j, r] = 0.0;
                        var temp = ndu[r, j - 1] / (right[r + 1] + left[j - r] + 1e-14);
                        ndu[r, j] = saved + right[r + 1] * temp;
                        saved = left[j - r] * temp;
                    }
                    else
                    {
                        ndu[j, r] = denominator;
                        var temp = ndu[r, j - 1] / denominator;
                        ndu[r, j] = saved + right[r + 1] * temp;
                        saved = left[j - r] * temp;
                    }
                }
                
                ndu[j, j] = saved;
            }

            // Initialize derivatives array: ders[derivativeOrder][localBasisIndex]
            var ders = new double[maxDerivativeOrder + 1][];
            for (var i = 0; i <= maxDerivativeOrder; i++)
            {
                ders[i] = new double[p + 1];
            }

            // Load the basis functions (0th derivatives)
            for (var j = 0; j <= p; j++)
            {
                ders[0][j] = ndu[j, p];
            }

            // Compute derivatives if needed
            if (maxDerivativeOrder > 0)
            {
                var a = new double[2, p + 1];
                
                for (var r = 0; r <= p; r++)
                {
                    var s1 = 0;
                    var s2 = 1;
                    a[0, 0] = 1.0;

                    for (var k = 1; k <= maxDerivativeOrder; k++)
                    {
                        var d = 0.0;
                        var rk = r - k;
                        var pk = p - k;

                        if (r >= k)
                        {
                            var denominator = ndu[pk + 1, rk];
                            if (Math.Abs(denominator) < 1e-14)
                            {
                                a[s2, 0] = 0.0;
                            }
                            else
                            {
                                a[s2, 0] = a[s1, 0] / denominator;
                            }
                            d = a[s2, 0] * ndu[rk, pk];
                        }

                        var j1 = (rk >= -1) ? 1 : -rk;
                        var j2 = (r - 1 <= pk) ? k - 1 : p - r;

                        for (var j = j1; j <= j2; j++)
                        {
                            var denominator = ndu[pk + 1, rk + j];
                            if (Math.Abs(denominator) < 1e-14)
                            {
                                a[s2, j] = 0.0;
                            }
                            else
                            {
                                a[s2, j] = (a[s1, j] - a[s1, j - 1]) / denominator;
                            }
                            d += a[s2, j] * ndu[rk + j, pk];
                        }

                        if (r <= pk)
                        {
                            var denominator = ndu[pk + 1, r];
                            if (Math.Abs(denominator) < 1e-14)
                            {
                                a[s2, k] = 0.0;
                            }
                            else
                            {
                                a[s2, k] = -a[s1, k - 1] / denominator;
                            }
                            d += a[s2, k] * ndu[r, pk];
                        }

                        ders[k][r] = d;
                        
                        // Swap s1 and s2
                        (s1, s2) = (s2, s1);
                    }
                }

                // Multiply through by the correct factorial factors
                var rfact = p;
                for (var k = 1; k <= maxDerivativeOrder; k++)
                {
                    for (var j = 0; j <= p; j++)
                    {
                        ders[k][j] *= rfact;
                    }
                    rfact *= (p - k);
                }
            }

            return ders;
        }

        /// <summary>
        /// First derivative of N_{i,p}(u).
        /// </summary>
        public double GetDerivative(int index, double u)
        {
            var n = BasisCount - 1;
            if (n < 0) return 0.0;
            if (index < 0 || index > n) 
                throw new ArgumentOutOfRangeException(nameof(index));

            if (Math.Abs(u - KnotVector.LastValue) < 1e-14) 
                return 0.0; // derivative at endpoint typically zero

            var span = KnotVector.FindSpan(Degree, u);
            var ders = GetBasisDerivatives(span, u, 1); // ders[0..1][0..p]
            var firstIndex = span - Degree;
            var offset = index - firstIndex;
            if (offset < 0 || offset > Degree) return 0.0;
            return ders[1][offset];
        }

        /// <summary>
        /// Second derivative of N_{i,p}(u).
        /// </summary>
        public double GetSecondDerivative(int index, double u)
        {
            var n = BasisCount - 1;
            if (n < 0) return 0.0;
            if (index < 0 || index > n) 
                throw new ArgumentOutOfRangeException(nameof(index));

            if (Math.Abs(u - KnotVector.LastValue) < 1e-14) 
                return 0.0;

            var span = KnotVector.FindSpan(Degree, u);
            var ders = GetBasisDerivatives(span, u, 2);
            var firstIndex = span - Degree;
            var offset = index - firstIndex;
            if (offset < 0 || offset > Degree) return 0.0;
            return ders[2][offset];
        }

        /// <summary>
        /// Get derivative of specified order for basis function N_{i,p}(u).
        /// </summary>
        public double GetDerivative(int index, double u, int derivativeOrder)
        {
            if (derivativeOrder < 0) 
                throw new ArgumentOutOfRangeException(nameof(derivativeOrder));
            if (derivativeOrder == 0) return GetValue(index, u);
            if (derivativeOrder == 1) return GetDerivative(index, u);
            if (derivativeOrder == 2) return GetSecondDerivative(index, u);

            var n = BasisCount - 1;
            if (n < 0) return 0.0;
            if (index < 0 || index > n) 
                throw new ArgumentOutOfRangeException(nameof(index));

            if (Math.Abs(u - KnotVector.LastValue) < 1e-14) 
                return 0.0;

            var span = KnotVector.FindSpan(Degree, u);
            var ders = GetBasisDerivatives(span, u, derivativeOrder);
            var firstIndex = span - Degree;
            var offset = index - firstIndex;
            if (offset < 0 || offset > Degree) return 0.0;
            return ders[derivativeOrder][offset];
        }

        #endregion

        #region Support and Utility Methods

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Interval GetSupportInterval(int index)
        {
            if (index < 0 || index >= BasisCount)
                throw new ArgumentOutOfRangeException(nameof(index));
                
            // support: [U_i, U_{i+p+1})
            var a = KnotVector[index];
            var b = KnotVector[index + Degree + 1];
            return Float64Interval.Create(a, b, false, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BSplineBasisPairProductSet CreatePairProductSet() 
            => BSplineBasisPairProductSet.Create(this);

        /// <summary>
        /// Clear the basis function cache to free memory.
        /// </summary>
        public void ClearCache()
        {
            _basisFunctionsCache.Clear();
            _basisDerivativesCache.Clear();
        }

        /// <summary>
        /// Get the span index for a given parameter value u.
        /// </summary>
        public int FindSpan(double u)
        {
            return KnotVector.FindSpan(Degree, u);
        }

        /// <summary>
        /// Evaluate all non-zero basis functions and their derivatives at parameter u.
        /// Returns a tuple (span, basisValues, basisDerivatives) where:
        /// - span is the knot span index
        /// - basisValues are the non-zero basis function values [0..p]
        /// - basisDerivatives[k][j] are the k-th derivatives for local basis j
        /// </summary>
        public (int span, double[] basisValues, double[][] basisDerivatives) EvaluateAllAt(double u, int maxDerivativeOrder = 0)
        {
            if (maxDerivativeOrder < 0)
                throw new ArgumentOutOfRangeException(nameof(maxDerivativeOrder));

            var span = FindSpan(u);
            var basisValues = GetBasisFunctions(span, u);
            var basisDerivatives = GetBasisDerivatives(span, u, maxDerivativeOrder);

            return (span, basisValues, basisDerivatives);
        }

        /// <summary>
        /// Check if the basis set is valid and well-conditioned.
        /// </summary>
        public bool IsValid()
        {
            try
            {
                // Test evaluation at a few points
                var testPoints = new[] { 0.0, 0.25, 0.5, 0.75, 1.0 };
                foreach (var u in testPoints)
                {
                    var span = FindSpan(u);
                    var basis = GetBasisFunctions(span, u);
                    
                    // Basis functions should sum to 1 (partition of unity)
                    var sum = basis.Sum();
                    if (Math.Abs(sum - 1.0) > 1e-10)
                        return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}