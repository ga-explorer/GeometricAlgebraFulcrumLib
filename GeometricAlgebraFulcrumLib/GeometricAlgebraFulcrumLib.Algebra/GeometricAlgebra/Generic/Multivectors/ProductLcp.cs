using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors
{
    public abstract partial class XGaMultivector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> ELcp(XGaScalar<T> kv2)
        {
            return TryGetScalarValue(out var s1) && !kv2.IsZero
                ? Processor.Scalar(ScalarProcessor.Times(s1, kv2.ScalarValue))
                : Processor.ScalarZero;
        }

        public abstract XGaMultivector<T> ELcp(XGaVector<T> kv2);

        public abstract XGaMultivector<T> ELcp(XGaBivector<T> kv2);

        public abstract XGaMultivector<T> ELcp(XGaHigherKVector<T> kv2);
        
        public abstract XGaMultivector<T> ELcp(XGaGradedMultivector<T> mv2);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> ELcp(XGaUniformMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddELcpTerms(this, mv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> ELcp(XGaKVector<T> kv2)
        {
            return kv2 switch
            {
                XGaScalar<T> s2 => ELcp(s2),
                XGaVector<T> v2 => ELcp(v2),
                XGaBivector<T> bv2 => ELcp(bv2),
                XGaHigherKVector<T> hkv2 => ELcp(hkv2),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> ELcp(XGaMultivector<T> mv2)
        {
            return mv2 switch
            {
                XGaScalar<T> s2 => ELcp(s2),
                XGaVector<T> v2 => ELcp(v2),
                XGaBivector<T> bv2 => ELcp(bv2),
                XGaHigherKVector<T> kv2 => ELcp(kv2),
                XGaGradedMultivector<T> gmv2 => ELcp(gmv2),
                XGaUniformMultivector<T> umv2 => ELcp(umv2),
                _ => throw new InvalidOperationException()
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> Lcp(XGaScalar<T> kv2)
        {
            return TryGetScalarValue(out var s1) && !kv2.IsZero
                ? Processor.Scalar(ScalarProcessor.Times(s1, kv2.ScalarValue))
                : Processor.ScalarZero;
        }

        public abstract XGaMultivector<T> Lcp(XGaVector<T> kv2);

        public abstract XGaMultivector<T> Lcp(XGaBivector<T> kv2);

        public abstract XGaMultivector<T> Lcp(XGaHigherKVector<T> kv2);
        
        public abstract XGaMultivector<T> Lcp(XGaGradedMultivector<T> mv2);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Lcp(XGaUniformMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddLcpTerms(this, mv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> Lcp(XGaKVector<T> kv2)
        {
            return kv2 switch
            {
                XGaScalar<T> s2 => Lcp(s2),
                XGaVector<T> v2 => Lcp(v2),
                XGaBivector<T> bv2 => Lcp(bv2),
                XGaHigherKVector<T> hkv2 => Lcp(hkv2),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Lcp(XGaMultivector<T> mv2)
        {
            return mv2 switch
            {
                XGaScalar<T> s2 => Lcp(s2),
                XGaVector<T> v2 => Lcp(v2),
                XGaBivector<T> bv2 => Lcp(bv2),
                XGaHigherKVector<T> kv2 => Lcp(kv2),
                XGaGradedMultivector<T> gmv2 => Lcp(gmv2),
                XGaUniformMultivector<T> umv2 => Lcp(umv2),
                _ => throw new InvalidOperationException()
            };
        }

    }

    public abstract partial class XGaKVector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> ELcp(XGaVector<T> kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return this switch
            {
                XGaScalar<T> s1 =>
                    kv2.Times(s1.ScalarValue),

                XGaVector<T> v1 =>
                    ScalarComposer<T>
                        .Create(ScalarProcessor)
                        .AddESpTerms(v1, kv2)
                        .GetXGaScalar(Processor),

                _ => Processor.ScalarZero
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> ELcp(XGaBivector<T> kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return this switch
            {
                XGaScalar<T> s1 =>
                    kv2.Times(s1.ScalarValue),

                XGaVector<T> v1 =>
                    Processor
                        .CreateVectorComposer()
                        .AddELcpTerms(v1, kv2)
                        .GetVector(),

                XGaBivector<T> bv1 =>
                    ScalarComposer<T>
                        .Create(ScalarProcessor)
                        .AddESpTerms(bv1, kv2)
                        .GetXGaScalar(Processor),

                _ => Processor.ScalarZero
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> ELcp(XGaHigherKVector<T> kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            if (this is XGaScalar<T> s1)
                return kv2.Times(s1.ScalarValue);

            if (Grade < kv2.Grade)
                return Processor
                    .CreateKVectorComposer(kv2.Grade - Grade)
                    .AddELcpTerms(this, kv2)
                    .GetKVector();

            if (Grade == kv2.Grade)
                return ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddESpTerms(this, kv2)
                    .GetXGaScalar(Processor);

            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> ELcp(XGaKVector<T> kv2)
        {
            if (IsZero || kv2.IsZero || Grade > kv2.Grade)
                return Processor.ScalarZero;

            if (this is XGaScalar<T> s1)
                return kv2.Times(s1.ScalarValue);

            if (kv2 is XGaScalar<T> s2)
                return Times(s2.ScalarValue);

            if (Grade < kv2.Grade)
                return Processor
                    .CreateKVectorComposer(kv2.Grade - Grade)
                    .AddELcpTerms(this, kv2)
                    .GetKVector();

            if (Grade == kv2.Grade)
                return ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddESpTerms(this, kv2)
                    .GetXGaScalar(Processor);

            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> ELcp(XGaGradedMultivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.ScalarZero;

            if (this is XGaScalar<T> s1)
                return mv2.Times(s1.ScalarValue);

            //return mv2.MapKVectorsSimplify(
            //    kv => Grade <= kv.Grade,
            //    ELcp
            //);

            return Processor
                .CreateMultivectorComposer()
                .AddELcpTerms(this, mv2)
                .GetMultivector();
        }

        //public override XGaMultivector<T> ELcp(XGaMultivector<T> mv2)
        //{
        //    if (IsZero || mv2.IsZero)
        //        return Processor.ScalarZero;

        //    if (this is XGaScalar<T> s1)
        //        return mv2.Times(s1.ScalarValue);

        //    return mv2 switch
        //    {
        //        XGaScalar<T> =>
        //            Processor.ScalarZero,

        //        XGaVector<T> v2 =>
        //            this is XGaVector<T> v1
        //                ? ScalarComposer<T>
        //                    .Create()
        //                    .AddESpTerms(v1, v2)
        //                    .GetXGaScalar(Processor)
        //                : Processor.ScalarZero,

        //        XGaBivector<T> kv2 =>
        //            this switch
        //            {
        //                XGaVector<T> v1 =>
        //                    Processor
        //                        .CreateUniformComposer(kv2.Grade - 1)
        //                        .AddELcpTerms(v1, kv2)
        //                        .GetSimpleKVector(),

        //                XGaBivector<T> bv1 =>
        //                    ScalarComposer<T>.Create()
        //                        .AddESpTerms(bv1, kv2)
        //                        .GetXGaScalar(Processor),

        //                _ => Processor.ScalarZero
        //            },

        //        XGaHigherKVector<T> kv2 when Grade < kv2.Grade =>
        //            Processor
        //                .CreateUniformComposer(kv2.Grade - Grade)
        //                .AddELcpTerms(this, mv2)
        //                .GetSimpleKVector(),

        //        XGaHigherKVector<T> kv2 when Grade == kv2.Grade =>
        //            ScalarComposer<T>
        //                .Create()
        //                .AddESpTerms(this, mv2)
        //                .GetXGaScalar(Processor),

        //        XGaHigherKVector<T> kv2 when Grade > kv2.Grade =>
        //            Processor.ScalarZero,

        //        XGaGradedMultivector<T> gmv2 =>
        //            Processor
        //                .CreateGradedComposer()
        //                .AddELcpTerms(this, gmv2)
        //                .GetSimpleKVector(),

        //        XGaUniformMultivector<T> =>
        //            Processor
        //                .CreateUniformComposer()
        //                .AddELcpTerms(this, mv2)
        //                .GetMultivector(),

        //        _ => throw new InvalidOperationException()
        //    };
        //}


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> Lcp(XGaVector<T> kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return this switch
            {
                XGaScalar<T> s1 =>
                    kv2.Times(s1.ScalarValue),

                XGaVector<T> v1 =>
                    ScalarComposer<T>
                        .Create(ScalarProcessor)
                        .AddSpTerms(v1, kv2)
                        .GetXGaScalar(Processor),

                _ => Processor.ScalarZero
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> Lcp(XGaBivector<T> kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return this switch
            {
                XGaScalar<T> s1 =>
                    kv2.Times(s1.ScalarValue),

                XGaVector<T> v1 =>
                    Processor
                        .CreateKVectorComposer(kv2.Grade - 1)
                        .AddLcpTerms(v1, kv2)
                        .GetKVector(),

                XGaBivector<T> bv1 =>
                    ScalarComposer<T>.Create(ScalarProcessor)
                        .AddSpTerms(bv1, kv2)
                        .GetXGaScalar(Processor),

                _ => Processor.ScalarZero
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> Lcp(XGaHigherKVector<T> kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            if (this is XGaScalar<T> s1)
                return kv2.Times(s1.ScalarValue);

            if (Grade < kv2.Grade)
                return Processor
                    .CreateKVectorComposer(kv2.Grade - Grade)
                    .AddLcpTerms(this, kv2)
                    .GetKVector();

            if (Grade == kv2.Grade)
                return ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddSpTerms(this, kv2)
                    .GetXGaScalar(Processor);

            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> Lcp(XGaKVector<T> kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            if (this is XGaScalar<T> s1)
                return kv2.Times(s1.ScalarValue);

            if (kv2 is XGaScalar<T> s2)
                return Times(s2.ScalarValue);

            if (Grade < kv2.Grade)
                return Processor
                    .CreateKVectorComposer(kv2.Grade - Grade)
                    .AddLcpTerms(this, kv2)
                    .GetKVector();

            if (Grade == kv2.Grade)
                return ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddSpTerms(this, kv2)
                    .GetXGaScalar(Processor);

            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Lcp(XGaGradedMultivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.ScalarZero;

            if (this is XGaScalar<T> s1)
                return mv2.Times(s1.ScalarValue);

            return mv2.MapKVectorsSimplify(
                kv => Grade <= kv.Grade,
                Lcp
            );
        }

        //public override XGaMultivector<T> Lcp(XGaMultivector<T> mv2)
        //{
        //    if (IsZero || mv2.IsZero)
        //        return Processor.ScalarZero;

        //    if (this is XGaScalar<T> s1)
        //        return mv2.Times(s1.ScalarValue);

        //    return mv2 switch
        //    {
        //        XGaScalar<T> =>
        //            Processor.ScalarZero,

        //        XGaVector<T> v2 =>
        //            this is XGaVector<T> v1
        //                ? ScalarComposer<T>
        //                    .Create()
        //                    .AddSpTerms(v1, v2)
        //                    .GetXGaScalar(Processor)
        //                : Processor.ScalarZero,

        //        XGaBivector<T> kv2 =>
        //            this switch
        //            {
        //                XGaVector<T> v1 =>
        //                    Processor
        //                        .CreateUniformComposer(kv2.Grade - 1)
        //                        .AddLcpTerms(v1, kv2)
        //                        .GetSimpleKVector(),

        //                XGaBivector<T> bv1 =>
        //                    ScalarComposer<T>.Create()
        //                        .AddSpTerms(bv1, kv2)
        //                        .GetXGaScalar(Processor),

        //                _ => Processor.ScalarZero
        //            },

        //        XGaHigherKVector<T> kv2 when Grade < kv2.Grade =>
        //            Processor
        //                .CreateUniformComposer(kv2.Grade - Grade)
        //                .AddLcpTerms(this, mv2)
        //                .GetSimpleKVector(),

        //        XGaHigherKVector<T> kv2 when Grade == kv2.Grade =>
        //            ScalarComposer<T>
        //                .Create()
        //                .AddSpTerms(this, mv2)
        //                .GetXGaScalar(Processor),

        //        XGaHigherKVector<T> kv2 when Grade > kv2.Grade =>
        //            Processor.ScalarZero,

        //        XGaGradedMultivector<T> gmv2 =>
        //            gmv2.MapKVectorsSimplify(
        //                kv => Grade <= kv.Grade,
        //                Lcp
        //            ),

        //        XGaUniformMultivector<T> =>
        //            Processor
        //                .CreateUniformComposer()
        //                .AddLcpTerms(this, mv2)
        //                .GetMultivector(),

        //        _ => throw new InvalidOperationException()
        //    };
        //}

    }

    public sealed partial class XGaScalar<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> ELcp(XGaVector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> ELcp(XGaBivector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> ELcp(XGaHigherKVector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> Lcp(XGaVector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> Lcp(XGaBivector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> Lcp(XGaHigherKVector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }

    }

    public sealed partial class XGaVector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ELcp(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddESpTerms(this, kv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> ELcp(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.VectorZero
                : Processor
                    .CreateVectorComposer()
                    .AddELcpTerms(this, kv2)
                    .GetVector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Lcp(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddSpTerms(this, kv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> Lcp(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.VectorZero
                : Processor
                    .CreateVectorComposer()
                    .AddLcpTerms(this, kv2)
                    .GetVector();
        }

    }

    public sealed partial class XGaBivector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ELcp(XGaVector<T> _)
        {
            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ELcp(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddESpTerms(this, kv2)
                    .GetXGaScalar(Processor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Lcp(XGaVector<T> _)
        {
            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Lcp(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddSpTerms(this, kv2)
                    .GetXGaScalar(Processor);
        }

    }

    public sealed partial class XGaHigherKVector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ELcp(XGaVector<T> _)
        {
            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ELcp(XGaBivector<T> _)
        {
            return Processor.ScalarZero;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Lcp(XGaVector<T> _)
        {
            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Lcp(XGaBivector<T> _)
        {
            return Processor.ScalarZero;
        }

    }

    public partial class XGaGradedMultivector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> ELcp(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddELcpTerms(this, kv2)
                    .GetMultivector();

            //return MapKVectorsSimplify(
            //    kv1 => kv1.Grade <= 1,
            //    kv1 => kv1.ELcp(kv2)
            //);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> ELcp(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddELcpTerms(this, kv2)
                    .GetMultivector();

            //return MapKVectorsSimplify(
            //    kv1 => kv1.Grade <= 2,
            //    kv1 => kv1.ELcp(kv2)
            //);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> ELcp(XGaHigherKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddELcpTerms(this, kv2)
                    .GetMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> ELcp(XGaGradedMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddELcpTerms(this, mv2)
                    .GetMultivector();

            //return MapKVectorsSimplify(
            //    mv2.KVectors,
            //    (kv1, kv2) => kv1.Grade <= kv2.Grade,
            //    (kv1, kv2) => kv1.ELcp(kv2)
            //);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Lcp(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddLcpTerms(this, kv2)
                    .GetMultivector();

            //return MapKVectorsSimplify(
            //    kv1 => kv1.Grade <= 1,
            //    kv1 => kv1.Lcp(kv2)
            //);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Lcp(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddLcpTerms(this, kv2)
                    .GetMultivector();

            //return MapKVectorsSimplify(
            //    kv1 => kv1.Grade <= 2,
            //    kv1 => kv1.Lcp(kv2)
            //);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Lcp(XGaHigherKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddLcpTerms(this, kv2)
                    .GetMultivector();

            //return MapKVectorsSimplify(
            //    kv1 => kv1.Grade <= kv2.Grade,
            //    kv1 => kv1.Lcp(kv2)
            //);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Lcp(XGaKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddLcpTerms(this, kv2)
                    .GetMultivector();

            //return MapKVectorsSimplify(
            //    kv1 => kv1.Grade <= kv2.Grade,
            //    kv1 => kv1.Lcp(kv2)
            //);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Lcp(XGaGradedMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddLcpTerms(this, mv2)
                    .GetMultivector();

            //return MapKVectorsSimplify(
            //    mv2.KVectors,
            //    (kv1, kv2) => kv1.Grade <= kv2.Grade,
            //    (kv1, kv2) => kv1.Lcp(kv2)
            //);
        }


    }

    public partial class XGaUniformMultivector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> ELcp(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddELcpTerms(this, kv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> ELcp(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddELcpTerms(this, kv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> ELcp(XGaHigherKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddELcpTerms(this, kv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> ELcp(XGaGradedMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddELcpTerms(this, mv2)
                    .GetMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Lcp(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddLcpTerms(this, kv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Lcp(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddLcpTerms(this, kv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Lcp(XGaHigherKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddLcpTerms(this, kv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Lcp(XGaKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddLcpTerms(this, kv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Lcp(XGaGradedMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddLcpTerms(this, mv2)
                    .GetMultivector();
        }
    }
    
    public sealed partial class XGaKVectorComposer<T>
    {
        public XGaKVectorComposer<T> AddELcpTerms(XGaKVector<T> kv1, XGaKVector<T> kv2)
        {
            if (kv1.Grade > kv2.Grade)
                return this;

            if (kv1.Grade == kv2.Grade)
                return AddESpTerms(kv1, kv2);

            return AddEuclideanProductTerms(
                kv1,
                kv2,
                BasisBladeProductUtils.ELcpIsNonZero
            );
        }

        public XGaKVectorComposer<T> AddELcpTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> kv2)
        {
            if (mv1.IsZero || kv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                if (kv1.Grade <= kv2.Grade)
                    AddELcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaKVectorComposer<T> AddELcpTerms(XGaKVector<T> kv1, XGaGradedMultivector<T> mv2)
        {
            if (kv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv2 in mv2.KVectors)
            {
                if (kv1.Grade <= kv2.Grade)
                    AddELcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaKVectorComposer<T> AddELcpTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                var grade1 = kv1.Grade;
                var kVectorList2 =
                    mv2.KVectors.Where(kv => grade1 <= kv.Grade);

                foreach (var kv2 in kVectorList2)
                    AddELcpTerms(kv1, kv2);
            }

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> AddELcpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ELcpIsNonZero
            );
        }


        public XGaKVectorComposer<T> AddLcpTerms(XGaKVector<T> kv1, XGaKVector<T> kv2)
        {
            if (kv1.Grade > kv2.Grade)
                return this;

            if (kv1.Grade == kv2.Grade)
                return AddSpTerms(kv1, kv2);

            return AddMetricProductTerms(
                kv1,
                kv2,
                BasisBladeProductUtils.ELcpIsNonZero
            );
        }

        public XGaKVectorComposer<T> AddLcpTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> kv2)
        {
            if (mv1.IsZero || kv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                if (kv1.Grade <= kv2.Grade)
                    AddLcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaKVectorComposer<T> AddLcpTerms(XGaKVector<T> kv1, XGaGradedMultivector<T> mv2)
        {
            if (kv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv2 in mv2.KVectors)
            {
                if (kv1.Grade <= kv2.Grade)
                    AddLcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaKVectorComposer<T> AddLcpTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                var grade1 = kv1.Grade;
                var kVectorList2 =
                    mv2.KVectors.Where(kv => grade1 <= kv.Grade);

                foreach (var kv2 in kVectorList2)
                    AddLcpTerms(kv1, kv2);
            }

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> AddLcpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ELcpIsNonZero
            );
        }

    }

    public sealed partial class XGaUniformMultivectorComposer<T>
    {
        public XGaUniformMultivectorComposer<T> AddELcpTerms(XGaKVector<T> kv1, XGaKVector<T> kv2)
        {
            if (kv1.Grade > kv2.Grade)
                return this;

            if (kv1.Grade == kv2.Grade)
                return AddESpTerms(kv1, kv2);

            return AddEuclideanProductTerms(
                kv1,
                kv2,
                BasisBladeProductUtils.ELcpIsNonZero
            );
        }

        public XGaUniformMultivectorComposer<T> AddELcpTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> kv2)
        {
            if (mv1.IsZero || kv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                if (kv1.Grade <= kv2.Grade)
                    AddELcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaUniformMultivectorComposer<T> AddELcpTerms(XGaKVector<T> kv1, XGaGradedMultivector<T> mv2)
        {
            if (kv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv2 in mv2.KVectors)
            {
                if (kv1.Grade <= kv2.Grade)
                    AddELcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaUniformMultivectorComposer<T> AddELcpTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                var grade1 = kv1.Grade;
                var kVectorList2 =
                    mv2.KVectors.Where(kv => grade1 <= kv.Grade);

                foreach (var kv2 in kVectorList2)
                    AddELcpTerms(kv1, kv2);
            }

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivectorComposer<T> AddELcpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ELcpIsNonZero
            );
        }


        public XGaUniformMultivectorComposer<T> AddLcpTerms(XGaKVector<T> kv1, XGaKVector<T> kv2)
        {
            if (kv1.Grade > kv2.Grade)
                return this;

            if (kv1.Grade == kv2.Grade)
                return AddSpTerms(kv1, kv2);

            return AddMetricProductTerms(
                kv1,
                kv2,
                BasisBladeProductUtils.ELcpIsNonZero
            );
        }

        public XGaUniformMultivectorComposer<T> AddLcpTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> kv2)
        {
            if (mv1.IsZero || kv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                if (kv1.Grade <= kv2.Grade)
                    AddLcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaUniformMultivectorComposer<T> AddLcpTerms(XGaKVector<T> kv1, XGaGradedMultivector<T> mv2)
        {
            if (kv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv2 in mv2.KVectors)
            {
                if (kv1.Grade <= kv2.Grade)
                    AddLcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaUniformMultivectorComposer<T> AddLcpTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                var grade1 = kv1.Grade;
                var kVectorList2 =
                    mv2.KVectors.Where(kv => grade1 <= kv.Grade);

                foreach (var kv2 in kVectorList2)
                    AddLcpTerms(kv1, kv2);
            }

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivectorComposer<T> AddLcpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ELcpIsNonZero
            );
        }

    }

    public sealed partial class XGaGradedMultivectorComposer<T>
    {
        public XGaGradedMultivectorComposer<T> AddELcpTerms(XGaKVector<T> kv1, XGaKVector<T> kv2)
        {
            if (kv1.Grade > kv2.Grade)
                return this;

            if (kv1.Grade == kv2.Grade)
                return AddESpTerms(kv1, kv2);
            
            var composer = 
                Processor
                    .CreateKVectorComposer(kv2.Grade - kv1.Grade)
                    .AddELcpTerms(kv1, kv2);

            if (!composer.IsZero)
                AddKVectorTerms(
                    kv2.Grade - kv1.Grade, 
                    composer.IdScalarPairs
                );

            return this;
        }

        public XGaGradedMultivectorComposer<T> AddELcpTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> kv2)
        {
            if (mv1.IsZero || kv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                if (kv1.Grade <= kv2.Grade)
                    AddELcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaGradedMultivectorComposer<T> AddELcpTerms(XGaKVector<T> kv1, XGaGradedMultivector<T> mv2)
        {
            if (kv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv2 in mv2.KVectors)
            {
                if (kv1.Grade <= kv2.Grade)
                    AddELcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaGradedMultivectorComposer<T> AddELcpTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                var grade1 = kv1.Grade;
                var kVectorList2 =
                    mv2.KVectors.Where(kv => grade1 <= kv.Grade);

                foreach (var kv2 in kVectorList2)
                    AddELcpTerms(kv1, kv2);
            }

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddELcpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv1 in mv1.GetKVectorParts())
            {
                var grade1 = kv1.Grade;
                var kVectorList2 =
                    mv2.GetKVectorParts().Where(kv => grade1 <= kv.Grade);

                foreach (var kv2 in kVectorList2)
                    AddELcpTerms(kv1, kv2);
            }

            return this;

            //return AddEuclideanProductTerms(
            //    mv1,
            //    mv2,
            //    BasisBladeProductUtils.ELcpIsNonZero
            //);
        }


        public XGaGradedMultivectorComposer<T> AddLcpTerms(XGaKVector<T> kv1, XGaKVector<T> kv2)
        {
            if (kv1.Grade > kv2.Grade)
                return this;

            if (kv1.Grade == kv2.Grade)
                return AddSpTerms(kv1, kv2);

            var composer = 
                Processor
                    .CreateKVectorComposer(kv2.Grade - kv1.Grade)
                    .AddLcpTerms(kv1, kv2);

            if (!composer.IsZero)
                AddKVectorTerms(
                    kv2.Grade - kv1.Grade, 
                    composer.IdScalarPairs
                );

            return this;
        }

        public XGaGradedMultivectorComposer<T> AddLcpTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> kv2)
        {
            if (mv1.IsZero || kv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                if (kv1.Grade <= kv2.Grade)
                    AddLcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaGradedMultivectorComposer<T> AddLcpTerms(XGaKVector<T> kv1, XGaGradedMultivector<T> mv2)
        {
            if (kv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv2 in mv2.KVectors)
            {
                if (kv1.Grade <= kv2.Grade)
                    AddLcpTerms(kv1, kv2);
            }

            return this;
        }

        public XGaGradedMultivectorComposer<T> AddLcpTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            {
                var grade1 = kv1.Grade;
                var kVectorList2 =
                    mv2.KVectors.Where(kv => grade1 <= kv.Grade);

                foreach (var kv2 in kVectorList2)
                    AddLcpTerms(kv1, kv2);
            }

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddLcpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv1 in mv1.GetKVectorParts())
            {
                var grade1 = kv1.Grade;
                var kVectorList2 =
                    mv2.GetKVectorParts().Where(kv => grade1 <= kv.Grade);

                foreach (var kv2 in kVectorList2)
                    AddLcpTerms(kv1, kv2);
            }

            return this;

            //return AddMetricProductTerms(
            //    mv1,
            //    mv2,
            //    BasisBladeProductUtils.ELcpIsNonZero
            //);
        }

    }
}
