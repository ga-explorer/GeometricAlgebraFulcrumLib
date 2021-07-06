using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.Extensions;
using GeometricAlgebraLib.Processing.Scalars;

namespace GeometricAlgebraLib.Processing.Tuples
{
    public sealed class GaSparseTuple<T> 
        : IGaTuple<T>, IReadOnlyDictionary<int, T>
    {
        public static GaSparseTuple<T> Create(IGaScalarProcessor<T> itemScalarsDomain, Dictionary<int, T> indexScalarDictionary)
        {
            return new(itemScalarsDomain, indexScalarDictionary);
        }

        public static GaSparseTuple<T> CreateZero(IGaScalarProcessor<T> itemScalarsDomain)
        {
            return new(itemScalarsDomain, new Dictionary<int, T>());
        }


        public static GaSparseTuple<T> operator -(GaSparseTuple<T> scalarsTuple1)
        {
            return new(
                scalarsTuple1.ScalarProcessor, 
                scalarsTuple1._scalarsDictionary.CopyToDictionary(scalarsTuple1.ScalarProcessor.Negative)
            );
        }

        public static GaSparseTuple<T> operator +(GaSparseTuple<T> scalarsTuple1, GaConstantTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Add(scalarsTuple2);
        }

        public static GaSparseTuple<T> operator +(GaSparseTuple<T> scalarsTuple1, GaSparseTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Add(scalarsTuple2);
        }

        public static GaSparseTuple<T> operator -(GaSparseTuple<T> scalarsTuple1, GaConstantTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Subtract(scalarsTuple2);
        }

        public static GaSparseTuple<T> operator -(GaSparseTuple<T> scalarsTuple1, GaSparseTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Subtract(scalarsTuple2);
        }
        
        public static GaSparseTuple<T> operator *(GaSparseTuple<T> scalarsTuple1, GaConstantTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Times(scalarsTuple2);
        }
        
        public static GaSparseTuple<T> operator *(GaSparseTuple<T> scalarsTuple1, GaSparseTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Times(scalarsTuple2);
        }
        
        public static GaSparseTuple<T> operator /(GaSparseTuple<T> scalarsTuple1, GaConstantTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Divide(scalarsTuple2);
        }

        public static GaSparseTuple<T> operator /(GaSparseTuple<T> scalarsTuple1, GaSparseTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Divide(scalarsTuple2);
        }


        private readonly Dictionary<int, T> _scalarsDictionary;


        public IGaScalarProcessor<T> ScalarProcessor { get; }

        public int Count 
            => _scalarsDictionary.Count;

        public T this[int key] 
            => _scalarsDictionary.TryGetValue(key, out var value) 
                ? value 
                : ScalarProcessor.ZeroScalar;

        public IEnumerable<int> Keys 
            => _scalarsDictionary.Keys;

        public IEnumerable<T> Values 
            => _scalarsDictionary.Values;


        internal GaSparseTuple([NotNull] IGaScalarProcessor<T> itemScalarsDomain, [NotNull] Dictionary<int, T> indexScalarDictionary)
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
        

        public GaSparseTuple<T> Add([NotNull] GaConstantTuple<T> scalarsTuple2)
        {
            var composer = new GaSparseTupleComposer<T>(
                ScalarProcessor,
                _scalarsDictionary.CopyToDictionary(
                    s => ScalarProcessor.Add(s, scalarsTuple2.Scalar)
                )
            );
            
            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }
        
        public GaSparseTuple<T> Add([NotNull] GaSparseTuple<T> scalarsTuple2)
        {
            var composer = ScalarProcessor.CreateSparseScalarsTupleComposer(
                _scalarsDictionary.CopyToDictionary()
            );

            composer.AddScalars(scalarsTuple2._scalarsDictionary);

            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }

        public IGaTuple<T> Add(IGaTuple<T> scalarsTuple2)
        {
            if (scalarsTuple2 is GaConstantTuple<T> constantScalarsTuple2)
                return Add(constantScalarsTuple2);

            return Add((GaSparseTuple<T>) scalarsTuple2);
        }
        
        public GaSparseTuple<T> Subtract([NotNull] GaConstantTuple<T> scalarsTuple2)
        {
            var composer = new GaSparseTupleComposer<T>(
                ScalarProcessor,
                _scalarsDictionary.CopyToDictionary(
                    s => ScalarProcessor.Subtract(s, scalarsTuple2.Scalar)
                )
            );
            
            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }
        
        public GaSparseTuple<T> Subtract([NotNull] GaSparseTuple<T> scalarsTuple2)
        {
            var composer = ScalarProcessor.CreateSparseScalarsTupleComposer(
                _scalarsDictionary.CopyToDictionary()
            );

            composer.SubtractScalars(scalarsTuple2._scalarsDictionary);

            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }

        public IGaTuple<T> Subtract(IGaTuple<T> scalarsTuple2)
        {
            if (scalarsTuple2 is GaConstantTuple<T> constantScalarsTuple2)
                return Subtract(constantScalarsTuple2);

            return Subtract((GaSparseTuple<T>) scalarsTuple2);
        }
        
        public GaSparseTuple<T> Times([NotNull] GaConstantTuple<T> scalarsTuple2)
        {
            var composer = new GaSparseTupleComposer<T>(
                ScalarProcessor,
                _scalarsDictionary.CopyToDictionary(
                    s => ScalarProcessor.Times(s, scalarsTuple2.Scalar)
                )
            );
            
            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }

        public GaSparseTuple<T> Times([NotNull] GaSparseTuple<T> scalarsTuple2)
        {
            var scalarProcessor = ScalarProcessor;

            var composer = scalarProcessor.CreateSparseScalarsTupleComposer(
                _scalarsDictionary.CopyToDictionary()
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

        public IGaTuple<T> Times(IGaTuple<T> scalarsTuple2)
        {
            if (scalarsTuple2 is GaConstantTuple<T> constantScalarsTuple2)
                return Times(constantScalarsTuple2);

            return Times((GaSparseTuple<T>) scalarsTuple2);
        }
        
        public GaSparseTuple<T> Divide([NotNull] GaConstantTuple<T> scalarsTuple2)
        {
            var composer = new GaSparseTupleComposer<T>(
                ScalarProcessor,
                _scalarsDictionary.CopyToDictionary(
                    s => ScalarProcessor.Divide(s, scalarsTuple2.Scalar)
                )
            );
            
            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }

        public GaSparseTuple<T> Divide([NotNull] GaSparseTuple<T> scalarsTuple2)
        {
            var fullOuterJoinTuples =
                _scalarsDictionary.FullOuterJoin(
                    scalarsTuple2._scalarsDictionary,
                    ScalarProcessor.ZeroScalar,
                    ScalarProcessor.ZeroScalar
                );

            var composer = ScalarProcessor.CreateSparseScalarsTupleComposer(
                _scalarsDictionary.CopyToDictionary()
            );

            foreach (var (index, scalar1, scalar2) in fullOuterJoinTuples)
                composer.SetScalar(
                    index, 
                    ScalarProcessor.Divide(scalar1, scalar2)
                );

            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }

        public IGaTuple<T> Divide(IGaTuple<T> scalarsTuple2)
        {
            if (scalarsTuple2 is GaConstantTuple<T> constantScalarsTuple2)
                return Divide(constantScalarsTuple2);

            return Divide((GaSparseTuple<T>) scalarsTuple2);
        }

        public IGaTuple<T> Negative()
        {
            return new GaSparseTuple<T>(
                ScalarProcessor, 
                _scalarsDictionary.CopyToDictionary(ScalarProcessor.Negative)
            );
        }

        public IGaTuple<T> MapScalars(Func<T, T> mappingFunc)
        {
            return new GaSparseTuple<T>(
                ScalarProcessor, 
                _scalarsDictionary.CopyToDictionary(mappingFunc)
            );
        }

        public GaSparseTuple<T> MapScalars([NotNull] GaConstantTuple<T> scalarsTuple2, Func<T, T, T> mappingFunc)
        {
            var composer = new GaSparseTupleComposer<T>(ScalarProcessor);

            var scalar2 = scalarsTuple2.Scalar;
            foreach (var (index, scalar1) in _scalarsDictionary)
                composer.SetScalar(index, mappingFunc(scalar1, scalar2));

            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }

        public GaSparseTuple<T> MapScalars([NotNull] GaSparseTuple<T> scalarsTuple2, Func<T, T, T> mappingFunc)
        {
            var fullOuterJoinTuples = _scalarsDictionary.FullOuterJoin(
                scalarsTuple2._scalarsDictionary,
                ScalarProcessor.ZeroScalar,
                ScalarProcessor.ZeroScalar
            );

            var composer = new GaSparseTupleComposer<T>(ScalarProcessor);

            foreach (var (index, scalar1, scalar2) in fullOuterJoinTuples)
                composer.SetScalar(index, mappingFunc(scalar1, scalar2));

            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }

        public IGaTuple<T> MapScalars(IGaTuple<T> scalarsTuple2, Func<T, T, T> mappingFunc)
        {
            if (scalarsTuple2 is GaConstantTuple<T> constantScalarsTuple2)
                return MapScalars(constantScalarsTuple2, mappingFunc);

            return MapScalars((GaSparseTuple<T>) scalarsTuple2, mappingFunc);
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
