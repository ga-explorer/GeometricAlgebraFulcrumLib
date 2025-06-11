using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

public sealed partial class XGaHigherKVector<T> :
    XGaKVector<T>
{
    private readonly IReadOnlyDictionary<IndexSet, T> _idScalarDictionary;


    public override string MultivectorClassName
        => $"Generic {Grade}-Vector";

    public override int Count
        => _idScalarDictionary.Count;

    public override IEnumerable<int> KVectorGrades
    {
        get
        {
            if (!IsZero) yield return Grade;
        }
    }

    public override int Grade { get; }

    public override bool IsZero
        => _idScalarDictionary.Count == 0;

    public override IEnumerable<XGaBasisBlade> BasisBlades
        => _idScalarDictionary.Keys.Select(Processor.BasisBlade);

    public override IEnumerable<IndexSet> Ids 
        => _idScalarDictionary.Keys;

    public override IEnumerable<T> Scalars
        => _idScalarDictionary.Values;

    public override IEnumerable<KeyValuePair<IndexSet, T>> IdScalarPairs
        => _idScalarDictionary;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaHigherKVector(XGaProcessor<T> processor, int grade)
        : base(processor)
    {
        if (grade < 3)
            throw new ArgumentOutOfRangeException(nameof(grade));

        _idScalarDictionary = new EmptyDictionary<IndexSet, T>();

        Grade = grade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaHigherKVector(XGaProcessor<T> processor, KeyValuePair<IndexSet, T> basisScalarPair)
        : base(processor)
    {
        var grade = basisScalarPair.Key.Count;

        if (grade < 3)
            throw new ArgumentOutOfRangeException(nameof(grade));

        _idScalarDictionary =
            new SingleItemDictionary<IndexSet, T>(basisScalarPair);

        Grade = grade;

        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaHigherKVector(XGaProcessor<T> processor, int grade, IReadOnlyDictionary<IndexSet, T> indexScalarDictionary)
        : base(processor)
    {
        if (grade < 3)
            throw new ArgumentOutOfRangeException(nameof(grade));

        _idScalarDictionary = indexScalarDictionary;

        Grade = grade;

        Debug.Assert(IsValid());
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return _idScalarDictionary.IsValidKVectorDictionary(Processor, Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IReadOnlyDictionary<IndexSet, T> GetIdScalarDictionary()
    {
        return _idScalarDictionary;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsKey(IndexSet key)
    {
        return !IsZero && _idScalarDictionary.ContainsKey(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> GetScalarPart()
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T> GetVectorPart()
    {
        return Processor.VectorZero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T> GetVectorPart(Func<int, bool> filterFunc)
    {
        return Processor.VectorZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> GetBivectorPart()
    {
        return Processor.BivectorZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> GetHigherKVectorPart(int grade)
    {
        return grade == Grade
            ? this
            : Processor.HigherKVectorZero(grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> GetPart(Func<IndexSet, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Key)
            ).ToDictionary();

        return Processor.HigherKVector(Grade, idScalarDictionary);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> GetPart(Func<T, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Value)
            ).ToDictionary();

        return Processor.HigherKVector(Grade, idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> GetPart(Func<IndexSet, T, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Key, p.Value)
            ).ToDictionary();

        return Processor.HigherKVector(Grade, idScalarDictionary);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Scalar<T> Scalar()
    {
        return ScalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Scalar<T> GetBasisBladeScalar(IndexSet basisBlade)
    {
        return _idScalarDictionary.TryGetValue(basisBlade, out var scalar)
            ? ScalarProcessor.ScalarFromValue(scalar)
            : ScalarProcessor.Zero;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool TryGetScalarValue(out T scalar)
    {
        scalar = ScalarProcessor.ZeroValue;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool TryGetBasisBladeScalarValue(IndexSet basisBlade, out T scalar)
    {
        if (basisBlade.Count == Grade && _idScalarDictionary.TryGetValue(basisBlade, out scalar))
            return true;

        scalar = ScalarProcessor.ZeroValue;
        return false;
    }


    public override IEnumerable<KeyValuePair<XGaBasisBlade, T>> BasisScalarPairs
    {
        get
        {
            return _idScalarDictionary.Select(p =>
                new KeyValuePair<XGaBasisBlade, T>(
                    Processor.BasisBlade(p.Key),
                    p.Value
                )
            );
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Simplify()
    {
        return IsZero
            ? Processor.ScalarZero
            : this;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivector<T> ToGradedMultivector()
    {
        if (IsZero)
            return Processor.GradedMultivectorZero;

        var gradeKVectorDictionary = new SingleItemDictionary<int, XGaKVector<T>>(
            Grade,
            this
        );

        return new XGaGradedMultivector<T>(Processor, gradeKVectorDictionary);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivector<T> ToUniformMultivector()
    {
        return ToComposer().GetUniformMultivector();
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

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> ReflectOn(XGaKVector<T> subspace)
    {
        Debug.Assert(subspace.IsNearBlade());

        return subspace
            .Gp(this)
            .Gp(subspace.Inverse())
            .GetHigherKVectorPart(Grade);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> ReflectDirectOnDirect(XGaKVector<T> subspace)
    {
        var mv1 = ReflectOn(subspace);

        var n = Grade * (subspace.Grade + 1);

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> ReflectDirectOnDual(XGaKVector<T> subspace)
    {
        var mv1 = ReflectOn(subspace);

        var n = Grade * subspace.Grade;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> ReflectDualOnDirect(XGaKVector<T> subspace, int vSpaceDimensions)
    {
        var mv1 = ReflectOn(subspace);

        var n = (Grade + 1) * (subspace.Grade + 1) + vSpaceDimensions - 1;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> ReflectDualOnDual(XGaKVector<T> subspace)
    {
        var mv1 = ReflectOn(subspace);

        var n = (Grade + 1) * subspace.Grade;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> ProjectOn(XGaKVector<T> subspace, bool useSubspaceInverse = false)
    {
        Debug.Assert(subspace.IsNearBlade());
        
        var subspaceInverse = 
            useSubspaceInverse 
                ? subspace.PseudoInverse() 
                : subspace;

        return Fdp(subspaceInverse).Gp(subspace).GetHigherKVectorPart(Grade);
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