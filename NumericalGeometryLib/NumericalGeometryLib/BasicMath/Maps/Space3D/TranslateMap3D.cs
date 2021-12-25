using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace NumericalGeometryLib.BasicMath.Maps.Space3D
{
    public sealed class TranslateMap3D :
        IAffineMap3D
    {
        private Tuple3D _translationVector = Tuple3D.Zero;
        public Tuple3D TranslationVector
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
        public TranslateMap3D(ITuple3D translationVector)
        {
            TranslationVector = translationVector.ToTuple3D();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return TranslationVector.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SquareMatrix4 ToSquareMatrix4()
        {
            return SquareMatrix4.CreateTranslationMatrix3D(_translationVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix4x4 ToMatrix4x4()
        {
            return new Matrix4x4(
                1, 0, 0, (float) _translationVector.X,
                0, 1, 0, (float) _translationVector.Y,
                0, 0, 1, (float) _translationVector.Z,
                0, 0, 0, 1
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[,] ToArray2D()
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
        public Tuple3D MapPoint(ITuple3D point)
        {
            return TranslationVector + point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D MapVector(ITuple3D vector)
        {
            return vector.ToTuple3D();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D MapNormal(ITuple3D normal)
        {
            return normal.ToTuple3D();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IAffineMap3D InverseMap()
        {
            return new TranslateMap3D(-TranslationVector);
        }
    }
}