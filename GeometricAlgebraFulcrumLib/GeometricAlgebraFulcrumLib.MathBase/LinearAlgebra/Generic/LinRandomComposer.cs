using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Random;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic
{
    public class LinRandomComposer<T> :
        ScalarRandomComposer<T>
    {
        public int VSpaceDimensions { get; }
    

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal LinRandomComposer(IScalarProcessor<T> scalarProcessor, int vSpaceDimensions)
            : base(scalarProcessor)
        {
            if (vSpaceDimensions is < 2 or > 31)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            VSpaceDimensions = vSpaceDimensions;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal LinRandomComposer(IScalarProcessor<T> scalarProcessor, int vSpaceDimensions, int seed)
            : base(scalarProcessor, seed)
        {
            if (vSpaceDimensions < 1)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            VSpaceDimensions = vSpaceDimensions;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal LinRandomComposer(IScalarProcessor<T> geometricProcessor, int vSpaceDimensions, System.Random randomGenerator)
            : base(geometricProcessor, randomGenerator)
        {
            if (vSpaceDimensions < 1)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            VSpaceDimensions = vSpaceDimensions;
        }

    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetBasisVectorIndex()
        {
            return RandomGenerator.Next(VSpaceDimensions);
        }
    

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Dictionary<int, T> GetVectorIndexScalarDictionary()
        {
            return Enumerable
                .Range(0, VSpaceDimensions)
                .ToDictionary(
                    index => index,
                    _ => GetScalarValue()
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Dictionary<int, T> GetVectorIndexScalarDictionary(double minValue, double maxValue)
        {
            return Enumerable
                .Range(0, VSpaceDimensions)
                .ToDictionary(
                    index => index,
                    _ => GetScalarValue(minValue, maxValue)
                );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinBasisVector GetBasisVector()
        {
            var id = GetBasisVectorIndex();

            return new LinBasisVector(id);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinSignedBasisVector GetSignedBasisVector()
        {
            var sign = RandomGenerator.GetSign();

            return GetBasisVector().ToSignedBasisVector(sign);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorTerm<T> GetVectorTerm()
        {
            var scalar = GetScalarValue();
            var basis = GetBasisVector();

            return basis.ToTerm(ScalarProcessor, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorTerm<T> GetVectorTerm(int index)
        {
            var scalar = GetScalarValue();

            return index.ToLinBasisVector().ToTerm(ScalarProcessor, scalar);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVector<T> GetVector(bool sparseVector = true)
        {
            if (sparseVector)
            {
                var termsCount = RandomGenerator.Next(VSpaceDimensions);

                var indexScalarDictionary =
                    VSpaceDimensions
                        .GetRange()
                        .Shuffled(RandomGenerator)
                        .Take(termsCount)
                        .ToDictionary(
                            p => p,
                            _ => GetScalarValue()
                        );

                return ScalarProcessor.CreateLinVector(indexScalarDictionary);
            }

            var scalarArray =
                VSpaceDimensions
                    .GetRange()
                    .Select(_ => GetScalarValue());

            return ScalarProcessor.CreateLinVector(scalarArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVector<T> GetSparseVector(int termsCount)
        {
            if (termsCount > VSpaceDimensions)
                throw new ArgumentOutOfRangeException(nameof(termsCount));

            var indexScalarDictionary =
                VSpaceDimensions
                    .GetRange()
                    .Shuffled(RandomGenerator)
                    .Take(termsCount)
                    .ToDictionary(
                        p => p,
                        _ => GetScalarValue()
                    );

            return ScalarProcessor.CreateLinVector(indexScalarDictionary);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars(int count)
        {
            return Enumerable
                .Range(0, count)
                .Select(_ => GetScalarValue());
        }

        public T[,] GetArray(int rowsCount, int columnsCount)
        {
            var array = new T[rowsCount, columnsCount];

            for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < columnsCount; j++)
                array[i, j] = GetScalarValue();

            return array;
        }

        public T[,] GetPermutationArray(int size)
        {
            var array = new T[size, size];

            var indexList = Enumerable
                .Range(0, size)
                .Shuffled(RandomGenerator);

            var i = 0;
            foreach (var colIndex in indexList)
            {
                for (var j = 0; j < size; j++)
                    array[i, j] = j == colIndex
                        ? ScalarProcessor.ScalarOne
                        : ScalarProcessor.ScalarZero;

                i++;
            }

            return array;
        }
    }
}