using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Extensions;
using MathNet.Numerics.LinearAlgebra;
using NumericalGeometryLib.BasicMath.Maps.SpaceND.Reflection;
using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace NumericalGeometryLib.BasicMath.Maps.SpaceND.Rotation;

public sealed class MatrixRotation :
    RotationLinearMap
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MatrixRotation CreateFromRotation(RotationLinearMap rotation)
    {
        var rotationArray = 
            rotation.GetArray();

        return new MatrixRotation(rotationArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MatrixRotation CreateForwardClarkeRotation(int size)
    {
        var rotationArray = 
            Float64ArrayUtils.CreateClarkeRotationArray(size);

        return new MatrixRotation(rotationArray);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MatrixRotation CreateInverseClarkeRotation(int size)
    {
        var rotationArray = 
            Float64ArrayUtils.CreateClarkeRotationArray(size).Transpose();

        return new MatrixRotation(rotationArray);
    }

    
    private readonly double[,] _rotationArray;


    public override int Dimensions
        => _rotationArray.GetLength(0);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private MatrixRotation(double[,] rotationArray)
    {
        Debug.Assert(
            rotationArray.Determinant().IsNearOne()
        );

        _rotationArray = rotationArray;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return _rotationArray.Determinant().IsNearOne();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsIdentity()
    {
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsNearIdentity(double epsilon = 1E-12)
    {
        return false;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Tuple MapVectorBasis(int basisIndex)
    {
        Debug.Assert(basisIndex >= 0 && basisIndex < Dimensions);

        return Float64Tuple.Create(
            Dimensions.GetRange().Select(i => _rotationArray[i, basisIndex])
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Tuple MapVector(Float64Tuple vector)
    {
        Debug.Assert(vector.Dimensions == Dimensions);
            
        return Float64Tuple.Create(
            _rotationArray.MatrixProduct(vector.ScalarArray)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RotationLinearMap GetVectorRotationInverse()
    {
        return new MatrixRotation(
            _rotationArray.Transpose()
        );
    }
    

    public override double[,] GetArray()
    {
        var array = new double[Dimensions, Dimensions];

        for (var j = 0; j < Dimensions; j++)
        for (var i = 0; i < Dimensions; i++) 
            array[i, j] = _rotationArray[i, j];

        return array;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Matrix<double> GetMatrix()
    {
        return _rotationArray.ToMatrix();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override HyperPlaneNormalReflectionSequence ToHyperPlaneReflectionSequence()
    {
        return HyperPlaneNormalReflectionSequence.CreateFromReflectionMatrix(
            _rotationArray.ToMatrix()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override VectorToVectorRotationSequence ToVectorToVectorRotationSequence()
    {
        return VectorToVectorRotationSequence.CreateFromRotationMatrix(
            _rotationArray.ToMatrix()
        );
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override SimpleRotationSequence ToSimpleVectorRotationSequence()
    //{
    //    return SimpleRotationSequence.CreateFromRotationMatrix(
    //        _rotationArray.ToMatrix()
    //    );
    //}
}