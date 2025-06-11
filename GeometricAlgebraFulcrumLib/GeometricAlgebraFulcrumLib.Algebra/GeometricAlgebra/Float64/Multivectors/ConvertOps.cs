using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors
{
    public abstract partial class XGaFloat64Multivector
    {
        /// <summary>
        /// Simplify the storage of this multivector
        /// </summary>
        /// <returns></returns>
        public abstract XGaFloat64Multivector Simplify();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaFloat64Multivector RemoveSmallTerms(double zeroEpsilon = Float64Utils.ZeroEpsilon);
        //{
        //    return this switch
        //    {
        //        XGaFloat64Scalar s => s,
        //        XGaFloat64Vector v => v.RemoveSmallTerms(zeroEpsilon),
        //        XGaFloat64Bivector bv => bv.RemoveSmallTerms(zeroEpsilon),
        //        XGaFloat64HigherKVector kv => kv.RemoveSmallTerms(zeroEpsilon),
        //        XGaFloat64GradedMultivector mv1 => mv1.RemoveSmallTerms(zeroEpsilon),
        //        XGaFloat64UniformMultivector mv1 => mv1.RemoveSmallTerms(zeroEpsilon),
        //        _ => throw new InvalidOperationException()
        //    };
        //}


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
                    ).GetSimpleMultivector()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
                    ).GetSimpleMultivector()
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64Multivector MapBasisBlades(Func<IndexSet, IndexSet> basisMapping)
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
                .GetSimpleMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64Multivector MapBasisBlades(Func<IndexSet, double, IndexSet> basisMapping)
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
                .GetSimpleMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64Multivector MapTerms(Func<IndexSet, double, KeyValuePair<IndexSet, double>> termMapping)
        {
            var termList =
                IdScalarPairs.Select(
                    term =>
                        termMapping(term.Key, term.Value)
                );

            return Processor
                .CreateMultivectorComposer()
                .AddTerms(termList)
                .GetSimpleMultivector();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar AsScalar()
        {
            return (XGaFloat64Scalar)this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Vector AsVector()
        {
            return (XGaFloat64Vector)this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Bivector AsBivector()
        {
            return (XGaFloat64Bivector)this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64HigherKVector AsHigherKVector()
        {
            return (XGaFloat64HigherKVector)this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64KVector AsKVector()
        {
            return (XGaFloat64KVector)this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64GradedMultivector AsGradedMultivector()
        {
            return (XGaFloat64GradedMultivector)this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64UniformMultivector AsUniformMultivector()
        {
            return (XGaFloat64UniformMultivector)this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector MultivectorToLinVector()
        {
            var indexScalarDictionary =
                IdScalarPairs.ToDictionary(
                    p => p.Key.DecodeCombinadicToInt32(),
                    p => p.Value
                );

            return indexScalarDictionary.CreateLinVector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64GradedMultivectorComposer ToComposer()
        {
            return Processor.CreateMultivectorComposer().SetMultivector(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64GradedMultivectorComposer NegativeToComposer()
        {
            return Processor.CreateMultivectorComposer().SetMultivectorNegative(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaFloat64GradedMultivectorComposer ToComposer(double scalingFactor)
        {
            return Processor.CreateMultivectorComposer().SetMultivectorScaled(this, scalingFactor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaFloat64GradedMultivector ToGradedMultivector();
        //{
        //    return this switch
        //    {
        //        XGaFloat64KVector kVector => kVector.ToGradedMultivector(),
        //        XGaFloat64GradedMultivector mv => mv,
        //        XGaFloat64UniformMultivector mv => mv.ToGradedMultivector(),
        //        _ => throw new InvalidOperationException()
        //    };
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaFloat64UniformMultivector ToUniformMultivector();
        //{
        //    return this switch
        //    {
        //        XGaFloat64KVector kVector => kVector.ToUniformMultivector(),
        //        XGaFloat64UniformMultivector mv => mv,
        //        XGaFloat64GradedMultivector mv => mv.ToUniformMultivector(),
        //        _ => throw new InvalidOperationException()
        //    };
        //}


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector2D VectorPartToVector2D()
        {
            return LinFloat64Vector2D.Create(Scalar(0), Scalar(1));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector3D VectorPartToVector3D()
        {
            return LinFloat64Vector3D.Create(Scalar(0), Scalar(1), Scalar(2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector4D VectorPartToVector4D()
        {
            return LinFloat64Vector4D.Create(Scalar(0), Scalar(1), Scalar(2), Scalar(3));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

            foreach (var (index, scalar) in GetMultivectorArrayItems())
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector MapScalars(Func<double, double> scalarMapping)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector MapScalars(Func<IndexSet, double, double> scalarMapping)
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


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64GradedMultivector ToGradedMultivector()
        {
            if (IsZero)
                return Processor.GradedMultivectorZero;

            var gradeKVectorDictionary =
                new SingleItemDictionary<int, XGaFloat64KVector>(Grade, this);

            return new XGaFloat64GradedMultivector(Processor, gradeKVectorDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64UniformMultivector ToUniformMultivector()
        {
            return ToComposer().GetUniformMultivector();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[] KVectorToArray1D()
        {
            return KVectorToArray1D(
                (int)KvSpaceDimensions
            );
        }

        public double[] KVectorToArray1D(int arraySize)
        {
            if ((ulong)arraySize < KvSpaceDimensions)
                throw new InvalidOperationException();

            var array = new double[arraySize];

            foreach (var (index, scalar) in GetKVectorArrayItems())
                array[index] = scalar;

            return array;
        }
    }

    public sealed partial class XGaFloat64Scalar
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Simplify()
        {
            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar MapScalar(Func<double, double> scalarMapping)
        {
            return IsZero
                ? this
                : new XGaFloat64Scalar(
                    Processor,
                    scalarMapping(ScalarValue)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar ToScalar()
        {
            return Float64Scalar.Create(_scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64GradedMultivectorComposer ToComposer()
        {
            return Processor.CreateMultivectorComposer().SetScalarTerm(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64GradedMultivectorComposer NegativeToComposer()
        {
            return Processor.CreateMultivectorComposer().SetScalarTerm(-ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64GradedMultivectorComposer ToComposer(double scalingFactor)
        {
            return Processor.CreateMultivectorComposer().SetScalarTerm(ScalarValue * scalingFactor);
        }
    }

    public sealed partial class XGaFloat64Vector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Simplify()
        {
            return IsZero
                ? Processor.ScalarZero
                : this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Vector RemoveSmallTerms(double zeroEpsilon = Float64Utils.ZeroEpsilon)
        {
            if (Count <= 1) return this;

            var scalarThreshold =
                zeroEpsilon.Abs() * Scalars.Max(s => s.Abs());

            return GetPart(s =>
                s <= -scalarThreshold || s >= scalarThreshold
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Vector MapScalars(Func<double, double> scalarMapping)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Vector MapScalars(Func<IndexSet, double, double> scalarMapping)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Vector MapTerms(Func<int, double, KeyValuePair<int, double>> termMapping)
        {
            if (IsZero) return this;

            var termList =
                IdScalarPairs.Select(
                    term =>
                    {
                        var (index, scalar) = termMapping(term.Key.FirstIndex, term.Value);

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
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[] VectorToArray1D()
        {
            return VectorToArray1D(VSpaceDimensions);
        }
        
        public double[] VectorToArray1D(int arraySize)
        {
            if (arraySize < VSpaceDimensions)
                throw new InvalidOperationException();

            var array = new double[arraySize];

            foreach (var (id, scalar) in IdScalarPairs)
                array[id.FirstIndex] = scalar;

            return array;
        }
        
        public double[,] VectorToRowArray2D(int arraySize)
        {
            if (arraySize < VSpaceDimensions)
                throw new InvalidOperationException();

            var array = new double[1, arraySize];

            foreach (var (id, scalar) in IdScalarPairs)
                array[0, id.FirstIndex] = scalar;

            return array;
        }
        
        public double[,] VectorToColumnArray2D(int arraySize)
        {
            if (arraySize < VSpaceDimensions)
                throw new InvalidOperationException();

            var array = new double[arraySize, 1];

            foreach (var (id, scalar) in IdScalarPairs)
                array[id.FirstIndex, 0] = scalar;

            return array;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector2D ToVector2D()
        {
            return LinFloat64Vector2D.Create(
                (Float64Scalar)Scalar(0),
                (Float64Scalar)Scalar(1)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector3D ToVector3D()
        {
            return LinFloat64Vector3D.Create(
                Scalar(0),
                Scalar(1),
                Scalar(2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector ToLinVector()
        {
            var indexScalarDictionary = this.ToDictionary(
                p => p.Key.FirstIndex,
                p => p.Value
            );

            return indexScalarDictionary.CreateLinVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64GradedMultivector ToGradedMultivector()
        {
            if (IsZero)
                return Processor.GradedMultivectorZero;

            var gradeKVectorDictionary =
                new SingleItemDictionary<int, XGaFloat64KVector>(1, this);

            return new XGaFloat64GradedMultivector(
                Processor,
                gradeKVectorDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64UniformMultivector ToUniformMultivector()
        {
            return ToComposer().GetUniformMultivector();
        }


    }

    public sealed partial class XGaFloat64Bivector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Simplify()
        {
            return IsZero
                ? Processor.ScalarZero
                : this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Bivector RemoveSmallTerms(double zeroEpsilon = Float64Utils.ZeroEpsilon)
        {
            if (Count <= 1) return this;

            var scalarThreshold =
                zeroEpsilon.Abs() * Scalars.Max(s => s.Abs());

            return GetPart(s =>
                s <= -scalarThreshold || s >= scalarThreshold
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Bivector MapScalars(Func<double, double> scalarMapping)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Bivector MapScalars(Func<IndexSet, double, double> scalarMapping)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
                        );

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


        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[] BivectorToArray1D()
        {
            return BivectorToArray1D((int) KvSpaceDimensions);
        }

        public double[] BivectorToArray1D(int arraySize)
        {
            if ((ulong)arraySize < KvSpaceDimensions)
                throw new InvalidOperationException();

            var array = new double[arraySize];

            foreach (var (id, scalar) in IdScalarPairs)
            {
                var index1 = id.FirstIndex;
                var index2 = id.LastIndex;

                var index = (int)BinaryCombinationsUtilsUInt64.CombinadicToIndex(index1, index2);

                array[index] = scalar;
            }

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[,] BivectorToArray2D()
        {
            return BivectorToArray2D((int) KvSpaceDimensions);
        }

        public double[,] BivectorToArray2D(int arraySize)
        {
            if ((ulong)arraySize < KvSpaceDimensions)
                throw new InvalidOperationException();

            var array = new double[arraySize, arraySize];

            foreach (var (id, scalar) in IdScalarPairs)
            {
                var index1 = id.FirstIndex;
                var index2 = id.LastIndex;

                array[index1, index2] = scalar;
                array[index2, index1] = -scalar;
            }

            return array;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64GradedMultivector ToGradedMultivector()
        {
            if (IsZero)
                return Processor.GradedMultivectorZero;

            var gradeKVectorDictionary =
                new SingleItemDictionary<int, XGaFloat64KVector>(2, this);

            return new XGaFloat64GradedMultivector(Processor, gradeKVectorDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64UniformMultivector ToUniformMultivector()
        {
            return ToComposer().GetUniformMultivector();
        }
    }

    public sealed partial class XGaFloat64HigherKVector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Simplify()
        {
            return IsZero
                ? Processor.ScalarZero
                : this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64HigherKVector RemoveSmallTerms(double zeroEpsilon = Float64Utils.ZeroEpsilon)
        {
            if (Count <= 1) return this;

            var scalarThreshold =
                zeroEpsilon.Abs() * Scalars.Max(s => s.Abs());

            return GetPart(s =>
                s <= -scalarThreshold || s >= scalarThreshold
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64GradedMultivector ToGradedMultivector()
        {
            if (IsZero)
                return Processor.GradedMultivectorZero;

            var gradeKVectorDictionary =
                new SingleItemDictionary<int, XGaFloat64KVector>(Grade, this);

            return new XGaFloat64GradedMultivector(Processor, gradeKVectorDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64UniformMultivector ToUniformMultivector()
        {
            return ToComposer().GetUniformMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64HigherKVector MapScalars(Func<double, double> scalarMapping)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64HigherKVector MapScalars(Func<IndexSet, double, double> scalarMapping)
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Simplify()
        {
            return KVectorCount switch
            {
                0 => Processor.ScalarZero,
                1 => _gradeKVectorDictionary.Values.First().Simplify(),
                _ => this
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64GradedMultivector RemoveSmallTerms(double zeroEpsilon = Float64Utils.ZeroEpsilon)
        {
            if (Count <= 1) return this;

            var scalarThreshold = 
                zeroEpsilon.Abs() * Scalars.Max(s => s.Abs());

            return GetPart(s => 
                s <= -scalarThreshold || s >= scalarThreshold
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<XGaFloat64KVector> MapKVectorPairs(IEnumerable<XGaFloat64KVector> mv2, Func<XGaFloat64KVector, XGaFloat64KVector, XGaFloat64KVector> kVectorMapping)
        {
            foreach (var kv2 in mv2)
                foreach (var kv1 in _gradeKVectorDictionary.Values)
                    yield return kVectorMapping(kv1, kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<XGaFloat64KVector> MapKVectorPairs(IEnumerable<XGaFloat64KVector> mv2, Func<XGaFloat64KVector, XGaFloat64KVector, bool> pairFilter, Func<XGaFloat64KVector, XGaFloat64KVector, XGaFloat64KVector> kVectorMapping)
        {
            foreach (var kv2 in mv2)
                foreach (var kv1 in _gradeKVectorDictionary.Values)
                    if (pairFilter(kv1, kv2))
                        yield return kVectorMapping(kv1, kv2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64GradedMultivector MapKVectors(IEnumerable<XGaFloat64KVector> mv2, Func<XGaFloat64KVector, XGaFloat64KVector, XGaFloat64KVector> kVectorMapping)
        {
            return Processor.GradedMultivectorFromSum(
                MapKVectorPairs(mv2, kVectorMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64GradedMultivector MapKVectors(IEnumerable<XGaFloat64KVector> mv2, Func<XGaFloat64KVector, XGaFloat64KVector, bool> pairFilter, Func<XGaFloat64KVector, XGaFloat64KVector, XGaFloat64KVector> kVectorMapping)
        {
            return Processor.GradedMultivectorFromSum(
                MapKVectorPairs(mv2, pairFilter, kVectorMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64GradedMultivector MapKVectors(Func<XGaFloat64KVector, XGaFloat64KVector> kVectorMapping)
        {
            return Processor.GradedMultivectorFromSum(
                _gradeKVectorDictionary.Values.Select(kVectorMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64GradedMultivector MapKVectors(Func<XGaFloat64KVector, bool> kVectorFilter, Func<XGaFloat64KVector, XGaFloat64KVector> kVectorMapping)
        {
            return Processor.GradedMultivectorFromSum(
                _gradeKVectorDictionary.Values.Where(kVectorFilter).Select(kVectorMapping)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector MapKVectorsSimplify(IEnumerable<XGaFloat64KVector> mv2, Func<XGaFloat64KVector, XGaFloat64KVector, XGaFloat64KVector> kVectorMapping)
        {
            return Processor.MultivectorFromSum(
                MapKVectorPairs(mv2, kVectorMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector MapKVectorsSimplify(IEnumerable<XGaFloat64KVector> mv2, Func<XGaFloat64KVector, XGaFloat64KVector, bool> pairFilter, Func<XGaFloat64KVector, XGaFloat64KVector, XGaFloat64KVector> kVectorMapping)
        {
            return Processor.MultivectorFromSum(
                MapKVectorPairs(mv2, pairFilter, kVectorMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector MapKVectorsSimplify(Func<XGaFloat64KVector, XGaFloat64KVector> kVectorMapping)
        {
            return Processor.MultivectorFromSum(
                _gradeKVectorDictionary.Values.Select(kVectorMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector MapKVectorsSimplify(Func<XGaFloat64KVector, bool> kVectorFilter, Func<XGaFloat64KVector, XGaFloat64KVector> kVectorMapping)
        {
            return Processor.MultivectorFromSum(
                _gradeKVectorDictionary.Values.Where(kVectorFilter).Select(kVectorMapping)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64GradedMultivector ToGradedMultivector()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64UniformMultivector ToUniformMultivector()
        {
            return ToComposer().GetUniformMultivector();
        }
    }

    public sealed partial class XGaFloat64UniformMultivector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Simplify()
        {
            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64UniformMultivector RemoveSmallTerms(double zeroEpsilon = Float64Utils.ZeroEpsilon)
        {
            if (Count <= 1) return this;

            var scalarThreshold = 
                zeroEpsilon.Abs() * Scalars.Max(s => s.Abs());

            return GetPart(s => 
                s <= -scalarThreshold || s >= scalarThreshold
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64UniformMultivector MapScalars(Func<double, double> scalarMapping)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64UniformMultivector MapScalars(Func<IndexSet, double, double> scalarMapping)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64UniformMultivector MapBasisBlades(Func<IndexSet, IndexSet> basisMapping)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64UniformMultivector MapBasisBlades(Func<IndexSet, double, IndexSet> basisMapping)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64UniformMultivector MapTerms(Func<IndexSet, double, KeyValuePair<IndexSet, double>> termMapping)
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

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64GradedMultivector ToGradedMultivector()
        {
            return ToComposer().GetGradedMultivector();
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64UniformMultivector ToUniformMultivector()
        {
            return this;
        }
    }
}
