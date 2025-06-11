using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors
{
    public abstract partial class XGaMultivector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> ECp(XGaScalar<T> _)
        {
            return Processor.ScalarZero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> ECp(XGaVector<T> mv2)
        {
            if (this is XGaVector<T> v1)
                return v1.Op(mv2);

            return this is XGaScalar<T> || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddECpTerms(this, mv2)
                    .GetSimpleMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> ECp(XGaBivector<T> mv2)
        {
            return this is XGaScalar<T> || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddECpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> ECp(XGaHigherKVector<T> mv2)
        {
            return this is XGaScalar<T> || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddECpTerms(this, mv2)
                    .GetSimpleMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> ECp(XGaGradedMultivector<T> mv2)
        {
            //return (EGp(mv2) - mv2.EGp(this)).Divide(2d);

            return this is XGaScalar<T> || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddECpTerms(this, mv2)
                    .GetSimpleMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> ECp(XGaUniformMultivector<T> mv2)
        {
            //return (EGp(mv2) - mv2.EGp(this)).Divide(2d);

            return this is XGaScalar<T> || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddECpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> ECp(XGaKVector<T> mv2)
        {
            if (this is XGaVector<T> v1 && mv2 is XGaVector<T> v2)
                return v1.Op(v2);

            return this is XGaScalar<T> || mv2 is XGaScalar<T> || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddECpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> ECp(XGaMultivector<T> mv2)
        {
            //return (EGp(mv2) - mv2.EGp(this)).Divide(2d);

            if (this is XGaVector<T> v1 && mv2 is XGaVector<T> v2)
                return v1.Op(v2);

            return this is XGaScalar<T> || mv2 is XGaScalar<T> || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddECpTerms(this, mv2)
                    .GetSimpleMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> Cp(XGaScalar<T> _)
        {
            return Processor.ScalarZero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> Cp(XGaVector<T> mv2)
        {
            if (this is XGaVector<T> v1)
                return v1.Op(mv2);

            return this is XGaScalar<T> || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddCpTerms(this, mv2)
                    .GetSimpleMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> Cp(XGaBivector<T> mv2)
        {
            return this is XGaScalar<T> || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddCpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> Cp(XGaHigherKVector<T> mv2)
        {
            return this is XGaScalar<T> || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddCpTerms(this, mv2)
                    .GetSimpleMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> Cp(XGaGradedMultivector<T> mv2)
        {
            //return (EGp(mv2) - mv2.EGp(this)).Divide(2d);

            return this is XGaScalar<T> || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddCpTerms(this, mv2)
                    .GetSimpleMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> Cp(XGaUniformMultivector<T> mv2)
        {
            //return (EGp(mv2) - mv2.EGp(this)).Divide(2d);

            return this is XGaScalar<T> || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddCpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> Cp(XGaKVector<T> mv2)
        {
            if (this is XGaVector<T> v1 && mv2 is XGaVector<T> v2)
                return v1.Op(v2);

            return this is XGaScalar<T> || mv2 is XGaScalar<T> || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddCpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> Cp(XGaMultivector<T> mv2)
        {
            //return (EGp(mv2) - mv2.EGp(this)).Divide(2d);

            if (this is XGaVector<T> v1 && mv2 is XGaVector<T> v2)
                return v1.Op(v2);

            return this is XGaScalar<T> || mv2 is XGaScalar<T> || IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddCpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

    }
    
    public sealed partial class XGaScalar<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ECp(XGaVector<T> kv2)
        {
            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ECp(XGaBivector<T> kv2)
        {
            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ECp(XGaHigherKVector<T> kv2)
        {
            return Processor.ScalarZero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ECp(XGaGradedMultivector<T> kv2)
        {
            return Processor.ScalarZero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ECp(XGaUniformMultivector<T> kv2)
        {
            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ECp(XGaKVector<T> kv2)
        {
            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> ECp(XGaMultivector<T> kv2)
        {
            return Processor.ScalarZero;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Cp(XGaVector<T> kv2)
        {
            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Cp(XGaBivector<T> kv2)
        {
            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Cp(XGaHigherKVector<T> kv2)
        {
            return Processor.ScalarZero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Cp(XGaGradedMultivector<T> kv2)
        {
            return Processor.ScalarZero;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Cp(XGaUniformMultivector<T> kv2)
        {
            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Cp(XGaKVector<T> kv2)
        {
            return Processor.ScalarZero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Cp(XGaMultivector<T> kv2)
        {
            return Processor.ScalarZero;
        }

    }
    
    public sealed partial class XGaVector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> ECp(XGaVector<T> mv2)
        {
            //return (Gp(mv2) - mv2.Gp(this)).Divide(2d);

            return Op(mv2);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> Cp(XGaVector<T> mv2)
        {
            //return (Gp(mv2) - mv2.Gp(this)).Divide(2d);

            return Op(mv2);
        }
    }

    
    public sealed partial class XGaKVectorComposer<T>
    {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> AddECpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ECpIsNonZero
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> AddCpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ECpIsNonZero
            );
        }

    }

    public sealed partial class XGaUniformMultivectorComposer<T>
    {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivectorComposer<T> AddECpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ECpIsNonZero
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivectorComposer<T> AddCpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ECpIsNonZero
            );
        }

    }

    
    public sealed partial class XGaGradedMultivectorComposer<T>
    {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddECpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ECpIsNonZero
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddCpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddMetricProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.ECpIsNonZero
            );
        }

    }
}
