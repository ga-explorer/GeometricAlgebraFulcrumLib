using System;
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Processors.TupleAlgebra
{
    public sealed class GeoConstantTuple<T>
        : IGeoTuple<T>
    {
        public static GeoConstantTuple<T> Create(IScalarAlgebraProcessor<T> itemScalarsDomain, T scalar)
        {
            return new(itemScalarsDomain, scalar);
        }

        public static GeoConstantTuple<T> CreateZero(IScalarAlgebraProcessor<T> itemScalarsDomain)
        {
            return new(itemScalarsDomain, itemScalarsDomain.ScalarZero);
        }


        public static GeoConstantTuple<T> operator -(GeoConstantTuple<T> scalarsTuple1)
        {
            return new(
                scalarsTuple1.ScalarProcessor, 
                scalarsTuple1.ScalarProcessor.Negative(scalarsTuple1.Scalar)
            );
        }

        public static GeoConstantTuple<T> operator +(GeoConstantTuple<T> scalarsTuple1, GeoConstantTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Add(scalarsTuple2);
        }

        public static GeoSparseTuple<T> operator +(GeoConstantTuple<T> scalarsTuple1, GeoSparseTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Add(scalarsTuple2);
        }

        public static GeoConstantTuple<T> operator -(GeoConstantTuple<T> scalarsTuple1, GeoConstantTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Subtract(scalarsTuple2);
        }

        public static GeoSparseTuple<T> operator -(GeoConstantTuple<T> scalarsTuple1, GeoSparseTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Subtract(scalarsTuple2);
        }
        
        public static GeoConstantTuple<T> operator *(GeoConstantTuple<T> scalarsTuple1, GeoConstantTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Times(scalarsTuple2);
        }
        
        public static GeoSparseTuple<T> operator *(GeoConstantTuple<T> scalarsTuple1, GeoSparseTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Times(scalarsTuple2);
        }
        
        public static GeoConstantTuple<T> operator /(GeoConstantTuple<T> scalarsTuple1, GeoConstantTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Divide(scalarsTuple2);
        }

        public static GeoSparseTuple<T> operator /(GeoConstantTuple<T> scalarsTuple1, GeoSparseTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Divide(scalarsTuple2);
        }


        public T Scalar { get; }

        public IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        public T this[int key] 
            => Scalar;


        private GeoConstantTuple([NotNull] IScalarAlgebraProcessor<T> itemScalarsDomain, [NotNull] T scalar)
        {
            ScalarProcessor = itemScalarsDomain;
            Scalar = scalar;
        }


        public bool ContainsKey(int key)
        {
            return key >= 0;
        }

        public bool TryGetValue(int key, out T value)
        {
            if (key >= 0)
            {
                value = Scalar;
                return true;
            }

            value = ScalarProcessor.ScalarZero;
            return false;
        }


        public GeoConstantTuple<T> Add([NotNull] GeoConstantTuple<T> scalarsTuple2)
        {
            return new(
                ScalarProcessor,
                ScalarProcessor.Add(Scalar, scalarsTuple2.Scalar)
            );
        }
        
        public GeoSparseTuple<T> Add([NotNull] GeoSparseTuple<T> scalarsTuple2)
        {
            var composer = new GeoSparseTupleComposer<T>(
                ScalarProcessor,
                scalarsTuple2.ToDictionary(
                    s => ScalarProcessor.Add(Scalar, s)
                )
            );

            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }

        public IGeoTuple<T> Add(IGeoTuple<T> scalarsTuple2)
        {
            if (scalarsTuple2 is GeoConstantTuple<T> constantScalarsTuple2)
                return Add(constantScalarsTuple2);

            return Add((GeoSparseTuple<T>) scalarsTuple2);
        }
        

        public GeoConstantTuple<T> Subtract([NotNull] GeoConstantTuple<T> scalarsTuple2)
        {
            return new(
                ScalarProcessor,
                ScalarProcessor.Subtract(Scalar, scalarsTuple2.Scalar)
            );
        }
        
        public GeoSparseTuple<T> Subtract([NotNull] GeoSparseTuple<T> scalarsTuple2)
        {
            var composer = new GeoSparseTupleComposer<T>(
                ScalarProcessor,
                scalarsTuple2.ToDictionary(
                    s => ScalarProcessor.Subtract(Scalar, s)
                )
            );

            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }

        public IGeoTuple<T> Subtract(IGeoTuple<T> scalarsTuple2)
        {
            if (scalarsTuple2 is GeoConstantTuple<T> constantScalarsTuple2)
                return Subtract(constantScalarsTuple2);

            return Subtract((GeoSparseTuple<T>) scalarsTuple2);
        }


        public GeoConstantTuple<T> Times([NotNull] GeoConstantTuple<T> scalarsTuple2)
        {
            return new(
                ScalarProcessor,
                ScalarProcessor.Times(Scalar, scalarsTuple2.Scalar)
            );
        }
        
        public GeoSparseTuple<T> Times([NotNull] GeoSparseTuple<T> scalarsTuple2)
        {
            var composer = new GeoSparseTupleComposer<T>(
                ScalarProcessor,
                scalarsTuple2.ToDictionary(
                    s => ScalarProcessor.Times(Scalar, s)
                )
            );

            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }

        public IGeoTuple<T> Times(IGeoTuple<T> scalarsTuple2)
        {
            if (scalarsTuple2 is GeoConstantTuple<T> constantScalarsTuple2)
                return Times(constantScalarsTuple2);

            return Times((GeoSparseTuple<T>) scalarsTuple2);
        }


        public GeoConstantTuple<T> Divide([NotNull] GeoConstantTuple<T> scalarsTuple2)
        {
            return new(
                ScalarProcessor,
                ScalarProcessor.Divide(Scalar, scalarsTuple2.Scalar)
            );
        }
        
        public GeoSparseTuple<T> Divide([NotNull] GeoSparseTuple<T> scalarsTuple2)
        {
            var composer = new GeoSparseTupleComposer<T>(
                ScalarProcessor,
                scalarsTuple2.ToDictionary(
                    s => ScalarProcessor.Divide(Scalar, s)
                )
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
            return new GeoConstantTuple<T>(
                ScalarProcessor,
                ScalarProcessor.Negative(Scalar)
            );
        }

        public IGeoTuple<T> MapScalars(Func<T, T> mappingFunc)
        {
            return new GeoConstantTuple<T>(
                ScalarProcessor,
                mappingFunc(Scalar)
            );
        }

        public GeoConstantTuple<T> MapScalars([NotNull] GeoConstantTuple<T> scalarsTuple2, Func<T, T, T> mappingFunc)
        {
            return new(
                ScalarProcessor,
                mappingFunc(Scalar, scalarsTuple2.Scalar)
            );
        }

        public GeoSparseTuple<T> MapScalars([NotNull] GeoSparseTuple<T> scalarsTuple2, Func<T, T, T> mappingFunc)
        {
            var composer = new GeoSparseTupleComposer<T>(ScalarProcessor);

            var scalar1 = Scalar;
            foreach (var (index, scalar2) in scalarsTuple2)
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
            return ScalarProcessor.IsValid(Scalar);
        }

        public bool IsZero()
        {
            return ScalarProcessor.IsZero(Scalar);
        }

        public bool IsNearZero()
        {
            return ScalarProcessor.IsNearZero(Scalar);
        }
    }
}