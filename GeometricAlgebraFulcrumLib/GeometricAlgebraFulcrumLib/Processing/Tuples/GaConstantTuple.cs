using System;
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Processing.Tuples
{
    public sealed class GaConstantTuple<T>
        : IGaTuple<T>
    {
        public static GaConstantTuple<T> Create(IScalarProcessor<T> itemScalarsDomain, T scalar)
        {
            return new(itemScalarsDomain, scalar);
        }

        public static GaConstantTuple<T> CreateZero(IScalarProcessor<T> itemScalarsDomain)
        {
            return new(itemScalarsDomain, itemScalarsDomain.ScalarZero);
        }


        public static GaConstantTuple<T> operator -(GaConstantTuple<T> scalarsTuple1)
        {
            return new(
                scalarsTuple1.ScalarProcessor, 
                scalarsTuple1.ScalarProcessor.Negative(scalarsTuple1.Scalar)
            );
        }

        public static GaConstantTuple<T> operator +(GaConstantTuple<T> scalarsTuple1, GaConstantTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Add(scalarsTuple2);
        }

        public static GaSparseTuple<T> operator +(GaConstantTuple<T> scalarsTuple1, GaSparseTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Add(scalarsTuple2);
        }

        public static GaConstantTuple<T> operator -(GaConstantTuple<T> scalarsTuple1, GaConstantTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Subtract(scalarsTuple2);
        }

        public static GaSparseTuple<T> operator -(GaConstantTuple<T> scalarsTuple1, GaSparseTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Subtract(scalarsTuple2);
        }
        
        public static GaConstantTuple<T> operator *(GaConstantTuple<T> scalarsTuple1, GaConstantTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Times(scalarsTuple2);
        }
        
        public static GaSparseTuple<T> operator *(GaConstantTuple<T> scalarsTuple1, GaSparseTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Times(scalarsTuple2);
        }
        
        public static GaConstantTuple<T> operator /(GaConstantTuple<T> scalarsTuple1, GaConstantTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Divide(scalarsTuple2);
        }

        public static GaSparseTuple<T> operator /(GaConstantTuple<T> scalarsTuple1, GaSparseTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Divide(scalarsTuple2);
        }


        public T Scalar { get; }

        public IScalarProcessor<T> ScalarProcessor { get; }

        public T this[int key] 
            => Scalar;


        private GaConstantTuple([NotNull] IScalarProcessor<T> itemScalarsDomain, [NotNull] T scalar)
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


        public GaConstantTuple<T> Add([NotNull] GaConstantTuple<T> scalarsTuple2)
        {
            return new(
                ScalarProcessor,
                ScalarProcessor.Add(Scalar, scalarsTuple2.Scalar)
            );
        }
        
        public GaSparseTuple<T> Add([NotNull] GaSparseTuple<T> scalarsTuple2)
        {
            var composer = new GaSparseTupleComposer<T>(
                ScalarProcessor,
                scalarsTuple2.CopyToDictionary(
                    s => ScalarProcessor.Add(Scalar, s)
                )
            );

            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }

        public IGaTuple<T> Add(IGaTuple<T> scalarsTuple2)
        {
            if (scalarsTuple2 is GaConstantTuple<T> constantScalarsTuple2)
                return Add(constantScalarsTuple2);

            return Add((GaSparseTuple<T>) scalarsTuple2);
        }
        

        public GaConstantTuple<T> Subtract([NotNull] GaConstantTuple<T> scalarsTuple2)
        {
            return new(
                ScalarProcessor,
                ScalarProcessor.Subtract(Scalar, scalarsTuple2.Scalar)
            );
        }
        
        public GaSparseTuple<T> Subtract([NotNull] GaSparseTuple<T> scalarsTuple2)
        {
            var composer = new GaSparseTupleComposer<T>(
                ScalarProcessor,
                scalarsTuple2.CopyToDictionary(
                    s => ScalarProcessor.Subtract(Scalar, s)
                )
            );

            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }

        public IGaTuple<T> Subtract(IGaTuple<T> scalarsTuple2)
        {
            if (scalarsTuple2 is GaConstantTuple<T> constantScalarsTuple2)
                return Subtract(constantScalarsTuple2);

            return Subtract((GaSparseTuple<T>) scalarsTuple2);
        }


        public GaConstantTuple<T> Times([NotNull] GaConstantTuple<T> scalarsTuple2)
        {
            return new(
                ScalarProcessor,
                ScalarProcessor.Times(Scalar, scalarsTuple2.Scalar)
            );
        }
        
        public GaSparseTuple<T> Times([NotNull] GaSparseTuple<T> scalarsTuple2)
        {
            var composer = new GaSparseTupleComposer<T>(
                ScalarProcessor,
                scalarsTuple2.CopyToDictionary(
                    s => ScalarProcessor.Times(Scalar, s)
                )
            );

            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }

        public IGaTuple<T> Times(IGaTuple<T> scalarsTuple2)
        {
            if (scalarsTuple2 is GaConstantTuple<T> constantScalarsTuple2)
                return Times(constantScalarsTuple2);

            return Times((GaSparseTuple<T>) scalarsTuple2);
        }


        public GaConstantTuple<T> Divide([NotNull] GaConstantTuple<T> scalarsTuple2)
        {
            return new(
                ScalarProcessor,
                ScalarProcessor.Divide(Scalar, scalarsTuple2.Scalar)
            );
        }
        
        public GaSparseTuple<T> Divide([NotNull] GaSparseTuple<T> scalarsTuple2)
        {
            var composer = new GaSparseTupleComposer<T>(
                ScalarProcessor,
                scalarsTuple2.CopyToDictionary(
                    s => ScalarProcessor.Divide(Scalar, s)
                )
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
            return new GaConstantTuple<T>(
                ScalarProcessor,
                ScalarProcessor.Negative(Scalar)
            );
        }

        public IGaTuple<T> MapScalars(Func<T, T> mappingFunc)
        {
            return new GaConstantTuple<T>(
                ScalarProcessor,
                mappingFunc(Scalar)
            );
        }

        public GaConstantTuple<T> MapScalars([NotNull] GaConstantTuple<T> scalarsTuple2, Func<T, T, T> mappingFunc)
        {
            return new(
                ScalarProcessor,
                mappingFunc(Scalar, scalarsTuple2.Scalar)
            );
        }

        public GaSparseTuple<T> MapScalars([NotNull] GaSparseTuple<T> scalarsTuple2, Func<T, T, T> mappingFunc)
        {
            var composer = new GaSparseTupleComposer<T>(ScalarProcessor);

            var scalar1 = Scalar;
            foreach (var (index, scalar2) in scalarsTuple2)
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