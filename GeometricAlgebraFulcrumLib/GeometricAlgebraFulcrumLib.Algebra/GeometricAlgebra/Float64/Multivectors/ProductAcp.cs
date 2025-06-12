using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors
{
    public abstract partial class XGaFloat64Multivector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64Multivector EAcp(XGaFloat64Scalar mv2)
        {
            return Times(mv2.ScalarValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64Multivector EAcp(XGaFloat64Vector mv2)
        {
            if (this is XGaFloat64Scalar s1)
                return mv2.Times(s1.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEAcpTerms(this, mv2)
                    .GetMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64Multivector EAcp(XGaFloat64Bivector mv2)
        {
            if (this is XGaFloat64Scalar s1)
                return mv2.Times(s1.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEAcpTerms(this, mv2)
                    .GetMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64Multivector EAcp(XGaFloat64HigherKVector mv2)
        {
            if (this is XGaFloat64Scalar s1)
                return mv2.Times(s1.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEAcpTerms(this, mv2)
                    .GetMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector EAcp(XGaFloat64GradedMultivector mv2)
        {
            //return (EGp(mv2) + mv2.EGp(this)).Divide(2d);

            if (this is XGaFloat64Scalar s1)
                return mv2.Times(s1.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEAcpTerms(this, mv2)
                    .GetMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector EAcp(XGaFloat64UniformMultivector mv2)
        {
            //return (EGp(mv2) + mv2.EGp(this)).Divide(2d);

            if (this is XGaFloat64Scalar s1)
                return mv2.Times(s1.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEAcpTerms(this, mv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64Multivector EAcp(XGaFloat64KVector mv2)
        {
            if (this is XGaFloat64Scalar s1)
                return mv2.Times(s1.ScalarValue);

            if (mv2 is XGaFloat64Scalar s2)
                return Times(s2.ScalarValue);

            if (this is XGaFloat64Vector v1 && mv2 is XGaFloat64Vector v2)
                return v1.ESp(v2);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEAcpTerms(this, mv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector EAcp(XGaFloat64Multivector mv2)
        {
            //return (EGp(mv2) + mv2.EGp(this)).Divide(2d);

            if (this is XGaFloat64Scalar s1)
                return mv2.Times(s1.ScalarValue);
            
            if (mv2 is XGaFloat64Scalar s2)
                return Times(s2.ScalarValue);

            if (this is XGaFloat64Vector v1 && mv2 is XGaFloat64Vector v2)
                return v1.ESp(v2);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddEAcpTerms(this, mv2)
                    .GetMultivector();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64Multivector Acp(XGaFloat64Scalar mv2)
        {
            return Times(mv2.ScalarValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64Multivector Acp(XGaFloat64Vector mv2)
        {
            if (this is XGaFloat64Scalar s1)
                return mv2.Times(s1.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddAcpTerms(this, mv2)
                    .GetMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64Multivector Acp(XGaFloat64Bivector mv2)
        {
            if (this is XGaFloat64Scalar s1)
                return mv2.Times(s1.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddAcpTerms(this, mv2)
                    .GetMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64Multivector Acp(XGaFloat64HigherKVector mv2)
        {
            if (this is XGaFloat64Scalar s1)
                return mv2.Times(s1.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddAcpTerms(this, mv2)
                    .GetMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector Acp(XGaFloat64GradedMultivector mv2)
        {
            //return (EGp(mv2) + mv2.EGp(this)).Divide(2d);

            if (this is XGaFloat64Scalar s1)
                return mv2.Times(s1.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddAcpTerms(this, mv2)
                    .GetMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector Acp(XGaFloat64UniformMultivector mv2)
        {
            //return (EGp(mv2) + mv2.EGp(this)).Divide(2d);

            if (this is XGaFloat64Scalar s1)
                return mv2.Times(s1.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddAcpTerms(this, mv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64Multivector Acp(XGaFloat64KVector mv2)
        {
            if (this is XGaFloat64Scalar s1)
                return mv2.Times(s1.ScalarValue);

            if (mv2 is XGaFloat64Scalar s2)
                return Times(s2.ScalarValue);

            if (this is XGaFloat64Vector v1 && mv2 is XGaFloat64Vector v2)
                return v1.Sp(v2);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddAcpTerms(this, mv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector Acp(XGaFloat64Multivector mv2)
        {
            //return (EGp(mv2) + mv2.EGp(this)).Divide(2d);

            if (this is XGaFloat64Scalar s1)
                return mv2.Times(s1.ScalarValue);
            
            if (mv2 is XGaFloat64Scalar s2)
                return Times(s2.ScalarValue);

            if (this is XGaFloat64Vector v1 && mv2 is XGaFloat64Vector v2)
                return v1.Sp(v2);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddAcpTerms(this, mv2)
                    .GetMultivector();
        }
    }

    public abstract partial class XGaFloat64KVector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector EAcp(XGaFloat64Scalar mv2)
        {
            return Times(mv2.ScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector Acp(XGaFloat64Scalar mv2)
        {
            return Times(mv2.ScalarValue);
        }
    }
    
    public sealed partial class XGaFloat64Scalar
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar EAcp(XGaFloat64Scalar kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Vector EAcp(XGaFloat64Vector kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Bivector EAcp(XGaFloat64Bivector kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64HigherKVector EAcp(XGaFloat64HigherKVector kv2)
        {
            return kv2.Times(ScalarValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector EAcp(XGaFloat64KVector kv2)
        {
            return kv2.Times(ScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar Acp(XGaFloat64Scalar kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Vector Acp(XGaFloat64Vector kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Bivector Acp(XGaFloat64Bivector kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64HigherKVector Acp(XGaFloat64HigherKVector kv2)
        {
            return kv2.Times(ScalarValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector Acp(XGaFloat64KVector kv2)
        {
            return kv2.Times(ScalarValue);
        }

    }

    public sealed partial class XGaFloat64Vector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Vector EAcp(XGaFloat64Scalar mv2)
        {
            return Times(mv2.ScalarValue);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar EAcp(XGaFloat64Vector mv2)
        {
            //return (Gp(mv2) + mv2.Gp(this)).Divide(2d);

            return ESp(mv2);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Vector Acp(XGaFloat64Scalar mv2)
        {
            return Times(mv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar Acp(XGaFloat64Vector mv2)
        {
            //return (Gp(mv2) + mv2.Gp(this)).Divide(2d);

            return Sp(mv2);
        }
    }
    
    public sealed partial class XGaFloat64Bivector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Bivector EAcp(XGaFloat64Scalar mv2)
        {
            return Times(mv2.ScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Bivector Acp(XGaFloat64Scalar mv2)
        {
            return Times(mv2.ScalarValue);
        }
    }
    
    public sealed partial class XGaFloat64HigherKVector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64HigherKVector EAcp(XGaFloat64Scalar mv2)
        {
            return Times(mv2.ScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64HigherKVector Acp(XGaFloat64Scalar mv2)
        {
            return Times(mv2.ScalarValue);
        }
    }
    
    public sealed partial class XGaFloat64KVectorComposer
    {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64KVectorComposer AddEAcpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EAcpIsNonZero
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64KVectorComposer AddAcpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EAcpIsNonZero
            );
        }

    }

    public sealed partial class XGaFloat64UniformMultivectorComposer
    {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64UniformMultivectorComposer AddEAcpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EAcpIsNonZero
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64UniformMultivectorComposer AddAcpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EAcpIsNonZero
            );
        }

    }
    
    public sealed partial class XGaFloat64GradedMultivectorComposer
    {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64GradedMultivectorComposer AddEAcpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EAcpIsNonZero
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64GradedMultivectorComposer AddAcpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.EAcpIsNonZero
            );
        }

    }
}
