using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Frames.Space2D;

/// <summary>
/// This class represents a directions frame of 2 orthonormal vectors U, V where
/// the components are double precision numbers
/// </summary>
public class LinFloat64AffineFrame2D
{
    public static LinFloat64AffineFrame2D Create(ILinFloat64Vector2D origin, ILinFloat64Vector2D uDirection, ILinFloat64Vector2D vDirection)
    {
        return new LinFloat64AffineFrame2D(
            origin.X,
            origin.Y,
            uDirection.X,
            uDirection.Y,
            vDirection.X,
            vDirection.Y
        );
    }

    /// <summary>
    /// Create a right-handed orthonormal affine frame from the given origin and vector
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="uDirection"></param>
    /// <returns></returns>
    public static LinFloat64AffineFrame2D CreateRightHanded(ILinFloat64Vector2D origin, ILinFloat64Vector2D uDirection)
    {
        Debug.Assert(origin.IsValid() && uDirection.IsValid());

        var s = uDirection.VectorENorm();

        Debug.Assert(!s.IsNearZero());

        s = 1.0d / s;
        return new LinFloat64AffineFrame2D(
            origin.X, origin.Y,
            uDirection.X * s, uDirection.Y * s,
            true
        );
    }

    /// <summary>
    /// Create a left-handed orthonormal affine frame from the given origin and vector
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="uDirection"></param>
    /// <returns></returns>
    public static LinFloat64AffineFrame2D CreateLeftHanded(ILinFloat64Vector2D origin, ILinFloat64Vector2D uDirection)
    {
        var s = uDirection.VectorENorm();

        Debug.Assert(!s.IsNearZero());

        s = 1.0d / s;
        return new LinFloat64AffineFrame2D(
            origin.X, origin.Y,
            uDirection.X * s, uDirection.Y * s,
            true
        );
    }


    public double OriginX { get; }

    public double OriginY { get; }

    public double UDirectionX { get; }

    public double UDirectionY { get; }

    public double VDirectionX { get; }

    public double VDirectionY { get; }

    public LinFloat64Vector2D Origin
        => LinFloat64Vector2D.Create((Float64Scalar)OriginX, (Float64Scalar)OriginY);

    public LinFloat64Vector2D UDirection
        => LinFloat64Vector2D.Create((Float64Scalar)UDirectionX, (Float64Scalar)UDirectionY);

    public LinFloat64Vector2D VDirection
        => LinFloat64Vector2D.Create((Float64Scalar)VDirectionX, (Float64Scalar)VDirectionY);

    public bool IsRightHanded
        => UDirection.Determinant(VDirection) > 0.0d;

    public bool IsLeftHanded
        => UDirection.Determinant(VDirection) < 0.0d;


    private LinFloat64AffineFrame2D(double ox, double oy, double ux, double uy, bool rightHanded)
    {
        Debug.Assert((ux * ux + uy * uy).IsNearEqual(1.0d));

        OriginX = ox;
        OriginY = oy;

        UDirectionX = ux;
        UDirectionY = uy;

        if (rightHanded)
        {
            VDirectionX = -uy;
            VDirectionY = ux;
        }
        else
        {
            VDirectionX = uy;
            VDirectionY = -ux;
        }
    }

    private LinFloat64AffineFrame2D(double ox, double oy, double ux, double uy, double vx, double vy)
    {
        OriginX = ox;
        OriginY = oy;

        UDirectionX = ux;
        UDirectionY = uy;

        VDirectionX = vx;
        VDirectionY = vy;
    }


    public bool IsValid()
    {
        return !double.IsNaN(OriginX) &&
               !double.IsNaN(OriginY) &&
               !double.IsNaN(UDirectionX) &&
               !double.IsNaN(UDirectionY) &&
               !double.IsNaN(VDirectionX) &&
               !double.IsNaN(VDirectionY);
    }
}