using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND.Rotation;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Extensions;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND.Reflection;

public sealed class LinFloat64MatrixReflection :
    LinFloat64ReflectionBase
{
    
    public static LinFloat64MatrixReflection CreateFromReflection(LinFloat64ReflectionBase reflection, int size)
    {
        var reflectionArray =
            reflection.ToArray(size, size);

        return new LinFloat64MatrixReflection(reflectionArray);
    }

    
    public static LinFloat64MatrixReflection CreateFromRotation(LinFloat64Rotation rotation, int size)
    {
        var rotationArray =
            rotation.ToArray(size, size);

        return new LinFloat64MatrixReflection(rotationArray);
    }

    
    public static LinFloat64MatrixReflection CreateForwardClarkeRotation(int size)
    {
        var rotationArray =
            Float64ArrayUtils.CreateClarkeRotationArray(size);

        return new LinFloat64MatrixReflection(rotationArray);
    }

    
    public static LinFloat64MatrixReflection CreateInverseClarkeRotation(int size)
    {
        var rotationArray =
            Float64ArrayUtils.CreateClarkeRotationArray(size).Transpose();

        return new LinFloat64MatrixReflection(rotationArray);
    }


    private readonly double[,] _reflectionArray;


    public override int VSpaceDimensions
        => _reflectionArray.GetLength(0);

    public override bool SwapsHandedness
        => _reflectionArray.Determinant() < 0;


    
    private LinFloat64MatrixReflection(double[,] rotationArray)
    {
        Debug.Assert(
            rotationArray.Determinant().Abs().IsNearOne()
        );

        _reflectionArray = rotationArray;
    }


    
    public override bool IsValid()
    {
        return _reflectionArray.Determinant().Abs().IsNearOne();
    }

    
    public override bool IsIdentity()
    {
        return false;
    }

    
    public override bool IsNearIdentity(double zeroEpsilon = 1E-12)
    {
        return false;
    }

    
    public override LinFloat64Vector MapBasisVector(int basisIndex)
    {
        Debug.Assert(
            basisIndex >= 0
        );

        return basisIndex < _reflectionArray.GetLength(1)
            ? _reflectionArray.ColumnToLinVector(basisIndex)
            : LinFloat64Vector.Zero;
    }

    
    public override LinFloat64Vector MapVector(LinFloat64Vector vector)
    {
        return _reflectionArray.MatrixProduct(vector);
    }

    
    public override LinFloat64ReflectionBase GetReflectionLinearMapInverse()
    {
        return new LinFloat64MatrixReflection(
            _reflectionArray.Transpose()
        );
    }

    public override double[,] ToArray(int rowCount, int colCount)
    {
        var array = new double[rowCount, colCount];

        for (var j = 0; j < rowCount; j++)
            for (var i = 0; i < colCount; i++)
                array[i, j] = _reflectionArray[i, j];

        return array;
    }

    
    public override Matrix<double> ToMatrix(int rowCount, int colCount)
    {
        return _reflectionArray
            .GetSubArray(0, 0, rowCount, colCount)
            .ToMatrix();
    }

    
    public override LinFloat64HyperPlaneNormalReflectionSequence ToHyperPlaneReflectionSequence()
    {
        return LinFloat64HyperPlaneNormalReflectionSequence.CreateFromReflectionMatrix(
            _reflectionArray.ToMatrix()
        );
    }
}