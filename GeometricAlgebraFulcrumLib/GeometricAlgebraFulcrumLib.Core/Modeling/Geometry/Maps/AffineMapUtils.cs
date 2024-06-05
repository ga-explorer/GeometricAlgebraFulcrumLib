using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Frames.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Maps.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Maps;

public static class AffineMapUtils
{
    private static Tuple<LinFloat64Vector3D, LinFloat64Vector3D> MapPoint(this SquareMatrix4 mapMatrix, LinFloat64Vector3D point, LinFloat64Vector3D ptError)
    {
        //TODO: Study techniques in book "Computer-Aided Geometric Design - A Totally Four-Dimensional Approach 2002"
        //and section Section 3.9 on managing rounding errors in PBRT book
        var x = point.X;
        var y = point.Y;
        var z = point.Z;

        var absErrorX =
            (Float64Utils.Geomma3 + 1) *
            (Math.Abs(mapMatrix[0, 0]) * ptError.X + Math.Abs(mapMatrix[0, 1]) * ptError.Y +
             Math.Abs(mapMatrix[0, 2]) * ptError.Z) +
            Float64Utils.Geomma3 * (Math.Abs(mapMatrix[0, 0] * x) + Math.Abs(mapMatrix[0, 1] * y) +
                                    Math.Abs(mapMatrix[0, 2] * z) + Math.Abs(mapMatrix[0, 3]));
        var absErrorY =
            (Float64Utils.Geomma3 + 1) *
            (Math.Abs(mapMatrix[1, 0]) * ptError.X + Math.Abs(mapMatrix[1, 1]) * ptError.Y +
             Math.Abs(mapMatrix[1, 2]) * ptError.Z) +
            Float64Utils.Geomma3 * (Math.Abs(mapMatrix[1, 0] * x) + Math.Abs(mapMatrix[1, 1] * y) +
                                    Math.Abs(mapMatrix[1, 2] * z) + Math.Abs(mapMatrix[1, 3]));
        var absErrorZ =
            (Float64Utils.Geomma3 + 1) *
            (Math.Abs(mapMatrix[2, 0]) * ptError.X + Math.Abs(mapMatrix[2, 1]) * ptError.Y +
             Math.Abs(mapMatrix[2, 2]) * ptError.Z) +
            Float64Utils.Geomma3 * (Math.Abs(mapMatrix[2, 0] * x) + Math.Abs(mapMatrix[2, 1] * y) +
                                    Math.Abs(mapMatrix[2, 2] * z) + Math.Abs(mapMatrix[2, 3]));

        var p = mapMatrix.MapProjectivePoint(point);

        var xp = p.X;
        var yp = p.Y;
        var zp = p.Z;
        var wp = p.W;

        Debug.Assert(wp != 0.0d);

        if (wp == 1.0d)
            return Tuple.Create(
                LinFloat64Vector3D.Create(xp, yp, zp),
                LinFloat64Vector3D.Create(absErrorX, absErrorY, absErrorZ)
            );
        else
        {
            var s = 1.0d / wp;

            return Tuple.Create(
                LinFloat64Vector3D.Create(xp * s, yp * s, zp * s),
                LinFloat64Vector3D.Create(absErrorX, absErrorY, absErrorZ)
            );
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D MapUnitVector(this IAffineMap3D linearMap, ILinFloat64Vector3D vector)
    {
        return linearMap.MapVector(vector).ToUnitLinVector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D MapUnitNormal(this IAffineMap3D linearMap, ILinFloat64Vector3D normal)
    {
        return linearMap.MapNormal(normal).ToUnitLinVector3D();
    }

    public static LinearFrame3D MapFrame(this IAffineMap3D linearMap, LinearFrame3D frame)
    {
        //TODO: Use GMac to generate this and compare
        //Map the 3 vectors and apply The Gram–Schmidt process
        //https://en.wikipedia.org/wiki/Gram%E2%80%93Schmidt_process
        var uDirection = linearMap.MapVector(frame.UDirection).ToUnitLinVector3D();

        var vDirection = linearMap.MapVector(frame.VDirection);
        vDirection = vDirection -
                     vDirection.ProjectOnUnitVector(uDirection);
        vDirection = vDirection.ToUnitLinVector3D();

        var wDirection = linearMap.MapVector(frame.WDirection);
        wDirection = wDirection -
                     wDirection.ProjectOnUnitVector(uDirection) -
                     wDirection.ProjectOnUnitVector(vDirection);
        wDirection = wDirection.ToUnitLinVector3D();

        return new LinearFrame3D(uDirection, vDirection, wDirection);
    }

    public static TrsMap3D Lerp(this double t, TrsMap3D v1, TrsMap3D v2)
    {
        var s = 1.0d - t;

        var newValue = new TrsMap3D
        {
            RotationAngle = s * v1.RotationAngle + t * v2.RotationAngle,
            RotationVector = LinFloat64Vector3D.Create(s * v1.RotationVector.X + t * v2.RotationVector.X,
                s * v1.RotationVector.Y + t * v2.RotationVector.Y,
                s * v1.RotationVector.Z + t * v2.RotationVector.Z),
            TranslationVector = LinFloat64Vector3D.Create(s * v1.TranslationVector.X + t * v2.TranslationVector.X,
                s * v1.TranslationVector.Y + t * v2.TranslationVector.Y,
                s * v1.TranslationVector.Z + t * v2.TranslationVector.Z),
            StretchMatrix =
            {
                Scalar00 = s * v1.StretchMatrix.Scalar00 + t * v2.StretchMatrix.Scalar00,
                Scalar10 = s * v1.StretchMatrix.Scalar10 + t * v2.StretchMatrix.Scalar10,
                Scalar20 = s * v1.StretchMatrix.Scalar20 + t * v2.StretchMatrix.Scalar20,
                Scalar01 = s * v1.StretchMatrix.Scalar01 + t * v2.StretchMatrix.Scalar01,
                Scalar11 = s * v1.StretchMatrix.Scalar11 + t * v2.StretchMatrix.Scalar11,
                Scalar21 = s * v1.StretchMatrix.Scalar21 + t * v2.StretchMatrix.Scalar21,
                Scalar02 = s * v1.StretchMatrix.Scalar02 + t * v2.StretchMatrix.Scalar02,
                Scalar12 = s * v1.StretchMatrix.Scalar12 + t * v2.StretchMatrix.Scalar12,
                Scalar22 = s * v1.StretchMatrix.Scalar22 + t * v2.StretchMatrix.Scalar22
            }
        };

        return newValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<TrsMap3D> Lerp(this IEnumerable<double> tList, TrsMap3D v1, TrsMap3D v2)
    {
        return tList.Select(t => t.Lerp(v1, v2));
    }

}