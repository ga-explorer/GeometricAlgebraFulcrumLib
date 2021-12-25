using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace NumericalGeometryLib.BasicMath.Maps.Space3D
{
    /// <summary>
    /// Applies a translation followed by a uniform scaling followed by rotation
    /// </summary>
    public class TranslateUniformScaleRotateMap3D :
        IAffineMap3D
    {
        private IRotateMap3D _rotateMap = IdentityMap3D.DefaultMap;
        public IRotateMap3D RotateMap
        {
            get => _rotateMap;
            set
            {
                if (value is null || !value.IsValid())
                    throw new ArgumentException(nameof(value));

                _rotateMap = value;
            }
        }

        private double _scalingFactor = 1d;
        public double ScalingFactor
        {
            get => _scalingFactor;
            set
            {
                if (value < 0 || double.IsNaN(value) || double.IsInfinity(value))
                    throw new InvalidOperationException(nameof(value));

                _scalingFactor = value;
            }
        }

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
        public bool IsValid()
        {
            return RotateMap.IsValid() &&
                   _translationVector.IsValid();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SquareMatrix4 ToSquareMatrix4()
        {
            return new SquareMatrix4(ToArray2D());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix4x4 ToMatrix4x4()
        {
            return ToArray2D().ToMatrix4x4();
        }

        public double[,] ToArray2D()
        {
            var array = _rotateMap.ToArray2D();

            var c0 = MapVector(Tuple3D.E1);
            var c1 = MapVector(Tuple3D.E2);
            var c2 = MapVector(Tuple3D.E3);
            var c3 = MapPoint(Tuple3D.Zero);

            array[0, 0] = c0.X;
            array[1, 0] = c0.Y;
            array[2, 0] = c0.Z;

            array[0, 1] = c1.X;
            array[1, 1] = c1.Y;
            array[2, 1] = c1.Z;

            array[0, 2] = c2.X;
            array[1, 2] = c2.Y;
            array[2, 2] = c2.Z;

            array[0, 3] = c3.X;
            array[1, 3] = c3.Y;
            array[2, 3] = c3.Z;

            array[3, 3] = 1d;

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D MapPoint(ITuple3D point)
        {
            return _rotateMap.MapPoint(
                new Tuple3D(
                    _scalingFactor * (point.X + _translationVector.X),
                    _scalingFactor * (point.Y + _translationVector.Y),
                    _scalingFactor * (point.Z + _translationVector.Z)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D MapVector(ITuple3D vector)
        {
            return _rotateMap.MapPoint(
                new Tuple3D(
                    _scalingFactor * vector.X,
                    _scalingFactor * vector.Y,
                    _scalingFactor * vector.Z
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D MapNormal(ITuple3D normal)
        {
            return _rotateMap.MapPoint(
                new Tuple3D(
                    _scalingFactor * normal.X,
                    _scalingFactor * normal.Y,
                    _scalingFactor * normal.Z
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IAffineMap3D InverseMap()
        {
            return new RotateUniformScaleTranslateMap3D()
            {
                RotateMap = _rotateMap.InverseRotateMap(),
                ScalingFactor = 1d / _scalingFactor,
                TranslationVector = -_translationVector
            };
        }
    }
}