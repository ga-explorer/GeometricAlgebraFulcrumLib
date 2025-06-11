using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.GuidedBinaryTraversal;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.GuidedBinaryTraversal.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

/// <summary>
/// A sparse Euclidean multivector or arbitrary dimensions with T
/// precision scalars
/// </summary>
public abstract partial class XGaMultivector<T> :
    IReadOnlyCollection<KeyValuePair<IndexSet, T>>,
    IXGaElement<T>
{
    public abstract string MultivectorClassName { get; }

    public XGaProcessor<T> Processor { get; }

    public XGaMetric Metric 
        => Processor; 

    public IScalarProcessor<T> ScalarProcessor 
        => Processor.ScalarProcessor;

    public bool IsEuclidean
        => Processor.IsEuclidean;

    public bool IsNonEuclidean
        => Processor.IsNonEuclidean;

    /// <summary>
    /// The dimensions of the base vector space, dynamically determined from
    /// stored terms
    /// </summary>
    public int VSpaceDimensions
        => IsZero ? 0 : Ids.Max(id => id.VSpaceDimensions());

    /// <summary>
    /// The number of stored terms in this multivector
    /// </summary>
    public abstract int Count { get; }

    /// <summary>
    /// The stored k-vector grades in this multivector
    /// </summary>
    public abstract IEnumerable<int> KVectorGrades { get; }

    /// <summary>
    /// True if this is a zero multivector
    /// </summary>
    public abstract bool IsZero { get; }

    /// <summary>
    /// Get basis blades of all stored terms
    /// </summary>
    /// <value></value>
    public abstract IEnumerable<XGaBasisBlade> BasisBlades { get; }

    /// <summary>
    /// Get basis blade IDs of all stored terms
    /// </summary>
    public abstract IEnumerable<IndexSet> Ids { get; }

    /// <summary>
    /// Get scalars of all stored terms
    /// </summary>
    /// <value></value>
    public abstract IEnumerable<T> Scalars { get; }

    /// <summary>
    /// Get all stored terms as pairs of (BasisBlade ID, Scalar)
    /// </summary>
    /// <value></value>
    public abstract IEnumerable<KeyValuePair<IndexSet, T>> IdScalarPairs { get; }

    /// <summary>
    /// Get all stored terms as tuples of (BasisBlade ID, Scalar)
    /// </summary>
    /// <value></value>
    public IEnumerable<Tuple<IndexSet, T>> IdScalarTuples 
        => IdScalarPairs.Select(p => 
            new Tuple<IndexSet, T>(p.Key, p.Value)
        );

    /// <summary>
    /// Get all stored terms as pairs of (BasisBlade, Scalar)
    /// </summary>
    /// <value></value>
    public abstract IEnumerable<KeyValuePair<XGaBasisBlade, T>> BasisScalarPairs { get; }
        
    public Scalar<T> this[int index]
        => Scalar(index);

    public Scalar<T> this[int index1, int index2]
        => Scalar(index1, index2);
        
    public Scalar<T> this[int index1, int index2, int index3]
        => Scalar(index1, index2, index3);

    public Scalar<T> this[params int[] indexList]
        => Scalar(indexList);
        
    public Scalar<T> this[IReadOnlyList<int> indexList]
        => Scalar(indexList);

    public Scalar<T> this[IXGaSignedBasisBlade basisBlade]
        => Scalar(basisBlade);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected XGaMultivector(XGaProcessor<T> processor)
    {
        Processor = processor;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero()
    {
        return IsZero ||
               Scalars.All(ScalarProcessor.IsNearZero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasSameMetric(XGaMultivector<T> mv)
    {
        return Processor.HasSameSignature(mv.Processor);
    }

    public abstract bool IsValid();

    public abstract bool IsScalar();

    public abstract bool IsVector();

    public abstract bool IsBivector();

    public abstract bool IsKVector(int grade);

    public abstract bool IsOdd();

    public abstract bool IsOdd(int maxGrade);

    public abstract bool IsEven();

    public abstract bool IsEven(int maxGrade);


    public abstract bool ContainsScalarPart();

    public abstract bool ContainsVectorPart();

    public abstract bool ContainsBivectorPart();

    public abstract bool ContainsKVectorPart(int grade);
    
    public abstract bool ContainsOddPart();

    public abstract bool ContainsOddPart(int maxGrade);
    
    public abstract bool ContainsEvenPart();

    public abstract bool ContainsEvenPart(int maxGrade);


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

    public abstract int GetMinGrade();

    public abstract int GetMaxGrade();

    public abstract bool ContainsKey(IndexSet key);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(IndexSet key, out T value)
    {
        return TryGetBasisBladeScalarValue(key, out value);
    }

    /// <summary>
    /// The number of stored k-vectors in this multivector
    /// </summary>
    public abstract int GetKVectorCount();

    public abstract XGaScalar<T> GetScalarPart();

    public abstract XGaVector<T> GetVectorPart();
    
    public abstract XGaVector<T> GetVectorPart(Func<int, bool> filter);

    public abstract XGaBivector<T> GetBivectorPart();

    public abstract XGaHigherKVector<T> GetHigherKVectorPart(int grade);

    public abstract XGaKVector<T> GetKVectorPart(int grade);
        
    public abstract XGaKVector<T> GetFirstKVectorPart();

    public abstract XGaMultivector<T> GetEvenPart();

    public abstract XGaMultivector<T> GetEvenPart(int maxGrade);

    public abstract XGaMultivector<T> GetOddPart();

    public abstract XGaMultivector<T> GetOddPart(int maxGrade);

    public abstract IEnumerable<XGaKVector<T>> GetKVectorParts();

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IndexSet GetStoredGradesBitPattern()
    {
        return IndexSet.Create(KVectorGrades, false);
    }

    /// <summary>
    /// Get the scalar coefficient associated with a basis blade term
    /// </summary>
    /// <param name="basisBladeId"></param>
    /// <returns></returns>
    public abstract Scalar<T> GetBasisBladeScalar(IndexSet basisBladeId);
        
    /// <summary>
    /// Get the scalar coefficient associated with the basis scalar term
    /// </summary>
    /// <returns></returns>
    public abstract Scalar<T> Scalar();
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Scalar(int index)
    {
        return Scalar(
            Metric.BasisVector(index)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Scalar(int index1, int index2)
    {
        return Scalar(
            Metric.Op(index1, index2)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Scalar(int index1, int index2, int index3)
    {
        return Scalar(
            Metric.Op(index1, index2, index3)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Scalar(params int[] indexList)
    {
        return Scalar(
            Metric.Op(indexList)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Scalar(IReadOnlyList<int> indexList)
    {
        return Scalar(
            Metric.Op(indexList)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Scalar(IXGaSignedBasisBlade basisBlade)
    {
        return basisBlade.IsZero
            ? ScalarProcessor.Zero
            : basisBlade.IsPositive
                ? GetBasisBladeScalar(basisBlade.Id)
                : -GetBasisBladeScalar(basisBlade.Id);
    }


    public abstract bool TryGetBasisBladeScalarValue(IndexSet basisBlade, out T scalar);
        
    public abstract bool TryGetScalarValue(out T scalar);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetScalarValue(out T scalar, int index)
    {
        return TryGetScalarValue(
            out scalar, 
            Metric.BasisVector(index)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetScalarValue(out T scalar, int index1, int index2)
    {
        return TryGetScalarValue(
            out scalar, 
            Metric.Op(index1, index2)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetScalarValue(out T scalar, int index1, int index2, int index3)
    {
        return TryGetScalarValue(
            out scalar, 
            Metric.Op(index1, index2, index3)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetScalarValue(out T scalar, params int[] indexList)
    {
        return TryGetScalarValue(
            out scalar, 
            Metric.Op(indexList)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetScalarValue(out T scalar, IReadOnlyList<int> indexList)
    {
        return TryGetScalarValue(
            out scalar, 
            Metric.Op(indexList)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetScalarValue(out T scalar, IXGaSignedBasisBlade basisBlade)
    {
        if (!basisBlade.IsZero && TryGetBasisBladeScalarValue(basisBlade.Id, out scalar))
        {
            if (basisBlade.IsNegative)
                scalar = ScalarProcessor.Negative(scalar).ScalarValue;

            return true;
        }

        scalar = ScalarProcessor.ZeroValue;
        return false;
    }
        

    /// <summary>
    /// Get all stored terms as (Id, Scalar) records for constructing a column
    /// vector array for this multivector
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<ulong, T>> GetMultivectorArrayItems()
    {
        return IdScalarPairs.Select(
            term => 
                new KeyValuePair<ulong, T>(
                    term.Key.ToUInt64(), 
                    term.Value
                )
        );
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
    public virtual XGaGradedMultivectorComposer<T> ToComposer()
    {
        return Processor.CreateMultivectorComposer().SetMultivector(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaGradedMultivectorComposer<T> NegativeToComposer()
    {
        return Processor.CreateMultivectorComposer().SetMultivectorNegative(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaGradedMultivectorComposer<T> ToComposer(T scalingFactor)
    {
        return Processor.CreateMultivectorComposer().SetMultivectorScaled(this, scalingFactor);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaGradedMultivector<T> ToGradedMultivector()
    {
        return this switch
        {
            XGaKVector<T> kVector => kVector.ToGradedMultivector(),
            XGaGradedMultivector<T> mv => mv,
            XGaUniformMultivector<T> mv => mv.ToGradedMultivector(),
            _ => throw new InvalidOperationException()
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaUniformMultivector<T> ToUniformMultivector()
    {
        return this switch
        {
            XGaKVector<T> kVector => kVector.ToUniformMultivector(),
            XGaUniformMultivector<T> mv => mv,
            XGaGradedMultivector<T> mv => mv.ToUniformMultivector(),
            _ => throw new InvalidOperationException()
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
    
    
    public virtual T[] MultivectorToArray(int vSpaceDimensions)
    {
        if (vSpaceDimensions > 31 || vSpaceDimensions < VSpaceDimensions)
            throw new ArgumentException(nameof(vSpaceDimensions));

        var gaSpaceDimensions =
            1 << vSpaceDimensions;

        var array = ScalarProcessor.CreateArrayZero1D(gaSpaceDimensions);

        foreach (var (index, scalar) in GetMultivectorArrayItems())
            array[index] = scalar;

        return array;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> GetPart(Func<IndexSet, bool> filterFunc);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> GetPart(Func<T, bool> filterFunc);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> GetPart(Func<IndexSet, T, bool> filterFunc);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual Tuple<XGaScalar<T>, XGaBivector<T>> GetScalarBivectorParts()
    {
        return new Tuple<XGaScalar<T>, XGaBivector<T>>(
            GetScalarPart(),
            GetBivectorPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual Tuple<XGaMultivector<T>, XGaMultivector<T>> GetEvenOddParts()
    {
        return new Tuple<XGaMultivector<T>, XGaMultivector<T>>(
            GetEvenPart(),
            GetOddPart()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual Tuple<XGaMultivector<T>, XGaMultivector<T>> GetEvenOddParts(int maxGrade)
    {
        return new Tuple<XGaMultivector<T>, XGaMultivector<T>>(
            GetEvenPart(maxGrade),
            GetOddPart(maxGrade)
        );
    }

        
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
            .GetSimpleMultivector();
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
            .GetSimpleMultivector();
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
            .GetSimpleMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> Negative();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> Reverse();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> GradeInvolution();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> CliffordConjugate();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> Conjugate();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> Times(T scalarValue);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> Times(int scalarValue);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> Times(double scalarValue);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> Times(Scalar<T> scalarValue);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> Times(IScalar<T> scalarValue);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> Divide(int scalarValue);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> Divide(double scalarValue);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> Divide(T scalarValue);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> Divide(Scalar<T> scalarValue);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> Divide(IScalar<T> scalarValue);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> DivideByENorm();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> DivideByENormSquared();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> DivideByNorm();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> DivideByNormSquared();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> EInverse();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> Inverse();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> PseudoInverse();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> EDual(int vSpaceDimensions);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> EDual(XGaKVector<T> blade);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> Dual(int vSpaceDimensions);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> Dual(XGaKVector<T> blade);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> EUnDual(int vSpaceDimensions);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> EUnDual(XGaKVector<T> blade);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> UnDual(int vSpaceDimensions);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> UnDual(XGaKVector<T> blade);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T[] MultivectorToArray1D(int arraySize)
    {
        var vSpaceDimensions = VSpaceDimensions;

        if (vSpaceDimensions > 31)
            throw new InvalidOperationException();

        var gaSpaceDimensions = 1UL << vSpaceDimensions;

        if ((ulong) arraySize < gaSpaceDimensions)
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
    public LinFloat64Vector2D GetVectorPartAsFloat64Vector2D()
    {
        return LinFloat64Vector2D.Create(
            Scalar(0).ToFloat64(),
            Scalar(1).ToFloat64()
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetVectorPartAsFloat64Vector3D()
    {
        return LinFloat64Vector3D.Create(
            Scalar(0).ToFloat64(),
            Scalar(1).ToFloat64(),
            Scalar(2).ToFloat64()
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4D GetVectorPartAsFloat64Vector4D()
    {
        return LinFloat64Vector4D.Create(
            Scalar(0).ToFloat64(),
            Scalar(1).ToFloat64(),
            Scalar(2).ToFloat64(),
            Scalar(3).ToFloat64()
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivector<T> ReflectOn(XGaKVector<T> subspace)
    {
        Debug.Assert(subspace.IsNearBlade());

        return subspace
            .Gp(this)
            .Gp(subspace.Inverse());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivector<T> ReflectDirectOnDirect(XGaKVector<T> subspace)
    {
        return GetKVectorParts()
            .Select(kv => kv.ReflectDirectOnDirect(subspace))
            .Aggregate(
                (XGaMultivector<T>) Processor.ScalarZero,
                (a, b) => a.Add(b)
            );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivector<T> ReflectDirectOnDual(XGaKVector<T> subspace)
    {
        return GetKVectorParts()
            .Select(kv => kv.ReflectDirectOnDual(subspace))
            .Aggregate(
                (XGaMultivector<T>) Processor.ScalarZero,
                (a, b) => a.Add(b)
            );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivector<T> ReflectDualOnDirect(XGaKVector<T> subspace, int vSpaceDimensions)
    {
        return GetKVectorParts()
            .Select(kv => kv.ReflectDualOnDirect(subspace, vSpaceDimensions))
            .Aggregate(
                (XGaMultivector<T>) Processor.ScalarZero,
                (a, b) => a.Add(b)
            );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivector<T> ReflectDualOnDual(XGaKVector<T> subspace)
    {
        return GetKVectorParts()
            .Select(kv => kv.ReflectDualOnDual(subspace))
            .Aggregate(
                (XGaMultivector<T>) Processor.ScalarZero,
                (a, b) => a.Add(b)
            );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual XGaMultivector<T> ProjectOn(XGaKVector<T> subspace, bool useSubspaceInverse = false)
    {
        Debug.Assert(subspace.IsNearBlade());
        
        var subspaceInverse = 
            useSubspaceInverse 
                ? subspace.PseudoInverse() 
                : subspace;

        return Fdp(subspaceInverse).Gp(subspace);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEuclideanRotor()
    {
        return IsEven() && (EGp(Reverse()) - 1d).IsZero;
    }


    /// <summary>
    /// Create a pure rotor from its scalar and bivector parts
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaPureRotor<T> ToPureRotor()
    {
        return XGaPureRotor<T>.Create(
            GetScalarPart().ScalarValue,
            GetBivectorPart()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetMultivectorText()
    {
        return IdScalarPairs
            .OrderByGradeIndex()
            .GetTermsText();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<KeyValuePair<IndexSet, T>> GetEnumerator()
    {
        return IdScalarPairs.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        if (IsZero)
            return $"'{ScalarProcessor.Zero.ToText()}'<>";

        return BasisScalarPairs
            .OrderBy(p => p.Key.Id.Count)
            .ThenBy(p => p.Key.Id)
            .Select(p => $"'{ScalarProcessor.ToText(p.Value)}'{p.Key}")
            .Concatenate(" + ");
    }
}