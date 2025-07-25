﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND.Rotation;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.SpaceND;

public sealed class LinFloat64Vector :
    IReadOnlyDictionary<int, double>,
    IFloat64LinearAlgebraElement
{
    public static LinFloat64Vector Zero { get; }
        = new LinFloat64Vector();


    
    public static LinFloat64Vector Create(KeyValuePair<int, double> basisScalarPair)
    {
        return new LinFloat64Vector(basisScalarPair);
    }

    
    public static LinFloat64Vector Create(double item0, double item1)
    {
        var scalarArray = new[]
        {
            item0,
            item1
        }.CreateValidLinVectorDictionary();

        return new LinFloat64Vector(scalarArray);
    }

    
    public static LinFloat64Vector Create(params double[] scalarList)
    {
        var scalarArray = 
            scalarList.CreateValidLinVectorDictionary();
        
        return new LinFloat64Vector(scalarArray);
    }

    
    public static LinFloat64Vector Create(IReadOnlyList<double> scalarList)
    {
        var scalarArray = 
            scalarList.ToArray().CreateValidLinVectorDictionary();

        return new LinFloat64Vector(scalarArray);
    }

    
    public static LinFloat64Vector Create(IEnumerable<double> scalarList)
    {
        var scalarArray = 
            scalarList.ToArray().CreateValidLinVectorDictionary();
        
        return new LinFloat64Vector(scalarArray);
    }
    
    
    internal static LinFloat64Vector Create(IReadOnlyDictionary<int, double> basisScalarPair)
    {
        return new LinFloat64Vector(basisScalarPair);
    }


    
    public static LinFloat64Vector Create(IPair<double> tuple)
    {
        var scalarArray = 
            tuple.GetItemArray().CreateValidLinVectorDictionary();

        return new LinFloat64Vector(scalarArray);
    }
    
    
    public static LinFloat64Vector Create(ITriplet<double> tuple)
    {
        var scalarArray = 
            tuple.GetItemArray().CreateValidLinVectorDictionary();

        return new LinFloat64Vector(scalarArray);
    }
    
    
    public static LinFloat64Vector Create(IQuad<double> tuple)
    {
        var scalarArray = 
            tuple.GetItemArray().CreateValidLinVectorDictionary();

        return new LinFloat64Vector(scalarArray);
    }
    
    
    public static LinFloat64Vector Create(IQuint<double> tuple)
    {
        var scalarArray = 
            tuple.GetItemArray().CreateValidLinVectorDictionary();

        return new LinFloat64Vector(scalarArray);
    }
    
    
    public static LinFloat64Vector Create(IHexad<double> tuple)
    {
        var scalarArray = 
            tuple.GetItemArray().CreateValidLinVectorDictionary();

        return new LinFloat64Vector(scalarArray);
    }
    

    
    public static LinFloat64Vector CreateBasis(int index)
    {
        var basisScalarDictionary =
            new SingleItemDictionary<int, double>(index, 1);

        return new LinFloat64Vector(basisScalarDictionary);
    }

    
    public static LinFloat64Vector CreateScaledBasis(int index, double scalar)
    {
        if (scalar.IsZero())
            return Zero;

        var basisScalarDictionary =
            new SingleItemDictionary<int, double>(index, scalar);

        return new LinFloat64Vector(basisScalarDictionary);
    }


    
    public static LinFloat64Vector operator -(LinFloat64Vector mv1)
    {
        return mv1.Negative();
    }


    
    public static LinFloat64Vector operator +(LinFloat64Vector mv1, LinFloat64Vector vector2)
    {
        return mv1.Add(vector2);
    }

    
    public static LinFloat64Vector operator -(LinFloat64Vector mv1, LinFloat64Vector vector2)
    {
        return mv1.Subtract(vector2);
    }


    
    public static LinFloat64Vector operator *(LinFloat64Vector mv1, IntegerSign vector2)
    {
        if (vector2.IsZero || mv1.IsZero)
            return Zero;

        return vector2.IsPositive ? mv1 : mv1.Negative();
    }

    
    public static LinFloat64Vector operator *(IntegerSign mv1, LinFloat64Vector vector2)
    {
        if (mv1.IsZero || vector2.IsZero)
            return Zero;

        return mv1.IsPositive ? vector2 : vector2.Negative();
    }

    
    public static LinFloat64Vector operator *(LinFloat64Vector mv1, int vector2)
    {
        return mv1.Times(vector2);
    }

    
    public static LinFloat64Vector operator *(int mv1, LinFloat64Vector vector2)
    {
        return vector2.Times(mv1);
    }

    
    public static LinFloat64Vector operator *(LinFloat64Vector mv1, uint vector2)
    {
        return mv1.Times(vector2);
    }

    
    public static LinFloat64Vector operator *(uint mv1, LinFloat64Vector vector2)
    {
        return vector2.Times(mv1);
    }

    
    public static LinFloat64Vector operator *(LinFloat64Vector mv1, long vector2)
    {
        return mv1.Times(vector2);
    }

    
    public static LinFloat64Vector operator *(long mv1, LinFloat64Vector vector2)
    {
        return vector2.Times(mv1);
    }

    
    public static LinFloat64Vector operator *(LinFloat64Vector mv1, ulong vector2)
    {
        return mv1.Times(vector2);
    }

    
    public static LinFloat64Vector operator *(ulong mv1, LinFloat64Vector vector2)
    {
        return vector2.Times(mv1);
    }

    
    public static LinFloat64Vector operator *(LinFloat64Vector mv1, float vector2)
    {
        return mv1.Times(vector2);
    }

    
    public static LinFloat64Vector operator *(float mv1, LinFloat64Vector vector2)
    {
        return vector2.Times(mv1);
    }

    
    public static LinFloat64Vector operator *(LinFloat64Vector mv1, double vector2)
    {
        return mv1.Times(vector2);
    }

    
    public static LinFloat64Vector operator *(double mv1, LinFloat64Vector vector2)
    {
        return vector2.Times(mv1);
    }


    
    public static LinFloat64Vector operator /(LinFloat64Vector mv1, IntegerSign vector2)
    {
        if (vector2.IsZero)
            throw new DivideByZeroException();

        return vector2.IsPositive ? mv1 : mv1.Negative();
    }

    
    public static LinFloat64Vector operator /(LinFloat64Vector mv1, int vector2)
    {
        return mv1.Divide(vector2);
    }

    
    public static LinFloat64Vector operator /(LinFloat64Vector mv1, uint vector2)
    {
        return mv1.Divide(vector2);
    }

    
    public static LinFloat64Vector operator /(LinFloat64Vector mv1, long vector2)
    {
        return mv1.Divide(vector2);
    }

    
    public static LinFloat64Vector operator /(LinFloat64Vector mv1, ulong vector2)
    {
        return mv1.Divide(vector2);
    }

    
    public static LinFloat64Vector operator /(LinFloat64Vector mv1, float vector2)
    {
        return mv1.Divide(vector2);
    }

    
    public static LinFloat64Vector operator /(LinFloat64Vector mv1, double vector2)
    {
        return mv1.Divide(vector2);
    }


    private readonly IReadOnlyDictionary<int, double> _indexScalarDictionary;


    public string VectorClassName
        => "Float64 Linear Vector";

    /// <summary>
    /// The dimensions of the base vector space, dynamically determined from
    /// stored terms
    /// </summary>
    public int VSpaceDimensions { get; }

    public int Count
        => _indexScalarDictionary.Count;

    public int FirstIndex
        => IsZero ? -1 : Indices.Min();

    public int LastIndex
        => IsZero ? -1 : Indices.Max();

    public bool IsZero
        => _indexScalarDictionary.Count == 0;

    public double X
        => this[0];

    public double Y
        => this[1];

    public double Z
        => this[2];

    public double W
        => this[3];

    public double this[int index]
        => GetTermScalar(index);

    public IEnumerable<int> Indices
        => _indexScalarDictionary.Keys;

    public IEnumerable<double> Scalars
        => _indexScalarDictionary.Values;

    public IEnumerable<KeyValuePair<int, double>> IndexScalarPairs
        => _indexScalarDictionary;
    
    public IEnumerable<Tuple<int, double>> IndexScalarTuples
        => _indexScalarDictionary.Select(
            p => new Tuple<int, double>(p.Key, p.Value)
        );
    
    public IEnumerable<KeyValuePair<LinBasisVector, double>> BasisScalarPairs
    {
        get
        {
            return _indexScalarDictionary.Select(p =>
                new KeyValuePair<LinBasisVector, double>(
                    p.Key.ToLinBasisVector(),
                    p.Value
                )
            );
        }
    }

    public IEnumerable<int> Keys
        => _indexScalarDictionary.Keys;

    public IEnumerable<double> Values
        => _indexScalarDictionary.Values;


    
    private LinFloat64Vector()
    {
        VSpaceDimensions = 0;
        _indexScalarDictionary = new EmptyDictionary<int, double>();

        Debug.Assert(IsValid());
    }

    
    private LinFloat64Vector(KeyValuePair<int, double> basisScalarPair)
    {
        if (basisScalarPair.Value.IsZero())
        {
            VSpaceDimensions = 0;
            _indexScalarDictionary = new EmptyDictionary<int, double>();
        }
        else
        {
            VSpaceDimensions = 1;
            _indexScalarDictionary = new SingleItemDictionary<int, double>(basisScalarPair);
        }
        
        Debug.Assert(IsValid());
    }

    
    private LinFloat64Vector(IReadOnlyDictionary<int, double> idScalarDictionary)
    {
        VSpaceDimensions = idScalarDictionary.Count == 0 ? 0 : idScalarDictionary.Keys.Max() + 1;
        _indexScalarDictionary = idScalarDictionary;

        Debug.Assert(IsValid());
    }


    
    public bool IsValid()
    {
        return _indexScalarDictionary.Count == 0
            ? VSpaceDimensions == 0
            : _indexScalarDictionary.IsValidLinVectorDictionary() &&
              VSpaceDimensions == _indexScalarDictionary.Keys.Max() + 1;
    }


    
    public bool IsNearZero(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return ENorm().IsNearZero(zeroEpsilon);
    }

    
    public bool IsNearUnit(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return ENormSquared().IsNearOne(zeroEpsilon);
    }

    
    public bool IsNearOrthonormalWith(LinFloat64Vector vector, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return IsNearUnit(zeroEpsilon) &&
               vector.IsNearUnit(zeroEpsilon) &&
               ESp(vector).IsNearZero(zeroEpsilon);
    }

    
    public bool IsNearOrthonormalWithUnit(LinFloat64Vector vector, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        Debug.Assert(
            vector.IsNearUnit(zeroEpsilon)
        );

        return IsNearUnit(zeroEpsilon) &&
               ESp(vector).IsNearZero(zeroEpsilon);
    }

    
    public bool IsNearParallelTo(LinBasisVector vector, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        Debug.Assert(
            vector.IsNonZero
        );

        return GetAngleCosWithUnit(vector).Abs().IsNearOne(zeroEpsilon);
    }

    
    public bool IsNearParallelTo(LinFloat64Vector vector, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return GetAngleCos(vector).Abs().IsNearOne(zeroEpsilon);
    }

    
    public bool IsNearParallelToUnit(LinBasisVector vector, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        Debug.Assert(
            vector.IsNonZero
        );

        return GetAngleCosWithUnit(vector).Abs().IsNearOne(zeroEpsilon);
    }

    
    public bool IsNearParallelToUnit(LinFloat64Vector vector, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        Debug.Assert(
            vector.IsNearUnit(zeroEpsilon)
        );

        return GetAngleCosWithUnit(vector).Abs().IsNearOne(zeroEpsilon);
    }

    
    public bool IsNearOppositeTo(LinFloat64Vector vector, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return GetAngleCos(vector).IsNearMinusOne(zeroEpsilon);
    }

    
    public bool IsNearOppositeToUnit(LinBasisVector vector, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return GetAngleCosWithUnit(vector).IsNearMinusOne(zeroEpsilon);
    }

    
    public bool IsNearOppositeToUnit(LinFloat64Vector vector, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return GetAngleCosWithUnit(vector).IsNearMinusOne(zeroEpsilon);
    }

    
    public bool IsNearOrthogonalTo(LinFloat64Vector vector, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return ESp(vector).IsNearZero(zeroEpsilon);
    }

    
    public bool IsVectorBasis(int basisIndex)
    {
        if (_indexScalarDictionary.Count != 1)
            return false;

        var (index, scalar) = _indexScalarDictionary.First().ToTuple();

        return index == basisIndex && scalar.IsOne();
    }

    
    public bool IsNearVectorBasis(int basisIndex, double zeroEpsilon = 1e-12d)
    {
        return _indexScalarDictionary.Aggregate(
            0d,
            (norm, indexScalarPair) =>
                norm +
                (indexScalarPair.Key == basisIndex
                    ? (indexScalarPair.Value - 1d).Square()
                    : indexScalarPair.Value.Square())
        ).IsNearZero(zeroEpsilon);
    }


    
    public LinFloat64Vector GetCopyByIndex(Func<int, bool> filterFunc)
    {
        return new LinFloat64Vector(
            _indexScalarDictionary
                .Where(p => filterFunc(p.Key))
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                ).ToSimpleDictionary()
        );
    }

    
    public LinFloat64Vector GetCopyByScalar(Func<double, bool> filterFunc)
    {
        return new LinFloat64Vector(
            _indexScalarDictionary
                .Where(p => filterFunc(p.Value))
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                ).ToSimpleDictionary()
        );
    }

    
    public LinFloat64Vector GetCopyByIndexScalar(Func<int, double, bool> filterFunc)
    {
        return new LinFloat64Vector(
            _indexScalarDictionary
                .Where(p => filterFunc(p.Key, p.Value))
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                ).ToSimpleDictionary()
        );
    }

    
    public bool ContainsKey(int key)
    {
        return !IsZero && _indexScalarDictionary.ContainsKey(key);
    }

    
    public bool TryGetValue(int key, out double value)
    {
        return _indexScalarDictionary.TryGetValue(key, out value);
    }


    
    public IReadOnlyDictionary<int, double> GetIndexScalarDictionary()
    {
        return _indexScalarDictionary;
    }

    
    public IEnumerable<LinFloat64VectorTerm> GetTerms()
    {
        return _indexScalarDictionary.Select(p =>
            p.Key.CreateLinTerm(p.Value)
        );
    }

    public IEnumerable<LinBasisVector> BasisVectors
        => _indexScalarDictionary.Keys.Select(index => index.ToLinBasisVector());

    
    public double GetTermScalar(int basisVectorIndex)
    {
        return _indexScalarDictionary.GetValueOrDefault(basisVectorIndex, 0d);
    }

    
    public bool TryGetTermScalar(int basisVectorIndex, out double scalar)
    {
        if (_indexScalarDictionary.TryGetValue(basisVectorIndex, out scalar))
            return true;

        scalar = 0d;
        return false;
    }

    
    public double GetComponent(LinBasisVector axis)
    {
        return GetTermScalar(axis.Index) * axis.Sign;
    }

    
    public double GetComponent(int axisIndex, bool axisNegative = false)
    {
        var component = GetTermScalar(axisIndex);

        return axisNegative ? -component : component;
    }

    
    public LinFloat64Vector GetSubVector(int vSpaceDimensions)
    {
        return _indexScalarDictionary
            .Where(p => p.Key < vSpaceDimensions)
            .ToDictionary()
            .CreateLinVector();
    }


    
    public LinFloat64Vector Negative()
    {
        return IsZero ? this : MapScalars(s => -s);
    }

    
    public LinFloat64Vector Times(double scalar)
    {
        if (IsZero || scalar.IsZero())
            return Zero;

        return scalar.IsOne()
            ? this
            : MapScalars(s => s * scalar);
    }

    
    public LinFloat64Vector Divide(double scalar)
    {
        var s1 = 1d / scalar;

        return MapScalars(s => s * s1);
    }

    
    public LinFloat64Vector DivideByENormSquared()
    {
        var scalar = 1d / ENormSquared();

        return MapScalars(s => s * scalar);
    }

    
    public LinFloat64Vector DivideByENorm()
    {
        var scalar = 1d / ENorm();

        return MapScalars(s => s * scalar);
    }

    
    public double ENormSquared()
    {
        return IsZero
            ? 0
            : _indexScalarDictionary
                .Values
                .Select(s => s * s)
                .Sum();
    }

    
    public double ENorm()
    {
        return Math.Sqrt(ENormSquared());
    }

    
    public LinFloat64Vector Add(LinFloat64VectorTerm vector2)
    {
        if (IsZero)
            return vector2.ToLinVector();

        if (vector2.IsZero)
            return this;

        return LinFloat64VectorComposer.Create()
            .SetVector(this)
            .AddTerm(vector2)
            .GetVector();
    }

    
    public LinFloat64Vector Add(LinFloat64Vector vector2)
    {
        if (IsZero)
            return vector2;

        if (vector2.IsZero)
            return this;

        return LinFloat64VectorComposer.Create()
            .SetVector(this)
            .AddVector(vector2)
            .GetVector();
    }

    
    public LinFloat64Vector Subtract(LinBasisVector vector2)
    {
        if (IsZero)
            return vector2.Negative().ToLinVector();

        if (vector2.IsZero)
            return this;

        return LinFloat64VectorComposer.Create()
            .SetVector(this)
            .SubtractVector(vector2)
            .GetVector();
    }

    
    public LinFloat64Vector Subtract(LinFloat64VectorTerm vector2)
    {
        if (IsZero)
            return vector2.Negative().ToLinVector();

        if (vector2.IsZero)
            return this;

        return LinFloat64VectorComposer.Create()
            .SetVector(this)
            .SubtractTerm(vector2)
            .GetVector();
    }

    
    public LinFloat64Vector Subtract(LinFloat64Vector vector2)
    {
        if (IsZero)
            return vector2.Negative();

        if (vector2.IsZero)
            return this;

        return LinFloat64VectorComposer.Create()
            .SetVector(this)
            .SubtractVector(vector2)
            .GetVector();
    }
    
    
    public LinFloat64Vector ComponentTimes(LinFloat64Vector vector2)
    {
        if (IsZero || vector2.IsZero)
            return Zero;

        return LinFloat64VectorComposer
            .Create()
            .AddComponentTimesTerms(this, vector2)
            .GetVector();
    }

    
    public double ESp(LinBasisVector axis)
    {
        return GetTermScalar(axis.Index) * axis.Sign;
    }

    
    public double ESp(LinFloat64VectorTerm vector2)
    {
        var scalar2 = vector2.ScalarValue;

        return TryGetTermScalar(vector2.BasisVector.Index, out var scalar1)
            ? scalar1 * scalar2
            : 0d;
    }

    
    public double ESp(LinFloat64Vector vector2)
    {
        return Float64ScalarComposer
            .Create()
            .AddESpTerms(this, vector2)
            .ScalarValue;
    }

    
    public LinFloat64Vector RejectOnVector(LinBasisVector vector2)
    {
        if (vector2.IsZero)
            return this;

        if (!_indexScalarDictionary.ContainsKey(vector2.Index))
            return this;

        return LinFloat64VectorComposer.Create()
            .SetVector(this)
            .RemoveTerm(vector2.Index)
            .GetVector();
    }

    
    public LinFloat64Vector RejectOnUnitVector(LinBasisVector vector2)
    {
        Debug.Assert(vector2.IsNonZero);

        if (vector2.IsZero)
            return this;

        if (!_indexScalarDictionary.ContainsKey(vector2.Index))
            return this;

        return LinFloat64VectorComposer.Create()
            .SetVector(this)
            .RemoveTerm(vector2.Index)
            .GetVector();
    }

    public double[] ToScalarArray()
    {
        var scalarArray = new double[VSpaceDimensions];

        foreach (var (index, scalar) in _indexScalarDictionary.ToTuples())
            scalarArray[index] = scalar;

        return scalarArray;
    }

    public double[] ToScalarArray(int length)
    {
        Debug.Assert(
            length >= VSpaceDimensions
        );

        var scalarArray = new double[length];

        foreach (var (index, scalar) in _indexScalarDictionary.ToTuples())
            scalarArray[index] = scalar;

        return scalarArray;
    }

    
    public Vector<double> ToMathNetVector()
    {
        return Vector<double>.Build.DenseOfArray(
            ToScalarArray()
        );
    }

    
    public Vector<double> ToMathNetVector(int length)
    {
        return Vector<double>.Build.DenseOfArray(
            ToScalarArray(length)
        );
    }

    
    
    public XGaFloat64Vector ToXGaFloat64Vector()
    {
        var idScalarDictionary =
            Enumerable.ToDictionary(GetIndexScalarDictionary(), p => p.Key.ToUnitIndexSet(),
                p => p.Value
            );

        return XGaFloat64Processor.Euclidean.Vector(idScalarDictionary);
    }

    
    public XGaFloat64Vector ToXGaFloat64Vector(XGaFloat64Processor processor)
    {
        var idScalarDictionary =
            Enumerable.ToDictionary(GetIndexScalarDictionary(), p => p.Key.ToUnitIndexSet(),
                p => p.Value
            );

        return processor.Vector(idScalarDictionary);
    }
    
    
    public LinFloat64VectorComposer ToLinVectorComposer()
    {
        return LinFloat64VectorComposer.Create().SetVector(this);
    }

    
    public LinFloat64VectorComposer NegativeToLinVectorComposer()
    {
        return LinFloat64VectorComposer.Create().SetVectorNegative(this);
    }

    
    public LinFloat64VectorComposer ToLinVectorComposer(double scalingFactor)
    {
        return LinFloat64VectorComposer.Create().SetVector(this, scalingFactor);
    }

    
    
    public double GetAngleCos(LinFloat64Vector vector2)
    {
        var uuDot = ENormSquared();
        var vvDot = vector2.ENormSquared();
        var uvDot = ESp(vector2);

        var norm = (uuDot * vvDot).Sqrt();

        return norm.IsZero()
            ? 0d
            : Float64Utils.Clamp(uvDot / norm, -1, 1);
    }
    
    
    public double GetAngleCosWithUnit(LinBasisVector vector2)
    {
        Debug.Assert(
            vector2.Sign.IsNotZero
        );

        var uuDot = ENormSquared();
        var uvDot = ESp(vector2);

        var norm = uuDot.Sqrt();

        return norm.IsZero()
            ? 0d
            : Float64Utils.Clamp(uvDot / norm, -1, 1);
    }

    
    public double GetAngleCosWithUnit(LinFloat64Vector vector2)
    {
        Debug.Assert(
            vector2.IsNearUnit()
        );

        var uuDot = ENormSquared();
        var uvDot = ESp(vector2);

        var norm = uuDot.Sqrt();

        return norm.IsZero()
            ? 0d
            : Float64Utils.Clamp(uvDot / norm, -1, 1);
    }

    
    public double GetUnitVectorsAngleCos(LinFloat64Vector vector2)
    {
        return Float64Utils.Clamp(ESp(vector2), -1, 1);
    }

    
    
    public LinFloat64PolarAngle GetAngleWithUnit(LinBasisVector vector2)
    {
        return GetAngleCosWithUnit(vector2).CosToPolarAngle();
    }

    
    public LinFloat64PolarAngle GetAngleWithUnit(LinFloat64Vector vector2)
    {
        return GetAngleCosWithUnit(vector2).CosToPolarAngle();
    }

    
    public LinFloat64PolarAngle GetUnitVectorsAngle(LinFloat64Vector vector2)
    {
        return GetUnitVectorsAngleCos(vector2).CosToPolarAngle();
    }

    
    public LinFloat64PolarAngle GetAngle(LinFloat64Vector vector2, bool assumeUnitVectors)
    {
        var v12Sp = ESp(vector2);

        var angle = assumeUnitVectors
            ? v12Sp
            : v12Sp / (ENormSquared() * vector2.ENormSquared()).Sqrt();

        return angle.CosToPolarAngle();
    }

    
    public LinFloat64PolarAngle GetAngle(LinFloat64Vector vector2)
    {
        return GetAngleCos(vector2).CosToPolarAngle();
    }
    
    
    public LinFloat64UnilinearMap ToDiagonalLinUnilinearMap()
    {
        var indexVectorDictionary =
            this.ToDictionary(
                p => p.Key,
                p => p.Key.CreateLinVector(p.Value)
            );

        return indexVectorDictionary.ToLinUnilinearMap();
    }
    
    public double[] ToArray(int vSpaceDimensions)
    {
        if (vSpaceDimensions < VSpaceDimensions)
            throw new ArgumentException(nameof(vSpaceDimensions));

        var array = new double[vSpaceDimensions];

        foreach (var (index, scalar) in IndexScalarPairs.ToTuples())
            array[index] = scalar;

        return array;
    }


    
    public double VectorENorm()
    {
        return ENormSquared().Sqrt();
    }

    
    public LinFloat64Vector VectorEInverse()
    {
        return Divide(ESp(this));
    }


    
    public LinFloat64Vector MapScalars(Func<double, double> scalarMapping)
    {
        var termList =
            IndexScalarPairs.Select(
                term => new KeyValuePair<int, double>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return LinFloat64VectorComposer.Create()
            .SetTerms(termList)
            .GetVector();
    }

    
    public LinFloat64Vector MapScalars(Func<int, double, double> scalarMapping)
    {
        var termList =
            IndexScalarPairs.Select(
                term => new KeyValuePair<int, double>(
                    term.Key,
                    scalarMapping(term.Key, term.Value)
                )
            );

        return LinFloat64VectorComposer.Create()
            .AddTerms(termList)
            .GetVector();
    }

    
    public LinFloat64Vector MapBasisVectors(Func<int, int> basisMapping, bool simplify = true)
    {
        var termList =
            IndexScalarPairs.Select(
                term => new KeyValuePair<int, double>(
                    basisMapping(term.Key),
                    term.Value
                )
            );

        return LinFloat64VectorComposer.Create()
            .AddTerms(termList)
            .GetVector();
    }

    
    public LinFloat64Vector MapBasisVectors(Func<int, double, int> basisMapping, bool simplify = true)
    {
        var termList =
            IndexScalarPairs.Select(
                term => new KeyValuePair<int, double>(
                    basisMapping(term.Key, term.Value),
                    term.Value
                )
            );

        return LinFloat64VectorComposer.Create()
            .AddTerms(termList)
            .GetVector();
    }

    
    public LinFloat64Vector MapTerms(Func<int, double, KeyValuePair<int, double>> termMapping, bool simplify = true)
    {
        var termList =
            IndexScalarPairs.Select(
                term => termMapping(term.Key, term.Value)
            );

        return LinFloat64VectorComposer.Create()
            .AddTerms(termList)
            .GetVector();
    }


    
    public LinFloat64Vector GetBisector(LinFloat64Vector v2, bool assumeEqualNormVectors = false)
    {
        return (this + v2).Times(05d);
    }

    
    public LinFloat64Vector GetUnitBisector(LinFloat64Vector v2, bool assumeEqualNormVectors = false)
    {
        var bisector = assumeEqualNormVectors
            ? this + v2
            : DivideByENorm() + v2.DivideByENorm();

        return bisector.DivideByENorm();
    }

    
    public LinFloat64Vector GetUnitNormal()
    {
        var rotatedVector = DivideByENorm();

        if (rotatedVector.IsNearVectorBasis(0))
            return 1.CreateLinVector();

        // For smoother motions, find the quaternion q that
        // rotates e1 to vector, then use q to rotate e2
        return LinFloat64AxisToVectorRotation
            .CreateFromRotatedVector(0.ToLinBasisVector(), rotatedVector)
            .MapBasisVector(1);
    }


    
    public double GetDistanceSquaredToPoint(LinFloat64Vector point2)
    {
        return Subtract(point2).ENormSquared();
    }

    
    public double GetDistanceToPoint(LinFloat64Vector point2)
    {
        return Subtract(point2).ENormSquared().Sqrt();
    }

    
    public LinFloat64Vector ToUnitLinVector()
    {
        var length = ENorm();

        return length.IsZero() ? Zero : Times(1d / length);
    }
    
    
    public IEnumerable<LinFloat64Vector> GetComponentVectors()
    {
        foreach (var (index, scalar) in IndexScalarPairs.ToTuples())
        {
            yield return CreateScaledBasis(index, scalar);
        }
    }

    
    public LinFloat64Vector VectorNegativeUnit()
    {
        var length = ENorm();

        return length.IsZero() ? Zero : Times(-1d / length);
    }

    
    public double VectorDot(LinFloat64Vector vector2)
    {
        return ESp(vector2);
    }

    
    public double VectorDot(LinBasisVector vector2)
    {
        return GetTermScalar(vector2.Index) * vector2.Sign;
    }
    

    
    public LinFloat64Vector ProjectOnUnitVector(LinFloat64Vector vector2)
    {
        Debug.Assert(
            vector2.IsNearUnit()
        );

        return vector2.Times(ESp(vector2));
    }

    
    public LinFloat64Vector ProjectOnVector(LinFloat64Vector vector2)
    {
        var uuDot = ENormSquared();
        var xuDot = ESp(vector2);

        return vector2.Times(xuDot / uuDot);
    }

    
    public LinFloat64Vector RejectOnUnitVector(LinFloat64Vector vector2)
    {
        Debug.Assert(
            vector2.IsNearUnit()
        );

        return Subtract(
            vector2.Times(
                ESp(vector2)
            )
        );
    }

    
    public LinFloat64Vector RejectOnVector(LinFloat64Vector vector2)
    {
        var uuDot = ENormSquared();
        var xuDot = ESp(vector2);

        return Subtract(
            vector2.Times(xuDot / uuDot)
        );
    }

    
    public LinFloat64Vector ReflectOnUnitVector(LinFloat64Vector vector2)
    {
        Debug.Assert(
            IsNearUnit()
        );

        return Times(
            2d * ESp(vector2)
        ).Subtract(vector2);
    }

    
    public LinFloat64Vector ReflectOnUnitNormalHyperPlane(LinFloat64Vector unitNormal)
    {
        Debug.Assert(
            unitNormal.IsNearUnit()
        );

        return Subtract(
            unitNormal.Times(
                2d * ESp(unitNormal)
            )
        );
    }

    
    public LinFloat64Vector RotateToUnitVector(LinFloat64Vector unitVector, LinFloat64DirectedAngle angle)
    {
        Debug.Assert(
            IsNearUnit() &&
            unitVector.IsNearUnit()
        );

        // Create a unit normal to u in the u-v rotational plane
        var v1 = unitVector.Subtract(Times(unitVector.VectorDot(this)));
        var v1Length = v1.ENorm();

        Debug.Assert(
            !v1Length.IsNearZero()
        );

        // Compute a rotated version of v in the u-v rotational plane by the given angle
        return Times(angle.Cos())
            .Add(v1.Times(angle.Sin() / v1Length));
    }

    
    public LinFloat64Vector RotateFromUnitVector(LinFloat64Vector unitVector, LinFloat64DirectedAngle angle)
    {
        Debug.Assert(
            unitVector.IsNearUnit() &&
            IsNearUnit()
        );

        // Create a unit normal to u in the u-v rotational plane
        var v1 = Subtract(unitVector.Times(VectorDot(unitVector)));
        var v1Length = v1.ENorm();

        Debug.Assert(
            !v1Length.IsNearZero()
        );

        // Compute a rotated version of v in the u-v rotational plane by the given angle
        return unitVector
            .Times(angle.Cos())
            .Add(v1.Times(angle.Sin() / v1Length));
    }


    
    public LinFloat64Vector2D ToLinVector2D()
    {
        return LinFloat64Vector2D.Create(X, Y);
    }

    
    public LinFloat64Vector3D ToLinVector3D()
    {
        return LinFloat64Vector3D.Create(X, Y, Z);
    }

    
    public LinFloat64Quaternion ToLinVector4D()
    {
        return LinFloat64Quaternion.Create(W, X, Y, Z);
    }

    
    public Tuple<bool, double, LinBasisVector> TryToAxis()
    {
        if (Count != 1)
            return new Tuple<bool, double, LinBasisVector>(
                false,
                0d,
                LinBasisVector.Px
            );

        var (basisIndex, scalar) = this.First().ToTuple();

        return new Tuple<bool, double, LinBasisVector>(
            true,
            scalar.Abs(),
            LinBasisVector.Create(basisIndex, scalar < 0)
        );
    }

    
    public Tuple<bool, double, LinBasisVector> TryToNearAxis(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        return GetCopyByScalar(s => !s.IsNearZero(zeroEpsilon)).TryToAxis();
    }


    
    public IEnumerator<KeyValuePair<int, double>> GetEnumerator()
    {
        return _indexScalarDictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    
    public override string ToString()
    {
        return BasisScalarPairs
            .OrderBy(p => p.Key.Index)
            .Select(p => $"({p.Value:G}){p.Key}")
            .ConcatenateText(" + ");
    }

}