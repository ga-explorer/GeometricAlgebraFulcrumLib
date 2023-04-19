//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using DataStructuresLib.BitManipulation;
//using DataStructuresLib.Random;
//using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
//using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
//using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;
//using GeometricAlgebraFulcrumLib.Utilities.Extensions;

//namespace GeometricAlgebraFulcrumLib.Utilities.Composers
//{
//    public class GeometricAlgebraRandomComposer<T> :
//        LinearAlgebraRandomComposer<T>,
//        IGeometricAlgebraSpace
//    {
//        private readonly ulong[] _kVectorSpaceDimensions;


//        public IGeometricAlgebraProcessor<T> GeometricProcessor { get; }

//        public IGeometricAlgebraEuclideanProcessor<T> GeometricEuclideanProcessor
//            => (IGeometricAlgebraEuclideanProcessor<T>) GeometricProcessor;

//        public uint VSpaceDimensions 
//            => GeometricProcessor.VSpaceDimensions;

//        public ulong GaSpaceDimensions 
//            => GeometricProcessor.GaSpaceDimensions;

//        public ulong MaxBasisBladeId 
//            => GeometricProcessor.MaxBasisBladeId;

//        public uint GradesCount 
//            => GeometricProcessor.GradesCount;

//        public IEnumerable<uint> Grades 
//            => GeometricProcessor.Grades;


//        internal GeometricAlgebraRandomComposer(IGeometricAlgebraProcessor<T> geometricProcessor)
//            : base(geometricProcessor)
//        {
//            GeometricProcessor = geometricProcessor;

//            _kVectorSpaceDimensions = 
//                GeometricProcessor
//                    .GradesCount
//                    .GetRange()
//                    .Select(grade => GeometricProcessor.KVectorSpaceDimension(grade))
//                    .ToArray();
//        }

//        internal GeometricAlgebraRandomComposer(IGeometricAlgebraProcessor<T> geometricProcessor, int seed)
//            : base(geometricProcessor, seed)
//        {
//            GeometricProcessor = geometricProcessor;
            
//            _kVectorSpaceDimensions = 
//                GeometricProcessor
//                    .GradesCount
//                    .GetRange()
//                    .Select(grade => GeometricProcessor.KVectorSpaceDimension(grade))
//                    .ToArray();
//        }

//        internal GeometricAlgebraRandomComposer(IGeometricAlgebraProcessor<T> geometricProcessor, Random randomGenerator)
//            : base(geometricProcessor, randomGenerator)
//        {
//            GeometricProcessor = geometricProcessor;
            
//            _kVectorSpaceDimensions = 
//                GeometricProcessor
//                    .GradesCount
//                    .GetRange()
//                    .Select(grade => GeometricProcessor.KVectorSpaceDimension(grade))
//                    .ToArray();
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public uint GetGrade()
//        {
//            return (uint) RandomGenerator.Next((int) VSpaceDimensions + 1);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public ulong GetBasisVectorId()
//        {
//            return GetBasisVectorIndex().BasisVectorIndexToId();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public ulong GetBasisBivectorId()
//        {
//            return GetBasisBivectorIndex().BasisBivectorIndexToId();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public ulong GetBasisBladeId()
//        {
//            return GetIndex(MaxBasisBladeId);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public ulong GetBasisBladeId(uint grade)
//        {
//            return GetBasisBladeIndex(grade).BasisBladeIndexToId(grade);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public RGaGradeKvIndexRecord GetBasisBladeGradeIndex()
//        {
//            return GetBasisBladeId().BasisBladeIdToGradeIndex();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public ulong GetBasisVectorIndex()
//        {
//            return GetIndex(VSpaceDimensions - 1);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public ulong GetBasisBivectorIndex()
//        {
//            return GetIndex(_kVectorSpaceDimensions[2]);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public ulong GetBasisBladeIndex(uint grade)
//        {
//            return GetIndex(_kVectorSpaceDimensions[grade]);
//        }


//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public Dictionary<ulong, T> GetKVectorIndexScalarDictionary(uint grade)
//        {
//            var kvSpaceDimensions = 
//                (int) VSpaceDimensions.KVectorSpaceDimension(grade);

//            return Enumerable
//                .Range(0, kvSpaceDimensions)
//                .ToDictionary(
//                    index => (ulong) index, 
//                    _ => GetScalarValue()
//                );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public Dictionary<ulong, T> GetKVectorIndexScalarDictionary(uint grade, double minValue, double maxValue)
//        {
//            var kvSpaceDimensions = 
//                (int) VSpaceDimensions.KVectorSpaceDimension(grade);

