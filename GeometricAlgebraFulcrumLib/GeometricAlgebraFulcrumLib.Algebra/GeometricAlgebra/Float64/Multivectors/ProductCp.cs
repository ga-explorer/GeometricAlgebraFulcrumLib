using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors
{
    public abstract partial class XGaFloat64Multivector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar ECp(XGaFloat64Scalar _)
        {
            return Processor.ScalarZero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64Multivector ECp(XGaFloat64Vector mv2)
        {
            if (this is XGaFloat64Vector v1)
                return v1.Op(mv2);

            return this is XGaFloat64Scalar || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddECpTerms(this, mv2)
                    .GetSimpleMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64Multivector ECp(XGaFloat64Bivector mv2)
        {
            return this is XGaFloat64Scalar || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddECpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64Multivector ECp(XGaFloat64HigherKVector mv2)
        {
            return this is XGaFloat64Scalar || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddECpTerms(this, mv2)
                    .GetSimpleMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64Multivector ECp(XGaFloat64GradedMultivector mv2)
        {
            //return (EGp(mv2) - mv2.EGp(this)).Divide(2d);

            return this is XGaFloat64Scalar || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddECpTerms(this, mv2)
                    .GetSimpleMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64Multivector ECp(XGaFloat64UniformMultivector mv2)
        {
            //return (EGp(mv2) - mv2.EGp(this)).Divide(2d);

            return this is XGaFloat64Scalar || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddECpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64Multivector ECp(XGaFloat64KVector mv2)
        {
            if (this is XGaFloat64Vector v1 && mv2 is XGaFloat64Vector v2)
                return v1.Op(v2);

            return this is XGaFloat64Scalar || mv2 is XGaFloat64Scalar || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddECpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64Multivector ECp(XGaFloat64Multivector mv2)
        {
            //return (EGp(mv2) - mv2.EGp(this)).Divide(2d);

            if (this is XGaFloat64Vector v1 && mv2 is XGaFloat64Vector v2)
                return v1.Op(v2);

            return this is XGaFloat64Scalar || mv2 is XGaFloat64Scalar || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddECpTerms(this, mv2)
                    .GetSimpleMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar Cp(XGaFloat64Scalar _)
        {
            return Processor.ScalarZero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64Multivector Cp(XGaFloat64Vector mv2)
        {
            if (this is XGaFloat64Vector v1)
                return v1.Op(mv2);

            return this is XGaFloat64Scalar || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddCpTerms(this, mv2)
                    .GetSimpleMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64Multivector Cp(XGaFloat64Bivector mv2)
        {
            return this is XGaFloat64Scalar || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddCpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64Multivector Cp(XGaFloat64HigherKVector mv2)
        {
            return this is XGaFloat64Scalar || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddCpTerms(this, mv2)
                    .GetSimpleMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64Multivector Cp(XGaFloat64GradedMultivector mv2)
        {
            //return (EGp(mv2) - mv2.EGp(this)).Divide(2d);

            return this is XGaFloat64Scalar || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddCpTerms(this, mv2)
                    .GetSimpleMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64Multivector Cp(XGaFloat64UniformMultivector mv2)
        {
            //return (EGp(mv2) - mv2.EGp(this)).Divide(2d);

            return this is XGaFloat64Scalar || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddCpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64Multivector Cp(XGaFloat64KVector mv2)
        {
            if (this is XGaFloat64Vector v1 && mv2 is XGaFloat64Vector v2)
                return v1.Op(v2);

            return this is XGaFloat64Scalar || mv2 is XGaFloat64Scalar || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddCpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64Multivector Cp(XGaFloat64Multivector mv2)
        {
            //return (EGp(mv2) - mv2.EGp(this)).Divide(2d);

            if (this is XGaFloat64Vector v1 && mv2 is XGaFloat64Vector v2)
                return v1.Op(v2);

            return this is XGaFloat64Scalar || mv2 is XGaFloat64Scalar || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddCpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

    }
    
    public sealed partial class XGaFloat64Scalar
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar ECp(XGaFloat64Vector kv2)
        {
            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar ECp(XGaFloat64Bivector kv2)
        {
            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar ECp(XGaFloat64HigherKVector kv2)
        {
            return Processor.ScalarZero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar ECp(XGaFloat64GradedMultivector kv2)
        {
            return Processor.ScalarZero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar ECp(XGaFloat64UniformMultivector kv2)
        {
            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar ECp(XGaFloat64KVector kv2)
        {
            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar ECp(XGaFloat64Multivector kv2)
        {
            return Processor.ScalarZero;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar Cp(XGaFloat64Vector kv2)
        {
            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar Cp(XGaFloat64Bivector kv2)
        {
            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar Cp(XGaFloat64HigherKVector kv2)
        {
            return Processor.ScalarZero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar Cp(XGaFloat64GradedMultivector kv2)
        {
            return Processor.ScalarZero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar Cp(XGaFloat64UniformMultivector kv2)
        {
            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar Cp(XGaFloat64KVector kv2)
        {
            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar Cp(XGaFloat64Multivector kv2)
        {
            return Processor.ScalarZero;
        }

    }
    
    public sealed partial class XGaFloat64Vector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Bivector ECp(XGaFloat64Vector mv2)
        {
            //return (Gp(mv2) - mv2.Gp(this)).Divide(2d);

            return Op(mv2);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Bivector Cp(XGaFloat64Vector mv2)
        {
            //return (Gp(mv2) - mv2.Gp(this)).Divide(2d);

            return Op(mv2);
        }
    }

    
    public sealed partial class XGaFloat64KVectorComposer
    {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64KVectorComposer AddECpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ECpIsNonZero
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64KVectorComposer AddCpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ECpIsNonZero
            );
        }

    }

    public sealed partial class XGaFloat64UniformMultivectorComposer
    {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64UniformMultivectorComposer AddECpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ECpIsNonZero
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64UniformMultivectorComposer AddCpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ECpIsNonZero
            );
        }

    }

    
    public sealed partial class XGaFloat64GradedMultivectorComposer
    {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64GradedMultivectorComposer AddECpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ECpIsNonZero
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64GradedMultivectorComposer AddCpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ECpIsNonZero
            );
        }

    }
}
