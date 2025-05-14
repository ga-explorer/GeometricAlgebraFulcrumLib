using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Combinations;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;

public abstract partial class XGaFloat64KVector :
    XGaFloat64Multivector
{
    public abstract int Grade { get; }

    public ulong KvSpaceDimensions
        => IsZero ? 1 : VSpaceDimensions.GetBinomialCoefficient(Grade);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected XGaFloat64KVector(XGaFloat64Processor processor)
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
    public override bool IsTrivector()
    {
        return IsZero || Grade == 3;
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
    public override XGaFloat64KVector GetKVectorPart(int grade)
    {
        return grade == Grade
            ? this
            : Processor.KVectorZero(grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector GetFirstKVectorPart()
    {
        return IsZero 
            ? Processor.ScalarZero
            : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector GetEvenPart()
    {
        return IsEven()
            ? this
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector GetEvenPart(int maxGrade)
    {
        return IsEven(maxGrade)
            ? this
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector GetOddPart()
    {
        return IsOdd()
            ? this
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector GetOddPart(int maxGrade)
    {
        return IsOdd(maxGrade)
            ? this
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IEnumerable<XGaFloat64KVector> GetKVectorParts()
    {
        if (!IsZero) yield return this;
    }
        
    public abstract IReadOnlyDictionary<IndexSet, double> GetIdScalarDictionary();
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<ulong, double>> GetKVectorArrayItems()
    {
        return IdScalarPairs.Select(
            term => 
                new KeyValuePair<ulong, double>(
                    term.Key.BasisBladeIdToIndex(), 
                    term.Value
                )
        );
    }

        
    /// <summary>
    /// Analyze this k-vector, assumed to be a blade, into a set of orthonormal
    /// vectors, where their outer product is equal to the original blade, up to
    /// a scalar factor
    /// </summary>
    /// <returns></returns>
    public IReadOnlyList<XGaFloat64Vector> BladeToVectors()
    {
        // Find basis blade with largest scalar magnitude in the current blade
        IndexSet maxId = IndexSet.EmptySet;
        var maxScalar = 0d;

        foreach (var (id, scalar) in IdScalarPairs)
        {
            var scalar1 = Math.Abs(scalar);

            if (scalar1 <= maxScalar) 
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
    public IReadOnlyList<XGaFloat64Vector> BladeToVectors(params int[] probeBasisVectorIndices)
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
    public IReadOnlyList<XGaFloat64Vector> BladeToVectors(IEnumerable<int> probeBasisVectorIndices)
    {
        var probeVectors = 
            probeBasisVectorIndices
                .Select(index => Processor.VectorTerm(index))
                .ToImmutableArray();

        return BladeToVectors(probeVectors);
    }

    public IReadOnlyList<XGaFloat64Vector> BladeToVectors(IReadOnlyList<XGaFloat64Vector> probeVectors)
    {
        if (IsZero || Grade == 0)
            return Array.Empty<XGaFloat64Vector>();

        if (this is XGaFloat64Vector vectorBlade)
            return new[] { vectorBlade };

        var vectorList = new List<XGaFloat64Vector>(Grade);

        // All computations are done assuming Euclidean space,
        // independent from the actual metric
            
        // Normalize the current blade
        var oldBlade = this.DivideByENorm();

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
}