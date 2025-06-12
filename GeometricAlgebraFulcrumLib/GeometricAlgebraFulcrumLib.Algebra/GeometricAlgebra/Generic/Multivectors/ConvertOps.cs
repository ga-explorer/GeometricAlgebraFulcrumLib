using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.GuidedBinaryTraversal;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors
{
    public abstract partial class XGaMultivector<T>
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> AsScalar()
        {
            return (XGaScalar<T>)this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaVector<T> AsVector()
        {
            return (XGaVector<T>)this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBivector<T> AsBivector()
        {
            return (XGaBivector<T>)this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaHigherKVector<T> AsHigherKVector()
        {
            return (XGaHigherKVector<T>)this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaKVector<T> AsKVector()
        {
            return (XGaKVector<T>)this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivector<T> AsGradedMultivector()
        {
            return (XGaGradedMultivector<T>)this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivector<T> AsUniformMultivector()
        {
            return (XGaUniformMultivector<T>)this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVector<T> MultivectorToLinVector()
        {
            var indexScalarDictionary =
                IdScalarPairs.ToDictionary(
                    p => p.Key.DecodeCombinadicToInt32(),
                    p => p.Value
                );

            return ScalarProcessor.CreateLinVector(indexScalarDictionary);
        }



        /// <summary>
        /// Simplify the storage of this multivector
        /// </summary>
        /// <returns></returns>
        public abstract XGaMultivector<T> Simplify();

        /// <summary>
        /// Construct a binary tree representation of this storage
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGbtBinaryTree<T> GetBinaryTree(int treeDepth)
        {
            if (treeDepth < VSpaceDimensions)
                throw new InvalidOperationException();

            var dict =
                IdScalarPairs.ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value
                );

            return new XGaGbtBinaryTree<T>(treeDepth, dict);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IXGaGbtMultivectorStorageStack1<T> CreateGbtStack(int treeDepth, int capacity)
        {
            //return XGaGbtKVectorStorageStack1<T>.Create(capacity, treeDepth, this);
            //return XGaGbtMultivectorStorageGradedStack1<T>.Create(capacity, treeDepth, this);
            return XGaMultivectorGbtUniformStack1<T>.Create(
                capacity,
                treeDepth,
                this
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivector<T> ToGradedMultivector()
        {
            return IsZero
                ? Processor.GradedMultivectorZero
                : this switch
                {
                    XGaGradedMultivector<T> gmv => gmv,

                    XGaScalar<T> s => Processor.GradedMultivector(
                        IndexSet.EmptySet,
                        s.ScalarValue
                    ),

                    XGaKVector<T> kv => new XGaGradedMultivector<T>(
                        Processor,
                        new SingleItemDictionary<int, XGaKVector<T>>(kv.Grade, kv)
                    ),

                    _ => Processor
                        .CreateMultivectorComposer()
                        .SetMultivector(this)
                        .GetGradedMultivector()
                };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaUniformMultivector<T> ToUniformMultivector()
        {
            return IsZero
                ? Processor.UniformMultivectorZero
                : this switch
                {
                    XGaUniformMultivector<T> umv => umv,

                    XGaScalar<T> s => Processor.UniformMultivector(
                        IndexSet.EmptySet,
                        s.ScalarValue
                    ),

                    _ => Processor
                        .CreateUniformComposer()
                        .SetMultivector(this)
                        .GetUniformMultivector()
                };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaFloat64Multivector Convert(XGaFloat64Processor metric);
        //{
        //    return mv switch
        //    {
        //        XGaScalar<T> mv1 => mv1.Convert(metric),
        //        XGaVector<T> mv1 => mv1.Convert(metric),
        //        XGaBivector<T> mv1 => mv1.Convert(metric),
        //        XGaHigherKVector<T> mv1 => mv1.Convert(metric),
        //        XGaGradedMultivector<T> mv1 => mv1.Convert(metric),
        //        _ => ((XGaUniformMultivector<T>)mv).Convert(metric)
        //    };
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaFloat64Multivector Convert(XGaFloat64Processor metric, Func<T, double> scalarMapping);
        //{
        //    return mv switch
        //    {
        //        XGaScalar<T> mv1 => mv1.Convert(metric, scalarMapping),
        //        XGaVector<T> mv1 => mv1.Convert(metric, scalarMapping),
        //        XGaBivector<T> mv1 => mv1.Convert(metric, scalarMapping),
        //        XGaHigherKVector<T> mv1 => mv1.Convert(metric, scalarMapping),
        //        XGaGradedMultivector<T> mv1 => mv1.Convert(metric, scalarMapping),
        //        _ => ((XGaUniformMultivector<T>)mv).Convert(metric, scalarMapping)
        //    };
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaMultivector<T2> Convert<T2>(XGaProcessor<T2> metric, Func<T, T2> scalarMapping);
        //{
        //    return mv switch
        //    {
        //        XGaScalar<T> mv1 => mv1.Convert(metric, scalarMapping),
        //        XGaVector<T> mv1 => mv1.Convert(metric, scalarMapping),
        //        XGaBivector<T> mv1 => mv1.Convert(metric, scalarMapping),
        //        XGaHigherKVector<T> mv1 => mv1.Convert(metric, scalarMapping),
        //        XGaGradedMultivector<T> mv1 => mv1.Convert(metric, scalarMapping),
        //        _ => ((XGaUniformMultivector<T>)mv).Convert(metric, scalarMapping)
        //    };
        //}


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaMultivector<T> MapScalars(Func<T, T> scalarMapping);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaFloat64Multivector MapScalars(XGaFloat64Processor processor, Func<T, double> scalarMapping);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaMultivector<T2> MapScalars<T2>(XGaProcessor<T2> processor, Func<T, T2> scalarMapping);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaMultivector<T> MapScalars(Func<IndexSet, T, T> scalarMapping);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaFloat64Multivector MapScalars(XGaFloat64Processor processor, Func<IndexSet, T, double> scalarMapping);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaMultivector<T2> MapScalars<T2>(XGaProcessor<T2> processor, Func<IndexSet, T, T2> scalarMapping);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract XGaMultivector<T> MapScalars(ScalarTransformer<T> transformer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> MapBasisBlades(Func<IndexSet, IndexSet> basisMapping)
        {
            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, T>(
                        basisMapping(term.Key),
                        term.Value
                    )
                );

            return Processor
                .CreateMultivectorComposer()
                .AddTerms(termList)
                .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> MapBasisBlades(Func<IndexSet, T, IndexSet> basisMapping)
        {
            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, T>(
                        basisMapping(term.Key, term.Value),
                        term.Value
                    )
                );

            return Processor
                .CreateMultivectorComposer()
                .AddTerms(termList)
                .GetMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual XGaMultivector<T> MapTerms(Func<IndexSet, T, KeyValuePair<IndexSet, T>> termMapping)
        {
            var termList =
                IdScalarPairs.Select(
                    term =>
                        termMapping(term.Key, term.Value)
                ).Where(p => !ScalarProcessor.IsZero(p.Value));

            return Processor
                .CreateMultivectorComposer()
                .AddTerms(termList)
                .GetMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[] MultivectorToArray1D(int arraySize)
        {
            var vSpaceDimensions = VSpaceDimensions;

            if (vSpaceDimensions > 31)
                throw new InvalidOperationException();

            var gaSpaceDimensions = 1UL << vSpaceDimensions;

            if ((ulong)arraySize < gaSpaceDimensions)
                throw new InvalidOperationException();

            var array = ScalarProcessor
                .CreateArrayZero1D(arraySize);

            foreach (var (id, scalar) in IdScalarPairs)
                array[id.ToInt32()] = scalar;

            return array;
        }

        public T[,] ScalarPlusBivectorToArray2D()
        {
            var array = GetBivectorPart().BivectorToArray2D();
            var scalar = Scalar().ScalarValue;
            var metric = Metric;
            var scalarProcessor = ScalarProcessor;

            var arraySize = array.GetLength(0);
            for (var i = 0; i < arraySize; i++)
            {
                var signature = metric.Signature(i);

                if (signature.IsZero) continue;

                array[i, i] = signature.IsPositive
                    ? scalar
                    : scalarProcessor.Negative(scalar).ScalarValue;
            }

            return array;
        }

        public T[,] ScalarPlusBivectorToArray2D(int arraySize)
        {
            var array = GetBivectorPart().BivectorToArray2D(arraySize);
            var scalar = Scalar().ScalarValue;
            var metric = Metric;
            var scalarProcessor = ScalarProcessor;

            for (var i = 0; i < arraySize; i++)
            {
                var signature = metric.Signature(i);

                if (signature.IsZero) continue;

                array[i, i] = signature.IsPositive
                    ? scalar
                    : scalarProcessor.Negative(scalar).ScalarValue;
            }

            return array;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector2D VectorPartAsFloat64Vector2D()
        {
            return LinFloat64Vector2D.Create(
                Scalar(0).ToFloat64(),
                Scalar(1).ToFloat64()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector3D VectorPartAsFloat64Vector3D()
        {
            return LinFloat64Vector3D.Create(
                Scalar(0).ToFloat64(),
                Scalar(1).ToFloat64(),
                Scalar(2).ToFloat64()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector4D VectorPartAsFloat64Vector4D()
        {
            return LinFloat64Vector4D.Create(
                Scalar(0).ToFloat64(),
                Scalar(1).ToFloat64(),
                Scalar(2).ToFloat64(),
                Scalar(3).ToFloat64()
            );
        }

    }

    public abstract partial class XGaKVector<T>
    {

        /// <summary>
        /// Analyze this k-vector, assumed to be a blade, into a set of orthonormal
        /// vectors, where their outer product is equal to the original blade, up to
        /// a scalar factor
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<XGaVector<T>> BladeToVectors()
        {
            // Find basis blade with the largest scalar magnitude in the current blade
            var maxId = IndexSet.EmptySet;
            var maxScalar = ScalarProcessor.ZeroValue;

            foreach (var (id, scalar) in IdScalarPairs)
            {
                var scalar1 = ScalarProcessor.Abs(scalar).ScalarValue;

                if (!ScalarProcessor.Subtract(scalar1, maxScalar).IsPositive())
                    continue;

                maxId = id;
                maxScalar = scalar1;
            }

            var probeVectors =
                maxId
                    .Select(index => Processor.VectorTerm(index))
                    .ToImmutableArray();

            return BladeToVectors(probeVectors);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<XGaVector<T>> BladeToVectors(params int[] probeBasisVectorIndices)
        {
            var probeVectors =
                probeBasisVectorIndices
                    .Select(index => Processor.VectorTerm(index))
                    .ToImmutableArray();

            return BladeToVectors(probeVectors);
        }

        /// <summary>
        /// Analyze this k-vector, assumed to be a blade, into a set of orthonormal
        /// vectors, where their outer product is equal to the original blade, up to
        /// a scalar factor
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyList<XGaVector<T>> BladeToVectors(IEnumerable<int> probeBasisVectorIndices)
        {
            var probeVectors =
                probeBasisVectorIndices
                    .Select(index => Processor.VectorTerm(index))
                    .ToImmutableArray();

            return BladeToVectors(probeVectors);
        }

        public IReadOnlyList<XGaVector<T>> BladeToVectors(IReadOnlyList<XGaVector<T>> probeVectors)
        {
            if (IsZero || Grade == 0)
                return [];

            if (this is XGaVector<T> vectorBlade)
                return [vectorBlade];

            var vectorList = new List<XGaVector<T>>(Grade);

            // All computations are done assuming Euclidean space,
            // independent of the actual metric

            // Normalize the current blade
            var oldBlade = DivideByENorm();

            // Repeat until the current blade is a single vector
            var basisVectorIndex = 0;
            while (oldBlade.Grade > 1)
            {
                // Get the next significant basis vector in the original blade
                var basisVector = probeVectors[basisVectorIndex];

                // Get orthogonal complement of basis vector inside the current blade
                // This is the new smaller blade for the next iteration
                var newBlade =
                    basisVector.ELcp(oldBlade).DivideByENorm();

                if (newBlade.Grade == oldBlade.Grade)
                    continue;

                // Get the Un-Dual of the new blade inside the current blade
                // This is one vector of the required vectors
                var vector = newBlade.ELcp(oldBlade.EInverse()).GetVectorPart().DivideByENorm();

                vectorList.Add(vector);

                oldBlade = newBlade;
                basisVectorIndex++;
            }

            // Add the current blade, which is a single vector
            if (vectorList.Count < Grade)
                vectorList.Add(
                    oldBlade.GetVectorPart().DivideByENorm()
                );

            return vectorList;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector Convert(XGaFloat64Processor metric)
        {
            return this switch
            {
                XGaScalar<T> mv1 => mv1.Convert(metric),
                XGaVector<T> mv1 => mv1.Convert(metric),
                XGaBivector<T> mv1 => mv1.Convert(metric),
                _ => ((XGaHigherKVector<T>)this).Convert(metric)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector Convert(XGaFloat64Processor metric, Func<T, double> scalarMapping)
        {
            return this switch
            {
                XGaScalar<T> mv1 => mv1.Convert(metric, scalarMapping),
                XGaVector<T> mv1 => mv1.Convert(metric, scalarMapping),
                XGaBivector<T> mv1 => mv1.Convert(metric, scalarMapping),
                _ => ((XGaHigherKVector<T>)this).Convert(metric, scalarMapping)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T2> Convert<T2>(XGaProcessor<T2> metric, Func<T, T2> scalarMapping)
        {
            return this switch
            {
                XGaScalar<T> mv1 => mv1.Convert(metric, scalarMapping),
                XGaVector<T> mv1 => mv1.Convert(metric, scalarMapping),
                XGaBivector<T> mv1 => mv1.Convert(metric, scalarMapping),
                _ => ((XGaHigherKVector<T>)this).Convert(metric, scalarMapping)
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> MapScalars(Func<T, T> scalarMapping)
        {
            return this switch
            {
                XGaScalar<T> s => s.MapScalar(scalarMapping),
                XGaVector<T> v => v.MapScalars(scalarMapping),
                XGaBivector<T> bv => bv.MapScalars(scalarMapping),
                XGaHigherKVector<T> kv => kv.MapScalars(scalarMapping),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T2> MapScalars<T2>(XGaProcessor<T2> processor, Func<T, T2> scalarMapping)
        {
            return this switch
            {
                XGaScalar<T> s => s.MapScalar(processor, scalarMapping),
                XGaVector<T> v => v.MapScalars(processor, scalarMapping),
                XGaBivector<T> bv => bv.MapScalars(processor, scalarMapping),
                XGaHigherKVector<T> kv => kv.MapScalars(processor, scalarMapping),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector MapScalars(XGaFloat64Processor processor, Func<T, double> scalarMapping)
        {
            return this switch
            {
                XGaScalar<T> s => s.MapScalar(processor, scalarMapping),
                XGaVector<T> v => v.MapScalars(processor, scalarMapping),
                XGaBivector<T> bv => bv.MapScalars(processor, scalarMapping),
                XGaHigherKVector<T> kv => kv.MapScalars(processor, scalarMapping),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> MapScalars(Func<IndexSet, T, T> scalarMapping)
        {
            return this switch
            {
                XGaScalar<T> s => s.MapScalar(scalarMapping),
                XGaVector<T> v => v.MapScalars(scalarMapping),
                XGaBivector<T> bv => bv.MapScalars(scalarMapping),
                XGaHigherKVector<T> kv => kv.MapScalars(scalarMapping),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T2> MapScalars<T2>(XGaProcessor<T2> processor, Func<IndexSet, T, T2> scalarMapping)
        {
            return this switch
            {
                XGaScalar<T> s => s.MapScalar(processor, scalarMapping),
                XGaVector<T> v => v.MapScalars(processor, scalarMapping),
                XGaBivector<T> bv => bv.MapScalars(processor, scalarMapping),
                XGaHigherKVector<T> kv => kv.MapScalars(processor, scalarMapping),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector MapScalars(XGaFloat64Processor processor, Func<IndexSet, T, double> scalarMapping)
        {
            return this switch
            {
                XGaScalar<T> s => s.MapScalar(processor, scalarMapping),
                XGaVector<T> v => v.MapScalars(processor, scalarMapping),
                XGaBivector<T> bv => bv.MapScalars(processor, scalarMapping),
                XGaHigherKVector<T> kv => kv.MapScalars(processor, scalarMapping),
                _ => throw new InvalidOperationException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaKVector<T> MapScalars(ScalarTransformer<T> transformer)
        {
            return MapScalars(transformer.MapScalarValue);
        }

        public T[] KVectorToArray(int vSpaceDimensions)
        {
            if (vSpaceDimensions < VSpaceDimensions)
                throw new ArgumentException(nameof(vSpaceDimensions));

            var kvSpaceDimensions =
                (int)vSpaceDimensions.GetBinomialCoefficient(Grade);

            var array = ScalarProcessor.CreateArrayZero1D(kvSpaceDimensions);

            foreach (var (index, scalar) in GetKVectorArrayItems())
                array[index] = scalar;

            return array;
        }

    }

    public sealed partial class XGaScalar<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> ToScalar()
        {
            return _scalar;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Simplify()
        {
            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar Convert(XGaFloat64Processor processor)
        {
            return processor.Scalar(
                ScalarProcessor.ToFloat64(ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar Convert(XGaFloat64Processor processor, Func<T, double> scalarMapping)
        {
            return processor.Scalar(
                scalarMapping(ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T2> Convert<T2>(XGaProcessor<T2> processor, Func<T, T2> scalarMapping)
        {
            return new XGaScalar<T2>(
                processor,
                scalarMapping(ScalarValue)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaScalar<T> MapScalars(ScalarTransformer<T> transformer)
        {
            return Processor.Scalar(
                transformer.MapScalarValue(ScalarValue)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> MapScalar(Func<T, T> scalarMapping)
        {
            return IsZero
                ? this
                : new XGaScalar<T>(
                    Processor,
                    scalarMapping(ScalarValue)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar MapScalar(XGaFloat64Processor processor, Func<T, double> scalarMapping)
        {
            return IsZero
                ? processor.ScalarZero
                : processor.Scalar(
                    scalarMapping(ScalarValue)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T1> MapScalar<T1>(XGaProcessor<T1> processor, Func<T, T1> scalarMapping)
        {
            return IsZero
                ? processor.ScalarZero
                : processor.Scalar(
                    scalarMapping(ScalarValue)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar MapScalar(XGaFloat64Processor processor, Func<IndexSet, T, double> scalarMapping)
        {
            return IsZero
                ? processor.ScalarZero
                : processor.Scalar(
                    scalarMapping(Processor.GetBasisScalarId(), ScalarValue)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T> MapScalar(Func<IndexSet, T, T> scalarMapping)
        {
            return IsZero
                ? this
                : new XGaScalar<T>(
                    Processor,
                    scalarMapping(
                        Processor.GetBasisScalarId(),
                        ScalarValue
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaScalar<T1> MapScalar<T1>(XGaProcessor<T1> processor, Func<IndexSet, T, T1> scalarMapping)
        {
            return IsZero
                ? processor.ScalarZero
                : processor.Scalar(
                    scalarMapping(Processor.GetBasisScalarId(), ScalarValue)
                );
        }

    }

    public sealed partial class XGaVector<T>
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Simplify()
        {
            return IsZero
                ? Processor.ScalarZero
                : this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Vector Convert(XGaFloat64Processor metric)
        {
            if (IsZero)
                return metric.VectorZero;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, double>(
                        term.Key,
                        ScalarProcessor.ToFloat64(term.Value)
                    )
                );

            return metric
                .CreateVectorComposer()
                .SetTerms(termList)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Vector Convert(XGaFloat64Processor metric, Func<T, double> scalarMapping)
        {
            if (IsZero)
                return metric.VectorZero;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, double>(
                        term.Key,
                        scalarMapping(term.Value)
                    )
                );

            return metric
                .CreateVectorComposer()
                .SetTerms(termList)
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T2> Convert<T2>(XGaProcessor<T2> metric, Func<T, T2> scalarMapping)
        {
            if (IsZero)
                return metric.VectorZero;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, T2>(
                        term.Key,
                        scalarMapping(term.Value)
                    )
                );

            return metric
                .CreateVectorComposer()
                .SetTerms(termList)
                .GetVector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> MapScalars(ScalarTransformer<T> transformer)
        {
            return MapScalars(transformer.MapScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVector<T> ToLinVector()
        {
            var indexScalarDictionary = IdScalarPairs.ToDictionary(
                p => p.Key.FirstIndex,
                p => p.Value
            );

            return ScalarProcessor.CreateLinVector(indexScalarDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[] VectorToFloat64Array1D()
        {
            var array = new double[VSpaceDimensions];

            foreach (var (id, scalar) in IdScalarPairs)
                array[id.FirstIndex] = ScalarProcessor.ToFloat64(scalar);

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[] VectorToArray1D()
        {
            var array = ScalarProcessor.CreateArrayZero1D(VSpaceDimensions);

            foreach (var (id, scalar) in IdScalarPairs)
                array[id.FirstIndex] = scalar;

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[] VectorToArray1D(int vectorSize)
        {
            if (vectorSize < VSpaceDimensions)
                throw new InvalidOperationException();

            var array = ScalarProcessor.CreateArrayZero1D(vectorSize);

            foreach (var (id, scalar) in IdScalarPairs)
                array[id.FirstIndex] = scalar;

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[,] VectorToRowArray2D(int vectorSize)
        {
            if (vectorSize < VSpaceDimensions)
                throw new InvalidOperationException();

            var array = ScalarProcessor.CreateArrayZero2D(1, vectorSize);

            foreach (var (id, scalar) in IdScalarPairs)
                array[0, id.FirstIndex] = scalar;

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[,] VectorToColumnArray2D(int vectorSize)
        {
            if (vectorSize < VSpaceDimensions)
                throw new InvalidOperationException();

            var array = ScalarProcessor.CreateArrayZero2D(vectorSize, 1);

            foreach (var (id, scalar) in IdScalarPairs)
                array[id.FirstIndex, 0] = scalar;

            return array;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector2D ToFloat64Vector2D()
        {
            return LinFloat64Vector2D.Create(
                Scalar(0).ToFloat64(),
                Scalar(1).ToFloat64()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector3D ToFloat64Vector3D()
        {
            return LinFloat64Vector3D.Create(
                Scalar(0).ToFloat64(),
                Scalar(1).ToFloat64(),
                Scalar(2).ToFloat64()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector4D ToFloat64Vector4D()
        {
            return LinFloat64Vector4D.Create(
                Scalar(0).ToFloat64(),
                Scalar(1).ToFloat64(),
                Scalar(2).ToFloat64(),
                Scalar(3).ToFloat64()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64Vector ToLinFloat64Vector()
        {
            return LinFloat64Vector.Create(
                VectorToFloat64Array1D()
            );
        }

    }

    public sealed partial class XGaBivector<T>
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Simplify()
        {
            return IsZero
                ? Processor.ScalarZero
                : this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Bivector Convert(XGaFloat64Processor metric)
        {
            if (IsZero)
                return metric.BivectorZero;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, double>(
                        term.Key,
                        ScalarProcessor.ToFloat64(term.Value)
                    )
                );

            return metric
                .CreateBivectorComposer()
                .SetTerms(termList)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Bivector Convert(XGaFloat64Processor metric, Func<T, double> scalarMapping)
        {
            if (IsZero)
                return metric.BivectorZero;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, double>(
                        term.Key,
                        scalarMapping(term.Value)
                    )
                );

            return metric
                .CreateBivectorComposer()
                .SetTerms(termList)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T2> Convert<T2>(XGaProcessor<T2> metric, Func<T, T2> scalarMapping)
        {
            if (IsZero)
                return metric.BivectorZero;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, T2>(
                        term.Key,
                        scalarMapping(term.Value)
                    )
                );

            return metric
                .CreateBivectorComposer()
                .SetTerms(termList)
                .GetBivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> MapScalars(ScalarTransformer<T> transformer)
        {
            return MapScalars(transformer.MapScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> MapScalars(Func<T, T> scalarMapping)
        {
            if (IsZero) return this;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, T>(
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
        public override XGaFloat64Bivector MapScalars(XGaFloat64Processor processor, Func<T, double> scalarMapping)
        {
            if (IsZero)
                return processor.BivectorZero;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, double>(
                        term.Key,
                        scalarMapping(term.Value)
                    )
                );

            return processor
                .CreateBivectorComposer()
                .SetTerms(termList)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T1> MapScalars<T1>(XGaProcessor<T1> processor, Func<T, T1> scalarMapping)
        {
            if (IsZero)
                return processor.BivectorZero;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, T1>(
                        term.Key,
                        scalarMapping(term.Value)
                    )
                );

            return processor
                .CreateBivectorComposer()
                .SetTerms(termList)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> MapScalars(Func<IndexSet, T, T> scalarMapping)
        {
            if (IsZero) return this;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, T>(
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
        public override XGaFloat64Bivector MapScalars(XGaFloat64Processor processor, Func<IndexSet, T, double> scalarMapping)
        {
            if (IsZero)
                return processor.BivectorZero;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, double>(
                        term.Key,
                        scalarMapping(term.Key, term.Value)
                    )
                );

            return processor
                .CreateBivectorComposer()
                .SetTerms(termList)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T1> MapScalars<T1>(XGaProcessor<T1> processor, Func<IndexSet, T, T1> scalarMapping)
        {
            if (IsZero)
                return processor.BivectorZero;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, T1>(
                        term.Key,
                        scalarMapping(term.Key, term.Value)
                    )
                );

            return processor
                .CreateBivectorComposer()
                .SetTerms(termList)
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaBivector<T> MapScalars(Func<int, int, T, T> scalarMapping)
        {
            if (IsZero) return this;

            var termList =
                IdScalarPairs
                    .Where(term => term.Key.Count == 1)
                    .Select(
                        term => new KeyValuePair<IndexSet, T>(
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
        public XGaBivector<T> MapBasisBivectors(Func<int, int, IPair<int>> basisMapping)
        {
            if (IsZero) return this;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, T>(
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
        public XGaBivector<T> MapBasisBivectors(Func<int, int, T, IPair<int>> basisMapping)
        {
            if (IsZero) return this;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, T>(
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
        public XGaBivector<T> MapTerms(Func<int, int, T, KeyValuePair<IPair<int>, T>> termMapping)
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

                        return new KeyValuePair<IndexSet, T>(
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


        public T[] BivectorToArray1D()
        {
            var array = ScalarProcessor.CreateArrayZero1D((int)KvSpaceDimensions);

            foreach (var (id, scalar) in IdScalarPairs)
            {
                var index1 = id.FirstIndex;
                var index2 = id.LastIndex;

                var index = (int)BinaryCombinationsUtilsUInt64.CombinadicToIndex(index1, index2);

                array[index] = scalar;
            }

            return array;
        }

        public T[] BivectorToArray1D(int arraySize)
        {
            if ((ulong)arraySize < KvSpaceDimensions)
                throw new InvalidOperationException();

            var array = ScalarProcessor.CreateArrayZero1D(arraySize);

            foreach (var (id, scalar) in IdScalarPairs)
            {
                var index1 = id.FirstIndex;
                var index2 = id.LastIndex;

                var index = (int)BinaryCombinationsUtilsUInt64.CombinadicToIndex(index1, index2);

                array[index] = scalar;
            }

            return array;
        }

        public T[,] BivectorToArray2D()
        {
            var array = ScalarProcessor.CreateArrayZero2D(VSpaceDimensions);

            foreach (var (id, scalar) in IdScalarPairs)
            {
                var index1 = id.FirstIndex;
                var index2 = id.LastIndex;

                array[index1, index2] = scalar;
                array[index2, index1] = ScalarProcessor.Negative(scalar).ScalarValue;
            }

            return array;
        }

        public T[,] BivectorToArray2D(int arraySize)
        {
            if (arraySize < VSpaceDimensions)
                throw new InvalidOperationException();

            var array = ScalarProcessor.CreateArrayZero2D(arraySize);

            foreach (var (id, scalar) in IdScalarPairs)
            {
                var index1 = id.FirstIndex;
                var index2 = id.LastIndex;

                array[index1, index2] = scalar;
                array[index2, index1] = ScalarProcessor.Negative(scalar).ScalarValue;
            }

            return array;
        }

    }

    public sealed partial class XGaHigherKVector<T>
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Simplify()
        {
            return IsZero
                ? Processor.ScalarZero
                : this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64HigherKVector Convert(XGaFloat64Processor metric)
        {
            if (IsZero)
                return (XGaFloat64HigherKVector)metric.KVectorZero(Grade);

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, double>(
                        term.Key,
                        ScalarProcessor.ToFloat64(term.Value)
                    )
                );

            return metric
                .CreateKVectorComposer(Grade)
                .SetTerms(termList)
                .GetHigherKVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64HigherKVector Convert(XGaFloat64Processor metric, Func<T, double> scalarMapping)
        {
            if (IsZero)
                return (XGaFloat64HigherKVector)metric.KVectorZero(Grade);

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, double>(
                        term.Key,
                        scalarMapping(term.Value)
                    )
                );

            return metric
                .CreateKVectorComposer(Grade)
                .SetTerms(termList)
                .GetHigherKVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T2> Convert<T2>(XGaProcessor<T2> metric, Func<T, T2> scalarMapping)
        {
            if (IsZero)
                return (XGaHigherKVector<T2>)metric.KVectorZero(Grade);

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, T2>(
                        term.Key,
                        scalarMapping(term.Value)
                    )
                );

            return (XGaHigherKVector<T2>)metric
                .CreateKVectorComposer(Grade)
                .SetTerms(termList)
                .GetKVector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaHigherKVector<T> MapScalars(ScalarTransformer<T> transformer)
        {
            return MapScalars(transformer.MapScalarValue);
        }

    }

    public sealed partial class XGaGradedMultivector<T>
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Simplify()
        {
            return KVectorCount switch
            {
                0 => Processor.ScalarZero,
                1 => _gradeKVectorDictionary.Values.First().Simplify(),
                _ => this
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64GradedMultivector Convert(XGaFloat64Processor metric)
        {
            if (IsZero)
                return metric.GradedMultivectorZero;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, double>(
                        term.Key,
                        ScalarProcessor.ToFloat64(term.Value)
                    )
                );

            return metric
                .CreateMultivectorComposer()
                .SetTerms(termList)
                .GetGradedMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64GradedMultivector Convert(XGaFloat64Processor metric, Func<T, double> scalarMapping)
        {
            if (IsZero)
                return metric.GradedMultivectorZero;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, double>(
                        term.Key,
                        scalarMapping(term.Value)
                    )
                );

            return metric
                .CreateMultivectorComposer()
                .SetTerms(termList)
                .GetGradedMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaGradedMultivector<T2> Convert<T2>(XGaProcessor<T2> metric, Func<T, T2> scalarMapping)
        {
            if (IsZero)
                return metric.GradedMultivectorZero;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, T2>(
                        term.Key,
                        scalarMapping(term.Value)
                    )
                );

            return metric
                .CreateMultivectorComposer()
                .SetTerms(termList)
                .GetGradedMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> MapScalars(ScalarTransformer<T> transformer)
        {
            return MapScalars(transformer.MapScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> MapScalars(Func<T, T> scalarMapping)
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
        public override XGaFloat64Multivector MapScalars(XGaFloat64Processor processor, Func<T, double> scalarMapping)
        {
            if (_gradeKVectorDictionary.Count == 1)
                return _gradeKVectorDictionary
                    .Values
                    .First()
                    .MapScalars(processor, scalarMapping);

            return IsZero
                ? processor.ScalarZero
                : MapKVectorsSimplify(
                    kv => kv.MapScalars(processor, scalarMapping),
                    processor
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T1> MapScalars<T1>(XGaProcessor<T1> processor, Func<T, T1> scalarMapping)
        {
            if (_gradeKVectorDictionary.Count == 1)
                return _gradeKVectorDictionary
                    .Values
                    .First()
                    .MapScalars(processor, scalarMapping);

            return IsZero
                ? processor.ScalarZero
                : MapKVectorsSimplify(
                    kv => kv.MapScalars(processor, scalarMapping),
                    processor
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> MapScalars(Func<IndexSet, T, T> scalarMapping)
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
        public override XGaFloat64Multivector MapScalars(XGaFloat64Processor processor, Func<IndexSet, T, double> scalarMapping)
        {
            if (_gradeKVectorDictionary.Count == 1)
                return _gradeKVectorDictionary
                    .Values
                    .First()
                    .MapScalars(processor, scalarMapping);

            return IsZero
                ? processor.ScalarZero
                : MapKVectorsSimplify(
                    kv => kv.MapScalars(processor, scalarMapping),
                    processor
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T1> MapScalars<T1>(XGaProcessor<T1> processor, Func<IndexSet, T, T1> scalarMapping)
        {
            if (_gradeKVectorDictionary.Count == 1)
                return _gradeKVectorDictionary
                    .Values
                    .First()
                    .MapScalars(processor, scalarMapping);

            return IsZero
                ? processor.ScalarZero
                : MapKVectorsSimplify(
                    kv => kv.MapScalars(processor, scalarMapping),
                    processor
                );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<XGaKVector<T>> MapKVectorPairs(IEnumerable<XGaKVector<T>> mv2, Func<XGaKVector<T>, XGaKVector<T>, XGaKVector<T>> kVectorMapping)
        {
            foreach (var kv2 in mv2)
                foreach (var kv1 in _gradeKVectorDictionary.Values)
                    yield return kVectorMapping(kv1, kv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<XGaKVector<T>> MapKVectorPairs(IEnumerable<XGaKVector<T>> mv2, Func<XGaKVector<T>, XGaKVector<T>, bool> pairFilter, Func<XGaKVector<T>, XGaKVector<T>, XGaKVector<T>> kVectorMapping)
        {
            foreach (var kv2 in mv2)
                foreach (var kv1 in _gradeKVectorDictionary.Values)
                    if (pairFilter(kv1, kv2))
                        yield return kVectorMapping(kv1, kv2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivector<T> MapKVectors(IEnumerable<XGaKVector<T>> mv2, Func<XGaKVector<T>, XGaKVector<T>, XGaKVector<T>> kVectorMapping)
        {
            return Processor.GradedMultivectorFromSum(
                MapKVectorPairs(mv2, kVectorMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivector<T> MapKVectors(IEnumerable<XGaKVector<T>> mv2, Func<XGaKVector<T>, XGaKVector<T>, bool> pairFilter, Func<XGaKVector<T>, XGaKVector<T>, XGaKVector<T>> kVectorMapping)
        {
            return Processor.GradedMultivectorFromSum(
                MapKVectorPairs(mv2, pairFilter, kVectorMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivector<T> MapKVectors(Func<XGaKVector<T>, XGaKVector<T>> kVectorMapping)
        {
            return Processor.GradedMultivectorFromSum(
                _gradeKVectorDictionary.Values.Select(kVectorMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivector<T> MapKVectors(Func<XGaKVector<T>, bool> kVectorFilter, Func<XGaKVector<T>, XGaKVector<T>> kVectorMapping)
        {
            return Processor.GradedMultivectorFromSum(
                _gradeKVectorDictionary.Values.Where(kVectorFilter).Select(kVectorMapping)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> MapKVectorsSimplify(IEnumerable<XGaKVector<T>> mv2, Func<XGaKVector<T>, XGaKVector<T>, XGaKVector<T>> kVectorMapping)
        {
            return Processor.MultivectorFromSum(
                MapKVectorPairs(mv2, kVectorMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> MapKVectorsSimplify(IEnumerable<XGaKVector<T>> mv2, Func<XGaKVector<T>, XGaKVector<T>, bool> pairFilter, Func<XGaKVector<T>, XGaKVector<T>, XGaKVector<T>> kVectorMapping)
        {
            return Processor.MultivectorFromSum(
                MapKVectorPairs(mv2, pairFilter, kVectorMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> MapKVectorsSimplify(Func<XGaKVector<T>, XGaKVector<T>> kVectorMapping)
        {
            return Processor.MultivectorFromSum(
                _gradeKVectorDictionary.Values.Select(kVectorMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T> MapKVectorsSimplify(Func<XGaKVector<T>, bool> kVectorFilter, Func<XGaKVector<T>, XGaKVector<T>> kVectorMapping)
        {
            return Processor.MultivectorFromSum(
                _gradeKVectorDictionary.Values.Where(kVectorFilter).Select(kVectorMapping)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64GradedMultivector MapKVectors(Func<XGaKVector<T>, XGaFloat64KVector> kVectorMapping, XGaFloat64Processor processor)
        {
            return processor.GradedMultivectorFromSum(
                _gradeKVectorDictionary.Values.Select(kVectorMapping)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivector<T1> MapKVectors<T1>(Func<XGaKVector<T>, XGaKVector<T1>> kVectorMapping, XGaProcessor<T1> processor)
        {
            return processor.GradedMultivectorFromSum(
                _gradeKVectorDictionary.Values.Select(kVectorMapping)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64GradedMultivector MapKVectors(Func<XGaKVector<T>, bool> kVectorFilter, Func<XGaKVector<T>, XGaFloat64KVector> kVectorMapping, XGaFloat64Processor processor)
        {
            return processor.GradedMultivectorFromSum(
                _gradeKVectorDictionary.Values.Where(kVectorFilter).Select(kVectorMapping)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaGradedMultivector<T1> MapKVectors<T1>(Func<XGaKVector<T>, bool> kVectorFilter, Func<XGaKVector<T>, XGaKVector<T1>> kVectorMapping, XGaProcessor<T1> processor)
        {
            return processor.GradedMultivectorFromSum(
                _gradeKVectorDictionary.Values.Where(kVectorFilter).Select(kVectorMapping)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector MapKVectorsSimplify(Func<XGaKVector<T>, XGaFloat64KVector> kVectorMapping, XGaFloat64Processor processor)
        {
            return processor.MultivectorFromSum(
                _gradeKVectorDictionary.Values.Select(kVectorMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T1> MapKVectorsSimplify<T1>(Func<XGaKVector<T>, XGaKVector<T1>> kVectorMapping, XGaProcessor<T1> processor)
        {
            return processor.MultivectorFromSum(
                _gradeKVectorDictionary.Values.Select(kVectorMapping)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector MapKVectorsSimplify(Func<XGaKVector<T>, bool> kVectorFilter, Func<XGaKVector<T>, XGaFloat64KVector> kVectorMapping, XGaFloat64Processor processor)
        {
            return processor.MultivectorFromSum(
                _gradeKVectorDictionary.Values.Where(kVectorFilter).Select(kVectorMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaMultivector<T1> MapKVectorsSimplify<T1>(Func<XGaKVector<T>, bool> kVectorFilter, Func<XGaKVector<T>, XGaKVector<T1>> kVectorMapping, XGaProcessor<T1> processor)
        {
            return processor.MultivectorFromSum(
                _gradeKVectorDictionary.Values.Where(kVectorFilter).Select(kVectorMapping)
            );
        }

    }

    public sealed partial class XGaUniformMultivector<T>
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaMultivector<T> Simplify()
        {
            return this;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64UniformMultivector Convert(XGaFloat64Processor metric)
        {
            if (IsZero)
                return metric.UniformMultivectorZero;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, double>(
                        term.Key,
                        ScalarProcessor.ToFloat64(term.Value)
                    )
                );

            return metric
                .CreateUniformComposer()
                .SetTerms(termList)
                .GetUniformMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64UniformMultivector Convert(XGaFloat64Processor metric, Func<T, double> scalarMapping)
        {
            if (IsZero)
                return metric.UniformMultivectorZero;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, double>(
                        term.Key,
                        scalarMapping(term.Value)
                    )
                );

            return metric
                .CreateUniformComposer()
                .SetTerms(termList)
                .GetUniformMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaUniformMultivector<T2> Convert<T2>(XGaProcessor<T2> metric, Func<T, T2> scalarMapping)
        {
            if (IsZero)
                return metric.UniformMultivectorZero;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, T2>(
                        term.Key,
                        scalarMapping(term.Value)
                    )
                );

            return metric
                .CreateUniformComposer()
                .SetTerms(termList)
                .GetUniformMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaUniformMultivector<T> MapScalars(ScalarTransformer<T> transformer)
        {
            return MapScalars(transformer.MapScalarValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaUniformMultivector<T> MapScalars(Func<T, T> scalarMapping)
        {
            if (IsZero)
                return this;

            var idScalarPairs =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, T>(
                        term.Key,
                        scalarMapping(term.Value)
                    )
                );

            return Processor
                .CreateUniformComposer()
                .AddTerms(idScalarPairs)
                .GetUniformMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaUniformMultivector<T1> MapScalars<T1>(XGaProcessor<T1> processor, Func<T, T1> scalarMapping)
        {
            if (IsZero)
                return processor.UniformMultivectorZero;

            var idScalarPairs =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, T1>(
                        term.Key,
                        scalarMapping(term.Value)
                    )
                );

            return processor
                .CreateUniformComposer()
                .AddTerms(idScalarPairs)
                .GetUniformMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64UniformMultivector MapScalars(XGaFloat64Processor processor, Func<T, double> scalarMapping)
        {
            if (IsZero)
                return processor.UniformMultivectorZero;

            var idScalarPairs =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, double>(
                        term.Key,
                        scalarMapping(term.Value)
                    )
                );

            return processor
                .CreateUniformComposer()
                .AddTerms(idScalarPairs)
                .GetUniformMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaUniformMultivector<T> MapScalars(Func<IndexSet, T, T> scalarMapping)
        {
            if (IsZero)
                return this;

            var idScalarPairs =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, T>(
                        term.Key,
                        scalarMapping(term.Key, term.Value)
                    )
                );

            return Processor
                .CreateUniformComposer()
                .AddTerms(idScalarPairs)
                .GetUniformMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaUniformMultivector<T1> MapScalars<T1>(XGaProcessor<T1> processor, Func<IndexSet, T, T1> scalarMapping)
        {
            if (IsZero)
                return processor.UniformMultivectorZero;

            var idScalarPairs =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, T1>(
                        term.Key,
                        scalarMapping(term.Key, term.Value)
                    )
                );

            return processor
                .CreateUniformComposer()
                .AddTerms(idScalarPairs)
                .GetUniformMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64UniformMultivector MapScalars(XGaFloat64Processor processor, Func<IndexSet, T, double> scalarMapping)
        {
            if (IsZero)
                return processor.UniformMultivectorZero;

            var idScalarPairs =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, double>(
                        term.Key,
                        scalarMapping(term.Key, term.Value)
                    )
                );

            return processor
                .CreateUniformComposer()
                .AddTerms(idScalarPairs)
                .GetUniformMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaUniformMultivector<T> MapBasisBlades(Func<IndexSet, IndexSet> basisMapping)
        {
            if (IsZero)
                return this;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, T>(
                        basisMapping(term.Key),
                        term.Value
                    )
                );

            return Processor
                .CreateUniformComposer()
                .AddTerms(termList)
                .GetUniformMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaUniformMultivector<T> MapBasisBlades(Func<IndexSet, T, IndexSet> basisMapping)
        {
            if (IsZero)
                return this;

            var termList =
                IdScalarPairs.Select(
                    term => new KeyValuePair<IndexSet, T>(
                        basisMapping(term.Key, term.Value),
                        term.Value
                    )
                );

            return Processor
                .CreateUniformComposer()
                .SetTerms(termList)
                .GetUniformMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaUniformMultivector<T> MapTerms(Func<IndexSet, T, KeyValuePair<IndexSet, T>> termMapping)
        {
            if (IsZero)
                return this;

            var termList =
                IdScalarPairs.Select(
                    term =>
                        termMapping(term.Key, term.Value)
                );

            return Processor
                .CreateUniformComposer()
                .SetTerms(termList)
                .GetUniformMultivector();
        }

    }
}
