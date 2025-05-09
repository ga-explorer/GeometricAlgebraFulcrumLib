using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Reflection;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Rotation;

public sealed class LinFloat64MatrixRotation :
    LinFloat64Rotation
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64MatrixRotation CreateFromRotation(LinFloat64Rotation rotation, int size)
    {
        var rotationArray =
            rotation.ToArray(size, size);

        return new LinFloat64MatrixRotation(rotationArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64MatrixRotation CreateForwardClarkeRotation(int size)
    {
        var rotationArray =
            Float64ArrayUtils.CreateClarkeRotationArray(size);

        return new LinFloat64MatrixRotation(rotationArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64MatrixRotation CreateInverseClarkeRotation(int size)
    {
        var rotationArray =
            Float64ArrayUtils.CreateClarkeRotationArray(size).Transpose();

        return new LinFloat64MatrixRotation(rotationArray);
    }


    private readonly double[,] _rotationArray;


    public override int VSpaceDimensions
        => _rotationArray.GetLength(0);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64MatrixRotation(double[,] rotationArray)
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
    public override bool IsNearIdentity(double zeroEpsilon = 1E-12)
    {
        return false;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector MapBasisVector(int basisIndex)
    {
        Debug.Assert(basisIndex >= 0);

        return _rotationArray.ColumnToLinVector(basisIndex);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector MapVector(LinFloat64Vector vector)
    {
        return _rotationArray.MatrixProduct(vector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Rotation GetInverseRotation()
    {
        return new LinFloat64MatrixRotation(
            _rotationArray.Transpose()
        );
    }


    public override double[,] ToArray(int rowCount, int colCount)
    {
        var array = new double[VSpaceDimensions, VSpaceDimensions];

        for (var j = 0; j < rowCount; j++)
            for (var i = 0; i < colCount; i++)
                array[i, j] = _rotationArray[i, j];

        return array;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Matrix<double> ToMatrix(int rowCount, int colCount)
    {
        return ToArray(rowCount, colCount).ToMatrix();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64HyperPlaneNormalReflectionSequence ToHyperPlaneReflectionSequence()
    {
        return LinFloat64HyperPlaneNormalReflectionSequence.CreateFromReflectionMatrix(
            _rotationArray.ToMatrix()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64PlanarRotationSequence ToVectorToVectorRotationSequence()
    {
        return LinFloat64PlanarRotationSequence.CreateFromRotationMatrix(
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