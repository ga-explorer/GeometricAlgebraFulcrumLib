using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors
{
    public abstract partial class XGaFloat64Multivector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64Multivector Op(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }
        
        public abstract XGaFloat64Multivector Op(XGaFloat64Vector kv2);

        public abstract XGaFloat64Multivector Op(XGaFloat64Bivector kv2);

        public abstract XGaFloat64Multivector Op(XGaFloat64HigherKVector kv2);
        
        public abstract XGaFloat64Multivector Op(XGaFloat64GradedMultivector mv2);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector Op(XGaFloat64UniformMultivector mv2)
        {
            if (this is XGaFloat64Scalar s1)
                return mv2.Times(s1.ScalarValue);

            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddOpTerms(this, mv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64Multivector Op(XGaFloat64KVector kv2)
        {
            return kv2 switch
            {
                XGaFloat64Scalar s2 => Op(s2),
                XGaFloat64Vector v2 => Op(v2),
                XGaFloat64Bivector bv2 => Op(bv2),
                XGaFloat64HigherKVector hkv2 => Op(hkv2),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector Op(XGaFloat64Multivector mv2)
        {
            return mv2 switch
            {
                XGaFloat64Scalar s2 => Op(s2),
                XGaFloat64Vector v2 => Op(v2),
                XGaFloat64Bivector bv2 => Op(bv2),
                XGaFloat64HigherKVector kv2 => Op(kv2),
                XGaFloat64GradedMultivector gmv2 => Op(gmv2),
                XGaFloat64UniformMultivector umv2 => Op(umv2),
                _ => throw new InvalidOperationException()
            };
        }


    }

    public abstract partial class XGaFloat64KVector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector Op(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector Op(XGaFloat64Vector kv2)
        {
            if (this is XGaFloat64Scalar s1)
                return kv2.Times(s1.ScalarValue);

            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateKVectorComposer(Grade + 1)
                    .AddOpTerms(this, kv2)
                    .GetKVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector Op(XGaFloat64Bivector kv2)
        {
            if (this is XGaFloat64Scalar s1)
                return kv2.Times(s1.ScalarValue);

            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateKVectorComposer(Grade + 2)
                    .AddOpTerms(this, kv2)
                    .GetKVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector Op(XGaFloat64HigherKVector kv2)
        {
            if (this is XGaFloat64Scalar s1)
                return kv2.Times(s1.ScalarValue);

            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateKVectorComposer(Grade + kv2.Grade)
                    .AddOpTerms(this, kv2)
                    .GetKVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector Op(XGaFloat64KVector kv2)
        {
            if (this is XGaFloat64Scalar s1)
                return kv2.Times(s1.ScalarValue);

            if (kv2 is XGaFloat64Scalar s2)
                return Times(s2.ScalarValue);

            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateKVectorComposer(Grade + kv2.Grade)
                    .AddOpTerms(this, kv2)
                    .GetKVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Op(XGaFloat64GradedMultivector mv2)
        {
            if (IsZero || mv2.IsZero)
                return Processor.ScalarZero;

            if (this is XGaFloat64Scalar s1)
                return mv2.Times(s1.ScalarValue);
            
            return Processor
                .CreateMultivectorComposer()
                .AddOpTerms(this, mv2)
                .GetSimpleMultivector();
        }
    }

    public sealed partial class XGaFloat64Scalar
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar Op(XGaFloat64Scalar kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Vector Op(XGaFloat64Vector kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Bivector Op(XGaFloat64Bivector kv2)
        {
            return kv2.Times(ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64HigherKVector Op(XGaFloat64HigherKVector kv2)
        {
            return kv2.Times(ScalarValue);
        }

    }

    public sealed partial class XGaFloat64Vector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Vector Op(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Bivector Op(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.BivectorZero
                : Processor
                    .CreateKVectorComposer(2)
                    .AddOpTerms(this, kv2)
                    .GetBivector();
        }
    }

    public sealed partial class XGaFloat64Bivector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Bivector Op(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }

    }

    public sealed partial class XGaFloat64HigherKVector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64HigherKVector Op(XGaFloat64Scalar kv2)
        {
            return Times(kv2.ScalarValue);
        }

    }

    public partial class XGaFloat64GradedMultivector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Op(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddOpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Op(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddOpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Op(XGaFloat64HigherKVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddOpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Op(XGaFloat64KVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddOpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Op(XGaFloat64GradedMultivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddOpTerms(this, mv2)
                    .GetSimpleMultivector();
        }
    }

    public partial class XGaFloat64UniformMultivector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Op(XGaFloat64Vector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddOpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Op(XGaFloat64Bivector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddOpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Op(XGaFloat64HigherKVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddOpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Op(XGaFloat64KVector kv2)
        {
            return IsZero || kv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddOpTerms(this, kv2)
                    .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Op(XGaFloat64GradedMultivector mv2)
        {
            return IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateMultivectorComposer()
                    .AddOpTerms(this, mv2)
                    .GetSimpleMultivector();
        }


    }
    
    public sealed partial class XGaFloat64KVectorComposer
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64KVectorComposer AddOpTerms(XGaFloat64KVector mv1, XGaFloat64KVector mv2)
        {
            return AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.OpIsNonZero
            );
        }
    }

    public sealed partial class XGaFloat64UniformMultivectorComposer
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64UniformMultivectorComposer AddOpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
        {
            return AddEuclideanProductTerms(
                mv1,
                mv2,
                BasisBladeProductUtils.OpIsNonZero
            );
        }

    }
    
    public sealed partial class XGaFloat64GradedMultivectorComposer
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64GradedMultivectorComposer AddOpTerms(XGaFloat64KVector kv1, XGaFloat64KVector kv2)
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
        public XGaFloat64GradedMultivectorComposer AddOpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64KVector kv2)
        {
            if (mv1.IsZero || kv2.IsZero)
                return this;

            foreach (var kv1 in mv1.KVectors) 
                AddOpTerms(kv1, kv2);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64GradedMultivectorComposer AddOpTerms(XGaFloat64KVector kv1, XGaFloat64GradedMultivector mv2)
        {
            if (kv1.IsZero || mv2.IsZero)
                return this;

            foreach (var kv2 in mv2.KVectors) 
                AddOpTerms(kv1, kv2);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64GradedMultivectorComposer AddOpTerms(XGaFloat64GradedMultivector mv1, XGaFloat64GradedMultivector mv2)
        {
            if (mv1.IsZero || mv2.IsZero)
                return this;
            
            foreach (var kv1 in mv1.KVectors)
            foreach (var kv2 in mv2.KVectors)
                AddOpTerms(kv1, kv2);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64GradedMultivectorComposer AddOpTerms(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
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
