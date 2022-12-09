using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Combinations;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Frames
{
    public class BasisKVectorFrame<T> :
        IKVectorFrame<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static BasisKVectorFrame<T> Create(IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            var kVectorArray =
                geometricProcessor.
                    GaSpaceDimension
                    .GetRange()
                    .Select(geometricProcessor.CreateKVectorStorageBasis)
                    .ToArray();

            return new BasisKVectorFrame<T>(geometricProcessor, kVectorArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static BasisKVectorFrame<T> Create(IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<GaKVector<T>> kVectorList)
        {
            var kVectorArray =
                kVectorList.Select(v => v.KVectorStorage).ToArray();

            return new BasisKVectorFrame<T>(geometricProcessor, kVectorArray);
        }

        internal static BasisKVectorFrame<T> CreateFrom(BasisVectorFrame<T> vectorFrame)
        {
            var geometricProcessor = vectorFrame.GeometricProcessor;

            var kVectorArray = new KVectorStorage<T>[geometricProcessor.GaSpaceDimension];

            kVectorArray[0] = KVectorStorage<T>.CreateKVectorScalar(geometricProcessor.ScalarOne);

            for (var index = 0; index < vectorFrame.Count; index++)
                kVectorArray[1ul << index] = vectorFrame[index].VectorStorage;

            for (var grade = 2U; grade <= geometricProcessor.VSpaceDimension; grade++)
            {
                var kvSpaceDimension =
                    geometricProcessor.VSpaceDimension.GetBinomialCoefficient(grade);

                for (var index = 0UL; index < kvSpaceDimension; index++)
                {
                    var id = BasisBladeUtils.BasisBladeGradeIndexToId(grade, index);

                    var (basisVectorId, basisBladeId) =
                        id.SplitByLargestBasisVectorId();

                    kVectorArray[id] = geometricProcessor.Op(
                        kVectorArray[basisBladeId],
                        kVectorArray[basisVectorId]
                    );
                }
            }

            return new BasisKVectorFrame<T>(geometricProcessor, kVectorArray);
        }


        private readonly KVectorStorage<T>[] _kVectorArray;


        public int Count
            => (int)GeometricProcessor.GaSpaceDimension;

        public GaKVector<T> this[int index]
        {
            get => _kVectorArray[index].CreateKVector(GeometricProcessor);
            set => _kVectorArray[index] = value.KVectorStorage ?? throw new ArgumentNullException(nameof(value));
        }

        public IScalarAlgebraProcessor<T> ScalarProcessor
            => GeometricProcessor;

        public ILinearAlgebraProcessor<T> LinearProcessor
            => GeometricProcessor;

        public IGeometricAlgebraProcessor<T> GeometricProcessor { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private BasisKVectorFrame([NotNull] IGeometricAlgebraProcessor<T> geometricProcessor, [NotNull] params KVectorStorage<T>[] kVectorArray)
        {
            GeometricProcessor = geometricProcessor;

            if (kVectorArray.Length != (int)geometricProcessor.GaSpaceDimension)
                throw new ArgumentException(nameof(kVectorArray));

            _kVectorArray = kVectorArray;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BasisKVectorFrame<T> MapAsBasisUsing(Func<GaKVector<T>, GaKVector<T>> vectorMapping)
        {
            var vectorArray =
                Count
                    .GetRange()
                    .Select(index => vectorMapping(this[index]).KVectorStorage)
                    .ToArray();

            return new BasisKVectorFrame<T>(GeometricProcessor, vectorArray);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<GaKVector<T>> GetEnumerator()
        {
            return _kVectorArray.Select(
                v => v.CreateKVector(GeometricProcessor)
            ).GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}