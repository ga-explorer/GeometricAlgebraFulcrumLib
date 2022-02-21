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
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms
{
    public class OutermorphismsSequence<T> :
        OutermorphismBase<T>,
        IOutermorphismSequence<T>,
        IReadOnlyList<IOutermorphism<T>>
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
        
        
        private readonly List<IOutermorphism<T>> _outermorphismsList;

        public override IGeometricAlgebraProcessor<T> GeometricProcessor { get; }

        public int Count 
            => _outermorphismsList.Count;

        public IOutermorphism<T> this[int index] 
            => _outermorphismsList[index];
        

        private OutermorphismsSequence([NotNull] IGeometricAlgebraProcessor<T> processor)
        {
            _outermorphismsList = new List<IOutermorphism<T>>();
            GeometricProcessor = processor;
        }

        private OutermorphismsSequence([NotNull] IGeometricAlgebraProcessor<T> processor, [NotNull] IEnumerable<IOutermorphism<T>> versorsList)
        {
            _outermorphismsList = new List<IOutermorphism<T>>(versorsList);
            GeometricProcessor = processor;
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
                GeometricProcessor,
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
                GeometricProcessor,
                _outermorphismsList.Select(om => om.GetOmAdjoint()).Reverse()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector<T> OmMapBasisVector(ulong index)
        {
            return _outermorphismsList.OmMapBasisVector(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Bivector<T> OmMapBasisBivector(ulong index)
        {
            return _outermorphismsList.OmMapBasisBivector(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Bivector<T> OmMapBasisBivector(ulong index1, ulong index2)
        {
            return _outermorphismsList.OmMapBasisBivector(index1, index2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override KVector<T> OmMapBasisBlade(ulong id)
        {
            return _outermorphismsList.OmMapBasisBlade(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override KVector<T> OmMapBasisBlade(uint grade, ulong index)
        {
            return _outermorphismsList.OmMapBasisBlade(grade, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector<T> OmMap(Vector<T> vector)
        {
            return _outermorphismsList.OmMap(vector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Bivector<T> OmMap(Bivector<T> bivector)
        {
            return _outermorphismsList.OmMap(bivector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override KVector<T> OmMap(KVector<T> kVector)
        {
            return _outermorphismsList.OmMap(kVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Multivector<T> OmMap(Multivector<T> multivector)
        {
            return _outermorphismsList.OmMap(multivector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetMultivectorMappingMatrix()
        {
            return GetMappedBasisBlades()
                .ToDictionary(
                    r => r.Id, 
                    r => r.Multivector.MultivectorStorage.GetLinVectorIdScalarStorage())
                .CreateLinMatrixColumnsListStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetVectorOmMappingMatrix()
        {
            return GetOmMappedBasisVectors()
                .ToDictionary(
                    r => r.Index, 
                    r => r.Vector.VectorStorage.GetLinVectorIndexScalarStorage()
                )
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
        public IEnumerable<IOutermorphism<T>> GetLeafOutermorphisms()
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