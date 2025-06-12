using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors
{
    public abstract partial class XGaMultivector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> Op(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }
        
        public abstract XGaMultivector<T> Op(XGaVector<T> kv2);

        public abstract XGaMultivector<T> Op(XGaBivector<T> kv2);

        public abstract XGaMultivector<T> Op(XGaHigherKVector<T> kv2);
        
        public abstract XGaMultivector<T> Op(XGaGradedMultivector<T> mv2);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Op(XGaUniformMultivector<T> mv2)
        {
            if (this is XGaScalar<T> s1)
                return mv2.Times(s1.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddOpTerms(this, mv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> Op(XGaKVector<T> kv2)
        {
            return kv2 switch
            {
                XGaScalar<T> s2 => Op(s2),
                XGaVector<T> v2 => Op(v2),
                XGaBivector<T> bv2 => Op(bv2),
                XGaHigherKVector<T> hkv2 => Op(hkv2),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> Op(XGaMultivector<T> mv2)
        {
            return mv2 switch
            {
                XGaScalar<T> s2 => Op(s2),
                XGaVector<T> v2 => Op(v2),
                XGaBivector<T> bv2 => Op(bv2),
                XGaHigherKVector<T> kv2 => Op(kv2),
                XGaGradedMultivector<T> gmv2 => Op(gmv2),
                XGaUniformMultivector<T> umv2 => Op(umv2),
                _ => throw new InvalidOperationException()
            };
        }


    }

    public abstract partial class XGaKVector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> Op(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> Op(XGaVector<T> kv2)
        {
            if (this is XGaScalar<T> s1)
                return kv2.Times(s1.ScalarValue);

            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateKVectorComposer(Grade + 1)
                    .AddOpTerms(this, kv2)
                    .GetKVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> Op(XGaBivector<T> kv2)
        {
            if (this is XGaScalar<T> s1)
                return kv2.Times(s1.ScalarValue);

            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateKVectorComposer(Grade + 2)
                    .AddOpTerms(this, kv2)
                    .GetKVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> Op(XGaHigherKVector<T> kv2)
        {
            if (this is XGaScalar<T> s1)
                return kv2.Times(s1.ScalarValue);

            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateKVectorComposer(Grade + kv2.Grade)
                    .AddOpTerms(this, kv2)
                    .GetKVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> Op(XGaKVector<T> kv2)
        {
            if (this is XGaScalar<T> s1)
                return kv2.Times(s1.ScalarValue);

            if (kv2 is XGaScalar<T> s2)
                return Times(s2.ScalarValue);

            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateKVectorComposer(Grade + kv2.Grade)
                    .AddOpTerms(this, kv2)
                    .GetKVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Op(XGaGradedMultivector<T> mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.ScalarZero;

            if (this is XGaScalar<T> s1)
                return mv2.Times(s1.ScalarValue);
            
            return Processor
                .CreateMultivectorComposer()
                .AddOpTerms(this, mv2)
                .GetMultivector();
        }
    }

    public sealed partial class XGaScalar<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> Op(XGaScalar<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> Op(XGaVector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> Op(XGaBivector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> Op(XGaHigherKVector<T> kv2)
        {
            return kv2.Times(ScalarValue);
        }

    }

    public sealed partial class XGaVector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> Op(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> Op(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.BivectorZero
                : Processor
                    .CreateKVectorComposer(2)
                    .AddOpTerms(this, kv2)
                    .GetBivector();
        }
    }

    public sealed partial class XGaBivector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> Op(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }

    }

    public sealed partial class XGaHigherKVector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> Op(XGaScalar<T> kv2)
        {
            return Times(kv2.ScalarValue);
        }

    }

    public partial class XGaGradedMultivector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Op(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddOpTerms(this, kv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Op(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddOpTerms(this, kv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Op(XGaHigherKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddOpTerms(this, kv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Op(XGaKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddOpTerms(this, kv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Op(XGaGradedMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddOpTerms(this, mv2)
                    .GetMultivector();
        }
    }

    public partial class XGaUniformMultivector<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Op(XGaVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddOpTerms(this, kv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Op(XGaBivector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddOpTerms(this, kv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Op(XGaHigherKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddOpTerms(this, kv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Op(XGaKVector<T> kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddOpTerms(this, kv2)
                    .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Op(XGaGradedMultivector<T> mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddOpTerms(this, mv2)
                    .GetMultivector();
        }


    }
    
    public sealed partial class XGaKVectorComposer<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVectorComposer<T> AddOpTerms(XGaKVector<T> mv1, XGaKVector<T> mv2)
        {
            return AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.OpIsNonZero
            );
        }
    }

    public sealed partial class XGaUniformMultivectorComposer<T>
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivectorComposer<T> AddOpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            return AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.OpIsNonZero
            );
        }

    }
    
    public sealed partial class XGaGradedMultivectorComposer<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddOpTerms(XGaKVector<T> kv1, XGaKVector<T> kv2)
        {
            if (kv1.IsZero || kv2.IsZero)
                return this;

            var composer = 
                Processor
                    .CreateKVectorComposer(kv2.Grade + kv1.Grade)
                    .AddOpTerms(kv1, kv2);

            if (!composer.IsZero)
                AddKVectorTerms(
                    kv2.Grade + kv1.Grade, 
                    composer.IdScalarPairs
                );

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddOpTerms(XGaGradedMultivector<T> mv1, XGaKVector<T> kv2)
        {
            if (mv1.IsZero || kv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors) 
                AddOpTerms(kv1, kv2);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddOpTerms(XGaKVector<T> kv1, XGaGradedMultivector<T> mv2)
        {
            if (kv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv2 in mv2.KVectors) 
                AddOpTerms(kv1, kv2);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddOpTerms(XGaGradedMultivector<T> mv1, XGaGradedMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;
            
            foreach (var kv1 in mv1.KVectors)
            foreach (var kv2 in mv2.KVectors)
                AddOpTerms(kv1, kv2);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivectorComposer<T> AddOpTerms(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;

            var mv2KVectors = 
                mv2.GetKVectorParts().ToArray();

            foreach (var kv1 in mv1.GetKVectorParts())
            foreach (var kv2 in mv2KVectors)
                AddOpTerms(kv1, kv2);

            return this;
        }
    }
}
