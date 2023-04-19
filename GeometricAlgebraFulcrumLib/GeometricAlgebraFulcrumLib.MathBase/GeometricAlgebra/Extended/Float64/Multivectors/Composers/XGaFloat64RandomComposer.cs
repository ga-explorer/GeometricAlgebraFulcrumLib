using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Combinations;
using DataStructuresLib.IndexSets;
using DataStructuresLib.Random;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors.Composers
{
    public class XGaFloat64RandomComposer :
        ScalarRandomComposer<double>
    {
        private readonly IReadOnlyList<int> _kVectorSpaceDimensions;


        public XGaFloat64Processor Processor { get; private set; }

        public XGaMetric Metric
            => Processor;

        public int VSpaceDimensions { get; }

        public int GaSpaceDimensions
            => 1 << VSpaceDimensions;

        public int MaxGrade
            => VSpaceDimensions;

        public int GradeCount
            => VSpaceDimensions + 1;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal XGaFloat64RandomComposer(XGaFloat64Processor metric, int vSpaceDimensions)
            : base(ScalarProcessorFloat64.DefaultProcessor)
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
        internal XGaFloat64RandomComposer(XGaFloat64Processor metric, int vSpaceDimensions, int seed)
            : base(ScalarProcessorFloat64.DefaultProcessor, seed)
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
        internal XGaFloat64RandomComposer(XGaFloat64Processor metric, int vSpaceDimensions, System.Random randomGenerator)
            : base(ScalarProcessorFloat64.DefaultProcessor, randomGenerator)
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
        public XGaBasisBlade GetBasisVector()
        {
            var id =
                GetBasisVectorIndex().IndexToIndexSet();

            return new XGaBasisBlade(Processor, id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBasisBlade GetBasisBivector()
        {
            var indexList =
                RandomGenerator
                    .GetUniqueIndices(2, VSpaceDimensions)
                    .ToImmutableArray();

            var id =
                new Pair<int>(indexList[0], indexList[1]).IndexPairToIndexSet();

            return new XGaBasisBlade(Processor, id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBasisBlade GetBasisKVector()
        {
            return GetBasisKVector(
                GetGrade(3)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBasisBlade GetBasisKVector(int grade)
        {
            if (grade < 3)
                throw new ArgumentOutOfRangeException(nameof(grade));

            var id =
                RandomGenerator
                    .GetUniqueIndices(grade, VSpaceDimensions)
                    .ToImmutableSortedSet()
                    .ToIndexSet();

            return new XGaBasisBlade(Processor, id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBasisBlade GetBasisBlade()
        {
            return GetBasisBlade(
                GetGrade()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBasisBlade GetBasisBlade(int grade)
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
        public IXGaSignedBasisBlade GetSignedBasisScalar()
        {
            var sign = RandomGenerator.GetSign();

            return Processor.CreateSignedBasisScalar(sign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaSignedBasisBlade GetSignedBasisVector()
        {
            var sign = RandomGenerator.GetSign();

            return GetBasisVector().ToSignedBasisBlade(sign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaSignedBasisBlade GetSignedBasisBivector()
        {
            var sign = RandomGenerator.GetSign();
            var basis = GetBasisBivector();

            return basis.ToSignedBasisBlade(sign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaSignedBasisBlade GetSignedBasisKVector()
        {
            var sign = RandomGenerator.GetSign();
            var basis = GetBasisKVector();

            return basis.ToSignedBasisBlade(sign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaSignedBasisBlade GetSignedBasisKVector(int grade)
        {
            var sign = RandomGenerator.GetSign();
            var basis = GetBasisKVector(grade);

            return basis.ToSignedBasisBlade(sign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaSignedBasisBlade GetSignedBasisBlade()
        {
            var sign = RandomGenerator.GetSign();
            var basis = GetBasisBlade();

            return basis.ToSignedBasisBlade(sign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaSignedBasisBlade GetSignedBasisBlade(int grade)
        {
            var sign = RandomGenerator.GetSign();
            var basis = GetBasisBlade(grade);

            return basis.ToSignedBasisBlade(sign);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar GetScalar()
        {
            return Processor.CreateScalar(GetScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Vector GetVector(double minValue, double maxValue, bool sparseVector = true)
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
                            new KeyValuePair<IIndexSet, double>(
                                p.IndexToIndexSet(),
                                GetScalarValue(minValue, maxValue)
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
                        .Select(_ => GetScalarValue(minValue, maxValue));

                return Processor.CreateVector(scalarArray);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Vector GetVector(bool sparseVector = true)
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
                            new KeyValuePair<IIndexSet, double>(
                                p.IndexToIndexSet(),
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
        public XGaFloat64Vector GetSparseVector(int termsCount)
        {
            if (termsCount > VSpaceDimensions)
                throw new ArgumentOutOfRangeException(nameof(termsCount));

            var indexScalarDictionary =
                VSpaceDimensions
                    .GetRange()
                    .Shuffled(RandomGenerator)
                    .Take(termsCount)
                    .Select(p => 
                        new KeyValuePair<IIndexSet, double>(
                            p.IndexToIndexSet(),
                            GetScalarValue()
                        )
                    );

            return Processor
                .CreateComposer()
                .SetTerms(indexScalarDictionary)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Bivector GetBivector()
        {
            var kvSpaceDimensions =
                VSpaceDimensions.GetBinomialCoefficient(2);

            var indexScalarDictionary =
                Enumerable
                    .Range(0, (int)kvSpaceDimensions)
                    .Select(index => 
                        new KeyValuePair<IIndexSet, double>(
                            index.BasisBivectorIndexToVectorIndexInt32Pair().IndexPairToIndexSet(),
                            GetScalarValue()
                        )
                    );

            return Processor
                .CreateComposer()
                .SetTerms(indexScalarDictionary)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Bivector GetBivector(double minValue, double maxValue)
        {
            var kvSpaceDimensions =
                VSpaceDimensions.GetBinomialCoefficient(2);

            var indexScalarDictionary =
                Enumerable
                    .Range(0, (int)kvSpaceDimensions)
                    .Select(index => 
                        new KeyValuePair<IIndexSet, double>(
                            index.BasisBivectorIndexToVectorIndexInt32Pair().IndexPairToIndexSet(),
                            GetScalarValue(minValue, maxValue)
                        )
                    );

            return Processor
                .CreateComposer()
                .SetTerms(indexScalarDictionary)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Bivector GetSparseBivector(int termsCount)
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
                        new KeyValuePair<IIndexSet, double>(
                            index.BasisBivectorIndexToVectorIndexInt32Pair().IndexPairToIndexSet(), 
                            GetScalarValue()
                        )
                    );

            return Processor
                .CreateComposer()
                .SetTerms(indexScalarDictionary)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Bivector GetSparseBivector(int termsCount, double minValue, double maxValue)
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
                        new KeyValuePair<IIndexSet, double>(
                            index.BasisBivectorIndexToVectorIndexInt32Pair().IndexPairToIndexSet(), 
                            GetScalarValue(minValue, maxValue)
                        )
                    );

            return Processor
                .CreateComposer()
                .SetTerms(indexScalarDictionary)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64KVector GetKVector()
        {
            return GetKVectorOfGrade(GetGrade());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64KVector GetKVector(double minValue, double maxValue)
        {
            return GetKVectorOfGrade(GetGrade(), minValue, maxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64KVector GetKVectorOfGrade(int grade)
        {
            var kvSpaceDimensions =
                VSpaceDimensions.GetBinomialCoefficient(grade);

            var indexScalarDictionary =
                Enumerable
                    .Range(0, (int)kvSpaceDimensions)
                    .ToDictionary(
                        index => BasisBladeUtils.BasisBladeGradeIndexToId((uint)grade, (ulong)index).BitPatternToIndexSet(),
                        _ => GetScalarValue()
                    );

            return Processor.CreateKVector(grade, indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64KVector GetKVectorOfGrade(int grade, double minValue, double maxValue)
        {
            var kvSpaceDimensions =
                VSpaceDimensions.GetBinomialCoefficient(grade);

            var indexScalarDictionary =
                Enumerable
                    .Range(0, (int)kvSpaceDimensions)
                    .ToDictionary(
                        index => BasisBladeUtils.BasisBladeGradeIndexToId((uint)grade, (ulong)index).BitPatternToIndexSet(),
                        _ => GetScalarValue(minValue, maxValue)
                    );

            return Processor.CreateKVector(grade, indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64KVector GetSparseKVectorOfGrade(int grade, int termsCount)
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
                    .ToDictionary(
                        index => BasisBladeUtils.BasisBladeGradeIndexToId((uint)grade, (ulong)index).BitPatternToIndexSet(),
                        _ => GetScalarValue()
                    );

            return Processor.CreateKVector(grade, indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64KVector GetSparseKVectorOfGrade(int grade, int termsCount, double minValue, double maxValue)
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
                    .ToDictionary(
                        index => BasisBladeUtils.BasisBladeGradeIndexToId((uint)grade, (ulong)index).BitPatternToIndexSet(),
                        _ => GetScalarValue(minValue, maxValue)
                    );

            return Processor.CreateKVector(grade, indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64GradedMultivector GetMultivector()
        {
            var gaSpaceDimensions = GaSpaceDimensions;

            var idScalarDictionary =
                Enumerable
                    .Range(0, gaSpaceDimensions)
                    .Select(index => 
                        new KeyValuePair<IIndexSet, double>(
                            ((ulong)index).BitPatternToIndexSet(),
                            GetScalarValue()
                        )
                    );

            return Processor
                .CreateComposer()
                .SetTerms(idScalarDictionary)
                .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64GradedMultivector GetMultivector(double minValue, double maxValue)
        {
            var gaSpaceDimensions = GaSpaceDimensions;
            
            var idScalarDictionary =
                Enumerable
                    .Range(0, gaSpaceDimensions)
                    .Select(index => 
                        new KeyValuePair<IIndexSet, double>(
                            ((ulong)index).BitPatternToIndexSet(),
                            GetScalarValue(minValue, maxValue)
                        )
                    );

            return Processor
                .CreateComposer()
                .SetTerms(idScalarDictionary)
                .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64GradedMultivector GetMultivector(int termsCount)
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
                        new KeyValuePair<IIndexSet, double>(
                            ((ulong)index).BitPatternToIndexSet(),
                            GetScalarValue()
                        )
                    );

            return Processor
                .CreateComposer()
                .SetTerms(idScalarDictionary)
                .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64GradedMultivector GetMultivector(int termsCount, double minValue, double maxValue)
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
                        new KeyValuePair<IIndexSet, double>(
                            ((ulong)index).BitPatternToIndexSet(),
                            GetScalarValue(minValue, maxValue)
                        )
                    );

            return Processor
                .CreateComposer()
                .SetTerms(idScalarDictionary)
                .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<XGaFloat64Vector> GetVectors(int count)
        {
            while (count > 0)
            {
                yield return GetVector();
                count--;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64KVector GetBlade(int grade)
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