//            return Enumerable
//                .Range(0, kvSpaceDimensions)
//                .ToDictionary(
//                    index => (ulong) index, 
//                    _ => GetScalarValue(minValue, maxValue)
//                );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaKVector<T> GetScalarTerm()
//        {
//            return GeometricProcessor.CreateKVectorScalar(GetScalarValue());
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaKVector<T> GetScalarTerm(double minValue, double maxValue)
//        {
//            return GeometricProcessor.CreateKVectorScalar(
//                GetScalarValue(minValue, maxValue)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaVector<T> GetVectorTerm()
//        {
//            var index = GetBasisVectorIndex();
//            var scalar = GetScalarValue();

//            return GeometricProcessor.CreateVectorTerm(index, scalar);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaVector<T> GetVectorTerm(double minValue, double maxValue)
//        {
//            var index = GetBasisVectorIndex();
//            var scalar = GetScalarValue(minValue, maxValue);

//            return GeometricProcessor.CreateVectorTerm(index, scalar);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaVector<T> GetVectorTermByIndex(ulong index)
//        {
//            return GeometricProcessor.CreateVectorTerm(
//                index,
//                GetScalarValue()
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaVector<T> GetVectorTermByIndex(ulong index, double minValue, double maxValue)
//        {
//            return GeometricProcessor.CreateVectorTerm(
//                index,
//                GetScalarValue(minValue, maxValue)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaBivector<T> GetBivectorTerm()
//        {
//            var index = GetBasisBivectorIndex();
//            var scalar = GetScalarValue();

//            return GeometricProcessor.CreateBivectorTerm(index, scalar);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaBivector<T> GetBivectorTerm(double minValue, double maxValue)
//        {
//            var index = GetBasisBivectorIndex();
//            var scalar = GetScalarValue(minValue, maxValue);

//            return GeometricProcessor.CreateBivectorTerm(index, scalar);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaBivector<T> GetBivectorTermByIndex(ulong index)
//        {
//            return GeometricProcessor.CreateBivectorTerm(index, GetScalarValue());
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaBivector<T> GetBivectorTermByIndex(ulong index, double minValue, double maxValue)
//        {
//            return GeometricProcessor.CreateBivectorTerm(index, GetScalarValue(minValue, maxValue));
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaKVector<T> GetKVectorTerm()
//        {
//            var (grade, index) = GetBasisBladeGradeIndex();
//            var scalar = GetScalarValue();

//            return GeometricProcessor.CreateKVectorTerm(grade, index, scalar);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaKVector<T> GetKVectorTerm(double minValue, double maxValue)
//        {
//            var (grade, index) = GetBasisBladeGradeIndex();
//            var scalar = GetScalarValue(minValue, maxValue);

//            return GeometricProcessor.CreateKVectorTerm(grade, index, scalar);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaKVector<T> GetKVectorTermOfGrade(uint grade)
//        {
//            var index = GetBasisBladeIndex(grade);
//            var scalar = GetScalarValue();

//            return GeometricProcessor.CreateKVectorTerm(grade, index, scalar);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaKVector<T> GetKVectorTermOfGrade(uint grade, double minValue, double maxValue)
//        {
//            var index = GetBasisBladeIndex(grade);
//            var scalar = GetScalarValue(minValue, maxValue);

//            return GeometricProcessor.CreateKVectorTerm(grade, index, scalar);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaKVector<T> GetKVectorTermById(ulong id)
//        {
//            return GeometricProcessor.CreateKVectorTerm(
//                id,
//                GetScalarValue()
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaKVector<T> GetKVectorTermById(ulong id, double minValue, double maxValue)
//        {
//            return GeometricProcessor.CreateKVectorTerm(
//                id,
//                GetScalarValue(minValue, maxValue)
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaKVector<T> GetKVectorTermByGradeIndex(uint grade, ulong index)
//        {
//            return GeometricProcessor.CreateKVectorTerm(
//                grade,
//                index,
//                GetScalarValue()
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaKVector<T> GetKVectorTermByGradeIndex(uint grade, ulong index, double minValue, double maxValue)
//        {
//            var scalar = GetScalarValue(minValue, maxValue);

//            return GeometricProcessor.CreateKVectorTerm(grade, index, scalar);
//        }
        
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaVector<T> GetVector()
//        {
//            var indexScalarDictionary =
//                VSpaceDimensions
//                    .GetRange()
//                    .ToDictionary(
//                        index => (ulong) index,
//                        _ => GetScalarValue()
//                    );

