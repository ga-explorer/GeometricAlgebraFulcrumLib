using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using GAFuLCore.Structures;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;

public sealed partial class RGaFloat64HigherKVector :
    RGaFloat64KVector
{
    private readonly IReadOnlyDictionary<ulong, double> _idScalarDictionary;


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

    public override IEnumerable<RGaBasisBlade> BasisBlades
        => _idScalarDictionary.Keys.Select(Processor.CreateBasisBlade);

    public override IEnumerable<ulong> Ids 
        => _idScalarDictionary.Keys;

    public override IEnumerable<double> Scalars
        => _idScalarDictionary.Values;

    public override IEnumerable<KeyValuePair<ulong, double>> IdScalarPairs
        => _idScalarDictionary;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaFloat64HigherKVector(RGaFloat64Processor metric, int grade)
        : base(metric)
    {
        if (grade < 3)
            throw new ArgumentOutOfRangeException(nameof(grade));

        _idScalarDictionary = new EmptyDictionary<ulong, double>();

        Grade = grade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaFloat64HigherKVector(RGaFloat64Processor metric, KeyValuePair<ulong, double> basisScalarPair)
        : base(metric)
    {
        var grade = basisScalarPair.Key.Grade();

        if (grade < 3)
            throw new ArgumentOutOfRangeException(nameof(grade));

        _idScalarDictionary =
            new SingleItemDictionary<ulong, double>(basisScalarPair);

        Grade = grade;

        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaFloat64HigherKVector(RGaFloat64Processor metric, int grade, IReadOnlyDictionary<ulong, double> indexScalarDictionary)
        : base(metric)
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
        return _idScalarDictionary.IsValidKVectorDictionary(Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IReadOnlyDictionary<ulong, double> GetIdScalarDictionary()
    {
        return _idScalarDictionary;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsKey(ulong key)
    {
        return !IsZero && _idScalarDictionary.ContainsKey(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar GetScalarPart()
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Vector GetVectorPart()
    {
        return Processor.VectorZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Vector GetVectorPart(Func<int, bool> filterFunc)
    {
        return Processor.VectorZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Vector GetVectorPart(Func<double, bool> filterFunc)
    {
        return Processor.VectorZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Vector GetVectorPart(Func<int, double, bool> filterFunc)
    {
        return Processor.VectorZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Bivector GetBivectorPart()
    {
        return Processor.BivectorZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64HigherKVector GetHigherKVectorPart(int grade)
    {
        return grade == Grade
            ? this
            : Processor.HigherKVectorZero(grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64HigherKVector GetPart(Func<ulong, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Key)
            ).ToDictionary();

        return Processor.HigherKVector(Grade, idScalarDictionary);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64HigherKVector GetPart(Func<double, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Value)
            ).ToDictionary();

        return Processor.HigherKVector(Grade, idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64HigherKVector GetPart(Func<ulong, double, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Key, p.Value)
            ).ToDictionary();

        return Processor.HigherKVector(Grade, idScalarDictionary);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64HigherKVector RemoveSmallTerms(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        if (Count <= 1) return this;

        var scalarThreshold = 
            zeroEpsilon.Abs() * Scalars.Max(s => s.Abs());

        return GetPart((double s) => 
            s <= -scalarThreshold || s >= scalarThreshold
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double Scalar()
    {
        return 0d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetBasisBladeScalar(ulong basisBlade)
    {
        return _idScalarDictionary.TryGetValue(basisBlade, out var scalar)
            ? scalar
            : 0d;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool TryGetScalarValue(out double scalar)
    {
        scalar = 0d;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool TryGetBasisBladeScalarValue(ulong basisBlade, out double scalar)
    {
        if (basisBlade.Grade() == Grade && _idScalarDictionary.TryGetValue(basisBlade, out scalar))
            return true;

        scalar = 0d;
        return false;
    }


    public override IEnumerable<KeyValuePair<RGaBasisBlade, double>> BasisScalarPairs
    {
        get
        {
            return _idScalarDictionary.Select(p =>
                new KeyValuePair<RGaBasisBlade, double>(
                    Processor.CreateBasisBlade(p.Key),
                    p.Value
                )
            );
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector Simplify()
    {
        return IsZero
            ? Processor.ScalarZero
            : this;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return IdScalarPairs
            .OrderBy(p => p.Key)
            .Select(p => $"({p.Value:G}){BasisBladeIdToString(p.Key)}")
            .ConcatenateText(" + ");
    }
}