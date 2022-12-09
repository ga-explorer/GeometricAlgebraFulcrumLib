using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Frames;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
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

        public abstract BasisBladeSet BasisSet { get; }

        public abstract bool IsEuclidean { get; }

        public abstract bool IsOrthonormal { get; }

        public abstract bool IsChangeOfBasis { get; }

        public bool IsOrthonormalEuclidean 
            => IsOrthonormal && BasisSet.IsEuclidean;

        public abstract GaKVector<T> PseudoScalar { get; }

        public abstract GaKVector<T> PseudoScalarInverse { get; }

        public abstract GaKVector<T> PseudoScalarReverse { get; }


        protected GeometricAlgebraProcessorBase([NotNull] IScalarAlgebraProcessor<T> scalarProcessor)
            : base(scalarProcessor)
        {
        }


        public abstract Pair<BasisVectorFrame<T>> GetBasisVectorFrame();

        public abstract T SpSquared(IMultivectorStorage<T> mv1);

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
            return IsEuclidean
                ? ScalarProcessor.Sqrt(NormSquared(mv1))
                : ScalarProcessor.SqrtOfAbs(NormSquared(mv1));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> Dual(IMultivectorStorage<T> mv1)
        {
            return Lcp(mv1, PseudoScalarInverse.KVectorStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> UnDual(IMultivectorStorage<T> mv1)
        {
            return Lcp(mv1, PseudoScalar.KVectorStorage);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> BladeInverse(IMultivectorStorage<T> mv1)
        {
            return ScalarProcessor.Divide(mv1, SpSquared(mv1));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> VersorInverse(IMultivectorStorage<T> mv1)
        {
            return ScalarProcessor.Divide(ScalarProcessor.Reverse(mv1), NormSquared(mv1));
        }
    }
}