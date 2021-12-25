using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace NumericalGeometryLib.BasicMath.Matrices
{
    public static class MatrixUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4 ToMatrix4x4(this double[,] array)
        {
            return new Matrix4x4(
                (float) array[0, 0], (float) array[0, 1], (float) array[0, 2], (float) array[0, 3],
                (float) array[1, 0], (float) array[1, 1], (float) array[1, 2], (float) array[1, 3],
                (float) array[2, 0], (float) array[2, 1], (float) array[2, 2], (float) array[2, 3],
                (float) array[3, 0], (float) array[3, 1], (float) array[3, 2], (float) array[3, 3]
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple3D TimesVector(this SquareMatrix4 matrix, ITuple3D vector)
        {
            return new Tuple3D(
                matrix[0, 0] * vector.X + matrix[0, 1] * vector.Y + matrix[0, 2] * vector.Z,
                matrix[1, 0] * vector.X + matrix[1, 1] * vector.Y + matrix[1, 2] * vector.Z,
                matrix[2, 0] * vector.X + matrix[2, 1] * vector.Y + matrix[2, 2] * vector.Z
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Pair<Tuple3D> TimesVectors(this SquareMatrix4 matrix, ITuple3D vector1, ITuple3D vector2)
        {
            return new Pair<Tuple3D>(
                matrix.TimesVector(vector1),
                matrix.TimesVector(vector2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Triplet<Tuple3D> TimesVectors(this SquareMatrix4 matrix, ITuple3D vector1, ITuple3D vector2, ITuple3D vector3)
        {
            return new Triplet<Tuple3D>(
                matrix.TimesVector(vector1),
                matrix.TimesVector(vector2),
                matrix.TimesVector(vector3)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple3D TimesPoint(this SquareMatrix4 matrix, ITuple3D point)
        {
            return new Tuple3D(
                matrix[0, 0] * point.X + matrix[0, 1] * point.Y + matrix[0, 2] * point.Z + matrix[0, 3],
                matrix[1, 0] * point.X + matrix[1, 1] * point.Y + matrix[1, 2] * point.Z + matrix[1, 3],
                matrix[2, 0] * point.X + matrix[2, 1] * point.Y + matrix[2, 2] * point.Z + matrix[2, 3]
            );
        }

   
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<double> GetComponents(this SquareMatrix4 matrix, bool columnMajorOrder = true)
        {
            if (columnMajorOrder)
            {
                yield return matrix[0];
                yield return matrix[1];
                yield return matrix[2];
                yield return matrix[3];

                yield return matrix[4];
                yield return matrix[5];
                yield return matrix[6];
                yield return matrix[7];

                yield return matrix[8];
                yield return matrix[9];
                yield return matrix[10];
                yield return matrix[11];

                yield return matrix[12];
                yield return matrix[13];
                yield return matrix[14];
                yield return matrix[15];
            }
            else
            {
                yield return matrix[0];
                yield return matrix[4];
                yield return matrix[8];
                yield return matrix[12];

                yield return matrix[1];
                yield return matrix[5];
                yield return matrix[9];
                yield return matrix[13];

                yield return matrix[2];
                yield return matrix[6];
                yield return matrix[10];
                yield return matrix[14];

                yield return matrix[3];
                yield return matrix[7];
                yield return matrix[11];
                yield return matrix[15];
            }
        }

    }
}
