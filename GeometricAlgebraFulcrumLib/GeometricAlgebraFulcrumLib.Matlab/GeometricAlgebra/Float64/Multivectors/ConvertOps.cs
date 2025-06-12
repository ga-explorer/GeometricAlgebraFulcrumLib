using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Combibnations;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors
{
    public abstract partial class XGaFloat64Multivector
    {
        /// <summary>
        /// Simplify the storage of this multivector
        /// </summary>
        /// <returns></returns>
        public abstract XGaFloat64Multivector Simplify();

        
        public XGaFloat64Multivector RemoveSmallTerms(double zeroEpsilon = Float64Utils.ZeroEpsilon)
        {
            return this switch
            {
                XGaFloat64Scalar s => s,
                XGaFloat64Vector v => v.RemoveSmallTerms(zeroEpsilon),
                XGaFloat64Bivector bv => bv.RemoveSmallTerms(zeroEpsilon),
                XGaFloat64HigherKVector kv => kv.RemoveSmallTerms(zeroEpsilon),
                XGaFloat64GradedMultivector mv1 => mv1.RemoveSmallTerms(zeroEpsilon),
                XGaFloat64UniformMultivector mv1 => mv1.RemoveSmallTerms(zeroEpsilon),
                _ => throw new InvalidOperationException()
            };
        }


        
        public virtual XGaFloat64Multivector MapScalars(Func<double, double> scalarMapping)
        {
            return this switch
            {
                XGaFloat64Scalar s => s.MapScalar(scalarMapping),
                XGaFloat64Vector v => v.MapScalars(scalarMapping),
                XGaFloat64Bivector bv => bv.MapScalars(scalarMapping),
                XGaFloat64HigherKVector kv => kv.MapScalars(scalarMapping),
                XGaFloat64GradedMultivector mv1 => mv1.MapScalars(scalarMapping),

                _ => Processor
                    .CreateMultivectorComposer()
                    .AddTerms(
                        IdScalarPairs.Select(
                            term => new KeyValuePair<IndexSet, double>(
                                term.Key,
                                scalarMapping(term.Value)
                            )
                        )
                    ).GetMultivector()
            };
        }

        
        public virtual XGaFloat64Multivector MapScalars(Func<IndexSet, double, double> scalarMapping)
        {
            return this switch
            {
                XGaFloat64Scalar s => s.MapScalar(scalarMapping),
                XGaFloat64Vector v => v.MapScalars(scalarMapping),
                XGaFloat64Bivector bv => bv.MapScalars(scalarMapping),
                XGaFloat64HigherKVector kv => kv.MapScalars(scalarMapping),
                XGaFloat64GradedMultivector mv1 => mv1.MapScalars(scalarMapping),

                _ => Processor
                    .CreateMultivectorComposer()
                    .AddTerms(
                        IdScalarPairs.Select(
                            term => new KeyValuePair<IndexSet, double>(
                                term.Key,
                                scalarMapping(term.Key, term.Value)
                            )
                        )
                    ).GetMultivector()
            };
        }


        
        public XGaFloat64Multivector MapBasisBlades(Func<IndexSet, IndexSet> basisMapping)
        {
            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, double>(
                        basisMapping(term.Key),
                        term.Value
                    )
                );

            return Processor
                .CreateMultivectorComposer()
                .AddTerms(termList)
                .GetMultivector();
        }

        
        public XGaFloat64Multivector MapBasisBlades(Func<IndexSet, double, IndexSet> basisMapping)
        {
            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, double>(
                        basisMapping(term.Key, term.Value),
                        term.Value
                    )
                );

            return Processor
                .CreateMultivectorComposer()
                .AddTerms(termList)
                .GetMultivector();
        }


        
        public XGaFloat64Multivector MapTerms(Func<IndexSet, double, KeyValuePair<IndexSet, double>> termMapping)
        {
            var termList =
                IdScalarPairs.Select(
                    term =>
                        termMapping(term.Key, term.Value)
                );

            return Processor
                .CreateMultivectorComposer()
                .AddTerms(termList)
                .GetMultivector();
        }
        

        
        public XGaFloat64Scalar AsScalar()
        {
            return (XGaFloat64Scalar)this;
        }

        
        public XGaFloat64Vector AsVector()
        {
            return (XGaFloat64Vector)this;
        }

        
        public XGaFloat64Bivector AsBivector()
        {
            return (XGaFloat64Bivector)this;
        }

        
        public XGaFloat64HigherKVector AsHigherKVector()
        {
            return (XGaFloat64HigherKVector)this;
        }

        
        public XGaFloat64KVector AsKVector()
        {
            return (XGaFloat64KVector)this;
        }

        
        public XGaFloat64GradedMultivector AsGradedMultivector()
        {
            return (XGaFloat64GradedMultivector)this;
        }

        
        public XGaFloat64UniformMultivector AsUniformMultivector()
        {
            return (XGaFloat64UniformMultivector)this;
        }

        
        public LinFloat64Vector MultivectorToLinVector()
        {
            var indexScalarDictionary =
                IdScalarPairs.ToDictionary(
                    p => p.Key.DecodeCombinadicToInt32(),
                    p => p.Value
                );

            return indexScalarDictionary.CreateLinVector();
        }


        public XGaFloat64GradedMultivector ToGradedMultivector()
        {
            return IsZero
                ? Processor.GradedMultivectorZero
                : this switch
                {
                    XGaFloat64GradedMultivector gmv => gmv,

                    XGaFloat64Scalar s => Processor.GradedMultivector(
                        IndexSet.EmptySet, 
                        s.ScalarValue
                    ),

                    XGaFloat64KVector kv => new XGaFloat64GradedMultivector(
                        Processor,
                        new SingleItemDictionary<int, XGaFloat64KVector>(kv.Grade, kv)
                    ),

                    _ => Processor
                        .CreateMultivectorComposer()
                        .SetMultivector(this)
                        .GetGradedMultivector()
                };
        }
    
        public XGaFloat64UniformMultivector ToUniformMultivector()
        {
            return IsZero
                ? Processor.UniformMultivectorZero
                : this switch
                {
                    XGaFloat64UniformMultivector umv => umv,

                    XGaFloat64Scalar s => Processor.UniformMultivector(
                        IndexSet.EmptySet, 
                        s.ScalarValue
                    ),

                    _ => Processor
                        .CreateUniformComposer()
                        .SetMultivector(this)
                        .GetUniformMultivector()
                };
        }


        
        public LinFloat64Vector2D VectorPartToVector2D()
        {
            return LinFloat64Vector2D.Create(Scalar(0), Scalar(1));
        }

        
        public LinFloat64Vector3D VectorPartToVector3D()
        {
            return LinFloat64Vector3D.Create(Scalar(0), Scalar(1), Scalar(2));
        }

        
        public LinFloat64Vector4D VectorPartToVector4D()
        {
            return LinFloat64Vector4D.Create(Scalar(0), Scalar(1), Scalar(2), Scalar(3));
        }


        public double[] MultivectorToArray1D()
        {
            if (VSpaceDimensions > 31)
                throw new InvalidOperationException();

            return MultivectorToArray1D(1 << VSpaceDimensions);
        }

        public double[] MultivectorToArray1D(int arraySize)
        {
            if (VSpaceDimensions > 31 || arraySize < (1 << VSpaceDimensions))
                throw new InvalidOperationException();

            var array = new double[arraySize];

            foreach (var (index, scalar) in GetMultivectorArrayItems().ToTuples())
                array[index] = scalar;

            return array;
        }

        public double[,] ScalarPlusBivectorToArray2D()
        {
            var array = GetBivectorPart().BivectorToArray2D();
            var scalar = Scalar();

            var arraySize = array.GetLength(0);
            for (var i = 0; i < arraySize; i++)
            {
                var signature = Processor.Signature(i);

                if (signature.IsZero) continue;

                array[i, i] = signature.IsPositive
                    ? scalar
                    : -scalar;
            }

            return array;
        }

        public double[,] ScalarPlusBivectorToArray2D(int arraySize)
        {
            var array = GetBivectorPart().BivectorToArray2D(arraySize);
            var scalar = Scalar();

            for (var i = 0; i < arraySize; i++)
            {
                var signature = Processor.Signature(i);

                if (signature.IsZero) continue;

                array[i, i] = signature.IsPositive
                    ? scalar
                    : -scalar;
            }

            return array;
        }

    }

    public abstract partial class XGaFloat64KVector
    {
        
        public new XGaFloat64KVector MapScalars(Func<double, double> scalarMapping)
        {
            return this switch
            {
                XGaFloat64Scalar s => s.MapScalar(scalarMapping),
                XGaFloat64Vector v => v.MapScalars(scalarMapping),
                XGaFloat64Bivector bv => bv.MapScalars(scalarMapping),
                XGaFloat64HigherKVector kv => kv.MapScalars(scalarMapping),
                _ => throw new InvalidOperationException()
            };
        }

        
        public new XGaFloat64KVector MapScalars(Func<IndexSet, double, double> scalarMapping)
        {
            return this switch
            {
                XGaFloat64Scalar s => s.MapScalar(scalarMapping),
                XGaFloat64Vector v => v.MapScalars(scalarMapping),
                XGaFloat64Bivector bv => bv.MapScalars(scalarMapping),
                XGaFloat64HigherKVector kv => kv.MapScalars(scalarMapping),
                _ => throw new InvalidOperationException()
            };
        }


        

        public double[] KVectorToArray1D()
        {
            return KVectorToArray1D(
                (int)VSpaceDimensions.GetBinomialCoefficient(Grade)
            );
        }

        public double[] KVectorToArray1D(int arraySize)
        {
            var kvSpaceDimensions =
                (int)VSpaceDimensions.GetBinomialCoefficient(Grade);

            if (arraySize < kvSpaceDimensions)
                throw new InvalidOperationException();

            var array = new double[arraySize];

            foreach (var (index, scalar) in GetKVectorArrayItems().ToTuples())
                array[index] = scalar;

            return array;
        }
    }

    public sealed partial class XGaFloat64Scalar
    {
        
        public override XGaFloat64Multivector Simplify()
        {
            return this;
        }


        
        public XGaFloat64Scalar MapScalar(Func<double, double> scalarMapping)
        {
            return IsZero
                ? this
                : new XGaFloat64Scalar(
                    Processor,
                    scalarMapping(ScalarValue)
                );
        }

        
        public XGaFloat64Scalar MapScalar(Func<IndexSet, double, double> scalarMapping)
        {
            return IsZero
                ? this
                : new XGaFloat64Scalar(
                    Processor,
                    scalarMapping(
                        Metric.GetBasisScalarId(),
                        ScalarValue
                    )
                );
        }
        

        
        public double ToScalar()
        {
            return _scalar;
        }

    }

    public sealed partial class XGaFloat64Vector
    {
        
        public override XGaFloat64Multivector Simplify()
        {
            return IsZero
                ? Processor.ScalarZero
                : this;
        }
        
        
        public new XGaFloat64Vector RemoveSmallTerms(double zeroEpsilon = Float64Utils.ZeroEpsilon)
        {
            if (Count <= 1) return this;

            var scalarThreshold =
                zeroEpsilon.Abs() * Scalars.Max(s => s.Abs());

            return GetPart(s =>
                s <= -scalarThreshold || s >= scalarThreshold
            );
        }


        
        public new XGaFloat64Vector MapScalars(Func<double, double> scalarMapping)
        {
            if (IsZero) return this;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, double>(
                        term.Key,
                        scalarMapping(term.Value)
                    )
                );

            return Processor
                .CreateVectorComposer()
                .SetTerms(termList)
                .GetVector();
        }

        
        public new XGaFloat64Vector MapScalars(Func<IndexSet, double, double> scalarMapping)
        {
            if (IsZero) return this;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, double>(
                        term.Key,
                        scalarMapping(term.Key, term.Value)
                    )
                );

            return Processor
                .CreateVectorComposer()
                .SetTerms(termList)
                .GetVector();
        }

        
        public XGaFloat64Vector MapScalars(Func<int, double, double> scalarMapping)
        {
            if (IsZero) return this;

            var termList =
                IdScalarPairs
                    .Where(term => term.Key.Count == 1)
                    .Select(
                        term => new KeyValuePair<IndexSet, double>(
                            term.Key,
                            scalarMapping(term.Key.FirstIndex, term.Value)
                        )
                    );

            return Processor
                .CreateVectorComposer()
                .SetTerms(termList)
                .GetVector();
        }

        
        public XGaFloat64Vector MapBasisVectors(Func<int, int> basisMapping)
        {
            if (IsZero) return this;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, double>(
                        basisMapping(term.Key.FirstIndex).ToUnitIndexSet(),
                        term.Value
                    )
                );

            return Processor
                .CreateVectorComposer()
                .AddTerms(termList)
                .GetVector();
        }

        
        public XGaFloat64Vector MapBasisVectors(Func<int, double, int> basisMapping)
        {
            if (IsZero) return this;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, double>(
                        basisMapping(term.Key.FirstIndex, term.Value).ToUnitIndexSet(),
                        term.Value
                    )
                );

            return Processor
                .CreateVectorComposer()
                .AddTerms(termList)
                .GetVector();
        }

        
        public XGaFloat64Vector MapTerms(Func<int, double, KeyValuePair<int, double>> termMapping)
        {
            if (IsZero) return this;

            var termList =
                IdScalarPairs.Select(
                    term =>
                    {
                        var (index, scalar) = termMapping(term.Key.FirstIndex, term.Value).ToTuple();

                        return new KeyValuePair<IndexSet, double>(
                            index.ToUnitIndexSet(),
                            scalar
                        );
                    }
                );

            return Processor
                .CreateVectorComposer()
                .AddTerms(termList)
                .GetVector();
        }
        

        
        public double[] VectorToArray1D()
        {
            return VectorToArray1D(VSpaceDimensions);
        }
        
        public double[] VectorToArray1D(int arraySize)
        {
            if (arraySize < VSpaceDimensions)
                throw new InvalidOperationException();

            var array = new double[arraySize];

            foreach (var (id, scalar) in IdScalarTuples)
                array[id.FirstIndex] = scalar;

            return array;
        }

        
        public double[,] VectorToRowArray2D(int arraySize)
        {
            if (arraySize < VSpaceDimensions)
                throw new InvalidOperationException();

            var array = new double[1, arraySize];

            foreach (var (id, scalar) in IdScalarTuples)
                array[0, id.FirstIndex] = scalar;

            return array;
        }

        
        public double[,] VectorToColumnArray2D(int arraySize)
        {
            if (arraySize < VSpaceDimensions)
                throw new InvalidOperationException();

            var array = new double[arraySize, 1];

            foreach (var (id, scalar) in IdScalarTuples)
                array[id.FirstIndex, 0] = scalar;

            return array;
        }


        
        public LinFloat64Vector2D ToVector2D()
        {
            return LinFloat64Vector2D.Create(
                Scalar(0),
                Scalar(1)
            );
        }

        
        public LinFloat64Vector3D ToVector3D()
        {
            return LinFloat64Vector3D.Create(
                Scalar(0),
                Scalar(1),
                Scalar(2)
            );
        }

        
        public LinFloat64Vector ToLinVector()
        {
            var indexScalarDictionary = this.ToDictionary(
                p => p.Key.FirstIndex,
                p => p.Value
            );

            return indexScalarDictionary.CreateLinVector();
        }

        
    }

    public sealed partial class XGaFloat64Bivector
    {
        
        public override XGaFloat64Multivector Simplify()
        {
            return IsZero
                ? Processor.ScalarZero
                : this;
        }

        
        public new XGaFloat64Bivector RemoveSmallTerms(double zeroEpsilon = Float64Utils.ZeroEpsilon)
        {
            if (Count <= 1) return this;

            var scalarThreshold =
                zeroEpsilon.Abs() * Scalars.Max(s => s.Abs());

            return GetPart(s =>
                s <= -scalarThreshold || s >= scalarThreshold
            );
        }


        
        public new XGaFloat64Bivector MapScalars(Func<double, double> scalarMapping)
        {
            if (IsZero) return this;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, double>(
                        term.Key,
                        scalarMapping(term.Value)
                    )
                );

            return Processor
                .CreateBivectorComposer()
                .SetTerms(termList)
                .GetBivector();
        }

        
        public new XGaFloat64Bivector MapScalars(Func<IndexSet, double, double> scalarMapping)
        {
            if (IsZero) return this;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, double>(
                        term.Key,
                        scalarMapping(term.Key, term.Value)
                    )
                );

            return Processor
                .CreateBivectorComposer()
                .SetTerms(termList)
                .GetBivector();
        }

        
        public XGaFloat64Bivector MapScalars(Func<int, int, double, double> scalarMapping)
        {
            if (IsZero) return this;

            var termList =
                IdScalarPairs
                    .Where(term => term.Key.Count == 1)
                    .Select(
                        term => new KeyValuePair<IndexSet, double>(
                            term.Key,
                            scalarMapping(term.Key.FirstIndex, term.Key.LastIndex, term.Value)
                        )
                    );

            return Processor
                .CreateBivectorComposer()
                .SetTerms(termList)
                .GetBivector();
        }

        
        public XGaFloat64Bivector MapBasisBivectors(Func<int, int, IPair<int>> basisMapping)
        {
            if (IsZero) return this;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, double>(
                        basisMapping(term.Key.FirstIndex, term.Key.LastIndex).ToPairIndexSet(),
                        term.Value
                    )
                );

            return Processor
                .CreateBivectorComposer()
                .AddTerms(termList)
                .GetBivector();
        }

        
        public XGaFloat64Bivector MapBasisBivectors(Func<int, int, double, IPair<int>> basisMapping)
        {
            if (IsZero) return this;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, double>(
                        basisMapping(term.Key.FirstIndex, term.Key.LastIndex, term.Value).ToPairIndexSet(),
                        term.Value
                    )
                );

            return Processor
                .CreateBivectorComposer()
                .AddTerms(termList)
                .GetBivector();
        }

        
        public XGaFloat64Bivector MapTerms(Func<int, int, double, KeyValuePair<IPair<int>, double>> termMapping)
        {
            if (IsZero) return this;

            var termList =
                IdScalarPairs.Select(
                    term =>
                    {
                        var (indexPair, scalar) = termMapping(
                            term.Key.FirstIndex,
                            term.Key.LastIndex,
                            term.Value
                        ).ToTuple();

                        return new KeyValuePair<IndexSet, double>(
                            indexPair.ToPairIndexSet(),
                            scalar
                        );
                    }
                );

            return Processor
                .CreateBivectorComposer()
                .AddTerms(termList)
                .GetBivector();
        }



        public double[] BivectorToArray1D()
        {
            return BivectorToArray1D((int) KvSpaceDimensions);
        }

        public double[] BivectorToArray1D(int arraySize)
        {
            if ((ulong)arraySize < KvSpaceDimensions)
                throw new InvalidOperationException();

            var array = new double[arraySize];

            foreach (var (id, scalar) in IdScalarTuples)
            {
                var index1 = id.FirstIndex;
                var index2 = id.LastIndex;

                var index = (int)BinaryCombinationsUtilsUInt64.CombinadicToIndex(index1, index2);

                array[index] = scalar;
            }

            return array;
        }

        public double[,] BivectorToArray2D()
        {
            return BivectorToArray2D((int) KvSpaceDimensions);
        }

        public double[,] BivectorToArray2D(int arraySize)
        {
            if ((ulong)arraySize < KvSpaceDimensions)
                throw new InvalidOperationException();

            var array = new double[arraySize, arraySize];

            foreach (var (id, scalar) in IdScalarTuples)
            {
                var index1 = id.FirstIndex;
                var index2 = id.LastIndex;

                array[index1, index2] = scalar;
                array[index2, index1] = -scalar;
            }

            return array;
        }
    }

    public sealed partial class XGaFloat64HigherKVector
    {
        
        public override XGaFloat64Multivector Simplify()
        {
            return IsZero
                ? Processor.ScalarZero
                : this;
        }
        
        
        public new XGaFloat64HigherKVector RemoveSmallTerms(double zeroEpsilon = Float64Utils.ZeroEpsilon)
        {
            if (Count <= 1) return this;

            var scalarThreshold =
                zeroEpsilon.Abs() * Scalars.Max(s => s.Abs());

            return GetPart(s =>
                s <= -scalarThreshold || s >= scalarThreshold
            );
        }

        
        public new XGaFloat64HigherKVector MapScalars(Func<double, double> scalarMapping)
        {
            if (IsZero) return this;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, double>(
                        term.Key,
                        scalarMapping(term.Value)
                    )
                );

            return Processor
                .CreateKVectorComposer(Grade)
                .SetTerms(termList)
                .GetHigherKVector();
        }

        
        public new XGaFloat64HigherKVector MapScalars(Func<IndexSet, double, double> scalarMapping)
        {
            if (IsZero) return this;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, double>(
                        term.Key,
                        scalarMapping(term.Key, term.Value)
                    )
                );

            return Processor
                .CreateKVectorComposer(Grade)
                .SetTerms(termList)
                .GetHigherKVector();
        }
    }

    public sealed partial class XGaFloat64GradedMultivector
    {
        
        public override XGaFloat64Multivector Simplify()
        {
            return KVectorCount switch
            {
                0 => Processor.ScalarZero,
                1 => _gradeKVectorDictionary.Values.First().Simplify(),
                _ => this
            };
        }
        
        
        public new XGaFloat64GradedMultivector RemoveSmallTerms(double zeroEpsilon = Float64Utils.ZeroEpsilon)
        {
            if (Count <= 1) return this;

            var scalarThreshold = 
                zeroEpsilon.Abs() * Scalars.Max(s => s.Abs());

            return GetPart(s => 
                s <= -scalarThreshold || s >= scalarThreshold
            );
        }


        
        public override XGaFloat64Multivector MapScalars(Func<double, double> scalarMapping)
        {
            if (_gradeKVectorDictionary.Count == 1)
                return _gradeKVectorDictionary
                    .Values
                    .First()
                    .MapScalars(scalarMapping);

            return IsZero
                ? this
                : MapKVectorsSimplify(kv => kv.MapScalars(scalarMapping));
        }

        
        public override XGaFloat64Multivector MapScalars(Func<IndexSet, double, double> scalarMapping)
        {
            if (_gradeKVectorDictionary.Count == 1)
                return _gradeKVectorDictionary
                    .Values
                    .First()
                    .MapScalars(scalarMapping);

            return IsZero
                ? Processor.ScalarZero
                : MapKVectorsSimplify(kv => kv.MapScalars(scalarMapping));
        }


        
        public IEnumerable<XGaFloat64KVector> MapKVectorPairs(IEnumerable<XGaFloat64KVector> mv2, Func<XGaFloat64KVector, XGaFloat64KVector, XGaFloat64KVector> kVectorMapping)
        {
            foreach (var kv2 in mv2)
                foreach (var kv1 in _gradeKVectorDictionary.Values)
                    yield return kVectorMapping(kv1, kv2);
        }

        
        public IEnumerable<XGaFloat64KVector> MapKVectorPairs(IEnumerable<XGaFloat64KVector> mv2, Func<XGaFloat64KVector, XGaFloat64KVector, bool> pairFilter, Func<XGaFloat64KVector, XGaFloat64KVector, XGaFloat64KVector> kVectorMapping)
        {
            foreach (var kv2 in mv2)
                foreach (var kv1 in _gradeKVectorDictionary.Values)
                    if (pairFilter(kv1, kv2))
                        yield return kVectorMapping(kv1, kv2);
        }


        
        public XGaFloat64GradedMultivector MapKVectors(IEnumerable<XGaFloat64KVector> mv2, Func<XGaFloat64KVector, XGaFloat64KVector, XGaFloat64KVector> kVectorMapping)
        {
            return Processor.GradedMultivectorFromSum(
                MapKVectorPairs(mv2, kVectorMapping)
            );
        }

        
        public XGaFloat64GradedMultivector MapKVectors(IEnumerable<XGaFloat64KVector> mv2, Func<XGaFloat64KVector, XGaFloat64KVector, bool> pairFilter, Func<XGaFloat64KVector, XGaFloat64KVector, XGaFloat64KVector> kVectorMapping)
        {
            return Processor.GradedMultivectorFromSum(
                MapKVectorPairs(mv2, pairFilter, kVectorMapping)
            );
        }

        
        public XGaFloat64GradedMultivector MapKVectors(Func<XGaFloat64KVector, XGaFloat64KVector> kVectorMapping)
        {
            return Processor.GradedMultivectorFromSum(
                _gradeKVectorDictionary.Values.Select(kVectorMapping)
            );
        }

        
        public XGaFloat64GradedMultivector MapKVectors(Func<XGaFloat64KVector, bool> kVectorFilter, Func<XGaFloat64KVector, XGaFloat64KVector> kVectorMapping)
        {
            return Processor.GradedMultivectorFromSum(
                _gradeKVectorDictionary.Values.Where(kVectorFilter).Select(kVectorMapping)
            );
        }


        
        public XGaFloat64Multivector MapKVectorsSimplify(IEnumerable<XGaFloat64KVector> mv2, Func<XGaFloat64KVector, XGaFloat64KVector, XGaFloat64KVector> kVectorMapping)
        {
            return Processor.MultivectorFromSum(
                MapKVectorPairs(mv2, kVectorMapping)
            );
        }

        
        public XGaFloat64Multivector MapKVectorsSimplify(IEnumerable<XGaFloat64KVector> mv2, Func<XGaFloat64KVector, XGaFloat64KVector, bool> pairFilter, Func<XGaFloat64KVector, XGaFloat64KVector, XGaFloat64KVector> kVectorMapping)
        {
            return Processor.MultivectorFromSum(
                MapKVectorPairs(mv2, pairFilter, kVectorMapping)
            );
        }

        
        public XGaFloat64Multivector MapKVectorsSimplify(Func<XGaFloat64KVector, XGaFloat64KVector> kVectorMapping)
        {
            return Processor.MultivectorFromSum(
                _gradeKVectorDictionary.Values.Select(kVectorMapping)
            );
        }

        
        public XGaFloat64Multivector MapKVectorsSimplify(Func<XGaFloat64KVector, bool> kVectorFilter, Func<XGaFloat64KVector, XGaFloat64KVector> kVectorMapping)
        {
            return Processor.MultivectorFromSum(
                _gradeKVectorDictionary.Values.Where(kVectorFilter).Select(kVectorMapping)
            );
        }
    }

    public sealed partial class XGaFloat64UniformMultivector
    {
        
        public override XGaFloat64Multivector Simplify()
        {
            return this;
        }
        
        
        public new XGaFloat64UniformMultivector RemoveSmallTerms(double zeroEpsilon = Float64Utils.ZeroEpsilon)
        {
            if (Count <= 1) return this;

            var scalarThreshold = 
                zeroEpsilon.Abs() * Scalars.Max(s => s.Abs());

            return GetPart(s => 
                s <= -scalarThreshold || s >= scalarThreshold
            );
        }


        
        public new XGaFloat64UniformMultivector MapScalars(Func<double, double> scalarMapping)
        {
            if (IsZero)
                return this;

            var idScalarPairs =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, double>(
                        term.Key,
                        scalarMapping(term.Value)
                    )
                );

            return Processor
                .CreateMultivectorComposer()
                .AddTerms(idScalarPairs)
                .GetUniformMultivector();
        }

        
        public new XGaFloat64UniformMultivector MapScalars(Func<IndexSet, double, double> scalarMapping)
        {
            if (IsZero)
                return this;

            var idScalarPairs =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, double>(
                        term.Key,
                        scalarMapping(term.Key, term.Value)
                    )
                );

            return Processor
                .CreateMultivectorComposer()
                .AddTerms(idScalarPairs)
                .GetUniformMultivector();
        }

        
        public new XGaFloat64UniformMultivector MapBasisBlades(Func<IndexSet, IndexSet> basisMapping)
        {
            if (IsZero)
                return this;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, double>(
                        basisMapping(term.Key),
                        term.Value
                    )
                );

            return Processor
                .CreateMultivectorComposer()
                .AddTerms(termList)
                .GetUniformMultivector();
        }

        
        public new XGaFloat64UniformMultivector MapBasisBlades(Func<IndexSet, double, IndexSet> basisMapping)
        {
            if (IsZero)
                return this;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, double>(
                        basisMapping(term.Key, term.Value),
                        term.Value
                    )
                );

            return Processor
                .CreateMultivectorComposer()
                .SetTerms(termList)
                .GetUniformMultivector();
        }

        
        public new XGaFloat64UniformMultivector MapTerms(Func<IndexSet, double, KeyValuePair<IndexSet, double>> termMapping)
        {
            if (IsZero)
                return this;

            var termList =
                IdScalarPairs.Select(
                    term =>
                        termMapping(term.Key, term.Value)
                );

            return Processor
                .CreateMultivectorComposer()
                .SetTerms(termList)
                .GetUniformMultivector();
        }
    }
}
