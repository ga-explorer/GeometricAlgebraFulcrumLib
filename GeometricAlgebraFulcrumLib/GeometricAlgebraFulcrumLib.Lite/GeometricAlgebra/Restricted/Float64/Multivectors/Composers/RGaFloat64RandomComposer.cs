using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Combinations;
using DataStructuresLib.IndexSets;
using DataStructuresLib.Random;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers
{
    public class RGaFloat64RandomComposer :
        ScalarRandomComposer<double>
    {
        private readonly IReadOnlyList<int> _kVectorSpaceDimensions;


        public RGaFloat64Processor Processor { get; private set; }

        public int VSpaceDimensions { get; }

        public int GaSpaceDimensions
            => 1 << VSpaceDimensions;

        public int MaxGrade
            => VSpaceDimensions;

        public int GradeCount
            => VSpaceDimensions + 1;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaFloat64RandomComposer(RGaFloat64Processor metric, int vSpaceDimensions)
            : base(ScalarProcessorOfFloat64.DefaultProcessor)
        {
            if (vSpaceDimensions is < 2 or > 31)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            Processor = metric;
            VSpaceDimensions = vSpaceDimensions;

            _kVectorSpaceDimensions =
                GradeCount
                    .GetRange()
                    .Select(grade => (int)VSpaceDimensions.GetBinomialCoefficient(grade))
                    .ToImmutableArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaFloat64RandomComposer(RGaFloat64Processor metric, int vSpaceDimensions, int seed)
            : base(ScalarProcessorOfFloat64.DefaultProcessor, seed)
        {
            if (vSpaceDimensions is < 2 or > 31)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            Processor = metric;
            VSpaceDimensions = vSpaceDimensions;

            _kVectorSpaceDimensions =
                GradeCount
                    .GetRange()
                    .Select(grade => (int)VSpaceDimensions.GetBinomialCoefficient(grade))
                    .ToImmutableArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaFloat64RandomComposer(RGaFloat64Processor metric, int vSpaceDimensions, System.Random randomGenerator)
            : base(ScalarProcessorOfFloat64.DefaultProcessor, randomGenerator)
        {
            if (vSpaceDimensions is < 2 or > 31)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            Processor = metric;
            VSpaceDimensions = vSpaceDimensions;

            _kVectorSpaceDimensions =
                GradeCount
                    .GetRange()
                    .Select(grade => (int)VSpaceDimensions.GetBinomialCoefficient(grade))
                    .ToImmutableArray();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetGrade()
        {
            return RandomGenerator.Next(GradeCount + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetGrade(int minGrade)
        {
            if (minGrade < 0 || minGrade > VSpaceDimensions)
                throw new ArgumentOutOfRangeException(nameof(minGrade));

            return RandomGenerator.Next(minGrade, MaxGrade + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetGrade(int minGrade, int maxGrade)
        {
            if (minGrade < 0 || minGrade > VSpaceDimensions)
                throw new ArgumentOutOfRangeException(nameof(minGrade));

            if (maxGrade < 0 || maxGrade > VSpaceDimensions)
                throw new ArgumentOutOfRangeException(nameof(maxGrade));

            return minGrade <= maxGrade
                ? RandomGenerator.Next(minGrade, maxGrade + 1)
                : RandomGenerator.Next(maxGrade, minGrade + 1);
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
            return (ulong)RandomGenerator.Next(1 << VSpaceDimensions);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetBasisBladeId(int grade)
        {
            return GetBasisBladeIndex(grade).BasisBladeIndexToId((uint)grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaGradeKvIndexRecord GetBasisBladeGradeIndex()
        {
            return GetBasisBladeId().BasisBladeIdToGradeIndex();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetBasisVectorIndex()
        {
            return RandomGenerator.Next(VSpaceDimensions);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetBasisBivectorIndex()
        {
            return (ulong)RandomGenerator.Next(_kVectorSpaceDimensions[2]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetBasisBladeIndex(int grade)
        {
            return (ulong)RandomGenerator.Next(_kVectorSpaceDimensions[grade]);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Dictionary<ulong, double> GetKVectorIndexScalarDictionary(int grade)
        {
            var kvSpaceDimensions =
                _kVectorSpaceDimensions[grade];

            return Enumerable
                .Range(0, kvSpaceDimensions)
                .ToDictionary(
                    index => (ulong)index,
                    _ => GetScalarValue()
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Dictionary<ulong, double> GetKVectorIndexScalarDictionary(int grade, double minValue, double maxValue)
        {
            var kvSpaceDimensions =
                _kVectorSpaceDimensions[grade];

            return Enumerable
                .Range(0, kvSpaceDimensions)
                .ToDictionary(
                    index => (ulong)index,
                    _ => GetScalarValue(minValue, maxValue)
                );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBasisBlade GetBasisVector()
        {
            var id =
                GetBasisVectorIndex().IndexToUInt64IndexSetBitPattern();

            return new RGaBasisBlade(Processor, id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBasisBlade GetBasisBivector()
        {
            var indexList =
                RandomGenerator
                    .GetUniqueIndices(2, VSpaceDimensions)
                    .ToImmutableArray();

            var id =
                new Pair<int>(indexList[0], indexList[1]).IndexPairToUInt64IndexSetBitPattern();

            return new RGaBasisBlade(Processor, id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBasisBlade GetBasisKVector()
        {
            return GetBasisKVector(
                GetGrade(3)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBasisBlade GetBasisKVector(int grade)
        {
            if (grade < 3)
                throw new ArgumentOutOfRangeException(nameof(grade));

            var id =
                RandomGenerator
                    .GetUniqueIndices(grade, VSpaceDimensions)
                    .ToImmutableSortedSet()
                    .ToUInt64IndexSetBitPattern();

            return new RGaBasisBlade(Processor, id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBasisBlade GetBasisBlade()
        {
            return GetBasisBlade(
                GetGrade()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaBasisBlade GetBasisBlade(int grade)
        {
            return grade switch
            {
                < 0 => throw new ArgumentOutOfRangeException(nameof(grade)),
                0 => Processor.BasisScalar,
                1 => GetBasisVector(),
                2 => GetBasisBivector(),
                _ => GetBasisKVector(grade)
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IRGaSignedBasisBlade GetSignedBasisScalar()
        {
            var sign = RandomGenerator.GetSign();

            return Processor.CreateSignedBasisScalar(sign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaSignedBasisBlade GetSignedBasisVector()
        {
            var sign = RandomGenerator.GetSign();

            return GetBasisVector().ToSignedBasisBlade(sign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaSignedBasisBlade GetSignedBasisBivector()
        {
            var sign = RandomGenerator.GetSign();
            var basis = GetBasisBivector();

            return basis.ToSignedBasisBlade(sign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaSignedBasisBlade GetSignedBasisKVector()
        {
            var sign = RandomGenerator.GetSign();
            var basis = GetBasisKVector();

            return basis.ToSignedBasisBlade(sign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaSignedBasisBlade GetSignedBasisKVector(int grade)
        {
            var sign = RandomGenerator.GetSign();
            var basis = GetBasisKVector(grade);

            return basis.ToSignedBasisBlade(sign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaSignedBasisBlade GetSignedBasisBlade()
        {
            var sign = RandomGenerator.GetSign();
            var basis = GetBasisBlade();

            return basis.ToSignedBasisBlade(sign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaSignedBasisBlade GetSignedBasisBlade(int grade)
        {
            var sign = RandomGenerator.GetSign();
            var basis = GetBasisBlade(grade);

            return basis.ToSignedBasisBlade(sign);
        }



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar GetScalar()
        {
            return Processor.CreateScalar(GetScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Vector GetVector(int index)
        {
            return Processor.CreateTermVector(index, GetScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Bivector GetBivector(int index)
        {
            return Processor.CreateTermBivector(
                index.BasisVectorIndexToId(),
                GetScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64KVector GetKVector(ulong id)
        {
            return Processor.CreateTermKVector(
                id,
                GetScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Vector GetVector(bool sparseVector = true)
        {
            if (sparseVector)
            {
                var termsCount = RandomGenerator.Next(VSpaceDimensions);

                var indexScalarDictionary =
                    VSpaceDimensions
                        .GetRange()
                        .Shuffled(RandomGenerator)
                        .Take(termsCount)
                        .Select(p => 
                            new KeyValuePair<ulong, double>(
                                p.IndexToUInt64IndexSetBitPattern(),
                                GetScalarValue()
                            )
                        );

                return Processor
                    .CreateComposer()
                    .SetTerms(indexScalarDictionary)
                    .GetVector();
            }
            else
            {
                var scalarArray =
                    VSpaceDimensions
                        .GetRange()
                        .Select(_ => GetScalarValue());

                return Processor.CreateVector(scalarArray);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Vector GetSparseVector(int termsCount)
        {
            if (termsCount > VSpaceDimensions)
                throw new ArgumentOutOfRangeException(nameof(termsCount));

            var indexScalarDictionary =
                VSpaceDimensions
                    .GetRange()
                    .Shuffled(RandomGenerator)
                    .Take(termsCount)
                    .Select(p => 
                        new KeyValuePair<ulong, double>(
                            p.IndexToUInt64IndexSetBitPattern(),
                            GetScalarValue()
                        )
                    );

            return Processor
                .CreateComposer()
                .SetTerms(indexScalarDictionary)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Bivector GetBivector()
        {
            var kvSpaceDimensions =
                VSpaceDimensions.GetBinomialCoefficient(2);

            var indexScalarDictionary =
                Enumerable
                    .Range(0, (int)kvSpaceDimensions)
                    .Select(index => 
                        new KeyValuePair<ulong, double>(
                            index.BasisBivectorIndexToVectorIndexInt32Pair().IndexPairToUInt64IndexSetBitPattern(),
                            GetScalarValue()
                        )
                    );

            return Processor
                .CreateComposer()
                .SetTerms(indexScalarDictionary)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Bivector GetBivector(double minValue, double maxValue)
        {
            var kvSpaceDimensions =
                VSpaceDimensions.GetBinomialCoefficient(2);

            var indexScalarDictionary =
                Enumerable
                    .Range(0, (int)kvSpaceDimensions)
                    .Select(index => 
                        new KeyValuePair<ulong, double>(
                            index.BasisBivectorIndexToVectorIndexInt32Pair().IndexPairToUInt64IndexSetBitPattern(),
                            GetScalarValue(minValue, maxValue)
                        )
                    );

            return Processor
                .CreateComposer()
                .SetTerms(indexScalarDictionary)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Bivector GetSparseBivector(int termsCount)
        {
            var kvSpaceDimensions =
                (int)VSpaceDimensions.GetBinomialCoefficient(2);

            if (termsCount > kvSpaceDimensions)
                throw new ArgumentOutOfRangeException(nameof(termsCount));

            var indexScalarDictionary =
                Enumerable
                    .Range(0, kvSpaceDimensions)
                    .Shuffled(RandomGenerator)
                    .Take(termsCount)
                    .Select(index => 
                        new KeyValuePair<ulong, double>(
                            index.BasisBivectorIndexToVectorIndexInt32Pair().IndexPairToUInt64IndexSetBitPattern(),
                            GetScalarValue()
                        )
                    );

            return Processor
                .CreateComposer()
                .SetTerms(indexScalarDictionary)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Bivector GetSparseBivector(int termsCount, double minValue, double maxValue)
        {
            var kvSpaceDimensions =
                (int)VSpaceDimensions.GetBinomialCoefficient(2);

            if (termsCount > kvSpaceDimensions)
                throw new ArgumentOutOfRangeException(nameof(termsCount));

            var indexScalarDictionary =
                Enumerable
                    .Range(0, kvSpaceDimensions)
                    .Shuffled(RandomGenerator)
                    .Take(termsCount)
                    .Select(index => 
                        new KeyValuePair<ulong, double>(
                            index.BasisBivectorIndexToVectorIndexInt32Pair().IndexPairToUInt64IndexSetBitPattern(),
                            GetScalarValue(minValue, maxValue)
                        )
                    );

            return Processor
                .CreateComposer()
                .SetTerms(indexScalarDictionary)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64KVector GetKVector()
        {
            return GetKVectorOfGrade(GetGrade());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64KVector GetKVector(double minValue, double maxValue)
        {
            return GetKVectorOfGrade(GetGrade(), minValue, maxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64KVector GetKVectorOfGrade(int grade)
        {
            var kvSpaceDimensions =
                VSpaceDimensions.GetBinomialCoefficient(grade);

            var indexScalarDictionary =
                Enumerable
                    .Range(0, (int)kvSpaceDimensions)
                    .Select(index => 
                        new KeyValuePair<ulong, double>(
                            BasisBladeUtils.BasisBladeGradeIndexToId((uint)grade, (ulong)index),
                            GetScalarValue()
                        )
                    );

            return Processor
                .CreateComposer()
                .SetTerms(indexScalarDictionary)
                .GetKVector(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64KVector GetKVectorOfGrade(int grade, double minValue, double maxValue)
        {
            var kvSpaceDimensions =
                VSpaceDimensions.GetBinomialCoefficient(grade);

            var indexScalarDictionary =
                Enumerable
                    .Range(0, (int)kvSpaceDimensions)
                    .Select(index => 
                        new KeyValuePair<ulong, double>(
                            BasisBladeUtils.BasisBladeGradeIndexToId((uint)grade, (ulong)index),
                            GetScalarValue(minValue, maxValue)
                        )
                    );

            return Processor
                .CreateComposer()
                .SetTerms(indexScalarDictionary)
                .GetKVector(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64KVector GetSparseKVectorOfGrade(int grade, int termsCount)
        {
            var kvSpaceDimensions =
                (int)VSpaceDimensions.GetBinomialCoefficient(grade);

            if (termsCount > kvSpaceDimensions)
                throw new ArgumentOutOfRangeException(nameof(termsCount));

            var indexScalarDictionary =
                Enumerable
                    .Range(0, kvSpaceDimensions)
                    .Shuffled(RandomGenerator)
                    .Take(termsCount)
                    .Select(index => 
                        new KeyValuePair<ulong, double>(
                            BasisBladeUtils.BasisBladeGradeIndexToId((uint)grade, (ulong)index),
                            GetScalarValue()
                        )
                    );

            return Processor
                .CreateComposer()
                .SetTerms(indexScalarDictionary)
                .GetKVector(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64KVector GetSparseKVectorOfGrade(int grade, int termsCount, double minValue, double maxValue)
        {
            var kvSpaceDimensions =
                (int)VSpaceDimensions.GetBinomialCoefficient(grade);

            if (termsCount > kvSpaceDimensions)
                throw new ArgumentOutOfRangeException(nameof(termsCount));

            var indexScalarDictionary =
                Enumerable
                    .Range(0, kvSpaceDimensions)
                    .Shuffled(RandomGenerator)
                    .Take(termsCount)
                    .Select(index => 
                        new KeyValuePair<ulong, double>(
                            BasisBladeUtils.BasisBladeGradeIndexToId((uint)grade, (ulong)index),
                            GetScalarValue(minValue, maxValue)
                        )
                    );

            return Processor
                .CreateComposer()
                .SetTerms(indexScalarDictionary)
                .GetKVector(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64GradedMultivector GetMultivector()
        {
            var gaSpaceDimensions = GaSpaceDimensions;

            var idScalarDictionary =
                Enumerable
                    .Range(0, gaSpaceDimensions)
                    .Select(index => 
                        new KeyValuePair<ulong, double>(
                            (ulong)index,
                            GetScalarValue()
                        )
                    );

            return Processor
                .CreateComposer()
                .SetTerms(idScalarDictionary)
                .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64GradedMultivector GetMultivector(double minValue, double maxValue)
        {
            var gaSpaceDimensions = GaSpaceDimensions;

            var idScalarDictionary =
                Enumerable
                    .Range(0, gaSpaceDimensions)
                    .Select(index => 
                        new KeyValuePair<ulong, double>(
                            (ulong)index,
                            GetScalarValue(minValue, maxValue)
                        )
                    );

            return Processor
                .CreateComposer()
                .SetTerms(idScalarDictionary)
                .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64GradedMultivector GetMultivector(int termsCount)
        {
            var gaSpaceDimensions = GaSpaceDimensions;

            if (termsCount > gaSpaceDimensions)
                throw new ArgumentOutOfRangeException(nameof(termsCount));

            var idScalarDictionary =
                Enumerable
                    .Range(0, gaSpaceDimensions)
                    .Shuffled(RandomGenerator)
                    .Take(termsCount)
                    .Select(index => 
                        new KeyValuePair<ulong, double>(
                            (ulong)index,
                            GetScalarValue()
                        )
                    );

            return Processor
                .CreateComposer()
                .SetTerms(idScalarDictionary)
                .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64UniformMultivector GetUniformMultivector(int termsCount)
        {
            var gaSpaceDimensions = GaSpaceDimensions;

            if (termsCount > gaSpaceDimensions)
                throw new ArgumentOutOfRangeException(nameof(termsCount));

            var idScalarDictionary =
                Enumerable
                    .Range(0, gaSpaceDimensions)
                    .Shuffled(RandomGenerator)
                    .Take(termsCount)
                    .Select(index => 
                        new KeyValuePair<ulong, double>(
                            (ulong)index,
                            GetScalarValue()
                        )
                    );

            return Processor
                .CreateComposer()
                .SetTerms(idScalarDictionary)
                .GetUniformMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64GradedMultivector GetMultivector(int termsCount, double minValue, double maxValue)
        {
            var gaSpaceDimensions = GaSpaceDimensions;

            if (termsCount > gaSpaceDimensions)
                throw new ArgumentOutOfRangeException(nameof(termsCount));

            var idScalarDictionary =
                Enumerable
                    .Range(0, gaSpaceDimensions)
                    .Shuffled(RandomGenerator)
                    .Take(termsCount)
                    .Select(index => 
                        new KeyValuePair<ulong, double>(
                            (ulong)index,
                            GetScalarValue(minValue, maxValue)
                        )
                    );

            return Processor
                .CreateComposer()
                .SetTerms(idScalarDictionary)
                .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<RGaFloat64Vector> GetVectors(int count)
        {
            while (count > 0)
            {
                yield return GetVector();
                count--;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64KVector GetBlade(int grade)
        {
            if (grade == 0U)
                return GetScalar();

            if (grade == 1U)
                return GetVector();

            return GetVectors(grade).Op();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<double> GetScalars(int count)
        {
            return Enumerable
                .Range(0, count)
                .Select(_ => GetScalarValue());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64PureRotor GetEuclideanPureRotor()
        {
            return GetVector().CreatePureRotor(GetVector());
        }


        public double[,] GetArray(int rowsCount, int columnsCount)
        {
            var array = new double[rowsCount, columnsCount];

            for (var i = 0; i < rowsCount; i++)
                for (var j = 0; j < columnsCount; j++)
                    array[i, j] = GetScalarValue();

            return array;
        }

        public double[,] GetPermutationArray(int size)
        {
            var array = new double[size, size];

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