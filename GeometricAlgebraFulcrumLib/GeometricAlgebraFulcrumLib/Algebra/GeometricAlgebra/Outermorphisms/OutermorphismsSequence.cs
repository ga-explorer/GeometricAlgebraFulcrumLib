using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms
{
    public class OutermorphismsSequence<T> :
        GaOutermorphismBase<T>,
        IOutermorphismSequence<T>,
        IReadOnlyList<IGaOutermorphism<T>>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static OutermorphismsSequence<T> CreateIdentity(IGeometricAlgebraProcessor<T> processor)
        {
            return new OutermorphismsSequence<T>(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static OutermorphismsSequence<T> Create(IGeometricAlgebraProcessor<T> processor, params Outermorphism<T>[] outermorphismsList)
        {
            return new OutermorphismsSequence<T>(processor, outermorphismsList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static OutermorphismsSequence<T> Create(IGeometricAlgebraProcessor<T> processor, IEnumerable<Outermorphism<T>> outermorphismsList)
        {
            return new OutermorphismsSequence<T>(processor, outermorphismsList);
        }
        
        
        private readonly List<IGaOutermorphism<T>> _outermorphismsList;

        public override IGeometricAlgebraProcessor<T> GeometricProcessor { get; }

        public int Count 
            => _outermorphismsList.Count;

        public IGaOutermorphism<T> this[int index] 
            => _outermorphismsList[index];
        

        private OutermorphismsSequence([NotNull] IGeometricAlgebraProcessor<T> processor)
        {
            _outermorphismsList = new List<IGaOutermorphism<T>>();
            GeometricProcessor = processor;
        }

        private OutermorphismsSequence([NotNull] IGeometricAlgebraProcessor<T> processor, [NotNull] IEnumerable<IGaOutermorphism<T>> versorsList)
        {
            _outermorphismsList = new List<IGaOutermorphism<T>>(versorsList);
            GeometricProcessor = processor;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaOutermorphism<T> GetOutermorphism(int index)
        {
            return _outermorphismsList[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OutermorphismsSequence<T> AppendOutermorphism([NotNull] IGaOutermorphism<T> om)
        {
            _outermorphismsList.Add(om);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OutermorphismsSequence<T> PrependOutermorphism([NotNull] IGaOutermorphism<T> om)
        {
            _outermorphismsList.Insert(0, om);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OutermorphismsSequence<T> InsertOutermorphism(int index, [NotNull] IGaOutermorphism<T> om)
        {
            _outermorphismsList.Insert(index, om);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OutermorphismsSequence<T> GetSubSequence(int startIndex, int count)
        {
            return new OutermorphismsSequence<T>(
                GeometricProcessor,
                _outermorphismsList.Skip(startIndex).Take(count)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IGaOutermorphism<T>> GetOutermorphisms()
        {
            return _outermorphismsList;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return _outermorphismsList.All(om => om.IsValid());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaOutermorphism<T> GetOmAdjoint()
        {
            return new OutermorphismsSequence<T>(
                GeometricProcessor,
                _outermorphismsList.Select(om => om.GetOmAdjoint()).Reverse()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaVector<T> OmMapBasisVector(ulong index)
        {
            return _outermorphismsList.OmMapBasisVector(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaBivector<T> OmMapBasisBivector(ulong index)
        {
            return _outermorphismsList.OmMapBasisBivector(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaBivector<T> OmMapBasisBivector(ulong index1, ulong index2)
        {
            return _outermorphismsList.OmMapBasisBivector(index1, index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaKVector<T> OmMapBasisBlade(ulong id)
        {
            return _outermorphismsList.OmMapBasisBlade(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaKVector<T> OmMapBasisBlade(uint grade, ulong index)
        {
            return _outermorphismsList.OmMapBasisBlade(grade, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaVector<T> OmMap(GaVector<T> vector)
        {
            return _outermorphismsList.OmMap(vector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaBivector<T> OmMap(GaBivector<T> bivector)
        {
            return _outermorphismsList.OmMap(bivector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaKVector<T> OmMap(GaKVector<T> kVector)
        {
            return _outermorphismsList.OmMap(kVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override GaMultivector<T> OmMap(GaMultivector<T> multivector)
        {
            return _outermorphismsList.OmMap(multivector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetMultivectorMappingMatrixStorage()
        {
            return GetMappedBasisBlades()
                .ToDictionary(
                    r => r.Id, 
                    r => r.Multivector.MultivectorStorage.GetLinVectorIdScalarStorage())
                .CreateLinMatrixColumnsListStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetVectorOmMappingMatrixStorage()
        {
            return GetOmMappedBasisVectors()
                .ToDictionary(
                    r => r.Index, 
                    r => r.Vector.VectorStorage.GetLinVectorIndexScalarStorage()
                )
                .CreateLinMatrixColumnsListStorage();
        }

        public override ILinMatrixStorage<T> GetBivectorOmMappingMatrixStorage()
        {
            throw new NotImplementedException();
        }

        public override ILinMatrixStorage<T> GetKVectorOmMappingMatrixStorage(uint grade)
        {
            throw new NotImplementedException();
        }

        public override ILinMatrixGradedStorage<T> GetMultivectorOmMappingMatrixStorage()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IndexVectorRecord<T>> GetOmMappedBasisVectors()
        {
            if (_outermorphismsList.Count == 0)
                throw new InvalidOperationException();

            var om1 = _outermorphismsList[0];
            var omList = _outermorphismsList.Skip(1);

            return om1
                .GetOmMappedBasisVectors()
                .Select(r => 
                    new IndexVectorRecord<T>(
                        r.Index,
                        omList.OmMap(r.Vector)
                    )
                )
                .Where(r => !r.Vector.VectorStorage.IsEmpty());
        }
        
        public override IEnumerable<IdMultivectorRecord<T>> GetMappedBasisBlades()
        {
            if (_outermorphismsList.Count == 0)
                throw new InvalidOperationException();

            var om1 = _outermorphismsList[0];
            var omList = _outermorphismsList.Skip(1);

            return om1
                .GetMappedBasisBlades()
                .Select(r => 
                    new IdMultivectorRecord<T>(
                        r.Id,
                        omList.OmMap(r.Multivector)
                    )
                )
                .Where(r => !r.Multivector.MultivectorStorage.IsEmpty());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IGaOutermorphism<T>> GetLeafOutermorphisms()
        {
            foreach (var om in _outermorphismsList)
            {
                if (om is IOutermorphismSequence<T> omSeq)
                    foreach (var childOm in omSeq.GetLeafOutermorphisms())
                        yield return childOm;
                else
                    yield return om;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<IGaOutermorphism<T>> GetEnumerator()
        {
            return _outermorphismsList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}