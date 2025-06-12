using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

public abstract partial class XGaKVector<T> :
    XGaMultivector<T>
{
    public abstract int Grade { get; }

    public ulong KvSpaceDimensions
        => IsZero ? 1 : VSpaceDimensions.GetBinomialCoefficient(Grade);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected XGaKVector(XGaProcessor<T> processor)
        : base(processor)
    {
    }
    
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsScalar()
    {
        return IsZero || Grade == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsVector()
    {
        return IsZero || Grade == 1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsBivector()
    {
        return IsZero || Grade == 2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsKVector(int grade)
    {
        return IsZero || Grade == grade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsOdd()
    {
        return Grade.IsOdd();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsOdd(int maxGrade)
    {
        return Grade.IsOdd(maxGrade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsEven()
    {
        return Grade.IsEven();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsEven(int maxGrade)
    {
        return Grade.IsEven(maxGrade);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearBlade()
    {
        return Gp(Reverse())
            .GetKVectorParts()
            .All(kv1 => 
                kv1.Grade == 0 || kv1.IsNearZero()
            );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsScalarPart()
    {
        return !IsZero && Grade == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsVectorPart()
    {
        return !IsZero && Grade == 1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsBivectorPart()
    {
        return !IsZero && Grade == 2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsKVectorPart(int grade)
    {
        return !IsZero && Grade == grade;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsOddPart()
    {
        return !IsZero && Grade.IsOdd();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsOddPart(int maxGrade)
    {
        return !IsZero && Grade.IsOdd(maxGrade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsEvenPart()
    {
        return !IsZero && Grade.IsEven();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsEvenPart(int maxGrade)
    {
        return !IsZero && Grade.IsEven(maxGrade);
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetMinGrade()
    {
        return IsZero ? 0 : Grade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetMaxGrade()
    {
        return IsZero ? 0 : Grade;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetKVectorCount()
    {
        return IsZero ? 0 : 1;
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> GetKVectorPart(int grade)
    {
        return grade == Grade
            ? this
            : Processor.KVectorZero(grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> GetFirstKVectorPart()
    {
        return IsZero
            ? Processor.ScalarZero
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> GetEvenPart()
    {
        return IsEven()
            ? this
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> GetEvenPart(int maxGrade)
    {
        return IsEven(maxGrade)
            ? this
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> GetOddPart()
    {
        return IsOdd()
            ? this
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> GetOddPart(int maxGrade)
    {
        return IsOdd(maxGrade)
            ? this
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IEnumerable<XGaKVector<T>> GetKVectorParts()
    {
        if (!IsZero) yield return this;
    }

    public abstract IReadOnlyDictionary<IndexSet, T> GetIdScalarDictionary();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<ulong, T>> GetKVectorArrayItems()
    {
        return IdScalarPairs.Select(
            term => 
                new KeyValuePair<ulong, T>(
                    term.Key.BasisBladeIdToIndex(), 
                    term.Value
                )
        );
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> GetPart(Func<IndexSet, bool> filterFunc)
    {
        return this switch
        {
            XGaScalar<T> s => s.GetPart(filterFunc),
            XGaVector<T> v => v.GetPart(filterFunc),
            XGaBivector<T> bv => bv.GetPart(filterFunc),
            XGaHigherKVector<T> kv => kv.GetPart(filterFunc),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> GetPart(Func<T, bool> filterFunc)
    {
        return this switch
        {
            XGaScalar<T> s => s.GetPart(filterFunc),
            XGaVector<T> v => v.GetPart(filterFunc),
            XGaBivector<T> bv => bv.GetPart(filterFunc),
            XGaHigherKVector<T> kv => kv.GetPart(filterFunc),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> GetPart(Func<IndexSet, T, bool> filterFunc)
    {
        return this switch
        {
            XGaScalar<T> s => s.GetPart(filterFunc),
            XGaVector<T> v => v.GetPart(filterFunc),
            XGaBivector<T> bv => bv.GetPart(filterFunc),
            XGaHigherKVector<T> kv => kv.GetPart(filterFunc),
            _ => throw new InvalidOperationException()
        };
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