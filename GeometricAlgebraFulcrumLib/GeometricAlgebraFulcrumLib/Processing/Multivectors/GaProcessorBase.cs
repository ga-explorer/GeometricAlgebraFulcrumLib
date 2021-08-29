using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors
{
    public abstract class GaProcessorBase<T> :
        LaProcessor<T>,
        IGaProcessor<T>
    {
        public abstract uint VSpaceDimension { get; }

        public ulong GaSpaceDimension 
            => VSpaceDimension.ToGaSpaceDimension();

        public ulong MaxBasisBladeId 
            => (VSpaceDimension.ToGaSpaceDimension()) - 1UL;

        public uint GradesCount 
            => VSpaceDimension + 1;

        public IEnumerable<uint> Grades 
            => GradesCount.GetRange();

        public abstract IGaSignature Signature { get; }
        
        public abstract bool IsOrthonormal { get; }

        public abstract bool IsChangeOfBasis { get; }

        public abstract IGaKVectorStorage<T> PseudoScalar { get; }

        public abstract IGaKVectorStorage<T> PseudoScalarInverse { get; }

        public abstract IGaKVectorStorage<T> PseudoScalarReverse { get; }


        protected GaProcessorBase([NotNull] IScalarProcessor<T> scalarProcessor)
            : base(scalarProcessor)
        {
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual IGaMultivectorStorage<T> Normalize(IGaMultivectorStorage<T> mv1)
        {
            return ScalarProcessor.Divide(
                mv1,
                ScalarProcessor.SqrtOfAbs(NormSquared(mv1))
            );
        }


        public abstract T Sp(IGaMultivectorStorage<T> mv1);

        public abstract T Sp(IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2);

        public abstract IGaMultivectorStorage<T> Lcp(IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2);

        public abstract IGaMultivectorStorage<T> Rcp(IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2);

        public abstract IGaMultivectorStorage<T> Hip(IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2);

        public abstract IGaMultivectorStorage<T> Fdp(IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2);

        public abstract IGaMultivectorStorage<T> Acp(IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2);

        public abstract IGaMultivectorStorage<T> Cp(IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2);

        public abstract T NormSquared(IGaMultivectorStorage<T> mv1);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Norm(IGaMultivectorStorage<T> mv1)
        {
            return ScalarProcessor.SqrtOfAbs(NormSquared(mv1));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaMultivectorStorage<T> Dual(IGaMultivectorStorage<T> mv1)
        {
            return Lcp(mv1, PseudoScalarInverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaMultivectorStorage<T> UnDual(IGaMultivectorStorage<T> mv1)
        {
            return Lcp(mv1, PseudoScalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaMultivectorStorage<T> BladeInverse(IGaMultivectorStorage<T> mv1)
        {
            var bladeSpSquared = Sp(mv1);

            return ScalarProcessor.Divide(mv1, bladeSpSquared);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaMultivectorStorage<T> VersorInverse(IGaMultivectorStorage<T> mv1)
        {
            var versorSpReverse = NormSquared(mv1);

            return ScalarProcessor.Divide(
                ScalarProcessor.Reverse(mv1), 
                versorSpReverse
            );
        }
    }
}