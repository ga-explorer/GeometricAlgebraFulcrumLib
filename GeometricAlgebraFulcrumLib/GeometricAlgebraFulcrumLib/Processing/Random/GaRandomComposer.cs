using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Random;
using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Geometry.Rotors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;
using GeometricAlgebraFulcrumLib.Storage.Factories;

namespace GeometricAlgebraFulcrumLib.Processing.Random
{
    public class GaRandomComposer<T> :
        RandomComposer, IGaSpace
    {
        //public IGaMultivectorProcessor<T> MultivectorProcessor { get; }

        public IGaScalarProcessor<T> ScalarProcessor { get; }
            //=> MultivectorProcessor.ScalarProcessor;

        public uint VSpaceDimension { get; }

        public ulong GaSpaceDimension 
            => 1UL << (int) VSpaceDimension;

        public ulong MaxBasisBladeId 
            => (1UL << (int) VSpaceDimension) - 1UL;

        public uint GradesCount 
            => VSpaceDimension + 1;

        public IEnumerable<uint> Grades 
            => GradesCount.GetRange();


        public GaRandomComposer([NotNull] IGaScalarProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            if (vSpaceDimension < 2 || vSpaceDimension > GaSpaceUtils.MaxVSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimension));

            ScalarProcessor = scalarProcessor;
            VSpaceDimension = vSpaceDimension;
        }

        public GaRandomComposer([NotNull] IGaScalarProcessor<T> scalarProcessor, uint vSpaceDimension, int seed)
            : base(seed)
        {
            if (vSpaceDimension < 2 || vSpaceDimension > GaSpaceUtils.MaxVSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimension));

