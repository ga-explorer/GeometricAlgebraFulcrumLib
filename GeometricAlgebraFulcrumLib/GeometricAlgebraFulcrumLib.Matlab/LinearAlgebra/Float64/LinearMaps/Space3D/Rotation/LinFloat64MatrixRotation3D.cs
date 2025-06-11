using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space3D.Reflection;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space3D.Rotation;

public sealed class LinFloat64MatrixRotation3D :
    LinFloat64Rotation3D
{
    
    public static LinFloat64MatrixRotation3D CreateFromRotation(LinFloat64Rotation3D rotation)
    {
        var rotationArray =
            rotation.ToSquareMatrix3();

        return new LinFloat64MatrixRotation3D(rotationArray);
    }


    private readonly SquareMatrix3 _rotationArray;


    
    private LinFloat64MatrixRotation3D(SquareMatrix3 rotationArray)
    {
        Debug.Assert(
            rotationArray.Determinant.IsNearOne()
        );

        _rotationArray = rotationArray;
    }


    
    public override bool IsValid()
    {
        return _rotationArray.IsValid() &&
               _rotationArray.Determinant.IsNearOne();
    }

    
    public override bool IsIdentity()
    {
        return false;
    }

    
    public override bool IsNearIdentity(double zeroEpsilon = 1E-12)
    {
        return false;
    }


    
    public override LinFloat64Vector3D MapBasisVector(int basisIndex)
    {
        Debug.Assert(basisIndex >= 0);

        return _rotationArray.ColumnToTuple3D(basisIndex);
    }

    
    public override LinFloat64Vector3D MapVector(ILinFloat64Vector3D vector)
    {
        return _rotationArray * vector;
    }


    
    public LinFloat64MatrixRotation3D GetInverseMatrixRotation()
    {
        return new LinFloat64MatrixRotation3D(
            _rotationArray.Transpose()
        );
    }

    
    public override LinFloat64Quaternion GetQuaternion()
    {
        return _rotationArray.ToQuaternion();
    }

    
    public override LinFloat64Rotation3D GetInverseRotation()
    {
        return GetInverseMatrixRotation();
    }


    
    public override LinFloat64HyperPlaneNormalReflectionSequence3D ToHyperPlaneReflectionSequence()
    {
        return LinFloat64HyperPlaneNormalReflectionSequence3D.CreateFromReflectionMatrix(
            _rotationArray
        );
    }
}