//            return GeometricProcessor.CreateVector(indexScalarDictionary);
//        }
        
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaVector<T> GetVector(int termsCount, bool makeUnitVector = false)
//        {
//            if (termsCount > VSpaceDimensions)
//                throw new ArgumentOutOfRangeException(nameof(termsCount));

//            var indexScalarDictionary =
//                termsCount
//                    .GetRange()
//                    .ToDictionary(
//                        index => (ulong) index,
//                        _ => GetScalarValue()
//                    );

//            var vector = 
//                GeometricProcessor.CreateVector(indexScalarDictionary);

//            return makeUnitVector 
//                ? vector.DivideByNorm() 
//                : vector;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaVector<T> GetVector(double minValue, double maxValue)
//        {
//            var indexScalarDictionary =
//                VSpaceDimensions
//                    .GetRange()
//                    .ToDictionary(
//                        index => (ulong) index,
//                        _ => GetScalarValue(minValue, maxValue)
//                    );

//            return GeometricProcessor.CreateVector(indexScalarDictionary);
//        }
        
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaVector<T> GetSparseVector(int termsCount)
//        {
//            if (termsCount > VSpaceDimensions)
//                throw new ArgumentOutOfRangeException(nameof(termsCount));

//            var indexScalarDictionary = 
//                VSpaceDimensions
//                    .GetRange()
//                    .Shuffled(RandomGenerator)
//                    .Take(termsCount)
//                    .ToDictionary(
//                        index => (ulong) index,
//                        _ => GetScalarValue()
//                    );
            
//            return GeometricProcessor.CreateVector(indexScalarDictionary);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaVector<T> GetSparseVector(int termsCount, double minValue, double maxValue)
//        {
//            if (termsCount > VSpaceDimensions)
//                throw new ArgumentOutOfRangeException(nameof(termsCount));

//            var indexScalarDictionary = 
//                VSpaceDimensions
//                    .GetRange()
//                    .Shuffled(RandomGenerator)
//                    .Take(termsCount)
//                    .ToDictionary(
//                        index => (ulong) index,
//                        _ => GetScalarValue(minValue, maxValue)
//                    );
            
//            return GeometricProcessor.CreateVector(indexScalarDictionary);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaBivector<T> GetBivector()
//        {
//            var kvSpaceDimensions = 
//                VSpaceDimensions.KVectorSpaceDimension(2);

//            var indexScalarDictionary =
//                Enumerable
//                    .Range(0, (int) kvSpaceDimensions)
//                    .ToDictionary(
//                        index => (ulong) index,
//                        _ => GetScalarValue()
//                    );

//            return GeometricProcessor.CreateBivector(indexScalarDictionary);
//        }
        
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaBivector<T> GetBivector(double minValue, double maxValue)
//        {
//            var kvSpaceDimensions = 
//                VSpaceDimensions.KVectorSpaceDimension(2);

//            var indexScalarDictionary =
//                Enumerable
//                    .Range(0, (int) kvSpaceDimensions)
//                    .ToDictionary(
//                        index => (ulong) index,
//                        _ => GetScalarValue(minValue, maxValue)
//                    );

//            return GeometricProcessor.CreateBivector(indexScalarDictionary);
//        }
        
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaBivector<T> GetSparseBivector(int termsCount)
//        {
//            var kvSpaceDimensions = 
//                (int) VSpaceDimensions.KVectorSpaceDimension(2);

//            if (termsCount > kvSpaceDimensions)
//                throw new ArgumentOutOfRangeException(nameof(termsCount));

//            var indexScalarDictionary =
//                Enumerable
//                    .Range(0, kvSpaceDimensions)
//                    .Shuffled(RandomGenerator)
//                    .Take(termsCount)
//                    .ToDictionary(
//                        index => (ulong) index,
//                        _ => GetScalarValue()
//                    );

//            return GeometricProcessor.CreateBivector(indexScalarDictionary);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaBivector<T> GetSparseBivector(int termsCount, double minValue, double maxValue)
//        {
//            var kvSpaceDimensions = 
//                (int) VSpaceDimensions.KVectorSpaceDimension(2);

//            if (termsCount > kvSpaceDimensions)
//                throw new ArgumentOutOfRangeException(nameof(termsCount));
            
//            var indexScalarDictionary =
//                Enumerable
//                    .Range(0, kvSpaceDimensions)
//                    .Shuffled(RandomGenerator)
//                    .Take(termsCount)
//                    .ToDictionary(
//                        index => (ulong) index,
//                        _ => GetScalarValue(minValue, maxValue)
//                    );

