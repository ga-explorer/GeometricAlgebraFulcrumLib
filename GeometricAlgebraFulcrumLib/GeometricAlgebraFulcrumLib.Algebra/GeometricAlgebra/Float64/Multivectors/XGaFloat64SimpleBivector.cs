using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors
{
    public sealed record XGaFloat64SimpleBivector :
        IXGaFloat64Element
    {
        private static XGaFloat64Vector RotateEuclidean(XGaFloat64Vector x, LinFloat64Angle angle, XGaFloat64Vector e1, XGaFloat64Vector e2)
        {
            var a1 = x.ESp(e1);
            var a2 = x.ESp(e2);

            var ct = angle.Cos();
            var st = angle.Sin();

            var a1P = a1 * ct - a2 * st;
            var a2P = a1 * st + a2 * ct;

            // x_perp = x - Proj_B(x)
            var px = e1.Times(a1).Add(e2.Times(a2));
            var xPerp = x.Subtract(px);

            return xPerp.Add(e1.Times(a1P)).Add(e2.Times(a2P));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64SimpleBivector Zero(XGaFloat64Processor processor)
        {
            return new XGaFloat64SimpleBivector(
                processor.VectorZero,
                processor.VectorZero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64SimpleBivector Basis(XGaFloat64Processor processor, int index1, int index2)
        {
            return new XGaFloat64SimpleBivector(
                processor.VectorTerm(index1),
                processor.VectorTerm(index2)
            );
        }

        /// <summary>
        /// Decompose a general bivector B into a simple bivector (two vectors) if B is simple.
        /// Uses existing GetVectorBasis() in XGaFloat64Bivector which returns orthonormal pair (e1, e2) s.t. e1 ∧ e2 = B up to scale.
        /// We then compute a scale s = &lt;B, e1∧e2&gt; / &lt;e1∧e2, e1∧e2&gt; and return (sqrt(|s|) e1, sqrt(|s|) e2) with sign encoded in e2 if needed.
        /// If B is near non-simple, throws.
        /// </summary>
        public static XGaFloat64SimpleBivector FromBivector(XGaFloat64Bivector bivector, double tolerance = Float64Utils.DefaultTolerance)
        {
            if (bivector.IsZero)
                throw new ArgumentException("Input bivector is zero");

            // Simplicity check
            if (!bivector.IsNearBlade(tolerance))
                throw new InvalidOperationException("Input bivector is not (near) simple");

            var (e1, e2) = bivector.GetVectorBasis();

            // Compute unit-simple blade U = e1 ∧ e2
            var e12 = e1.Op(e2);

            // s = <B, U> / <U, U>;
            var denom = e12.SpSquared();
            if (denom.IsNearZero(tolerance))
                throw new InvalidOperationException("Degenerate orthonormal basis for bivector decomposition");

            var numer = bivector.Sp(e12);
            var s = numer / denom;

            // Factor scale evenly across e1, e2: choose alpha = sqrt(|s|), and fold sign into second vector if s < 0
            var alpha = Math.Sqrt(Math.Abs(s));
            var v1 = e1.Times(alpha);
            var v2 = s >= 0.0 ? e2.Times(alpha) : e2.Times(-alpha);

            return new XGaFloat64SimpleBivector(v1, v2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64SimpleBivector operator *(XGaFloat64SimpleBivector mv1, IntegerSign mv2)
        {
            if (mv2.IsZero)
                return Zero(mv1.Processor);

            return mv2.IsPositive ? mv1 : mv1.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64SimpleBivector operator *(IntegerSign mv1, XGaFloat64SimpleBivector mv2)
        {
            if (mv1.IsZero)
                return Zero(mv2.Processor);

            return mv1.IsPositive ? mv2 : mv2.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64SimpleBivector operator *(XGaFloat64SimpleBivector mv1, int mv2)
        {
            return mv1.Times(
                mv2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64SimpleBivector operator *(int mv1, XGaFloat64SimpleBivector mv2)
        {
            return mv2.Times(
                mv1
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64SimpleBivector operator *(XGaFloat64SimpleBivector mv1, uint mv2)
        {
            return mv1.Times(
                mv2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64SimpleBivector operator *(uint mv1, XGaFloat64SimpleBivector mv2)
        {
            return mv2.Times(
                mv1
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64SimpleBivector operator *(XGaFloat64SimpleBivector mv1, long mv2)
        {
            return mv1.Times(
                mv2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64SimpleBivector operator *(long mv1, XGaFloat64SimpleBivector mv2)
        {
            return mv2.Times(
                mv1
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64SimpleBivector operator *(XGaFloat64SimpleBivector mv1, ulong mv2)
        {
            return mv1.Times(
                mv2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64SimpleBivector operator *(ulong mv1, XGaFloat64SimpleBivector mv2)
        {
            return mv2.Times(
                mv1
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64SimpleBivector operator *(XGaFloat64SimpleBivector mv1, float mv2)
        {
            return mv1.Times(
                mv2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64SimpleBivector operator *(float mv1, XGaFloat64SimpleBivector mv2)
        {
            return mv2.Times(
                mv1
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64SimpleBivector operator *(XGaFloat64SimpleBivector mv1, double mv2)
        {
            return mv1.Times(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64SimpleBivector operator *(double mv1, XGaFloat64SimpleBivector mv2)
        {
            return mv2.Times(mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64SimpleBivector operator *(XGaFloat64SimpleBivector mv1, XGaFloat64Scalar mv2)
        {
            return mv1.Times(mv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64SimpleBivector operator *(XGaFloat64Scalar mv1, XGaFloat64SimpleBivector mv2)
        {
            return mv2.Times(mv1.ScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64SimpleBivector operator /(XGaFloat64SimpleBivector mv1, IntegerSign mv2)
        {
            if (mv2.IsZero)
                throw new DivideByZeroException();

            return mv2.IsPositive ? mv1 : mv1.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64SimpleBivector operator /(XGaFloat64SimpleBivector mv1, int mv2)
        {
            return mv1.Divide(
                mv2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64SimpleBivector operator /(XGaFloat64SimpleBivector mv1, uint mv2)
        {
            return mv1.Divide(
                mv2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64SimpleBivector operator /(XGaFloat64SimpleBivector mv1, long mv2)
        {
            return mv1.Divide(
                mv2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64SimpleBivector operator /(XGaFloat64SimpleBivector mv1, ulong mv2)
        {
            return mv1.Divide(
                mv2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64SimpleBivector operator /(XGaFloat64SimpleBivector mv1, float mv2)
        {
            return mv1.Divide(
                mv2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64SimpleBivector operator /(XGaFloat64SimpleBivector mv1, double mv2)
        {
            return mv1.Divide(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64SimpleBivector operator /(XGaFloat64SimpleBivector mv1, XGaFloat64Scalar mv2)
        {
            return mv1.Divide(mv2.ScalarValue);
        }


        public XGaFloat64Vector Vector1 { get; }

        public XGaFloat64Vector Vector2 { get; }

        public XGaMetric Metric
            => Vector1.Metric;

        public XGaFloat64Processor Processor
            => Vector1.Processor;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64SimpleBivector(XGaFloat64Vector vector1, XGaFloat64Vector vector2)
        {
            Vector1 = vector1;
            Vector2 = vector2;

            Debug.Assert(IsValid());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return Vector1.IsValid() &&
                   Vector2.IsValid() &&
                   Vector1.HasSameMetric(Vector2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar EGpSquared()
        {
            return Vector1.ESp(Vector2).Square() - Vector1.ESpSquared() * Vector2.ESpSquared();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar ENormSquared()
        {
            return Vector1.ESpSquared() * Vector2.ESpSquared() - Vector1.ESp(Vector2).Square();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar ENorm()
        {
            return ENormSquared().SqrtOfAbs();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar GpSquared()
        {
            return Vector1.Sp(Vector2).Square() - Vector1.SpSquared() * Vector2.SpSquared();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar NormSquared()
        {
            return Vector1.SpSquared() * Vector2.SpSquared() - Vector1.Sp(Vector2).Square();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar Norm()
        {
            return NormSquared().SqrtOfAbs();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64SimpleBivector Negative()
        {
            return new XGaFloat64SimpleBivector(Vector2, Vector1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64SimpleBivector Reverse()
        {
            return new XGaFloat64SimpleBivector(Vector2, Vector1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64SimpleBivector GradeInvolution()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64SimpleBivector CliffordConjugate()
        {
            return new XGaFloat64SimpleBivector(Vector2, Vector1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64SimpleBivector Times(Float64Scalar scalar)
        {
            if (scalar.IsZero())
                throw new InvalidOperationException();

            if (scalar.IsPositive())
            {
                var scalarSqrt = scalar.Sqrt();

                return new XGaFloat64SimpleBivector(
                    Vector1.Times(scalarSqrt),
                    Vector2.Times(scalarSqrt)
                );
            }
            else
            {
                var scalarSqrt = scalar.Negative().Sqrt();

                return new XGaFloat64SimpleBivector(
                    Vector2.Divide(scalarSqrt),
                    Vector1.Divide(scalarSqrt)
                );
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64SimpleBivector Divide(Float64Scalar scalar)
        {
            if (scalar.IsZero())
                throw new InvalidOperationException();

            if (scalar.IsPositive())
            {
                var scalarSqrt = scalar.Sqrt();

                return new XGaFloat64SimpleBivector(
                    Vector1.Divide(scalarSqrt),
                    Vector2.Divide(scalarSqrt)
                );
            }
            else
            {
                var scalarSqrt = scalar.Negative().Sqrt();

                return new XGaFloat64SimpleBivector(
                    Vector2.Divide(scalarSqrt),
                    Vector1.Divide(scalarSqrt)
                );
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64SimpleBivector DivideByENorm()
        {
            return Divide(ENorm());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64SimpleBivector DivideByNorm()
        {
            return Divide(Norm());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64SimpleBivector DivideByENormSquared()
        {
            return Divide(ENormSquared());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64SimpleBivector DivideByNormSquared()
        {
            return Divide(NormSquared());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64SimpleBivector EInverse()
        {
            var norm2 = ENormSquared();

            if (norm2.IsZero())
                throw new DivideByZeroException();

            if (norm2.IsPositive())
            {
                var norm = norm2.Sqrt();

                return new XGaFloat64SimpleBivector(
                    Vector2.Divide(norm),
                    Vector1.Divide(norm)
                );
            }
            else
            {
                var norm = norm2.Negative().Sqrt();

                return new XGaFloat64SimpleBivector(
                    Vector1.Divide(norm),
                    Vector2.Divide(norm)
                );
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64SimpleBivector Inverse()
        {
            var norm2 = NormSquared();

            if (norm2.IsZero())
                throw new DivideByZeroException();

            if (norm2.IsPositive())
            {
                var norm = norm2.Sqrt();

                return new XGaFloat64SimpleBivector(
                    Vector2.Divide(norm),
                    Vector1.Divide(norm)
                );
            }
            else
            {
                var norm = norm2.Negative().Sqrt();

                return new XGaFloat64SimpleBivector(
                    Vector1.Divide(norm),
                    Vector2.Divide(norm)
                );
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ESp(XGaFloat64SimpleBivector b)
        {
            return Vector1.ESp(b.Vector1) * Vector2.ESp(b.Vector2) -
                   Vector1.ESp(b.Vector2) * Vector2.ESp(b.Vector1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Sp(XGaFloat64SimpleBivector b)
        {
            return Vector1.Sp(b.Vector1) * Vector2.Sp(b.Vector2) -
                   Vector1.Sp(b.Vector2) * Vector2.Sp(b.Vector1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Vector ERcp(XGaFloat64Vector u)
        {
            return Vector1.Times(Vector2.ESp(u)) - Vector2.Times(Vector1.ESp(u));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Vector Rcp(XGaFloat64Vector u)
        {
            return Vector1.Times(Vector2.Sp(u)) - Vector2.Times(Vector1.Sp(u));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Vector EFdp(XGaFloat64Vector u)
        {
            return ERcp(u);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Vector Fdp(XGaFloat64Vector u)
        {
            return Rcp(u);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Vector EHip(XGaFloat64Vector u)
        {
            return ERcp(u);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Vector Hip(XGaFloat64Vector u)
        {
            return Rcp(u);
        }


        /// <summary>
        /// Return the normalized orthonormal basis (e1, e2) spanning the same
        /// 2D subspace as (v1, v2). The orientation matches v1 ∧ v2: we set
        /// e1 = unit(v1), e2 = unit( (I - e1e1)v2 ).
        /// If (v1, v2) are linearly dependent, throws an error.
        /// </summary>
        public Pair<XGaFloat64Vector> GetOrthonormalBasisEuclidean()
        {
            var n1 = Vector1.ENorm();

            if (n1.IsZero())
                throw new InvalidOperationException("Vector1 is zero; 2-blade is degenerate");

            var e1 = Vector1.Divide(n1);

            // Orthogonalize v2 against e1 using left contraction projection
            // in current metric:
            // v2_parallel = (v2 ⋅ e1) e1, v2_perp = v2 - v2_parallel
            // In GA library we can compute scalars via Sp.
            // Use e1.Sp(v2) == v2.Sp(e1) for symmetric metrics.
            var s12 = e1.ESp(Vector2); // scalar
            var v2Perp = Vector2 - e1.Times(s12);

            var n2 = v2Perp.ENorm();
            if (n2.IsZero())
                throw new InvalidOperationException("Vector1 and Vector2 are linearly dependent; 2-blade is degenerate");

            var e2 = v2Perp.Divide(n2);

            return new Pair<XGaFloat64Vector>(e1, e2);
        }

        /// <summary>
        /// Return the normalized orthonormal basis (e1, e2) spanning the same
        /// 2D subspace as (v1, v2). The orientation matches v1 ∧ v2: we set
        /// e1 = unit(v1), e2 = unit( (I - e1e1)v2 ).
        /// If (v1, v2) are linearly dependent, throws an error.
        /// </summary>
        public Pair<XGaFloat64Vector> GetOrthonormalBasis()
        {
            var n1 = Vector1.Norm();

            if (n1.IsZero())
                throw new InvalidOperationException("Vector1 is zero; 2-blade is degenerate");

            var e1 = Vector1.Divide(n1);

            // Orthogonalize v2 against e1 using left contraction projection
            // in current metric:
            // v2_parallel = (v2 ⋅ e1) e1, v2_perp = v2 - v2_parallel
            // In GA library we can compute scalars via Sp.
            // Use e1.Sp(v2) == v2.Sp(e1) for symmetric metrics.
            var s12 = e1.Sp(Vector2); // scalar
            var v2Perp = Vector2 - e1.Times(s12);

            var n2 = v2Perp.Norm();
            if (n2.IsZero())
                throw new InvalidOperationException("Vector1 and Vector2 are linearly dependent; 2-blade is degenerate");

            var e2 = v2Perp.Divide(n2);

            return new Pair<XGaFloat64Vector>(e1, e2);
        }

        /// <summary>
        /// Projection of an external vector x on the 2-blade span{v1, v2}.
        /// Uses the orthonormal basis (e1, e2) of the blade:
        /// Proj_B(x) = (x·e1) e1 + (x·e2) e2.
        /// Works for any signature since Sp() uses the current processor metric.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Vector Project(XGaFloat64Vector x)
        {
            if (!Metric.HasSameSignature(x.Metric))
                throw new ArgumentException("x must share the same metric/processor");

            var (e1, e2) = GetOrthonormalBasis();

            var a1 = x.Sp(e1);
            var a2 = x.Sp(e2);

            return e1.Times(a1).Add(e2.Times(a2));
        }

        /// <summary>
        /// Reflection of a vector x in the 2-blade span{v1, v2}.
        /// If (e1, e2) is an orthonormal basis, the reflection across the plane flips the component
        /// perpendicular to the plane and keeps in-plane components: R(x) = 2Proj_B(x) - x.
        /// This formula is signature-agnostic as it relies on projection via Sp() with the given metric.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Vector Reflect(XGaFloat64Vector x)
        {
            var px = Project(x);

            return px.Times(2.0).Subtract(x);
        }

        /// <summary>
        /// Reflection of a simple bivector C = a ∧ b in the 2-blade using linearity:
        /// Reflect(C) = Reflect(a) ∧ Reflect(b), with Reflect(v) as for vectors.
        /// This preserves simplicity and orientation where appropriate.
        /// </summary>
        public XGaFloat64SimpleBivector Reflect(XGaFloat64SimpleBivector blade)
        {
            if (!Metric.HasSameSignature(blade.Metric))
                throw new ArgumentException("Blade being reflected must share the same metric/processor");

            var (e1, e2) = GetOrthonormalBasis();

            var a1 = blade.Vector1.Sp(e1);
            var a2 = blade.Vector1.Sp(e2);
            var px1 = e1.Times(a1).Add(e2.Times(a2));
            var aR = px1.Times(2.0).Subtract(blade.Vector1);

            if (!Vector1.HasSameMetric(blade.Vector2))
                throw new ArgumentException("x must share the same metric/processor");

            var a3 = blade.Vector2.Sp(e1);
            var a4 = blade.Vector2.Sp(e2);
            var px2 = e1.Times(a3).Add(e2.Times(a4));
            var bR = px2.Times(2.0).Subtract(blade.Vector2);

            return new XGaFloat64SimpleBivector(aR, bR);
        }

        /// <summary>
        /// Euclidean rotation by angle t of a vector x inside the blade span{v1, v2}.
        /// x = x_parallel + x_perp, where x_parallel ∈ span{e1,e2}, x_perp ⟂ plane.
        /// R_t(x) = x_perp + ( (x·e1)e1 + (x·e2)e2 ) rotated in-plane:
        /// Let a1 = x·e1, a2 = x·e2, then new coefficients are
        /// a1' = a1 cos t - a2 sin t, a2' = a1 sin t + a2 cos t.
        /// Non-Euclidean signatures are not supported here; caller must ensure Processor.IsEuclidean.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Vector RotateEuclidean(XGaFloat64Vector x, LinFloat64Angle angle)
        {
            var (e1, e2) =
                GetOrthonormalBasisEuclidean();

            return RotateEuclidean(x, angle, e1, e2);
        }

        /// <summary>
        /// Euclidean rotation by angle t of a simple bivector C = a ∧ b inside the plane span{v1, v2}.
        /// Defined by linearity: R_t(C) = R_t(a) ∧ R_t(b).
        /// Non-Euclidean signatures are not supported here; caller must ensure Processor.IsEuclidean.
        /// </summary>
        public XGaFloat64SimpleBivector RotateEuclidean(XGaFloat64SimpleBivector blade, LinFloat64Angle angle)
        {
            var (e1, e2) =
                GetOrthonormalBasisEuclidean();

            var aR = RotateEuclidean(blade.Vector1, angle, e1, e2);
            var bR = RotateEuclidean(blade.Vector2, angle, e1, e2);

            return new XGaFloat64SimpleBivector(aR, bR);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64SimpleBivector RemoveSmallTerms(double tolerance = Float64Utils.DefaultTolerance)
        {
            return new XGaFloat64SimpleBivector(
                Vector1.RemoveSmallTerms(tolerance),
                Vector2.RemoveSmallTerms(tolerance)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Bivector ToBivector()
        {
            return Vector1.Op(Vector2);
        }


        public (XGaFloat64Scalar scalarPart, XGaFloat64SimpleBivector bivectorPart) Exp(double tolerance = Float64Utils.DefaultTolerance)
        {
            var bv2 =
                GpSquared();

            var bladeSignature = bv2.ScalarValue;

            if (bladeSignature.IsNearZero(tolerance))
                return (Processor.ScalarOne, this);

            if (bladeSignature < 0)
            {
                var alpha = (-bladeSignature).Sqrt();

                return (
                    Processor.Scalar(alpha.Cos()),
                    Times(alpha.Sin() / alpha)
                );
            }
            else
            {
                var alpha = bladeSignature.Sqrt();

                return (
                    Processor.Scalar(alpha.Cosh()),
                    Times(alpha.Sinh() / alpha)
                );
            }
        }

        /// <summary>
        /// Create a pure rotor multivector from a 2-blade
        /// </summary>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector ExpToMultivector(double tolerance = Float64Utils.DefaultTolerance)
        {
            var (scalarPart, bivectorPart) = Exp(tolerance);

            return bivectorPart.ToBivector().Add(scalarPart);
        }
    }
}
