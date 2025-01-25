using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;

public sealed class Float64UniformScaleAffineMap3D :
    IFloat64AffineMap3D
{
    private double _scalingFactor = 1d;
    public double ScalingFactor
    {
        get => _scalingFactor;
        set
        {
            if (value is < 0 or double.NaN || double.IsInfinity(value))
                throw new InvalidOperationException(nameof(value));

            _scalingFactor = value;
        }
    }

    public bool SwapsHandedness
        => false;


    public bool IsIdentity()
    {
        throw new NotImplementedException();
    }

    public bool IsNearIdentity(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        throw new NotImplementedException();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64UniformScaleAffineMap3D(double scalingFactor = 1d)
    {
        ScalingFactor = scalingFactor;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix4 GetSquareMatrix4()
    {
        return SquareMatrix4.CreateScalingMatrix3D(_scalingFactor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Matrix4x4 GetMatrix4x4()
    {
        var s = (float)_scalingFactor;

        return new Matrix4x4(
            s, 0, 0, 0,
            0, s, 0, 0,
            0, 0, s, 0,
            0, 0, 0, 1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[,] GetArray2D()
    {
        var array = new double[4, 4];

        array[0, 0] = _scalingFactor;
        array[1, 1] = _scalingFactor;
        array[2, 2] = _scalingFactor;
        array[3, 3] = 1d;

        return array;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapPoint(ILinFloat64Vector3D point)
    {
        return LinFloat64Vector3D.Create(_scalingFactor * point.X,
            _scalingFactor * point.Y,
            _scalingFactor * point.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapVector(ILinFloat64Vector3D vector)
    {
        return LinFloat64Vector3D.Create(_scalingFactor * vector.X,
            _scalingFactor * vector.Y,
            _scalingFactor * vector.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapNormal(ILinFloat64Vector3D normal)
    {
        return LinFloat64Vector3D.Create(_scalingFactor * normal.X,
            _scalingFactor * normal.Y,
            _scalingFactor * normal.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IFloat64AffineMap3D GetInverseAffineMap()
    {
        return new Float64UniformScaleAffineMap3D(1d / _scalingFactor);
    }
}