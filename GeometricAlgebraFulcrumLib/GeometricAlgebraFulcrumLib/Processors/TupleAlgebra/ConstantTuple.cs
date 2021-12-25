using System;
using System.Diagnostics.CodeAnalysis;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Processors.TupleAlgebra
{
    public sealed class ConstantTuple<T>
        : ITuple<T>
    {
        public static ConstantTuple<T> Create(IScalarAlgebraProcessor<T> itemScalarsDomain, T scalar)
        {
            return new(itemScalarsDomain, scalar);
        }

        public static ConstantTuple<T> CreateZero(IScalarAlgebraProcessor<T> itemScalarsDomain)
        {
            return new(itemScalarsDomain, itemScalarsDomain.ScalarZero);
        }


        public static ConstantTuple<T> operator -(ConstantTuple<T> scalarsTuple1)
        {
            return new(
                scalarsTuple1.ScalarProcessor, 
                scalarsTuple1.ScalarProcessor.Negative(scalarsTuple1.Scalar)
            );
        }

        public static ConstantTuple<T> operator +(ConstantTuple<T> scalarsTuple1, ConstantTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Add(scalarsTuple2);
        }

        public static SparseTuple<T> operator +(ConstantTuple<T> scalarsTuple1, SparseTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Add(scalarsTuple2);
        }

        public static ConstantTuple<T> operator -(ConstantTuple<T> scalarsTuple1, ConstantTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Subtract(scalarsTuple2);
        }

        public static SparseTuple<T> operator -(ConstantTuple<T> scalarsTuple1, SparseTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Subtract(scalarsTuple2);
        }
        
        public static ConstantTuple<T> operator *(ConstantTuple<T> scalarsTuple1, ConstantTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Times(scalarsTuple2);
        }
        
        public static SparseTuple<T> operator *(ConstantTuple<T> scalarsTuple1, SparseTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Times(scalarsTuple2);
        }
        
        public static ConstantTuple<T> operator /(ConstantTuple<T> scalarsTuple1, ConstantTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Divide(scalarsTuple2);
        }

        public static SparseTuple<T> operator /(ConstantTuple<T> scalarsTuple1, SparseTuple<T> scalarsTuple2)
        {
            return scalarsTuple1.Divide(scalarsTuple2);
        }


        public T Scalar { get; }

        public IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        public T this[int key] 
            => Scalar;


        private ConstantTuple([NotNull] IScalarAlgebraProcessor<T> itemScalarsDomain, [NotNull] T scalar)
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


        public ConstantTuple<T> Add([NotNull] ConstantTuple<T> scalarsTuple2)
        {
            return new(
                ScalarProcessor,
                ScalarProcessor.Add(Scalar, scalarsTuple2.Scalar)
            );
        }
        
        public SparseTuple<T> Add([NotNull] SparseTuple<T> scalarsTuple2)
        {
            var composer = new SparseTupleComposer<T>(
                ScalarProcessor,
                scalarsTuple2.ToDictionary(
                    s => ScalarProcessor.Add(Scalar, s)
                )
            );

            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }

        public ITuple<T> Add(ITuple<T> scalarsTuple2)
        {
            if (scalarsTuple2 is ConstantTuple<T> constantScalarsTuple2)
                return Add(constantScalarsTuple2);

            return Add((SparseTuple<T>) scalarsTuple2);
        }
        

        public ConstantTuple<T> Subtract([NotNull] ConstantTuple<T> scalarsTuple2)
        {
            return new(
                ScalarProcessor,
                ScalarProcessor.Subtract(Scalar, scalarsTuple2.Scalar)
            );
        }
        
        public SparseTuple<T> Subtract([NotNull] SparseTuple<T> scalarsTuple2)
        {
            var composer = new SparseTupleComposer<T>(
                ScalarProcessor,
                scalarsTuple2.ToDictionary(
                    s => ScalarProcessor.Subtract(Scalar, s)
                )
            );

            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }

        public ITuple<T> Subtract(ITuple<T> scalarsTuple2)
        {
            if (scalarsTuple2 is ConstantTuple<T> constantScalarsTuple2)
                return Subtract(constantScalarsTuple2);

            return Subtract((SparseTuple<T>) scalarsTuple2);
        }


        public ConstantTuple<T> Times([NotNull] ConstantTuple<T> scalarsTuple2)
        {
            return new(
                ScalarProcessor,
                ScalarProcessor.Times(Scalar, scalarsTuple2.Scalar)
            );
        }
        
        public SparseTuple<T> Times([NotNull] SparseTuple<T> scalarsTuple2)
        {
            var composer = new SparseTupleComposer<T>(
                ScalarProcessor,
                scalarsTuple2.ToDictionary(
                    s => ScalarProcessor.Times(Scalar, s)
                )
            );

            composer.RemoveZeroScalars();

            return composer.GetSparseScalarsTuple();
        }

        public ITuple<T> Times(ITuple<T> scalarsTuple2)
        {
            if (scalarsTuple2 is ConstantTuple<T> constantScalarsTuple2)
                return Times(constantScalarsTuple2);

            return Times((SparseTuple<T>) scalarsTuple2);
        }


        public ConstantTuple<T> Divide([NotNull] ConstantTuple<T> scalarsTuple2)
        {
            return new(
                ScalarProcessor,
                ScalarProcessor.Divide(Scalar, scalarsTuple2.Scalar)
            );
        }
        
        public SparseTuple<T> Divide([NotNull] SparseTuple<T> scalarsTuple2)
        {
            var composer = new SparseTupleComposer<T>(
                ScalarProcessor,
                scalarsTuple2.ToDictionary(
                    s => ScalarProcessor.Divide(Scalar, s)
                )
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
            return new ConstantTuple<T>(
                ScalarProcessor,
                ScalarProcessor.Negative(Scalar)
            );
        }

        public ITuple<T> MapScalars(Func<T, T> mappingFunc)
        {
            return new ConstantTuple<T>(
                ScalarProcessor,
                mappingFunc(Scalar)
            );
        }

        public ConstantTuple<T> MapScalars([NotNull] ConstantTuple<T> scalarsTuple2, Func<T, T, T> mappingFunc)
        {
            return new(
                ScalarProcessor,
                mappingFunc(Scalar, scalarsTuple2.Scalar)
            );
        }

        public SparseTuple<T> MapScalars([NotNull] SparseTuple<T> scalarsTuple2, Func<T, T, T> mappingFunc)
        {
            var composer = new SparseTupleComposer<T>(ScalarProcessor);

            var scalar1 = Scalar;
            foreach (var (index, scalar2) in scalarsTuple2)
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