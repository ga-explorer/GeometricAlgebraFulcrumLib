using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.Signatures;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra
{
    public abstract class GeometricAlgebraProcessorBase<T> :
        LinearAlgebraProcessor<T>,
        IGeometricAlgebraProcessor<T>
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

        public abstract IGeometricAlgebraSignature Signature { get; }

        public abstract bool IsEuclidean { get; }

        public abstract bool IsOrthonormal { get; }

        public abstract bool IsChangeOfBasis { get; }

        public abstract KVectorStorage<T> PseudoScalar { get; }

        public abstract KVectorStorage<T> PseudoScalarInverse { get; }

        public abstract KVectorStorage<T> PseudoScalarReverse { get; }


        protected GeometricAlgebraProcessorBase([NotNull] IScalarAlgebraProcessor<T> scalarProcessor)
            : base(scalarProcessor)
        {
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual IMultivectorStorage<T> Normalize(IMultivectorStorage<T> mv1)
        {
            return ScalarProcessor.Divide(
                mv1,
                ScalarProcessor.SqrtOfAbs(NormSquared(mv1))
            );
        }


        public abstract T Sp(IMultivectorStorage<T> mv1);

        public abstract T Sp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2);

        public abstract IMultivectorStorage<T> Lcp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2);

        public abstract IMultivectorStorage<T> Rcp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2);

        public abstract IMultivectorStorage<T> Hip(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2);

        public abstract IMultivectorStorage<T> Fdp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2);

        public abstract IMultivectorStorage<T> Acp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2);

        public abstract IMultivectorStorage<T> Cp(IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2);

        public abstract T NormSquared(IMultivectorStorage<T> mv1);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Norm(IMultivectorStorage<T> mv1)
        {
            return ScalarProcessor.SqrtOfAbs(NormSquared(mv1));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> Dual(IMultivectorStorage<T> mv1)
        {
            return Lcp(mv1, PseudoScalarInverse);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> UnDual(IMultivectorStorage<T> mv1)
        {
            return Lcp(mv1, PseudoScalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> BladeInverse(IMultivectorStorage<T> mv1)
        {
            var bladeSpSquared = Sp(mv1);

            return ScalarProcessor.Divide(mv1, bladeSpSquared);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> VersorInverse(IMultivectorStorage<T> mv1)
        {
            var versorSpReverse = NormSquared(mv1);

            return ScalarProcessor.Divide(
                ScalarProcessor.Reverse(mv1), 
                versorSpReverse
            );
        }
    }
}