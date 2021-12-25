﻿using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace NumericalGeometryLib.BasicMath.Maps.Space3D
{
    /// <summary>
    /// Applies a rotation followed by a uniform scaling followed by translation
    /// </summary>
    public class RotateUniformScaleTranslateMap3D : 
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double[,] ToArray2D()
        {
            var array = _rotateMap.ToArray2D();

            array[0, 0] *= _scalingFactor;
            array[1, 1] *= _scalingFactor;
            array[2, 2] *= _scalingFactor;

            array[0, 3] = _translationVector.X;
            array[1, 3] = _translationVector.Y;
            array[2, 3] = _translationVector.Z;

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D MapPoint(ITuple3D point)
        {
            var p = _rotateMap.MapPoint(point);

            return new Tuple3D(
                _scalingFactor * p.X + _translationVector.X,
                _scalingFactor * p.Y + _translationVector.Y,
                _scalingFactor * p.Z + _translationVector.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D MapVector(ITuple3D vector)
        {
            var p = _rotateMap.MapPoint(vector);

            return new Tuple3D(
                _scalingFactor * p.X,
                _scalingFactor * p.Y,
                _scalingFactor * p.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D MapNormal(ITuple3D normal)
        {
            var p = _rotateMap.MapPoint(normal);

            return new Tuple3D(
                _scalingFactor * p.X,
                _scalingFactor * p.Y,
                _scalingFactor * p.Z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IAffineMap3D InverseMap()
        {
            return new TranslateUniformScaleRotateMap3D()
            {
                RotateMap = _rotateMap.InverseRotateMap(),
                ScalingFactor = 1d / _scalingFactor,
                TranslationVector = -_translationVector
            };
        }
    }
}