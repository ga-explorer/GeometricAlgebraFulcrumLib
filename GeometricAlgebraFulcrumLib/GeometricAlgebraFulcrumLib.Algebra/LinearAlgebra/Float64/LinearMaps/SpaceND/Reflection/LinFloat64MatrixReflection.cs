using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Rotation;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Reflection;

public sealed class LinFloat64MatrixReflection :
    LinFloat64ReflectionBase
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64MatrixReflection CreateFromReflection(LinFloat64ReflectionBase reflection, int size)
    {
        var reflectionArray =
            reflection.ToArray(size, size);

        return new LinFloat64MatrixReflection(reflectionArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64MatrixReflection CreateFromRotation(LinFloat64Rotation rotation, int size)
    {
        var rotationArray =
            rotation.ToArray(size, size);

        return new LinFloat64MatrixReflection(rotationArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64MatrixReflection CreateForwardClarkeRotation(int size)
    {
        var rotationArray =
            NumericalLinearAlgebraUtils.ClarkeRotationMatrix(size);

        return new LinFloat64MatrixReflection(rotationArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64MatrixReflection CreateInverseClarkeRotation(int size)
    {
        var rotationArray =
            NumericalLinearAlgebraUtils.ClarkeRotationMatrix(size).Transpose();

        return new LinFloat64MatrixReflection(rotationArray);
    }


    private readonly Matrix<double> _reflectionMatrix;


    public override int VSpaceDimensions
        => _reflectionMatrix.RowCount;

    public override bool SwapsHandedness
        => _reflectionMatrix.Determinant() < 0;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64MatrixReflection(Matrix<double> rotationArray)
    {
        Debug.Assert(
            rotationArray.Determinant().Abs().IsNearOne()
        );

        _reflectionMatrix = rotationArray;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return _reflectionMatrix.Determinant().Abs().IsNearOne();
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
        Debug.Assert(
            basisIndex >= 0
        );

        return basisIndex < _reflectionMatrix.ColumnCount
            ? _reflectionMatrix.Column(basisIndex).ToLinFloat64Vector()
            : LinFloat64Vector.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector MapVector(LinFloat64Vector vector)
    {
        return _reflectionMatrix.Multiply(vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64ReflectionBase GetReflectionLinearMapInverse()
    {
        return new LinFloat64MatrixReflection(
            _reflectionMatrix.Transpose()
        );
    }

    public override Matrix<double> ToArray(int rowCount, int colCount)
    {
        var array = new double[rowCount, colCount];

        for (var j = 0; j < rowCount; j++)
            for (var i = 0; i < colCount; i++)
                array[i, j] = _reflectionMatrix[i, j];

        return array.ToMathNetDenseMatrix();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Matrix<double> ToMatrix(int rowCount, int colCount)
    {
        return _reflectionMatrix.SubMatrix(0, rowCount, 0, colCount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64HyperPlaneNormalReflectionSequence ToHyperPlaneReflectionSequence()
    {
        return LinFloat64HyperPlaneNormalReflectionSequence.CreateFromReflectionMatrix(
            _reflectionMatrix
        );
    }
}