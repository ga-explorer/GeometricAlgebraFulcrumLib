using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Processors.TupleAlgebra
{
    public sealed class SparseTuple<T> 
        : ITuple<T>, IReadOnlyDictionary<int, T>
    {
        public static SparseTuple<T> Create(IScalarAlgebraProcessor<T> itemScalarsDomain, Dictionary<int, T> indexScalarDictionary)
        {
            return new(itemScalarsDomain, indexScalarDictionary);
        }

        public static SparseTuple<T> CreateZero(IScalarAlgebraProcessor<T> itemScalarsDomain)
        {
            return new(itemScalarsDomain, new Dictionary<int, T>());
        }


        public static SparseTuple<T> operator -(SparseTuple<T> scalarsTuple1)
        {
            return new(
                scalarsTuple1.ScalarProcessor, 
                scalarsTuple1._scalarsDictionary.ToDictionary(scalarsTuple1.ScalarProcessor.Negative)
            );
        }

        public static SparseTuple<T> operator +(SparseTuple<T> scalarsTuple1, ConstantTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Add(scalarsTuple2);
        }

        public static SparseTuple<T> operator +(SparseTuple<T> scalarsTuple1, SparseTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Add(scalarsTuple2);
        }

        public static SparseTuple<T> operator -(SparseTuple<T> scalarsTuple1, ConstantTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Subtract(scalarsTuple2);
        }

        public static SparseTuple<T> operator -(SparseTuple<T> scalarsTuple1, SparseTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Subtract(scalarsTuple2);
        }
        
        public static SparseTuple<T> operator *(SparseTuple<T> scalarsTuple1, ConstantTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Times(scalarsTuple2);
        }
        
        public static SparseTuple<T> operator *(SparseTuple<T> scalarsTuple1, SparseTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Times(scalarsTuple2);
        }
        
        public static SparseTuple<T> operator /(SparseTuple<T> scalarsTuple1, ConstantTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Divide(scalarsTuple2);
        }

        public static SparseTuple<T> operator /(SparseTuple<T> scalarsTuple1, SparseTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Divide(scalarsTuple2);
        }


        private readonly Dictionary<int, T> _scalarsDictionary;


        public IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        public int Count 
            => _scalarsDictionary.Count;

        public T this[int key] 
            => _scalarsDictionary.TryGetValue(key, out var value) 
                ? value 
                : ScalarProcessor.ScalarZero;

        public IEnumerable<int> Keys 
            => _scalarsDictionary.Keys;

        public IEnumerable<T> Values 
            => _scalarsDictionary.Values;


        internal SparseTuple([NotNull] IScalarAlgebraProcessor<T> itemScalarsDomain, [NotNull] Dictionary<int, T> indexScalarDictionary)
        {
            ScalarProcessor = itemScalarsDomain;
            _scalarsDictionary = indexScalarDictionary;
        }


        public bool ContainsKey(int key)
        {
            return _scalarsDictionary.ContainsKey(key);
        }

        public bool TryGetValue(int key, out T value)
        {
            return _scalarsDictionary.TryGetValue(key, out value);
        }
        

        public SparseTuple<T> Add([NotNull] ConstantTuple<T> scalarsTuple2)
        {
            var composer = new SparseTupleComposer<T>(
                ScalarProcessor,
                _scalarsDictionary.ToDictionary(
                    s => ScalarProcessor.Add(s, scalarsTuple2.Scalar)
                )
            );
            
            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }
        
        public SparseTuple<T> Add([NotNull] SparseTuple<T> scalarsTuple2)
        {
            var composer = ScalarProcessor.CreateSparseScalarsTupleComposer(
                _scalarsDictionary.ToDictionary()
            );

            composer.AddScalars(scalarsTuple2._scalarsDictionary);

            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }

        public ITuple<T> Add(ITuple<T> scalarsTuple2)
        {
            if (scalarsTuple2 is ConstantTuple<T> constantScalarsTuple2)
                return Add(constantScalarsTuple2);

            return Add((SparseTuple<T>) scalarsTuple2);
        }
        
        public SparseTuple<T> Subtract([NotNull] ConstantTuple<T> scalarsTuple2)
        {
            var composer = new SparseTupleComposer<T>(
                ScalarProcessor,
                _scalarsDictionary.ToDictionary(
                    s => ScalarProcessor.Subtract(s, scalarsTuple2.Scalar)
                )
            );
            
            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }
        
        public SparseTuple<T> Subtract([NotNull] SparseTuple<T> scalarsTuple2)
        {
            var composer = ScalarProcessor.CreateSparseScalarsTupleComposer(
                _scalarsDictionary.ToDictionary()
            );

            composer.SubtractScalars(scalarsTuple2._scalarsDictionary);

            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }

        public ITuple<T> Subtract(ITuple<T> scalarsTuple2)
        {
            if (scalarsTuple2 is ConstantTuple<T> constantScalarsTuple2)
                return Subtract(constantScalarsTuple2);

            return Subtract((SparseTuple<T>) scalarsTuple2);
        }
        
        public SparseTuple<T> Times([NotNull] ConstantTuple<T> scalarsTuple2)
        {
            var composer = new SparseTupleComposer<T>(
                ScalarProcessor,
                _scalarsDictionary.ToDictionary(
                    s => ScalarProcessor.Times(s, scalarsTuple2.Scalar)
                )
            );
            
            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }

        public SparseTuple<T> Times([NotNull] SparseTuple<T> scalarsTuple2)
        {
            var scalarProcessor = ScalarProcessor;

            var composer = scalarProcessor.CreateSparseScalarsTupleComposer(
                _scalarsDictionary.ToDictionary()
            );

            foreach (var (index, scalar1) in _scalarsDictionary)
            {
                if (scalarsTuple2.TryGetValue(index, out var scalar2))
                    composer.SetScalar(
                        index, 
                        scalarProcessor.Times(scalar1, scalar2)
                    );
            }

            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }

        public ITuple<T> Times(ITuple<T> scalarsTuple2)
        {
            if (scalarsTuple2 is ConstantTuple<T> constantScalarsTuple2)
                return Times(constantScalarsTuple2);

            return Times((SparseTuple<T>) scalarsTuple2);
        }
        
        public SparseTuple<T> Divide([NotNull] ConstantTuple<T> scalarsTuple2)
        {
            var composer = new SparseTupleComposer<T>(
                ScalarProcessor,
                _scalarsDictionary.ToDictionary(
                    s => ScalarProcessor.Divide(s, scalarsTuple2.Scalar)
                )
            );
            
            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }

        public SparseTuple<T> Divide([NotNull] SparseTuple<T> scalarsTuple2)
        {
            var fullOuterJoinTuples =
                _scalarsDictionary.FullOuterJoin(
                    scalarsTuple2._scalarsDictionary,
                    ScalarProcessor.ScalarZero,
                    ScalarProcessor.ScalarZero
                );

            var composer = ScalarProcessor.CreateSparseScalarsTupleComposer(
                _scalarsDictionary.ToDictionary()
            );

            foreach (var (index, scalar1, scalar2) in fullOuterJoinTuples)
                composer.SetScalar(
                    index, 
                    ScalarProcessor.Divide(scalar1, scalar2)
                );

            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }

        public ITuple<T> Divide(ITuple<T> scalarsTuple2)
        {
            if (scalarsTuple2 is ConstantTuple<T> constantScalarsTuple2)
                return Divide(constantScalarsTuple2);

            return Divide((SparseTuple<T>) scalarsTuple2);
        }

        public ITuple<T> Negative()
        {
            return new SparseTuple<T>(
                ScalarProcessor, 
                _scalarsDictionary.ToDictionary(ScalarProcessor.Negative)
            );
        }

        public ITuple<T> MapScalars(Func<T, T> mappingFunc)
        {
            return new SparseTuple<T>(
                ScalarProcessor, 
                _scalarsDictionary.ToDictionary(mappingFunc)
            );
        }

        public SparseTuple<T> MapScalars([NotNull] ConstantTuple<T> scalarsTuple2, Func<T, T, T> mappingFunc)
        {
            var composer = new SparseTupleComposer<T>(ScalarProcessor);

            var scalar2 = scalarsTuple2.Scalar;
            foreach (var (index, scalar1) in _scalarsDictionary)
                composer.SetScalar(index, mappingFunc(scalar1, scalar2));

            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }

        public SparseTuple<T> MapScalars([NotNull] SparseTuple<T> scalarsTuple2, Func<T, T, T> mappingFunc)
        {
            var fullOuterJoinTuples = _scalarsDictionary.FullOuterJoin(
                scalarsTuple2._scalarsDictionary,
                ScalarProcessor.ScalarZero,
                ScalarProcessor.ScalarZero
            );

            var composer = new SparseTupleComposer<T>(ScalarProcessor);

            foreach (var (index, scalar1, scalar2) in fullOuterJoinTuples)
                composer.SetScalar(index, mappingFunc(scalar1, scalar2));

            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }

        public ITuple<T> MapScalars(ITuple<T> scalarsTuple2, Func<T, T, T> mappingFunc)
        {
            if (scalarsTuple2 is ConstantTuple<T> constantScalarsTuple2)
                return MapScalars(constantScalarsTuple2, mappingFunc);

            return MapScalars((SparseTuple<T>) scalarsTuple2, mappingFunc);
        }

        public bool IsValid()
        {
            return _scalarsDictionary.Values.All(ScalarProcessor.IsValid);
        }

        public bool IsZero()
        {
            return _scalarsDictionary.Values.All(ScalarProcessor.IsZero);
        }

        public bool IsNearZero()
        {
            return _scalarsDictionary.Values.All(ScalarProcessor.IsNearZero);
        }

        public IEnumerator<KeyValuePair<int, T>> GetEnumerator()
        {
            return _scalarsDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
