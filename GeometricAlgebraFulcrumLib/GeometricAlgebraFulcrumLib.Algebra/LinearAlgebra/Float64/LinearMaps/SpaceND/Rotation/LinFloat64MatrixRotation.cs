using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Reflection;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
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
            NumericalLinearAlgebraUtils.ClarkeRotationMatrix(size);

        return new LinFloat64MatrixRotation(rotationArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64MatrixRotation CreateInverseClarkeRotation(int size)
    {
        var rotationArray =
            NumericalLinearAlgebraUtils.ClarkeRotationMatrix(size).Transpose();

        return new LinFloat64MatrixRotation(rotationArray);
    }


    private readonly Matrix<double> _rotationArray;


    public override int VSpaceDimensions
        => _rotationArray.RowCount;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64MatrixRotation(Matrix<double> rotationArray)
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
    public override bool IsNearIdentity(double tolerance = Float64Utils.DefaultTolerance)
    {
        return false;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector MapBasisVector(int basisIndex)
    {
        Debug.Assert(basisIndex >= 0);

        return _rotationArray.Column(basisIndex).ToLinFloat64Vector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector MapVector(LinFloat64Vector vector)
    {
        return _rotationArray.Multiply(
            vector.ToMathNetSparseVector(_rotationArray.ColumnCount)
        ).ToLinFloat64Vector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Rotation GetInverseRotation()
    {
        return new LinFloat64MatrixRotation(
            _rotationArray.Transpose()
        );
    }


    public override Matrix<double> ToArray(int rowCount, int colCount)
    {
        var array = new double[VSpaceDimensions, VSpaceDimensions];

        for (var j = 0; j < rowCount; j++)
            for (var i = 0; i < colCount; i++)
                array[i, j] = _rotationArray[i, j];

        return array.ToMatrix();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Matrix<double> ToMatrix(int rowCount, int colCount)
    {
        return ToArray(rowCount, colCount);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64HyperPlaneNormalReflectionSequence ToHyperPlaneReflectionSequence()
    {
        return LinFloat64HyperPlaneNormalReflectionSequence.CreateFromReflectionMatrix(
            _rotationArray
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64PlanarRotationSequence ToVectorToVectorRotationSequence()
    {
        return LinFloat64PlanarRotationSequence.CreateFromRotationMatrix(
            _rotationArray
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