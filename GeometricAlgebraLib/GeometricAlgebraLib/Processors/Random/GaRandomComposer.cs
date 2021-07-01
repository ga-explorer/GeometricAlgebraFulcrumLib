using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.Random;
using GeometricAlgebraLib.Geometry.Euclidean;
using GeometricAlgebraLib.Multivectors.Basis;
using GeometricAlgebraLib.Processors.Multivectors;
using GeometricAlgebraLib.Processors.Scalars;
using GeometricAlgebraLib.Storage;
using GeometricAlgebraLib.Storage.Composers;

namespace GeometricAlgebraLib.Processors.Random
{
    public class GaRandomComposer<TScalar> :
        RandomComposer
    {
        public IGaScalarProcessor<TScalar> ScalarProcessor { get; }

        public int VSpaceDimension { get; }

        public ulong GaSpaceDimension 
            => 1UL << VSpaceDimension;


        public GaRandomComposer([NotNull] IGaScalarProcessor<TScalar> scalarProcessor, int vSpaceDimension)
        {
            if (vSpaceDimension is < 2 or > 63)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimension));

            ScalarProcessor = scalarProcessor;
            VSpaceDimension = vSpaceDimension;
        }

        public GaRandomComposer([NotNull] IGaScalarProcessor<TScalar> scalarProcessor, int vSpaceDimension, int seed)
            : base(seed)
        {
            if (vSpaceDimension is < 2 or > 63)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimension));

            ScalarProcessor = scalarProcessor;
            VSpaceDimension = vSpaceDimension;
        }

        public GaRandomComposer([NotNull] IGaScalarProcessor<TScalar> scalarProcessor, int vSpaceDimension, System.Random randomGenerator)
            : base(randomGenerator)
        {
            if (vSpaceDimension is < 2 or > 63)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimension));
            
            ScalarProcessor = scalarProcessor;
            VSpaceDimension = vSpaceDimension;
        }


        public TScalar GetScalar()
        {
            return ScalarProcessor.GetRandomScalar(
                RandomGenerator, 
                -1d, 
                1d
            );
        }

        public TScalar GetScalar(double minValue, double maxValue)
        {
            return ScalarProcessor.GetRandomScalar(
                RandomGenerator, 
                minValue, 
                maxValue
            );
        }

        public int GetGrade()
        {
            return RandomGenerator.Next(VSpaceDimension + 1);
        }

        public ulong GetBasisVectorId()
        {
            return 1UL << RandomGenerator.Next(VSpaceDimension);
        }

        public ulong GetBasisBivectorId()
        {
            return GaBasisUtils.BasisBladeId(
                2, 
                GetBasisBivectorIndex()
            );
        }

        public ulong GetBasisBladeId()
        {
            return (ulong) RandomGenerator.Next((int) GaSpaceDimension);
        }

        public ulong GetBasisBladeId(int grade)
        {
            return GaBasisUtils.BasisBladeId(
                grade, 
                GetBasisBladeIndex(grade)
            );
        }

        public Tuple<int, ulong> GetBasisBladeGradeIndex()
        {
            return GetBasisBladeId().BasisBladeGradeIndex();
        }

        public ulong GetBasisVectorIndex()
        {
            return (ulong) RandomGenerator.Next(VSpaceDimension);
        }

        public ulong GetBasisBivectorIndex()
        {
            return GetBasisBladeIndex(2);
        }

        public ulong GetBasisBladeIndex(int grade)
        {
            var kvSpaceDimension = GaBasisUtils.KvSpaceDimension(VSpaceDimension, grade);

            return (ulong) RandomGenerator.Next((int) kvSpaceDimension);
        }

        public Dictionary<ulong, TScalar> GetKVectorIndexScalarDictionary(int grade)
        {
            var kvSpaceDimension = 
                (int) GaBasisUtils.KvSpaceDimension(VSpaceDimension, grade);

            return Enumerable
                .Range(0, kvSpaceDimension)
                .ToDictionary(
                    index => (ulong) index, 
                    _ => GetScalar()
                );
        }

        public Dictionary<ulong, TScalar> GetKVectorIndexScalarDictionary(int grade, double minValue, double maxValue)
        {
            var kvSpaceDimension = 
                (int) GaBasisUtils.KvSpaceDimension(VSpaceDimension, grade);

            return Enumerable
                .Range(0, kvSpaceDimension)
                .ToDictionary(
                    index => (ulong) index, 
                    _ => GetScalar(minValue, maxValue)
                );
        }

        public GaScalarTermStorage<TScalar> GetScalarTerm()
        {
            return GaScalarTermStorage<TScalar>.Create(
                ScalarProcessor,
                GetScalar()
            );
        }

        public GaScalarTermStorage<TScalar> GetScalarTerm(double minValue, double maxValue)
        {
            return GaScalarTermStorage<TScalar>.Create(
                ScalarProcessor,
                GetScalar(minValue, maxValue)
            );
        }

        public GaVectorTermStorage<TScalar> GetVectorTerm()
        {
            var index = GetBasisVectorIndex();
            var scalar = GetScalar();

            return GaVectorTermStorage<TScalar>.Create(
                ScalarProcessor,
                index,
                scalar
            );
        }

        public GaVectorTermStorage<TScalar> GetVectorTerm(double minValue, double maxValue)
        {
            var index = GetBasisVectorIndex();
            var scalar = GetScalar(minValue, maxValue);

            return GaVectorTermStorage<TScalar>.Create(
                ScalarProcessor,
                index,
                scalar
            );
        }

        public GaVectorTermStorage<TScalar> GetVectorTermByIndex(ulong index)
        {
            return GaVectorTermStorage<TScalar>.Create(
                ScalarProcessor,
                index,
                GetScalar()
            );
        }

        public GaVectorTermStorage<TScalar> GetVectorTermByIndex(ulong index, double minValue, double maxValue)
        {
            return GaVectorTermStorage<TScalar>.Create(
                ScalarProcessor,
                index,
                GetScalar(minValue, maxValue)
            );
        }

        public GaBivectorTermStorage<TScalar> GetBivectorTerm()
        {
            var index = GetBasisBivectorIndex();
            var scalar = GetScalar();

            return GaBivectorTermStorage<TScalar>.Create(
                ScalarProcessor,
                index,
                scalar
            );
        }

        public GaBivectorTermStorage<TScalar> GetBivectorTerm(double minValue, double maxValue)
        {
            var index = GetBasisBivectorIndex();
            var scalar = GetScalar(minValue, maxValue);

            return GaBivectorTermStorage<TScalar>.Create(
                ScalarProcessor,
                index,
                scalar
            );
        }

        public GaBivectorTermStorage<TScalar> GetBivectorTermByIndex(ulong index)
        {
            return GaBivectorTermStorage<TScalar>.Create(
                ScalarProcessor,
                index,
                GetScalar()
            );
        }

        public GaBivectorTermStorage<TScalar> GetBivectorTermByIndex(ulong index, double minValue, double maxValue)
        {
            return GaBivectorTermStorage<TScalar>.Create(
                ScalarProcessor,
                index,
                GetScalar(minValue, maxValue)
            );
        }

        public GaKVectorTermStorage<TScalar> GetKVectorTerm()
        {
            var (grade, index) = GetBasisBladeGradeIndex();
            var scalar = GetScalar();

            return GaKVectorTermStorage<TScalar>.Create(
                ScalarProcessor,
                grade,
                index,
                scalar
            );
        }

        public GaKVectorTermStorage<TScalar> GetKVectorTerm(double minValue, double maxValue)
        {
            var (grade, index) = GetBasisBladeGradeIndex();
            var scalar = GetScalar(minValue, maxValue);

            return GaKVectorTermStorage<TScalar>.Create(
                ScalarProcessor,
                grade,
                index,
                scalar
            );
        }

        public GaKVectorTermStorage<TScalar> GetKVectorTermOfGrade(int grade)
        {
            var index = GetBasisBladeIndex(grade);
            var scalar = GetScalar();

            return GaKVectorTermStorage<TScalar>.Create(
                ScalarProcessor,
                grade,
                index,
                scalar
            );
        }

        public GaKVectorTermStorage<TScalar> GetKVectorTermOfGrade(int grade, double minValue, double maxValue)
        {
            var index = GetBasisBladeIndex(grade);
            var scalar = GetScalar(minValue, maxValue);

            return GaKVectorTermStorage<TScalar>.Create(
                ScalarProcessor,
                grade,
                index,
                scalar
            );
        }

        public GaKVectorTermStorage<TScalar> GetKVectorTermById(ulong id)
        {
            return GaKVectorTermStorage<TScalar>.Create(
                ScalarProcessor,
                id,
                GetScalar()
            );
        }

        public GaKVectorTermStorage<TScalar> GetKVectorTermById(ulong id, double minValue, double maxValue)
        {
            return GaKVectorTermStorage<TScalar>.Create(
                ScalarProcessor,
                id,
                GetScalar(minValue, maxValue)
            );
        }

        public GaKVectorTermStorage<TScalar> GetKVectorTermByGradeIndex(int grade, ulong index)
        {
            return GaKVectorTermStorage<TScalar>.Create(
                ScalarProcessor,
                grade,
                index,
                GetScalar()
            );
        }

        public GaKVectorTermStorage<TScalar> GetKVectorTermByGradeIndex(int grade, ulong index, double minValue, double maxValue)
        {
            var scalar = GetScalar(minValue, maxValue);

            return GaKVectorTermStorage<TScalar>.Create(
                ScalarProcessor,
                grade,
                index,
                scalar
            );
        }

        public GaVectorStorage<TScalar> GetVector()
        {
            var indexScalarDictionary =
                Enumerable
                    .Range(0, VSpaceDimension)
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar()
                    );

            return GaVectorStorage<TScalar>.Create(
                ScalarProcessor,
                indexScalarDictionary
            );
        }

        public GaVectorStorage<TScalar> GetVector(double minValue, double maxValue)
        {
            var indexScalarDictionary =
                Enumerable
                    .Range(0, VSpaceDimension)
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar(minValue, maxValue)
                    );

            return GaVectorStorage<TScalar>.Create(
                ScalarProcessor,
                indexScalarDictionary
            );
        }

        public GaVectorStorage<TScalar> GetSparseVector(int termsCount)
        {
            if (termsCount > VSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(termsCount));

            var indexScalarDictionary = Enumerable
                .Range(0, VSpaceDimension)
                .Shuffled(RandomGenerator)
                .Take(termsCount)
                .ToDictionary(
                    index => (ulong) index,
                    _ => GetScalar()
                );
            
            return GaVectorStorage<TScalar>.Create(
                ScalarProcessor,
                indexScalarDictionary
            );
        }

        public GaVectorStorage<TScalar> GetSparseVector(int termsCount, double minValue, double maxValue)
        {
            if (termsCount > VSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(termsCount));

            var indexScalarDictionary = Enumerable
                .Range(0, VSpaceDimension)
                .Shuffled(RandomGenerator)
                .Take(termsCount)
                .ToDictionary(
                    index => (ulong) index,
                    _ => GetScalar(minValue, maxValue)
                );
            
            return GaVectorStorage<TScalar>.Create(
                ScalarProcessor,
                indexScalarDictionary
            );
        }

        public GaBivectorStorage<TScalar> GetBivector()
        {
            var kvSpaceDimension = 
                GaBasisUtils.KvSpaceDimension(VSpaceDimension, 2);

            var indexScalarDictionary =
                Enumerable
                    .Range(0, (int) kvSpaceDimension)
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar()
                    );

            return GaBivectorStorage<TScalar>.Create(
                ScalarProcessor,
                indexScalarDictionary
            );
        }

        public GaBivectorStorage<TScalar> GetBivector(double minValue, double maxValue)
        {
            var kvSpaceDimension = 
                GaBasisUtils.KvSpaceDimension(VSpaceDimension, 2);

            var indexScalarDictionary =
                Enumerable
                    .Range(0, (int) kvSpaceDimension)
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar(minValue, maxValue)
                    );

            return GaBivectorStorage<TScalar>.Create(
                ScalarProcessor,
                indexScalarDictionary
            );
        }

        public GaBivectorStorage<TScalar> GetSparseBivector(int termsCount)
        {
            var kvSpaceDimension = 
                (int) GaBasisUtils.KvSpaceDimension(VSpaceDimension, 2);

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

            return GaBivectorStorage<TScalar>.Create(
                ScalarProcessor,
                indexScalarDictionary
            );
        }

        public GaBivectorStorage<TScalar> GetSparseBivector(int termsCount, double minValue, double maxValue)
        {
            var kvSpaceDimension = 
                (int) GaBasisUtils.KvSpaceDimension(VSpaceDimension, 2);

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

            return GaBivectorStorage<TScalar>.Create(
                ScalarProcessor,
                indexScalarDictionary
            );
        }

        public GaKVectorStorage<TScalar> GetKVector()
        {
            return GetKVectorOfGrade(
                GetGrade()
            );
        }

        public GaKVectorStorage<TScalar> GetKVector(double minValue, double maxValue)
        {
            return GetKVectorOfGrade(
                GetGrade(),
                minValue,
                maxValue
            );
        }

        public GaKVectorStorage<TScalar> GetKVectorOfGrade(int grade)
        {
            var kvSpaceDimension = 
                GaBasisUtils.KvSpaceDimension(VSpaceDimension, grade);

            var indexScalarDictionary =
                Enumerable
                    .Range(0, (int) kvSpaceDimension)
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar()
                    );

            return GaKVectorStorage<TScalar>.Create(
                ScalarProcessor,
                grade,
                indexScalarDictionary
            );
        }

        public GaKVectorStorage<TScalar> GetKVectorOfGrade(int grade, double minValue, double maxValue)
        {
            var kvSpaceDimension = 
                GaBasisUtils.KvSpaceDimension(VSpaceDimension, grade);

            var indexScalarDictionary =
                Enumerable
                    .Range(0, (int) kvSpaceDimension)
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar(minValue, maxValue)
                    );

            return GaKVectorStorage<TScalar>.Create(
                ScalarProcessor,
                grade,
                indexScalarDictionary
            );
        }

        public GaKVectorStorage<TScalar> GetSparseKVectorOfGrade(int grade, int termsCount)
        {
            var kvSpaceDimension = 
                (int) GaBasisUtils.KvSpaceDimension(VSpaceDimension, grade);

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

            return GaKVectorStorage<TScalar>.Create(
                ScalarProcessor,
                grade,
                indexScalarDictionary
            );
        }

        public GaKVectorStorage<TScalar> GetSparseKVectorOfGrade(int grade, int termsCount, double minValue, double maxValue)
        {
            var kvSpaceDimension = 
                (int) GaBasisUtils.KvSpaceDimension(VSpaceDimension, grade);

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

            return GaKVectorStorage<TScalar>.Create(
                ScalarProcessor,
                grade,
                indexScalarDictionary
            );
        }

        public GaMultivectorTermsStorage<TScalar> GetMultivector()
        {
            var gaSpaceDimension = (int) GaSpaceDimension;

            var idScalarDictionary =
                Enumerable
                    .Range(0, gaSpaceDimension)
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar()
                    );

            return GaMultivectorTermsStorage<TScalar>.Create(
                ScalarProcessor,
                idScalarDictionary
            );
        }
        
        public GaMultivectorTermsStorage<TScalar> GetMultivector(double minValue, double maxValue)
        {
            var gaSpaceDimension = (int) GaSpaceDimension;

            var idScalarDictionary =
                Enumerable
                    .Range(0, gaSpaceDimension)
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar(minValue, maxValue)
                    );

            return GaMultivectorTermsStorage<TScalar>.Create(
                ScalarProcessor,
                idScalarDictionary
            );
        }
        
        public GaMultivectorTermsStorage<TScalar> GetSparseMultivector(int termsCount)
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

            return GaMultivectorTermsStorage<TScalar>.Create(
                ScalarProcessor,
                idScalarDictionary
            );
        }
        
        public GaMultivectorTermsStorage<TScalar> GetSparseMultivector(int termsCount, double minValue, double maxValue)
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

            return GaMultivectorTermsStorage<TScalar>.Create(
                ScalarProcessor,
                idScalarDictionary
            );
        }

        public GaMultivectorGradedStorage<TScalar> GetGradedMultivector()
        {
            var gradeIndexScalarDictionary = 
                new Dictionary<int, Dictionary<ulong, TScalar>>();

            for (var grade = 0; grade <= VSpaceDimension; grade++)
                gradeIndexScalarDictionary.Add(
                    grade, 
                    GetKVectorIndexScalarDictionary(grade)
                );

            return GaMultivectorGradedStorage<TScalar>.Create(
                ScalarProcessor,
                gradeIndexScalarDictionary
            );
        }

        public GaMultivectorGradedStorage<TScalar> GetGradedMultivector(double minValue, double maxValue)
        {
            var gradeIndexScalarDictionary = 
                new Dictionary<int, Dictionary<ulong, TScalar>>();

            for (var grade = 0; grade <= VSpaceDimension; grade++)
                gradeIndexScalarDictionary.Add(
                    grade, 
                    GetKVectorIndexScalarDictionary(grade, minValue, maxValue)
                );

            return GaMultivectorGradedStorage<TScalar>.Create(
                ScalarProcessor,
                gradeIndexScalarDictionary
            );
        }

        public GaMultivectorGradedStorage<TScalar> GetSparseGradedMultivector(int termsCount)
        {
            var gaSpaceDimension = (int) GaSpaceDimension;

            if (termsCount > gaSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(termsCount));

            var composer = new GaMultivectorGradedStorageComposer<TScalar>(ScalarProcessor);

            var idList =
                Enumerable
                    .Range(0, gaSpaceDimension)
                    .Shuffled(RandomGenerator)
                    .Take(termsCount);

            foreach (var id in idList)
                composer.AddTerm((ulong) id, GetScalar());

            return composer.CreateMultivectorGradedStorage();
        }

        public GaMultivectorGradedStorage<TScalar> GetSparseGradedMultivector(int termsCount, double minValue, double maxValue)
        {
            var gaSpaceDimension = (int) GaSpaceDimension;

            if (termsCount > gaSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(termsCount));

            var composer = new GaMultivectorGradedStorageComposer<TScalar>(ScalarProcessor);

            var idList =
                Enumerable
                    .Range(0, gaSpaceDimension)
                    .Shuffled(RandomGenerator)
                    .Take(termsCount);

            foreach (var id in idList)
                composer.AddTerm((ulong) id, GetScalar(minValue, maxValue));

            return composer.CreateMultivectorGradedStorage();
        }

        public IEnumerable<GaVectorStorage<TScalar>> GetVectors(int count)
        {
            while (count > 0)
            {
                yield return GetVector();
                count--;
            }
        }

        public IGaKVectorStorage<TScalar> GetBlade(int grade)
        {
            if (grade == 0)
                return GetScalarTerm();

            if (grade == 1)
                return GetVector();

            if (grade == VSpaceDimension)
                return GetKVectorTermByGradeIndex(grade, 0);

            return ScalarProcessor.Op(GetVectors(grade));
        }

        public GaEuclideanSimpleRotor<TScalar> GetEuclideanSimpleRotor()
        {
            return GaEuclideanSimpleRotor<TScalar>.Create(
                GetVector(),
                GetVector()
            );
        }

        public IEnumerable<TScalar> GetScalars(int count)
        {
            return Enumerable
                .Range(0, count)
                .Select(_ => GetScalar());
        }

        public TScalar[,] GetArray(int rowsCount, int columnsCount)
        {
            var array = new TScalar[rowsCount, columnsCount];

            for (var i = 0; i < rowsCount; i++)
            for (var j = 0; j < columnsCount; j++)
                array[i, j] = GetScalar();

            return array;
        }

        public TScalar[,] GetPermutationArray(int size)
        {
            var array = new TScalar[size, size];

            var indexList = Enumerable
                .Range(0, size)
                .Shuffled(RandomGenerator);

            var i = 0;
            foreach (var colIndex in indexList)
            {
                for (var j = 0; j < size; j++)
                    array[i, j] = j == colIndex
                        ? ScalarProcessor.OneScalar 
                        : ScalarProcessor.ZeroScalar;

                i++;
            }

            return array;
        }
    }
}