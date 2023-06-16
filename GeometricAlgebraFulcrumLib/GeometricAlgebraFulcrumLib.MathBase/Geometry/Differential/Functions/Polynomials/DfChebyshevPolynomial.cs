using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using MathNet.Numerics;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Differential.Functions.Polynomials
{
    /// <summary>
    /// Chebyshev Type1 Polynomials
    /// </summary>
    public class DfChebyshevPolynomial :
        DfPolynomial
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfAffinePolynomial GetVarToUnitMap(double minVarValue, double maxVarValue)
        {
            if (minVarValue.IsNaNOrInfinite())
                throw new NotFiniteNumberException(nameof(minVarValue));

            if (maxVarValue.IsNaNOrInfinite())
                throw new NotFiniteNumberException(nameof(maxVarValue));

            var x = DfAffinePolynomial.Identity;
            return 2d * (x - minVarValue) / (maxVarValue - minVarValue) - 1d;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfAffinePolynomial GetUnitToVarMap(double minVarValue, double maxVarValue)
        {
            if (minVarValue.IsNaNOrInfinite())
                throw new ArgumentException(nameof(minVarValue));

            if (maxVarValue.IsNaNOrInfinite())
                throw new ArgumentException(nameof(maxVarValue));

            var zPlus1 = DfAffinePolynomial.OnePlusIdentity;
            return 0.5 * (maxVarValue - minVarValue) * zPlus1 + minVarValue;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<DfAffinePolynomial> GetVarUnitMapPair(double minVarValue, double maxVarValue, int sampleCount)
        {
            if (minVarValue.IsNaNOrInfinite())
                throw new ArgumentException(nameof(minVarValue));

            if (maxVarValue.IsNaNOrInfinite())
                throw new ArgumentException(nameof(maxVarValue));

            var m = Math.Cos(0.5d * Math.PI / sampleCount);
            var x = DfAffinePolynomial.Identity;
            var z = DfAffinePolynomial.Identity;
            var x21Difference = maxVarValue - minVarValue;

            var varToUnitMap = m * (2d * (x - minVarValue) / x21Difference - 1d);
            var unitToVarMap = 0.5 * x21Difference * (z / m + 1) + minVarValue;

            return new Pair<DfAffinePolynomial>(
                varToUnitMap,
                unitToVarMap
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<DfAffinePolynomial> GetVarUnitMapPair(double minVarValue, double maxVarValue)
        {
            if (minVarValue.IsNaNOrInfinite())
                throw new ArgumentException(nameof(minVarValue));

            if (maxVarValue.IsNaNOrInfinite())
                throw new ArgumentException(nameof(maxVarValue));

            var x = DfAffinePolynomial.Identity;
            var zPlus1 = DfAffinePolynomial.OnePlusIdentity;
            var x21Difference = maxVarValue - minVarValue;

            var varToUnitMap = 2d * (x - minVarValue) / x21Difference - 1d;
            var unitToVarMap = 0.5 * x21Difference * zPlus1 + minVarValue;

            return new Pair<DfAffinePolynomial>(
                varToUnitMap,
                unitToVarMap
            );
        }

        public static Float64Vector GetScalarCoefficients(int degree, Func<double, double> f)
        {
            if (degree < 0)
                throw new ArgumentOutOfRangeException(nameof(degree));

            var n = degree + 1;
            var scalarCoefficients = Float64VectorComposer.Create();

            for (var j = 0; j < n; j++)
            {
                for (var k = 1; k <= n; k++)
                {
                    var theta = Math.PI * (k - 0.5d) / n;
                    var t = Math.Cos(theta);

                    scalarCoefficients[j] += f(t) * Math.Cos(j * theta);
                }

                scalarCoefficients[j] *= 2d / n;
            }

            return scalarCoefficients.GetVector();
        }
    
        public static Float64Vector GetScalarCoefficients(int degree, Func<double, double> f, int sampleCount, double minVarValue, double maxVarValue)
        {
            if (degree < 0)
                throw new ArgumentOutOfRangeException(nameof(degree));

            if (sampleCount < degree)
                throw new ArgumentOutOfRangeException(nameof(sampleCount));

            var n = degree;
            var m = sampleCount - 2;
            var scalarCoefficients = Float64VectorComposer.Create();

            for (var i = 1; i <= m; i++)
            {
                var zi = -Math.Cos((2 * i - 1) * Math.PI / m);
                var xi = 0.5 * (zi / Math.Cos(0.5 * Math.PI / m) + 1) * (maxVarValue - minVarValue) + minVarValue;
                var fxi = f(xi);

                scalarCoefficients[0] += fxi / m;

                for (var j = 1; j <= degree; j++)
                    scalarCoefficients[j] += 
                        2d / m * fxi * DfChebyshevBasis.GetChebyshevValue(j, zi);
            }

            return scalarCoefficients.GetVector();
        }

        public static Float64Vector GetScalarCoefficients(int degree, Func<double, double> f, DfAffinePolynomial unitToVarMap)
        {
            if (degree < 0)
                throw new ArgumentOutOfRangeException(nameof(degree));

            var n = degree + 1;
            var scalarCoefficients = Float64VectorComposer.Create();

            for (var j = 0; j < n; j++)
            {
                for (var k = 1; k <= n; k++)
                {
                    var theta = Math.PI * (k - 0.5d) / n;
                    var x = Math.Cos(theta);
                    var t = unitToVarMap.GetValue(x);

                    scalarCoefficients[j] += f(t) * Math.Cos(j * theta);
                }

                scalarCoefficients[j] *= 2d / n;
            }

            return scalarCoefficients.GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double GetValue(IReadOnlyList<double> scalarList, DfAffinePolynomial varToUnitMap, double x)
        {
            var z = varToUnitMap.GetValue(x);

            return GetValue(scalarList, z);
        }

        public static double GetValue(IReadOnlyList<double> scalarList, double z)
        {
            var degree = scalarList.Count - 1;

            if (degree < 0) 
                return 0d;
            //throw new ArgumentOutOfRangeException(nameof(degree));

            if (degree == 0) return scalarList[0];
            if (degree == 1) return scalarList[0] + scalarList[1] * z;
            if (degree == 2) return scalarList[0] + scalarList[1] * z + scalarList[2] * (2 * z * z - 1);

            var valueArray = new double[degree + 1];

            valueArray[0] = 1d;
            valueArray[1] = z;
            valueArray[2] = 2 * z * z - 1;

            var y =
                scalarList[0] +
                scalarList[1] * z +
                scalarList[2] * (2 * z * z - 1);

            z = 2d * z;
            for (var i = 3; i <= degree; i++)
            {
                var t = z * valueArray[i - 1] - valueArray[i - 2];
                y += scalarList[i] * t;

                valueArray[i] = t;
            }

            return y;
        }
        
        public static double GetValue(Float64Vector scalarList, double z)
        {
            var degree = scalarList.VSpaceDimensions - 1;

            if (degree < 0) 
                return 0d;

            //throw new ArgumentOutOfRangeException(nameof(degree));

            if (degree == 0) return scalarList[0];
            if (degree == 1) return scalarList[0] + scalarList[1] * z;
            if (degree == 2) return scalarList[0] + scalarList[1] * z + scalarList[2] * (2 * z * z - 1);

            var valueArray = new double[degree + 1];

            valueArray[0] = 1d;
            valueArray[1] = z;
            valueArray[2] = 2 * z * z - 1;

            var y =
                scalarList[0] +
                scalarList[1] * z +
                scalarList[2] * (2 * z * z - 1);

            z = 2d * z;
            for (var i = 3; i <= degree; i++)
            {
                var t = z * valueArray[i - 1] - valueArray[i - 2];
                y += scalarList[i] * t;

                valueArray[i] = t;
            }

            return y;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfChebyshevPolynomial CreateZero()
        {
            return new DfChebyshevPolynomial(
                Float64Vector.ZeroVector
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfChebyshevPolynomial Create(Float64Vector scalarCoefficients)
        {
            return new DfChebyshevPolynomial(scalarCoefficients);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfChebyshevPolynomial Create(Float64Vector scalarCoefficients, double minVarValue, double maxVarValue)
        {
            var (varToUnitMap, unitToVarMap) =
                GetVarUnitMapPair(minVarValue, maxVarValue);

            return new DfChebyshevPolynomial(
                scalarCoefficients,
                varToUnitMap,
                unitToVarMap
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfChebyshevPolynomial Create(Float64Vector scalarCoefficients, DfAffinePolynomial varToUnitMap, DfAffinePolynomial unitToVarMap)
        {
            return new DfChebyshevPolynomial(
                scalarCoefficients,
                varToUnitMap,
                unitToVarMap
            );
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfChebyshevPolynomial Create(IEnumerable<Tuple<double, DfChebyshevBasis>> scalarBasisList)
        {
            var scalarCoefficients = Float64VectorComposer.Create();

            foreach (var (scalar, basis) in scalarBasisList)
                scalarCoefficients[basis.Degree] += scalar;

            return new DfChebyshevPolynomial(scalarCoefficients.GetVector());
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfChebyshevPolynomial Create(IEnumerable<Tuple<double, DfChebyshevPolynomial>> scalarPolynomialList)
        {
            var scalarCoefficients = Float64VectorComposer.Create();

            foreach (var (scalar1, polynomial) in scalarPolynomialList)
            foreach (var (scalar2, basis) in polynomial.GetScaledBasis())
                scalarCoefficients[basis.Degree] += scalar1 * scalar2;

            return new DfChebyshevPolynomial(scalarCoefficients.GetVector());
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfChebyshevPolynomial CreateApproximating(int degree, Func<double, double> f, double minVarValue, double maxVarValue, int sampleCount)
        {
            if (sampleCount < degree)
                throw new InvalidOperationException();

            var (varToUnitMap, unitToVarMap) =
                GetVarUnitMapPair(minVarValue, maxVarValue, sampleCount);

            var scalarCoefficients =
                GetScalarCoefficients(degree, f, sampleCount, minVarValue, maxVarValue);

            return new DfChebyshevPolynomial(
                scalarCoefficients,
                varToUnitMap,
                unitToVarMap
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfChebyshevPolynomial CreateApproximating(int degree, Func<double, double> f, double minVarValue, double maxVarValue)
        {
            var (varToUnitMap, unitToVarMap) =
                GetVarUnitMapPair(minVarValue, maxVarValue);

            var scalarCoefficients =
                GetScalarCoefficients(degree, f, unitToVarMap);

            return new DfChebyshevPolynomial(
                scalarCoefficients,
                varToUnitMap,
                unitToVarMap
            );
        }

        /// <summary>
        /// Approximate the Sin function in the range [-Pi, Pi]
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfChebyshevPolynomial CreateApproximatingSin()
        {
            var (varToUnitMap, unitToVarMap) =
                GetVarUnitMapPair(-Math.PI, Math.PI);

            var scalarCoefficients =
                GetScalarCoefficients(
                    19,
                    Math.Sin,
                    unitToVarMap
                ).GetCopyByScalar(c => !c.IsNearZero(1e-15));

            return new DfChebyshevPolynomial(
                scalarCoefficients,
                varToUnitMap,
                unitToVarMap
            );
        }

        /// <summary>
        /// Approximate the Cos function in the range [-0.5 Pi, 1.5 Pi]
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfChebyshevPolynomial CreateApproximatingCos()
        {
            var (varToUnitMap, unitToVarMap) =
                GetVarUnitMapPair(-0.5 * Math.PI, 1.5 * Math.PI);

            var scalarCoefficients =
                GetScalarCoefficients(
                    19,
                    Math.Cos,
                    unitToVarMap
                ).GetCopyByScalar(c => !c.IsNearZero(1e-15));

            return new DfChebyshevPolynomial(
                scalarCoefficients,
                varToUnitMap,
                unitToVarMap
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfChebyshevPolynomial operator -(DfChebyshevPolynomial p1)
        {
            return p1.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfChebyshevPolynomial operator +(DfChebyshevPolynomial p1, double p2)
        {
            return p1.Add(p2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfChebyshevPolynomial operator +(DfChebyshevPolynomial p1, DfChebyshevBasis p2)
        {
            return p1.Add(p2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfChebyshevPolynomial operator +(DfChebyshevPolynomial p1, DfChebyshevPolynomial p2)
        {
            return p1.Add(p2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfChebyshevPolynomial operator -(DfChebyshevPolynomial p1, double p2)
        {
            return p1.Subtract(p2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfChebyshevPolynomial operator -(DfChebyshevPolynomial p1, DfChebyshevBasis p2)
        {
            return p1.Subtract(p2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfChebyshevPolynomial operator -(DfChebyshevPolynomial p1, DfChebyshevPolynomial p2)
        {
            return p1.Subtract(p2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfChebyshevPolynomial operator *(DfChebyshevPolynomial p1, double p2)
        {
            return p1.Times(p2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfChebyshevPolynomial operator *(double p1, DfChebyshevPolynomial p2)
        {
            return p2.Times(p1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfChebyshevPolynomial operator *(DfChebyshevPolynomial p1, DfChebyshevBasis p2)
        {
            return p1.Times(p2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfChebyshevPolynomial operator *(DfChebyshevBasis p1, DfChebyshevPolynomial p2)
        {
            return p2.Times(p1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfChebyshevPolynomial operator *(DfChebyshevPolynomial p1, DfChebyshevPolynomial p2)
        {
            return p2.Times(p1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DfChebyshevPolynomial operator /(DfChebyshevPolynomial p1, double p2)
        {
            return p1.Divide(p2);
        }


        public bool HasVarMap { get; }

        public DfAffinePolynomial VarToUnitMap { get; }

        public DfAffinePolynomial UnitToVarMap { get; }
    

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private DfChebyshevPolynomial(Float64Vector scalarCoefficients, DfAffinePolynomial varToUnitMap, DfAffinePolynomial unitToVarMap)
            : base(scalarCoefficients)
        {
            //Debug.Assert(
            //    unitToVarMap.IsNearSame(varToUnitMap.InverseAffinePolynomial())
            //);

            if (varToUnitMap.IsIdentity || unitToVarMap.IsIdentity)
            {
                HasVarMap = false;
                VarToUnitMap = DfAffinePolynomial.Identity;
                UnitToVarMap = DfAffinePolynomial.Identity;
            }
            else
            {
                HasVarMap = true;
                VarToUnitMap = varToUnitMap;
                UnitToVarMap = unitToVarMap;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private DfChebyshevPolynomial(Float64Vector scalarCoefficients)
            : base(scalarCoefficients)
        {
            HasVarMap = false;
            VarToUnitMap = DfAffinePolynomial.Identity;
            UnitToVarMap = DfAffinePolynomial.Identity;
        }


        /// <summary>
        /// Evaluation of Chebyshev polynomials for the Chebyshev nodes.
        /// https://en.wikipedia.org/wiki/Chebyshev_nodes
        /// </summary>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        private double Tn(int j, int k)
        {
            return Math.Cos(Math.PI * j * (k - 0.5d) / (Degree + 1));
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<Tuple<double, DfChebyshevBasis>> GetScaledBasis()
        {
            return ScalarCoefficients.IndexScalarPairs.Select(r => 
                new Tuple<double, DfChebyshevBasis>(
                    r.Value,
                    DfChebyshevBasis.Create(r.Key)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double GetValue(double x)
        {
            var z =
                HasVarMap ? VarToUnitMap.GetValue(x) : x;

            return GetValue(ScalarCoefficients, z);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction GetDerivative1()
        {
            return GetPolynomialDerivative1().Simplify();
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override DifferentialFunction GetDerivativeN(int order)
        {
            return GetPolynomialDerivativeN(order);
        }

        public DfChebyshevPolynomial GetPolynomialDerivative1()
        {
            var scalarCoefficients = Float64VectorComposer.Create();

            foreach (var (n, c) in ScalarCoefficients.IndexScalarPairs)
            {
                for (var k = 0; k <= n - 1; k++)
                {
                    if ((n - 1 - k).IsOdd()) continue;

                    scalarCoefficients[k] +=
                        (k == 0 ? 0.5d : 1) * 2d * n * c;
                }
            }

            if (HasVarMap) 
                scalarCoefficients.Times(VarToUnitMap.ScalarFactor);

            return new DfChebyshevPolynomial(
                scalarCoefficients.GetVector(),
                VarToUnitMap,
                UnitToVarMap
            );
        }
    
        public DfChebyshevPolynomial GetPolynomialDerivativeN(int order)
        {
            if (order < 0)
                throw new ArgumentOutOfRangeException(nameof(order));

            if (order == 0)
                return this;

            if (order > Degree)
                return CreateZero();

            var derivative = this;
            while (order > 0)
            {
                derivative = derivative.GetPolynomialDerivative1();

                order--;
            }

            return derivative;
        }
    

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DfMonomialPolynomial ToMonomialPolynomial()
        {
            return DfMonomialPolynomial.Create(
                GetScaledBasis().MapItem2(
                    b => b.ToMonomialPolynomial()
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DfChebyshevPolynomial Negative()
        {
            var scalarCoefficients = -ScalarCoefficients;

            return HasVarMap
                ? new DfChebyshevPolynomial(
                    scalarCoefficients,
                    VarToUnitMap,
                    UnitToVarMap
                )
                : new DfChebyshevPolynomial(scalarCoefficients);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DfChebyshevPolynomial Add(double p2)
        {
            if (p2.IsNaNOrInfinite())
                throw new ArgumentException(nameof(p2));

            var scalarCoefficients = Float64VectorComposer.Create(ScalarCoefficients);

            scalarCoefficients[0] += p2;
        
            return HasVarMap
                ? new DfChebyshevPolynomial(
                    scalarCoefficients.GetVector(),
                    VarToUnitMap,
                    UnitToVarMap
                )
                : new DfChebyshevPolynomial(scalarCoefficients.GetVector());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DfChebyshevPolynomial Add(DfChebyshevBasis p2)
        {
            var scalarCoefficients = Float64VectorComposer.Create(ScalarCoefficients);

            scalarCoefficients[p2.Degree] += 1d;

            return HasVarMap
                ? new DfChebyshevPolynomial(
                    scalarCoefficients.GetVector(),
                    VarToUnitMap,
                    UnitToVarMap
                )
                : new DfChebyshevPolynomial(scalarCoefficients.GetVector());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DfChebyshevPolynomial Add(DfChebyshevBasis p2, double scalarFactor)
        {
            if (scalarFactor.IsNaNOrInfinite())
                throw new ArgumentException(nameof(scalarFactor));

            var scalarCoefficients = Float64VectorComposer.Create(ScalarCoefficients);

            scalarCoefficients[p2.Degree] += scalarFactor;

            return HasVarMap
                ? new DfChebyshevPolynomial(
                    scalarCoefficients.GetVector(),
                    VarToUnitMap,
                    UnitToVarMap
                )
                : new DfChebyshevPolynomial(scalarCoefficients.GetVector());
        }

        public DfChebyshevPolynomial Add(DfChebyshevPolynomial p2)
        {
            // Make sure the variable intervals of both polynomials intersect properly
            if (!VarToUnitMap.IsSame(p2.VarToUnitMap))
                throw new InvalidOperationException();

            var scalarCoefficients = Float64VectorComposer.Create(ScalarCoefficients);

            foreach (var (i, c) in p2.ScalarCoefficients.IndexScalarPairs)
                scalarCoefficients[i] += c;

            return HasVarMap
                ? new DfChebyshevPolynomial(
                    scalarCoefficients.GetVector(),
                    VarToUnitMap,
                    UnitToVarMap
                )
                : new DfChebyshevPolynomial(scalarCoefficients.GetVector());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DfChebyshevPolynomial Subtract(double p2)
        {
            if (p2.IsNaNOrInfinite())
                throw new ArgumentException(nameof(p2));

            var scalarCoefficients = Float64VectorComposer.Create(ScalarCoefficients);

            scalarCoefficients[0] -= p2;

            return HasVarMap
                ? new DfChebyshevPolynomial(
                    scalarCoefficients.GetVector(),
                    VarToUnitMap,
                    UnitToVarMap
                )
                : new DfChebyshevPolynomial(scalarCoefficients.GetVector());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DfChebyshevPolynomial Subtract(DfChebyshevBasis p2)
        {
            var scalarCoefficients = Float64VectorComposer.Create(ScalarCoefficients);

            scalarCoefficients[p2.Degree] -= 1d;

            return HasVarMap
                ? new DfChebyshevPolynomial(
                    scalarCoefficients.GetVector(),
                    VarToUnitMap,
                    UnitToVarMap
                )
                : new DfChebyshevPolynomial(scalarCoefficients.GetVector());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DfChebyshevPolynomial Subtract(DfChebyshevBasis p2, double scalarFactor)
        {
            if (scalarFactor.IsNaNOrInfinite())
                throw new ArgumentException(nameof(scalarFactor));

            var scalarCoefficients = Float64VectorComposer.Create(ScalarCoefficients);

            scalarCoefficients[p2.Degree] -= scalarFactor;

            return HasVarMap
                ? new DfChebyshevPolynomial(
                    scalarCoefficients.GetVector(),
                    VarToUnitMap,
                    UnitToVarMap
                )
                : new DfChebyshevPolynomial(scalarCoefficients.GetVector());
        }

        public DfChebyshevPolynomial Subtract(DfChebyshevPolynomial p2)
        {
            // Make sure the variable intervals of both polynomials intersect properly
            if (!VarToUnitMap.IsSame(p2.VarToUnitMap))
                throw new InvalidOperationException();

            var scalarCoefficients = Float64VectorComposer.Create(ScalarCoefficients);

            foreach (var (i, c) in p2.ScalarCoefficients.IndexScalarPairs)
                scalarCoefficients[i] -= c;

            return HasVarMap
                ? new DfChebyshevPolynomial(
                    scalarCoefficients.GetVector(),
                    VarToUnitMap,
                    UnitToVarMap
                )
                : new DfChebyshevPolynomial(scalarCoefficients.GetVector());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DfChebyshevPolynomial Times(double scalar)
        {
            if (scalar.IsNaNOrInfinite())
                throw new InvalidOperationException();

            var scalarCoefficients = 
                ScalarCoefficients * scalar;

            return HasVarMap
                ? new DfChebyshevPolynomial(
                    scalarCoefficients,
                    VarToUnitMap,
                    UnitToVarMap
                )
                : new DfChebyshevPolynomial(scalarCoefficients);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DfChebyshevPolynomial Times(DfChebyshevBasis p2)
        {
            var scalarCoefficients = Float64VectorComposer.Create();

            var i2 = p2.Degree;
            foreach (var (i1, c1) in ScalarCoefficients.IndexScalarPairs)
            {
                var i = i1 + i2;
                var j = Math.Abs(i1 - i2);
                var d = 0.5 * c1;

                scalarCoefficients[i] += d;
                scalarCoefficients[j] += d;
            }

            return HasVarMap
                ? new DfChebyshevPolynomial(
                    scalarCoefficients.GetVector(),
                    VarToUnitMap,
                    UnitToVarMap
                )
                : new DfChebyshevPolynomial(scalarCoefficients.GetVector());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DfChebyshevPolynomial Times(DfChebyshevBasis p2, double scalingFactor)
        {
            if (scalingFactor.IsNaNOrInfinite())
                throw new NotFiniteNumberException(nameof(scalingFactor));

            var scalarCoefficients = Float64VectorComposer.Create();

            var i2 = p2.Degree;
            foreach (var (i1, c1) in ScalarCoefficients.IndexScalarPairs)
            {
                var i = i1 + i2;
                var j = Math.Abs(i1 - i2);
                var d = 0.5 * c1 * scalingFactor;

                scalarCoefficients[i] += d;
                scalarCoefficients[j] += d;
            }

            return HasVarMap
                ? new DfChebyshevPolynomial(
                    scalarCoefficients.GetVector(),
                    VarToUnitMap,
                    UnitToVarMap
                )
                : new DfChebyshevPolynomial(scalarCoefficients.GetVector());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DfChebyshevPolynomial Times(DfChebyshevPolynomial p2)
        {
            // Make sure the variable intervals of both polynomials intersect properly
            if (!VarToUnitMap.IsSame(p2.VarToUnitMap))
                throw new InvalidOperationException();

            var scalarCoefficients = Float64VectorComposer.Create();

            foreach (var (i1, c1) in ScalarCoefficients.IndexScalarPairs)
            foreach (var (i2, c2) in p2.ScalarCoefficients.IndexScalarPairs)
            {
                var i = i1 + i2;
                var j = Math.Abs(i1 - i2);
                var d = 0.5 * c1 * c2;

                scalarCoefficients[i] += d;
                scalarCoefficients[j] += d;
            }

            return HasVarMap
                ? new DfChebyshevPolynomial(
                    scalarCoefficients.GetVector(),
                    VarToUnitMap,
                    UnitToVarMap
                )
                : new DfChebyshevPolynomial(scalarCoefficients.GetVector());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DfChebyshevPolynomial Divide(double scalar)
        {
            return Times(1d / scalar);
        }



    }
}