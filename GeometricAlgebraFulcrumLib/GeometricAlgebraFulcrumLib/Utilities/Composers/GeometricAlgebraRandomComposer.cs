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
        public KVector<T> GetScalarTerm()
        {
            return GeometricProcessor.CreateKVectorScalar(GetScalar());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVector<T> GetScalarTerm(double minValue, double maxValue)
        {
            return GeometricProcessor.CreateKVectorScalar(
                GetScalar(minValue, maxValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<T> GetVectorTerm()
        {
            var index = GetBasisVectorIndex();
            var scalar = GetScalar();

            return GeometricProcessor.CreateVectorTerm(index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<T> GetVectorTerm(double minValue, double maxValue)
        {
            var index = GetBasisVectorIndex();
            var scalar = GetScalar(minValue, maxValue);

            return GeometricProcessor.CreateVectorTerm(index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<T> GetVectorTermByIndex(ulong index)
        {
            return GeometricProcessor.CreateVectorTerm(
                index,
                GetScalar()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<T> GetVectorTermByIndex(ulong index, double minValue, double maxValue)
        {
            return GeometricProcessor.CreateVectorTerm(
                index,
                GetScalar(minValue, maxValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Bivector<T> GetBivectorTerm()
        {
            var index = GetBasisBivectorIndex();
            var scalar = GetScalar();

            return GeometricProcessor.CreateBivectorTerm(index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Bivector<T> GetBivectorTerm(double minValue, double maxValue)
        {
            var index = GetBasisBivectorIndex();
            var scalar = GetScalar(minValue, maxValue);

            return GeometricProcessor.CreateBivectorTerm(index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Bivector<T> GetBivectorTermByIndex(ulong index)
        {
            return GeometricProcessor.CreateBivectorTerm(index, GetScalar());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Bivector<T> GetBivectorTermByIndex(ulong index, double minValue, double maxValue)
        {
            return GeometricProcessor.CreateBivectorTerm(index, GetScalar(minValue, maxValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVector<T> GetKVectorTerm()
        {
            var (grade, index) = GetBasisBladeGradeIndex();
            var scalar = GetScalar();

            return GeometricProcessor.CreateKVectorTerm(grade, index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVector<T> GetKVectorTerm(double minValue, double maxValue)
        {
            var (grade, index) = GetBasisBladeGradeIndex();
            var scalar = GetScalar(minValue, maxValue);

            return GeometricProcessor.CreateKVectorTerm(grade, index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVector<T> GetKVectorTermOfGrade(uint grade)
        {
            var index = GetBasisBladeIndex(grade);
            var scalar = GetScalar();

            return GeometricProcessor.CreateKVectorTerm(grade, index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVector<T> GetKVectorTermOfGrade(uint grade, double minValue, double maxValue)
        {
            var index = GetBasisBladeIndex(grade);
            var scalar = GetScalar(minValue, maxValue);

            return GeometricProcessor.CreateKVectorTerm(grade, index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVector<T> GetKVectorTermById(ulong id)
        {
            return GeometricProcessor.CreateKVectorTerm(
                id,
                GetScalar()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVector<T> GetKVectorTermById(ulong id, double minValue, double maxValue)
        {
            return GeometricProcessor.CreateKVectorTerm(
                id,
                GetScalar(minValue, maxValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVector<T> GetKVectorTermByGradeIndex(uint grade, ulong index)
        {
            return GeometricProcessor.CreateKVectorTerm(
                grade,
                index,
                GetScalar()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVector<T> GetKVectorTermByGradeIndex(uint grade, ulong index, double minValue, double maxValue)
        {
            var scalar = GetScalar(minValue, maxValue);

            return GeometricProcessor.CreateKVectorTerm(grade, index, scalar);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<T> GetVector()
        {
            var indexScalarDictionary =
                VSpaceDimension
                    .GetRange()
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar()
                    );

            return GeometricProcessor.CreateVector(indexScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<T> GetVector(int termsCount, bool makeUnitVector = false)
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

            var vector = 
                GeometricProcessor.CreateVector(indexScalarDictionary);

            return makeUnitVector 
                ? vector.DivideByNorm() 
                : vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<T> GetVector(double minValue, double maxValue)
        {
            var indexScalarDictionary =
                VSpaceDimension
                    .GetRange()
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar(minValue, maxValue)
                    );

            return GeometricProcessor.CreateVector(indexScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<T> GetSparseVector(int termsCount)
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
            
            return GeometricProcessor.CreateVector(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<T> GetSparseVector(int termsCount, double minValue, double maxValue)
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
            
            return GeometricProcessor.CreateVector(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Bivector<T> GetBivector()
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

            return GeometricProcessor.CreateBivector(indexScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Bivector<T> GetBivector(double minValue, double maxValue)
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

            return GeometricProcessor.CreateBivector(indexScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Bivector<T> GetSparseBivector(int termsCount)
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

            return GeometricProcessor.CreateBivector(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Bivector<T> GetSparseBivector(int termsCount, double minValue, double maxValue)
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

            return GeometricProcessor.CreateBivector(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVector<T> GetKVector()
        {
            return GetKVectorOfGrade(GetGrade());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVector<T> GetKVector(double minValue, double maxValue)
        {
            return GetKVectorOfGrade(GetGrade(), minValue, maxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVector<T> GetKVectorOfGrade(uint grade)
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

            return GeometricProcessor.CreateKVector(grade, indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVector<T> GetKVectorOfGrade(uint grade, double minValue, double maxValue)
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

            return GeometricProcessor.CreateKVector(grade, indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVector<T> GetSparseKVectorOfGrade(uint grade, int termsCount)
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

            return GeometricProcessor.CreateKVector(grade, indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVector<T> GetSparseKVectorOfGrade(uint grade, int termsCount, double minValue, double maxValue)
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

            return GeometricProcessor.CreateKVector(grade, indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Multivector<T> GetTermsMultivector()
        {
            var gaSpaceDimension = (int) GaSpaceDimension;

            var idScalarDictionary =
                Enumerable
                    .Range(0, gaSpaceDimension)
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar()
                    );

            return GeometricProcessor.CreateMultivectorSparse(idScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Multivector<T> GetTermsMultivector(double minValue, double maxValue)
        {
            var gaSpaceDimension = (int) GaSpaceDimension;

            var idScalarDictionary =
                Enumerable
                    .Range(0, gaSpaceDimension)
                    .ToDictionary(
                        index => (ulong) index,
                        _ => GetScalar(minValue, maxValue)
                    );

            return GeometricProcessor.CreateMultivectorSparse(idScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Multivector<T> GetTermsMultivector(int termsCount)
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

            return GeometricProcessor.CreateMultivectorSparse(idScalarDictionary);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Multivector<T> GetTermsMultivector(int termsCount, double minValue, double maxValue)
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

            return GeometricProcessor.CreateMultivectorSparse(idScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Multivector<T> GetGradedMultivector()
        {
            var gradeIndexScalarDictionary = 
                new Dictionary<uint, Dictionary<ulong, T>>();

            for (var grade = 0U; grade <= VSpaceDimension; grade++)
                gradeIndexScalarDictionary.Add(
                    grade, 
                    GetKVectorIndexScalarDictionary(grade)
                );

            return GeometricProcessor.CreateMultivectorGraded(gradeIndexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Multivector<T> GetGradedMultivector(double minValue, double maxValue)
        {
            var gradeIndexScalarDictionary = 
                new Dictionary<uint, Dictionary<ulong, T>>();

            for (var grade = 0U; grade <= VSpaceDimension; grade++)
                gradeIndexScalarDictionary.Add(
                    grade, 
                    GetKVectorIndexScalarDictionary(grade, minValue, maxValue)
                );

            return GeometricProcessor.CreateMultivectorGraded(gradeIndexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Multivector<T> GetGradedMultivector(int termsCount)
        {
            var gaSpaceDimension = (int) GaSpaceDimension;

            if (termsCount > gaSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(termsCount));

            var composer = GeometricProcessor.CreateMultivectorGradedStorageComposer();

            var idList =
                Enumerable
                    .Range(0, gaSpaceDimension)
                    .Shuffled(RandomGenerator)
                    .Take(termsCount);

            foreach (var id in idList)
                composer.AddTerm((ulong) id, GetScalar());

            return composer.CreateMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Multivector<T> GetGradedMultivector(int termsCount, double minValue, double maxValue)
        {
            var gaSpaceDimension = (int) GaSpaceDimension;

            if (termsCount > gaSpaceDimension)
                throw new ArgumentOutOfRangeException(nameof(termsCount));

            var composer = GeometricProcessor.CreateMultivectorGradedStorageComposer();

            var idList =
                Enumerable
                    .Range(0, gaSpaceDimension)
                    .Shuffled(RandomGenerator)
                    .Take(termsCount);

            foreach (var id in idList)
                composer.AddTerm((ulong) id, GetScalar(minValue, maxValue));

            return composer.CreateMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<Vector<T>> GetVectors(int count)
        {
            while (count > 0)
            {
                yield return GetVector();
                count--;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVector<T> GetBlade(uint grade)
        {
            if (grade == 0U)
                return GetScalarTerm();

            if (grade == 1U)
                return GetVector().AsKVector();

            if (grade == VSpaceDimension)
                return GetKVectorTermByGradeIndex(grade, 0);

            return GetVectors((int) grade).Op();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PureRotor<T> GetEuclideanPureRotor()
        {
            return GeometricEuclideanProcessor.CreatePureRotor(
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
                        ? GeometricProcessor.ScalarOne 
                        : GeometricProcessor.ScalarZero;

                i++;
            }

            return array;
        }
    }
}