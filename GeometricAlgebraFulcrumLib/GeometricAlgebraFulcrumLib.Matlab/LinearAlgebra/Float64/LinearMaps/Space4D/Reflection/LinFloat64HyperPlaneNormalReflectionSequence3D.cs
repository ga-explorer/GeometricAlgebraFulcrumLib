﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space4D.Reflection;

public sealed class LinFloat64HyperPlaneNormalReflectionSequence4D :
    LinFloat64ReflectionBase4D,
    IReadOnlyList<LinFloat64HyperPlaneNormalReflection4D>
{
    //public static int MatrixEigenDecomposition(Matrix<double> matrix, out Tuple<double, double[]>[] realPairs, out Tuple<double, double[]>[] imagPairs)
    //{
    //    var sysExpr = matrix.ToComplex().Evd();

    //    var count = sysExpr.EigenValues.Count;

    //    realPairs = new Tuple<double, double[]>[count];
    //    imagPairs = new Tuple<double, double[]>[count];

    //    //Console.WriteLine("Eigen Vectors Matrix");
    //    //Console.WriteLine(
    //    //    GeoTextComposerFloat64.DefaultComposer.GetArrayText(
    //    //        sysExpr.EigenVectors.Real().ToArray()
    //    //    )
    //    //);
    //    //Console.WriteLine();

    //    //Console.WriteLine(
    //    //    GeoTextComposerFloat64.DefaultComposer.GetArrayText(
    //    //        sysExpr.EigenVectors.Imaginary().ToArray()
    //    //    )
    //    //);
    //    //Console.WriteLine();

    //    for (var j = 0; j < count; j++)
    //    {
    //        var complexEigenValue = sysExpr.EigenValues[j];
    //        var complexEigenVector = sysExpr.EigenVectors.Column(j);

    //        realPairs[j] = new Tuple<double, double[]>(
    //            complexEigenValue.Real,
    //            complexEigenVector.Real().ToArray()
    //        );

    //        imagPairs[j] = new Tuple<double, double[]>(
    //            complexEigenValue.Imaginary,
    //            complexEigenVector.Imaginary().ToArray()
    //        );
    //    }

    //    return count;
    //}

    //
    //public static Pair<HyperPlaneNormalReflection> ComplexEigenPairToHyperPlaneReflections(double realValue, double imagValue, double[] realVector, double[] imagVector)
    //{
    //    return VectorToVectorRotationSequence
    //        .ComplexEigenPairToSimpleVectorRotation(
    //            realValue, 
    //            imagValue, 
    //            realVector, 
    //            imagVector
    //        ).GetHyperPlaneReflectionPair();
    //}

    //public static LinFloat64HyperPlaneNormalReflectionSequence CreateFromReflectionMatrix(Matrix<double> matrix)
    //{
    //    // Make sure it's a reflection matrix
    //    Debug.Assert(
    //        matrix.RowCount == matrix.ColumnCount &&
    //        matrix.Determinant().Abs().IsNearOne()
    //    );

    //    var reflectionSequence = 
    //        new LinFloat64HyperPlaneNormalReflectionSequence(matrix.RowCount);

    //    var eigenPairsCount = MatrixEigenDecomposition(
    //        matrix,
    //        out var realPairs,
    //        out var imagPairs
    //    );

    //    var eigenValueList = new List<System.Numerics.Complex>(eigenPairsCount / 2);
    //    for (var i = 0; i < eigenPairsCount; i++)
    //    {
    //        var realValue = realPairs[i].Item1;
    //        var imagValue = imagPairs[i].Item1;

    //        var realVector = realPairs[i].Item2;
    //        var imagVector = imagPairs[i].Item2;

    //        Console.WriteLine($"Real Eigen Value {i + 1}: {realValue}");
    //        Console.WriteLine($"Imag Eigen Value {i + 1}: {imagValue}");
    //        Console.WriteLine();

    //        Console.WriteLine($"Real Eigen Vector {i + 1}: {realVector.CreateTuple()}");
    //        Console.WriteLine($"Imag Eigen Vector {i + 1}: {imagVector.CreateTuple()}");
    //        Console.WriteLine();

    //        if (imagValue.IsNearZero())
    //        {
    //            // Ignore identity reflections
    //            if (realValue.IsNearOne())
    //                continue;

    //            var v1 = realVector.VectorDivide(realVector.GetVectorNorm()).CreateTuple();
    //            var v2 = imagVector.VectorDivide(imagVector.GetVectorNorm()).CreateTuple();

    //            // Make sure both eigen vector parts encode the same 1-dimensional subspace
    //            Debug.Assert(
    //                v1.ESp(v2).Abs().IsNearOne()
    //            );

    //            Console.WriteLine("Hyper Plane Reflection: ");
    //            Console.WriteLine($"   Reflection Unit Normal: {v1}");
    //            Console.WriteLine();

    //            reflectionSequence.AppendMap(v1);

    //            continue;
    //        }

    //        // Ignore complex conjugate eigen values (only take first one of the pair)
    //        var conjIndex = eigenValueList.FindIndex(
    //            c => c.IsNearConjugateTo(realValue, imagValue)
    //        );

    //        if (conjIndex >= 0)
    //        {
    //            eigenValueList.RemoveAt(conjIndex);

    //            continue;
    //        }

    //        eigenValueList.Add(
    //            new System.Numerics.Complex(realValue, imagValue)
    //        );

    //        var (r1, r2) =
    //            ComplexEigenPairToHyperPlaneReflections(
    //                realValue,
    //                imagValue,
    //                realVector,
    //                imagVector
    //            );

    //        reflectionSequence
    //            .AppendMap(r1)
    //            .AppendMap(r2);
    //    }

    //    return reflectionSequence;
    //}

    
    public static LinFloat64HyperPlaneNormalReflectionSequence4D CreateFromReflectionMatrix(SquareMatrix4 matrix)
    {
        return matrix.GetHyperPlaneNormalReflectionSequence();
    }

    
    public static LinFloat64HyperPlaneNormalReflectionSequence4D Create()
    {
        return new LinFloat64HyperPlaneNormalReflectionSequence4D();
    }

    
    public static LinFloat64HyperPlaneNormalReflectionSequence4D Create(ILinFloat64HyperPlaneNormalReflectionLinearMap4D reflection)
    {
        var reflectionSequence =
            new LinFloat64HyperPlaneNormalReflectionSequence4D();

        reflectionSequence.AppendMap(reflection);

        return reflectionSequence;
    }

    public static LinFloat64HyperPlaneNormalReflectionSequence4D CreateRandom(Random random, int count)
    {
        var rotationSequence = new LinFloat64HyperPlaneNormalReflectionSequence4D();

        for (var i = 0; i < count; i++)
        {
            var u = random.GetLinVector4D();

            rotationSequence.AppendMap(
                LinFloat64HyperPlaneNormalReflection4D.Create(u)
            );
        }

        return rotationSequence;
    }

    public static LinFloat64HyperPlaneNormalReflectionSequence4D CreateRandomOrthogonal(Random random, int dimensions, int count)
    {
        if (count > dimensions)
            throw new ArgumentOutOfRangeException(nameof(count));

        var rotationSequence = new LinFloat64HyperPlaneNormalReflectionSequence4D();

        var vectorList =
            random.GetMathNetOrthonormalVectors(dimensions, count);

        for (var i = 0; i < count; i++)
        {
            var u = vectorList[i].ToLinVector4D();

            rotationSequence.AppendMap(
                LinFloat64HyperPlaneNormalReflection4D.Create(u)
            );
        }

        return rotationSequence;
    }


    private readonly List<LinFloat64HyperPlaneNormalReflection4D> _mapList
        = new List<LinFloat64HyperPlaneNormalReflection4D>();


    public int Count
        => _mapList.Count;

    public LinFloat64HyperPlaneNormalReflection4D this[int index]
        => _mapList[index];

    public override bool SwapsHandedness
        => _mapList.Count.IsOdd();


    
    private LinFloat64HyperPlaneNormalReflectionSequence4D()
    {
    }

    
    private LinFloat64HyperPlaneNormalReflectionSequence4D(List<LinFloat64HyperPlaneNormalReflection4D> reflectionNormalList)
    {
        _mapList = reflectionNormalList;

        Debug.Assert(IsValid());
    }


    
    public LinFloat64HyperPlaneNormalReflectionSequence4D AppendMap(LinFloat64Vector4D reflectionNormal)
    {
        _mapList.Add(
            LinFloat64HyperPlaneNormalReflection4D.Create(reflectionNormal)
        );

        return this;
    }

    
    public LinFloat64HyperPlaneNormalReflectionSequence4D AppendMap(LinFloat64HyperPlaneNormalReflection4D reflection)
    {
        if (reflection.VSpaceDimensions != VSpaceDimensions)
            throw new ArgumentException();

        _mapList.Add(reflection);

        return this;
    }

    
    public LinFloat64HyperPlaneNormalReflectionSequence4D AppendMap(ILinFloat64HyperPlaneNormalReflectionLinearMap4D reflection)
    {
        var r2 =
            reflection as LinFloat64HyperPlaneNormalReflection4D
            ?? reflection.ToHyperPlaneNormalReflection();

        _mapList.Add(r2);

        return this;
    }

    
    public LinFloat64HyperPlaneNormalReflectionSequence4D PrependMap(LinFloat64Vector4D reflectionNormal)
    {
        _mapList.Insert(
            0,
            LinFloat64HyperPlaneNormalReflection4D.Create(reflectionNormal)
        );

        return this;
    }

    
    public LinFloat64HyperPlaneNormalReflectionSequence4D PrependMap(LinFloat64HyperPlaneNormalReflection4D reflection)
    {
        _mapList.Insert(
            0,
            reflection
        );

        return this;
    }

    
    public LinFloat64HyperPlaneNormalReflectionSequence4D InsertMap(int index, LinFloat64Vector4D reflectionNormal)
    {
        _mapList.Insert(
            index,
            LinFloat64HyperPlaneNormalReflection4D.Create(reflectionNormal)
        );

        return this;
    }

    
    public LinFloat64HyperPlaneNormalReflectionSequence4D InsertMap(int index, LinFloat64HyperPlaneNormalReflection4D reflection)
    {
        _mapList.Insert(
            index,
            reflection
        );

        return this;
    }

    
    public LinFloat64HyperPlaneNormalReflectionSequence4D AppendMaps(IEnumerable<LinFloat64HyperPlaneNormalReflection4D> reflectionList)
    {
        _mapList.AddRange(reflectionList);

        return this;
    }

    
    public LinFloat64HyperPlaneNormalReflectionSequence4D PrependMaps(IEnumerable<LinFloat64HyperPlaneNormalReflection4D> reflectionList)
    {
        _mapList.InsertRange(0, reflectionList);

        return this;
    }

    
    public LinFloat64HyperPlaneNormalReflectionSequence4D InsertMaps(int index, IEnumerable<LinFloat64HyperPlaneNormalReflection4D> reflectionList)
    {
        _mapList.InsertRange(index, reflectionList);

        return this;
    }


    
    public override bool IsValid()
    {
        return _mapList.All(a => a.IsValid());
    }

    public override bool IsIdentity()
    {
        if (_mapList.Count == 0)
            return true;

        for (var basisIndex = 0; basisIndex < VSpaceDimensions; basisIndex++)
        {
            var isSameVectorBasis =
                MapBasisVector(basisIndex).IsVectorBasis(basisIndex);

            if (!isSameVectorBasis) return false;
        }

        return true;
    }

    public override bool IsNearIdentity(double zeroEpsilon = 1E-12)
    {
        for (var basisIndex = 0; basisIndex < VSpaceDimensions; basisIndex++)
        {
            var isSameVectorBasis =
                MapBasisVector(basisIndex).IsNearVectorBasis(basisIndex, zeroEpsilon);

            if (!isSameVectorBasis) return false;
        }

        return true;
    }

    /// <summary>
    /// Test if all reflection normals in this sequence are nearly pair-wise orthogonal
    /// </summary>
    /// <returns></returns>
    public bool IsNearOrthogonalReflectionsSequence(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        if (_mapList.Count > VSpaceDimensions)
            return false;

        for (var i = 0; i < _mapList.Count; i++)
        {
            var u1 = _mapList[i].ReflectionNormal;

            for (var j = i + 1; j < _mapList.Count; j++)
            {
                var u2 = _mapList[j].ReflectionNormal;

                if (!u1.IsNearOrthogonalTo(u2, zeroEpsilon)) return false;
            }
        }

        return true;
    }


    public double[] MapVectorInPlace(double[] vector)
    {
        foreach (var reflection in _mapList)
        {
            var u = reflection.ReflectionNormal;

            var s = -2d * vector.VectorDot(u.GetItemArray());

            for (var i = 0; i < VSpaceDimensions; i++)
                vector[i] += s * u[i];
        }

        return vector;
    }

    
    public override LinFloat64Vector4D MapBasisVector(int basisIndex)
    {
        Debug.Assert(
            basisIndex >= 0
        );

        if (_mapList.Count == 0)
            return LinFloat64Vector4D.BasisVectors[basisIndex];

        var composer = LinFloat64Vector4DComposer
            .Create()
            .SetTerm(basisIndex, 1d);

        foreach (var reflection in _mapList)
        {
            var u = reflection.ReflectionNormal;
            var s = -2d * composer.VectorESp(u);

            composer.AddVector(u, s);
        }

        return composer.GetVector();
    }

    
    public override LinFloat64Vector4D MapVector(ILinFloat64Vector4D vector)
    {
        if (_mapList.Count == 0)
            return LinFloat64Vector4DUtils.ToLinVector4D(vector);

        var composer = LinFloat64Vector4DComposer
            .Create()
            .SetVector(vector, 1d);

        foreach (var reflection in _mapList)
        {
            var u = reflection.ReflectionNormal;
            var s = -2d * composer.VectorESp(u);

            composer.AddVector(u, s);
        }

        return composer.GetVector();
    }


    
    public LinFloat64HyperPlaneNormalReflectionSequence4D GetHyperPlaneReflectionSequenceInverse()
    {
        if (_mapList.Count == 0)
            return this;

        var reflectionNormalList =
            ((IEnumerable<LinFloat64HyperPlaneNormalReflection4D>)_mapList)
            .Reverse()
            .ToList();

        return new LinFloat64HyperPlaneNormalReflectionSequence4D(reflectionNormalList);
    }

    
    public override LinFloat64ReflectionBase4D GetReflectionLinearMapInverse()
    {
        return GetHyperPlaneReflectionSequenceInverse();
    }

    
    public override LinFloat64HyperPlaneNormalReflectionSequence4D ToHyperPlaneReflectionSequence()
    {
        return this;
    }

    /// <summary>
    /// Create a new sequence containing the minimum number of pair-wise
    /// orthogonal rotations and reflections equivalent to this one
    /// </summary>
    /// <returns></returns>
    
    public LinFloat64OrthogonalLinearMapSequence4D ReduceSequence()
    {
        return LinFloat64OrthogonalLinearMapSequence4D.CreateFromMatrix(
            this.ToSquareMatrix3()
        );
    }


    
    public IEnumerator<LinFloat64HyperPlaneNormalReflection4D> GetEnumerator()
    {
        return _mapList.GetEnumerator();
    }

    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}