//            return GeometricProcessor.CreateBivector(indexScalarDictionary);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaKVector<T> GetKVector()
//        {
//            return GetKVectorOfGrade(GetGrade());
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaKVector<T> GetKVector(double minValue, double maxValue)
//        {
//            return GetKVectorOfGrade(GetGrade(), minValue, maxValue);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaKVector<T> GetKVectorOfGrade(uint grade)
//        {
//            var kvSpaceDimensions = 
//                VSpaceDimensions.KVectorSpaceDimension(grade);

//            var indexScalarDictionary =
//                Enumerable
//                    .Range(0, (int) kvSpaceDimensions)
//                    .ToDictionary(
//                        index => (ulong) index,
//                        _ => GetScalarValue()
//                    );

//            return GeometricProcessor.CreateKVector(grade, indexScalarDictionary);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaKVector<T> GetKVectorOfGrade(uint grade, double minValue, double maxValue)
//        {
//            var kvSpaceDimensions = 
//                VSpaceDimensions.KVectorSpaceDimension(grade);

//            var indexScalarDictionary =
//                Enumerable
//                    .Range(0, (int) kvSpaceDimensions)
//                    .ToDictionary(
//                        index => (ulong) index,
//                        _ => GetScalarValue(minValue, maxValue)
//                    );

//            return GeometricProcessor.CreateKVector(grade, indexScalarDictionary);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaKVector<T> GetSparseKVectorOfGrade(uint grade, int termsCount)
//        {
//            var kvSpaceDimensions = 
//                (int) VSpaceDimensions.KVectorSpaceDimension(grade);

//            if (termsCount > kvSpaceDimensions)
//                throw new ArgumentOutOfRangeException(nameof(termsCount));

//            var indexScalarDictionary =
//                Enumerable
//                    .Range(0, kvSpaceDimensions)
//                    .Shuffled(RandomGenerator)
//                    .Take(termsCount)
//                    .ToDictionary(
//                        index => (ulong) index,
//                        _ => GetScalarValue()
//                    );

//            return GeometricProcessor.CreateKVector(grade, indexScalarDictionary);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaKVector<T> GetSparseKVectorOfGrade(uint grade, int termsCount, double minValue, double maxValue)
//        {
//            var kvSpaceDimensions = 
//                (int) VSpaceDimensions.KVectorSpaceDimension(grade);

//            if (termsCount > kvSpaceDimensions)
//                throw new ArgumentOutOfRangeException(nameof(termsCount));

//            var indexScalarDictionary =
//                Enumerable
//                    .Range(0, kvSpaceDimensions)
//                    .Shuffled(RandomGenerator)
//                    .Take(termsCount)
//                    .ToDictionary(
//                        index => (ulong) index,
//                        _ => GetScalarValue(minValue, maxValue)
//                    );

//            return GeometricProcessor.CreateKVector(grade, indexScalarDictionary);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaMultivector<T> GetTermsMultivector()
//        {
//            var gaSpaceDimensions = (int) GaSpaceDimensions;

//            var idScalarDictionary =
//                Enumerable
//                    .Range(0, gaSpaceDimensions)
//                    .ToDictionary(
//                        index => (ulong) index,
//                        _ => GetScalarValue()
//                    );

//            return GeometricProcessor.CreateMultivectorSparse(idScalarDictionary);
//        }
        
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaMultivector<T> GetTermsMultivector(double minValue, double maxValue)
//        {
//            var gaSpaceDimensions = (int) GaSpaceDimensions;

//            var idScalarDictionary =
//                Enumerable
//                    .Range(0, gaSpaceDimensions)
//                    .ToDictionary(
//                        index => (ulong) index,
//                        _ => GetScalarValue(minValue, maxValue)
//                    );

//            return GeometricProcessor.CreateMultivectorSparse(idScalarDictionary);
//        }
        
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaMultivector<T> GetTermsMultivector(int termsCount)
//        {
//            var gaSpaceDimensions = (int) GaSpaceDimensions;

//            if (termsCount > gaSpaceDimensions)
//                throw new ArgumentOutOfRangeException(nameof(termsCount));

//            var idScalarDictionary =
//                Enumerable
//                    .Range(0, gaSpaceDimensions)
//                    .Shuffled(RandomGenerator)
//                    .Take(termsCount)
//                    .ToDictionary(
//                        index => (ulong) index,
//                        _ => GetScalarValue()
//                    );

//            return GeometricProcessor.CreateMultivectorSparse(idScalarDictionary);
//        }
        
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaMultivector<T> GetTermsMultivector(int termsCount, double minValue, double maxValue)
//        {
//            var gaSpaceDimensions = (int) GaSpaceDimensions;

//            if (termsCount > gaSpaceDimensions)
//                throw new ArgumentOutOfRangeException(nameof(termsCount));

