using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;

/// <summary>
/// Applies a rotation followed by a uniform scaling followed by translation
/// </summary>
public class Float64RotateUniformScaleTranslateAffineMap3D :
    IFloat64AffineMap3D
{
    private IFloat64RotateAffineMap3D _rotateMap = Float64IdentityAffineMap3D.Instance;
    public IFloat64RotateAffineMap3D RotateMap
    {
        get => _rotateMap;
        set
        {
            if (value is null || !value.IsValid())
                throw new ArgumentException(nameof(value));

            _rotateMap = value;
        }
    }

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

    private LinFloat64Vector3D _translationVector = LinFloat64Vector3D.Zero;
    public LinFloat64Vector3D TranslationVector
    {
        get => _translationVector;
        set
        {
            if (value is null || !value.IsValid())
                throw new InvalidOperationException(nameof(value));

            _translationVector = value;
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
    public bool IsValid()
    {
        return RotateMap.IsValid() &&
               _translationVector.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix4 GetSquareMatrix4()
    {
        return new SquareMatrix4(GetArray2D());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Matrix4x4 GetMatrix4x4()
    {
        return GetArray2D().ToMatrix4x4();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[,] GetArray2D()
    {
        var array = _rotateMap.GetArray2D();

        array[0, 0] *= _scalingFactor;
        array[1, 1] *= _scalingFactor;
        array[2, 2] *= _scalingFactor;

        array[0, 3] = _translationVector.X;
        array[1, 3] = _translationVector.Y;
        array[2, 3] = _translationVector.Z;

        return array;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapPoint(ILinFloat64Vector3D point)
    {
        var p = _rotateMap.MapPoint(point);

        return LinFloat64Vector3D.Create(_scalingFactor * p.X + _translationVector.X,
            _scalingFactor * p.Y + _translationVector.Y,
            _scalingFactor * p.Z + _translationVector.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapVector(ILinFloat64Vector3D vector)
    {
        var p = _rotateMap.MapPoint(vector);

        return LinFloat64Vector3D.Create(_scalingFactor * p.X,
            _scalingFactor * p.Y,
            _scalingFactor * p.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapNormal(ILinFloat64Vector3D normal)
    {
        var p = _rotateMap.MapPoint(normal);

        return LinFloat64Vector3D.Create(_scalingFactor * p.X,
            _scalingFactor * p.Y,
            _scalingFactor * p.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IFloat64AffineMap3D GetInverseAffineMap()
    {
        return new Float64TranslateUniformScaleRotateAffineMap3D()
        {
            RotateMap = _rotateMap.InverseRotateMap(),
            ScalingFactor = 1d / _scalingFactor,
            TranslationVector = -_translationVector
        };
    }
}