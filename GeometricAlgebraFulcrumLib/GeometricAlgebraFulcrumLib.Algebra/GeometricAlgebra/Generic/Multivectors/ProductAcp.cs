using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors
{
    public abstract partial class XGaMultivector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> EAcp(XGaScalar<T> mv2)
        {
            return Times(mv2.ScalarValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> EAcp(XGaVector<T> mv2)
        {
            if (this is XGaScalar<T> s1)
                return mv2.Times(s1.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEAcpTerms(this, mv2)
                    .GetMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> EAcp(XGaBivector<T> mv2)
        {
            if (this is XGaScalar<T> s1)
                return mv2.Times(s1.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEAcpTerms(this, mv2)
                    .GetMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> EAcp(XGaHigherKVector<T> mv2)
        {
            if (this is XGaScalar<T> s1)
                return mv2.Times(s1.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEAcpTerms(this, mv2)
                    .GetMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> EAcp(XGaGradedMultivector<T> mv2)
        {
            //return (EGp(mv2) + mv2.EGp(this)).Divide(2d);

            if (this is XGaScalar<T> s1)
                return mv2.Times(s1.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEAcpTerms(this, mv2)
                    .GetMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> EAcp(XGaUniformMultivector<T> mv2)
        {
            //return (EGp(mv2) + mv2.EGp(this)).Divide(2d);

            if (this is XGaScalar<T> s1)
                return mv2.Times(s1.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEAcpTerms(this, mv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> EAcp(XGaKVector<T> mv2)
        {
            if (this is XGaScalar<T> s1)
                return mv2.Times(s1.ScalarValue);

            if (mv2 is XGaScalar<T> s2)
                return Times(s2.ScalarValue);

            if (this is XGaVector<T> v1 && mv2 is XGaVector<T> v2)
                return v1.ESp(v2);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEAcpTerms(this, mv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> EAcp(XGaMultivector<T> mv2)
        {
            //return (EGp(mv2) + mv2.EGp(this)).Divide(2d);

            if (this is XGaScalar<T> s1)
                return mv2.Times(s1.ScalarValue);
            
            if (mv2 is XGaScalar<T> s2)
                return Times(s2.ScalarValue);

            if (this is XGaVector<T> v1 && mv2 is XGaVector<T> v2)
                return v1.ESp(v2);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEAcpTerms(this, mv2)
                    .GetMultivector();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> Acp(XGaScalar<T> mv2)
        {
            return Times(mv2.ScalarValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> Acp(XGaVector<T> mv2)
        {
            if (this is XGaScalar<T> s1)
                return mv2.Times(s1.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddAcpTerms(this, mv2)
                    .GetMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> Acp(XGaBivector<T> mv2)
        {
            if (this is XGaScalar<T> s1)
                return mv2.Times(s1.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddAcpTerms(this, mv2)
                    .GetMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> Acp(XGaHigherKVector<T> mv2)
        {
            if (this is XGaScalar<T> s1)
                return mv2.Times(s1.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddAcpTerms(this, mv2)
                    .GetMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Acp(XGaGradedMultivector<T> mv2)
        {
            //return (EGp(mv2) + mv2.EGp(this)).Divide(2d);

            if (this is XGaScalar<T> s1)
                return mv2.Times(s1.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddAcpTerms(this, mv2)
                    .GetMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Acp(XGaUniformMultivector<T> mv2)
        {
            //return (EGp(mv2) + mv2.EGp(this)).Divide(2d);

            if (this is XGaScalar<T> s1)
                return mv2.Times(s1.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddAcpTerms(this, mv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> Acp(XGaKVector<T> mv2)
        {
            if (this is XGaScalar<T> s1)
                return mv2.Times(s1.ScalarValue);

            if (mv2 is XGaScalar<T> s2)
                return Times(s2.ScalarValue);

            if (this is XGaVector<T> v1 && mv2 is XGaVector<T> v2)
                return v1.Sp(v2);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddAcpTerms(this, mv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Acp(XGaMultivector<T> mv2)
        {
            //return (EGp(mv2) + mv2.EGp(this)).Divide(2d);

            if (this is XGaScalar<T> s1)
                return mv2.Times(s1.ScalarValue);
            
            if (mv2 is XGaScalar<T> s2)
                return Times(s2.ScalarValue);

            if (this is XGaVector<T> v1 && mv2 is XGaVector<T> v2)
                return v1.Sp(v2);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddAcpTerms(this, mv2)
                    .GetMultivector();
        }
    }

    public abstract partial class XGaKVector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> EAcp(XGaScalar<T> mv2)
        {
            return Times(mv2.ScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> Acp(XGaScalar<T> mv2)
        {
            return Times(mv2.ScalarValue);
        }
    }
    
    public sealed partial class XGaScalar<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> EAcp(XGaScalar<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> EAcp(XGaVector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> EAcp(XGaBivector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> EAcp(XGaHigherKVector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> EAcp(XGaKVector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Acp(XGaScalar<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> Acp(XGaVector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> Acp(XGaBivector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> Acp(XGaHigherKVector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> Acp(XGaKVector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }

    }

    public sealed partial class XGaVector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> EAcp(XGaScalar<T> mv2)
        {
            return Times(mv2.ScalarValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> EAcp(XGaVector<T> mv2)
        {
            //return (Gp(mv2) + mv2.Gp(this)).Divide(2d);

            return ESp(mv2);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> Acp(XGaScalar<T> mv2)
        {
            return Times(mv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Acp(XGaVector<T> mv2)
        {
            //return (Gp(mv2) + mv2.Gp(this)).Divide(2d);

            return Sp(mv2);
        }
    }
    
    public sealed partial class XGaBivector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> EAcp(XGaScalar<T> mv2)
        {
            return Times(mv2.ScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> Acp(XGaScalar<T> mv2)
        {
            return Times(mv2.ScalarValue);
        }
    }
    
    public sealed partial class XGaHigherKVector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> EAcp(XGaScalar<T> mv2)
        {
            return Times(mv2.ScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> Acp(XGaScalar<T> mv2)
        {
            return Times(mv2.ScalarValue);
        }
    }
    
    public sealed partial class XGaKVectorComposer<T>
    {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> AddEAcpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EAcpIsNonZero
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> AddAcpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EAcpIsNonZero
            );
        }

    }

    public sealed partial class XGaUniformMultivectorComposer<T>
    {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivectorComposer<T> AddEAcpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EAcpIsNonZero
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivectorComposer<T> AddAcpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EAcpIsNonZero
            );
        }

    }
    
    public sealed partial class XGaGradedMultivectorComposer<T>
    {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddEAcpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EAcpIsNonZero
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddAcpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EAcpIsNonZero
            );
        }

    }
}
