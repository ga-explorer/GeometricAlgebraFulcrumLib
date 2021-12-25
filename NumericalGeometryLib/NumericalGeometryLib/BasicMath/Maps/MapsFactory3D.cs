using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Constants;
using NumericalGeometryLib.BasicMath.Maps.Space3D;
using NumericalGeometryLib.BasicMath.Tuples;

namespace NumericalGeometryLib.BasicMath.Maps
{
    public static class MapsFactory3D
    {
        public static IdentityMap3D IdentityMap3D 
            => IdentityMap3D.DefaultMap;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TranslateMap3D CreateTranslateMap3D(this ITuple3D translationVector)
        {
            return new TranslateMap3D(translationVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniformScaleMap3D CreateUniformScaleMap3D(this double scalingFactor)
        {
            return new UniformScaleMap3D(scalingFactor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RotateByAxisAngleMap3D CreateRotateMap3D(this ITuple3D normal, PlanarAngle angle)
        {
            return RotateByAxisAngleMap3D.Create(normal, angle);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RotateByAxisToVectorMap3D CreateRotateMap3D(this Axis3D axis, ITuple3D unitVector)
        {
            return RotateByAxisToVectorMap3D.Create(axis, unitVector);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RotateByVectorToAxisMap3D CreateRotateMap3D(this ITuple3D unitVector, Axis3D axis)
        {
            return RotateByVectorToAxisMap3D.Create(unitVector, axis);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RotateByVectorToVectorMap3D CreateRotateMap3D(this ITuple3D unitVector1, ITuple3D unitVector2)
        {
            return RotateByVectorToVectorMap3D.Create(unitVector1, unitVector2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RotateUniformScaleMap3D CreateRotateScaleMap3D(this ITuple3D normal, PlanarAngle angle, double scalingFactor)
        {
            return RotateByAxisAngleMap3D.Create(normal, angle).CreateRotateScaleMap3D(scalingFactor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RotateUniformScaleMap3D CreateRotateScaleMap3D(this Axis3D axis, ITuple3D unitVector, double scalingFactor)
        {
            return RotateByAxisToVectorMap3D.Create(axis, unitVector).CreateRotateScaleMap3D(scalingFactor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RotateUniformScaleMap3D CreateRotateScaleMap3D(this ITuple3D unitVector, Axis3D axis, double scalingFactor)
        {
            return RotateByVectorToAxisMap3D.Create(unitVector, axis).CreateRotateScaleMap3D(scalingFactor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RotateUniformScaleMap3D CreateRotateScaleMap3D(this ITuple3D unitVector1, ITuple3D unitVector2, double scalingFactor)
        {
            return RotateByVectorToVectorMap3D.Create(unitVector1, unitVector2).CreateRotateScaleMap3D(scalingFactor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RotateUniformScaleMap3D CreateRotateScaleMap3D(this IRotateMap3D rotateMap, double scalingFactor)
        {
            return new RotateUniformScaleMap3D()
            {
                RotateMap = rotateMap,
                ScalingFactor = scalingFactor
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RotateUniformScaleTranslateMap3D CreateRotateScaleTranslateMap3D(this IRotateMap3D rotateMap, double scalingFactor, ITuple3D translationVector)
        {
            return new RotateUniformScaleTranslateMap3D()
            {
                RotateMap = rotateMap,
                ScalingFactor = scalingFactor,
                TranslationVector = translationVector.ToTuple3D()
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TranslateUniformScaleRotateMap3D CreateTranslateScaleRotateMap3D(this ITuple3D translationVector, double scalingFactor, IRotateMap3D rotateMap)
        {
            return new TranslateUniformScaleRotateMap3D()
            {
                RotateMap = rotateMap,
                ScalingFactor = scalingFactor,
                TranslationVector = translationVector.ToTuple3D()
            };
        }
    }
}
