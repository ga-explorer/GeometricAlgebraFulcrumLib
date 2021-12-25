using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Random;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    public class GeometricAlgebraRandomComposer<T> :
        LinearAlgebraRandomComposer<T>,
        IGeometricAlgebraSpace
    {
        private readonly ulong[] _kVectorSpaceDimensions;


        public IGeometricAlgebraProcessor<T> GeometricProcessor { get; }

        public IGeometricAlgebraEuclideanProcessor<T> GeometricEuclideanProcessor
            => (IGeometricAlgebraEuclideanProcessor<T>) GeometricProcessor;

        public uint VSpaceDimension 
            => GeometricProcessor.VSpaceDimension;

        public ulong GaSpaceDimension 
            => GeometricProcessor.GaSpaceDimension;

        public ulong MaxBasisBladeId 
            => GeometricProcessor.MaxBasisBladeId;

        public uint GradesCount 
            => GeometricProcessor.GradesCount;

        public IEnumerable<uint> Grades 
            => GeometricProcessor.Grades;


        internal GeometricAlgebraRandomComposer([NotNull] IGeometricAlgebraProcessor<T> geometricProcessor)
            : base(geometricProcessor)
        {
            GeometricProcessor = geometricProcessor;

            _kVectorSpaceDimensions = 
                GeometricProcessor
                    .GradesCount
                    .GetRange()
                    .Select(grade => GeometricProcessor.KVectorSpaceDimension(grade))
                    .ToArray();
        }

        internal GeometricAlgebraRandomComposer([NotNull] IGeometricAlgebraProcessor<T> geometricProcessor, int seed)
            : base(geometricProcessor, seed)
        {
            GeometricProcessor = geometricProcessor;
            
            _kVectorSpaceDimensions = 
                GeometricProcessor
                    .GradesCount
                    .GetRange()
                    .Select(grade => GeometricProcessor.KVectorSpaceDimension(grade))
                    .ToArray();
        }

        internal GeometricAlgebraRandomComposer([NotNull] IGeometricAlgebraProcessor<T> geometricProcessor, Random randomGenerator)
            : base(geometricProcessor, randomGenerator)
        {
            GeometricProcessor = geometricProcessor;
            
            _kVectorSpaceDimensions = 
                GeometricProcessor
                    .GradesCount
                    .GetRange()
                    .Select(grade => GeometricProcessor.KVectorSpaceDimension(grade))
                    .ToArray();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetGrade()
        {
            return (uint) RandomGenerator.Next((int) VSpaceDimension + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetBasisVectorId()
        {
            return GetBasisVectorIndex().BasisVectorIndexToId();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetBasisBivectorId()
        {
            return GetBasisBivectorIndex().BasisBivectorIndexToId();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetBasisBladeId()
        {
            return GetIndex(MaxBasisBladeId);
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
            return GetIndex(VSpaceDimension - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetBasisBivectorIndex()
        {
            return GetIndex(_kVectorSpaceDimensions[2]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetBasisBladeIndex(uint grade)
        {
            return GetIndex(_kVectorSpaceDimensions[grade]);
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
        public KVectorStorage<T> GetScalarTermStorage()
        {
            return ScalarProcessor.CreateKVectorScalarStorage(GetScalar());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetScalarTermStorage(double minValue, double maxValue)
        {
            return ScalarProcessor.CreateKVectorScalarStorage(
                GetScalar(minValue, maxValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> GetVectorTermStorage()
        {
            var index = GetBasisVectorIndex();
            var scalar = GetScalar();

            return ScalarProcessor.CreateVectorTermStorage(index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> GetVectorTermStorage(double minValue, double maxValue)
        {
            var index = GetBasisVectorIndex();
            var scalar = GetScalar(minValue, maxValue);

            return ScalarProcessor.CreateVectorTermStorage(index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> GetVectorTermStorageByIndex(ulong index)
        {
            return ScalarProcessor.CreateVectorTermStorage(
                index,
                GetScalar()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> GetVectorTermStorageByIndex(ulong index, double minValue, double maxValue)
        {
            return ScalarProcessor.CreateVectorTermStorage(
                index,
                GetScalar(minValue, maxValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> GetBivectorTermStorage()
        {
            var index = GetBasisBivectorIndex();
            var scalar = GetScalar();

            return ScalarProcessor.CreateBivectorTermStorage(index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> GetBivectorTermStorage(double minValue, double maxValue)
        {
            var index = GetBasisBivectorIndex();
            var scalar = GetScalar(minValue, maxValue);

            return ScalarProcessor.CreateBivectorTermStorage(index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> GetBivectorTermStorageByIndex(ulong index)
        {
            return ScalarProcessor.CreateBivectorTermStorage(index, GetScalar());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> GetBivectorTermStorageByIndex(ulong index, double minValue, double maxValue)
        {
            return ScalarProcessor.CreateBivectorTermStorage(index, GetScalar(minValue, maxValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetKVectorTermStorage()
        {
            var (grade, index) = GetBasisBladeGradeIndex();
            var scalar = GetScalar();

            return ScalarProcessor.CreateKVectorTermStorage(grade, index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetKVectorTermStorage(double minValue, double maxValue)
        {
            var (grade, index) = GetBasisBladeGradeIndex();
            var scalar = GetScalar(minValue, maxValue);

            return ScalarProcessor.CreateKVectorTermStorage(grade, index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetKVectorTermStorageOfGrade(uint grade)
        {
            var index = GetBasisBladeIndex(grade);
            var scalar = GetScalar();

            return ScalarProcessor.CreateKVectorTermStorage(grade, index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetKVectorTermStorageOfGrade(uint grade, double minValue, double maxValue)
        {
            var index = GetBasisBladeIndex(grade);
            var scalar = GetScalar(minValue, maxValue);

            return ScalarProcessor.CreateKVectorTermStorage(grade, index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetKVectorTermStorageById(ulong id)
        {
            return ScalarProcessor.CreateKVectorTermStorage(
                id,
                GetScalar()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetKVectorTermStorageById(ulong id, double minValue, double maxValue)
        {
            return ScalarProcessor.CreateKVectorTermStorage(
                id,
                GetScalar(minValue, maxValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetKVectorTermStorageByGradeIndex(uint grade, ulong index)
        {
            return ScalarProcessor.CreateKVectorTermStorage(
                grade,
                index,
                GetScalar()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetKVectorTermStorageByGradeIndex(uint grade, ulong index, double minValue, double maxValue)
        {
            var scalar = GetScalar(minValue, maxValue);

            return ScalarProcessor.CreateKVectorTermStorage(grade, index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<T> GetVector()
        {
            return new Vector<T>(GeometricProcessor, GetVectorStorage());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<T> GetVector(int termsCount, bool makeUnitVector = false)
        {
            return new Vector<T>(GeometricProcessor, GetVectorStorage(termsCount, makeUnitVector));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<T> GetVector(double minValue, double maxValue)
        {
            return new Vector<T>(GeometricProcessor, GetVectorStorage(minValue, maxValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> GetVectorStorage()
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
        public VectorStorage<T> GetVectorStorage(int termsCount, bool makeUnitVector = false)
        {
            if (termsCount > VSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(termsCount));

            var indexScalarDictionary =
                termsCount
                    .GetRange()
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar()
                    );

            var vectorStorage = 
                ScalarProcessor.CreateVectorStorage(indexScalarDictionary);

            return makeUnitVector 
                ? GeometricProcessor.DivideByNorm(vectorStorage) 
                : vectorStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> GetVectorStorage(double minValue, double maxValue)
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
        public VectorStorage<T> GetSparseVectorStorage(int termsCount)
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
        public VectorStorage<T> GetSparseVectorStorage(int termsCount, double minValue, double maxValue)
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
        public BivectorStorage<T> GetBivectorStorage()
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
        public Bivector<T> GetBivector()
        {
            return new Bivector<T>(GeometricProcessor, GetBivectorStorage());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> GetBivectorStorage(double minValue, double maxValue)
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
        public Bivector<T> GetBivector(double minValue, double maxValue)
        {
            return new Bivector<T>(GeometricProcessor, GetBivectorStorage(minValue, maxValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> GetSparseBivectorStorage(int termsCount)
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
        public BivectorStorage<T> GetSparseBivectorStorage(int termsCount, double minValue, double maxValue)
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
        public KVectorStorage<T> GetKVectorStorage()
        {
            return GetKVectorStorageOfGrade(GetGrade());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetKVectorStorage(double minValue, double maxValue)
        {
            return GetKVectorStorageOfGrade(GetGrade(), minValue, maxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetKVectorStorageOfGrade(uint grade)
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
        public KVectorStorage<T> GetKVectorStorageOfGrade(uint grade, double minValue, double maxValue)
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
        public KVectorStorage<T> GetSparseKVectorStorageOfGrade(uint grade, int termsCount)
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
        public KVectorStorage<T> GetSparseKVectorStorageOfGrade(uint grade, int termsCount, double minValue, double maxValue)
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
        public MultivectorStorage<T> GetTermsMultivectorStorage()
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
        public MultivectorStorage<T> GetTermsMultivectorStorage(double minValue, double maxValue)
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
        public MultivectorStorage<T> GetTermsMultivectorStorage(int termsCount)
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
        public MultivectorStorage<T> GetTermsMultivectorStorage(int termsCount, double minValue, double maxValue)
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
        public IMultivectorGradedStorage<T> GetGradedMultivectorStorage()
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
        public IMultivectorGradedStorage<T> GetGradedMultivectorStorage(double minValue, double maxValue)
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
        public IMultivectorGradedStorage<T> GetGradedMultivectorStorage(int termsCount)
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
        public IMultivectorGradedStorage<T> GetGradedMultivectorStorage(int termsCount, double minValue, double maxValue)
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
        public IEnumerable<VectorStorage<T>> GetVectorStorages(int count)
        {
            while (count > 0)
            {
                yield return GetVectorStorage();
                count--;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetBladeStorage(uint grade)
        {
            if (grade == 0U)
                return GetScalarTermStorage();

            if (grade == 1U)
                return GetVectorStorage();

            if (grade == VSpaceDimension)
                return GetKVectorTermStorageByGradeIndex(grade, 0);

            return ScalarProcessor.Op(GetVectorStorages((int) grade));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVector<T> GetBlade(uint grade)
        {
            return new KVector<T>(GeometricProcessor, GetBladeStorage(grade));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PureRotor<T> GetEuclideanPureRotor()
        {
            return GeometricEuclideanProcessor.CreatePureRotor(
                GetVectorStorage(),
                GetVectorStorage()
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