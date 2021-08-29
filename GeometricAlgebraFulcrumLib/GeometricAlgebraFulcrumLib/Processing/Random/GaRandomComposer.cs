using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Random;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Space;
using GeometricAlgebraFulcrumLib.Geometry.Rotors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Processing.Random
{
    public class GaRandomComposer<T> :
        RandomComposer, IGaSpace
    {
        //public IGaMultivectorProcessor<T> MultivectorProcessor { get; }

        public IScalarProcessor<T> ScalarProcessor { get; }
            //=> MultivectorProcessor.ScalarProcessor;

        public uint VSpaceDimension { get; }

        public ulong GaSpaceDimension 
            => VSpaceDimension.ToGaSpaceDimension();

        public ulong MaxBasisBladeId 
            => (VSpaceDimension.ToGaSpaceDimension()) - 1UL;

        public uint GradesCount 
            => VSpaceDimension + 1;

        public IEnumerable<uint> Grades 
            => GradesCount.GetRange();


        public GaRandomComposer([NotNull] IScalarProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            if (vSpaceDimension < 2 || vSpaceDimension > GaSpaceUtils.MaxVSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimension));

            ScalarProcessor = scalarProcessor;
            VSpaceDimension = vSpaceDimension;
        }

        public GaRandomComposer([NotNull] IScalarProcessor<T> scalarProcessor, uint vSpaceDimension, int seed)
            : base(seed)
        {
            if (vSpaceDimension < 2 || vSpaceDimension > GaSpaceUtils.MaxVSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimension));

            ScalarProcessor = scalarProcessor;
            VSpaceDimension = vSpaceDimension;
        }

        public GaRandomComposer([NotNull] IScalarProcessor<T> scalarProcessor, uint vSpaceDimension, System.Random randomGenerator)
            : base(randomGenerator)
        {
            if (vSpaceDimension < 2 || vSpaceDimension > GaSpaceUtils.MaxVSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimension));
            
            ScalarProcessor = scalarProcessor;
            VSpaceDimension = vSpaceDimension;
        }


        public T GetScalar()
        {
            return ScalarProcessor.GetScalarFromRandom(
                RandomGenerator, 
                -1d, 
                1d
            );
        }

        public T GetScalar(double minValue, double maxValue)
        {
            return ScalarProcessor.GetScalarFromRandom(
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
            return RandomGenerator.Next((int) VSpaceDimension).BasisVectorIndexToId();
        }

        public ulong GetBasisBivectorId()
        {
            return GetBasisBivectorIndex().BasisBivectorIndexToId();
        }

        public ulong GetBasisBladeId()
        {
            return (ulong) RandomGenerator.Next((int) GaSpaceDimension);
        }

        public ulong GetBasisBladeId(uint grade)
        {
            return GetBasisBladeIndex(grade).BasisBladeIndexToId(grade);
        }

        public GradeIndexRecord GetBasisBladeGradeIndex()
        {
            return GetBasisBladeId().BasisBladeIdToGradeIndex();
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
            var kvSpaceDimension = VSpaceDimension.KVectorSpaceDimension(grade);

            return (ulong) RandomGenerator.Next((int) kvSpaceDimension);
        }

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

        public GaScalarStorage<T> GetScalarTerm()
        {
            return ScalarProcessor.CreateStorageScalar(GetScalar());
        }

        public GaScalarStorage<T> GetScalarTerm(double minValue, double maxValue)
        {
            return ScalarProcessor.CreateStorageScalar(
                GetScalar(minValue, maxValue)
            );
        }

        public GaVectorStorage<T> GetVectorTerm()
        {
            var index = GetBasisVectorIndex();
            var scalar = GetScalar();

            return ScalarProcessor.CreateGaVectorStorage(index, scalar);
        }

        public GaVectorStorage<T> GetVectorTerm(double minValue, double maxValue)
        {
            var index = GetBasisVectorIndex();
            var scalar = GetScalar(minValue, maxValue);

            return ScalarProcessor.CreateGaVectorStorage(index, scalar);
        }

        public GaVectorStorage<T> GetVectorTermByIndex(ulong index)
        {
            return ScalarProcessor.CreateGaVectorStorage(
                index,
                GetScalar()
            );
        }

        public GaVectorStorage<T> GetVectorTermByIndex(ulong index, double minValue, double maxValue)
        {
            return ScalarProcessor.CreateGaVectorStorage(index,
                GetScalar(minValue, maxValue)
            );
        }

        public GaBivectorStorage<T> GetBivectorTerm()
        {
            var index = GetBasisBivectorIndex();
            var scalar = GetScalar();

            return ScalarProcessor.CreateBivectorStorage(
                index,
                scalar
            );
        }

        public GaBivectorStorage<T> GetBivectorTerm(double minValue, double maxValue)
        {
            var index = GetBasisBivectorIndex();
            var scalar = GetScalar(minValue, maxValue);

            return ScalarProcessor.CreateBivectorStorage(
                index,
                scalar
            );
        }

        public GaBivectorStorage<T> GetBivectorTermByIndex(ulong index)
        {
            return ScalarProcessor.CreateBivectorStorage(
                index,
                GetScalar()
            );
        }

        public GaBivectorStorage<T> GetBivectorTermByIndex(ulong index, double minValue, double maxValue)
        {
            return ScalarProcessor.CreateBivectorStorage(index,
                GetScalar(minValue, maxValue)
            );
        }

        public IGaKVectorStorage<T> GetKVectorTerm()
        {
            var (grade, index) = GetBasisBladeGradeIndex();
            var scalar = GetScalar();

            return ScalarProcessor.CreateKVectorStorage(
                grade,
                index,
                scalar
            );
        }

        public IGaKVectorStorage<T> GetKVectorTerm(double minValue, double maxValue)
        {
            var (grade, index) = GetBasisBladeGradeIndex();
            var scalar = GetScalar(minValue, maxValue);

            return ScalarProcessor.CreateKVectorStorage(
                grade,
                index,
                scalar
            );
        }

        public IGaKVectorStorage<T> GetKVectorTermOfGrade(uint grade)
        {
            var index = GetBasisBladeIndex(grade);
            var scalar = GetScalar();

            return ScalarProcessor.CreateKVectorStorage(
                grade,
                index,
                scalar
            );
        }

        public IGaKVectorStorage<T> GetKVectorTermOfGrade(uint grade, double minValue, double maxValue)
        {
            var index = GetBasisBladeIndex(grade);
            var scalar = GetScalar(minValue, maxValue);

            return ScalarProcessor.CreateKVectorStorage(
                grade,
                index,
                scalar
            );
        }

        public IGaKVectorStorage<T> GetKVectorTermById(ulong id)
        {
            return ScalarProcessor.CreateKVectorStorage(
                id,
                GetScalar()
            );
        }

        public IGaKVectorStorage<T> GetKVectorTermById(ulong id, double minValue, double maxValue)
        {
            return ScalarProcessor.CreateKVectorStorage(
                id,
                GetScalar(minValue, maxValue)
            );
        }

        public IGaKVectorStorage<T> GetKVectorTermByGradeIndex(uint grade, ulong index)
        {
            return ScalarProcessor.CreateKVectorStorage(
                grade,
                index,
                GetScalar()
            );
        }

        public IGaKVectorStorage<T> GetKVectorTermByGradeIndex(uint grade, ulong index, double minValue, double maxValue)
        {
            var scalar = GetScalar(minValue, maxValue);

            return ScalarProcessor.CreateKVectorStorage(
                grade,
                index,
                scalar
            );
        }

        public IGaVectorStorage<T> GetVector()
        {
            var indexScalarDictionary =
                VSpaceDimension
                    .GetRange()
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar()
                    );

            return ScalarProcessor.CreateGaVectorStorage(indexScalarDictionary);
        }

        public IGaVectorStorage<T> GetVector(double minValue, double maxValue)
        {
            var indexScalarDictionary =
                VSpaceDimension
                    .GetRange()
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar(minValue, maxValue)
                    );

            return ScalarProcessor.CreateGaVectorStorage(indexScalarDictionary);
        }

        public IGaVectorStorage<T> GetSparseVector(int termsCount)
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
            
            return ScalarProcessor.CreateGaVectorStorage(indexScalarDictionary);
        }

        public IGaVectorStorage<T> GetSparseVector(int termsCount, double minValue, double maxValue)
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
            
            return ScalarProcessor.CreateGaVectorStorage(indexScalarDictionary);
        }

        public IGaBivectorStorage<T> GetBivector()
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

        public IGaBivectorStorage<T> GetBivector(double minValue, double maxValue)
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

        public IGaBivectorStorage<T> GetSparseBivector(int termsCount)
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

        public IGaBivectorStorage<T> GetSparseBivector(int termsCount, double minValue, double maxValue)
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

        public IGaKVectorStorage<T> GetKVector()
        {
            return GetKVectorOfGrade(GetGrade());
        }

        public IGaKVectorStorage<T> GetKVector(double minValue, double maxValue)
        {
            return GetKVectorOfGrade(
                GetGrade(),
                minValue,
                maxValue
            );
        }

        public IGaKVectorStorage<T> GetKVectorOfGrade(uint grade)
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

        public IGaKVectorStorage<T> GetKVectorOfGrade(uint grade, double minValue, double maxValue)
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

        public IGaKVectorStorage<T> GetSparseKVectorOfGrade(uint grade, int termsCount)
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

        public IGaKVectorStorage<T> GetSparseKVectorOfGrade(uint grade, int termsCount, double minValue, double maxValue)
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

        public IGaMultivectorSparseStorage<T> GetTermsMultivector()
        {
            var gaSpaceDimension = (int) GaSpaceDimension;

            var idScalarDictionary =
                Enumerable
                    .Range(0, gaSpaceDimension)
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar()
                    );

            return GaMultivectorSparseStorage<T>.Create(idScalarDictionary);
        }
        
        public IGaMultivectorSparseStorage<T> GetTermsMultivector(double minValue, double maxValue)
        {
            var gaSpaceDimension = (int) GaSpaceDimension;

            var idScalarDictionary =
                Enumerable
                    .Range(0, gaSpaceDimension)
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar(minValue, maxValue)
                    );

            return GaMultivectorSparseStorage<T>.Create(idScalarDictionary);
        }
        
        public IGaMultivectorSparseStorage<T> GetTermsMultivector(int termsCount)
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

            return GaMultivectorSparseStorage<T>.Create(idScalarDictionary);
        }
        
        public IGaMultivectorSparseStorage<T> GetTermsMultivector(int termsCount, double minValue, double maxValue)
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

            return GaMultivectorSparseStorage<T>.Create(idScalarDictionary);
        }

        public IGaMultivectorGradedStorage<T> GetGradedMultivector()
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

        public IGaMultivectorGradedStorage<T> GetGradedMultivector(double minValue, double maxValue)
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

        public IGaMultivectorGradedStorage<T> GetGradedMultivector(int termsCount)
        {
            var gaSpaceDimension = (int) GaSpaceDimension;

            if (termsCount > gaSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(termsCount));

            var composer = ScalarProcessor.CreateStorageGradedMultivectorComposer();

            var idList =
                Enumerable
                    .Range(0, gaSpaceDimension)
                    .Shuffled(RandomGenerator)
                    .Take(termsCount);

            foreach (var id in idList)
                composer.AddTerm((ulong) id, GetScalar());

            return composer.CreateGaMultivectorGradedStorage();
        }

        public IGaMultivectorGradedStorage<T> GetGradedMultivector(int termsCount, double minValue, double maxValue)
        {
            var gaSpaceDimension = (int) GaSpaceDimension;

            if (termsCount > gaSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(termsCount));

            var composer = ScalarProcessor.CreateStorageGradedMultivectorComposer();

            var idList =
                Enumerable
                    .Range(0, gaSpaceDimension)
                    .Shuffled(RandomGenerator)
                    .Take(termsCount);

            foreach (var id in idList)
                composer.AddTerm((ulong) id, GetScalar(minValue, maxValue));

            return composer.CreateGaMultivectorGradedStorage();
        }

        public IEnumerable<IGaVectorStorage<T>> GetVectors(int count)
        {
            while (count > 0)
            {
                yield return GetVector();
                count--;
            }
        }

        public IGaKVectorStorage<T> GetBlade(uint grade)
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
                        ? ScalarProcessor.ScalarOne 
                        : ScalarProcessor.ScalarZero;

                i++;
            }

            return array;
        }
    }
}