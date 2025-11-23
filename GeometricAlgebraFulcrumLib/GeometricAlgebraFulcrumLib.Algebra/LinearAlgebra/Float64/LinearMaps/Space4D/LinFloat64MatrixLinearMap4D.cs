using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space4D;

public sealed class LinFloat64MatrixLinearMap4D :
    ILinFloat64UnilinearMap4D
{
    private readonly SquareMatrix4 _mapArray;

    public int VSpaceDimensions
        => 3;

    public double Determinant { get; }

    public bool SwapsHandedness
        => Determinant < 0;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64MatrixLinearMap4D(SquareMatrix4 mapArray)
    {
        _mapArray = mapArray;
        Determinant = _mapArray.Determinant;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return _mapArray.IsValid();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4D MapBasisVector(int basisIndex)
    {
        return _mapArray.ColumnToTuple4D(basisIndex);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4D MapVector(ILinFloat64Vector4D vector)
    {
        return _mapArray * vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinFloat64UnilinearMap4D GetInverseMap()
    {
        return new LinFloat64MatrixLinearMap4D(
            _mapArray.Inverse()
        );
    }

    public bool IsIdentity()
    {
        for (var i = 0; i < VSpaceDimensions; i++)
        {
            for (var j = 0; j < i; j++)
                if (!_mapArray[i, j].IsZero())
                    return false;

            if (!_mapArray[i, i].IsOne())
                return false;

            for (var j = i + 1; j < VSpaceDimensions; j++)
                if (!_mapArray[i, j].IsZero())
                    return false;
        }

        return true;
    }

    public bool IsNearIdentity(double tolerance = Float64Utils.DefaultTolerance)
    {
        for (var i = 0; i < VSpaceDimensions; i++)
        {
            for (var j = 0; j < i; j++)
                if (!_mapArray[i, j].IsNearZero(tolerance))
                    return false;

            if (!_mapArray[i, i].IsNearOne(tolerance))
                return false;

            for (var j = i + 1; j < VSpaceDimensions; j++)
                if (!_mapArray[i, j].IsNearZero(tolerance))
                    return false;
        }

        return true;
    }
}