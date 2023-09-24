using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Maps.Space3D
{
    public sealed class UniformScaleMap3D :
        IAffineMap3D
    {
        private double _scalingFactor = 1d;
        public double ScalingFactor
        {
            get => _scalingFactor;
            set
            {
                if (value is < 0 or Double.NaN || double.IsInfinity(value))
                    throw new InvalidOperationException(nameof(value));

                _scalingFactor = value;
            }
        }

        public bool SwapsHandedness 
            => false;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniformScaleMap3D(double scalingFactor = 1d)
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
            var s = (float) _scalingFactor;

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
        public Float64Vector3D MapPoint(IFloat64Vector3D point)
        {
            return Float64Vector3D.Create(_scalingFactor * point.X,
                _scalingFactor * point.Y,
                _scalingFactor * point.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D MapVector(IFloat64Vector3D vector)
        {
            return Float64Vector3D.Create(_scalingFactor * vector.X,
                _scalingFactor * vector.Y,
                _scalingFactor * vector.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D MapNormal(IFloat64Vector3D normal)
        {
            return Float64Vector3D.Create(_scalingFactor * normal.X,
                _scalingFactor * normal.Y,
                _scalingFactor * normal.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IAffineMap3D GetInverseAffineMap()
        {
            return new UniformScaleMap3D(1d / _scalingFactor);
        }
    }
}