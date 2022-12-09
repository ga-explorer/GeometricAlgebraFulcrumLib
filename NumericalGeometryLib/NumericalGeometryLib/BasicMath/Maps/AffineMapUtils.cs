using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Frames.Space3D;
using NumericalGeometryLib.BasicMath.Maps.Space3D;
using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using NumericalGeometryLib.Borders.Space3D.Mutable;

namespace NumericalGeometryLib.BasicMath.Maps
{
    public static class AffineMapUtils
    {
        private static Tuple<Float64Tuple3D, Float64Tuple3D> MapPoint(this SquareMatrix4 mapMatrix, Float64Tuple3D point, Float64Tuple3D ptError)
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
                    new Float64Tuple3D(xp, yp, zp),
                    new Float64Tuple3D(absErrorX, absErrorY, absErrorZ)
                );
            else
            {
                var s = 1.0d / wp;

                return Tuple.Create(
                    new Float64Tuple3D(xp * s, yp * s, zp * s),
                    new Float64Tuple3D(absErrorX, absErrorY, absErrorZ)
                );
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MutableBoundingBox3D MapBoundingBox(this IAffineMap3D affineMap, MutableBoundingBox3D boundingBox)
        {
            return boundingBox.MapUsing(affineMap) as MutableBoundingBox3D;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D MapUnitVector(this IAffineMap3D linearMap, IFloat64Tuple3D vector)
        {
            return linearMap.MapVector(vector).ToUnitVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D MapUnitNormal(this IAffineMap3D linearMap, IFloat64Tuple3D normal)
        {
            return linearMap.MapNormal(normal).ToUnitVector();
        }

        public static LinearFrame3D MapFrame(this IAffineMap3D linearMap, LinearFrame3D frame)
        {
            //TODO: Use GMac to generate this and compare
            //Map the 3 vectors and apply The Gram–Schmidt process
            //https://en.wikipedia.org/wiki/Gram%E2%80%93Schmidt_process
            var uDirection = linearMap.MapVector(frame.UDirection).ToUnitVector();

            var vDirection = linearMap.MapVector(frame.VDirection);
            vDirection = vDirection.ToTuple3D() -
                         vDirection.ProjectOnUnitVector(uDirection);
            vDirection = vDirection.ToUnitVector();

            var wDirection = linearMap.MapVector(frame.WDirection);
            wDirection = wDirection.ToTuple3D() -
                         wDirection.ProjectOnUnitVector(uDirection) -
                         wDirection.ProjectOnUnitVector(vDirection);
            wDirection = wDirection.ToUnitVector();

            return new LinearFrame3D(uDirection, vDirection, wDirection);
        }

        public static TrsMap3D Lerp(this double t, TrsMap3D v1, TrsMap3D v2)
        {
            var s = 1.0d - t;

            var newValue = new TrsMap3D
            {
                RotationAngle = s * v1.RotationAngle + t * v2.RotationAngle,
                RotationVector = new Float64Tuple3D(
                    s * v1.RotationVector.X + t * v2.RotationVector.X,
                    s * v1.RotationVector.Y + t * v2.RotationVector.Y,
                    s * v1.RotationVector.Z + t * v2.RotationVector.Z
                ),
                TranslationVector = new Float64Tuple3D(
                    s * v1.TranslationVector.X + t * v2.TranslationVector.X,
                    s * v1.TranslationVector.Y + t * v2.TranslationVector.Y,
                    s * v1.TranslationVector.Z + t * v2.TranslationVector.Z
                ),
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
}
