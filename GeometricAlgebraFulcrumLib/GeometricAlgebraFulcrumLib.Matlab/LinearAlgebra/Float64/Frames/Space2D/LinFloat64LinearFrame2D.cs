using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Frames.Space2D;

/// <summary>
/// This class represents a directions frame of 2 orthonormal vectors U, V where
/// the components are double precision numbers
/// </summary>
public class LinFloat64LinearFrame2D
{
    /// <summary>
    /// Create a set of 2 right-handed orthonormal direction vectors from the given vector
    /// </summary>
    /// <returns></returns>
    public static LinFloat64LinearFrame2D CreateRightHanded(ILinFloat64Vector2D uDirection)
    {
        var s = uDirection.VectorENorm();

        Debug.Assert(!s.IsNearZero());

        s = 1.0d / s;
        return new LinFloat64LinearFrame2D(uDirection.X * s, uDirection.Y * s, true);
    }

    /// <summary>
    /// Create a set of 2 left-handed orthonormal direction vectors from the given vector
    /// </summary>
    /// <param name="uDirection"></param>
    /// <returns></returns>
    public static LinFloat64LinearFrame2D CreateLeftHanded(ILinFloat64Vector2D uDirection)
    {
        var s = uDirection.VectorENorm();

        Debug.Assert(!s.IsNearZero());

        s = 1.0d / s;
        return new LinFloat64LinearFrame2D(uDirection.X * s, uDirection.Y * s, false);
    }



    public double UDirectionX { get; }

    public double UDirectionY { get; }

    public double VDirectionX { get; }

    public double VDirectionY { get; }

    public LinFloat64Vector2D UDirection
    {
        get { return LinFloat64Vector2D.Create(UDirectionX, UDirectionY); }
    }

    public LinFloat64Vector2D VDirection
    {
        get { return LinFloat64Vector2D.Create(VDirectionX, VDirectionY); }
    }

    public bool IsRightHanded
    {
        get { return UDirection.Determinant(VDirection) > 0.0d; }
    }

    public bool IsLeftHanded
    {
        get { return UDirection.Determinant(VDirection) < 0.0d; }
    }

    public bool HasNaNComponent
    {
        get
        {
            return double.IsNaN(UDirectionX) || double.IsNaN(UDirectionY) ||
                   double.IsNaN(VDirectionX) || double.IsNaN(VDirectionY);
        }
    }


    public LinFloat64LinearFrame2D(ILinFloat64Vector2D uDirection, ILinFloat64Vector2D vDirection)
    {
        UDirectionX = uDirection.X;
        UDirectionY = uDirection.Y;

        VDirectionX = vDirection.X;
        VDirectionY = vDirection.Y;

        Debug.Assert(!HasNaNComponent);
    }

    private LinFloat64LinearFrame2D(double ux, double uy, bool rightHanded)
    {
        Debug.Assert((ux * ux + uy * uy).IsNearOne());

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

        Debug.Assert(!HasNaNComponent);
    }
}