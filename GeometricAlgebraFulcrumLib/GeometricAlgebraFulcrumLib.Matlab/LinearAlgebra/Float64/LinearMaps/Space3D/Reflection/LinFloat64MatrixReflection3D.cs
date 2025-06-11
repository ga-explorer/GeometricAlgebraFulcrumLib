using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space3D.Reflection;

public sealed class LinFloat64MatrixReflection3D :
    LinFloat64ReflectionBase3D
{
    
    public static LinFloat64MatrixReflection3D CreateFromReflection(LinFloat64ReflectionBase3D reflection)
    {
        var reflectionArray =
            reflection.ToSquareMatrix3();

        return new LinFloat64MatrixReflection3D(reflectionArray);
    }


    private readonly SquareMatrix3 _reflectionArray;


    public override bool SwapsHandedness
        => _reflectionArray.Determinant.IsNegative();


    
    private LinFloat64MatrixReflection3D(SquareMatrix3 rotationArray)
    {
        Debug.Assert(
            rotationArray.Determinant.Abs().IsNearOne()
        );

        _reflectionArray = rotationArray;
    }


    
    public override bool IsValid()
    {
        return _reflectionArray.IsValid() &&
               _reflectionArray.Determinant.Abs().IsNearOne();
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
        Debug.Assert(
            basisIndex >= 0
        );

        return basisIndex < 3
            ? _reflectionArray.ColumnToTuple3D(basisIndex)
            : LinFloat64Vector3D.Zero;
    }

    
    public override LinFloat64Vector3D MapVector(ILinFloat64Vector3D vector)
    {
        return _reflectionArray * vector;
    }

    
    public override LinFloat64ReflectionBase3D GetReflectionLinearMapInverse()
    {
        return new LinFloat64MatrixReflection3D(
            _reflectionArray.Transpose()
        );
    }

    
    public override LinFloat64HyperPlaneNormalReflectionSequence3D ToHyperPlaneReflectionSequence()
    {
        return LinFloat64HyperPlaneNormalReflectionSequence3D.CreateFromReflectionMatrix(_reflectionArray);
    }
}