//            var idScalarDictionary =
//                Enumerable
//                    .Range(0, gaSpaceDimensions)
//                    .Shuffled(RandomGenerator)
//                    .Take(termsCount)
//                    .ToDictionary(
//                        index => (ulong) index,
//                        _ => GetScalarValue(minValue, maxValue)
//                    );

//            return GeometricProcessor.CreateMultivectorSparse(idScalarDictionary);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaMultivector<T> GetGradedMultivector()
//        {
//            var gradeIndexScalarDictionary = 
//                new Dictionary<uint, Dictionary<ulong, T>>();

//            for (var grade = 0U; grade <= VSpaceDimensions; grade++)
//                gradeIndexScalarDictionary.Add(
//                    grade, 
//                    GetKVectorIndexScalarDictionary(grade)
//                );

//            return GeometricProcessor.CreateMultivectorGraded(gradeIndexScalarDictionary);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaMultivector<T> GetGradedMultivector(double minValue, double maxValue)
//        {
//            var gradeIndexScalarDictionary = 
//                new Dictionary<uint, Dictionary<ulong, T>>();

//            for (var grade = 0U; grade <= VSpaceDimensions; grade++)
//                gradeIndexScalarDictionary.Add(
//                    grade, 
//                    GetKVectorIndexScalarDictionary(grade, minValue, maxValue)
//                );

//            return GeometricProcessor.CreateMultivectorGraded(gradeIndexScalarDictionary);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaMultivector<T> GetGradedMultivector(int termsCount)
//        {
//            var gaSpaceDimensions = (int) GaSpaceDimensions;

//            if (termsCount > gaSpaceDimensions)
//                throw new ArgumentOutOfRangeException(nameof(termsCount));

//            var composer = GeometricProcessor.CreateMultivectorGradedStorageComposer();

//            var idList =
//                Enumerable
//                    .Range(0, gaSpaceDimensions)
//                    .Shuffled(RandomGenerator)
//                    .Take(termsCount);

//            foreach (var id in idList)
//                composer.AddTerm((ulong) id, GetScalarValue());

//            return composer.CreateMultivector();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaMultivector<T> GetGradedMultivector(int termsCount, double minValue, double maxValue)
//        {
//            var gaSpaceDimensions = (int) GaSpaceDimensions;

//            if (termsCount > gaSpaceDimensions)
//                throw new ArgumentOutOfRangeException(nameof(termsCount));

//            var composer = GeometricProcessor.CreateMultivectorGradedStorageComposer();

//            var idList =
//                Enumerable
//                    .Range(0, gaSpaceDimensions)
//                    .Shuffled(RandomGenerator)
//                    .Take(termsCount);

//            foreach (var id in idList)
//                composer.AddTerm((ulong) id, GetScalarValue(minValue, maxValue));

//            return composer.CreateMultivector();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public IEnumerable<GaVector<T>> GetVectors(int count)
//        {
//            while (count > 0)
//            {
//                yield return GetVector();
//                count--;
//            }
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public GaKVector<T> GetBlade(uint grade)
//        {
//            if (grade == 0U)
//                return GetScalarTerm();

//            if (grade == 1U)
//                return GetVector().AsKVector();

//            if (grade == VSpaceDimensions)
//                return GetKVectorTermByGradeIndex(grade, 0);

//            return GetVectors((int) grade).Op();
//        }
        
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public PureRotor<T> GetEuclideanPureRotor()
//        {
//            return GeometricEuclideanProcessor.CreatePureRotor(
//                GetVector(),
//                GetVector()
//            );
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public IEnumerable<T> GetScalars(int count)
//        {
//            return Enumerable
//                .Range(0, count)
//                .Select(_ => GetScalarValue());
//        }

//        public T[,] GetArray(int rowsCount, int columnsCount)
//        {
//            var array = new T[rowsCount, columnsCount];

//            for (var i = 0; i < rowsCount; i++)
//            for (var j = 0; j < columnsCount; j++)
//                array[i, j] = GetScalarValue();

//            return array;
//        }

//        public T[,] GetPermutationArray(int size)
//        {
//            var array = new T[size, size];

//            var indexList = Enumerable
//                .Range(0, size)
//                .Shuffled(RandomGenerator);

//            var i = 0;
//            foreach (var colIndex in indexList)
//            {
//                for (var j = 0; j < size; j++)
//                    array[i, j] = j == colIndex
//                        ? GeometricProcessor.ScalarOne 
//                        : GeometricProcessor.ScalarZero;

//                i++;
//            }

//            return array;
//        }
//    }
//}