using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors
{
    public abstract partial class XGaFloat64Multivector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar ELcp(XGaFloat64Scalar kv2)
        {
            return TryGetScalarValue(out var s1) && !kv2.IsZero
                ? Processor.Scalar(s1 * kv2.ScalarValue)
                : Processor.ScalarZero;
        }

        public abstract XGaFloat64Multivector ELcp(XGaFloat64Vector kv2);

        public abstract XGaFloat64Multivector ELcp(XGaFloat64Bivector kv2);

        public abstract XGaFloat64Multivector ELcp(XGaFloat64HigherKVector kv2);
        
        public abstract XGaFloat64Multivector ELcp(XGaFloat64GradedMultivector mv2);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector ELcp(XGaFloat64UniformMultivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddELcpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64Multivector ELcp(XGaFloat64KVector kv2)
        {
            return kv2 switch
            {
                XGaFloat64Scalar s2 => ELcp(s2),
                XGaFloat64Vector v2 => ELcp(v2),
                XGaFloat64Bivector bv2 => ELcp(bv2),
                XGaFloat64HigherKVector hkv2 => ELcp(hkv2),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector ELcp(XGaFloat64Multivector mv2)
        {
            return mv2 switch
            {
                XGaFloat64Scalar s2 => ELcp(s2),
                XGaFloat64Vector v2 => ELcp(v2),
                XGaFloat64Bivector bv2 => ELcp(bv2),
                XGaFloat64HigherKVector kv2 => ELcp(kv2),
                XGaFloat64GradedMultivector gmv2 => ELcp(gmv2),
                XGaFloat64UniformMultivector umv2 => ELcp(umv2),
                _ => throw new InvalidOperationException()
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar Lcp(XGaFloat64Scalar kv2)
        {
            return TryGetScalarValue(out var s1) && !kv2.IsZero
                ? Processor.Scalar(s1 * kv2.ScalarValue)
                : Processor.ScalarZero;
        }

        public abstract XGaFloat64Multivector Lcp(XGaFloat64Vector kv2);

        public abstract XGaFloat64Multivector Lcp(XGaFloat64Bivector kv2);

        public abstract XGaFloat64Multivector Lcp(XGaFloat64HigherKVector kv2);
        
        public abstract XGaFloat64Multivector Lcp(XGaFloat64GradedMultivector mv2);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector Lcp(XGaFloat64UniformMultivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddLcpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64Multivector Lcp(XGaFloat64KVector kv2)
        {
            return kv2 switch
            {
                XGaFloat64Scalar s2 => Lcp(s2),
                XGaFloat64Vector v2 => Lcp(v2),
                XGaFloat64Bivector bv2 => Lcp(bv2),
                XGaFloat64HigherKVector hkv2 => Lcp(hkv2),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector Lcp(XGaFloat64Multivector mv2)
        {
            return mv2 switch
            {
                XGaFloat64Scalar s2 => Lcp(s2),
                XGaFloat64Vector v2 => Lcp(v2),
                XGaFloat64Bivector bv2 => Lcp(bv2),
                XGaFloat64HigherKVector kv2 => Lcp(kv2),
                XGaFloat64GradedMultivector gmv2 => Lcp(gmv2),
                XGaFloat64UniformMultivector umv2 => Lcp(umv2),
                _ => throw new InvalidOperationException()
            };
        }

    }

    public abstract partial class XGaFloat64KVector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector ELcp(XGaFloat64Vector kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return this switch
            {
                XGaFloat64Scalar s1 =>
                    kv2.Times(s1.ScalarValue),

                XGaFloat64Vector v1 =>
                    Float64ScalarComposer
                        .Create()
                        .AddESpTerms(v1, kv2)
                        .GetXGaFloat64Scalar(Processor),

                _ => Processor.ScalarZero
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector ELcp(XGaFloat64Bivector kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return this switch
            {
                XGaFloat64Scalar s1 =>
                    kv2.Times(s1.ScalarValue),

                XGaFloat64Vector v1 =>
                    Processor
                        .CreateVectorComposer()
                        .AddELcpTerms(v1, kv2)
                        .GetVector(),

                XGaFloat64Bivector bv1 =>
                    Float64ScalarComposer
                        .Create()
                        .AddESpTerms(bv1, kv2)
                        .GetXGaFloat64Scalar(Processor),

                _ => Processor.ScalarZero
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector ELcp(XGaFloat64HigherKVector kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            if (this is XGaFloat64Scalar s1)
                return kv2.Times(s1.ScalarValue);

            if (Grade < kv2.Grade)
                return Processor
                    .CreateKVectorComposer(kv2.Grade - Grade)
                    .AddELcpTerms(this, kv2)
                    .GetKVector();

            if (Grade == kv2.Grade)
                return Float64ScalarComposer
                    .Create()
                    .AddESpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);

            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector ELcp(XGaFloat64KVector kv2)
        {
            if (IsZero || kv2.IsZero || Grade > kv2.Grade)
                return Processor.ScalarZero;

            if (this is XGaFloat64Scalar s1)
                return kv2.Times(s1.ScalarValue);

            if (kv2 is XGaFloat64Scalar s2)
                return Times(s2.ScalarValue);

            if (Grade < kv2.Grade)
                return Processor
                    .CreateKVectorComposer(kv2.Grade - Grade)
                    .AddELcpTerms(this, kv2)
                    .GetKVector();

            if (Grade == kv2.Grade)
                return Float64ScalarComposer
                    .Create()
                    .AddESpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);

            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector ELcp(XGaFloat64GradedMultivector mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.ScalarZero;

            if (this is XGaFloat64Scalar s1)
                return mv2.Times(s1.ScalarValue);

            //return mv2.MapKVectorsSimplify(
            //    kv => Grade <= kv.Grade,
            //    ELcp
            //);

            return Processor
                .CreateMultivectorComposer()
                .AddELcpTerms(this, mv2)
                .GetSimpleMultivector();
        }

        //public override XGaFloat64Multivector ELcp(XGaFloat64Multivector mv2)
        //{
        //    if (IsZero || mv2.IsZero)
        //        return Processor.ScalarZero;

        //    if (this is XGaFloat64Scalar s1)
        //        return mv2.Times(s1.ScalarValue);

        //    return mv2 switch
        //    {
        //        XGaFloat64Scalar =>
        //            Processor.ScalarZero,

        //        XGaFloat64Vector v2 =>
        //            this is XGaFloat64Vector v1
        //                ? Float64ScalarComposer
        //                    .Create()
        //                    .AddESpTerms(v1, v2)
        //                    .GetXGaFloat64Scalar(Processor)
        //                : Processor.ScalarZero,

        //        XGaFloat64Bivector kv2 =>
        //            this switch
        //            {
        //                XGaFloat64Vector v1 =>
        //                    Processor
        //                        .CreateUniformComposer(kv2.Grade - 1)
        //                        .AddELcpTerms(v1, kv2)
        //                        .GetSimpleKVector(),

        //                XGaFloat64Bivector bv1 =>
        //                    Float64ScalarComposer.Create()
        //                        .AddESpTerms(bv1, kv2)
        //                        .GetXGaFloat64Scalar(Processor),

        //                _ => Processor.ScalarZero
        //            },

        //        XGaFloat64HigherKVector kv2 when Grade < kv2.Grade =>
        //            Processor
        //                .CreateUniformComposer(kv2.Grade - Grade)
        //                .AddELcpTerms(this, mv2)
        //                .GetSimpleKVector(),

        //        XGaFloat64HigherKVector kv2 when Grade == kv2.Grade =>
        //            Float64ScalarComposer
        //                .Create()
        //                .AddESpTerms(this, mv2)
        //                .GetXGaFloat64Scalar(Processor),

        //        XGaFloat64HigherKVector kv2 when Grade > kv2.Grade =>
        //            Processor.ScalarZero,

        //        XGaFloat64GradedMultivector gmv2 =>
        //            Processor
        //                .CreateGradedComposer()
        //                .AddELcpTerms(this, gmv2)
        //                .GetSimpleKVector(),

        //        XGaFloat64UniformMultivector =>
        //            Processor
        //                .CreateUniformComposer()
        //                .AddELcpTerms(this, mv2)
        //                .GetSimpleMultivector(),

        //        _ => throw new InvalidOperationException()
        //    };
        //}


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector Lcp(XGaFloat64Vector kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return this switch
            {
                XGaFloat64Scalar s1 =>
                    kv2.Times(s1.ScalarValue),

                XGaFloat64Vector v1 =>
                    Float64ScalarComposer
                        .Create()
                        .AddSpTerms(v1, kv2)
                        .GetXGaFloat64Scalar(Processor),

                _ => Processor.ScalarZero
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector Lcp(XGaFloat64Bivector kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return this switch
            {
                XGaFloat64Scalar s1 =>
                    kv2.Times(s1.ScalarValue),

                XGaFloat64Vector v1 =>
                    Processor
                        .CreateKVectorComposer(kv2.Grade - 1)
                        .AddLcpTerms(v1, kv2)
                        .GetKVector(),

                XGaFloat64Bivector bv1 =>
                    Float64ScalarComposer.Create()
                        .AddSpTerms(bv1, kv2)
                        .GetXGaFloat64Scalar(Processor),

                _ => Processor.ScalarZero
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector Lcp(XGaFloat64HigherKVector kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            if (this is XGaFloat64Scalar s1)
                return kv2.Times(s1.ScalarValue);

            if (Grade < kv2.Grade)
                return Processor
                    .CreateKVectorComposer(kv2.Grade - Grade)
                    .AddLcpTerms(this, kv2)
                    .GetKVector();

            if (Grade == kv2.Grade)
                return Float64ScalarComposer
                    .Create()
                    .AddSpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);

            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector Lcp(XGaFloat64KVector kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            if (this is XGaFloat64Scalar s1)
                return kv2.Times(s1.ScalarValue);

            if (kv2 is XGaFloat64Scalar s2)
                return Times(s2.ScalarValue);

            if (Grade < kv2.Grade)
                return Processor
                    .CreateKVectorComposer(kv2.Grade - Grade)
                    .AddLcpTerms(this, kv2)
                    .GetKVector();

            if (Grade == kv2.Grade)
                return Float64ScalarComposer
                    .Create()
                    .AddSpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);

            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Lcp(XGaFloat64GradedMultivector mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.ScalarZero;

            if (this is XGaFloat64Scalar s1)
                return mv2.Times(s1.ScalarValue);

            return mv2.MapKVectorsSimplify(
                kv => Grade <= kv.Grade,
                Lcp
            );
        }

        //public override XGaFloat64Multivector Lcp(XGaFloat64Multivector mv2)
        //{
        //    if (IsZero || mv2.IsZero)
        //        return Processor.ScalarZero;

        //    if (this is XGaFloat64Scalar s1)
        //        return mv2.Times(s1.ScalarValue);

        //    return mv2 switch
        //    {
        //        XGaFloat64Scalar =>
        //            Processor.ScalarZero,

        //        XGaFloat64Vector v2 =>
        //            this is XGaFloat64Vector v1
        //                ? Float64ScalarComposer
        //                    .Create()
        //                    .AddSpTerms(v1, v2)
        //                    .GetXGaFloat64Scalar(Processor)
        //                : Processor.ScalarZero,

        //        XGaFloat64Bivector kv2 =>
        //            this switch
        //            {
        //                XGaFloat64Vector v1 =>
        //                    Processor
        //                        .CreateUniformComposer(kv2.Grade - 1)
        //                        .AddLcpTerms(v1, kv2)
        //                        .GetSimpleKVector(),

        //                XGaFloat64Bivector bv1 =>
        //                    Float64ScalarComposer.Create()
        //                        .AddSpTerms(bv1, kv2)
        //                        .GetXGaFloat64Scalar(Processor),

        //                _ => Processor.ScalarZero
        //            },

        //        XGaFloat64HigherKVector kv2 when Grade < kv2.Grade =>
        //            Processor
        //                .CreateUniformComposer(kv2.Grade - Grade)
        //                .AddLcpTerms(this, mv2)
        //                .GetSimpleKVector(),

        //        XGaFloat64HigherKVector kv2 when Grade == kv2.Grade =>
        //            Float64ScalarComposer
        //                .Create()
        //                .AddSpTerms(this, mv2)
        //                .GetXGaFloat64Scalar(Processor),

        //        XGaFloat64HigherKVector kv2 when Grade > kv2.Grade =>
        //            Processor.ScalarZero,

        //        XGaFloat64GradedMultivector gmv2 =>
        //            gmv2.MapKVectorsSimplify(
        //                kv => Grade <= kv.Grade,
        //                Lcp
        //            ),

        //        XGaFloat64UniformMultivector =>
        //            Processor
        //                .CreateUniformComposer()
        //                .AddLcpTerms(this, mv2)
        //                .GetSimpleMultivector(),

        //        _ => throw new InvalidOperationException()
        //    };
        //}

    }

    public sealed partial class XGaFloat64Scalar
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Vector ELcp(XGaFloat64Vector kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Bivector ELcp(XGaFloat64Bivector kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64HigherKVector ELcp(XGaFloat64HigherKVector kv2)
        {
            return kv2.Times(ScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Vector Lcp(XGaFloat64Vector kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Bivector Lcp(XGaFloat64Bivector kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64HigherKVector Lcp(XGaFloat64HigherKVector kv2)
        {
            return kv2.Times(ScalarValue);
        }

    }

    public sealed partial class XGaFloat64Vector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar ELcp(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddESpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Vector ELcp(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.VectorZero
                : Processor
                    .CreateVectorComposer()
                    .AddELcpTerms(this, kv2)
                    .GetVector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar Lcp(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddSpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Vector Lcp(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.VectorZero
                : Processor
                    .CreateVectorComposer()
                    .AddLcpTerms(this, kv2)
                    .GetVector();
        }

    }

    public sealed partial class XGaFloat64Bivector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar ELcp(XGaFloat64Vector _)
        {
            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar ELcp(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddESpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar Lcp(XGaFloat64Vector _)
        {
            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar Lcp(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Float64ScalarComposer
                    .Create()
                    .AddSpTerms(this, kv2)
                    .GetXGaFloat64Scalar(Processor);
        }

    }

    public sealed partial class XGaFloat64HigherKVector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar ELcp(XGaFloat64Vector _)
        {
            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar ELcp(XGaFloat64Bivector _)
        {
            return Processor.ScalarZero;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar Lcp(XGaFloat64Vector _)
        {
            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar Lcp(XGaFloat64Bivector _)
        {
            return Processor.ScalarZero;
        }

    }

    public partial class XGaFloat64GradedMultivector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector ELcp(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddELcpTerms(this, kv2)
                    .GetSimpleMultivector();

            //return MapKVectorsSimplify(
            //    kv1 => kv1.Grade <= 1,
            //    kv1 => kv1.ELcp(kv2)
            //);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector ELcp(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddELcpTerms(this, kv2)
                    .GetSimpleMultivector();

            //return MapKVectorsSimplify(
            //    kv1 => kv1.Grade <= 2,
            //    kv1 => kv1.ELcp(kv2)
            //);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector ELcp(XGaFloat64HigherKVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddELcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector ELcp(XGaFloat64GradedMultivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddELcpTerms(this, mv2)
                    .GetSimpleMultivector();

            //return MapKVectorsSimplify(
            //    mv2.KVectors,
            //    (kv1, kv2) => kv1.Grade <= kv2.Grade,
            //    (kv1, kv2) => kv1.ELcp(kv2)
            //);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Lcp(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddLcpTerms(this, kv2)
                    .GetSimpleMultivector();

            //return MapKVectorsSimplify(
            //    kv1 => kv1.Grade <= 1,
            //    kv1 => kv1.Lcp(kv2)
            //);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Lcp(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddLcpTerms(this, kv2)
                    .GetSimpleMultivector();

            //return MapKVectorsSimplify(
            //    kv1 => kv1.Grade <= 2,
            //    kv1 => kv1.Lcp(kv2)
            //);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Lcp(XGaFloat64HigherKVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddLcpTerms(this, kv2)
                    .GetSimpleMultivector();

            //return MapKVectorsSimplify(
            //    kv1 => kv1.Grade <= kv2.Grade,
            //    kv1 => kv1.Lcp(kv2)
            //);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Lcp(XGaFloat64KVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddLcpTerms(this, kv2)
                    .GetSimpleMultivector();

            //return MapKVectorsSimplify(
            //    kv1 => kv1.Grade <= kv2.Grade,
            //    kv1 => kv1.Lcp(kv2)
            //);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Lcp(XGaFloat64GradedMultivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddLcpTerms(this, mv2)
                    .GetSimpleMultivector();

            //return MapKVectorsSimplify(
            //    mv2.KVectors,
            //    (kv1, kv2) => kv1.Grade <= kv2.Grade,
            //    (kv1, kv2) => kv1.Lcp(kv2)
            //);
        }


    }

    public partial class XGaFloat64UniformMultivector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector ELcp(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddELcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector ELcp(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddELcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector ELcp(XGaFloat64HigherKVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddELcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector ELcp(XGaFloat64GradedMultivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddELcpTerms(this, mv2)
                    .GetSimpleMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Lcp(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddLcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Lcp(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddLcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Lcp(XGaFloat64HigherKVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddLcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Lcp(XGaFloat64KVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddLcpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Lcp(XGaFloat64GradedMultivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddLcpTerms(this, mv2)
                    .GetSimpleMultivector();
        }
    }
    
    public sealed partial class XGaFloat64KVectorComposer
    {
        public XGaFloat64KVectorComposer AddELcpTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
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

        public XGaFloat64KVectorComposer AddELcpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64KVector kv2)
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

        public XGaFloat64KVectorComposer AddELcpTerms(XGaFloat64KVector kv1, XGaFloat64GradedMultivector mv2)
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

        public XGaFloat64KVectorComposer AddELcpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
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
        public XGaFloat64KVectorComposer AddELcpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ELcpIsNonZero
            );
        }


        public XGaFloat64KVectorComposer AddLcpTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
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

        public XGaFloat64KVectorComposer AddLcpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64KVector kv2)
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

        public XGaFloat64KVectorComposer AddLcpTerms(XGaFloat64KVector kv1, XGaFloat64GradedMultivector mv2)
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

        public XGaFloat64KVectorComposer AddLcpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
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
        public XGaFloat64KVectorComposer AddLcpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ELcpIsNonZero
            );
        }

    }

    public sealed partial class XGaFloat64UniformMultivectorComposer
    {
        public XGaFloat64UniformMultivectorComposer AddELcpTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
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

        public XGaFloat64UniformMultivectorComposer AddELcpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64KVector kv2)
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

        public XGaFloat64UniformMultivectorComposer AddELcpTerms(XGaFloat64KVector kv1, XGaFloat64GradedMultivector mv2)
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

        public XGaFloat64UniformMultivectorComposer AddELcpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
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
        public XGaFloat64UniformMultivectorComposer AddELcpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ELcpIsNonZero
            );
        }


        public XGaFloat64UniformMultivectorComposer AddLcpTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
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

        public XGaFloat64UniformMultivectorComposer AddLcpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64KVector kv2)
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

        public XGaFloat64UniformMultivectorComposer AddLcpTerms(XGaFloat64KVector kv1, XGaFloat64GradedMultivector mv2)
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

        public XGaFloat64UniformMultivectorComposer AddLcpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
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
        public XGaFloat64UniformMultivectorComposer AddLcpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ELcpIsNonZero
            );
        }

    }

    public sealed partial class XGaFloat64GradedMultivectorComposer
    {
        public XGaFloat64GradedMultivectorComposer AddELcpTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
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

        public XGaFloat64GradedMultivectorComposer AddELcpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64KVector kv2)
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

        public XGaFloat64GradedMultivectorComposer AddELcpTerms(XGaFloat64KVector kv1, XGaFloat64GradedMultivector mv2)
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

        public XGaFloat64GradedMultivectorComposer AddELcpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
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
        public XGaFloat64GradedMultivectorComposer AddELcpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
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


        public XGaFloat64GradedMultivectorComposer AddLcpTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
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

        public XGaFloat64GradedMultivectorComposer AddLcpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64KVector kv2)
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

        public XGaFloat64GradedMultivectorComposer AddLcpTerms(XGaFloat64KVector kv1, XGaFloat64GradedMultivector mv2)
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

        public XGaFloat64GradedMultivectorComposer AddLcpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
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
        public XGaFloat64GradedMultivectorComposer AddLcpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
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
