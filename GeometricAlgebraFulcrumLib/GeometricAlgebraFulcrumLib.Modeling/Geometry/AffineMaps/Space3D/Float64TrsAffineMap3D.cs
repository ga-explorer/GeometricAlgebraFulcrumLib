﻿using System.Diagnostics;
using System.Numerics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;

//TODO: Review and integrate this class
/// <summary>
/// Represents a Stretch then Rotate then Translate map
/// </summary>
public sealed class Float64TrsAffineMap3D : IFloat64AffineMap3D
{


    public static Float64TrsAffineMap3D CreateFromMatrix(SquareMatrix4 c)
    {
        var newMap = new Float64TrsAffineMap3D();

        //Extract translation part
        newMap.TranslationVector = LinFloat64Vector3D.Create(c.Scalar03,
            c.Scalar13,
            c.Scalar23);

        //Separate upper left 3x3 block into rotation of stretch
        var m = c.UpperLeftBlock3X3;
        var r = m.NearestOrthogonalMatrix();
        newMap.StretchMatrix = r.Transpose() * m;

        return newMap;
    }

    //TODO: Implement this using GA and compare with classical method

    /// <summary>
    /// The translation vector
    /// </summary>
    public LinFloat64Vector3D TranslationVector { get; set; }

    /// <summary>
    /// The direction of the rotation axis; this must be a unit vector
    /// </summary>
    public LinFloat64Vector3D RotationVector { get; set; }

    /// <summary>
    /// The angle of the rotation
    /// </summary>
    public double RotationAngle { get; set; }

    /// <summary>
    /// The symmetric stretch matrix
    /// </summary>
    public SquareMatrix3 StretchMatrix { get; set; }

    public SquareMatrix3 RotationMatrix
    {
        get
        {
            var qr = Math.Cos(RotationAngle);
            var sinAngle = Math.Sin(RotationAngle);
            var qi = RotationVector.X * sinAngle;
            var qj = RotationVector.Y * sinAngle;
            var qk = RotationVector.Z * sinAngle;

            return new SquareMatrix3()
            {
                Scalar00 = 1.0d - 2.0d * (qj * qj + qk * qk),
                Scalar10 = 2.0d * (qi * qj - qk * qr),
                Scalar20 = 2.0d * (qi * qk + qj * qr),

                Scalar01 = 2.0d * (qi * qj + qk * qr),
                Scalar11 = 1.0d - 2.0d * (qi * qi + qk * qk),
                Scalar21 = 2.0d * (qj * qk - qi * qr),

                Scalar02 = 2.0d * (qi * qk - qj * qr),
                Scalar12 = 2.0d * (qj * qk + qi * qr),
                Scalar22 = 1.0d - 2.0d * (qi * qi + qj * qj)
            };
        }
    }

    public bool SwapsHandedness { get; }


    public bool IsIdentity()
    {
        throw new NotImplementedException();
    }

    public bool IsNearIdentity(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        throw new NotImplementedException();
    }

    public SquareMatrix4 GetSquareMatrix4()
    {
        Debug.Assert(RotationVector.IsUnitVector());

        return new SquareMatrix4
        {
            UpperLeftBlock3X3 = RotationMatrix * StretchMatrix,
            UpperRightBlock3X1 = TranslationVector,
            Scalar33 = 1.0d
        };
    }

    public Matrix4x4 GetMatrix4x4()
    {
        return GetSquareMatrix4().GetMatrix4x4();
    }

    public double[,] GetArray2D()
    {
        throw new NotImplementedException();
    }

    public LinFloat64Vector3D MapPoint(ILinFloat64Vector3D point)
    {
        throw new NotImplementedException();
    }

    public LinFloat64Vector3D MapVector(ILinFloat64Vector3D point)
    {
        throw new NotImplementedException();
    }

    public LinFloat64Vector3D MapNormal(ILinFloat64Vector3D normal)
    {
        throw new NotImplementedException();
    }

    public IFloat64AffineMap3D GetInverseAffineMap()
    {
        throw new NotImplementedException();
    }

    public bool IsValid()
    {
        throw new NotImplementedException();
    }
}