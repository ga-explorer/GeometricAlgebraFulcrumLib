using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors
{
    public abstract partial class XGaMultivector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> ERcp(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }

        public abstract XGaMultivector<T> ERcp(XGaVector<T> kv2);

        public abstract XGaMultivector<T> ERcp(XGaBivector<T> kv2);

        public abstract XGaMultivector<T> ERcp(XGaHigherKVector<T> kv2);
        
        public abstract XGaMultivector<T> ERcp(XGaGradedMultivector<T> mv2);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> ERcp(XGaUniformMultivector<T> mv2)
        {
            if (this is XGaScalar<T> s1)
                return mv2.TryGetScalarValue(out var s2)
                    ? Processor.Scalar(ScalarProcessor.Times(s1.ScalarValue, s2))
                    : Processor.ScalarZero;

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddERcpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> ERcp(XGaKVector<T> kv2)
        {
            return kv2 switch
            {
                XGaScalar<T> s2 => ERcp(s2),
                XGaVector<T> v2 => ERcp(v2),
                XGaBivector<T> bv2 => ERcp(bv2),
                XGaHigherKVector<T> hkv2 => ERcp(hkv2),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> ERcp(XGaMultivector<T> mv2)
        {
            return mv2 switch
            {
                XGaScalar<T> s2 => ERcp(s2),
                XGaVector<T> v2 => ERcp(v2),
                XGaBivector<T> bv2 => ERcp(bv2),
                XGaHigherKVector<T> kv2 => ERcp(kv2),
                XGaGradedMultivector<T> gmv2 => ERcp(gmv2),
                XGaUniformMultivector<T> umv2 => ERcp(umv2),
                _ => throw new InvalidOperationException()
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> Rcp(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }

        public abstract XGaMultivector<T> Rcp(XGaVector<T> kv2);

        public abstract XGaMultivector<T> Rcp(XGaBivector<T> kv2);

        public abstract XGaMultivector<T> Rcp(XGaHigherKVector<T> kv2);
        
        public abstract XGaMultivector<T> Rcp(XGaGradedMultivector<T> mv2);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Rcp(XGaUniformMultivector<T> mv2)
        {
            if (this is XGaScalar<T> s1)
                return mv2.TryGetScalarValue(out var s2)
                    ? Processor.Scalar(ScalarProcessor.Times(s1.ScalarValue, s2))
                    : Processor.ScalarZero;

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddRcpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> Rcp(XGaKVector<T> kv2)
        {
            return kv2 switch
            {
                XGaScalar<T> s2 => Rcp(s2),
                XGaVector<T> v2 => Rcp(v2),
                XGaBivector<T> bv2 => Rcp(bv2),
                XGaHigherKVector<T> hkv2 => Rcp(hkv2),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Rcp(XGaMultivector<T> mv2)
        {
            return mv2 switch
            {
                XGaScalar<T> s2 => Rcp(s2),
                XGaVector<T> v2 => Rcp(v2),
                XGaBivector<T> bv2 => Rcp(bv2),
                XGaHigherKVector<T> kv2 => Rcp(kv2),
                XGaGradedMultivector<T> gmv2 => Rcp(gmv2),
                XGaUniformMultivector<T> umv2 => Rcp(umv2),
                _ => throw new InvalidOperationException()
            };
        }

    }

    public abstract partial class XGaKVector<T>
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> ERcp(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> ERcp(XGaVector<T> kv2)
        {
            if (IsZero || kv2.IsZero || Grade < 1)
                return Processor.ScalarZero;

            if (this is XGaVector<T> v1)
                return ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddESpTerms(v1, kv2)
                    .GetXGaScalar(Processor);

            return Processor
                .CreateKVectorComposer(Grade - 1)
                .AddERcpTerms(this, kv2)
                .GetKVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> ERcp(XGaBivector<T> kv2)
        {
            if (IsZero || kv2.IsZero || Grade < 2)
                return Processor.ScalarZero;

            if (this is XGaBivector<T> bv1)
                return ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddESpTerms(bv1, kv2)
                    .GetXGaScalar(Processor);

            return Processor
                .CreateKVectorComposer(Grade - 2)
                .AddERcpTerms(this, kv2)
                .GetKVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> ERcp(XGaHigherKVector<T> kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return (Grade - kv2.Grade) switch
            {
                < 0 => Processor.ScalarZero,

                0 => ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddESpTerms(this, kv2)
                    .GetXGaScalar(Processor),

                _ => Processor
                    .CreateKVectorComposer(Grade - kv2.Grade)
                    .AddERcpTerms(this, kv2)
                    .GetKVector()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> ERcp(XGaKVector<T> kv2)
        {
            if (IsZero || kv2.IsZero || Grade < kv2.Grade)
                return Processor.ScalarZero;

            if (this is XGaScalar<T> s1)
                return kv2 is XGaScalar<T> s2
                    ? Processor.Scalar(ScalarProcessor.Times(s1.ScalarValue, s2.ScalarValue).ScalarValue)
                    : Processor.ScalarZero;

            if (kv2 is XGaScalar<T> s3)
                return Times(s3.ScalarValue);

            if (Grade == kv2.Grade)
                return ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddESpTerms(this, kv2)
                    .GetXGaScalar(Processor);

            return Processor
                .CreateKVectorComposer(Grade - kv2.Grade)
                .AddERcpTerms(this, kv2)
                .GetKVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> ERcp(XGaGradedMultivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.ScalarZero;

            if (this is XGaScalar<T> s1)
                return Processor.Scalar(ScalarProcessor.Times(s1.ScalarValue, mv2.Scalar().ScalarValue));

            return Processor
                .CreateMultivectorComposer()
                .AddERcpTerms(this, mv2)
                .GetSimpleMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> Rcp(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> Rcp(XGaVector<T> kv2)
        {
            if (IsZero || kv2.IsZero || Grade < 1)
                return Processor.ScalarZero;

            if (this is XGaVector<T> v1)
                return ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddSpTerms(v1, kv2)
                    .GetXGaScalar(Processor);

            return Processor
                .CreateKVectorComposer(Grade - 1)
                .AddRcpTerms(this, kv2)
                .GetKVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> Rcp(XGaBivector<T> kv2)
        {
            if (IsZero || kv2.IsZero || Grade < 2)
                return Processor.ScalarZero;

            if (this is XGaBivector<T> bv1)
                return ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddSpTerms(bv1, kv2)
                    .GetXGaScalar(Processor);

            return Processor
                .CreateKVectorComposer(Grade - 2)
                .AddRcpTerms(this, kv2)
                .GetKVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> Rcp(XGaHigherKVector<T> kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return (Grade - kv2.Grade) switch
            {
                < 0 => Processor.ScalarZero,

                0 => ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddSpTerms(this, kv2)
                    .GetXGaScalar(Processor),

                _ => Processor
                    .CreateKVectorComposer(Grade - kv2.Grade)
                    .AddRcpTerms(this, kv2)
                    .GetKVector()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> Rcp(XGaKVector<T> kv2)
        {
            if (IsZero || kv2.IsZero || Grade < kv2.Grade)
                return Processor.ScalarZero;

            if (this is XGaScalar<T> s1)
                return kv2 is XGaScalar<T> s2
                    ? Processor.Scalar(ScalarProcessor.Times(s1.ScalarValue, s2.ScalarValue).ScalarValue)
                    : Processor.ScalarZero;

            if (kv2 is XGaScalar<T> s3)
                return Times(s3.ScalarValue);

            if (Grade == kv2.Grade)
                return ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddSpTerms(this, kv2)
                    .GetXGaScalar(Processor);

            return Processor
                .CreateKVectorComposer(Grade - kv2.Grade)
                .AddRcpTerms(this, kv2)
                .GetKVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Rcp(XGaGradedMultivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.ScalarZero;

            if (this is XGaScalar<T> s1)
                return Processor.Scalar(ScalarProcessor.Times(s1.ScalarValue, mv2.Scalar().ScalarValue));

            return Processor
                .CreateMultivectorComposer()
                .AddRcpTerms(this, mv2)
                .GetSimpleMultivector();
        }

    }

    public sealed partial class XGaScalar<T>
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ERcp(XGaScalar<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ERcp(XGaVector<T> _)
        {
            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ERcp(XGaBivector<T> _)
        {
            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ERcp(XGaHigherKVector<T> _)
        {
            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ERcp(XGaKVector<T> kv2)
        {
            return kv2.TryGetScalarValue(out var s2)
                ? Times(s2)
                : Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ERcp(XGaGradedMultivector<T> mv2)
        {
            return mv2.TryGetScalarValue(out var s2)
                ? Times(s2)
                : Processor.ScalarZero;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Rcp(XGaScalar<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Rcp(XGaVector<T> _)
        {
            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Rcp(XGaBivector<T> _)
        {
            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Rcp(XGaHigherKVector<T> _)
        {
            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Rcp(XGaKVector<T> kv2)
        {
            return kv2 is XGaScalar<T> kv
                ? kv.Times(ScalarValue)
                : Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Rcp(XGaGradedMultivector<T> mv2)
        {
            return mv2.TryGetKVector(0, out var mv)
                ? Times(((XGaScalar<T>)mv).ScalarValue)
                : Processor.ScalarZero;
        }

    }

    public sealed partial class XGaVector<T>
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> ERcp(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ERcp(XGaVector<T> kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return ScalarComposer<T>
                .Create(ScalarProcessor)
                .AddESpTerms(this, kv2)
                .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ERcp(XGaBivector<T> _)
        {
            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ERcp(XGaHigherKVector<T> _)
        {
            return Processor.ScalarZero;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> Rcp(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Rcp(XGaVector<T> kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return ScalarComposer<T>
                .Create(ScalarProcessor)
                .AddSpTerms(this, kv2)
                .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Rcp(XGaBivector<T> _)
        {
            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Rcp(XGaHigherKVector<T> _)
        {
            return Processor.ScalarZero;
        }

    }

    public sealed partial class XGaBivector<T>
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> ERcp(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> ERcp(XGaVector<T> kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.VectorZero;

            return Processor
                .CreateKVectorComposer(1)
                .AddERcpTerms(this, kv2)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ERcp(XGaBivector<T> kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return ScalarComposer<T>
                .Create(ScalarProcessor)
                .AddESpTerms(this, kv2)
                .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> ERcp(XGaHigherKVector<T> _)
        {
            return Processor.ScalarZero;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> Rcp(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> Rcp(XGaVector<T> kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.VectorZero;

            return Processor
                .CreateKVectorComposer(1)
                .AddRcpTerms(this, kv2)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Rcp(XGaBivector<T> kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return ScalarComposer<T>
                .Create(ScalarProcessor)
                .AddSpTerms(this, kv2)
                .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> Rcp(XGaHigherKVector<T> _)
        {
            return Processor.ScalarZero;
        }

    }

    public sealed partial class XGaHigherKVector<T>
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> ERcp(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> Rcp(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }

    }

    public partial class XGaGradedMultivector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> ERcp(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddERcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> ERcp(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddERcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> ERcp(XGaHigherKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddERcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> ERcp(XGaKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddERcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> ERcp(XGaGradedMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddERcpTerms(this, mv2)
                    .GetSimpleMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Rcp(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddRcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Rcp(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddRcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Rcp(XGaHigherKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddRcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Rcp(XGaKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddRcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Rcp(XGaGradedMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddRcpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

    }

    public partial class XGaUniformMultivector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> ERcp(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddERcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> ERcp(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddERcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> ERcp(XGaHigherKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddERcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> ERcp(XGaKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddERcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> ERcp(XGaGradedMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddERcpTerms(this, mv2)
                    .GetSimpleMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Rcp(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddRcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Rcp(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddRcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Rcp(XGaHigherKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddRcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Rcp(XGaKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddRcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Rcp(XGaGradedMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddRcpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

    }

    public sealed partial class XGaUniformMultivectorComposer<T>
    {

        public XGaUniformMultivectorComposer<T> AddERcpTerms(XGaKVector<T> kv1, XGaKVector<T> kv2)
        {
            if (kv1.Grade < kv2.Grade)
                return this;

            if (kv1.Grade == kv2.Grade)
                return AddESpTerms(kv1, kv2);

            return AddEuclideanProductTerms(
                kv1,
                kv2,
                BasisBladeProductUtils.ERcpIsNonZero
            );
        }

        public XGaUniformMultivectorComposer<T> AddERcpTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> kv2)
        {
            if (mv1.IsZero || kv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                if (kv1.Grade >= kv2.Grade)
                    AddERcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaUniformMultivectorComposer<T> AddERcpTerms(XGaKVector<T> kv1, XGaGradedMultivector<T> mv2)
        {
            if (kv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv2 in mv2.KVectors)
            {
                if (kv1.Grade >= kv2.Grade)
                    AddERcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaUniformMultivectorComposer<T> AddERcpTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                var grade1 = kv1.Grade;
                var kVectorList2 =
                    mv2.KVectors.Where(kv => grade1 >= kv.Grade);

                foreach (var kv2 in kVectorList2)
                    AddERcpTerms(kv1, kv2);
            }

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivectorComposer<T> AddERcpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ERcpIsNonZero
            );
        }


        public XGaUniformMultivectorComposer<T> AddRcpTerms(XGaKVector<T> kv1, XGaKVector<T> kv2)
        {
            if (kv1.Grade < kv2.Grade)
                return this;

            if (kv1.Grade == kv2.Grade)
                return AddSpTerms(kv1, kv2);

            return AddMetricProductTerms(
                kv1,
                kv2,
                BasisBladeProductUtils.ERcpIsNonZero
            );
        }

        public XGaUniformMultivectorComposer<T> AddRcpTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> kv2)
        {
            if (mv1.IsZero || kv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                if (kv1.Grade >= kv2.Grade)
                    AddRcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaUniformMultivectorComposer<T> AddRcpTerms(XGaKVector<T> kv1, XGaGradedMultivector<T> mv2)
        {
            if (kv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv2 in mv2.KVectors)
            {
                if (kv1.Grade >= kv2.Grade)
                    AddRcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaUniformMultivectorComposer<T> AddRcpTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                var grade1 = kv1.Grade;
                var kVectorList2 =
                    mv2.KVectors.Where(kv => grade1 >= kv.Grade);

                foreach (var kv2 in kVectorList2)
                    AddRcpTerms(kv1, kv2);
            }

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivectorComposer<T> AddRcpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ERcpIsNonZero
            );
        }

    }
    
    public sealed partial class XGaKVectorComposer<T>
    {

        public XGaKVectorComposer<T> AddERcpTerms(XGaKVector<T> kv1, XGaKVector<T> kv2)
        {
            if (kv1.Grade < kv2.Grade)
                return this;

            if (kv1.Grade == kv2.Grade)
                return AddESpTerms(kv1, kv2);

            return AddEuclideanProductTerms(
                kv1,
                kv2,
                BasisBladeProductUtils.ERcpIsNonZero
            );
        }

        public XGaKVectorComposer<T> AddERcpTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> kv2)
        {
            if (mv1.IsZero || kv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                if (kv1.Grade >= kv2.Grade)
                    AddERcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaKVectorComposer<T> AddERcpTerms(XGaKVector<T> kv1, XGaGradedMultivector<T> mv2)
        {
            if (kv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv2 in mv2.KVectors)
            {
                if (kv1.Grade >= kv2.Grade)
                    AddERcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaKVectorComposer<T> AddERcpTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                var grade1 = kv1.Grade;
                var kVectorList2 =
                    mv2.KVectors.Where(kv => grade1 >= kv.Grade);

                foreach (var kv2 in kVectorList2)
                    AddERcpTerms(kv1, kv2);
            }

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> AddERcpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ERcpIsNonZero
            );
        }


        public XGaKVectorComposer<T> AddRcpTerms(XGaKVector<T> kv1, XGaKVector<T> kv2)
        {
            if (kv1.Grade < kv2.Grade)
                return this;

            if (kv1.Grade == kv2.Grade)
                return AddSpTerms(kv1, kv2);

            return AddMetricProductTerms(
                kv1,
                kv2,
                BasisBladeProductUtils.ERcpIsNonZero
            );
        }

        public XGaKVectorComposer<T> AddRcpTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> kv2)
        {
            if (mv1.IsZero || kv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                if (kv1.Grade >= kv2.Grade)
                    AddRcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaKVectorComposer<T> AddRcpTerms(XGaKVector<T> kv1, XGaGradedMultivector<T> mv2)
        {
            if (kv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv2 in mv2.KVectors)
            {
                if (kv1.Grade >= kv2.Grade)
                    AddRcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaKVectorComposer<T> AddRcpTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                var grade1 = kv1.Grade;
                var kVectorList2 =
                    mv2.KVectors.Where(kv => grade1 >= kv.Grade);

                foreach (var kv2 in kVectorList2)
                    AddRcpTerms(kv1, kv2);
            }

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> AddRcpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ERcpIsNonZero
            );
        }

    }
    
    public sealed partial class XGaGradedMultivectorComposer<T>
    {

        public XGaGradedMultivectorComposer<T> AddERcpTerms(XGaKVector<T> kv1, XGaKVector<T> kv2)
        {
            if (kv1.Grade < kv2.Grade)
                return this;

            if (kv1.Grade == kv2.Grade)
                return AddESpTerms(kv1, kv2);
            
            var composer = 
                Processor
                    .CreateKVectorComposer(kv1.Grade - kv2.Grade)
                    .AddERcpTerms(kv1, kv2);

            if (!composer.IsZero)
                AddKVectorTerms(
                    kv1.Grade - kv2.Grade, 
                    composer.IdScalarPairs
                );

            return this;
        }

        public XGaGradedMultivectorComposer<T> AddERcpTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> kv2)
        {
            if (mv1.IsZero || kv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                if (kv1.Grade >= kv2.Grade)
                    AddERcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaGradedMultivectorComposer<T> AddERcpTerms(XGaKVector<T> kv1, XGaGradedMultivector<T> mv2)
        {
            if (kv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv2 in mv2.KVectors)
            {
                if (kv1.Grade >= kv2.Grade)
                    AddERcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaGradedMultivectorComposer<T> AddERcpTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                var grade1 = kv1.Grade;
                var kVectorList2 =
                    mv2.KVectors.Where(kv => grade1 >= kv.Grade);

                foreach (var kv2 in kVectorList2)
                    AddERcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaGradedMultivectorComposer<T> AddERcpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv1 in mv1.GetKVectorParts())
            {
                var grade1 = kv1.Grade;
                var kVectorList2 =
                    mv2.GetKVectorParts().Where(kv => grade1 >= kv.Grade);

                foreach (var kv2 in kVectorList2)
                    AddERcpTerms(kv1, kv2);
            }

            return this;
        }


        public XGaGradedMultivectorComposer<T> AddRcpTerms(XGaKVector<T> kv1, XGaKVector<T> kv2)
        {
            if (kv1.Grade < kv2.Grade)
                return this;

            if (kv1.Grade == kv2.Grade)
                return AddSpTerms(kv1, kv2);
            
            var composer = 
                Processor
                    .CreateKVectorComposer(kv1.Grade - kv2.Grade)
                    .AddRcpTerms(kv1, kv2);

            if (!composer.IsZero)
                AddKVectorTerms(
                    kv1.Grade - kv2.Grade, 
                    composer.IdScalarPairs
                );

            return this;
        }

        public XGaGradedMultivectorComposer<T> AddRcpTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> kv2)
        {
            if (mv1.IsZero || kv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                if (kv1.Grade >= kv2.Grade)
                    AddRcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaGradedMultivectorComposer<T> AddRcpTerms(XGaKVector<T> kv1, XGaGradedMultivector<T> mv2)
        {
            if (kv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv2 in mv2.KVectors)
            {
                if (kv1.Grade >= kv2.Grade)
                    AddRcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaGradedMultivectorComposer<T> AddRcpTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                var grade1 = kv1.Grade;
                var kVectorList2 =
                    mv2.KVectors.Where(kv => grade1 >= kv.Grade);

                foreach (var kv2 in kVectorList2)
                    AddRcpTerms(kv1, kv2);
            }

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddRcpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv1 in mv1.GetKVectorParts())
            {
                var grade1 = kv1.Grade;
                var kVectorList2 =
                    mv2.GetKVectorParts().Where(kv => grade1 >= kv.Grade);

                foreach (var kv2 in kVectorList2)
                    AddRcpTerms(kv1, kv2);
            }

            return this;
        }

    }
}
