using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space4D.Reflection;

public sealed class LinFloat64MatrixReflection4D :
    LinFloat64ReflectionBase4D
{
    
    public static LinFloat64MatrixReflection4D CreateFromReflection(LinFloat64ReflectionBase4D reflection)
    {
        var reflectionArray =
            reflection.ToSquareMatrix3();

        return new LinFloat64MatrixReflection4D(reflectionArray);
    }


    private readonly SquareMatrix4 _reflectionArray;


    public override bool SwapsHandedness
        => _reflectionArray.Determinant.IsNegative();


    
    private LinFloat64MatrixReflection4D(SquareMatrix4 rotationArray)
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

    
    public override LinFloat64Vector4D MapBasisVector(int basisIndex)
    {
        Debug.Assert(
            basisIndex >= 0
        );

        return basisIndex < 3
            ? _reflectionArray.ColumnToTuple4D(basisIndex)
            : LinFloat64Vector4D.Zero;
    }

    
    public override LinFloat64Vector4D MapVector(ILinFloat64Vector4D vector)
    {
        return _reflectionArray * vector;
    }

    
    public override LinFloat64ReflectionBase4D GetReflectionLinearMapInverse()
    {
        return new LinFloat64MatrixReflection4D(
            _reflectionArray.Transpose()
        );
    }

    
    public override LinFloat64HyperPlaneNormalReflectionSequence4D ToHyperPlaneReflectionSequence()
    {
        return LinFloat64HyperPlaneNormalReflectionSequence4D.CreateFromReflectionMatrix(_reflectionArray);
    }
}