using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace NumericalGeometryLib.BasicMath.Maps.Space3D
{
    public abstract class RotateMap3D :
        IRotateMap3D
    {
        protected SquareMatrix3 RotationMatrix { get; set; }


        public bool SwapsHandedness 
            => false;

        public abstract bool IsValid();

        protected abstract void UpdateRotationMatrix();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SquareMatrix4 GetSquareMatrix4()
        {
            return SquareMatrix4.CreateAffineMatrix3D(RotationMatrix);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix4x4 GetMatrix4x4()
        {
            return new Matrix4x4(
                (float) RotationMatrix.Scalar00, (float) RotationMatrix.Scalar01, (float) RotationMatrix.Scalar02, 0f,
                (float) RotationMatrix.Scalar10, (float) RotationMatrix.Scalar11, (float) RotationMatrix.Scalar12, 0f,
                (float) RotationMatrix.Scalar20, (float) RotationMatrix.Scalar21, (float) RotationMatrix.Scalar22, 0f,
                0f, 0f, 0f, 1f
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[,] GetArray2D()
        {
            var array = new double[4, 4];

            array[0, 0] = RotationMatrix.Scalar00; array[0, 1] = RotationMatrix.Scalar01; array[0, 2] = RotationMatrix.Scalar02;
            array[1, 0] = RotationMatrix.Scalar10; array[1, 1] = RotationMatrix.Scalar11; array[1, 2] = RotationMatrix.Scalar12;
            array[2, 0] = RotationMatrix.Scalar20; array[2, 1] = RotationMatrix.Scalar21; array[2, 2] = RotationMatrix.Scalar22;
            array[3, 3] = 1d;

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D MapPoint(IFloat64Tuple3D point)
        {
            return RotationMatrix * point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D MapVector(IFloat64Tuple3D vector)
        {
            return RotationMatrix * vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D MapNormal(IFloat64Tuple3D normal)
        {
            return RotationMatrix * normal;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IAffineMap3D GetInverseAffineMap()
        {
            return InverseRotateMap();
        }

        public abstract IRotateMap3D InverseRotateMap();

        public abstract Tuple<PlanarAngle, Float64Tuple3D> GetAngleAxis();
    }
}