using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND.Scaling;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND.Reflection;

public sealed class LinFloat64HyperPlaneAxisReflection :
    LinFloat64ReflectionBase,
    ILinFloat64HyperPlaneNormalReflectionLinearMap
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64HyperPlaneAxisReflection Create(int dimensions, int axisIndex)
    {
        return new LinFloat64HyperPlaneAxisReflection(dimensions, axisIndex);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64HyperPlaneAxisReflection Create(int dimensions, LinSignedBasisVector axis)
    {
        return new LinFloat64HyperPlaneAxisReflection(
            dimensions,
            axis.Index
        );
    }


    public LinSignedBasisVector ReflectionNormalAxis { get; }

    public Float64Vector ReflectionNormal
        => ReflectionNormalAxis.ToVector();

    public override int VSpaceDimensions { get; }

    public override bool SwapsHandedness
        => true;

    public double ScalingFactor
        => -1d;

    public Float64Vector ScalingVector
        => ReflectionNormalAxis.ToVector();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64HyperPlaneAxisReflection(int dimensions, int basisIndex)
    {
        VSpaceDimensions = dimensions;
        ReflectionNormalAxis = new LinSignedBasisVector(basisIndex, false);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsIdentity()
    {
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsNearIdentity(double epsilon = 1E-12)
    {
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Vector MapBasisVector(int basisIndex)
    {
        Debug.Assert(basisIndex >= 0);

        return Float64VectorComposer
            .Create()
            .SetTerm(
                basisIndex,
                ReflectionNormalAxis.Index == basisIndex
                    ? -1d : 1d
            ).GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Vector MapVector(Float64Vector vector)
    {
        var composer =
            Float64VectorComposer
                .Create()
                .SetVector(vector);

        composer[ReflectionNormalAxis.Index] *= -1d;

        return composer.GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64ReflectionBase GetReflectionLinearMapInverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64HyperPlaneAxisReflection GetHyperPlaneAxisReflectionInverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinFloat64HyperPlaneNormalReflectionLinearMap GetHyperPlaneNormalReflectionLinearMapInverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64HyperPlaneNormalReflection ToHyperPlaneNormalReflection()
    {
        return LinFloat64HyperPlaneNormalReflection.Create(ReflectionNormal);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinFloat64DirectionalScalingLinearMap GetDirectionalScalingInverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64HyperPlaneNormalReflectionSequence ToHyperPlaneReflectionSequence()
    {
        return LinFloat64HyperPlaneNormalReflectionSequence.Create(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorDirectionalScaling ToVectorDirectionalScaling()
    {
        return LinFloat64VectorDirectionalScaling.Create(
            -1d,
            ReflectionNormal
        );
    }
}