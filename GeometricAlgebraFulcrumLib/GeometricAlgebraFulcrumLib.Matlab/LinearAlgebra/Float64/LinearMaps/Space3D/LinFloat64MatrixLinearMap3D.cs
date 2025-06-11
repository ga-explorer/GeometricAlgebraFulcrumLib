using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space3D;

public sealed class LinFloat64MatrixLinearMap3D :
    ILinFloat64UnilinearMap3D
{
    private readonly SquareMatrix3 _mapArray;

    public int VSpaceDimensions
        => 3;

    public double Determinant { get; }

    public bool SwapsHandedness
        => Determinant < 0;


    
    private LinFloat64MatrixLinearMap3D(SquareMatrix3 mapArray)
    {
        _mapArray = mapArray;
        Determinant = _mapArray.Determinant;
    }


    
    public bool IsValid()
    {
        return _mapArray.IsValid();
    }


    
    public LinFloat64Vector3D MapBasisVector(int basisIndex)
    {
        return _mapArray.ColumnToTuple3D(basisIndex);
    }

    
    public LinFloat64Vector3D MapVector(ILinFloat64Vector3D vector)
    {
        return _mapArray * vector;
    }

    
    public ILinFloat64UnilinearMap3D GetInverseMap()
    {
        return new LinFloat64MatrixLinearMap3D(
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

    public bool IsNearIdentity(double zeroEpsilon = 1E-12)
    {
        for (var i = 0; i < VSpaceDimensions; i++)
        {
            for (var j = 0; j < i; j++)
                if (!_mapArray[i, j].IsNearZero(zeroEpsilon))
                    return false;

            if (!_mapArray[i, i].IsNearOne(zeroEpsilon))
                return false;

            for (var j = i + 1; j < VSpaceDimensions; j++)
                if (!_mapArray[i, j].IsNearZero(zeroEpsilon))
                    return false;
        }

        return true;
    }
}