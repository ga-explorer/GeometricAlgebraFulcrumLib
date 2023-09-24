using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Samples.MetaProgramming
{
    public sealed class CGaScalar4D
    {
        public static CGaScalar4D Zero { get; } = new CGaScalar4D();

        public static CGaScalar4D E { get; } = new CGaScalar4D() { Scalar = Float64Scalar.One };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaScalar4D operator +(CGaScalar4D mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaScalar4D operator -(CGaScalar4D mv)
        {
            return mv.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaScalar4D operator *(CGaScalar4D mv1, double mv2)
        {
            return mv1.Times(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaScalar4D operator *(double mv1, CGaScalar4D mv2)
        {
            return mv2.Times(mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaScalar4D operator /(CGaScalar4D mv1, double mv2)
        {
            return mv1.Times(1d / mv2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaScalar4D operator +(CGaScalar4D mv1, CGaScalar4D mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaScalar4D operator -(CGaScalar4D mv1, CGaScalar4D mv2)
        {
            return mv1.Subtract(mv2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator +(CGaScalar4D mv1, CGaVector4D mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator -(CGaScalar4D mv1, CGaVector4D mv2)
        {
            return mv1.Subtract(mv2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator +(CGaScalar4D mv1, CGaBivector4D mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator -(CGaScalar4D mv1, CGaBivector4D mv2)
        {
            return mv1.Subtract(mv2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator +(CGaScalar4D mv1, CGaTrivector4D mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator -(CGaScalar4D mv1, CGaTrivector4D mv2)
        {
            return mv1.Subtract(mv2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator +(CGaScalar4D mv1, CGaMultivector4D mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator -(CGaScalar4D mv1, CGaMultivector4D mv2)
        {
            return mv1.Subtract(mv2);
        }

        public Float64Scalar Scalar { get; init; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D()
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return
                Scalar.IsValid();
        }

        private bool? _isZero;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero()
        {
            _isZero ??=
                Scalar.IsZero();

            return _isZero.Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(double epsilon = 1e-12d)
        {
            return Norm().IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[] GetMultivectorArray()
        {
            var scalarArray = new double[16];

            scalarArray[0] = Scalar;

            return scalarArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[] GetKVectorArray()
        {
            return new double[]
            {
            Scalar
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar NormSquared()
        {
            return Scalar * Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar Norm()
        {
            return NormSquared().SqrtOfAbs();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Times(double mv2)
        {
            return new CGaScalar4D()
            {
                Scalar = Scalar * mv2
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Divide(double mv2)
        {
            return Times(1d / mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D DivideByNorm()
        {
            return Times(1d / Norm());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D DivideByNormSquared()
        {
            return Times(1d / NormSquared());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Negative()
        {
            return new CGaScalar4D()
            {
                Scalar = -Scalar
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Reverse()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D GradeInvolution()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D CliffordConjugate()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Inverse()
        {
            return Times(1d / NormSquared());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D InverseTimes(double mv2)
        {
            return Times(mv2 / NormSquared());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D PseudoInverse()
        {
            var conjugate = Conjugate();

            return conjugate.Times(
                1d / conjugate.Sp(this).Scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D PseudoInverseTimes(double mv2)
        {
            var conjugate = Conjugate();

            return conjugate.Times(
                mv2 / conjugate.Sp(this).Scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Conjugate()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGa4Vector4D Dual()
        {
            return new CGa4Vector4D()
            {
                Scalar1234 = -Scalar
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGa4Vector4D UnDual()
        {
            return new CGa4Vector4D()
            {
                Scalar1234 = Scalar
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Dual(CGaScalar4D kv2)
        {
            return Lcp(kv2.Inverse());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D UnDual(CGaScalar4D kv2)
        {
            return Lcp(kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D Dual(CGaVector4D kv2)
        {
            return Lcp(kv2.Inverse());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D UnDual(CGaVector4D kv2)
        {
            return Lcp(kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D Dual(CGaBivector4D kv2)
        {
            return Lcp(kv2.Inverse());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D UnDual(CGaBivector4D kv2)
        {
            return Lcp(kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D Dual(CGaTrivector4D kv2)
        {
            return Lcp(kv2.Inverse());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D UnDual(CGaTrivector4D kv2)
        {
            return Lcp(kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGa4Vector4D Dual(CGa4Vector4D kv2)
        {
            return Lcp(kv2.Inverse());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGa4Vector4D UnDual(CGa4Vector4D kv2)
        {
            return Lcp(kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Add(CGaScalar4D mv2)
        {
            if (mv2.IsZero()) return this;
            if (IsZero()) return mv2;

            return new CGaScalar4D()
            {
                Scalar = Scalar + mv2.Scalar
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Add(CGaVector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2);

            return CGaMultivector4D.Create(
                this,
                mv2,
                CGaBivector4D.Zero,
                CGaTrivector4D.Zero,
                CGa4Vector4D.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Add(CGaBivector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2);

            return CGaMultivector4D.Create(
                this,
                CGaVector4D.Zero,
                mv2,
                CGaTrivector4D.Zero,
                CGa4Vector4D.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Add(CGaTrivector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2);

            return CGaMultivector4D.Create(
                this,
                CGaVector4D.Zero,
                CGaBivector4D.Zero,
                mv2,
                CGa4Vector4D.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Add(CGa4Vector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2);

            return CGaMultivector4D.Create(
                this,
                CGaVector4D.Zero,
                CGaBivector4D.Zero,
                CGaTrivector4D.Zero,
                mv2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Add(CGaMultivector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return mv2;

            return CGaMultivector4D.Create(
                Add(mv2.KVector0),
                mv2.KVector1,
                mv2.KVector2,
                mv2.KVector3,
                mv2.KVector4
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Subtract(CGaScalar4D mv2)
        {
            if (mv2.IsZero()) return this;
            if (IsZero()) return mv2.Negative();

            return new CGaScalar4D()
            {
                Scalar = Scalar - mv2.Scalar
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Subtract(CGaVector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2.Negative());

            return CGaMultivector4D.Create(
                this,
                mv2.Negative(),
                CGaBivector4D.Zero,
                CGaTrivector4D.Zero,
                CGa4Vector4D.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Subtract(CGaBivector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2.Negative());

            return CGaMultivector4D.Create(
                this,
                CGaVector4D.Zero,
                mv2.Negative(),
                CGaTrivector4D.Zero,
                CGa4Vector4D.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Subtract(CGaTrivector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2.Negative());

            return CGaMultivector4D.Create(
                this,
                CGaVector4D.Zero,
                CGaBivector4D.Zero,
                mv2.Negative(),
                CGa4Vector4D.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Subtract(CGa4Vector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2.Negative());

            return CGaMultivector4D.Create(
                this,
                CGaVector4D.Zero,
                CGaBivector4D.Zero,
                CGaTrivector4D.Zero,
                mv2.Negative()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Subtract(CGaMultivector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return mv2.Negative();

            return CGaMultivector4D.Create(
                Subtract(mv2.KVector0),
                mv2.KVector1.Negative(),
                mv2.KVector2.Negative(),
                mv2.KVector3.Negative(),
                mv2.KVector4.Negative()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Sp(CGaScalar4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaScalar4D.Zero;

            return new CGaScalar4D()
            {
                Scalar = Scalar * mv2.Scalar
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Sp(CGaVector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Sp(CGaBivector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Sp(CGaTrivector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Sp(CGa4Vector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void SpUpdateMultivectorArray(CGaScalar4D kv2, double[] scalarArray)
        {
            scalarArray[0] += Scalar * kv2.Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Op(CGaScalar4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaScalar4D.Zero;

            return new CGaScalar4D()
            {
                Scalar = Scalar * mv2.Scalar
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D Op(CGaVector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaVector4D.Zero;

            return new CGaVector4D()
            {
                Scalar1 = Scalar * mv2.Scalar1,
                Scalar2 = Scalar * mv2.Scalar2,
                Scalar3 = Scalar * mv2.Scalar3,
                Scalar4 = Scalar * mv2.Scalar4
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D Op(CGaBivector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaBivector4D.Zero;

            return new CGaBivector4D()
            {
                Scalar12 = Scalar * mv2.Scalar12,
                Scalar13 = Scalar * mv2.Scalar13,
                Scalar23 = Scalar * mv2.Scalar23,
                Scalar14 = Scalar * mv2.Scalar14,
                Scalar24 = Scalar * mv2.Scalar24,
                Scalar34 = Scalar * mv2.Scalar34
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D Op(CGaTrivector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaTrivector4D.Zero;

            return new CGaTrivector4D()
            {
                Scalar123 = Scalar * mv2.Scalar123,
                Scalar124 = Scalar * mv2.Scalar124,
                Scalar134 = Scalar * mv2.Scalar134,
                Scalar234 = Scalar * mv2.Scalar234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGa4Vector4D Op(CGa4Vector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGa4Vector4D.Zero;

            return new CGa4Vector4D()
            {
                Scalar1234 = Scalar * mv2.Scalar1234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void OpUpdateMultivectorArray(CGaScalar4D kv2, double[] scalarArray)
        {
            scalarArray[0] += Scalar * kv2.Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void OpUpdateMultivectorArray(CGaVector4D kv2, double[] scalarArray)
        {
            scalarArray[1] += Scalar * kv2.Scalar1;
            scalarArray[2] += Scalar * kv2.Scalar2;
            scalarArray[4] += Scalar * kv2.Scalar3;
            scalarArray[8] += Scalar * kv2.Scalar4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void OpUpdateMultivectorArray(CGaBivector4D kv2, double[] scalarArray)
        {
            scalarArray[3] += Scalar * kv2.Scalar12;
            scalarArray[5] += Scalar * kv2.Scalar13;
            scalarArray[6] += Scalar * kv2.Scalar23;
            scalarArray[9] += Scalar * kv2.Scalar14;
            scalarArray[10] += Scalar * kv2.Scalar24;
            scalarArray[12] += Scalar * kv2.Scalar34;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void OpUpdateMultivectorArray(CGaTrivector4D kv2, double[] scalarArray)
        {
            scalarArray[7] += Scalar * kv2.Scalar123;
            scalarArray[11] += Scalar * kv2.Scalar124;
            scalarArray[13] += Scalar * kv2.Scalar134;
            scalarArray[14] += Scalar * kv2.Scalar234;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void OpUpdateMultivectorArray(CGa4Vector4D kv2, double[] scalarArray)
        {
            scalarArray[15] += Scalar * kv2.Scalar1234;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Lcp(CGaScalar4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaScalar4D.Zero;

            return new CGaScalar4D()
            {
                Scalar = Scalar * mv2.Scalar
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D Lcp(CGaVector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaVector4D.Zero;

            return new CGaVector4D()
            {
                Scalar1 = Scalar * mv2.Scalar1,
                Scalar2 = Scalar * mv2.Scalar2,
                Scalar3 = Scalar * mv2.Scalar3,
                Scalar4 = Scalar * mv2.Scalar4
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D Lcp(CGaBivector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaBivector4D.Zero;

            return new CGaBivector4D()
            {
                Scalar12 = Scalar * mv2.Scalar12,
                Scalar13 = Scalar * mv2.Scalar13,
                Scalar23 = Scalar * mv2.Scalar23,
                Scalar14 = Scalar * mv2.Scalar14,
                Scalar24 = Scalar * mv2.Scalar24,
                Scalar34 = Scalar * mv2.Scalar34
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D Lcp(CGaTrivector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaTrivector4D.Zero;

            return new CGaTrivector4D()
            {
                Scalar123 = Scalar * mv2.Scalar123,
                Scalar124 = Scalar * mv2.Scalar124,
                Scalar134 = Scalar * mv2.Scalar134,
                Scalar234 = Scalar * mv2.Scalar234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGa4Vector4D Lcp(CGa4Vector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGa4Vector4D.Zero;

            return new CGa4Vector4D()
            {
                Scalar1234 = Scalar * mv2.Scalar1234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void LcpUpdateMultivectorArray(CGaScalar4D kv2, double[] scalarArray)
        {
            scalarArray[0] += Scalar * kv2.Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void LcpUpdateMultivectorArray(CGaVector4D kv2, double[] scalarArray)
        {
            scalarArray[1] += Scalar * kv2.Scalar1;
            scalarArray[2] += Scalar * kv2.Scalar2;
            scalarArray[4] += Scalar * kv2.Scalar3;
            scalarArray[8] += Scalar * kv2.Scalar4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void LcpUpdateMultivectorArray(CGaBivector4D kv2, double[] scalarArray)
        {
            scalarArray[3] += Scalar * kv2.Scalar12;
            scalarArray[5] += Scalar * kv2.Scalar13;
            scalarArray[6] += Scalar * kv2.Scalar23;
            scalarArray[9] += Scalar * kv2.Scalar14;
            scalarArray[10] += Scalar * kv2.Scalar24;
            scalarArray[12] += Scalar * kv2.Scalar34;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void LcpUpdateMultivectorArray(CGaTrivector4D kv2, double[] scalarArray)
        {
            scalarArray[7] += Scalar * kv2.Scalar123;
            scalarArray[11] += Scalar * kv2.Scalar124;
            scalarArray[13] += Scalar * kv2.Scalar134;
            scalarArray[14] += Scalar * kv2.Scalar234;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void LcpUpdateMultivectorArray(CGa4Vector4D kv2, double[] scalarArray)
        {
            scalarArray[15] += Scalar * kv2.Scalar1234;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Rcp(CGaScalar4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaScalar4D.Zero;

            return new CGaScalar4D()
            {
                Scalar = Scalar * mv2.Scalar
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Rcp(CGaVector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Rcp(CGaBivector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Rcp(CGaTrivector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Rcp(CGa4Vector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void RcpUpdateMultivectorArray(CGaScalar4D kv2, double[] scalarArray)
        {
            scalarArray[0] += Scalar * kv2.Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Fdp(CGaScalar4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaScalar4D.Zero;

            return new CGaScalar4D()
            {
                Scalar = Scalar * mv2.Scalar
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D Fdp(CGaVector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaVector4D.Zero;

            return new CGaVector4D()
            {
                Scalar1 = Scalar * mv2.Scalar1,
                Scalar2 = Scalar * mv2.Scalar2,
                Scalar3 = Scalar * mv2.Scalar3,
                Scalar4 = Scalar * mv2.Scalar4
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D Fdp(CGaBivector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaBivector4D.Zero;

            return new CGaBivector4D()
            {
                Scalar12 = Scalar * mv2.Scalar12,
                Scalar13 = Scalar * mv2.Scalar13,
                Scalar23 = Scalar * mv2.Scalar23,
                Scalar14 = Scalar * mv2.Scalar14,
                Scalar24 = Scalar * mv2.Scalar24,
                Scalar34 = Scalar * mv2.Scalar34
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D Fdp(CGaTrivector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaTrivector4D.Zero;

            return new CGaTrivector4D()
            {
                Scalar123 = Scalar * mv2.Scalar123,
                Scalar124 = Scalar * mv2.Scalar124,
                Scalar134 = Scalar * mv2.Scalar134,
                Scalar234 = Scalar * mv2.Scalar234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGa4Vector4D Fdp(CGa4Vector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGa4Vector4D.Zero;

            return new CGa4Vector4D()
            {
                Scalar1234 = Scalar * mv2.Scalar1234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void FdpUpdateMultivectorArray(CGaScalar4D kv2, double[] scalarArray)
        {
            scalarArray[0] += Scalar * kv2.Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void FdpUpdateMultivectorArray(CGaVector4D kv2, double[] scalarArray)
        {
            scalarArray[1] += Scalar * kv2.Scalar1;
            scalarArray[2] += Scalar * kv2.Scalar2;
            scalarArray[4] += Scalar * kv2.Scalar3;
            scalarArray[8] += Scalar * kv2.Scalar4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void FdpUpdateMultivectorArray(CGaBivector4D kv2, double[] scalarArray)
        {
            scalarArray[3] += Scalar * kv2.Scalar12;
            scalarArray[5] += Scalar * kv2.Scalar13;
            scalarArray[6] += Scalar * kv2.Scalar23;
            scalarArray[9] += Scalar * kv2.Scalar14;
            scalarArray[10] += Scalar * kv2.Scalar24;
            scalarArray[12] += Scalar * kv2.Scalar34;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void FdpUpdateMultivectorArray(CGaTrivector4D kv2, double[] scalarArray)
        {
            scalarArray[7] += Scalar * kv2.Scalar123;
            scalarArray[11] += Scalar * kv2.Scalar124;
            scalarArray[13] += Scalar * kv2.Scalar134;
            scalarArray[14] += Scalar * kv2.Scalar234;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void FdpUpdateMultivectorArray(CGa4Vector4D kv2, double[] scalarArray)
        {
            scalarArray[15] += Scalar * kv2.Scalar1234;
        }

        public CGaMultivector4D Gp(CGaScalar4D mv2)
        {
            var scalarArray = new double[16];

            if (!IsZero() && !mv2.IsZero()) GpUpdateMultivectorArray(mv2, scalarArray);

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Gp(CGaVector4D mv2)
        {
            var scalarArray = new double[16];

            if (!IsZero() && !mv2.IsZero()) GpUpdateMultivectorArray(mv2, scalarArray);

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Gp(CGaBivector4D mv2)
        {
            var scalarArray = new double[16];

            if (!IsZero() && !mv2.IsZero()) GpUpdateMultivectorArray(mv2, scalarArray);

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Gp(CGaTrivector4D mv2)
        {
            var scalarArray = new double[16];

            if (!IsZero() && !mv2.IsZero()) GpUpdateMultivectorArray(mv2, scalarArray);

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Gp(CGa4Vector4D mv2)
        {
            var scalarArray = new double[16];

            if (!IsZero() && !mv2.IsZero()) GpUpdateMultivectorArray(mv2, scalarArray);

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Gp(CGaMultivector4D mv2)
        {
            var scalarArray = new double[16];

            if (!IsZero())
            {
                if (!mv2.KVector0.IsZero()) GpUpdateMultivectorArray(mv2.KVector0, scalarArray);
                if (!mv2.KVector1.IsZero()) GpUpdateMultivectorArray(mv2.KVector1, scalarArray);
                if (!mv2.KVector2.IsZero()) GpUpdateMultivectorArray(mv2.KVector2, scalarArray);
                if (!mv2.KVector3.IsZero()) GpUpdateMultivectorArray(mv2.KVector3, scalarArray);
                if (!mv2.KVector4.IsZero()) GpUpdateMultivectorArray(mv2.KVector4, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void GpUpdateMultivectorArray(CGaScalar4D kv2, double[] scalarArray)
        {
            scalarArray[0] += Scalar * kv2.Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void GpUpdateMultivectorArray(CGaVector4D kv2, double[] scalarArray)
        {
            scalarArray[1] += Scalar * kv2.Scalar1;
            scalarArray[2] += Scalar * kv2.Scalar2;
            scalarArray[4] += Scalar * kv2.Scalar3;
            scalarArray[8] += Scalar * kv2.Scalar4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void GpUpdateMultivectorArray(CGaBivector4D kv2, double[] scalarArray)
        {
            scalarArray[3] += Scalar * kv2.Scalar12;
            scalarArray[5] += Scalar * kv2.Scalar13;
            scalarArray[6] += Scalar * kv2.Scalar23;
            scalarArray[9] += Scalar * kv2.Scalar14;
            scalarArray[10] += Scalar * kv2.Scalar24;
            scalarArray[12] += Scalar * kv2.Scalar34;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void GpUpdateMultivectorArray(CGaTrivector4D kv2, double[] scalarArray)
        {
            scalarArray[7] += Scalar * kv2.Scalar123;
            scalarArray[11] += Scalar * kv2.Scalar124;
            scalarArray[13] += Scalar * kv2.Scalar134;
            scalarArray[14] += Scalar * kv2.Scalar234;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void GpUpdateMultivectorArray(CGa4Vector4D kv2, double[] scalarArray)
        {
            scalarArray[15] += Scalar * kv2.Scalar1234;
        }

    }

    public sealed class CGaVector4D
    {
        public static CGaVector4D Zero { get; } = new CGaVector4D();

        public static CGaVector4D E1 { get; } = new CGaVector4D() { Scalar1 = Float64Scalar.One };

        public static CGaVector4D E2 { get; } = new CGaVector4D() { Scalar2 = Float64Scalar.One };

        public static CGaVector4D E3 { get; } = new CGaVector4D() { Scalar3 = Float64Scalar.One };

        public static CGaVector4D E4 { get; } = new CGaVector4D() { Scalar4 = Float64Scalar.One };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaVector4D operator +(CGaVector4D mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaVector4D operator -(CGaVector4D mv)
        {
            return mv.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaVector4D operator *(CGaVector4D mv1, double mv2)
        {
            return mv1.Times(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaVector4D operator *(double mv1, CGaVector4D mv2)
        {
            return mv2.Times(mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaVector4D operator /(CGaVector4D mv1, double mv2)
        {
            return mv1.Times(1d / mv2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator +(CGaVector4D mv1, CGaScalar4D mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator -(CGaVector4D mv1, CGaScalar4D mv2)
        {
            return mv1.Subtract(mv2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaVector4D operator +(CGaVector4D mv1, CGaVector4D mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaVector4D operator -(CGaVector4D mv1, CGaVector4D mv2)
        {
            return mv1.Subtract(mv2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator +(CGaVector4D mv1, CGaBivector4D mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator -(CGaVector4D mv1, CGaBivector4D mv2)
        {
            return mv1.Subtract(mv2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator +(CGaVector4D mv1, CGaTrivector4D mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator -(CGaVector4D mv1, CGaTrivector4D mv2)
        {
            return mv1.Subtract(mv2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator +(CGaVector4D mv1, CGaMultivector4D mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator -(CGaVector4D mv1, CGaMultivector4D mv2)
        {
            return mv1.Subtract(mv2);
        }

        public Float64Scalar Scalar1 { get; init; }

        public Float64Scalar Scalar2 { get; init; }

        public Float64Scalar Scalar3 { get; init; }

        public Float64Scalar Scalar4 { get; init; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D()
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return
                Scalar1.IsValid() &&
                Scalar2.IsValid() &&
                Scalar3.IsValid() &&
                Scalar4.IsValid();
        }

        private bool? _isZero;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero()
        {
            _isZero ??=
                Scalar1.IsZero() &&
                Scalar2.IsZero() &&
                Scalar3.IsZero() &&
                Scalar4.IsZero();

            return _isZero.Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(double epsilon = 1e-12d)
        {
            return Norm().IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[] GetMultivectorArray()
        {
            var scalarArray = new double[16];

            scalarArray[1] = Scalar1;
            scalarArray[2] = Scalar2;
            scalarArray[4] = Scalar3;
            scalarArray[8] = Scalar4;

            return scalarArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[] GetKVectorArray()
        {
            return new double[]
            {
            Scalar1,
            Scalar2,
            Scalar3,
            Scalar4
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar NormSquared()
        {
            return -Scalar1 * Scalar1 + Scalar2 * Scalar2 + Scalar3 * Scalar3 + Scalar4 * Scalar4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar Norm()
        {
            return NormSquared().SqrtOfAbs();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D Times(double mv2)
        {
            return new CGaVector4D()
            {
                Scalar1 = Scalar1 * mv2,
                Scalar2 = Scalar2 * mv2,
                Scalar3 = Scalar3 * mv2,
                Scalar4 = Scalar4 * mv2
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D Divide(double mv2)
        {
            return Times(1d / mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D DivideByNorm()
        {
            return Times(1d / Norm());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D DivideByNormSquared()
        {
            return Times(1d / NormSquared());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D Negative()
        {
            return new CGaVector4D()
            {
                Scalar1 = -Scalar1,
                Scalar2 = -Scalar2,
                Scalar3 = -Scalar3,
                Scalar4 = -Scalar4
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D Reverse()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D GradeInvolution()
        {
            return Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D CliffordConjugate()
        {
            return Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D Inverse()
        {
            return Times(1d / NormSquared());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D InverseTimes(double mv2)
        {
            return Times(mv2 / NormSquared());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D PseudoInverse()
        {
            var conjugate = Conjugate();

            return conjugate.Times(
                1d / conjugate.Sp(this).Scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D PseudoInverseTimes(double mv2)
        {
            var conjugate = Conjugate();

            return conjugate.Times(
                mv2 / conjugate.Sp(this).Scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D Conjugate()
        {
            return new CGaVector4D()
            {
                Scalar1 = -Scalar1,
                Scalar2 = Scalar2,
                Scalar3 = Scalar3,
                Scalar4 = Scalar4
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D Dual()
        {
            return new CGaTrivector4D()
            {
                Scalar123 = Scalar4,
                Scalar124 = -Scalar3,
                Scalar134 = Scalar2,
                Scalar234 = Scalar1
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D UnDual()
        {
            return new CGaTrivector4D()
            {
                Scalar123 = -Scalar4,
                Scalar124 = Scalar3,
                Scalar134 = -Scalar2,
                Scalar234 = -Scalar1
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Dual(CGaVector4D kv2)
        {
            return Lcp(kv2.Inverse());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D UnDual(CGaVector4D kv2)
        {
            return Lcp(kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D Dual(CGaBivector4D kv2)
        {
            return Lcp(kv2.Inverse());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D UnDual(CGaBivector4D kv2)
        {
            return Lcp(kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D Dual(CGaTrivector4D kv2)
        {
            return Lcp(kv2.Inverse());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D UnDual(CGaTrivector4D kv2)
        {
            return Lcp(kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D Dual(CGa4Vector4D kv2)
        {
            return Lcp(kv2.Inverse());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D UnDual(CGa4Vector4D kv2)
        {
            return Lcp(kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Add(CGaScalar4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2);

            return CGaMultivector4D.Create(
                mv2,
                this,
                CGaBivector4D.Zero,
                CGaTrivector4D.Zero,
                CGa4Vector4D.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D Add(CGaVector4D mv2)
        {
            if (mv2.IsZero()) return this;
            if (IsZero()) return mv2;

            return new CGaVector4D()
            {
                Scalar1 = Scalar1 + mv2.Scalar1,
                Scalar2 = Scalar2 + mv2.Scalar2,
                Scalar3 = Scalar3 + mv2.Scalar3,
                Scalar4 = Scalar4 + mv2.Scalar4
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Add(CGaBivector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2);

            return CGaMultivector4D.Create(
                CGaScalar4D.Zero,
                this,
                mv2,
                CGaTrivector4D.Zero,
                CGa4Vector4D.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Add(CGaTrivector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2);

            return CGaMultivector4D.Create(
                CGaScalar4D.Zero,
                this,
                CGaBivector4D.Zero,
                mv2,
                CGa4Vector4D.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Add(CGa4Vector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2);

            return CGaMultivector4D.Create(
                CGaScalar4D.Zero,
                this,
                CGaBivector4D.Zero,
                CGaTrivector4D.Zero,
                mv2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Add(CGaMultivector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return mv2;

            return CGaMultivector4D.Create(
                mv2.KVector0,
                Add(mv2.KVector1),
                mv2.KVector2,
                mv2.KVector3,
                mv2.KVector4
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Subtract(CGaScalar4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2.Negative());

            return CGaMultivector4D.Create(
                mv2.Negative(),
                this,
                CGaBivector4D.Zero,
                CGaTrivector4D.Zero,
                CGa4Vector4D.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D Subtract(CGaVector4D mv2)
        {
            if (mv2.IsZero()) return this;
            if (IsZero()) return mv2.Negative();

            return new CGaVector4D()
            {
                Scalar1 = Scalar1 - mv2.Scalar1,
                Scalar2 = Scalar2 - mv2.Scalar2,
                Scalar3 = Scalar3 - mv2.Scalar3,
                Scalar4 = Scalar4 - mv2.Scalar4
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Subtract(CGaBivector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2.Negative());

            return CGaMultivector4D.Create(
                CGaScalar4D.Zero,
                this,
                mv2.Negative(),
                CGaTrivector4D.Zero,
                CGa4Vector4D.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Subtract(CGaTrivector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2.Negative());

            return CGaMultivector4D.Create(
                CGaScalar4D.Zero,
                this,
                CGaBivector4D.Zero,
                mv2.Negative(),
                CGa4Vector4D.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Subtract(CGa4Vector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2.Negative());

            return CGaMultivector4D.Create(
                CGaScalar4D.Zero,
                this,
                CGaBivector4D.Zero,
                CGaTrivector4D.Zero,
                mv2.Negative()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Subtract(CGaMultivector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return mv2.Negative();

            return CGaMultivector4D.Create(
                mv2.KVector0.Negative(),
                Subtract(mv2.KVector1),
                mv2.KVector2.Negative(),
                mv2.KVector3.Negative(),
                mv2.KVector4.Negative()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Sp(CGaScalar4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Sp(CGaVector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaScalar4D.Zero;

            return new CGaScalar4D()
            {
                Scalar = -Scalar1 * mv2.Scalar1 + Scalar2 * mv2.Scalar2 + Scalar3 * mv2.Scalar3 + Scalar4 * mv2.Scalar4
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Sp(CGaBivector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Sp(CGaTrivector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Sp(CGa4Vector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void SpUpdateMultivectorArray(CGaVector4D kv2, double[] scalarArray)
        {
            scalarArray[0] += -Scalar1 * kv2.Scalar1 + Scalar2 * kv2.Scalar2 + Scalar3 * kv2.Scalar3 + Scalar4 * kv2.Scalar4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D Op(CGaScalar4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaVector4D.Zero;

            return new CGaVector4D()
            {
                Scalar1 = Scalar1 * mv2.Scalar,
                Scalar2 = Scalar2 * mv2.Scalar,
                Scalar3 = Scalar3 * mv2.Scalar,
                Scalar4 = Scalar4 * mv2.Scalar
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D Op(CGaVector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaBivector4D.Zero;

            return new CGaBivector4D()
            {
                Scalar12 = Scalar1 * mv2.Scalar2 - Scalar2 * mv2.Scalar1,
                Scalar13 = Scalar1 * mv2.Scalar3 - Scalar3 * mv2.Scalar1,
                Scalar23 = Scalar2 * mv2.Scalar3 - Scalar3 * mv2.Scalar2,
                Scalar14 = Scalar1 * mv2.Scalar4 - Scalar4 * mv2.Scalar1,
                Scalar24 = Scalar2 * mv2.Scalar4 - Scalar4 * mv2.Scalar2,
                Scalar34 = Scalar3 * mv2.Scalar4 - Scalar4 * mv2.Scalar3
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D Op(CGaBivector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaTrivector4D.Zero;

            return new CGaTrivector4D()
            {
                Scalar123 = Scalar1 * mv2.Scalar23 - Scalar2 * mv2.Scalar13 + Scalar3 * mv2.Scalar12,
                Scalar124 = Scalar1 * mv2.Scalar24 - Scalar2 * mv2.Scalar14 + Scalar4 * mv2.Scalar12,
                Scalar134 = Scalar1 * mv2.Scalar34 - Scalar3 * mv2.Scalar14 + Scalar4 * mv2.Scalar13,
                Scalar234 = Scalar2 * mv2.Scalar34 - Scalar3 * mv2.Scalar24 + Scalar4 * mv2.Scalar23
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGa4Vector4D Op(CGaTrivector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGa4Vector4D.Zero;

            return new CGa4Vector4D()
            {
                Scalar1234 = Scalar1 * mv2.Scalar234 - Scalar2 * mv2.Scalar134 + Scalar3 * mv2.Scalar124 - Scalar4 * mv2.Scalar123
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Op(CGa4Vector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void OpUpdateMultivectorArray(CGaScalar4D kv2, double[] scalarArray)
        {
            scalarArray[1] += Scalar1 * kv2.Scalar;
            scalarArray[2] += Scalar2 * kv2.Scalar;
            scalarArray[4] += Scalar3 * kv2.Scalar;
            scalarArray[8] += Scalar4 * kv2.Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void OpUpdateMultivectorArray(CGaVector4D kv2, double[] scalarArray)
        {
            scalarArray[3] += Scalar1 * kv2.Scalar2 - Scalar2 * kv2.Scalar1;
            scalarArray[5] += Scalar1 * kv2.Scalar3 - Scalar3 * kv2.Scalar1;
            scalarArray[6] += Scalar2 * kv2.Scalar3 - Scalar3 * kv2.Scalar2;
            scalarArray[9] += Scalar1 * kv2.Scalar4 - Scalar4 * kv2.Scalar1;
            scalarArray[10] += Scalar2 * kv2.Scalar4 - Scalar4 * kv2.Scalar2;
            scalarArray[12] += Scalar3 * kv2.Scalar4 - Scalar4 * kv2.Scalar3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void OpUpdateMultivectorArray(CGaBivector4D kv2, double[] scalarArray)
        {
            scalarArray[7] += Scalar1 * kv2.Scalar23 - Scalar2 * kv2.Scalar13 + Scalar3 * kv2.Scalar12;
            scalarArray[11] += Scalar1 * kv2.Scalar24 - Scalar2 * kv2.Scalar14 + Scalar4 * kv2.Scalar12;
            scalarArray[13] += Scalar1 * kv2.Scalar34 - Scalar3 * kv2.Scalar14 + Scalar4 * kv2.Scalar13;
            scalarArray[14] += Scalar2 * kv2.Scalar34 - Scalar3 * kv2.Scalar24 + Scalar4 * kv2.Scalar23;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void OpUpdateMultivectorArray(CGaTrivector4D kv2, double[] scalarArray)
        {
            scalarArray[15] += Scalar1 * kv2.Scalar234 - Scalar2 * kv2.Scalar134 + Scalar3 * kv2.Scalar124 - Scalar4 * kv2.Scalar123;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Lcp(CGaScalar4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Lcp(CGaVector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaScalar4D.Zero;

            return new CGaScalar4D()
            {
                Scalar = -Scalar1 * mv2.Scalar1 + Scalar2 * mv2.Scalar2 + Scalar3 * mv2.Scalar3 + Scalar4 * mv2.Scalar4
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D Lcp(CGaBivector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaVector4D.Zero;

            return new CGaVector4D()
            {
                Scalar1 = -Scalar2 * mv2.Scalar12 - Scalar3 * mv2.Scalar13 - Scalar4 * mv2.Scalar14,
                Scalar2 = -Scalar1 * mv2.Scalar12 - Scalar3 * mv2.Scalar23 - Scalar4 * mv2.Scalar24,
                Scalar3 = -Scalar1 * mv2.Scalar13 + Scalar2 * mv2.Scalar23 - Scalar4 * mv2.Scalar34,
                Scalar4 = -Scalar1 * mv2.Scalar14 + Scalar2 * mv2.Scalar24 + Scalar3 * mv2.Scalar34
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D Lcp(CGaTrivector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaBivector4D.Zero;

            return new CGaBivector4D()
            {
                Scalar12 = Scalar3 * mv2.Scalar123 + Scalar4 * mv2.Scalar124,
                Scalar13 = -Scalar2 * mv2.Scalar123 + Scalar4 * mv2.Scalar134,
                Scalar23 = -Scalar1 * mv2.Scalar123 + Scalar4 * mv2.Scalar234,
                Scalar14 = -Scalar2 * mv2.Scalar124 - Scalar3 * mv2.Scalar134,
                Scalar24 = -Scalar1 * mv2.Scalar124 - Scalar3 * mv2.Scalar234,
                Scalar34 = -Scalar1 * mv2.Scalar134 + Scalar2 * mv2.Scalar234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D Lcp(CGa4Vector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaTrivector4D.Zero;

            return new CGaTrivector4D()
            {
                Scalar123 = -Scalar4 * mv2.Scalar1234,
                Scalar124 = Scalar3 * mv2.Scalar1234,
                Scalar134 = -Scalar2 * mv2.Scalar1234,
                Scalar234 = -Scalar1 * mv2.Scalar1234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void LcpUpdateMultivectorArray(CGaVector4D kv2, double[] scalarArray)
        {
            scalarArray[0] += -Scalar1 * kv2.Scalar1 + Scalar2 * kv2.Scalar2 + Scalar3 * kv2.Scalar3 + Scalar4 * kv2.Scalar4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void LcpUpdateMultivectorArray(CGaBivector4D kv2, double[] scalarArray)
        {
            scalarArray[1] += -Scalar2 * kv2.Scalar12 - Scalar3 * kv2.Scalar13 - Scalar4 * kv2.Scalar14;
            scalarArray[2] += -Scalar1 * kv2.Scalar12 - Scalar3 * kv2.Scalar23 - Scalar4 * kv2.Scalar24;
            scalarArray[4] += -Scalar1 * kv2.Scalar13 + Scalar2 * kv2.Scalar23 - Scalar4 * kv2.Scalar34;
            scalarArray[8] += -Scalar1 * kv2.Scalar14 + Scalar2 * kv2.Scalar24 + Scalar3 * kv2.Scalar34;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void LcpUpdateMultivectorArray(CGaTrivector4D kv2, double[] scalarArray)
        {
            scalarArray[3] += Scalar3 * kv2.Scalar123 + Scalar4 * kv2.Scalar124;
            scalarArray[5] += -Scalar2 * kv2.Scalar123 + Scalar4 * kv2.Scalar134;
            scalarArray[6] += -Scalar1 * kv2.Scalar123 + Scalar4 * kv2.Scalar234;
            scalarArray[9] += -Scalar2 * kv2.Scalar124 - Scalar3 * kv2.Scalar134;
            scalarArray[10] += -Scalar1 * kv2.Scalar124 - Scalar3 * kv2.Scalar234;
            scalarArray[12] += -Scalar1 * kv2.Scalar134 + Scalar2 * kv2.Scalar234;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void LcpUpdateMultivectorArray(CGa4Vector4D kv2, double[] scalarArray)
        {
            scalarArray[7] += -Scalar4 * kv2.Scalar1234;
            scalarArray[11] += Scalar3 * kv2.Scalar1234;
            scalarArray[13] += -Scalar2 * kv2.Scalar1234;
            scalarArray[14] += -Scalar1 * kv2.Scalar1234;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D Rcp(CGaScalar4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaVector4D.Zero;

            return new CGaVector4D()
            {
                Scalar1 = Scalar1 * mv2.Scalar,
                Scalar2 = Scalar2 * mv2.Scalar,
                Scalar3 = Scalar3 * mv2.Scalar,
                Scalar4 = Scalar4 * mv2.Scalar
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Rcp(CGaVector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaScalar4D.Zero;

            return new CGaScalar4D()
            {
                Scalar = -Scalar1 * mv2.Scalar1 + Scalar2 * mv2.Scalar2 + Scalar3 * mv2.Scalar3 + Scalar4 * mv2.Scalar4
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Rcp(CGaBivector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Rcp(CGaTrivector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Rcp(CGa4Vector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void RcpUpdateMultivectorArray(CGaScalar4D kv2, double[] scalarArray)
        {
            scalarArray[1] += Scalar1 * kv2.Scalar;
            scalarArray[2] += Scalar2 * kv2.Scalar;
            scalarArray[4] += Scalar3 * kv2.Scalar;
            scalarArray[8] += Scalar4 * kv2.Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void RcpUpdateMultivectorArray(CGaVector4D kv2, double[] scalarArray)
        {
            scalarArray[0] += -Scalar1 * kv2.Scalar1 + Scalar2 * kv2.Scalar2 + Scalar3 * kv2.Scalar3 + Scalar4 * kv2.Scalar4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D Fdp(CGaScalar4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaVector4D.Zero;

            return new CGaVector4D()
            {
                Scalar1 = Scalar1 * mv2.Scalar,
                Scalar2 = Scalar2 * mv2.Scalar,
                Scalar3 = Scalar3 * mv2.Scalar,
                Scalar4 = Scalar4 * mv2.Scalar
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Fdp(CGaVector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaScalar4D.Zero;

            return new CGaScalar4D()
            {
                Scalar = -Scalar1 * mv2.Scalar1 + Scalar2 * mv2.Scalar2 + Scalar3 * mv2.Scalar3 + Scalar4 * mv2.Scalar4
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D Fdp(CGaBivector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaVector4D.Zero;

            return new CGaVector4D()
            {
                Scalar1 = -Scalar2 * mv2.Scalar12 - Scalar3 * mv2.Scalar13 - Scalar4 * mv2.Scalar14,
                Scalar2 = -Scalar1 * mv2.Scalar12 - Scalar3 * mv2.Scalar23 - Scalar4 * mv2.Scalar24,
                Scalar3 = -Scalar1 * mv2.Scalar13 + Scalar2 * mv2.Scalar23 - Scalar4 * mv2.Scalar34,
                Scalar4 = -Scalar1 * mv2.Scalar14 + Scalar2 * mv2.Scalar24 + Scalar3 * mv2.Scalar34
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D Fdp(CGaTrivector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaBivector4D.Zero;

            return new CGaBivector4D()
            {
                Scalar12 = Scalar3 * mv2.Scalar123 + Scalar4 * mv2.Scalar124,
                Scalar13 = -Scalar2 * mv2.Scalar123 + Scalar4 * mv2.Scalar134,
                Scalar23 = -Scalar1 * mv2.Scalar123 + Scalar4 * mv2.Scalar234,
                Scalar14 = -Scalar2 * mv2.Scalar124 - Scalar3 * mv2.Scalar134,
                Scalar24 = -Scalar1 * mv2.Scalar124 - Scalar3 * mv2.Scalar234,
                Scalar34 = -Scalar1 * mv2.Scalar134 + Scalar2 * mv2.Scalar234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D Fdp(CGa4Vector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaTrivector4D.Zero;

            return new CGaTrivector4D()
            {
                Scalar123 = -Scalar4 * mv2.Scalar1234,
                Scalar124 = Scalar3 * mv2.Scalar1234,
                Scalar134 = -Scalar2 * mv2.Scalar1234,
                Scalar234 = -Scalar1 * mv2.Scalar1234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void FdpUpdateMultivectorArray(CGaScalar4D kv2, double[] scalarArray)
        {
            scalarArray[1] += Scalar1 * kv2.Scalar;
            scalarArray[2] += Scalar2 * kv2.Scalar;
            scalarArray[4] += Scalar3 * kv2.Scalar;
            scalarArray[8] += Scalar4 * kv2.Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void FdpUpdateMultivectorArray(CGaVector4D kv2, double[] scalarArray)
        {
            scalarArray[0] += -Scalar1 * kv2.Scalar1 + Scalar2 * kv2.Scalar2 + Scalar3 * kv2.Scalar3 + Scalar4 * kv2.Scalar4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void FdpUpdateMultivectorArray(CGaBivector4D kv2, double[] scalarArray)
        {
            scalarArray[1] += -Scalar2 * kv2.Scalar12 - Scalar3 * kv2.Scalar13 - Scalar4 * kv2.Scalar14;
            scalarArray[2] += -Scalar1 * kv2.Scalar12 - Scalar3 * kv2.Scalar23 - Scalar4 * kv2.Scalar24;
            scalarArray[4] += -Scalar1 * kv2.Scalar13 + Scalar2 * kv2.Scalar23 - Scalar4 * kv2.Scalar34;
            scalarArray[8] += -Scalar1 * kv2.Scalar14 + Scalar2 * kv2.Scalar24 + Scalar3 * kv2.Scalar34;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void FdpUpdateMultivectorArray(CGaTrivector4D kv2, double[] scalarArray)
        {
            scalarArray[3] += Scalar3 * kv2.Scalar123 + Scalar4 * kv2.Scalar124;
            scalarArray[5] += -Scalar2 * kv2.Scalar123 + Scalar4 * kv2.Scalar134;
            scalarArray[6] += -Scalar1 * kv2.Scalar123 + Scalar4 * kv2.Scalar234;
            scalarArray[9] += -Scalar2 * kv2.Scalar124 - Scalar3 * kv2.Scalar134;
            scalarArray[10] += -Scalar1 * kv2.Scalar124 - Scalar3 * kv2.Scalar234;
            scalarArray[12] += -Scalar1 * kv2.Scalar134 + Scalar2 * kv2.Scalar234;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void FdpUpdateMultivectorArray(CGa4Vector4D kv2, double[] scalarArray)
        {
            scalarArray[7] += -Scalar4 * kv2.Scalar1234;
            scalarArray[11] += Scalar3 * kv2.Scalar1234;
            scalarArray[13] += -Scalar2 * kv2.Scalar1234;
            scalarArray[14] += -Scalar1 * kv2.Scalar1234;
        }

        public CGaMultivector4D Gp(CGaScalar4D mv2)
        {
            var scalarArray = new double[16];

            if (!IsZero() && !mv2.IsZero()) GpUpdateMultivectorArray(mv2, scalarArray);

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Gp(CGaVector4D mv2)
        {
            var scalarArray = new double[16];

            if (!IsZero() && !mv2.IsZero()) GpUpdateMultivectorArray(mv2, scalarArray);

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Gp(CGaBivector4D mv2)
        {
            var scalarArray = new double[16];

            if (!IsZero() && !mv2.IsZero()) GpUpdateMultivectorArray(mv2, scalarArray);

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Gp(CGaTrivector4D mv2)
        {
            var scalarArray = new double[16];

            if (!IsZero() && !mv2.IsZero()) GpUpdateMultivectorArray(mv2, scalarArray);

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Gp(CGa4Vector4D mv2)
        {
            var scalarArray = new double[16];

            if (!IsZero() && !mv2.IsZero()) GpUpdateMultivectorArray(mv2, scalarArray);

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Gp(CGaMultivector4D mv2)
        {
            var scalarArray = new double[16];

            if (!IsZero())
            {
                if (!mv2.KVector0.IsZero()) GpUpdateMultivectorArray(mv2.KVector0, scalarArray);
                if (!mv2.KVector1.IsZero()) GpUpdateMultivectorArray(mv2.KVector1, scalarArray);
                if (!mv2.KVector2.IsZero()) GpUpdateMultivectorArray(mv2.KVector2, scalarArray);
                if (!mv2.KVector3.IsZero()) GpUpdateMultivectorArray(mv2.KVector3, scalarArray);
                if (!mv2.KVector4.IsZero()) GpUpdateMultivectorArray(mv2.KVector4, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void GpUpdateMultivectorArray(CGaScalar4D kv2, double[] scalarArray)
        {
            scalarArray[1] += Scalar1 * kv2.Scalar;
            scalarArray[2] += Scalar2 * kv2.Scalar;
            scalarArray[4] += Scalar3 * kv2.Scalar;
            scalarArray[8] += Scalar4 * kv2.Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void GpUpdateMultivectorArray(CGaVector4D kv2, double[] scalarArray)
        {
            scalarArray[0] += -Scalar1 * kv2.Scalar1 + Scalar2 * kv2.Scalar2 + Scalar3 * kv2.Scalar3 + Scalar4 * kv2.Scalar4;
            scalarArray[3] += Scalar1 * kv2.Scalar2 - Scalar2 * kv2.Scalar1;
            scalarArray[5] += Scalar1 * kv2.Scalar3 - Scalar3 * kv2.Scalar1;
            scalarArray[6] += Scalar2 * kv2.Scalar3 - Scalar3 * kv2.Scalar2;
            scalarArray[9] += Scalar1 * kv2.Scalar4 - Scalar4 * kv2.Scalar1;
            scalarArray[10] += Scalar2 * kv2.Scalar4 - Scalar4 * kv2.Scalar2;
            scalarArray[12] += Scalar3 * kv2.Scalar4 - Scalar4 * kv2.Scalar3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void GpUpdateMultivectorArray(CGaBivector4D kv2, double[] scalarArray)
        {
            scalarArray[1] += -Scalar2 * kv2.Scalar12 - Scalar3 * kv2.Scalar13 - Scalar4 * kv2.Scalar14;
            scalarArray[2] += -Scalar1 * kv2.Scalar12 - Scalar3 * kv2.Scalar23 - Scalar4 * kv2.Scalar24;
            scalarArray[4] += -Scalar1 * kv2.Scalar13 + Scalar2 * kv2.Scalar23 - Scalar4 * kv2.Scalar34;
            scalarArray[8] += -Scalar1 * kv2.Scalar14 + Scalar2 * kv2.Scalar24 + Scalar3 * kv2.Scalar34;
            scalarArray[7] += Scalar1 * kv2.Scalar23 - Scalar2 * kv2.Scalar13 + Scalar3 * kv2.Scalar12;
            scalarArray[11] += Scalar1 * kv2.Scalar24 - Scalar2 * kv2.Scalar14 + Scalar4 * kv2.Scalar12;
            scalarArray[13] += Scalar1 * kv2.Scalar34 - Scalar3 * kv2.Scalar14 + Scalar4 * kv2.Scalar13;
            scalarArray[14] += Scalar2 * kv2.Scalar34 - Scalar3 * kv2.Scalar24 + Scalar4 * kv2.Scalar23;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void GpUpdateMultivectorArray(CGaTrivector4D kv2, double[] scalarArray)
        {
            scalarArray[3] += Scalar3 * kv2.Scalar123 + Scalar4 * kv2.Scalar124;
            scalarArray[5] += -Scalar2 * kv2.Scalar123 + Scalar4 * kv2.Scalar134;
            scalarArray[6] += -Scalar1 * kv2.Scalar123 + Scalar4 * kv2.Scalar234;
            scalarArray[9] += -Scalar2 * kv2.Scalar124 - Scalar3 * kv2.Scalar134;
            scalarArray[10] += -Scalar1 * kv2.Scalar124 - Scalar3 * kv2.Scalar234;
            scalarArray[12] += -Scalar1 * kv2.Scalar134 + Scalar2 * kv2.Scalar234;
            scalarArray[15] += Scalar1 * kv2.Scalar234 - Scalar2 * kv2.Scalar134 + Scalar3 * kv2.Scalar124 - Scalar4 * kv2.Scalar123;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void GpUpdateMultivectorArray(CGa4Vector4D kv2, double[] scalarArray)
        {
            scalarArray[7] += -Scalar4 * kv2.Scalar1234;
            scalarArray[11] += Scalar3 * kv2.Scalar1234;
            scalarArray[13] += -Scalar2 * kv2.Scalar1234;
            scalarArray[14] += -Scalar1 * kv2.Scalar1234;
        }

    }

    public sealed class CGaBivector4D
    {
        public static CGaBivector4D Zero { get; } = new CGaBivector4D();

        public static CGaBivector4D E12 { get; } = new CGaBivector4D() { Scalar12 = Float64Scalar.One };

        public static CGaBivector4D E13 { get; } = new CGaBivector4D() { Scalar13 = Float64Scalar.One };

        public static CGaBivector4D E23 { get; } = new CGaBivector4D() { Scalar23 = Float64Scalar.One };

        public static CGaBivector4D E14 { get; } = new CGaBivector4D() { Scalar14 = Float64Scalar.One };

        public static CGaBivector4D E24 { get; } = new CGaBivector4D() { Scalar24 = Float64Scalar.One };

        public static CGaBivector4D E34 { get; } = new CGaBivector4D() { Scalar34 = Float64Scalar.One };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaBivector4D operator +(CGaBivector4D mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaBivector4D operator -(CGaBivector4D mv)
        {
            return mv.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaBivector4D operator *(CGaBivector4D mv1, double mv2)
        {
            return mv1.Times(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaBivector4D operator *(double mv1, CGaBivector4D mv2)
        {
            return mv2.Times(mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaBivector4D operator /(CGaBivector4D mv1, double mv2)
        {
            return mv1.Times(1d / mv2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator +(CGaBivector4D mv1, CGaScalar4D mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator -(CGaBivector4D mv1, CGaScalar4D mv2)
        {
            return mv1.Subtract(mv2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator +(CGaBivector4D mv1, CGaVector4D mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator -(CGaBivector4D mv1, CGaVector4D mv2)
        {
            return mv1.Subtract(mv2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaBivector4D operator +(CGaBivector4D mv1, CGaBivector4D mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaBivector4D operator -(CGaBivector4D mv1, CGaBivector4D mv2)
        {
            return mv1.Subtract(mv2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator +(CGaBivector4D mv1, CGaTrivector4D mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator -(CGaBivector4D mv1, CGaTrivector4D mv2)
        {
            return mv1.Subtract(mv2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator +(CGaBivector4D mv1, CGaMultivector4D mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator -(CGaBivector4D mv1, CGaMultivector4D mv2)
        {
            return mv1.Subtract(mv2);
        }

        public Float64Scalar Scalar12 { get; init; }

        public Float64Scalar Scalar13 { get; init; }

        public Float64Scalar Scalar23 { get; init; }

        public Float64Scalar Scalar14 { get; init; }

        public Float64Scalar Scalar24 { get; init; }

        public Float64Scalar Scalar34 { get; init; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D()
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return
                Scalar12.IsValid() &&
                Scalar13.IsValid() &&
                Scalar23.IsValid() &&
                Scalar14.IsValid() &&
                Scalar24.IsValid() &&
                Scalar34.IsValid();
        }

        private bool? _isZero;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero()
        {
            _isZero ??=
                Scalar12.IsZero() &&
                Scalar13.IsZero() &&
                Scalar23.IsZero() &&
                Scalar14.IsZero() &&
                Scalar24.IsZero() &&
                Scalar34.IsZero();

            return _isZero.Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(double epsilon = 1e-12d)
        {
            return Norm().IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[] GetMultivectorArray()
        {
            var scalarArray = new double[16];

            scalarArray[3] = Scalar12;
            scalarArray[5] = Scalar13;
            scalarArray[6] = Scalar23;
            scalarArray[9] = Scalar14;
            scalarArray[10] = Scalar24;
            scalarArray[12] = Scalar34;

            return scalarArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[] GetKVectorArray()
        {
            return new double[]
            {
            Scalar12,
            Scalar13,
            Scalar23,
            Scalar14,
            Scalar24,
            Scalar34
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar NormSquared()
        {
            return -Scalar12 * Scalar12 - Scalar13 * Scalar13 + Scalar23 * Scalar23 - Scalar14 * Scalar14 + Scalar24 * Scalar24 + Scalar34 * Scalar34;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar Norm()
        {
            return NormSquared().SqrtOfAbs();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D Times(double mv2)
        {
            return new CGaBivector4D()
            {
                Scalar12 = Scalar12 * mv2,
                Scalar13 = Scalar13 * mv2,
                Scalar23 = Scalar23 * mv2,
                Scalar14 = Scalar14 * mv2,
                Scalar24 = Scalar24 * mv2,
                Scalar34 = Scalar34 * mv2
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D Divide(double mv2)
        {
            return Times(1d / mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D DivideByNorm()
        {
            return Times(1d / Norm());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D DivideByNormSquared()
        {
            return Times(1d / NormSquared());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D Negative()
        {
            return new CGaBivector4D()
            {
                Scalar12 = -Scalar12,
                Scalar13 = -Scalar13,
                Scalar23 = -Scalar23,
                Scalar14 = -Scalar14,
                Scalar24 = -Scalar24,
                Scalar34 = -Scalar34
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D Reverse()
        {
            return Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D GradeInvolution()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D CliffordConjugate()
        {
            return Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D Inverse()
        {
            return Times(-1d / NormSquared());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D InverseTimes(double mv2)
        {
            return Times(-mv2 / NormSquared());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D PseudoInverse()
        {
            var conjugate = Conjugate();

            return conjugate.Times(
                1d / conjugate.Sp(this).Scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D PseudoInverseTimes(double mv2)
        {
            var conjugate = Conjugate();

            return conjugate.Times(
                mv2 / conjugate.Sp(this).Scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D Conjugate()
        {
            return new CGaBivector4D()
            {
                Scalar12 = Scalar12,
                Scalar13 = Scalar13,
                Scalar23 = -Scalar23,
                Scalar14 = Scalar14,
                Scalar24 = -Scalar24,
                Scalar34 = -Scalar34
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D Dual()
        {
            return new CGaBivector4D()
            {
                Scalar12 = Scalar34,
                Scalar13 = -Scalar24,
                Scalar23 = -Scalar14,
                Scalar14 = Scalar23,
                Scalar24 = Scalar13,
                Scalar34 = -Scalar12
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D UnDual()
        {
            return new CGaBivector4D()
            {
                Scalar12 = -Scalar34,
                Scalar13 = Scalar24,
                Scalar23 = Scalar14,
                Scalar14 = -Scalar23,
                Scalar24 = -Scalar13,
                Scalar34 = Scalar12
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Dual(CGaBivector4D kv2)
        {
            return Lcp(kv2.Inverse());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D UnDual(CGaBivector4D kv2)
        {
            return Lcp(kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D Dual(CGaTrivector4D kv2)
        {
            return Lcp(kv2.Inverse());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D UnDual(CGaTrivector4D kv2)
        {
            return Lcp(kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D Dual(CGa4Vector4D kv2)
        {
            return Lcp(kv2.Inverse());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D UnDual(CGa4Vector4D kv2)
        {
            return Lcp(kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Add(CGaScalar4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2);

            return CGaMultivector4D.Create(
                mv2,
                CGaVector4D.Zero,
                this,
                CGaTrivector4D.Zero,
                CGa4Vector4D.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Add(CGaVector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2);

            return CGaMultivector4D.Create(
                CGaScalar4D.Zero,
                mv2,
                this,
                CGaTrivector4D.Zero,
                CGa4Vector4D.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D Add(CGaBivector4D mv2)
        {
            if (mv2.IsZero()) return this;
            if (IsZero()) return mv2;

            return new CGaBivector4D()
            {
                Scalar12 = Scalar12 + mv2.Scalar12,
                Scalar13 = Scalar13 + mv2.Scalar13,
                Scalar23 = Scalar23 + mv2.Scalar23,
                Scalar14 = Scalar14 + mv2.Scalar14,
                Scalar24 = Scalar24 + mv2.Scalar24,
                Scalar34 = Scalar34 + mv2.Scalar34
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Add(CGaTrivector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2);

            return CGaMultivector4D.Create(
                CGaScalar4D.Zero,
                CGaVector4D.Zero,
                this,
                mv2,
                CGa4Vector4D.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Add(CGa4Vector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2);

            return CGaMultivector4D.Create(
                CGaScalar4D.Zero,
                CGaVector4D.Zero,
                this,
                CGaTrivector4D.Zero,
                mv2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Add(CGaMultivector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return mv2;

            return CGaMultivector4D.Create(
                mv2.KVector0,
                mv2.KVector1,
                Add(mv2.KVector2),
                mv2.KVector3,
                mv2.KVector4
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Subtract(CGaScalar4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2.Negative());

            return CGaMultivector4D.Create(
                mv2.Negative(),
                CGaVector4D.Zero,
                this,
                CGaTrivector4D.Zero,
                CGa4Vector4D.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Subtract(CGaVector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2.Negative());

            return CGaMultivector4D.Create(
                CGaScalar4D.Zero,
                mv2.Negative(),
                this,
                CGaTrivector4D.Zero,
                CGa4Vector4D.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D Subtract(CGaBivector4D mv2)
        {
            if (mv2.IsZero()) return this;
            if (IsZero()) return mv2.Negative();

            return new CGaBivector4D()
            {
                Scalar12 = Scalar12 - mv2.Scalar12,
                Scalar13 = Scalar13 - mv2.Scalar13,
                Scalar23 = Scalar23 - mv2.Scalar23,
                Scalar14 = Scalar14 - mv2.Scalar14,
                Scalar24 = Scalar24 - mv2.Scalar24,
                Scalar34 = Scalar34 - mv2.Scalar34
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Subtract(CGaTrivector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2.Negative());

            return CGaMultivector4D.Create(
                CGaScalar4D.Zero,
                CGaVector4D.Zero,
                this,
                mv2.Negative(),
                CGa4Vector4D.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Subtract(CGa4Vector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2.Negative());

            return CGaMultivector4D.Create(
                CGaScalar4D.Zero,
                CGaVector4D.Zero,
                this,
                CGaTrivector4D.Zero,
                mv2.Negative()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Subtract(CGaMultivector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return mv2.Negative();

            return CGaMultivector4D.Create(
                mv2.KVector0.Negative(),
                mv2.KVector1.Negative(),
                Subtract(mv2.KVector2),
                mv2.KVector3.Negative(),
                mv2.KVector4.Negative()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Sp(CGaScalar4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Sp(CGaVector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Sp(CGaBivector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaScalar4D.Zero;

            return new CGaScalar4D()
            {
                Scalar = Scalar12 * mv2.Scalar12 + Scalar13 * mv2.Scalar13 - Scalar23 * mv2.Scalar23 + Scalar14 * mv2.Scalar14 - Scalar24 * mv2.Scalar24 - Scalar34 * mv2.Scalar34
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Sp(CGaTrivector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Sp(CGa4Vector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void SpUpdateMultivectorArray(CGaBivector4D kv2, double[] scalarArray)
        {
            scalarArray[0] += Scalar12 * kv2.Scalar12 + Scalar13 * kv2.Scalar13 - Scalar23 * kv2.Scalar23 + Scalar14 * kv2.Scalar14 - Scalar24 * kv2.Scalar24 - Scalar34 * kv2.Scalar34;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D Op(CGaScalar4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaBivector4D.Zero;

            return new CGaBivector4D()
            {
                Scalar12 = Scalar12 * mv2.Scalar,
                Scalar13 = Scalar13 * mv2.Scalar,
                Scalar23 = Scalar23 * mv2.Scalar,
                Scalar14 = Scalar14 * mv2.Scalar,
                Scalar24 = Scalar24 * mv2.Scalar,
                Scalar34 = Scalar34 * mv2.Scalar
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D Op(CGaVector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaTrivector4D.Zero;

            return new CGaTrivector4D()
            {
                Scalar123 = Scalar12 * mv2.Scalar3 - Scalar13 * mv2.Scalar2 + Scalar23 * mv2.Scalar1,
                Scalar124 = Scalar12 * mv2.Scalar4 - Scalar14 * mv2.Scalar2 + Scalar24 * mv2.Scalar1,
                Scalar134 = Scalar13 * mv2.Scalar4 - Scalar14 * mv2.Scalar3 + Scalar34 * mv2.Scalar1,
                Scalar234 = Scalar23 * mv2.Scalar4 - Scalar24 * mv2.Scalar3 + Scalar34 * mv2.Scalar2
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGa4Vector4D Op(CGaBivector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGa4Vector4D.Zero;

            return new CGa4Vector4D()
            {
                Scalar1234 = Scalar12 * mv2.Scalar34 - Scalar13 * mv2.Scalar24 + Scalar23 * mv2.Scalar14 + Scalar14 * mv2.Scalar23 - Scalar24 * mv2.Scalar13 + Scalar34 * mv2.Scalar12
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Op(CGaTrivector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Op(CGa4Vector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void OpUpdateMultivectorArray(CGaScalar4D kv2, double[] scalarArray)
        {
            scalarArray[3] += Scalar12 * kv2.Scalar;
            scalarArray[5] += Scalar13 * kv2.Scalar;
            scalarArray[6] += Scalar23 * kv2.Scalar;
            scalarArray[9] += Scalar14 * kv2.Scalar;
            scalarArray[10] += Scalar24 * kv2.Scalar;
            scalarArray[12] += Scalar34 * kv2.Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void OpUpdateMultivectorArray(CGaVector4D kv2, double[] scalarArray)
        {
            scalarArray[7] += Scalar12 * kv2.Scalar3 - Scalar13 * kv2.Scalar2 + Scalar23 * kv2.Scalar1;
            scalarArray[11] += Scalar12 * kv2.Scalar4 - Scalar14 * kv2.Scalar2 + Scalar24 * kv2.Scalar1;
            scalarArray[13] += Scalar13 * kv2.Scalar4 - Scalar14 * kv2.Scalar3 + Scalar34 * kv2.Scalar1;
            scalarArray[14] += Scalar23 * kv2.Scalar4 - Scalar24 * kv2.Scalar3 + Scalar34 * kv2.Scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void OpUpdateMultivectorArray(CGaBivector4D kv2, double[] scalarArray)
        {
            scalarArray[15] += Scalar12 * kv2.Scalar34 - Scalar13 * kv2.Scalar24 + Scalar23 * kv2.Scalar14 + Scalar14 * kv2.Scalar23 - Scalar24 * kv2.Scalar13 + Scalar34 * kv2.Scalar12;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Lcp(CGaScalar4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Lcp(CGaVector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Lcp(CGaBivector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaScalar4D.Zero;

            return new CGaScalar4D()
            {
                Scalar = Scalar12 * mv2.Scalar12 + Scalar13 * mv2.Scalar13 - Scalar23 * mv2.Scalar23 + Scalar14 * mv2.Scalar14 - Scalar24 * mv2.Scalar24 - Scalar34 * mv2.Scalar34
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D Lcp(CGaTrivector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaVector4D.Zero;

            return new CGaVector4D()
            {
                Scalar1 = -Scalar23 * mv2.Scalar123 - Scalar24 * mv2.Scalar124 - Scalar34 * mv2.Scalar134,
                Scalar2 = -Scalar13 * mv2.Scalar123 - Scalar14 * mv2.Scalar124 - Scalar34 * mv2.Scalar234,
                Scalar3 = Scalar12 * mv2.Scalar123 - Scalar14 * mv2.Scalar134 + Scalar24 * mv2.Scalar234,
                Scalar4 = Scalar12 * mv2.Scalar124 + Scalar13 * mv2.Scalar134 - Scalar23 * mv2.Scalar234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D Lcp(CGa4Vector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaBivector4D.Zero;

            return new CGaBivector4D()
            {
                Scalar12 = -Scalar34 * mv2.Scalar1234,
                Scalar13 = Scalar24 * mv2.Scalar1234,
                Scalar23 = Scalar14 * mv2.Scalar1234,
                Scalar14 = -Scalar23 * mv2.Scalar1234,
                Scalar24 = -Scalar13 * mv2.Scalar1234,
                Scalar34 = Scalar12 * mv2.Scalar1234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void LcpUpdateMultivectorArray(CGaBivector4D kv2, double[] scalarArray)
        {
            scalarArray[0] += Scalar12 * kv2.Scalar12 + Scalar13 * kv2.Scalar13 - Scalar23 * kv2.Scalar23 + Scalar14 * kv2.Scalar14 - Scalar24 * kv2.Scalar24 - Scalar34 * kv2.Scalar34;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void LcpUpdateMultivectorArray(CGaTrivector4D kv2, double[] scalarArray)
        {
            scalarArray[1] += -Scalar23 * kv2.Scalar123 - Scalar24 * kv2.Scalar124 - Scalar34 * kv2.Scalar134;
            scalarArray[2] += -Scalar13 * kv2.Scalar123 - Scalar14 * kv2.Scalar124 - Scalar34 * kv2.Scalar234;
            scalarArray[4] += Scalar12 * kv2.Scalar123 - Scalar14 * kv2.Scalar134 + Scalar24 * kv2.Scalar234;
            scalarArray[8] += Scalar12 * kv2.Scalar124 + Scalar13 * kv2.Scalar134 - Scalar23 * kv2.Scalar234;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void LcpUpdateMultivectorArray(CGa4Vector4D kv2, double[] scalarArray)
        {
            scalarArray[3] += -Scalar34 * kv2.Scalar1234;
            scalarArray[5] += Scalar24 * kv2.Scalar1234;
            scalarArray[6] += Scalar14 * kv2.Scalar1234;
            scalarArray[9] += -Scalar23 * kv2.Scalar1234;
            scalarArray[10] += -Scalar13 * kv2.Scalar1234;
            scalarArray[12] += Scalar12 * kv2.Scalar1234;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D Rcp(CGaScalar4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaBivector4D.Zero;

            return new CGaBivector4D()
            {
                Scalar12 = Scalar12 * mv2.Scalar,
                Scalar13 = Scalar13 * mv2.Scalar,
                Scalar23 = Scalar23 * mv2.Scalar,
                Scalar14 = Scalar14 * mv2.Scalar,
                Scalar24 = Scalar24 * mv2.Scalar,
                Scalar34 = Scalar34 * mv2.Scalar
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D Rcp(CGaVector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaVector4D.Zero;

            return new CGaVector4D()
            {
                Scalar1 = Scalar12 * mv2.Scalar2 + Scalar13 * mv2.Scalar3 + Scalar14 * mv2.Scalar4,
                Scalar2 = Scalar12 * mv2.Scalar1 + Scalar23 * mv2.Scalar3 + Scalar24 * mv2.Scalar4,
                Scalar3 = Scalar13 * mv2.Scalar1 - Scalar23 * mv2.Scalar2 + Scalar34 * mv2.Scalar4,
                Scalar4 = Scalar14 * mv2.Scalar1 - Scalar24 * mv2.Scalar2 - Scalar34 * mv2.Scalar3
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Rcp(CGaBivector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaScalar4D.Zero;

            return new CGaScalar4D()
            {
                Scalar = Scalar12 * mv2.Scalar12 + Scalar13 * mv2.Scalar13 - Scalar23 * mv2.Scalar23 + Scalar14 * mv2.Scalar14 - Scalar24 * mv2.Scalar24 - Scalar34 * mv2.Scalar34
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Rcp(CGaTrivector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Rcp(CGa4Vector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void RcpUpdateMultivectorArray(CGaScalar4D kv2, double[] scalarArray)
        {
            scalarArray[3] += Scalar12 * kv2.Scalar;
            scalarArray[5] += Scalar13 * kv2.Scalar;
            scalarArray[6] += Scalar23 * kv2.Scalar;
            scalarArray[9] += Scalar14 * kv2.Scalar;
            scalarArray[10] += Scalar24 * kv2.Scalar;
            scalarArray[12] += Scalar34 * kv2.Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void RcpUpdateMultivectorArray(CGaVector4D kv2, double[] scalarArray)
        {
            scalarArray[1] += Scalar12 * kv2.Scalar2 + Scalar13 * kv2.Scalar3 + Scalar14 * kv2.Scalar4;
            scalarArray[2] += Scalar12 * kv2.Scalar1 + Scalar23 * kv2.Scalar3 + Scalar24 * kv2.Scalar4;
            scalarArray[4] += Scalar13 * kv2.Scalar1 - Scalar23 * kv2.Scalar2 + Scalar34 * kv2.Scalar4;
            scalarArray[8] += Scalar14 * kv2.Scalar1 - Scalar24 * kv2.Scalar2 - Scalar34 * kv2.Scalar3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void RcpUpdateMultivectorArray(CGaBivector4D kv2, double[] scalarArray)
        {
            scalarArray[0] += Scalar12 * kv2.Scalar12 + Scalar13 * kv2.Scalar13 - Scalar23 * kv2.Scalar23 + Scalar14 * kv2.Scalar14 - Scalar24 * kv2.Scalar24 - Scalar34 * kv2.Scalar34;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D Fdp(CGaScalar4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaBivector4D.Zero;

            return new CGaBivector4D()
            {
                Scalar12 = Scalar12 * mv2.Scalar,
                Scalar13 = Scalar13 * mv2.Scalar,
                Scalar23 = Scalar23 * mv2.Scalar,
                Scalar14 = Scalar14 * mv2.Scalar,
                Scalar24 = Scalar24 * mv2.Scalar,
                Scalar34 = Scalar34 * mv2.Scalar
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D Fdp(CGaVector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaVector4D.Zero;

            return new CGaVector4D()
            {
                Scalar1 = Scalar12 * mv2.Scalar2 + Scalar13 * mv2.Scalar3 + Scalar14 * mv2.Scalar4,
                Scalar2 = Scalar12 * mv2.Scalar1 + Scalar23 * mv2.Scalar3 + Scalar24 * mv2.Scalar4,
                Scalar3 = Scalar13 * mv2.Scalar1 - Scalar23 * mv2.Scalar2 + Scalar34 * mv2.Scalar4,
                Scalar4 = Scalar14 * mv2.Scalar1 - Scalar24 * mv2.Scalar2 - Scalar34 * mv2.Scalar3
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Fdp(CGaBivector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaScalar4D.Zero;

            return new CGaScalar4D()
            {
                Scalar = Scalar12 * mv2.Scalar12 + Scalar13 * mv2.Scalar13 - Scalar23 * mv2.Scalar23 + Scalar14 * mv2.Scalar14 - Scalar24 * mv2.Scalar24 - Scalar34 * mv2.Scalar34
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D Fdp(CGaTrivector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaVector4D.Zero;

            return new CGaVector4D()
            {
                Scalar1 = -Scalar23 * mv2.Scalar123 - Scalar24 * mv2.Scalar124 - Scalar34 * mv2.Scalar134,
                Scalar2 = -Scalar13 * mv2.Scalar123 - Scalar14 * mv2.Scalar124 - Scalar34 * mv2.Scalar234,
                Scalar3 = Scalar12 * mv2.Scalar123 - Scalar14 * mv2.Scalar134 + Scalar24 * mv2.Scalar234,
                Scalar4 = Scalar12 * mv2.Scalar124 + Scalar13 * mv2.Scalar134 - Scalar23 * mv2.Scalar234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D Fdp(CGa4Vector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaBivector4D.Zero;

            return new CGaBivector4D()
            {
                Scalar12 = -Scalar34 * mv2.Scalar1234,
                Scalar13 = Scalar24 * mv2.Scalar1234,
                Scalar23 = Scalar14 * mv2.Scalar1234,
                Scalar14 = -Scalar23 * mv2.Scalar1234,
                Scalar24 = -Scalar13 * mv2.Scalar1234,
                Scalar34 = Scalar12 * mv2.Scalar1234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void FdpUpdateMultivectorArray(CGaScalar4D kv2, double[] scalarArray)
        {
            scalarArray[3] += Scalar12 * kv2.Scalar;
            scalarArray[5] += Scalar13 * kv2.Scalar;
            scalarArray[6] += Scalar23 * kv2.Scalar;
            scalarArray[9] += Scalar14 * kv2.Scalar;
            scalarArray[10] += Scalar24 * kv2.Scalar;
            scalarArray[12] += Scalar34 * kv2.Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void FdpUpdateMultivectorArray(CGaVector4D kv2, double[] scalarArray)
        {
            scalarArray[1] += Scalar12 * kv2.Scalar2 + Scalar13 * kv2.Scalar3 + Scalar14 * kv2.Scalar4;
            scalarArray[2] += Scalar12 * kv2.Scalar1 + Scalar23 * kv2.Scalar3 + Scalar24 * kv2.Scalar4;
            scalarArray[4] += Scalar13 * kv2.Scalar1 - Scalar23 * kv2.Scalar2 + Scalar34 * kv2.Scalar4;
            scalarArray[8] += Scalar14 * kv2.Scalar1 - Scalar24 * kv2.Scalar2 - Scalar34 * kv2.Scalar3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void FdpUpdateMultivectorArray(CGaBivector4D kv2, double[] scalarArray)
        {
            scalarArray[0] += Scalar12 * kv2.Scalar12 + Scalar13 * kv2.Scalar13 - Scalar23 * kv2.Scalar23 + Scalar14 * kv2.Scalar14 - Scalar24 * kv2.Scalar24 - Scalar34 * kv2.Scalar34;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void FdpUpdateMultivectorArray(CGaTrivector4D kv2, double[] scalarArray)
        {
            scalarArray[1] += -Scalar23 * kv2.Scalar123 - Scalar24 * kv2.Scalar124 - Scalar34 * kv2.Scalar134;
            scalarArray[2] += -Scalar13 * kv2.Scalar123 - Scalar14 * kv2.Scalar124 - Scalar34 * kv2.Scalar234;
            scalarArray[4] += Scalar12 * kv2.Scalar123 - Scalar14 * kv2.Scalar134 + Scalar24 * kv2.Scalar234;
            scalarArray[8] += Scalar12 * kv2.Scalar124 + Scalar13 * kv2.Scalar134 - Scalar23 * kv2.Scalar234;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void FdpUpdateMultivectorArray(CGa4Vector4D kv2, double[] scalarArray)
        {
            scalarArray[3] += -Scalar34 * kv2.Scalar1234;
            scalarArray[5] += Scalar24 * kv2.Scalar1234;
            scalarArray[6] += Scalar14 * kv2.Scalar1234;
            scalarArray[9] += -Scalar23 * kv2.Scalar1234;
            scalarArray[10] += -Scalar13 * kv2.Scalar1234;
            scalarArray[12] += Scalar12 * kv2.Scalar1234;
        }

        public CGaMultivector4D Gp(CGaScalar4D mv2)
        {
            var scalarArray = new double[16];

            if (!IsZero() && !mv2.IsZero()) GpUpdateMultivectorArray(mv2, scalarArray);

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Gp(CGaVector4D mv2)
        {
            var scalarArray = new double[16];

            if (!IsZero() && !mv2.IsZero()) GpUpdateMultivectorArray(mv2, scalarArray);

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Gp(CGaBivector4D mv2)
        {
            var scalarArray = new double[16];

            if (!IsZero() && !mv2.IsZero()) GpUpdateMultivectorArray(mv2, scalarArray);

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Gp(CGaTrivector4D mv2)
        {
            var scalarArray = new double[16];

            if (!IsZero() && !mv2.IsZero()) GpUpdateMultivectorArray(mv2, scalarArray);

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Gp(CGa4Vector4D mv2)
        {
            var scalarArray = new double[16];

            if (!IsZero() && !mv2.IsZero()) GpUpdateMultivectorArray(mv2, scalarArray);

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Gp(CGaMultivector4D mv2)
        {
            var scalarArray = new double[16];

            if (!IsZero())
            {
                if (!mv2.KVector0.IsZero()) GpUpdateMultivectorArray(mv2.KVector0, scalarArray);
                if (!mv2.KVector1.IsZero()) GpUpdateMultivectorArray(mv2.KVector1, scalarArray);
                if (!mv2.KVector2.IsZero()) GpUpdateMultivectorArray(mv2.KVector2, scalarArray);
                if (!mv2.KVector3.IsZero()) GpUpdateMultivectorArray(mv2.KVector3, scalarArray);
                if (!mv2.KVector4.IsZero()) GpUpdateMultivectorArray(mv2.KVector4, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void GpUpdateMultivectorArray(CGaScalar4D kv2, double[] scalarArray)
        {
            scalarArray[3] += Scalar12 * kv2.Scalar;
            scalarArray[5] += Scalar13 * kv2.Scalar;
            scalarArray[6] += Scalar23 * kv2.Scalar;
            scalarArray[9] += Scalar14 * kv2.Scalar;
            scalarArray[10] += Scalar24 * kv2.Scalar;
            scalarArray[12] += Scalar34 * kv2.Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void GpUpdateMultivectorArray(CGaVector4D kv2, double[] scalarArray)
        {
            scalarArray[1] += Scalar12 * kv2.Scalar2 + Scalar13 * kv2.Scalar3 + Scalar14 * kv2.Scalar4;
            scalarArray[2] += Scalar12 * kv2.Scalar1 + Scalar23 * kv2.Scalar3 + Scalar24 * kv2.Scalar4;
            scalarArray[4] += Scalar13 * kv2.Scalar1 - Scalar23 * kv2.Scalar2 + Scalar34 * kv2.Scalar4;
            scalarArray[8] += Scalar14 * kv2.Scalar1 - Scalar24 * kv2.Scalar2 - Scalar34 * kv2.Scalar3;
            scalarArray[7] += Scalar12 * kv2.Scalar3 - Scalar13 * kv2.Scalar2 + Scalar23 * kv2.Scalar1;
            scalarArray[11] += Scalar12 * kv2.Scalar4 - Scalar14 * kv2.Scalar2 + Scalar24 * kv2.Scalar1;
            scalarArray[13] += Scalar13 * kv2.Scalar4 - Scalar14 * kv2.Scalar3 + Scalar34 * kv2.Scalar1;
            scalarArray[14] += Scalar23 * kv2.Scalar4 - Scalar24 * kv2.Scalar3 + Scalar34 * kv2.Scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void GpUpdateMultivectorArray(CGaBivector4D kv2, double[] scalarArray)
        {
            scalarArray[0] += Scalar12 * kv2.Scalar12 + Scalar13 * kv2.Scalar13 - Scalar23 * kv2.Scalar23 + Scalar14 * kv2.Scalar14 - Scalar24 * kv2.Scalar24 - Scalar34 * kv2.Scalar34;
            scalarArray[3] += -Scalar13 * kv2.Scalar23 + Scalar23 * kv2.Scalar13 - Scalar14 * kv2.Scalar24 + Scalar24 * kv2.Scalar14;
            scalarArray[5] += Scalar12 * kv2.Scalar23 - Scalar23 * kv2.Scalar12 - Scalar14 * kv2.Scalar34 + Scalar34 * kv2.Scalar14;
            scalarArray[6] += Scalar12 * kv2.Scalar13 - Scalar13 * kv2.Scalar12 - Scalar24 * kv2.Scalar34 + Scalar34 * kv2.Scalar24;
            scalarArray[9] += Scalar12 * kv2.Scalar24 + Scalar13 * kv2.Scalar34 - Scalar24 * kv2.Scalar12 - Scalar34 * kv2.Scalar13;
            scalarArray[10] += Scalar12 * kv2.Scalar14 + Scalar23 * kv2.Scalar34 - Scalar14 * kv2.Scalar12 - Scalar34 * kv2.Scalar23;
            scalarArray[12] += Scalar13 * kv2.Scalar14 - Scalar23 * kv2.Scalar24 - Scalar14 * kv2.Scalar13 + Scalar24 * kv2.Scalar23;
            scalarArray[15] += Scalar12 * kv2.Scalar34 - Scalar13 * kv2.Scalar24 + Scalar23 * kv2.Scalar14 + Scalar14 * kv2.Scalar23 - Scalar24 * kv2.Scalar13 + Scalar34 * kv2.Scalar12;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void GpUpdateMultivectorArray(CGaTrivector4D kv2, double[] scalarArray)
        {
            scalarArray[1] += -Scalar23 * kv2.Scalar123 - Scalar24 * kv2.Scalar124 - Scalar34 * kv2.Scalar134;
            scalarArray[2] += -Scalar13 * kv2.Scalar123 - Scalar14 * kv2.Scalar124 - Scalar34 * kv2.Scalar234;
            scalarArray[4] += Scalar12 * kv2.Scalar123 - Scalar14 * kv2.Scalar134 + Scalar24 * kv2.Scalar234;
            scalarArray[8] += Scalar12 * kv2.Scalar124 + Scalar13 * kv2.Scalar134 - Scalar23 * kv2.Scalar234;
            scalarArray[7] += Scalar14 * kv2.Scalar234 - Scalar24 * kv2.Scalar134 + Scalar34 * kv2.Scalar124;
            scalarArray[11] += -Scalar13 * kv2.Scalar234 + Scalar23 * kv2.Scalar134 - Scalar34 * kv2.Scalar123;
            scalarArray[13] += Scalar12 * kv2.Scalar234 - Scalar23 * kv2.Scalar124 + Scalar24 * kv2.Scalar123;
            scalarArray[14] += Scalar12 * kv2.Scalar134 - Scalar13 * kv2.Scalar124 + Scalar14 * kv2.Scalar123;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void GpUpdateMultivectorArray(CGa4Vector4D kv2, double[] scalarArray)
        {
            scalarArray[3] += -Scalar34 * kv2.Scalar1234;
            scalarArray[5] += Scalar24 * kv2.Scalar1234;
            scalarArray[6] += Scalar14 * kv2.Scalar1234;
            scalarArray[9] += -Scalar23 * kv2.Scalar1234;
            scalarArray[10] += -Scalar13 * kv2.Scalar1234;
            scalarArray[12] += Scalar12 * kv2.Scalar1234;
        }

    }

    public sealed class CGaTrivector4D
    {
        public static CGaTrivector4D Zero { get; } = new CGaTrivector4D();

        public static CGaTrivector4D E123 { get; } = new CGaTrivector4D() { Scalar123 = Float64Scalar.One };

        public static CGaTrivector4D E124 { get; } = new CGaTrivector4D() { Scalar124 = Float64Scalar.One };

        public static CGaTrivector4D E134 { get; } = new CGaTrivector4D() { Scalar134 = Float64Scalar.One };

        public static CGaTrivector4D E234 { get; } = new CGaTrivector4D() { Scalar234 = Float64Scalar.One };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaTrivector4D operator +(CGaTrivector4D mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaTrivector4D operator -(CGaTrivector4D mv)
        {
            return mv.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaTrivector4D operator *(CGaTrivector4D mv1, double mv2)
        {
            return mv1.Times(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaTrivector4D operator *(double mv1, CGaTrivector4D mv2)
        {
            return mv2.Times(mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaTrivector4D operator /(CGaTrivector4D mv1, double mv2)
        {
            return mv1.Times(1d / mv2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator +(CGaTrivector4D mv1, CGaScalar4D mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator -(CGaTrivector4D mv1, CGaScalar4D mv2)
        {
            return mv1.Subtract(mv2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator +(CGaTrivector4D mv1, CGaVector4D mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator -(CGaTrivector4D mv1, CGaVector4D mv2)
        {
            return mv1.Subtract(mv2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator +(CGaTrivector4D mv1, CGaBivector4D mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator -(CGaTrivector4D mv1, CGaBivector4D mv2)
        {
            return mv1.Subtract(mv2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaTrivector4D operator +(CGaTrivector4D mv1, CGaTrivector4D mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaTrivector4D operator -(CGaTrivector4D mv1, CGaTrivector4D mv2)
        {
            return mv1.Subtract(mv2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator +(CGaTrivector4D mv1, CGaMultivector4D mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator -(CGaTrivector4D mv1, CGaMultivector4D mv2)
        {
            return mv1.Subtract(mv2);
        }

        public Float64Scalar Scalar123 { get; init; }

        public Float64Scalar Scalar124 { get; init; }

        public Float64Scalar Scalar134 { get; init; }

        public Float64Scalar Scalar234 { get; init; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D()
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return
                Scalar123.IsValid() &&
                Scalar124.IsValid() &&
                Scalar134.IsValid() &&
                Scalar234.IsValid();
        }

        private bool? _isZero;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero()
        {
            _isZero ??=
                Scalar123.IsZero() &&
                Scalar124.IsZero() &&
                Scalar134.IsZero() &&
                Scalar234.IsZero();

            return _isZero.Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(double epsilon = 1e-12d)
        {
            return Norm().IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[] GetMultivectorArray()
        {
            var scalarArray = new double[16];

            scalarArray[7] = Scalar123;
            scalarArray[11] = Scalar124;
            scalarArray[13] = Scalar134;
            scalarArray[14] = Scalar234;

            return scalarArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[] GetKVectorArray()
        {
            return new double[]
            {
            Scalar123,
            Scalar124,
            Scalar134,
            Scalar234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar NormSquared()
        {
            return -Scalar123 * Scalar123 - Scalar124 * Scalar124 - Scalar134 * Scalar134 + Scalar234 * Scalar234;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar Norm()
        {
            return NormSquared().SqrtOfAbs();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D Times(double mv2)
        {
            return new CGaTrivector4D()
            {
                Scalar123 = Scalar123 * mv2,
                Scalar124 = Scalar124 * mv2,
                Scalar134 = Scalar134 * mv2,
                Scalar234 = Scalar234 * mv2
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D Divide(double mv2)
        {
            return Times(1d / mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D DivideByNorm()
        {
            return Times(1d / Norm());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D DivideByNormSquared()
        {
            return Times(1d / NormSquared());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D Negative()
        {
            return new CGaTrivector4D()
            {
                Scalar123 = -Scalar123,
                Scalar124 = -Scalar124,
                Scalar134 = -Scalar134,
                Scalar234 = -Scalar234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D Reverse()
        {
            return Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D GradeInvolution()
        {
            return Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D CliffordConjugate()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D Inverse()
        {
            return Times(-1d / NormSquared());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D InverseTimes(double mv2)
        {
            return Times(-mv2 / NormSquared());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D PseudoInverse()
        {
            var conjugate = Conjugate();

            return conjugate.Times(
                1d / conjugate.Sp(this).Scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D PseudoInverseTimes(double mv2)
        {
            var conjugate = Conjugate();

            return conjugate.Times(
                mv2 / conjugate.Sp(this).Scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D Conjugate()
        {
            return new CGaTrivector4D()
            {
                Scalar123 = Scalar123,
                Scalar124 = Scalar124,
                Scalar134 = Scalar134,
                Scalar234 = -Scalar234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D Dual()
        {
            return new CGaVector4D()
            {
                Scalar1 = -Scalar234,
                Scalar2 = -Scalar134,
                Scalar3 = Scalar124,
                Scalar4 = -Scalar123
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D UnDual()
        {
            return new CGaVector4D()
            {
                Scalar1 = Scalar234,
                Scalar2 = Scalar134,
                Scalar3 = -Scalar124,
                Scalar4 = Scalar123
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Dual(CGaTrivector4D kv2)
        {
            return Lcp(kv2.Inverse());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D UnDual(CGaTrivector4D kv2)
        {
            return Lcp(kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D Dual(CGa4Vector4D kv2)
        {
            return Lcp(kv2.Inverse());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D UnDual(CGa4Vector4D kv2)
        {
            return Lcp(kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Add(CGaScalar4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2);

            return CGaMultivector4D.Create(
                mv2,
                CGaVector4D.Zero,
                CGaBivector4D.Zero,
                this,
                CGa4Vector4D.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Add(CGaVector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2);

            return CGaMultivector4D.Create(
                CGaScalar4D.Zero,
                mv2,
                CGaBivector4D.Zero,
                this,
                CGa4Vector4D.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Add(CGaBivector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2);

            return CGaMultivector4D.Create(
                CGaScalar4D.Zero,
                CGaVector4D.Zero,
                mv2,
                this,
                CGa4Vector4D.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D Add(CGaTrivector4D mv2)
        {
            if (mv2.IsZero()) return this;
            if (IsZero()) return mv2;

            return new CGaTrivector4D()
            {
                Scalar123 = Scalar123 + mv2.Scalar123,
                Scalar124 = Scalar124 + mv2.Scalar124,
                Scalar134 = Scalar134 + mv2.Scalar134,
                Scalar234 = Scalar234 + mv2.Scalar234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Add(CGa4Vector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2);

            return CGaMultivector4D.Create(
                CGaScalar4D.Zero,
                CGaVector4D.Zero,
                CGaBivector4D.Zero,
                this,
                mv2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Add(CGaMultivector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return mv2;

            return CGaMultivector4D.Create(
                mv2.KVector0,
                mv2.KVector1,
                mv2.KVector2,
                Add(mv2.KVector3),
                mv2.KVector4
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Subtract(CGaScalar4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2.Negative());

            return CGaMultivector4D.Create(
                mv2.Negative(),
                CGaVector4D.Zero,
                CGaBivector4D.Zero,
                this,
                CGa4Vector4D.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Subtract(CGaVector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2.Negative());

            return CGaMultivector4D.Create(
                CGaScalar4D.Zero,
                mv2.Negative(),
                CGaBivector4D.Zero,
                this,
                CGa4Vector4D.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Subtract(CGaBivector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2.Negative());

            return CGaMultivector4D.Create(
                CGaScalar4D.Zero,
                CGaVector4D.Zero,
                mv2.Negative(),
                this,
                CGa4Vector4D.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D Subtract(CGaTrivector4D mv2)
        {
            if (mv2.IsZero()) return this;
            if (IsZero()) return mv2.Negative();

            return new CGaTrivector4D()
            {
                Scalar123 = Scalar123 - mv2.Scalar123,
                Scalar124 = Scalar124 - mv2.Scalar124,
                Scalar134 = Scalar134 - mv2.Scalar134,
                Scalar234 = Scalar234 - mv2.Scalar234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Subtract(CGa4Vector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2.Negative());

            return CGaMultivector4D.Create(
                CGaScalar4D.Zero,
                CGaVector4D.Zero,
                CGaBivector4D.Zero,
                this,
                mv2.Negative()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Subtract(CGaMultivector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return mv2.Negative();

            return CGaMultivector4D.Create(
                mv2.KVector0.Negative(),
                mv2.KVector1.Negative(),
                mv2.KVector2.Negative(),
                Subtract(mv2.KVector3),
                mv2.KVector4.Negative()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Sp(CGaScalar4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Sp(CGaVector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Sp(CGaBivector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Sp(CGaTrivector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaScalar4D.Zero;

            return new CGaScalar4D()
            {
                Scalar = Scalar123 * mv2.Scalar123 + Scalar124 * mv2.Scalar124 + Scalar134 * mv2.Scalar134 - Scalar234 * mv2.Scalar234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Sp(CGa4Vector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void SpUpdateMultivectorArray(CGaTrivector4D kv2, double[] scalarArray)
        {
            scalarArray[0] += Scalar123 * kv2.Scalar123 + Scalar124 * kv2.Scalar124 + Scalar134 * kv2.Scalar134 - Scalar234 * kv2.Scalar234;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D Op(CGaScalar4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaTrivector4D.Zero;

            return new CGaTrivector4D()
            {
                Scalar123 = Scalar123 * mv2.Scalar,
                Scalar124 = Scalar124 * mv2.Scalar,
                Scalar134 = Scalar134 * mv2.Scalar,
                Scalar234 = Scalar234 * mv2.Scalar
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGa4Vector4D Op(CGaVector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGa4Vector4D.Zero;

            return new CGa4Vector4D()
            {
                Scalar1234 = Scalar123 * mv2.Scalar4 - Scalar124 * mv2.Scalar3 + Scalar134 * mv2.Scalar2 - Scalar234 * mv2.Scalar1
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Op(CGaBivector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Op(CGaTrivector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Op(CGa4Vector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void OpUpdateMultivectorArray(CGaScalar4D kv2, double[] scalarArray)
        {
            scalarArray[7] += Scalar123 * kv2.Scalar;
            scalarArray[11] += Scalar124 * kv2.Scalar;
            scalarArray[13] += Scalar134 * kv2.Scalar;
            scalarArray[14] += Scalar234 * kv2.Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void OpUpdateMultivectorArray(CGaVector4D kv2, double[] scalarArray)
        {
            scalarArray[15] += Scalar123 * kv2.Scalar4 - Scalar124 * kv2.Scalar3 + Scalar134 * kv2.Scalar2 - Scalar234 * kv2.Scalar1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Lcp(CGaScalar4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Lcp(CGaVector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Lcp(CGaBivector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Lcp(CGaTrivector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaScalar4D.Zero;

            return new CGaScalar4D()
            {
                Scalar = Scalar123 * mv2.Scalar123 + Scalar124 * mv2.Scalar124 + Scalar134 * mv2.Scalar134 - Scalar234 * mv2.Scalar234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D Lcp(CGa4Vector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaVector4D.Zero;

            return new CGaVector4D()
            {
                Scalar1 = Scalar234 * mv2.Scalar1234,
                Scalar2 = Scalar134 * mv2.Scalar1234,
                Scalar3 = -Scalar124 * mv2.Scalar1234,
                Scalar4 = Scalar123 * mv2.Scalar1234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void LcpUpdateMultivectorArray(CGaTrivector4D kv2, double[] scalarArray)
        {
            scalarArray[0] += Scalar123 * kv2.Scalar123 + Scalar124 * kv2.Scalar124 + Scalar134 * kv2.Scalar134 - Scalar234 * kv2.Scalar234;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void LcpUpdateMultivectorArray(CGa4Vector4D kv2, double[] scalarArray)
        {
            scalarArray[1] += Scalar234 * kv2.Scalar1234;
            scalarArray[2] += Scalar134 * kv2.Scalar1234;
            scalarArray[4] += -Scalar124 * kv2.Scalar1234;
            scalarArray[8] += Scalar123 * kv2.Scalar1234;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D Rcp(CGaScalar4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaTrivector4D.Zero;

            return new CGaTrivector4D()
            {
                Scalar123 = Scalar123 * mv2.Scalar,
                Scalar124 = Scalar124 * mv2.Scalar,
                Scalar134 = Scalar134 * mv2.Scalar,
                Scalar234 = Scalar234 * mv2.Scalar
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D Rcp(CGaVector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaBivector4D.Zero;

            return new CGaBivector4D()
            {
                Scalar12 = Scalar123 * mv2.Scalar3 + Scalar124 * mv2.Scalar4,
                Scalar13 = -Scalar123 * mv2.Scalar2 + Scalar134 * mv2.Scalar4,
                Scalar23 = -Scalar123 * mv2.Scalar1 + Scalar234 * mv2.Scalar4,
                Scalar14 = -Scalar124 * mv2.Scalar2 - Scalar134 * mv2.Scalar3,
                Scalar24 = -Scalar124 * mv2.Scalar1 - Scalar234 * mv2.Scalar3,
                Scalar34 = -Scalar134 * mv2.Scalar1 + Scalar234 * mv2.Scalar2
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D Rcp(CGaBivector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaVector4D.Zero;

            return new CGaVector4D()
            {
                Scalar1 = -Scalar123 * mv2.Scalar23 - Scalar124 * mv2.Scalar24 - Scalar134 * mv2.Scalar34,
                Scalar2 = -Scalar123 * mv2.Scalar13 - Scalar124 * mv2.Scalar14 - Scalar234 * mv2.Scalar34,
                Scalar3 = Scalar123 * mv2.Scalar12 - Scalar134 * mv2.Scalar14 + Scalar234 * mv2.Scalar24,
                Scalar4 = Scalar124 * mv2.Scalar12 + Scalar134 * mv2.Scalar13 - Scalar234 * mv2.Scalar23
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Rcp(CGaTrivector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaScalar4D.Zero;

            return new CGaScalar4D()
            {
                Scalar = Scalar123 * mv2.Scalar123 + Scalar124 * mv2.Scalar124 + Scalar134 * mv2.Scalar134 - Scalar234 * mv2.Scalar234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Rcp(CGa4Vector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void RcpUpdateMultivectorArray(CGaScalar4D kv2, double[] scalarArray)
        {
            scalarArray[7] += Scalar123 * kv2.Scalar;
            scalarArray[11] += Scalar124 * kv2.Scalar;
            scalarArray[13] += Scalar134 * kv2.Scalar;
            scalarArray[14] += Scalar234 * kv2.Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void RcpUpdateMultivectorArray(CGaVector4D kv2, double[] scalarArray)
        {
            scalarArray[3] += Scalar123 * kv2.Scalar3 + Scalar124 * kv2.Scalar4;
            scalarArray[5] += -Scalar123 * kv2.Scalar2 + Scalar134 * kv2.Scalar4;
            scalarArray[6] += -Scalar123 * kv2.Scalar1 + Scalar234 * kv2.Scalar4;
            scalarArray[9] += -Scalar124 * kv2.Scalar2 - Scalar134 * kv2.Scalar3;
            scalarArray[10] += -Scalar124 * kv2.Scalar1 - Scalar234 * kv2.Scalar3;
            scalarArray[12] += -Scalar134 * kv2.Scalar1 + Scalar234 * kv2.Scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void RcpUpdateMultivectorArray(CGaBivector4D kv2, double[] scalarArray)
        {
            scalarArray[1] += -Scalar123 * kv2.Scalar23 - Scalar124 * kv2.Scalar24 - Scalar134 * kv2.Scalar34;
            scalarArray[2] += -Scalar123 * kv2.Scalar13 - Scalar124 * kv2.Scalar14 - Scalar234 * kv2.Scalar34;
            scalarArray[4] += Scalar123 * kv2.Scalar12 - Scalar134 * kv2.Scalar14 + Scalar234 * kv2.Scalar24;
            scalarArray[8] += Scalar124 * kv2.Scalar12 + Scalar134 * kv2.Scalar13 - Scalar234 * kv2.Scalar23;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void RcpUpdateMultivectorArray(CGaTrivector4D kv2, double[] scalarArray)
        {
            scalarArray[0] += Scalar123 * kv2.Scalar123 + Scalar124 * kv2.Scalar124 + Scalar134 * kv2.Scalar134 - Scalar234 * kv2.Scalar234;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D Fdp(CGaScalar4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaTrivector4D.Zero;

            return new CGaTrivector4D()
            {
                Scalar123 = Scalar123 * mv2.Scalar,
                Scalar124 = Scalar124 * mv2.Scalar,
                Scalar134 = Scalar134 * mv2.Scalar,
                Scalar234 = Scalar234 * mv2.Scalar
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D Fdp(CGaVector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaBivector4D.Zero;

            return new CGaBivector4D()
            {
                Scalar12 = Scalar123 * mv2.Scalar3 + Scalar124 * mv2.Scalar4,
                Scalar13 = -Scalar123 * mv2.Scalar2 + Scalar134 * mv2.Scalar4,
                Scalar23 = -Scalar123 * mv2.Scalar1 + Scalar234 * mv2.Scalar4,
                Scalar14 = -Scalar124 * mv2.Scalar2 - Scalar134 * mv2.Scalar3,
                Scalar24 = -Scalar124 * mv2.Scalar1 - Scalar234 * mv2.Scalar3,
                Scalar34 = -Scalar134 * mv2.Scalar1 + Scalar234 * mv2.Scalar2
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D Fdp(CGaBivector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaVector4D.Zero;

            return new CGaVector4D()
            {
                Scalar1 = -Scalar123 * mv2.Scalar23 - Scalar124 * mv2.Scalar24 - Scalar134 * mv2.Scalar34,
                Scalar2 = -Scalar123 * mv2.Scalar13 - Scalar124 * mv2.Scalar14 - Scalar234 * mv2.Scalar34,
                Scalar3 = Scalar123 * mv2.Scalar12 - Scalar134 * mv2.Scalar14 + Scalar234 * mv2.Scalar24,
                Scalar4 = Scalar124 * mv2.Scalar12 + Scalar134 * mv2.Scalar13 - Scalar234 * mv2.Scalar23
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Fdp(CGaTrivector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaScalar4D.Zero;

            return new CGaScalar4D()
            {
                Scalar = Scalar123 * mv2.Scalar123 + Scalar124 * mv2.Scalar124 + Scalar134 * mv2.Scalar134 - Scalar234 * mv2.Scalar234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D Fdp(CGa4Vector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaVector4D.Zero;

            return new CGaVector4D()
            {
                Scalar1 = Scalar234 * mv2.Scalar1234,
                Scalar2 = Scalar134 * mv2.Scalar1234,
                Scalar3 = -Scalar124 * mv2.Scalar1234,
                Scalar4 = Scalar123 * mv2.Scalar1234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void FdpUpdateMultivectorArray(CGaScalar4D kv2, double[] scalarArray)
        {
            scalarArray[7] += Scalar123 * kv2.Scalar;
            scalarArray[11] += Scalar124 * kv2.Scalar;
            scalarArray[13] += Scalar134 * kv2.Scalar;
            scalarArray[14] += Scalar234 * kv2.Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void FdpUpdateMultivectorArray(CGaVector4D kv2, double[] scalarArray)
        {
            scalarArray[3] += Scalar123 * kv2.Scalar3 + Scalar124 * kv2.Scalar4;
            scalarArray[5] += -Scalar123 * kv2.Scalar2 + Scalar134 * kv2.Scalar4;
            scalarArray[6] += -Scalar123 * kv2.Scalar1 + Scalar234 * kv2.Scalar4;
            scalarArray[9] += -Scalar124 * kv2.Scalar2 - Scalar134 * kv2.Scalar3;
            scalarArray[10] += -Scalar124 * kv2.Scalar1 - Scalar234 * kv2.Scalar3;
            scalarArray[12] += -Scalar134 * kv2.Scalar1 + Scalar234 * kv2.Scalar2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void FdpUpdateMultivectorArray(CGaBivector4D kv2, double[] scalarArray)
        {
            scalarArray[1] += -Scalar123 * kv2.Scalar23 - Scalar124 * kv2.Scalar24 - Scalar134 * kv2.Scalar34;
            scalarArray[2] += -Scalar123 * kv2.Scalar13 - Scalar124 * kv2.Scalar14 - Scalar234 * kv2.Scalar34;
            scalarArray[4] += Scalar123 * kv2.Scalar12 - Scalar134 * kv2.Scalar14 + Scalar234 * kv2.Scalar24;
            scalarArray[8] += Scalar124 * kv2.Scalar12 + Scalar134 * kv2.Scalar13 - Scalar234 * kv2.Scalar23;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void FdpUpdateMultivectorArray(CGaTrivector4D kv2, double[] scalarArray)
        {
            scalarArray[0] += Scalar123 * kv2.Scalar123 + Scalar124 * kv2.Scalar124 + Scalar134 * kv2.Scalar134 - Scalar234 * kv2.Scalar234;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void FdpUpdateMultivectorArray(CGa4Vector4D kv2, double[] scalarArray)
        {
            scalarArray[1] += Scalar234 * kv2.Scalar1234;
            scalarArray[2] += Scalar134 * kv2.Scalar1234;
            scalarArray[4] += -Scalar124 * kv2.Scalar1234;
            scalarArray[8] += Scalar123 * kv2.Scalar1234;
        }

        public CGaMultivector4D Gp(CGaScalar4D mv2)
        {
            var scalarArray = new double[16];

            if (!IsZero() && !mv2.IsZero()) GpUpdateMultivectorArray(mv2, scalarArray);

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Gp(CGaVector4D mv2)
        {
            var scalarArray = new double[16];

            if (!IsZero() && !mv2.IsZero()) GpUpdateMultivectorArray(mv2, scalarArray);

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Gp(CGaBivector4D mv2)
        {
            var scalarArray = new double[16];

            if (!IsZero() && !mv2.IsZero()) GpUpdateMultivectorArray(mv2, scalarArray);

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Gp(CGaTrivector4D mv2)
        {
            var scalarArray = new double[16];

            if (!IsZero() && !mv2.IsZero()) GpUpdateMultivectorArray(mv2, scalarArray);

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Gp(CGa4Vector4D mv2)
        {
            var scalarArray = new double[16];

            if (!IsZero() && !mv2.IsZero()) GpUpdateMultivectorArray(mv2, scalarArray);

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Gp(CGaMultivector4D mv2)
        {
            var scalarArray = new double[16];

            if (!IsZero())
            {
                if (!mv2.KVector0.IsZero()) GpUpdateMultivectorArray(mv2.KVector0, scalarArray);
                if (!mv2.KVector1.IsZero()) GpUpdateMultivectorArray(mv2.KVector1, scalarArray);
                if (!mv2.KVector2.IsZero()) GpUpdateMultivectorArray(mv2.KVector2, scalarArray);
                if (!mv2.KVector3.IsZero()) GpUpdateMultivectorArray(mv2.KVector3, scalarArray);
                if (!mv2.KVector4.IsZero()) GpUpdateMultivectorArray(mv2.KVector4, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void GpUpdateMultivectorArray(CGaScalar4D kv2, double[] scalarArray)
        {
            scalarArray[7] += Scalar123 * kv2.Scalar;
            scalarArray[11] += Scalar124 * kv2.Scalar;
            scalarArray[13] += Scalar134 * kv2.Scalar;
            scalarArray[14] += Scalar234 * kv2.Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void GpUpdateMultivectorArray(CGaVector4D kv2, double[] scalarArray)
        {
            scalarArray[3] += Scalar123 * kv2.Scalar3 + Scalar124 * kv2.Scalar4;
            scalarArray[5] += -Scalar123 * kv2.Scalar2 + Scalar134 * kv2.Scalar4;
            scalarArray[6] += -Scalar123 * kv2.Scalar1 + Scalar234 * kv2.Scalar4;
            scalarArray[9] += -Scalar124 * kv2.Scalar2 - Scalar134 * kv2.Scalar3;
            scalarArray[10] += -Scalar124 * kv2.Scalar1 - Scalar234 * kv2.Scalar3;
            scalarArray[12] += -Scalar134 * kv2.Scalar1 + Scalar234 * kv2.Scalar2;
            scalarArray[15] += Scalar123 * kv2.Scalar4 - Scalar124 * kv2.Scalar3 + Scalar134 * kv2.Scalar2 - Scalar234 * kv2.Scalar1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void GpUpdateMultivectorArray(CGaBivector4D kv2, double[] scalarArray)
        {
            scalarArray[1] += -Scalar123 * kv2.Scalar23 - Scalar124 * kv2.Scalar24 - Scalar134 * kv2.Scalar34;
            scalarArray[2] += -Scalar123 * kv2.Scalar13 - Scalar124 * kv2.Scalar14 - Scalar234 * kv2.Scalar34;
            scalarArray[4] += Scalar123 * kv2.Scalar12 - Scalar134 * kv2.Scalar14 + Scalar234 * kv2.Scalar24;
            scalarArray[8] += Scalar124 * kv2.Scalar12 + Scalar134 * kv2.Scalar13 - Scalar234 * kv2.Scalar23;
            scalarArray[7] += -Scalar124 * kv2.Scalar34 + Scalar134 * kv2.Scalar24 - Scalar234 * kv2.Scalar14;
            scalarArray[11] += Scalar123 * kv2.Scalar34 - Scalar134 * kv2.Scalar23 + Scalar234 * kv2.Scalar13;
            scalarArray[13] += -Scalar123 * kv2.Scalar24 + Scalar124 * kv2.Scalar23 - Scalar234 * kv2.Scalar12;
            scalarArray[14] += -Scalar123 * kv2.Scalar14 + Scalar124 * kv2.Scalar13 - Scalar134 * kv2.Scalar12;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void GpUpdateMultivectorArray(CGaTrivector4D kv2, double[] scalarArray)
        {
            scalarArray[0] += Scalar123 * kv2.Scalar123 + Scalar124 * kv2.Scalar124 + Scalar134 * kv2.Scalar134 - Scalar234 * kv2.Scalar234;
            scalarArray[3] += -Scalar134 * kv2.Scalar234 + Scalar234 * kv2.Scalar134;
            scalarArray[5] += Scalar124 * kv2.Scalar234 - Scalar234 * kv2.Scalar124;
            scalarArray[6] += Scalar124 * kv2.Scalar134 - Scalar134 * kv2.Scalar124;
            scalarArray[9] += -Scalar123 * kv2.Scalar234 + Scalar234 * kv2.Scalar123;
            scalarArray[10] += -Scalar123 * kv2.Scalar134 + Scalar134 * kv2.Scalar123;
            scalarArray[12] += Scalar123 * kv2.Scalar124 - Scalar124 * kv2.Scalar123;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void GpUpdateMultivectorArray(CGa4Vector4D kv2, double[] scalarArray)
        {
            scalarArray[1] += Scalar234 * kv2.Scalar1234;
            scalarArray[2] += Scalar134 * kv2.Scalar1234;
            scalarArray[4] += -Scalar124 * kv2.Scalar1234;
            scalarArray[8] += Scalar123 * kv2.Scalar1234;
        }

    }

    public sealed class CGa4Vector4D
    {
        public static CGa4Vector4D Zero { get; } = new CGa4Vector4D();

        public static CGa4Vector4D E1234 { get; } = new CGa4Vector4D() { Scalar1234 = Float64Scalar.One };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGa4Vector4D operator +(CGa4Vector4D mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGa4Vector4D operator -(CGa4Vector4D mv)
        {
            return mv.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGa4Vector4D operator *(CGa4Vector4D mv1, double mv2)
        {
            return mv1.Times(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGa4Vector4D operator *(double mv1, CGa4Vector4D mv2)
        {
            return mv2.Times(mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGa4Vector4D operator /(CGa4Vector4D mv1, double mv2)
        {
            return mv1.Times(1d / mv2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator +(CGa4Vector4D mv1, CGaScalar4D mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator -(CGa4Vector4D mv1, CGaScalar4D mv2)
        {
            return mv1.Subtract(mv2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator +(CGa4Vector4D mv1, CGaVector4D mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator -(CGa4Vector4D mv1, CGaVector4D mv2)
        {
            return mv1.Subtract(mv2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator +(CGa4Vector4D mv1, CGaBivector4D mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator -(CGa4Vector4D mv1, CGaBivector4D mv2)
        {
            return mv1.Subtract(mv2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator +(CGa4Vector4D mv1, CGaTrivector4D mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator -(CGa4Vector4D mv1, CGaTrivector4D mv2)
        {
            return mv1.Subtract(mv2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator +(CGa4Vector4D mv1, CGaMultivector4D mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator -(CGa4Vector4D mv1, CGaMultivector4D mv2)
        {
            return mv1.Subtract(mv2);
        }

        public Float64Scalar Scalar1234 { get; init; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGa4Vector4D()
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return
                Scalar1234.IsValid();
        }

        private bool? _isZero;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero()
        {
            _isZero ??=
                Scalar1234.IsZero();

            return _isZero.Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(double epsilon = 1e-12d)
        {
            return Norm().IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[] GetMultivectorArray()
        {
            var scalarArray = new double[16];

            scalarArray[15] = Scalar1234;

            return scalarArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[] GetKVectorArray()
        {
            return new double[]
            {
            Scalar1234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar NormSquared()
        {
            return -Scalar1234 * Scalar1234;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar Norm()
        {
            return NormSquared().SqrtOfAbs();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGa4Vector4D Times(double mv2)
        {
            return new CGa4Vector4D()
            {
                Scalar1234 = Scalar1234 * mv2
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGa4Vector4D Divide(double mv2)
        {
            return Times(1d / mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGa4Vector4D DivideByNorm()
        {
            return Times(1d / Norm());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGa4Vector4D DivideByNormSquared()
        {
            return Times(1d / NormSquared());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGa4Vector4D Negative()
        {
            return new CGa4Vector4D()
            {
                Scalar1234 = -Scalar1234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGa4Vector4D Reverse()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGa4Vector4D GradeInvolution()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGa4Vector4D CliffordConjugate()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGa4Vector4D Inverse()
        {
            return Times(1d / NormSquared());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGa4Vector4D InverseTimes(double mv2)
        {
            return Times(mv2 / NormSquared());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGa4Vector4D PseudoInverse()
        {
            var conjugate = Conjugate();

            return conjugate.Times(
                1d / conjugate.Sp(this).Scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGa4Vector4D PseudoInverseTimes(double mv2)
        {
            var conjugate = Conjugate();

            return conjugate.Times(
                mv2 / conjugate.Sp(this).Scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGa4Vector4D Conjugate()
        {
            return Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Dual()
        {
            return new CGaScalar4D()
            {
                Scalar = Scalar1234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D UnDual()
        {
            return new CGaScalar4D()
            {
                Scalar = -Scalar1234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Dual(CGa4Vector4D kv2)
        {
            return Lcp(kv2.Inverse());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D UnDual(CGa4Vector4D kv2)
        {
            return Lcp(kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Add(CGaScalar4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2);

            return CGaMultivector4D.Create(
                mv2,
                CGaVector4D.Zero,
                CGaBivector4D.Zero,
                CGaTrivector4D.Zero,
                this
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Add(CGaVector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2);

            return CGaMultivector4D.Create(
                CGaScalar4D.Zero,
                mv2,
                CGaBivector4D.Zero,
                CGaTrivector4D.Zero,
                this
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Add(CGaBivector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2);

            return CGaMultivector4D.Create(
                CGaScalar4D.Zero,
                CGaVector4D.Zero,
                mv2,
                CGaTrivector4D.Zero,
                this
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Add(CGaTrivector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2);

            return CGaMultivector4D.Create(
                CGaScalar4D.Zero,
                CGaVector4D.Zero,
                CGaBivector4D.Zero,
                mv2,
                this
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGa4Vector4D Add(CGa4Vector4D mv2)
        {
            if (mv2.IsZero()) return this;
            if (IsZero()) return mv2;

            return new CGa4Vector4D()
            {
                Scalar1234 = Scalar1234 + mv2.Scalar1234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Add(CGaMultivector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return mv2;

            return CGaMultivector4D.Create(
                mv2.KVector0,
                mv2.KVector1,
                mv2.KVector2,
                mv2.KVector3,
                Add(mv2.KVector4)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Subtract(CGaScalar4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2.Negative());

            return CGaMultivector4D.Create(
                mv2.Negative(),
                CGaVector4D.Zero,
                CGaBivector4D.Zero,
                CGaTrivector4D.Zero,
                this
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Subtract(CGaVector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2.Negative());

            return CGaMultivector4D.Create(
                CGaScalar4D.Zero,
                mv2.Negative(),
                CGaBivector4D.Zero,
                CGaTrivector4D.Zero,
                this
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Subtract(CGaBivector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2.Negative());

            return CGaMultivector4D.Create(
                CGaScalar4D.Zero,
                CGaVector4D.Zero,
                mv2.Negative(),
                CGaTrivector4D.Zero,
                this
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Subtract(CGaTrivector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return CGaMultivector4D.Create(mv2.Negative());

            return CGaMultivector4D.Create(
                CGaScalar4D.Zero,
                CGaVector4D.Zero,
                CGaBivector4D.Zero,
                mv2.Negative(),
                this
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGa4Vector4D Subtract(CGa4Vector4D mv2)
        {
            if (mv2.IsZero()) return this;
            if (IsZero()) return mv2.Negative();

            return new CGa4Vector4D()
            {
                Scalar1234 = Scalar1234 - mv2.Scalar1234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Subtract(CGaMultivector4D mv2)
        {
            if (mv2.IsZero()) return CGaMultivector4D.Create(this);
            if (IsZero()) return mv2.Negative();

            return CGaMultivector4D.Create(
                mv2.KVector0.Negative(),
                mv2.KVector1.Negative(),
                mv2.KVector2.Negative(),
                mv2.KVector3.Negative(),
                Subtract(mv2.KVector4)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Sp(CGaScalar4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Sp(CGaVector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Sp(CGaBivector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Sp(CGaTrivector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Sp(CGa4Vector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaScalar4D.Zero;

            return new CGaScalar4D()
            {
                Scalar = -Scalar1234 * mv2.Scalar1234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void SpUpdateMultivectorArray(CGa4Vector4D kv2, double[] scalarArray)
        {
            scalarArray[0] += -Scalar1234 * kv2.Scalar1234;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGa4Vector4D Op(CGaScalar4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGa4Vector4D.Zero;

            return new CGa4Vector4D()
            {
                Scalar1234 = Scalar1234 * mv2.Scalar
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Op(CGaVector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Op(CGaBivector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Op(CGaTrivector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Op(CGa4Vector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void OpUpdateMultivectorArray(CGaScalar4D kv2, double[] scalarArray)
        {
            scalarArray[15] += Scalar1234 * kv2.Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Lcp(CGaScalar4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Lcp(CGaVector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Lcp(CGaBivector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Lcp(CGaTrivector4D mv2)
        {
            return CGaScalar4D.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Lcp(CGa4Vector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaScalar4D.Zero;

            return new CGaScalar4D()
            {
                Scalar = -Scalar1234 * mv2.Scalar1234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void LcpUpdateMultivectorArray(CGa4Vector4D kv2, double[] scalarArray)
        {
            scalarArray[0] += -Scalar1234 * kv2.Scalar1234;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGa4Vector4D Rcp(CGaScalar4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGa4Vector4D.Zero;

            return new CGa4Vector4D()
            {
                Scalar1234 = Scalar1234 * mv2.Scalar
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D Rcp(CGaVector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaTrivector4D.Zero;

            return new CGaTrivector4D()
            {
                Scalar123 = Scalar1234 * mv2.Scalar4,
                Scalar124 = -Scalar1234 * mv2.Scalar3,
                Scalar134 = Scalar1234 * mv2.Scalar2,
                Scalar234 = Scalar1234 * mv2.Scalar1
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D Rcp(CGaBivector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaBivector4D.Zero;

            return new CGaBivector4D()
            {
                Scalar12 = -Scalar1234 * mv2.Scalar34,
                Scalar13 = Scalar1234 * mv2.Scalar24,
                Scalar23 = Scalar1234 * mv2.Scalar14,
                Scalar14 = -Scalar1234 * mv2.Scalar23,
                Scalar24 = -Scalar1234 * mv2.Scalar13,
                Scalar34 = Scalar1234 * mv2.Scalar12
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D Rcp(CGaTrivector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaVector4D.Zero;

            return new CGaVector4D()
            {
                Scalar1 = -Scalar1234 * mv2.Scalar234,
                Scalar2 = -Scalar1234 * mv2.Scalar134,
                Scalar3 = Scalar1234 * mv2.Scalar124,
                Scalar4 = -Scalar1234 * mv2.Scalar123
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Rcp(CGa4Vector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaScalar4D.Zero;

            return new CGaScalar4D()
            {
                Scalar = -Scalar1234 * mv2.Scalar1234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void RcpUpdateMultivectorArray(CGaScalar4D kv2, double[] scalarArray)
        {
            scalarArray[15] += Scalar1234 * kv2.Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void RcpUpdateMultivectorArray(CGaVector4D kv2, double[] scalarArray)
        {
            scalarArray[7] += Scalar1234 * kv2.Scalar4;
            scalarArray[11] += -Scalar1234 * kv2.Scalar3;
            scalarArray[13] += Scalar1234 * kv2.Scalar2;
            scalarArray[14] += Scalar1234 * kv2.Scalar1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void RcpUpdateMultivectorArray(CGaBivector4D kv2, double[] scalarArray)
        {
            scalarArray[3] += -Scalar1234 * kv2.Scalar34;
            scalarArray[5] += Scalar1234 * kv2.Scalar24;
            scalarArray[6] += Scalar1234 * kv2.Scalar14;
            scalarArray[9] += -Scalar1234 * kv2.Scalar23;
            scalarArray[10] += -Scalar1234 * kv2.Scalar13;
            scalarArray[12] += Scalar1234 * kv2.Scalar12;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void RcpUpdateMultivectorArray(CGaTrivector4D kv2, double[] scalarArray)
        {
            scalarArray[1] += -Scalar1234 * kv2.Scalar234;
            scalarArray[2] += -Scalar1234 * kv2.Scalar134;
            scalarArray[4] += Scalar1234 * kv2.Scalar124;
            scalarArray[8] += -Scalar1234 * kv2.Scalar123;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void RcpUpdateMultivectorArray(CGa4Vector4D kv2, double[] scalarArray)
        {
            scalarArray[0] += -Scalar1234 * kv2.Scalar1234;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGa4Vector4D Fdp(CGaScalar4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGa4Vector4D.Zero;

            return new CGa4Vector4D()
            {
                Scalar1234 = Scalar1234 * mv2.Scalar
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaTrivector4D Fdp(CGaVector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaTrivector4D.Zero;

            return new CGaTrivector4D()
            {
                Scalar123 = Scalar1234 * mv2.Scalar4,
                Scalar124 = -Scalar1234 * mv2.Scalar3,
                Scalar134 = Scalar1234 * mv2.Scalar2,
                Scalar234 = Scalar1234 * mv2.Scalar1
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaBivector4D Fdp(CGaBivector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaBivector4D.Zero;

            return new CGaBivector4D()
            {
                Scalar12 = -Scalar1234 * mv2.Scalar34,
                Scalar13 = Scalar1234 * mv2.Scalar24,
                Scalar23 = Scalar1234 * mv2.Scalar14,
                Scalar14 = -Scalar1234 * mv2.Scalar23,
                Scalar24 = -Scalar1234 * mv2.Scalar13,
                Scalar34 = Scalar1234 * mv2.Scalar12
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaVector4D Fdp(CGaTrivector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaVector4D.Zero;

            return new CGaVector4D()
            {
                Scalar1 = -Scalar1234 * mv2.Scalar234,
                Scalar2 = -Scalar1234 * mv2.Scalar134,
                Scalar3 = Scalar1234 * mv2.Scalar124,
                Scalar4 = -Scalar1234 * mv2.Scalar123
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Fdp(CGa4Vector4D mv2)
        {
            if (IsZero() || mv2.IsZero())
                return CGaScalar4D.Zero;

            return new CGaScalar4D()
            {
                Scalar = -Scalar1234 * mv2.Scalar1234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void FdpUpdateMultivectorArray(CGaScalar4D kv2, double[] scalarArray)
        {
            scalarArray[15] += Scalar1234 * kv2.Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void FdpUpdateMultivectorArray(CGaVector4D kv2, double[] scalarArray)
        {
            scalarArray[7] += Scalar1234 * kv2.Scalar4;
            scalarArray[11] += -Scalar1234 * kv2.Scalar3;
            scalarArray[13] += Scalar1234 * kv2.Scalar2;
            scalarArray[14] += Scalar1234 * kv2.Scalar1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void FdpUpdateMultivectorArray(CGaBivector4D kv2, double[] scalarArray)
        {
            scalarArray[3] += -Scalar1234 * kv2.Scalar34;
            scalarArray[5] += Scalar1234 * kv2.Scalar24;
            scalarArray[6] += Scalar1234 * kv2.Scalar14;
            scalarArray[9] += -Scalar1234 * kv2.Scalar23;
            scalarArray[10] += -Scalar1234 * kv2.Scalar13;
            scalarArray[12] += Scalar1234 * kv2.Scalar12;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void FdpUpdateMultivectorArray(CGaTrivector4D kv2, double[] scalarArray)
        {
            scalarArray[1] += -Scalar1234 * kv2.Scalar234;
            scalarArray[2] += -Scalar1234 * kv2.Scalar134;
            scalarArray[4] += Scalar1234 * kv2.Scalar124;
            scalarArray[8] += -Scalar1234 * kv2.Scalar123;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void FdpUpdateMultivectorArray(CGa4Vector4D kv2, double[] scalarArray)
        {
            scalarArray[0] += -Scalar1234 * kv2.Scalar1234;
        }

        public CGaMultivector4D Gp(CGaScalar4D mv2)
        {
            var scalarArray = new double[16];

            if (!IsZero() && !mv2.IsZero()) GpUpdateMultivectorArray(mv2, scalarArray);

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Gp(CGaVector4D mv2)
        {
            var scalarArray = new double[16];

            if (!IsZero() && !mv2.IsZero()) GpUpdateMultivectorArray(mv2, scalarArray);

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Gp(CGaBivector4D mv2)
        {
            var scalarArray = new double[16];

            if (!IsZero() && !mv2.IsZero()) GpUpdateMultivectorArray(mv2, scalarArray);

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Gp(CGaTrivector4D mv2)
        {
            var scalarArray = new double[16];

            if (!IsZero() && !mv2.IsZero()) GpUpdateMultivectorArray(mv2, scalarArray);

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Gp(CGa4Vector4D mv2)
        {
            var scalarArray = new double[16];

            if (!IsZero() && !mv2.IsZero()) GpUpdateMultivectorArray(mv2, scalarArray);

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Gp(CGaMultivector4D mv2)
        {
            var scalarArray = new double[16];

            if (!IsZero())
            {
                if (!mv2.KVector0.IsZero()) GpUpdateMultivectorArray(mv2.KVector0, scalarArray);
                if (!mv2.KVector1.IsZero()) GpUpdateMultivectorArray(mv2.KVector1, scalarArray);
                if (!mv2.KVector2.IsZero()) GpUpdateMultivectorArray(mv2.KVector2, scalarArray);
                if (!mv2.KVector3.IsZero()) GpUpdateMultivectorArray(mv2.KVector3, scalarArray);
                if (!mv2.KVector4.IsZero()) GpUpdateMultivectorArray(mv2.KVector4, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void GpUpdateMultivectorArray(CGaScalar4D kv2, double[] scalarArray)
        {
            scalarArray[15] += Scalar1234 * kv2.Scalar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void GpUpdateMultivectorArray(CGaVector4D kv2, double[] scalarArray)
        {
            scalarArray[7] += Scalar1234 * kv2.Scalar4;
            scalarArray[11] += -Scalar1234 * kv2.Scalar3;
            scalarArray[13] += Scalar1234 * kv2.Scalar2;
            scalarArray[14] += Scalar1234 * kv2.Scalar1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void GpUpdateMultivectorArray(CGaBivector4D kv2, double[] scalarArray)
        {
            scalarArray[3] += -Scalar1234 * kv2.Scalar34;
            scalarArray[5] += Scalar1234 * kv2.Scalar24;
            scalarArray[6] += Scalar1234 * kv2.Scalar14;
            scalarArray[9] += -Scalar1234 * kv2.Scalar23;
            scalarArray[10] += -Scalar1234 * kv2.Scalar13;
            scalarArray[12] += Scalar1234 * kv2.Scalar12;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void GpUpdateMultivectorArray(CGaTrivector4D kv2, double[] scalarArray)
        {
            scalarArray[1] += -Scalar1234 * kv2.Scalar234;
            scalarArray[2] += -Scalar1234 * kv2.Scalar134;
            scalarArray[4] += Scalar1234 * kv2.Scalar124;
            scalarArray[8] += -Scalar1234 * kv2.Scalar123;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void GpUpdateMultivectorArray(CGa4Vector4D kv2, double[] scalarArray)
        {
            scalarArray[0] += -Scalar1234 * kv2.Scalar1234;
        }

    }

    public sealed class CGaMultivector4D
    {
        public static CGaScalar4D Zero => CGaScalar4D.Zero;

        public static CGaScalar4D E => CGaScalar4D.E;

        public static CGaVector4D E1 => CGaVector4D.E1;

        public static CGaVector4D E2 => CGaVector4D.E2;

        public static CGaVector4D E3 => CGaVector4D.E3;

        public static CGaVector4D E4 => CGaVector4D.E4;

        public static CGaBivector4D E12 => CGaBivector4D.E12;

        public static CGaBivector4D E13 => CGaBivector4D.E13;

        public static CGaBivector4D E23 => CGaBivector4D.E23;

        public static CGaBivector4D E14 => CGaBivector4D.E14;

        public static CGaBivector4D E24 => CGaBivector4D.E24;

        public static CGaBivector4D E34 => CGaBivector4D.E34;

        public static CGaTrivector4D E123 => CGaTrivector4D.E123;

        public static CGaTrivector4D E124 => CGaTrivector4D.E124;

        public static CGaTrivector4D E134 => CGaTrivector4D.E134;

        public static CGaTrivector4D E234 => CGaTrivector4D.E234;

        public static CGa4Vector4D E1234 => CGa4Vector4D.E1234;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D Create(CGaScalar4D kVector)
        {
            return new CGaMultivector4D(
                kVector,
                CGaVector4D.Zero,
                CGaBivector4D.Zero,
                CGaTrivector4D.Zero,
                CGa4Vector4D.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D Create(CGaVector4D kVector)
        {
            return new CGaMultivector4D(
                CGaScalar4D.Zero,
                kVector,
                CGaBivector4D.Zero,
                CGaTrivector4D.Zero,
                CGa4Vector4D.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D Create(CGaBivector4D kVector)
        {
            return new CGaMultivector4D(
                CGaScalar4D.Zero,
                CGaVector4D.Zero,
                kVector,
                CGaTrivector4D.Zero,
                CGa4Vector4D.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D Create(CGaTrivector4D kVector)
        {
            return new CGaMultivector4D(
                CGaScalar4D.Zero,
                CGaVector4D.Zero,
                CGaBivector4D.Zero,
                kVector,
                CGa4Vector4D.Zero
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D Create(CGa4Vector4D kVector)
        {
            return new CGaMultivector4D(
                CGaScalar4D.Zero,
                CGaVector4D.Zero,
                CGaBivector4D.Zero,
                CGaTrivector4D.Zero,
                kVector
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D Create(CGaScalar4D kVector0, CGaVector4D kVector1, CGaBivector4D kVector2, CGaTrivector4D kVector3, CGa4Vector4D kVector4)
        {
            return new CGaMultivector4D(
                kVector0,
                kVector1,
                kVector2,
                kVector3,
                kVector4
            );
        }

        public static CGaMultivector4D Create(params double[] scalarArray)
        {
            var kVector0 = new CGaScalar4D()
            {
                Scalar = scalarArray[0]
            };

            var kVector1 = new CGaVector4D()
            {
                Scalar1 = scalarArray[1],
                Scalar2 = scalarArray[2],
                Scalar3 = scalarArray[4],
                Scalar4 = scalarArray[8]
            };

            var kVector2 = new CGaBivector4D()
            {
                Scalar12 = scalarArray[3],
                Scalar13 = scalarArray[5],
                Scalar23 = scalarArray[6],
                Scalar14 = scalarArray[9],
                Scalar24 = scalarArray[10],
                Scalar34 = scalarArray[12]
            };

            var kVector3 = new CGaTrivector4D()
            {
                Scalar123 = scalarArray[7],
                Scalar124 = scalarArray[11],
                Scalar134 = scalarArray[13],
                Scalar234 = scalarArray[14]
            };

            var kVector4 = new CGa4Vector4D()
            {
                Scalar1234 = scalarArray[15]
            };

            return new CGaMultivector4D(
                kVector0,
                kVector1,
                kVector2,
                kVector3,
                kVector4
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator +(CGaMultivector4D mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator -(CGaMultivector4D mv)
        {
            return mv.Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator *(CGaMultivector4D mv1, double mv2)
        {
            return mv1.Times(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator *(double mv1, CGaMultivector4D mv2)
        {
            return mv2.Times(mv1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator /(CGaMultivector4D mv1, double mv2)
        {
            return mv1.Times(1d / mv2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator +(CGaMultivector4D mv1, CGaScalar4D mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator -(CGaMultivector4D mv1, CGaScalar4D mv2)
        {
            return mv1.Subtract(mv2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator +(CGaMultivector4D mv1, CGaVector4D mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator -(CGaMultivector4D mv1, CGaVector4D mv2)
        {
            return mv1.Subtract(mv2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator +(CGaMultivector4D mv1, CGaBivector4D mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator -(CGaMultivector4D mv1, CGaBivector4D mv2)
        {
            return mv1.Subtract(mv2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator +(CGaMultivector4D mv1, CGaTrivector4D mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator -(CGaMultivector4D mv1, CGaTrivector4D mv2)
        {
            return mv1.Subtract(mv2);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator +(CGaMultivector4D mv1, CGaMultivector4D mv2)
        {
            return mv1.Add(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CGaMultivector4D operator -(CGaMultivector4D mv1, CGaMultivector4D mv2)
        {
            return mv1.Subtract(mv2);
        }

        public CGaScalar4D KVector0 { get; }

        public CGaVector4D KVector1 { get; }

        public CGaBivector4D KVector2 { get; }

        public CGaTrivector4D KVector3 { get; }

        public CGa4Vector4D KVector4 { get; }

        public Float64Scalar Scalar => KVector0.Scalar;

        public Float64Scalar Scalar1 => KVector1.Scalar1;

        public Float64Scalar Scalar2 => KVector1.Scalar2;

        public Float64Scalar Scalar3 => KVector1.Scalar3;

        public Float64Scalar Scalar4 => KVector1.Scalar4;

        public Float64Scalar Scalar12 => KVector2.Scalar12;

        public Float64Scalar Scalar13 => KVector2.Scalar13;

        public Float64Scalar Scalar23 => KVector2.Scalar23;

        public Float64Scalar Scalar14 => KVector2.Scalar14;

        public Float64Scalar Scalar24 => KVector2.Scalar24;

        public Float64Scalar Scalar34 => KVector2.Scalar34;

        public Float64Scalar Scalar123 => KVector3.Scalar123;

        public Float64Scalar Scalar124 => KVector3.Scalar124;

        public Float64Scalar Scalar134 => KVector3.Scalar134;

        public Float64Scalar Scalar234 => KVector3.Scalar234;

        public Float64Scalar Scalar1234 => KVector4.Scalar1234;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private CGaMultivector4D(CGaScalar4D kVector0, CGaVector4D kVector1, CGaBivector4D kVector2, CGaTrivector4D kVector3, CGa4Vector4D kVector4)
        {
            KVector0 = kVector0;
            KVector1 = kVector1;
            KVector2 = kVector2;
            KVector3 = kVector3;
            KVector4 = kVector4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return
                KVector0.IsValid() &&
                KVector1.IsValid() &&
                KVector2.IsValid() &&
                KVector3.IsValid() &&
                KVector4.IsValid();
        }

        private bool? _isZero;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero()
        {
            _isZero ??=
                KVector0.IsZero() &&
                KVector1.IsZero() &&
                KVector2.IsZero() &&
                KVector3.IsZero() &&
                KVector4.IsZero();

            return _isZero.Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(double epsilon = 1e-12d)
        {
            return Norm().IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[] GetMultivectorArray()
        {
            return new double[]
            {
            Scalar,
            Scalar1,
            Scalar2,
            Scalar12,
            Scalar3,
            Scalar13,
            Scalar23,
            Scalar123,
            Scalar4,
            Scalar14,
            Scalar24,
            Scalar124,
            Scalar34,
            Scalar134,
            Scalar234,
            Scalar1234
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[][] GetKVectorArrays()
        {
            return new double[][]
            {
            KVector0.GetKVectorArray(),
            KVector1.GetKVectorArray(),
            KVector2.GetKVectorArray(),
            KVector3.GetKVectorArray(),
            KVector4.GetKVectorArray()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar NormSquared()
        {
            return
                KVector0.NormSquared() +
                KVector1.NormSquared() +
                KVector2.NormSquared() +
                KVector3.NormSquared() +
                KVector4.NormSquared();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar Norm()
        {
            return NormSquared().SqrtOfAbs();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Times(double mv2)
        {
            return new CGaMultivector4D(
                KVector0 * mv2,
                KVector1 * mv2,
                KVector2 * mv2,
                KVector3 * mv2,
                KVector4 * mv2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Divide(double mv2)
        {
            return Times(1d / mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D DivideByNorm()
        {
            return Times(1d / Norm());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D DivideByNormSquared()
        {
            return Times(1d / NormSquared());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Negative()
        {
            return new CGaMultivector4D(
                KVector0.Negative(),
                KVector1.Negative(),
                KVector2.Negative(),
                KVector3.Negative(),
                KVector4.Negative()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Reverse()
        {
            return new CGaMultivector4D(
                KVector0,
                KVector1,
                KVector2.Negative(),
                KVector3.Negative(),
                KVector4
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D GradeInvolution()
        {
            return new CGaMultivector4D(
                KVector0,
                KVector1.Negative(),
                KVector2,
                KVector3.Negative(),
                KVector4
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D CliffordConjugate()
        {
            return new CGaMultivector4D(
                KVector0,
                KVector1.Negative(),
                KVector2.Negative(),
                KVector3,
                KVector4
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Inverse()
        {
            var mvReverse = Reverse();

            return mvReverse.Times(
                1d / mvReverse.Sp(this).Scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D InverseTimes(double mv2)
        {
            var mvReverse = Reverse();

            return mvReverse.Times(
                mv2 / mvReverse.Sp(this).Scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D PseudoInverse()
        {
            var conjugate = Conjugate();

            return conjugate.Times(
                1d / conjugate.Sp(this).Scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D PseudoInverseTimes(double mv2)
        {
            var conjugate = Conjugate();

            return conjugate.Times(
                mv2 / conjugate.Sp(this).Scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Conjugate()
        {
            return new CGaMultivector4D(
                KVector0.Conjugate(),
                KVector1.Conjugate(),
                KVector2.Conjugate(),
                KVector3.Conjugate(),
                KVector4.Conjugate()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Dual(CGaScalar4D kv2)
        {
            return Lcp(kv2.Inverse());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D UnDual(CGaScalar4D kv2)
        {
            return Lcp(kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Dual(CGaVector4D kv2)
        {
            return Lcp(kv2.Inverse());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D UnDual(CGaVector4D kv2)
        {
            return Lcp(kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Dual(CGaBivector4D kv2)
        {
            return Lcp(kv2.Inverse());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D UnDual(CGaBivector4D kv2)
        {
            return Lcp(kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Dual(CGaTrivector4D kv2)
        {
            return Lcp(kv2.Inverse());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D UnDual(CGaTrivector4D kv2)
        {
            return Lcp(kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Dual(CGa4Vector4D kv2)
        {
            return Lcp(kv2.Inverse());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D UnDual(CGa4Vector4D kv2)
        {
            return Lcp(kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Add(CGaScalar4D mv2)
        {
            if (mv2.IsZero()) return this;
            if (IsZero()) return CGaMultivector4D.Create(mv2);

            return CGaMultivector4D.Create(
                KVector0.Add(mv2),
                KVector1,
                KVector2,
                KVector3,
                KVector4
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Add(CGaVector4D mv2)
        {
            if (mv2.IsZero()) return this;
            if (IsZero()) return CGaMultivector4D.Create(mv2);

            return CGaMultivector4D.Create(
                KVector0,
                KVector1.Add(mv2),
                KVector2,
                KVector3,
                KVector4
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Add(CGaBivector4D mv2)
        {
            if (mv2.IsZero()) return this;
            if (IsZero()) return CGaMultivector4D.Create(mv2);

            return CGaMultivector4D.Create(
                KVector0,
                KVector1,
                KVector2.Add(mv2),
                KVector3,
                KVector4
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Add(CGaTrivector4D mv2)
        {
            if (mv2.IsZero()) return this;
            if (IsZero()) return CGaMultivector4D.Create(mv2);

            return CGaMultivector4D.Create(
                KVector0,
                KVector1,
                KVector2,
                KVector3.Add(mv2),
                KVector4
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Add(CGa4Vector4D mv2)
        {
            if (mv2.IsZero()) return this;
            if (IsZero()) return CGaMultivector4D.Create(mv2);

            return CGaMultivector4D.Create(
                KVector0,
                KVector1,
                KVector2,
                KVector3,
                KVector4.Add(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Add(CGaMultivector4D mv2)
        {
            if (mv2.IsZero()) return this;
            if (IsZero()) return mv2;

            return CGaMultivector4D.Create(
                KVector0.Add(mv2.KVector0),
                KVector1.Add(mv2.KVector1),
                KVector2.Add(mv2.KVector2),
                KVector3.Add(mv2.KVector3),
                KVector4.Add(mv2.KVector4)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Subtract(CGaScalar4D mv2)
        {
            if (mv2.IsZero()) return this;
            if (IsZero()) return CGaMultivector4D.Create(mv2.Negative());

            return CGaMultivector4D.Create(
                KVector0.Subtract(mv2),
                KVector1,
                KVector2,
                KVector3,
                KVector4
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Subtract(CGaVector4D mv2)
        {
            if (mv2.IsZero()) return this;
            if (IsZero()) return CGaMultivector4D.Create(mv2.Negative());

            return CGaMultivector4D.Create(
                KVector0,
                KVector1.Subtract(mv2),
                KVector2,
                KVector3,
                KVector4
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Subtract(CGaBivector4D mv2)
        {
            if (mv2.IsZero()) return this;
            if (IsZero()) return CGaMultivector4D.Create(mv2.Negative());

            return CGaMultivector4D.Create(
                KVector0,
                KVector1,
                KVector2.Subtract(mv2),
                KVector3,
                KVector4
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Subtract(CGaTrivector4D mv2)
        {
            if (mv2.IsZero()) return this;
            if (IsZero()) return CGaMultivector4D.Create(mv2.Negative());

            return CGaMultivector4D.Create(
                KVector0,
                KVector1,
                KVector2,
                KVector3.Subtract(mv2),
                KVector4
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Subtract(CGa4Vector4D mv2)
        {
            if (mv2.IsZero()) return this;
            if (IsZero()) return CGaMultivector4D.Create(mv2.Negative());

            return CGaMultivector4D.Create(
                KVector0,
                KVector1,
                KVector2,
                KVector3,
                KVector4.Subtract(mv2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaMultivector4D Subtract(CGaMultivector4D mv2)
        {
            if (mv2.IsZero()) return this;
            if (IsZero()) return mv2.Negative();

            return CGaMultivector4D.Create(
                KVector0.Subtract(mv2.KVector0),
                KVector1.Subtract(mv2.KVector1),
                KVector2.Subtract(mv2.KVector2),
                KVector3.Subtract(mv2.KVector3),
                KVector4.Subtract(mv2.KVector4)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Sp(CGaScalar4D mv2)
        {
            return KVector0.Sp(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Sp(CGaVector4D mv2)
        {
            return KVector1.Sp(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Sp(CGaBivector4D mv2)
        {
            return KVector2.Sp(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Sp(CGaTrivector4D mv2)
        {
            return KVector3.Sp(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Sp(CGa4Vector4D mv2)
        {
            return KVector4.Sp(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CGaScalar4D Sp(CGaMultivector4D mv2)
        {
            return
                KVector0.Sp(mv2.KVector0) +
                KVector1.Sp(mv2.KVector1) +
                KVector2.Sp(mv2.KVector2) +
                KVector3.Sp(mv2.KVector3) +
                KVector4.Sp(mv2.KVector4);
        }

        public CGaMultivector4D Op(CGaScalar4D mv2)
        {
            var scalarArray = new double[16];

            if (!mv2.IsZero())
            {
                if (!KVector0.IsZero()) KVector0.OpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector1.IsZero()) KVector1.OpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector2.IsZero()) KVector2.OpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector3.IsZero()) KVector3.OpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector4.IsZero()) KVector4.OpUpdateMultivectorArray(mv2, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Op(CGaVector4D mv2)
        {
            var scalarArray = new double[16];

            if (!mv2.IsZero())
            {
                if (!KVector0.IsZero()) KVector0.OpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector1.IsZero()) KVector1.OpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector2.IsZero()) KVector2.OpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector3.IsZero()) KVector3.OpUpdateMultivectorArray(mv2, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Op(CGaBivector4D mv2)
        {
            var scalarArray = new double[16];

            if (!mv2.IsZero())
            {
                if (!KVector0.IsZero()) KVector0.OpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector1.IsZero()) KVector1.OpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector2.IsZero()) KVector2.OpUpdateMultivectorArray(mv2, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Op(CGaTrivector4D mv2)
        {
            var scalarArray = new double[16];

            if (!mv2.IsZero())
            {
                if (!KVector0.IsZero()) KVector0.OpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector1.IsZero()) KVector1.OpUpdateMultivectorArray(mv2, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Op(CGa4Vector4D mv2)
        {
            var scalarArray = new double[16];

            if (!mv2.IsZero())
            {
                if (!KVector0.IsZero()) KVector0.OpUpdateMultivectorArray(mv2, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Op(CGaMultivector4D mv2)
        {
            var scalarArray = new double[16];

            if (!KVector0.IsZero())
            {
                if (!mv2.KVector0.IsZero()) KVector0.OpUpdateMultivectorArray(mv2.KVector0, scalarArray);
                if (!mv2.KVector1.IsZero()) KVector0.OpUpdateMultivectorArray(mv2.KVector1, scalarArray);
                if (!mv2.KVector2.IsZero()) KVector0.OpUpdateMultivectorArray(mv2.KVector2, scalarArray);
                if (!mv2.KVector3.IsZero()) KVector0.OpUpdateMultivectorArray(mv2.KVector3, scalarArray);
                if (!mv2.KVector4.IsZero()) KVector0.OpUpdateMultivectorArray(mv2.KVector4, scalarArray);
            }

            if (!KVector1.IsZero())
            {
                if (!mv2.KVector0.IsZero()) KVector1.OpUpdateMultivectorArray(mv2.KVector0, scalarArray);
                if (!mv2.KVector1.IsZero()) KVector1.OpUpdateMultivectorArray(mv2.KVector1, scalarArray);
                if (!mv2.KVector2.IsZero()) KVector1.OpUpdateMultivectorArray(mv2.KVector2, scalarArray);
                if (!mv2.KVector3.IsZero()) KVector1.OpUpdateMultivectorArray(mv2.KVector3, scalarArray);
            }

            if (!KVector2.IsZero())
            {
                if (!mv2.KVector0.IsZero()) KVector2.OpUpdateMultivectorArray(mv2.KVector0, scalarArray);
                if (!mv2.KVector1.IsZero()) KVector2.OpUpdateMultivectorArray(mv2.KVector1, scalarArray);
                if (!mv2.KVector2.IsZero()) KVector2.OpUpdateMultivectorArray(mv2.KVector2, scalarArray);
            }

            if (!KVector3.IsZero())
            {
                if (!mv2.KVector0.IsZero()) KVector3.OpUpdateMultivectorArray(mv2.KVector0, scalarArray);
                if (!mv2.KVector1.IsZero()) KVector3.OpUpdateMultivectorArray(mv2.KVector1, scalarArray);
            }

            if (!KVector4.IsZero())
            {
                if (!mv2.KVector0.IsZero()) KVector4.OpUpdateMultivectorArray(mv2.KVector0, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Lcp(CGaScalar4D mv2)
        {
            var scalarArray = new double[16];

            if (!mv2.IsZero())
            {
                if (!KVector0.IsZero()) KVector0.LcpUpdateMultivectorArray(mv2, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Lcp(CGaVector4D mv2)
        {
            var scalarArray = new double[16];

            if (!mv2.IsZero())
            {
                if (!KVector0.IsZero()) KVector0.LcpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector1.IsZero()) KVector1.LcpUpdateMultivectorArray(mv2, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Lcp(CGaBivector4D mv2)
        {
            var scalarArray = new double[16];

            if (!mv2.IsZero())
            {
                if (!KVector0.IsZero()) KVector0.LcpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector1.IsZero()) KVector1.LcpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector2.IsZero()) KVector2.LcpUpdateMultivectorArray(mv2, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Lcp(CGaTrivector4D mv2)
        {
            var scalarArray = new double[16];

            if (!mv2.IsZero())
            {
                if (!KVector0.IsZero()) KVector0.LcpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector1.IsZero()) KVector1.LcpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector2.IsZero()) KVector2.LcpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector3.IsZero()) KVector3.LcpUpdateMultivectorArray(mv2, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Lcp(CGa4Vector4D mv2)
        {
            var scalarArray = new double[16];

            if (!mv2.IsZero())
            {
                if (!KVector0.IsZero()) KVector0.LcpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector1.IsZero()) KVector1.LcpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector2.IsZero()) KVector2.LcpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector3.IsZero()) KVector3.LcpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector4.IsZero()) KVector4.LcpUpdateMultivectorArray(mv2, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Lcp(CGaMultivector4D mv2)
        {
            var scalarArray = new double[16];

            if (!KVector0.IsZero())
            {
                if (!mv2.KVector0.IsZero()) KVector0.LcpUpdateMultivectorArray(mv2.KVector0, scalarArray);
                if (!mv2.KVector1.IsZero()) KVector0.LcpUpdateMultivectorArray(mv2.KVector1, scalarArray);
                if (!mv2.KVector2.IsZero()) KVector0.LcpUpdateMultivectorArray(mv2.KVector2, scalarArray);
                if (!mv2.KVector3.IsZero()) KVector0.LcpUpdateMultivectorArray(mv2.KVector3, scalarArray);
                if (!mv2.KVector4.IsZero()) KVector0.LcpUpdateMultivectorArray(mv2.KVector4, scalarArray);
            }

            if (!KVector1.IsZero())
            {
                if (!mv2.KVector1.IsZero()) KVector1.LcpUpdateMultivectorArray(mv2.KVector1, scalarArray);
                if (!mv2.KVector2.IsZero()) KVector1.LcpUpdateMultivectorArray(mv2.KVector2, scalarArray);
                if (!mv2.KVector3.IsZero()) KVector1.LcpUpdateMultivectorArray(mv2.KVector3, scalarArray);
                if (!mv2.KVector4.IsZero()) KVector1.LcpUpdateMultivectorArray(mv2.KVector4, scalarArray);
            }

            if (!KVector2.IsZero())
            {
                if (!mv2.KVector2.IsZero()) KVector2.LcpUpdateMultivectorArray(mv2.KVector2, scalarArray);
                if (!mv2.KVector3.IsZero()) KVector2.LcpUpdateMultivectorArray(mv2.KVector3, scalarArray);
                if (!mv2.KVector4.IsZero()) KVector2.LcpUpdateMultivectorArray(mv2.KVector4, scalarArray);
            }

            if (!KVector3.IsZero())
            {
                if (!mv2.KVector3.IsZero()) KVector3.LcpUpdateMultivectorArray(mv2.KVector3, scalarArray);
                if (!mv2.KVector4.IsZero()) KVector3.LcpUpdateMultivectorArray(mv2.KVector4, scalarArray);
            }

            if (!KVector4.IsZero())
            {
                if (!mv2.KVector4.IsZero()) KVector4.LcpUpdateMultivectorArray(mv2.KVector4, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Rcp(CGaScalar4D mv2)
        {
            var scalarArray = new double[16];

            if (!mv2.IsZero())
            {
                if (!KVector0.IsZero()) KVector0.RcpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector1.IsZero()) KVector1.RcpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector2.IsZero()) KVector2.RcpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector3.IsZero()) KVector3.RcpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector4.IsZero()) KVector4.RcpUpdateMultivectorArray(mv2, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Rcp(CGaVector4D mv2)
        {
            var scalarArray = new double[16];

            if (!mv2.IsZero())
            {
                if (!KVector1.IsZero()) KVector1.RcpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector2.IsZero()) KVector2.RcpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector3.IsZero()) KVector3.RcpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector4.IsZero()) KVector4.RcpUpdateMultivectorArray(mv2, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Rcp(CGaBivector4D mv2)
        {
            var scalarArray = new double[16];

            if (!mv2.IsZero())
            {
                if (!KVector2.IsZero()) KVector2.RcpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector3.IsZero()) KVector3.RcpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector4.IsZero()) KVector4.RcpUpdateMultivectorArray(mv2, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Rcp(CGaTrivector4D mv2)
        {
            var scalarArray = new double[16];

            if (!mv2.IsZero())
            {
                if (!KVector3.IsZero()) KVector3.RcpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector4.IsZero()) KVector4.RcpUpdateMultivectorArray(mv2, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Rcp(CGa4Vector4D mv2)
        {
            var scalarArray = new double[16];

            if (!mv2.IsZero())
            {
                if (!KVector4.IsZero()) KVector4.RcpUpdateMultivectorArray(mv2, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Rcp(CGaMultivector4D mv2)
        {
            var scalarArray = new double[16];

            if (!KVector0.IsZero())
            {
                if (!mv2.KVector0.IsZero()) KVector0.RcpUpdateMultivectorArray(mv2.KVector0, scalarArray);
            }

            if (!KVector1.IsZero())
            {
                if (!mv2.KVector0.IsZero()) KVector1.RcpUpdateMultivectorArray(mv2.KVector0, scalarArray);
                if (!mv2.KVector1.IsZero()) KVector1.RcpUpdateMultivectorArray(mv2.KVector1, scalarArray);
            }

            if (!KVector2.IsZero())
            {
                if (!mv2.KVector0.IsZero()) KVector2.RcpUpdateMultivectorArray(mv2.KVector0, scalarArray);
                if (!mv2.KVector1.IsZero()) KVector2.RcpUpdateMultivectorArray(mv2.KVector1, scalarArray);
                if (!mv2.KVector2.IsZero()) KVector2.RcpUpdateMultivectorArray(mv2.KVector2, scalarArray);
            }

            if (!KVector3.IsZero())
            {
                if (!mv2.KVector0.IsZero()) KVector3.RcpUpdateMultivectorArray(mv2.KVector0, scalarArray);
                if (!mv2.KVector1.IsZero()) KVector3.RcpUpdateMultivectorArray(mv2.KVector1, scalarArray);
                if (!mv2.KVector2.IsZero()) KVector3.RcpUpdateMultivectorArray(mv2.KVector2, scalarArray);
                if (!mv2.KVector3.IsZero()) KVector3.RcpUpdateMultivectorArray(mv2.KVector3, scalarArray);
            }

            if (!KVector4.IsZero())
            {
                if (!mv2.KVector0.IsZero()) KVector4.RcpUpdateMultivectorArray(mv2.KVector0, scalarArray);
                if (!mv2.KVector1.IsZero()) KVector4.RcpUpdateMultivectorArray(mv2.KVector1, scalarArray);
                if (!mv2.KVector2.IsZero()) KVector4.RcpUpdateMultivectorArray(mv2.KVector2, scalarArray);
                if (!mv2.KVector3.IsZero()) KVector4.RcpUpdateMultivectorArray(mv2.KVector3, scalarArray);
                if (!mv2.KVector4.IsZero()) KVector4.RcpUpdateMultivectorArray(mv2.KVector4, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Fdp(CGaScalar4D mv2)
        {
            var scalarArray = new double[16];

            if (!mv2.IsZero())
            {
                if (!KVector0.IsZero()) KVector0.FdpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector1.IsZero()) KVector1.FdpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector2.IsZero()) KVector2.FdpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector3.IsZero()) KVector3.FdpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector4.IsZero()) KVector4.FdpUpdateMultivectorArray(mv2, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Fdp(CGaVector4D mv2)
        {
            var scalarArray = new double[16];

            if (!mv2.IsZero())
            {
                if (!KVector0.IsZero()) KVector0.FdpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector1.IsZero()) KVector1.FdpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector2.IsZero()) KVector2.FdpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector3.IsZero()) KVector3.FdpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector4.IsZero()) KVector4.FdpUpdateMultivectorArray(mv2, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Fdp(CGaBivector4D mv2)
        {
            var scalarArray = new double[16];

            if (!mv2.IsZero())
            {
                if (!KVector0.IsZero()) KVector0.FdpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector1.IsZero()) KVector1.FdpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector2.IsZero()) KVector2.FdpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector3.IsZero()) KVector3.FdpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector4.IsZero()) KVector4.FdpUpdateMultivectorArray(mv2, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Fdp(CGaTrivector4D mv2)
        {
            var scalarArray = new double[16];

            if (!mv2.IsZero())
            {
                if (!KVector0.IsZero()) KVector0.FdpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector1.IsZero()) KVector1.FdpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector2.IsZero()) KVector2.FdpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector3.IsZero()) KVector3.FdpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector4.IsZero()) KVector4.FdpUpdateMultivectorArray(mv2, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Fdp(CGa4Vector4D mv2)
        {
            var scalarArray = new double[16];

            if (!mv2.IsZero())
            {
                if (!KVector0.IsZero()) KVector0.FdpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector1.IsZero()) KVector1.FdpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector2.IsZero()) KVector2.FdpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector3.IsZero()) KVector3.FdpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector4.IsZero()) KVector4.FdpUpdateMultivectorArray(mv2, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Fdp(CGaMultivector4D mv2)
        {
            var scalarArray = new double[16];

            if (!KVector0.IsZero())
            {
                if (!mv2.KVector0.IsZero()) KVector0.FdpUpdateMultivectorArray(mv2.KVector0, scalarArray);
                if (!mv2.KVector1.IsZero()) KVector0.FdpUpdateMultivectorArray(mv2.KVector1, scalarArray);
                if (!mv2.KVector2.IsZero()) KVector0.FdpUpdateMultivectorArray(mv2.KVector2, scalarArray);
                if (!mv2.KVector3.IsZero()) KVector0.FdpUpdateMultivectorArray(mv2.KVector3, scalarArray);
                if (!mv2.KVector4.IsZero()) KVector0.FdpUpdateMultivectorArray(mv2.KVector4, scalarArray);
            }

            if (!KVector1.IsZero())
            {
                if (!mv2.KVector0.IsZero()) KVector1.FdpUpdateMultivectorArray(mv2.KVector0, scalarArray);
                if (!mv2.KVector1.IsZero()) KVector1.FdpUpdateMultivectorArray(mv2.KVector1, scalarArray);
                if (!mv2.KVector2.IsZero()) KVector1.FdpUpdateMultivectorArray(mv2.KVector2, scalarArray);
                if (!mv2.KVector3.IsZero()) KVector1.FdpUpdateMultivectorArray(mv2.KVector3, scalarArray);
                if (!mv2.KVector4.IsZero()) KVector1.FdpUpdateMultivectorArray(mv2.KVector4, scalarArray);
            }

            if (!KVector2.IsZero())
            {
                if (!mv2.KVector0.IsZero()) KVector2.FdpUpdateMultivectorArray(mv2.KVector0, scalarArray);
                if (!mv2.KVector1.IsZero()) KVector2.FdpUpdateMultivectorArray(mv2.KVector1, scalarArray);
                if (!mv2.KVector2.IsZero()) KVector2.FdpUpdateMultivectorArray(mv2.KVector2, scalarArray);
                if (!mv2.KVector3.IsZero()) KVector2.FdpUpdateMultivectorArray(mv2.KVector3, scalarArray);
                if (!mv2.KVector4.IsZero()) KVector2.FdpUpdateMultivectorArray(mv2.KVector4, scalarArray);
            }

            if (!KVector3.IsZero())
            {
                if (!mv2.KVector0.IsZero()) KVector3.FdpUpdateMultivectorArray(mv2.KVector0, scalarArray);
                if (!mv2.KVector1.IsZero()) KVector3.FdpUpdateMultivectorArray(mv2.KVector1, scalarArray);
                if (!mv2.KVector2.IsZero()) KVector3.FdpUpdateMultivectorArray(mv2.KVector2, scalarArray);
                if (!mv2.KVector3.IsZero()) KVector3.FdpUpdateMultivectorArray(mv2.KVector3, scalarArray);
                if (!mv2.KVector4.IsZero()) KVector3.FdpUpdateMultivectorArray(mv2.KVector4, scalarArray);
            }

            if (!KVector4.IsZero())
            {
                if (!mv2.KVector0.IsZero()) KVector4.FdpUpdateMultivectorArray(mv2.KVector0, scalarArray);
                if (!mv2.KVector1.IsZero()) KVector4.FdpUpdateMultivectorArray(mv2.KVector1, scalarArray);
                if (!mv2.KVector2.IsZero()) KVector4.FdpUpdateMultivectorArray(mv2.KVector2, scalarArray);
                if (!mv2.KVector3.IsZero()) KVector4.FdpUpdateMultivectorArray(mv2.KVector3, scalarArray);
                if (!mv2.KVector4.IsZero()) KVector4.FdpUpdateMultivectorArray(mv2.KVector4, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Gp(CGaScalar4D mv2)
        {
            var scalarArray = new double[16];

            if (!mv2.IsZero())
            {
                if (!KVector0.IsZero()) KVector0.GpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector1.IsZero()) KVector1.GpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector2.IsZero()) KVector2.GpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector3.IsZero()) KVector3.GpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector4.IsZero()) KVector4.GpUpdateMultivectorArray(mv2, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Gp(CGaVector4D mv2)
        {
            var scalarArray = new double[16];

            if (!mv2.IsZero())
            {
                if (!KVector0.IsZero()) KVector0.GpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector1.IsZero()) KVector1.GpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector2.IsZero()) KVector2.GpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector3.IsZero()) KVector3.GpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector4.IsZero()) KVector4.GpUpdateMultivectorArray(mv2, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Gp(CGaBivector4D mv2)
        {
            var scalarArray = new double[16];

            if (!mv2.IsZero())
            {
                if (!KVector0.IsZero()) KVector0.GpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector1.IsZero()) KVector1.GpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector2.IsZero()) KVector2.GpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector3.IsZero()) KVector3.GpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector4.IsZero()) KVector4.GpUpdateMultivectorArray(mv2, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Gp(CGaTrivector4D mv2)
        {
            var scalarArray = new double[16];

            if (!mv2.IsZero())
            {
                if (!KVector0.IsZero()) KVector0.GpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector1.IsZero()) KVector1.GpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector2.IsZero()) KVector2.GpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector3.IsZero()) KVector3.GpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector4.IsZero()) KVector4.GpUpdateMultivectorArray(mv2, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Gp(CGa4Vector4D mv2)
        {
            var scalarArray = new double[16];

            if (!mv2.IsZero())
            {
                if (!KVector0.IsZero()) KVector0.GpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector1.IsZero()) KVector1.GpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector2.IsZero()) KVector2.GpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector3.IsZero()) KVector3.GpUpdateMultivectorArray(mv2, scalarArray);
                if (!KVector4.IsZero()) KVector4.GpUpdateMultivectorArray(mv2, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

        public CGaMultivector4D Gp(CGaMultivector4D mv2)
        {
            var scalarArray = new double[16];

            if (!KVector0.IsZero())
            {
                if (!mv2.KVector0.IsZero()) KVector0.GpUpdateMultivectorArray(mv2.KVector0, scalarArray);
                if (!mv2.KVector1.IsZero()) KVector0.GpUpdateMultivectorArray(mv2.KVector1, scalarArray);
                if (!mv2.KVector2.IsZero()) KVector0.GpUpdateMultivectorArray(mv2.KVector2, scalarArray);
                if (!mv2.KVector3.IsZero()) KVector0.GpUpdateMultivectorArray(mv2.KVector3, scalarArray);
                if (!mv2.KVector4.IsZero()) KVector0.GpUpdateMultivectorArray(mv2.KVector4, scalarArray);
            }

            if (!KVector1.IsZero())
            {
                if (!mv2.KVector0.IsZero()) KVector1.GpUpdateMultivectorArray(mv2.KVector0, scalarArray);
                if (!mv2.KVector1.IsZero()) KVector1.GpUpdateMultivectorArray(mv2.KVector1, scalarArray);
                if (!mv2.KVector2.IsZero()) KVector1.GpUpdateMultivectorArray(mv2.KVector2, scalarArray);
                if (!mv2.KVector3.IsZero()) KVector1.GpUpdateMultivectorArray(mv2.KVector3, scalarArray);
                if (!mv2.KVector4.IsZero()) KVector1.GpUpdateMultivectorArray(mv2.KVector4, scalarArray);
            }

            if (!KVector2.IsZero())
            {
                if (!mv2.KVector0.IsZero()) KVector2.GpUpdateMultivectorArray(mv2.KVector0, scalarArray);
                if (!mv2.KVector1.IsZero()) KVector2.GpUpdateMultivectorArray(mv2.KVector1, scalarArray);
                if (!mv2.KVector2.IsZero()) KVector2.GpUpdateMultivectorArray(mv2.KVector2, scalarArray);
                if (!mv2.KVector3.IsZero()) KVector2.GpUpdateMultivectorArray(mv2.KVector3, scalarArray);
                if (!mv2.KVector4.IsZero()) KVector2.GpUpdateMultivectorArray(mv2.KVector4, scalarArray);
            }

            if (!KVector3.IsZero())
            {
                if (!mv2.KVector0.IsZero()) KVector3.GpUpdateMultivectorArray(mv2.KVector0, scalarArray);
                if (!mv2.KVector1.IsZero()) KVector3.GpUpdateMultivectorArray(mv2.KVector1, scalarArray);
                if (!mv2.KVector2.IsZero()) KVector3.GpUpdateMultivectorArray(mv2.KVector2, scalarArray);
                if (!mv2.KVector3.IsZero()) KVector3.GpUpdateMultivectorArray(mv2.KVector3, scalarArray);
                if (!mv2.KVector4.IsZero()) KVector3.GpUpdateMultivectorArray(mv2.KVector4, scalarArray);
            }

            if (!KVector4.IsZero())
            {
                if (!mv2.KVector0.IsZero()) KVector4.GpUpdateMultivectorArray(mv2.KVector0, scalarArray);
                if (!mv2.KVector1.IsZero()) KVector4.GpUpdateMultivectorArray(mv2.KVector1, scalarArray);
                if (!mv2.KVector2.IsZero()) KVector4.GpUpdateMultivectorArray(mv2.KVector2, scalarArray);
                if (!mv2.KVector3.IsZero()) KVector4.GpUpdateMultivectorArray(mv2.KVector3, scalarArray);
                if (!mv2.KVector4.IsZero()) KVector4.GpUpdateMultivectorArray(mv2.KVector4, scalarArray);
            }

            return CGaMultivector4D.Create(scalarArray);
        }

    }

}
