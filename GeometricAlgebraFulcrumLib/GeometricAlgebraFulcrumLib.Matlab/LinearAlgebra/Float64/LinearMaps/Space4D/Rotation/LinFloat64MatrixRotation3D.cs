using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space4D.Reflection;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space4D.Rotation;

public sealed class LinFloat64MatrixRotation4D :
    LinFloat64RotationBase4D
{
    
    public static LinFloat64MatrixRotation4D CreateFromRotation(LinFloat64RotationBase4D rotation)
    {
        var rotationArray =
            rotation.ToSquareMatrix3();

        return new LinFloat64MatrixRotation4D(rotationArray);
    }


    private readonly SquareMatrix4 _rotationArray;


    
    private LinFloat64MatrixRotation4D(SquareMatrix4 rotationArray)
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


    
    public override LinFloat64Vector4D MapBasisVector(int basisIndex)
    {
        Debug.Assert(basisIndex >= 0);

        return _rotationArray.ColumnToTuple4D(basisIndex);
    }

    
    public override LinFloat64Vector4D MapVector(ILinFloat64Vector4D vector)
    {
        return _rotationArray * vector;
    }


    
    public override LinFloat64RotationBase4D GetVectorRotationInverse()
    {
        return new LinFloat64MatrixRotation4D(
            _rotationArray.Transpose()
        );
    }


    
    public override LinFloat64HyperPlaneNormalReflectionSequence4D ToHyperPlaneReflectionSequence()
    {
        return LinFloat64HyperPlaneNormalReflectionSequence4D.CreateFromReflectionMatrix(
            _rotationArray
        );
    }

    
    public override LinFloat64VectorToVectorRotationSequence4D ToVectorToVectorRotationSequence()
    {
        return LinFloat64VectorToVectorRotationSequence4D.CreateFromRotationMatrix(
            _rotationArray
        );
    }

    //
    //public override SimpleRotationSequence ToSimpleVectorRotationSequence()
    //{
    //    return SimpleRotationSequence.CreateFromRotationMatrix(
    //        _rotationArray.ToMatrix()
    //    );
    //}
}