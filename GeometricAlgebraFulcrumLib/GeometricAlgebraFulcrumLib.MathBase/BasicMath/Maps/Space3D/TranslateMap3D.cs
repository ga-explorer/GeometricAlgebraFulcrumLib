using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.Space3D
{
    public sealed class TranslateMap3D :
        IAffineMap3D
    {
        private Float64Vector3D _translationVector = Float64Vector3D.Zero;
        public Float64Vector3D TranslationVector
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


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TranslateMap3D()
        {
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TranslateMap3D(IFloat64Tuple3D translationVector)
        {
            TranslationVector = translationVector.ToVector3D();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return TranslationVector.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SquareMatrix4 GetSquareMatrix4()
        {
            return SquareMatrix4.CreateTranslationMatrix3D(_translationVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix4x4 GetMatrix4x4()
        {
            return new Matrix4x4(
                1, 0, 0, (float) _translationVector.X,
                0, 1, 0, (float) _translationVector.Y,
                0, 0, 1, (float) _translationVector.Z,
                0, 0, 0, 1
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[,] GetArray2D()
        {
            var array = new double[4, 4];

            array[0, 3] = _translationVector.X;
            array[1, 3] = _translationVector.Y;
            array[2, 3] = _translationVector.Z;

            array[0, 0] = 1d;
            array[1, 1] = 1d;
            array[2, 2] = 1d;
            array[3, 3] = 1d;

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D MapPoint(IFloat64Tuple3D point)
        {
            return TranslationVector + point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D MapVector(IFloat64Tuple3D vector)
        {
            return vector.ToVector3D();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D MapNormal(IFloat64Tuple3D normal)
        {
            return normal.ToVector3D();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IAffineMap3D GetInverseAffineMap()
        {
            return new TranslateMap3D(-TranslationVector);
        }
    }
}