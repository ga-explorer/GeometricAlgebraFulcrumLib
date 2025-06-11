using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Combibnations;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;

public abstract partial class XGaFloat64KVector :
    XGaFloat64Multivector
{
    public abstract int Grade { get; }

    public ulong KvSpaceDimensions
    {
        get
        {
            if (IsZero) return 1;

            return Grade switch
            {
                0 => 1,
                1 => (ulong)VSpaceDimensions,
                2 => (ulong)VSpaceDimensions * ((ulong)VSpaceDimensions - 1UL),
                _ => VSpaceDimensions.GetBinomialCoefficient(Grade)
            };
        }
    }


    protected XGaFloat64KVector(XGaFloat64Processor processor)
        : base(processor)
    {
    }


    
    public override bool IsScalar()
    {
        return IsZero || Grade == 0;
    }

    
    public override bool IsVector()
    {
        return IsZero || Grade == 1;
    }

    
    public override bool IsBivector()
    {
        return IsZero || Grade == 2;
    }

    
    public override bool IsTrivector()
    {
        return IsZero || Grade == 3;
    }

    
    public override bool IsKVector(int grade)
    {
        return IsZero || Grade == grade;
    }

    
    public override bool IsOdd()
    {
        return Grade.IsOdd();
    }

    
    public override bool IsOdd(int maxGrade)
    {
        return Grade.IsOdd(maxGrade);
    }

    
    public override bool IsEven()
    {
        return Grade.IsEven();
    }

    
    public override bool IsEven(int maxGrade)
    {
        return Grade.IsEven(maxGrade);
    }


    
    public override bool ContainsScalarPart()
    {
        return !IsZero && Grade == 0;
    }

    
    public override bool ContainsVectorPart()
    {
        return !IsZero && Grade == 1;
    }

    
    public override bool ContainsBivectorPart()
    {
        return !IsZero && Grade == 2;
    }

    
    public override bool ContainsKVectorPart(int grade)
    {
        return !IsZero && Grade == grade;
    }

    
    public override bool ContainsOddPart()
    {
        return !IsZero && Grade.IsOdd();
    }

    
    public override bool ContainsOddPart(int maxGrade)
    {
        return !IsZero && Grade.IsOdd(maxGrade);
    }

    
    public override bool ContainsEvenPart()
    {
        return !IsZero && Grade.IsEven();
    }

    
    public override bool ContainsEvenPart(int maxGrade)
    {
        return !IsZero && Grade.IsEven(maxGrade);
    }


    
    public override int GetMinGrade()
    {
        return IsZero ? 0 : Grade;
    }

    
    public override int GetMaxGrade()
    {
        return IsZero ? 0 : Grade;
    }

    
    public override int GetKVectorCount()
    {
        return IsZero ? 0 : 1;
    }


    
    public override XGaFloat64KVector GetKVectorPart(int grade)
    {
        return grade == Grade
            ? this
            : Processor.KVectorZero(grade);
    }

    
    public override XGaFloat64KVector GetFirstKVectorPart()
    {
        return IsZero
            ? Processor.ScalarZero
            : this;
    }

    
    public override XGaFloat64Multivector GetEvenPart()
    {
        return IsEven()
            ? this
            : Processor.ScalarZero;
    }

    
    public override XGaFloat64Multivector GetEvenPart(int maxGrade)
    {
        return IsEven(maxGrade)
            ? this
            : Processor.ScalarZero;
    }

    
    public override XGaFloat64Multivector GetOddPart()
    {
        return IsOdd()
            ? this
            : Processor.ScalarZero;
    }

    
    public override XGaFloat64Multivector GetOddPart(int maxGrade)
    {
        return IsOdd(maxGrade)
            ? this
            : Processor.ScalarZero;
    }

    
    public override IEnumerable<XGaFloat64KVector> GetKVectorParts()
    {
        if (!IsZero) yield return this;
    }

    public abstract IReadOnlyDictionary<IndexSet, double> GetIdScalarDictionary();

    
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
        // Find basis blade with the largest scalar magnitude in the current blade
        var maxId = IndexSet.EmptySet;
        var maxScalar = 0d;

        foreach (var (id, scalar) in IdScalarTuples)
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
                .ToArray();

        return BladeToVectors(probeVectors);
    }

    
    public IReadOnlyList<XGaFloat64Vector> BladeToVectors(params int[] probeBasisVectorIndices)
    {
        var probeVectors =
            probeBasisVectorIndices
                .Select(index => Processor.VectorTerm(index))
                .ToArray();

        return BladeToVectors(probeVectors);
    }

    /// <summary>
    /// Analyze this k-vector, assumed to be a blade, into a set of orthonormal
    /// vectors, where their outer product is equal to the original blade, up to
    /// a scalar factor
    /// </summary>
    /// <returns></returns>
    
    public IReadOnlyList<XGaFloat64Vector> BladeToVectors(IEnumerable<int> probeBasisVectorIndices)
    {
        var probeVectors =
            probeBasisVectorIndices
                .Select(index => Processor.VectorTerm(index))
                .ToArray();

        return BladeToVectors(probeVectors);
    }

    public IReadOnlyList<XGaFloat64Vector> BladeToVectors(IReadOnlyList<XGaFloat64Vector> probeVectors)
    {
        if (IsZero || Grade == 0)
            return [];

        if (this is XGaFloat64Vector vectorBlade)
            return [vectorBlade];

        var vectorList = new List<XGaFloat64Vector>(Grade);

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


    
    public bool IsNearBlade(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return Gp(Reverse())
            .GetKVectorParts()
            .All(kv1 =>
                kv1.Grade == 0 || kv1.IsNearZero(zeroEpsilon)
            );
    }


    
    public new XGaFloat64KVector GetPart(Func<IndexSet, bool> filterFunc)
    {
        return this switch
        {
            XGaFloat64Scalar s => s.GetPart(filterFunc),
            XGaFloat64Vector v => v.GetPart(filterFunc),
            XGaFloat64Bivector bv => bv.GetPart(filterFunc),
            XGaFloat64HigherKVector kv => kv.GetPart(filterFunc),
            _ => throw new InvalidOperationException()
        };
    }

    
    public new XGaFloat64KVector GetPart(Func<double, bool> filterFunc)
    {
        return this switch
        {
            XGaFloat64Scalar s => s.GetPart(filterFunc),
            XGaFloat64Vector v => v.GetPart(filterFunc),
            XGaFloat64Bivector bv => bv.GetPart(filterFunc),
            XGaFloat64HigherKVector kv => kv.GetPart(filterFunc),
            _ => throw new InvalidOperationException()
        };
    }

    
    public new XGaFloat64KVector GetPart(Func<IndexSet, double, bool> filterFunc)
    {
        return this switch
        {
            XGaFloat64Scalar s => s.GetPart(filterFunc),
            XGaFloat64Vector v => v.GetPart(filterFunc),
            XGaFloat64Bivector bv => bv.GetPart(filterFunc),
            XGaFloat64HigherKVector kv => kv.GetPart(filterFunc),
            _ => throw new InvalidOperationException()
        };
    }
    
    
    public new XGaFloat64KVector RemoveSmallTerms(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return this switch
        {
            XGaFloat64Scalar s => s,
            XGaFloat64Vector v => v.RemoveSmallTerms(zeroEpsilon),
            XGaFloat64Bivector bv => bv.RemoveSmallTerms(zeroEpsilon),
            XGaFloat64HigherKVector kv => kv.RemoveSmallTerms(zeroEpsilon),
            _ => throw new InvalidOperationException()
        };
    }

    
    public override string ToString()
    {
        if (IsZero) return "0";

        return BasisScalarPairs
            .OrderBy(p => p.Key.Id)
            .Select(p => $"{p.Value:G} {p.Key}")
            .ConcatenateText(" + ");
    }
}