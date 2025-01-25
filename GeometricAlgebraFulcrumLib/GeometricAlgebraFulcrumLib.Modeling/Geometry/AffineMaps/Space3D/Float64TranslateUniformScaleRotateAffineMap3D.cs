using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;

/// <summary>
/// Applies a translation followed by a uniform scaling followed by rotation
/// </summary>
public class Float64TranslateUniformScaleRotateAffineMap3D :
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

    public double[,] GetArray2D()
    {
        var array = _rotateMap.GetArray2D();

        var c0 = MapVector(LinFloat64Vector3D.E1);
        var c1 = MapVector(LinFloat64Vector3D.E2);
        var c2 = MapVector(LinFloat64Vector3D.E3);
        var c3 = MapPoint(LinFloat64Vector3D.Zero);

        array[0, 0] = c0.X;
        array[1, 0] = c0.Y;
        array[2, 0] = c0.Z;

        array[0, 1] = c1.X;
        array[1, 1] = c1.Y;
        array[2, 1] = c1.Z;

        array[0, 2] = c2.X;
        array[1, 2] = c2.Y;
        array[2, 2] = c2.Z;

        array[0, 3] = c3.X;
        array[1, 3] = c3.Y;
        array[2, 3] = c3.Z;

        array[3, 3] = 1d;

        return array;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapPoint(ILinFloat64Vector3D point)
    {
        return _rotateMap.MapPoint(
            LinFloat64Vector3D.Create(_scalingFactor * (point.X + _translationVector.X),
                _scalingFactor * (point.Y + _translationVector.Y),
                _scalingFactor * (point.Z + _translationVector.Z))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapVector(ILinFloat64Vector3D vector)
    {
        return _rotateMap.MapPoint(
            LinFloat64Vector3D.Create(_scalingFactor * vector.X,
                _scalingFactor * vector.Y,
                _scalingFactor * vector.Z)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapNormal(ILinFloat64Vector3D normal)
    {
        return _rotateMap.MapPoint(
            LinFloat64Vector3D.Create(_scalingFactor * normal.X,
                _scalingFactor * normal.Y,
                _scalingFactor * normal.Z)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IFloat64AffineMap3D GetInverseAffineMap()
    {
        return new Float64RotateUniformScaleTranslateAffineMap3D()
        {
            RotateMap = _rotateMap.InverseRotateMap(),
            ScalingFactor = 1d / _scalingFactor,
            TranslationVector = -_translationVector
        };
    }
}