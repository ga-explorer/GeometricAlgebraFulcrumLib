using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND.Scaling;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.SpaceND;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND.Reflection;

public sealed class LinFloat64HyperPlaneAxisReflection :
    LinFloat64ReflectionBase,
    ILinFloat64HyperPlaneNormalReflectionLinearMap
{
    
    public static LinFloat64HyperPlaneAxisReflection Create(int dimensions, int axisIndex)
    {
        return new LinFloat64HyperPlaneAxisReflection(dimensions, axisIndex);
    }

    
    public static LinFloat64HyperPlaneAxisReflection Create(int dimensions, LinBasisVector axis)
    {
        return new LinFloat64HyperPlaneAxisReflection(
            dimensions,
            axis.Index
        );
    }


    public LinBasisVector ReflectionNormalAxis { get; }

    public LinFloat64Vector ReflectionNormal
        => ReflectionNormalAxis.ToLinVector();

    public override int VSpaceDimensions { get; }

    public override bool SwapsHandedness
        => true;

    public double ScalingFactor
        => -1d;

    public LinFloat64Vector ScalingVector
        => ReflectionNormalAxis.ToLinVector();


    
    private LinFloat64HyperPlaneAxisReflection(int dimensions, int basisIndex)
    {
        VSpaceDimensions = dimensions;
        ReflectionNormalAxis = LinBasisVector.Positive(basisIndex);
    }


    
    public override bool IsValid()
    {
        return true;
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
        Debug.Assert(basisIndex >= 0);

        return LinFloat64VectorComposer
            .Create()
            .SetTerm(
                basisIndex,
                ReflectionNormalAxis.Index == basisIndex
                    ? -1d : 1d
            ).GetVector();
    }

    
    public override LinFloat64Vector MapVector(LinFloat64Vector vector)
    {
        var composer =
            LinFloat64VectorComposer
                .Create()
                .SetVector(vector);

        composer[ReflectionNormalAxis.Index] *= -1d;

        return composer.GetVector();
    }

    
    public override LinFloat64ReflectionBase GetReflectionLinearMapInverse()
    {
        return this;
    }

    
    public override Matrix<double> ToMatrix(int rowCount, int colCount)
    {
        return Matrix<double>
            .Build
            .DenseOfArray(
                ToArray(rowCount, colCount)
            );
    }

    public override double[,] ToArray(int rowCount, int colCount)
    {
        var array = new double[rowCount, colCount];

        for (var j = 0; j < colCount; j++)
            array[j, j] = 1d;

        var i = ReflectionNormalAxis.Index;
        array[i, i] = -1d;

        return array;
    }

    
    public LinFloat64HyperPlaneAxisReflection GetHyperPlaneAxisReflectionInverse()
    {
        return this;
    }

    
    public ILinFloat64HyperPlaneNormalReflectionLinearMap GetHyperPlaneNormalReflectionLinearMapInverse()
    {
        return this;
    }

    
    public LinFloat64HyperPlaneNormalReflection ToHyperPlaneNormalReflection()
    {
        return LinFloat64HyperPlaneNormalReflection.Create(ReflectionNormal);
    }

    
    public ILinFloat64DirectionalScalingLinearMap GetDirectionalScalingInverse()
    {
        return this;
    }

    
    public override LinFloat64HyperPlaneNormalReflectionSequence ToHyperPlaneReflectionSequence()
    {
        return LinFloat64HyperPlaneNormalReflectionSequence.Create(this);
    }

    
    public LinFloat64VectorDirectionalScaling ToVectorDirectionalScaling()
    {
        return LinFloat64VectorDirectionalScaling.Create(
            -1d,
            ReflectionNormal
        );
    }
}