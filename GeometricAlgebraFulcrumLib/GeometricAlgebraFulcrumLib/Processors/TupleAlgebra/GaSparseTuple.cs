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
    public sealed class GeoSparseTuple<T> 
        : IGeoTuple<T>, IReadOnlyDictionary<int, T>
    {
        public static GeoSparseTuple<T> Create(IScalarAlgebraProcessor<T> itemScalarsDomain, Dictionary<int, T> indexScalarDictionary)
        {
            return new(itemScalarsDomain, indexScalarDictionary);
        }

        public static GeoSparseTuple<T> CreateZero(IScalarAlgebraProcessor<T> itemScalarsDomain)
        {
            return new(itemScalarsDomain, new Dictionary<int, T>());
        }


        public static GeoSparseTuple<T> operator -(GeoSparseTuple<T> scalarsTuple1)
        {
            return new(
                scalarsTuple1.ScalarProcessor, 
                scalarsTuple1._scalarsDictionary.ToDictionary(scalarsTuple1.ScalarProcessor.Negative)
            );
        }

        public static GeoSparseTuple<T> operator +(GeoSparseTuple<T> scalarsTuple1, GeoConstantTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Add(scalarsTuple2);
        }

        public static GeoSparseTuple<T> operator +(GeoSparseTuple<T> scalarsTuple1, GeoSparseTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Add(scalarsTuple2);
        }

        public static GeoSparseTuple<T> operator -(GeoSparseTuple<T> scalarsTuple1, GeoConstantTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Subtract(scalarsTuple2);
        }

        public static GeoSparseTuple<T> operator -(GeoSparseTuple<T> scalarsTuple1, GeoSparseTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Subtract(scalarsTuple2);
        }
        
        public static GeoSparseTuple<T> operator *(GeoSparseTuple<T> scalarsTuple1, GeoConstantTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Times(scalarsTuple2);
        }
        
        public static GeoSparseTuple<T> operator *(GeoSparseTuple<T> scalarsTuple1, GeoSparseTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Times(scalarsTuple2);
        }
        
        public static GeoSparseTuple<T> operator /(GeoSparseTuple<T> scalarsTuple1, GeoConstantTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Divide(scalarsTuple2);
        }

        public static GeoSparseTuple<T> operator /(GeoSparseTuple<T> scalarsTuple1, GeoSparseTuple<T> scalarsTuple2)
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


        internal GeoSparseTuple([NotNull] IScalarAlgebraProcessor<T> itemScalarsDomain, [NotNull] Dictionary<int, T> indexScalarDictionary)
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
        

        public GeoSparseTuple<T> Add([NotNull] GeoConstantTuple<T> scalarsTuple2)
        {
            var composer = new GeoSparseTupleComposer<T>(
                ScalarProcessor,
                _scalarsDictionary.ToDictionary(
                    s => ScalarProcessor.Add(s, scalarsTuple2.Scalar)
                )
            );
            
            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }
        
        public GeoSparseTuple<T> Add([NotNull] GeoSparseTuple<T> scalarsTuple2)
        {
            var composer = ScalarProcessor.CreateSparseScalarsTupleComposer(
                _scalarsDictionary.ToDictionary()
            );

            composer.AddScalars(scalarsTuple2._scalarsDictionary);

            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }

        public IGeoTuple<T> Add(IGeoTuple<T> scalarsTuple2)
        {
            if (scalarsTuple2 is GeoConstantTuple<T> constantScalarsTuple2)
                return Add(constantScalarsTuple2);

            return Add((GeoSparseTuple<T>) scalarsTuple2);
        }
        
        public GeoSparseTuple<T> Subtract([NotNull] GeoConstantTuple<T> scalarsTuple2)
        {
            var composer = new GeoSparseTupleComposer<T>(
                ScalarProcessor,
                _scalarsDictionary.ToDictionary(
                    s => ScalarProcessor.Subtract(s, scalarsTuple2.Scalar)
                )
            );
            
            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }
        
        public GeoSparseTuple<T> Subtract([NotNull] GeoSparseTuple<T> scalarsTuple2)
        {
            var composer = ScalarProcessor.CreateSparseScalarsTupleComposer(
                _scalarsDictionary.ToDictionary()
            );

            composer.SubtractScalars(scalarsTuple2._scalarsDictionary);

            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }

        public IGeoTuple<T> Subtract(IGeoTuple<T> scalarsTuple2)
        {
            if (scalarsTuple2 is GeoConstantTuple<T> constantScalarsTuple2)
                return Subtract(constantScalarsTuple2);

            return Subtract((GeoSparseTuple<T>) scalarsTuple2);
        }
        
        public GeoSparseTuple<T> Times([NotNull] GeoConstantTuple<T> scalarsTuple2)
        {
            var composer = new GeoSparseTupleComposer<T>(
                ScalarProcessor,
                _scalarsDictionary.ToDictionary(
                    s => ScalarProcessor.Times(s, scalarsTuple2.Scalar)
                )
            );
            
            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }

        public GeoSparseTuple<T> Times([NotNull] GeoSparseTuple<T> scalarsTuple2)
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

        public IGeoTuple<T> Times(IGeoTuple<T> scalarsTuple2)
        {
            if (scalarsTuple2 is GeoConstantTuple<T> constantScalarsTuple2)
                return Times(constantScalarsTuple2);

            return Times((GeoSparseTuple<T>) scalarsTuple2);
        }
        
        public GeoSparseTuple<T> Divide([NotNull] GeoConstantTuple<T> scalarsTuple2)
        {
            var composer = new GeoSparseTupleComposer<T>(
                ScalarProcessor,
                _scalarsDictionary.ToDictionary(
                    s => ScalarProcessor.Divide(s, scalarsTuple2.Scalar)
                )
            );
            
            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }

        public GeoSparseTuple<T> Divide([NotNull] GeoSparseTuple<T> scalarsTuple2)
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

        public IGeoTuple<T> Divide(IGeoTuple<T> scalarsTuple2)
        {
            if (scalarsTuple2 is GeoConstantTuple<T> constantScalarsTuple2)
                return Divide(constantScalarsTuple2);

            return Divide((GeoSparseTuple<T>) scalarsTuple2);
        }

        public IGeoTuple<T> Negative()
        {
            return new GeoSparseTuple<T>(
                ScalarProcessor, 
                _scalarsDictionary.ToDictionary(ScalarProcessor.Negative)
            );
        }

        public IGeoTuple<T> MapScalars(Func<T, T> mappingFunc)
        {
            return new GeoSparseTuple<T>(
                ScalarProcessor, 
                _scalarsDictionary.ToDictionary(mappingFunc)
            );
        }

        public GeoSparseTuple<T> MapScalars([NotNull] GeoConstantTuple<T> scalarsTuple2, Func<T, T, T> mappingFunc)
        {
            var composer = new GeoSparseTupleComposer<T>(ScalarProcessor);

            var scalar2 = scalarsTuple2.Scalar;
            foreach (var (index, scalar1) in _scalarsDictionary)
                composer.SetScalar(index, mappingFunc(scalar1, scalar2));

            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }

        public GeoSparseTuple<T> MapScalars([NotNull] GeoSparseTuple<T> scalarsTuple2, Func<T, T, T> mappingFunc)
        {
            var fullOuterJoinTuples = _scalarsDictionary.FullOuterJoin(
                scalarsTuple2._scalarsDictionary,
                ScalarProcessor.ScalarZero,
                ScalarProcessor.ScalarZero
            );

            var composer = new GeoSparseTupleComposer<T>(ScalarProcessor);

            foreach (var (index, scalar1, scalar2) in fullOuterJoinTuples)
                composer.SetScalar(index, mappingFunc(scalar1, scalar2));

            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }

        public IGeoTuple<T> MapScalars(IGeoTuple<T> scalarsTuple2, Func<T, T, T> mappingFunc)
        {
            if (scalarsTuple2 is GeoConstantTuple<T> constantScalarsTuple2)
                return MapScalars(constantScalarsTuple2, mappingFunc);

            return MapScalars((GeoSparseTuple<T>) scalarsTuple2, mappingFunc);
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