            ScalarProcessor = scalarProcessor;
            VSpaceDimension = vSpaceDimension;
        }

        public GaRandomComposer([NotNull] IGaScalarProcessor<T> scalarProcessor, uint vSpaceDimension, System.Random randomGenerator)
            : base(randomGenerator)
        {
            if (vSpaceDimension < 2 || vSpaceDimension > GaSpaceUtils.MaxVSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimension));
            
            ScalarProcessor = scalarProcessor;
            VSpaceDimension = vSpaceDimension;
        }


        public T GetScalar()
        {
            return ScalarProcessor.GetRandomScalar(
                RandomGenerator, 
                -1d, 
                1d
            );
        }

        public T GetScalar(double minValue, double maxValue)
        {
            return ScalarProcessor.GetRandomScalar(
                RandomGenerator, 
                minValue, 
                maxValue
            );
        }

        public uint GetGrade()
        {
            return (uint) RandomGenerator.Next((int) VSpaceDimension + 1);
        }

        public ulong GetBasisVectorId()
        {
            return 1UL << RandomGenerator.Next((int) VSpaceDimension);
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

        public ulong GetBasisBladeId(uint grade)
        {
            return GaBasisUtils.BasisBladeId(
                grade, 
                GetBasisBladeIndex(grade)
            );
        }

        public Tuple<uint, ulong> GetBasisBladeGradeIndex()
        {
            return GetBasisBladeId().BasisBladeGradeIndex();
        }

        public ulong GetBasisVectorIndex()
        {
            return (ulong) RandomGenerator.Next((int) VSpaceDimension);
        }

        public ulong GetBasisBivectorIndex()
        {
            return GetBasisBladeIndex(2);
        }

        public ulong GetBasisBladeIndex(uint grade)
        {
            var kvSpaceDimension = GaBasisUtils.KvSpaceDimension(VSpaceDimension, grade);

            return (ulong) RandomGenerator.Next((int) kvSpaceDimension);
        }

        public Dictionary<ulong, T> GetKVectorIndexScalarDictionary(uint grade)
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

        public Dictionary<ulong, T> GetKVectorIndexScalarDictionary(uint grade, double minValue, double maxValue)
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

        public GaStorageScalar<T> GetScalarTerm()
        {
            return ScalarProcessor.CreateStorageScalar(GetScalar());
        }

        public GaStorageScalar<T> GetScalarTerm(double minValue, double maxValue)
        {
            return ScalarProcessor.CreateStorageScalar(
                GetScalar(minValue, maxValue)
            );
        }

        public GaStorageVector<T> GetVectorTerm()
        {
            var index = GetBasisVectorIndex();
            var scalar = GetScalar();

            return ScalarProcessor.CreateStorageVector(index, scalar);
        }

        public GaStorageVector<T> GetVectorTerm(double minValue, double maxValue)
        {
            var index = GetBasisVectorIndex();
            var scalar = GetScalar(minValue, maxValue);

            return ScalarProcessor.CreateStorageVector(index, scalar);
        }

        public GaStorageVector<T> GetVectorTermByIndex(ulong index)
        {
            return ScalarProcessor.CreateStorageVector(
                index,
                GetScalar()
            );
        }

        public GaStorageVector<T> GetVectorTermByIndex(ulong index, double minValue, double maxValue)
        {
            return ScalarProcessor.CreateStorageVector(index,
                GetScalar(minValue, maxValue)
            );
        }

        public GaStorageBivector<T> GetBivectorTerm()
        {
            var index = GetBasisBivectorIndex();
            var scalar = GetScalar();

            return ScalarProcessor.CreateStorageBivector(
                index,
                scalar
            );
        }

        public GaStorageBivector<T> GetBivectorTerm(double minValue, double maxValue)
        {
            var index = GetBasisBivectorIndex();
            var scalar = GetScalar(minValue, maxValue);

            return ScalarProcessor.CreateStorageBivector(
                index,
                scalar
            );
        }

        public GaStorageBivector<T> GetBivectorTermByIndex(ulong index)
        {
            return ScalarProcessor.CreateStorageBivector(
                index,
                GetScalar()
            );
        }

        public GaStorageBivector<T> GetBivectorTermByIndex(ulong index, double minValue, double maxValue)
        {
            return ScalarProcessor.CreateStorageBivector(index,
                GetScalar(minValue, maxValue)
            );
        }

        public IGaStorageKVector<T> GetKVectorTerm()
        {
            var (grade, index) = GetBasisBladeGradeIndex();
            var scalar = GetScalar();

            return ScalarProcessor.CreateStorageKVector(
                grade,
                index,
                scalar
            );
        }

        public IGaStorageKVector<T> GetKVectorTerm(double minValue, double maxValue)
        {
            var (grade, index) = GetBasisBladeGradeIndex();
            var scalar = GetScalar(minValue, maxValue);

            return ScalarProcessor.CreateStorageKVector(
                grade,
                index,
                scalar
            );
        }

        public IGaStorageKVector<T> GetKVectorTermOfGrade(uint grade)
        {
            var index = GetBasisBladeIndex(grade);
            var scalar = GetScalar();

            return ScalarProcessor.CreateStorageKVector(
                grade,
                index,
                scalar
            );
        }

        public IGaStorageKVector<T> GetKVectorTermOfGrade(uint grade, double minValue, double maxValue)
        {
            var index = GetBasisBladeIndex(grade);
            var scalar = GetScalar(minValue, maxValue);

            return ScalarProcessor.CreateStorageKVector(
                grade,
                index,
                scalar
            );
        }

        public IGaStorageKVector<T> GetKVectorTermById(ulong id)
        {
            return ScalarProcessor.CreateStorageKVector(
                id,
                GetScalar()
            );
        }

        public IGaStorageKVector<T> GetKVectorTermById(ulong id, double minValue, double maxValue)
        {
            return ScalarProcessor.CreateStorageKVector(
                id,
                GetScalar(minValue, maxValue)
            );
        }

        public IGaStorageKVector<T> GetKVectorTermByGradeIndex(uint grade, ulong index)
        {
            return ScalarProcessor.CreateStorageKVector(
                grade,
                index,
                GetScalar()
            );
        }

        public IGaStorageKVector<T> GetKVectorTermByGradeIndex(uint grade, ulong index, double minValue, double maxValue)
        {
            var scalar = GetScalar(minValue, maxValue);

            return ScalarProcessor.CreateStorageKVector(
                grade,
                index,
                scalar
            );
        }

        public IGaStorageVector<T> GetVector()
        {
            var indexScalarDictionary =
                VSpaceDimension
                    .GetRange()
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar()
                    );

            return ScalarProcessor.CreateStorageVector(indexScalarDictionary);
        }

        public IGaStorageVector<T> GetVector(double minValue, double maxValue)
        {
            var indexScalarDictionary =
                VSpaceDimension
                    .GetRange()
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar(minValue, maxValue)
                    );

            return ScalarProcessor.CreateStorageVector(indexScalarDictionary);
        }

        public IGaStorageVector<T> GetSparseVector(int termsCount)
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
            
            return ScalarProcessor.CreateStorageVector(indexScalarDictionary);
        }

        public IGaStorageVector<T> GetSparseVector(int termsCount, double minValue, double maxValue)
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
            
            return ScalarProcessor.CreateStorageVector(indexScalarDictionary);
        }

        public IGaStorageBivector<T> GetBivector()
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

            return ScalarProcessor.CreateStorageBivector(indexScalarDictionary);
        }

        public IGaStorageBivector<T> GetBivector(double minValue, double maxValue)
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

            return ScalarProcessor.CreateStorageBivector(indexScalarDictionary);
        }

        public IGaStorageBivector<T> GetSparseBivector(int termsCount)
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

            return ScalarProcessor.CreateStorageBivector(indexScalarDictionary);
        }

        public IGaStorageBivector<T> GetSparseBivector(int termsCount, double minValue, double maxValue)
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

            return ScalarProcessor.CreateStorageBivector(indexScalarDictionary);
        }

        public IGaStorageKVector<T> GetKVector()
        {
            return GetKVectorOfGrade(GetGrade());
        }

        public IGaStorageKVector<T> GetKVector(double minValue, double maxValue)
        {
            return GetKVectorOfGrade(
                GetGrade(),
                minValue,
                maxValue
            );
        }

        public IGaStorageKVector<T> GetKVectorOfGrade(uint grade)
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

            return ScalarProcessor.CreateStorageKVector(grade, indexScalarDictionary);
        }

        public IGaStorageKVector<T> GetKVectorOfGrade(uint grade, double minValue, double maxValue)
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

            return ScalarProcessor.CreateStorageKVector(grade, indexScalarDictionary);
        }

        public IGaStorageKVector<T> GetSparseKVectorOfGrade(uint grade, int termsCount)
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

            return ScalarProcessor.CreateStorageKVector(grade, indexScalarDictionary);
        }

        public IGaStorageKVector<T> GetSparseKVectorOfGrade(uint grade, int termsCount, double minValue, double maxValue)
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

            return ScalarProcessor.CreateStorageKVector(grade, indexScalarDictionary);
        }

        public IGaStorageMultivectorSparse<T> GetTermsMultivector()
        {
            var gaSpaceDimension = (int) GaSpaceDimension;

            var idScalarDictionary =
                Enumerable
                    .Range(0, gaSpaceDimension)
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar()
                    );

            return GaStorageMultivectorSparse<T>.Create(idScalarDictionary);
        }
        
        public IGaStorageMultivectorSparse<T> GetTermsMultivector(double minValue, double maxValue)
        {
            var gaSpaceDimension = (int) GaSpaceDimension;

            var idScalarDictionary =
                Enumerable
                    .Range(0, gaSpaceDimension)
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar(minValue, maxValue)
                    );

            return GaStorageMultivectorSparse<T>.Create(idScalarDictionary);
        }
        
        public IGaStorageMultivectorSparse<T> GetTermsMultivector(int termsCount)
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

            return GaStorageMultivectorSparse<T>.Create(idScalarDictionary);
        }
        
        public IGaStorageMultivectorSparse<T> GetTermsMultivector(int termsCount, double minValue, double maxValue)
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

            return GaStorageMultivectorSparse<T>.Create(idScalarDictionary);
        }

        public IGaStorageMultivectorGraded<T> GetGradedMultivector()
        {
            var gradeIndexScalarDictionary = 
                new Dictionary<uint, Dictionary<ulong, T>>();

            for (var grade = 0U; grade <= VSpaceDimension; grade++)
                gradeIndexScalarDictionary.Add(
                    grade, 
                    GetKVectorIndexScalarDictionary(grade)
                );

            return ScalarProcessor.CreateStorageGradedMultivector(gradeIndexScalarDictionary);
        }

        public IGaStorageMultivectorGraded<T> GetGradedMultivector(double minValue, double maxValue)
        {
            var gradeIndexScalarDictionary = 
                new Dictionary<uint, Dictionary<ulong, T>>();

            for (var grade = 0U; grade <= VSpaceDimension; grade++)
                gradeIndexScalarDictionary.Add(
                    grade, 
                    GetKVectorIndexScalarDictionary(grade, minValue, maxValue)
                );

            return ScalarProcessor.CreateStorageGradedMultivector(gradeIndexScalarDictionary);
        }

        public IGaStorageMultivectorGraded<T> GetGradedMultivector(int termsCount)
        {
            var gaSpaceDimension = (int) GaSpaceDimension;

            if (termsCount > gaSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(termsCount));

            var composer = new GaStorageComposerMultivectorGraded<T>(ScalarProcessor);

            var idList =
                Enumerable
                    .Range(0, gaSpaceDimension)
                    .Shuffled(RandomGenerator)
                    .Take(termsCount);

            foreach (var id in idList)
                composer.AddTerm((ulong) id, GetScalar());

            return composer.GetGradedMultivector();
        }

        public IGaStorageMultivectorGraded<T> GetGradedMultivector(int termsCount, double minValue, double maxValue)
        {
            var gaSpaceDimension = (int) GaSpaceDimension;

            if (termsCount > gaSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(termsCount));

            var composer = new GaStorageComposerMultivectorGraded<T>(ScalarProcessor);

            var idList =
                Enumerable
                    .Range(0, gaSpaceDimension)
                    .Shuffled(RandomGenerator)
                    .Take(termsCount);

            foreach (var id in idList)
                composer.AddTerm((ulong) id, GetScalar(minValue, maxValue));

            return composer.GetGradedMultivector();
        }

        public IEnumerable<IGaStorageVector<T>> GetVectors(int count)
        {
            while (count > 0)
            {
                yield return GetVector();
                count--;
            }
        }

        public IGaStorageKVector<T> GetBlade(uint grade)
        {
            if (grade == 0U)
                return GetScalarTerm();

            if (grade == 1U)
                return GetVector();

            if (grade == VSpaceDimension)
                return GetKVectorTermByGradeIndex(grade, 0);

            return ScalarProcessor.Op(GetVectors((int) grade));
        }

        public GaPureRotor<T> GetEuclideanSimpleRotor(IGaProcessor<T> processor)
        {
            return processor.CreateEuclideanRotor(
                GetVector(),
                GetVector()
            );
        }

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
                        ? ScalarProcessor.OneScalar 
                        : ScalarProcessor.ZeroScalar;

                i++;
            }

            return array;
        }
    }
}