using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms
{
    public class OutermorphismsSequence<T> :
        OutermorphismBase<T>,
        IReadOnlyList<IOutermorphism<T>>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static OutermorphismsSequence<T> CreateIdentity(ILinearAlgebraProcessor<T> processor)
        {
            return new OutermorphismsSequence<T>(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static OutermorphismsSequence<T> Create(ILinearAlgebraProcessor<T> processor, params Outermorphism<T>[] outermorphismsList)
        {
            return new OutermorphismsSequence<T>(processor, outermorphismsList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static OutermorphismsSequence<T> Create(ILinearAlgebraProcessor<T> processor, IEnumerable<Outermorphism<T>> outermorphismsList)
        {
            return new OutermorphismsSequence<T>(processor, outermorphismsList);
        }
        
        
        private readonly List<IOutermorphism<T>> _outermorphismsList;

        public override ILinearAlgebraProcessor<T> LinearProcessor { get; }

        public int Count 
            => _outermorphismsList.Count;

        public IOutermorphism<T> this[int index] 
            => _outermorphismsList[index];
        

        private OutermorphismsSequence([NotNull] ILinearAlgebraProcessor<T> processor)
        {
            _outermorphismsList = new List<IOutermorphism<T>>();
            LinearProcessor = processor;
        }

        private OutermorphismsSequence([NotNull] ILinearAlgebraProcessor<T> processor, [NotNull] IEnumerable<IOutermorphism<T>> versorsList)
        {
            _outermorphismsList = new List<IOutermorphism<T>>(versorsList);
            LinearProcessor = processor;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IOutermorphism<T> GetOutermorphism(int index)
        {
            return _outermorphismsList[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OutermorphismsSequence<T> AppendOutermorphism([NotNull] IOutermorphism<T> om)
        {
            _outermorphismsList.Add(om);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OutermorphismsSequence<T> PrependOutermorphism([NotNull] IOutermorphism<T> om)
        {
            _outermorphismsList.Insert(0, om);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OutermorphismsSequence<T> InsertOutermorphism(int index, [NotNull] IOutermorphism<T> om)
        {
            _outermorphismsList.Insert(index, om);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OutermorphismsSequence<T> GetSubSequence(int startIndex, int count)
        {
            return new OutermorphismsSequence<T>(
                LinearProcessor,
                _outermorphismsList.Skip(startIndex).Take(count)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IOutermorphism<T>> GetOutermorphisms()
        {
            return _outermorphismsList;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return _outermorphismsList.All(om => om.IsValid());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IOutermorphism<T> GetOmAdjoint()
        {
            return new OutermorphismsSequence<T>(
                LinearProcessor,
                _outermorphismsList.Select(om => om.GetOmAdjoint()).Reverse()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorage<T> OmMapBasisVector(ulong index)
        {
            return _outermorphismsList.OmMapBasisVector(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override BivectorStorage<T> OmMapBasisBivector(ulong index)
        {
            return _outermorphismsList.OmMapBasisBivector(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override BivectorStorage<T> OmMapBasisBivector(ulong index1, ulong index2)
        {
            return _outermorphismsList.OmMapBasisBivector(index1, index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override KVectorStorage<T> OmMapBasisBlade(ulong id)
        {
            return _outermorphismsList.OmMapBasisBlade(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override KVectorStorage<T> OmMapBasisBlade(uint grade, ulong index)
        {
            return _outermorphismsList.OmMapBasisBlade(grade, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override VectorStorage<T> OmMapVector(VectorStorage<T> vector)
        {
            return _outermorphismsList.OmMapVector(vector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override BivectorStorage<T> OmMapBivector(BivectorStorage<T> bivector)
        {
            return _outermorphismsList.OmMapBivector(bivector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override KVectorStorage<T> OmMapKVector(KVectorStorage<T> kVector)
        {
            return _outermorphismsList.OmMapKVector(kVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override MultivectorStorage<T> OmMapMultivector(MultivectorStorage<T> multivector)
        {
            return _outermorphismsList.OmMapMultivector(multivector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override MultivectorGradedStorage<T> OmMapMultivector(MultivectorGradedStorage<T> multivector)
        {
            return _outermorphismsList.OmMapMultivector(multivector);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetMultivectorMappingMatrix()
        {
            return GetMappedBasisBlades()
                .ToDictionary(
                    r => r.Id, 
                    r => r.Storage.GetLinVectorIdScalarStorage())
                .CreateLinMatrixColumnsListStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetVectorOmMappingMatrix()
        {
            return GetOmMappedBasisVectors()
                .ToDictionary(
                    r => r.Index, 
                    r => r.Storage.GetLinVectorIndexScalarStorage())
                .CreateLinMatrixColumnsListStorage();
        }

        public override ILinMatrixStorage<T> GetBivectorOmMappingMatrix()
        {
            throw new NotImplementedException();
        }

        public override ILinMatrixStorage<T> GetKVectorOmMappingMatrix(uint grade)
        {
            throw new NotImplementedException();
        }

        public override ILinMatrixGradedStorage<T> GetMultivectorOmMappingMatrix()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IndexVectorStorageRecord<T>> GetOmMappedBasisVectors()
        {
            if (_outermorphismsList.Count == 0)
                throw new InvalidOperationException();

            var om1 = _outermorphismsList[0];
            var omList = _outermorphismsList.Skip(1);

            return om1
                .GetOmMappedBasisVectors()
                .Select(r => 
                    new IndexVectorStorageRecord<T>(
                        r.Index,
                        omList.OmMapVector(r.Storage)
                    )
                )
                .Where(r => !r.Storage.IsEmpty());
        }
        
        public override IEnumerable<IdMultivectorStorageRecord<T>> GetMappedBasisBlades()
        {
            if (_outermorphismsList.Count == 0)
                throw new InvalidOperationException();

            var om1 = _outermorphismsList[0];
            var omList = _outermorphismsList.Skip(1);

            return om1
                .GetMappedBasisBlades()
                .Select(r => 
                    new IdMultivectorStorageRecord<T>(
                        r.Id,
                        omList.OmMapMultivector(r.Storage)
                    )
                )
                .Where(r => !r.Storage.IsEmpty());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<IOutermorphism<T>> GetEnumerator()
        {
            return _outermorphismsList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}