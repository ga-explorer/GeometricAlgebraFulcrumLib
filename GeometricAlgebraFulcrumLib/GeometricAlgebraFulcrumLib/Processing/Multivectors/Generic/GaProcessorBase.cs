using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Binary;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Unary;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Processing.ScalarsGrids.Generic;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Generic
{
    public abstract class GaProcessorBase<T> :
        GaScalarsGridProcessor<T>,
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

        public abstract IGaStorageKVector<T> PseudoScalar { get; }

        public abstract IGaStorageKVector<T> PseudoScalarInverse { get; }

        public abstract IGaStorageKVector<T> PseudoScalarReverse { get; }


        protected GaProcessorBase([NotNull] IGaScalarProcessor<T> scalarProcessor)
            : base(scalarProcessor)
        {
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual IGaStorageMultivector<T> Normalize(IGaStorageMultivector<T> mv1)
        {
            return ScalarProcessor.Divide(
                mv1,
                ScalarProcessor.SqrtOfAbs(NormSquared(mv1))
            );
        }


        public abstract T Sp(IGaStorageMultivector<T> mv1);

        public abstract T Sp(IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2);

        public abstract IGaStorageMultivector<T> Lcp(IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2);

        public abstract IGaStorageMultivector<T> Rcp(IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2);

        public abstract IGaStorageMultivector<T> Hip(IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2);

        public abstract IGaStorageMultivector<T> Fdp(IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2);

        public abstract IGaStorageMultivector<T> Acp(IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2);

        public abstract IGaStorageMultivector<T> Cp(IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2);

        public abstract T NormSquared(IGaStorageMultivector<T> mv1);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Norm(IGaStorageMultivector<T> mv1)
        {
            return ScalarProcessor.SqrtOfAbs(NormSquared(mv1));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaStorageMultivector<T> Dual(IGaStorageMultivector<T> mv1)
        {
            return Lcp(mv1, PseudoScalarInverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaStorageMultivector<T> UnDual(IGaStorageMultivector<T> mv1)
        {
            return Lcp(mv1, PseudoScalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaStorageMultivector<T> BladeInverse(IGaStorageMultivector<T> mv1)
        {
            var bladeSpSquared = Sp(mv1);

            return ScalarProcessor.Divide(mv1, bladeSpSquared);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaStorageMultivector<T> VersorInverse(IGaStorageMultivector<T> mv1)
        {
            var versorSpReverse = NormSquared(mv1);

            return ScalarProcessor.Divide(
                ScalarProcessor.Reverse(mv1), 
                versorSpReverse
            );
        }
    }
}