using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors
{
    public abstract partial class XGaMultivector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> EFdp(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }

        public abstract XGaMultivector<T> EFdp(XGaVector<T> kv2);

        public abstract XGaMultivector<T> EFdp(XGaBivector<T> kv2);

        public abstract XGaMultivector<T> EFdp(XGaHigherKVector<T> kv2);
        
        public abstract XGaMultivector<T> EFdp(XGaGradedMultivector<T> mv2);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> EFdp(XGaUniformMultivector<T> mv2)
        {
            if (this is XGaScalar<T> s1)
                return mv2.Times(s1.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEFdpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> EFdp(XGaKVector<T> kv2)
        {
            return kv2 switch
            {
                XGaScalar<T> s2 => EFdp(s2),
                XGaVector<T> v2 => EFdp(v2),
                XGaBivector<T> bv2 => EFdp(bv2),
                XGaHigherKVector<T> hkv2 => EFdp(hkv2),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> EFdp(XGaMultivector<T> mv2)
        {
            return mv2 switch
            {
                XGaScalar<T> s2 => EFdp(s2),
                XGaVector<T> v2 => EFdp(v2),
                XGaBivector<T> bv2 => EFdp(bv2),
                XGaHigherKVector<T> kv2 => EFdp(kv2),
                XGaGradedMultivector<T> gmv2 => EFdp(gmv2),
                XGaUniformMultivector<T> umv2 => EFdp(umv2),
                _ => throw new InvalidOperationException()
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> Fdp(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }

        public abstract XGaMultivector<T> Fdp(XGaVector<T> kv2);

        public abstract XGaMultivector<T> Fdp(XGaBivector<T> kv2);

        public abstract XGaMultivector<T> Fdp(XGaHigherKVector<T> kv2);
        
        public abstract XGaMultivector<T> Fdp(XGaGradedMultivector<T> mv2);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Fdp(XGaUniformMultivector<T> mv2)
        {
            if (this is XGaScalar<T> s1)
                return mv2.Times(s1.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddFdpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> Fdp(XGaKVector<T> kv2)
        {
            return kv2 switch
            {
                XGaScalar<T> s2 => Fdp(s2),
                XGaVector<T> v2 => Fdp(v2),
                XGaBivector<T> bv2 => Fdp(bv2),
                XGaHigherKVector<T> hkv2 => Fdp(hkv2),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Fdp(XGaMultivector<T> mv2)
        {
            return mv2 switch
            {
                XGaScalar<T> s2 => Fdp(s2),
                XGaVector<T> v2 => Fdp(v2),
                XGaBivector<T> bv2 => Fdp(bv2),
                XGaHigherKVector<T> kv2 => Fdp(kv2),
                XGaGradedMultivector<T> gmv2 => Fdp(gmv2),
                XGaUniformMultivector<T> umv2 => Fdp(umv2),
                _ => throw new InvalidOperationException()
            };
        }

    }

    public abstract partial class XGaKVector<T>
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> EFdp(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> EFdp(XGaVector<T> kv2)
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

                _ => Processor
                    .CreateKVectorComposer(Grade - 1)
                    .AddERcpTerms(this, kv2)
                    .GetKVector()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> EFdp(XGaBivector<T> kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return this switch
            {
                XGaScalar<T> kv1 =>
                    kv2.Times(kv1.ScalarValue),

                XGaVector<T> kv1 =>
                    Processor
                        .CreateKVectorComposer(1)
                        .AddELcpTerms(kv1, kv2)
                        .GetVector(),

                XGaBivector<T> kv1 =>
                    ScalarComposer<T>
                        .Create(ScalarProcessor)
                        .AddESpTerms(kv1, kv2)
                        .GetXGaScalar(Processor),

                _ => Processor
                    .CreateKVectorComposer(Grade - 2)
                    .AddERcpTerms(this, kv2)
                    .GetKVector()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> EFdp(XGaHigherKVector<T> kv2)
        {
            return Grade == kv2.Grade
                ? ESp(kv2)
                : Grade < kv2.Grade ? ELcp(kv2) : ERcp(kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> EFdp(XGaKVector<T> kv2)
        {
            return Grade == kv2.Grade
                ? ESp(kv2)
                : Grade < kv2.Grade ? ELcp(kv2) : ERcp(kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> EFdp(XGaGradedMultivector<T> mv2)
        {
            if (this is XGaScalar<T> s1)
                return mv2.Times(s1.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEFdpTerms(this, mv2)
                    .GetSimpleMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> Fdp(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> Fdp(XGaVector<T> kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return this switch
            {
                XGaScalar<T> s1 =>
                    kv2.Times(s1.ScalarValue),

                XGaVector<T> v1 =>
                    v1.Sp(kv2),

                _ => Processor
                    .CreateKVectorComposer(Grade - 1)
                    .AddRcpTerms(this, kv2)
                    .GetKVector()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> Fdp(XGaBivector<T> kv2)
        {
            if (IsZero || kv2.IsZero)
                return Processor.ScalarZero;

            return this switch
            {
                XGaScalar<T> kv1 =>
                    kv2.Times(kv1.ScalarValue),

                XGaVector<T> kv1 =>
                    Processor
                        .CreateKVectorComposer(1)
                        .AddLcpTerms(kv1, kv2)
                        .GetVector(),

                XGaBivector<T> kv1 =>
                    ScalarComposer<T>
                        .Create(ScalarProcessor)
                        .AddSpTerms(kv1, kv2)
                        .GetXGaScalar(Processor),

                _ => Processor
                    .CreateKVectorComposer(Grade - 2)
                    .AddRcpTerms(this, kv2)
                    .GetKVector()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> Fdp(XGaHigherKVector<T> kv2)
        {
            return Grade == kv2.Grade
                ? Sp(kv2)
                : Grade < kv2.Grade ? Lcp(kv2) : Rcp(kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> Fdp(XGaKVector<T> kv2)
        {
            return Grade == kv2.Grade
                ? Sp(kv2)
                : Grade < kv2.Grade ? Lcp(kv2) : Rcp(kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Fdp(XGaGradedMultivector<T> mv2)
        {
            if (this is XGaScalar<T> s1)
                return mv2.Times(s1.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddFdpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

    }

    public sealed partial class XGaScalar<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> EFdp(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> EFdp(XGaVector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> EFdp(XGaBivector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> EFdp(XGaHigherKVector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> EFdp(XGaKVector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> EFdp(XGaGradedMultivector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Fdp(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> Fdp(XGaVector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> Fdp(XGaBivector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> Fdp(XGaHigherKVector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> Fdp(XGaKVector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Fdp(XGaGradedMultivector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }

    }

    public sealed partial class XGaVector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> EFdp(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> EFdp(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddESpTerms(this, kv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> EFdp(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.VectorZero
                : Processor
                    .CreateVectorComposer()
                    .AddELcpTerms(this, kv2)
                    .GetVector();
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> Fdp(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Fdp(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddSpTerms(this, kv2)
                    .GetXGaScalar(Processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> Fdp(XGaBivector<T> kv2)
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
        public override XGaBivector<T> EFdp(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> EFdp(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.VectorZero
                : Processor
                    .CreateKVectorComposer(1)
                    .AddERcpTerms(this, kv2)
                    .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> EFdp(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : ScalarComposer<T>
                    .Create(ScalarProcessor)
                    .AddESpTerms(this, kv2)
                    .GetXGaScalar(Processor);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> Fdp(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> Fdp(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.VectorZero
                : Processor
                    .CreateKVectorComposer(1)
                    .AddRcpTerms(this, kv2)
                    .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Fdp(XGaBivector<T> kv2)
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
        public override XGaHigherKVector<T> EFdp(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> Fdp(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }

    }

    public partial class XGaGradedMultivector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> EFdp(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEFdpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> EFdp(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEFdpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> EFdp(XGaHigherKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEFdpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> EFdp(XGaKVector<T> kv2)
        {
            if (kv2 is XGaScalar<T> scalar)
                return Times(scalar.ScalarValue);

            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEFdpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> EFdp(XGaGradedMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEFdpTerms(this, mv2)
                    .GetSimpleMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Fdp(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddFdpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Fdp(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddFdpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Fdp(XGaHigherKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddFdpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Fdp(XGaKVector<T> kv2)
        {
            if (kv2 is XGaScalar<T> scalar)
                return Times(scalar.ScalarValue);

            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddFdpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Fdp(XGaGradedMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddFdpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

    }

    public partial class XGaUniformMultivector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> EFdp(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEFdpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> EFdp(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEFdpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> EFdp(XGaHigherKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEFdpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> EFdp(XGaKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEFdpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> EFdp(XGaGradedMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEFdpTerms(this, mv2)
                    .GetSimpleMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Fdp(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddFdpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Fdp(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddFdpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Fdp(XGaHigherKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddFdpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Fdp(XGaKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddFdpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Fdp(XGaGradedMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddFdpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

    }
    
    public sealed partial class XGaKVectorComposer<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> AddEFdpTerms(XGaKVector<T> kv1, XGaKVector<T> kv2)
        {
            if (kv1.Grade == kv2.Grade)
                return AddESpTerms(kv1, kv2);

            return kv1.Grade < kv2.Grade 
                ? AddELcpTerms(kv1, kv2) 
                : AddERcpTerms(kv1, kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> AddEFdpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EFdpIsNonZero
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> AddFdpTerms(XGaKVector<T> kv1, XGaKVector<T> kv2)
        {
            if (kv1.Grade == kv2.Grade)
                return AddSpTerms(kv1, kv2);

            return kv1.Grade < kv2.Grade 
                ? AddLcpTerms(kv1, kv2) 
                : AddRcpTerms(kv1, kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> AddFdpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EFdpIsNonZero
            );
        }

    }

    public sealed partial class XGaUniformMultivectorComposer<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivectorComposer<T> AddEFdpTerms(XGaKVector<T> kv1, XGaKVector<T> kv2)
        {
            if (kv1.Grade == kv2.Grade)
                return AddESpTerms(kv1, kv2);

            return kv1.Grade < kv2.Grade 
                ? AddELcpTerms(kv1, kv2) 
                : AddERcpTerms(kv1, kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivectorComposer<T> AddEFdpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EFdpIsNonZero
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivectorComposer<T> AddFdpTerms(XGaKVector<T> kv1, XGaKVector<T> kv2)
        {
            if (kv1.Grade == kv2.Grade)
                return AddSpTerms(kv1, kv2);

            return kv1.Grade < kv2.Grade 
                ? AddLcpTerms(kv1, kv2) 
                : AddRcpTerms(kv1, kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivectorComposer<T> AddFdpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EFdpIsNonZero
            );
        }

    }
    
    public sealed partial class XGaGradedMultivectorComposer<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddEFdpTerms(XGaKVector<T> kv1, XGaKVector<T> kv2)
        {
            if (kv1.Grade == kv2.Grade)
                return AddESpTerms(kv1, kv2);

            return kv1.Grade < kv2.Grade 
                ? AddELcpTerms(kv1, kv2) 
                : AddERcpTerms(kv1, kv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddEFdpTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> kv2)
        {
            if (mv1.IsZero || kv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors) 
                AddEFdpTerms(kv1, kv2);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddEFdpTerms(XGaKVector<T> kv1, XGaGradedMultivector<T> mv2)
        {
            if (kv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv2 in mv2.KVectors) 
                AddEFdpTerms(kv1, kv2);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddEFdpTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            foreach (var kv2 in mv2.KVectors)
                AddEFdpTerms(kv1, kv2);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddEFdpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            var mv2KVectors = 
                mv2.GetKVectorParts().ToArray();

            foreach (var kv1 in mv1.GetKVectorParts())
            foreach (var kv2 in mv2KVectors)
                AddEFdpTerms(kv1, kv2);

            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddFdpTerms(XGaKVector<T> kv1, XGaKVector<T> kv2)
        {
            if (kv1.Grade == kv2.Grade)
                return AddSpTerms(kv1, kv2);

            return kv1.Grade < kv2.Grade 
                ? AddLcpTerms(kv1, kv2) 
                : AddRcpTerms(kv1, kv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddFdpTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> kv2)
        {
            if (mv1.IsZero || kv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors) 
                AddFdpTerms(kv1, kv2);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddFdpTerms(XGaKVector<T> kv1, XGaGradedMultivector<T> mv2)
        {
            if (kv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv2 in mv2.KVectors) 
                AddFdpTerms(kv1, kv2);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddFdpTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors)
            foreach (var kv2 in mv2.KVectors)
                AddFdpTerms(kv1, kv2);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddFdpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            var mv2KVectors = 
                mv2.GetKVectorParts().ToArray();

            foreach (var kv1 in mv1.GetKVectorParts())
            foreach (var kv2 in mv2KVectors)
                AddFdpTerms(kv1, kv2);

            return this;
        }

    }
}
