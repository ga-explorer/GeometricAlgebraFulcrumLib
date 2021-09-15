using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Random;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    public class GeometricAlgebraRandomComposer<T> :
        RandomComposer, IGeometricAlgebraSpace
    {
        public IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        public uint VSpaceDimension { get; }

        public ulong GaSpaceDimension 
            => VSpaceDimension.ToGaSpaceDimension();

        public ulong MaxBasisBladeId 
            => (VSpaceDimension.ToGaSpaceDimension()) - 1UL;

        public uint GradesCount 
            => VSpaceDimension + 1;

        public IEnumerable<uint> Grades 
            => GradesCount.GetRange();


        public GeometricAlgebraRandomComposer([NotNull] IScalarAlgebraProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            if (vSpaceDimension < 2 || vSpaceDimension > GeometricAlgebraSpaceUtils.MaxVSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimension));

            ScalarProcessor = scalarProcessor;
            VSpaceDimension = vSpaceDimension;
        }

        public GeometricAlgebraRandomComposer([NotNull] IScalarAlgebraProcessor<T> scalarProcessor, uint vSpaceDimension, int seed)
            : base(seed)
        {
            if (vSpaceDimension < 2 || vSpaceDimension > GeometricAlgebraSpaceUtils.MaxVSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimension));

            ScalarProcessor = scalarProcessor;
            VSpaceDimension = vSpaceDimension;
        }

        public GeometricAlgebraRandomComposer([NotNull] IScalarAlgebraProcessor<T> scalarProcessor, uint vSpaceDimension, Random randomGenerator)
            : base(randomGenerator)
        {
            if (vSpaceDimension < 2 || vSpaceDimension > GeometricAlgebraSpaceUtils.MaxVSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimension));
            
            ScalarProcessor = scalarProcessor;
            VSpaceDimension = vSpaceDimension;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar()
        {
            return ScalarProcessor.GetScalarFromRandom(
                RandomGenerator, 
                -1d, 
                1d
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetScalar(double minValue, double maxValue)
        {
            return ScalarProcessor.GetScalarFromRandom(
                RandomGenerator, 
                minValue, 
                maxValue
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetGrade()
        {
            return (uint) RandomGenerator.Next((int) VSpaceDimension + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetBasisVectorId()
        {
            return RandomGenerator.Next((int) VSpaceDimension).BasisVectorIndexToId();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetBasisBivectorId()
        {
            return GetBasisBivectorIndex().BasisBivectorIndexToId();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetBasisBladeId()
        {
            return (ulong) RandomGenerator.Next((int) GaSpaceDimension);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetBasisBladeId(uint grade)
        {
            return GetBasisBladeIndex(grade).BasisBladeIndexToId(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GradeIndexRecord GetBasisBladeGradeIndex()
        {
            return GetBasisBladeId().BasisBladeIdToGradeIndex();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetBasisVectorIndex()
        {
            return (ulong) RandomGenerator.Next((int) VSpaceDimension);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetBasisBivectorIndex()
        {
            return GetBasisBladeIndex(2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetBasisBladeIndex(uint grade)
        {
            var kvSpaceDimension = VSpaceDimension.KVectorSpaceDimension(grade);

            return (ulong) RandomGenerator.Next((int) kvSpaceDimension);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Dictionary<ulong, T> GetKVectorIndexScalarDictionary(uint grade)
        {
            var kvSpaceDimension = 
                (int) VSpaceDimension.KVectorSpaceDimension(grade);

            return Enumerable
                .Range(0, kvSpaceDimension)
                .ToDictionary(
                    index => (ulong) index, 
                    _ => GetScalar()
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Dictionary<ulong, T> GetKVectorIndexScalarDictionary(uint grade, double minValue, double maxValue)
        {
            var kvSpaceDimension = 
                (int) VSpaceDimension.KVectorSpaceDimension(grade);

            return Enumerable
                .Range(0, kvSpaceDimension)
                .ToDictionary(
                    index => (ulong) index, 
                    _ => GetScalar(minValue, maxValue)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetScalarTerm()
        {
            return ScalarProcessor.CreateKVectorScalarStorage(GetScalar());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetScalarTerm(double minValue, double maxValue)
        {
            return ScalarProcessor.CreateKVectorScalarStorage(
                GetScalar(minValue, maxValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> GetVectorTerm()
        {
            var index = GetBasisVectorIndex();
            var scalar = GetScalar();

            return ScalarProcessor.CreateVectorTermStorage(index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> GetVectorTerm(double minValue, double maxValue)
        {
            var index = GetBasisVectorIndex();
            var scalar = GetScalar(minValue, maxValue);

            return ScalarProcessor.CreateVectorTermStorage(index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> GetVectorTermByIndex(ulong index)
        {
            return ScalarProcessor.CreateVectorTermStorage(
                index,
                GetScalar()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> GetVectorTermByIndex(ulong index, double minValue, double maxValue)
        {
            return ScalarProcessor.CreateVectorTermStorage(
                index,
                GetScalar(minValue, maxValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> GetBivectorTerm()
        {
            var index = GetBasisBivectorIndex();
            var scalar = GetScalar();

            return ScalarProcessor.CreateBivectorTermStorage(index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> GetBivectorTerm(double minValue, double maxValue)
        {
            var index = GetBasisBivectorIndex();
            var scalar = GetScalar(minValue, maxValue);

            return ScalarProcessor.CreateBivectorTermStorage(index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> GetBivectorTermByIndex(ulong index)
        {
            return ScalarProcessor.CreateBivectorTermStorage(index, GetScalar());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> GetBivectorTermByIndex(ulong index, double minValue, double maxValue)
        {
            return ScalarProcessor.CreateBivectorTermStorage(index, GetScalar(minValue, maxValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetKVectorTerm()
        {
            var (grade, index) = GetBasisBladeGradeIndex();
            var scalar = GetScalar();

            return ScalarProcessor.CreateKVectorTermStorage(grade, index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetKVectorTerm(double minValue, double maxValue)
        {
            var (grade, index) = GetBasisBladeGradeIndex();
            var scalar = GetScalar(minValue, maxValue);

            return ScalarProcessor.CreateKVectorTermStorage(grade, index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetKVectorTermOfGrade(uint grade)
        {
            var index = GetBasisBladeIndex(grade);
            var scalar = GetScalar();

            return ScalarProcessor.CreateKVectorTermStorage(grade, index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetKVectorTermOfGrade(uint grade, double minValue, double maxValue)
        {
            var index = GetBasisBladeIndex(grade);
            var scalar = GetScalar(minValue, maxValue);

            return ScalarProcessor.CreateKVectorTermStorage(grade, index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetKVectorTermById(ulong id)
        {
            return ScalarProcessor.CreateKVectorTermStorage(
                id,
                GetScalar()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetKVectorTermById(ulong id, double minValue, double maxValue)
        {
            return ScalarProcessor.CreateKVectorTermStorage(
                id,
                GetScalar(minValue, maxValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetKVectorTermByGradeIndex(uint grade, ulong index)
        {
            return ScalarProcessor.CreateKVectorTermStorage(
                grade,
                index,
                GetScalar()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetKVectorTermByGradeIndex(uint grade, ulong index, double minValue, double maxValue)
        {
            var scalar = GetScalar(minValue, maxValue);

            return ScalarProcessor.CreateKVectorTermStorage(grade, index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> GetVector()
        {
            var indexScalarDictionary =
                VSpaceDimension
                    .GetRange()
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar()
                    );

            return ScalarProcessor.CreateVectorStorage(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> GetVector(double minValue, double maxValue)
        {
            var indexScalarDictionary =
                VSpaceDimension
                    .GetRange()
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar(minValue, maxValue)
                    );

            return ScalarProcessor.CreateVectorStorage(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> GetSparseVector(int termsCount)
        {
            if (termsCount > VSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(termsCount));

            var indexScalarDictionary = 
                VSpaceDimension
                    .GetRange()
                    .Shuffled(RandomGenerator)
                    .Take(termsCount)
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar()
                    );
            
            return ScalarProcessor.CreateVectorStorage(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> GetSparseVector(int termsCount, double minValue, double maxValue)
        {
            if (termsCount > VSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(termsCount));

            var indexScalarDictionary = 
                VSpaceDimension
                    .GetRange()
                    .Shuffled(RandomGenerator)
                    .Take(termsCount)
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar(minValue, maxValue)
                    );
            
            return ScalarProcessor.CreateVectorStorage(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> GetBivector()
        {
            var kvSpaceDimension = 
                VSpaceDimension.KVectorSpaceDimension(2);

            var indexScalarDictionary =
                Enumerable
                    .Range(0, (int) kvSpaceDimension)
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar()
                    );

            return ScalarProcessor.CreateBivectorStorage(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> GetBivector(double minValue, double maxValue)
        {
            var kvSpaceDimension = 
                VSpaceDimension.KVectorSpaceDimension(2);

            var indexScalarDictionary =
                Enumerable
                    .Range(0, (int) kvSpaceDimension)
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar(minValue, maxValue)
                    );

            return ScalarProcessor.CreateBivectorStorage(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> GetSparseBivector(int termsCount)
        {
            var kvSpaceDimension = 
                (int) VSpaceDimension.KVectorSpaceDimension(2);

            if (termsCount > kvSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(termsCount));

            var indexScalarDictionary =
                Enumerable
                    .Range(0, kvSpaceDimension)
                    .Shuffled(RandomGenerator)
                    .Take(termsCount)
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar()
                    );

            return ScalarProcessor.CreateBivectorStorage(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> GetSparseBivector(int termsCount, double minValue, double maxValue)
        {
            var kvSpaceDimension = 
                (int) VSpaceDimension.KVectorSpaceDimension(2);

            if (termsCount > kvSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(termsCount));
            
            var indexScalarDictionary =
                Enumerable
                    .Range(0, kvSpaceDimension)
                    .Shuffled(RandomGenerator)
                    .Take(termsCount)
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar(minValue, maxValue)
                    );

            return ScalarProcessor.CreateBivectorStorage(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetKVector()
        {
            return GetKVectorOfGrade(GetGrade());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetKVector(double minValue, double maxValue)
        {
            return GetKVectorOfGrade(GetGrade(), minValue, maxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetKVectorOfGrade(uint grade)
        {
            var kvSpaceDimension = 
                VSpaceDimension.KVectorSpaceDimension(grade);

            var indexScalarDictionary =
                Enumerable
                    .Range(0, (int) kvSpaceDimension)
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar()
                    );

            return ScalarProcessor.CreateKVectorStorage(grade, indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetKVectorOfGrade(uint grade, double minValue, double maxValue)
        {
            var kvSpaceDimension = 
                VSpaceDimension.KVectorSpaceDimension(grade);

            var indexScalarDictionary =
                Enumerable
                    .Range(0, (int) kvSpaceDimension)
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar(minValue, maxValue)
                    );

            return ScalarProcessor.CreateKVectorStorage(grade, indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetSparseKVectorOfGrade(uint grade, int termsCount)
        {
            var kvSpaceDimension = 
                (int) VSpaceDimension.KVectorSpaceDimension(grade);

            if (termsCount > kvSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(termsCount));

            var indexScalarDictionary =
                Enumerable
                    .Range(0, kvSpaceDimension)
                    .Shuffled(RandomGenerator)
                    .Take(termsCount)
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar()
                    );

            return ScalarProcessor.CreateKVectorStorage(grade, indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetSparseKVectorOfGrade(uint grade, int termsCount, double minValue, double maxValue)
        {
            var kvSpaceDimension = 
                (int) VSpaceDimension.KVectorSpaceDimension(grade);

            if (termsCount > kvSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(termsCount));

            var indexScalarDictionary =
                Enumerable
                    .Range(0, kvSpaceDimension)
                    .Shuffled(RandomGenerator)
                    .Take(termsCount)
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar(minValue, maxValue)
                    );

            return ScalarProcessor.CreateKVectorStorage(grade, indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MultivectorStorage<T> GetTermsMultivector()
        {
            var gaSpaceDimension = (int) GaSpaceDimension;

            var idScalarDictionary =
                Enumerable
                    .Range(0, gaSpaceDimension)
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar()
                    );

            return MultivectorStorage<T>.Create(idScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MultivectorStorage<T> GetTermsMultivector(double minValue, double maxValue)
        {
            var gaSpaceDimension = (int) GaSpaceDimension;

            var idScalarDictionary =
                Enumerable
                    .Range(0, gaSpaceDimension)
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar(minValue, maxValue)
                    );

            return MultivectorStorage<T>.Create(idScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MultivectorStorage<T> GetTermsMultivector(int termsCount)
        {
            var gaSpaceDimension = (int) GaSpaceDimension;

            if (termsCount > gaSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(termsCount));

            var idScalarDictionary =
                Enumerable
                    .Range(0, gaSpaceDimension)
                    .Shuffled(RandomGenerator)
                    .Take(termsCount)
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar()
                    );

            return MultivectorStorage<T>.Create(idScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MultivectorStorage<T> GetTermsMultivector(int termsCount, double minValue, double maxValue)
        {
            var gaSpaceDimension = (int) GaSpaceDimension;

            if (termsCount > gaSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(termsCount));

            var idScalarDictionary =
                Enumerable
                    .Range(0, gaSpaceDimension)
                    .Shuffled(RandomGenerator)
                    .Take(termsCount)
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar(minValue, maxValue)
                    );

            return MultivectorStorage<T>.Create(idScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorGradedStorage<T> GetGradedMultivector()
        {
            var gradeIndexScalarDictionary = 
                new Dictionary<uint, Dictionary<ulong, T>>();

            for (var grade = 0U; grade <= VSpaceDimension; grade++)
                gradeIndexScalarDictionary.Add(
                    grade, 
                    GetKVectorIndexScalarDictionary(grade)
                );

            return ScalarProcessor.CreateMultivectorGradedStorage(gradeIndexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorGradedStorage<T> GetGradedMultivector(double minValue, double maxValue)
        {
            var gradeIndexScalarDictionary = 
                new Dictionary<uint, Dictionary<ulong, T>>();

            for (var grade = 0U; grade <= VSpaceDimension; grade++)
                gradeIndexScalarDictionary.Add(
                    grade, 
                    GetKVectorIndexScalarDictionary(grade, minValue, maxValue)
                );

            return ScalarProcessor.CreateMultivectorGradedStorage(gradeIndexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorGradedStorage<T> GetGradedMultivector(int termsCount)
        {
            var gaSpaceDimension = (int) GaSpaceDimension;

            if (termsCount > gaSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(termsCount));

            var composer = ScalarProcessor.CreateMultivectorGradedStorageComposer();

            var idList =
                Enumerable
                    .Range(0, gaSpaceDimension)
                    .Shuffled(RandomGenerator)
                    .Take(termsCount);

            foreach (var id in idList)
                composer.AddTerm((ulong) id, GetScalar());

            return composer.CreateMultivectorGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorGradedStorage<T> GetGradedMultivector(int termsCount, double minValue, double maxValue)
        {
            var gaSpaceDimension = (int) GaSpaceDimension;

            if (termsCount > gaSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(termsCount));

            var composer = ScalarProcessor.CreateMultivectorGradedStorageComposer();

            var idList =
                Enumerable
                    .Range(0, gaSpaceDimension)
                    .Shuffled(RandomGenerator)
                    .Take(termsCount);

            foreach (var id in idList)
                composer.AddTerm((ulong) id, GetScalar(minValue, maxValue));

            return composer.CreateMultivectorGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<VectorStorage<T>> GetVectors(int count)
        {
            while (count > 0)
            {
                yield return GetVector();
                count--;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetBlade(uint grade)
        {
            if (grade == 0U)
                return GetScalarTerm();

            if (grade == 1U)
                return GetVector();

            if (grade == VSpaceDimension)
                return GetKVectorTermByGradeIndex(grade, 0);

            return ScalarProcessor.Op(GetVectors((int) grade));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PureRotor<T> GetEuclideanSimpleRotor(IGeometricAlgebraProcessor<T> processor)
        {
            return processor.CreateEuclideanRotor(
                GetVector(),
                GetVector()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars(int count)
        {
            return Enumerable
                .Range(0, count)
                .Select(_ => GetScalar());
        }

        public T[,] GetArray(int rowsCount, int columnsCount)
        {
            var array = new T[rowsCount, columnsCount];

            for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < columnsCount; j++)
                array[i, j] = GetScalar();

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