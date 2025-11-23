using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using SixLabors.ImageSharp;
using System.Runtime.CompilerServices;
// ReSharper disable PossibleMultipleEnumeration

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ImageSharp
{
    public static class ImageSharpUtils
    {
        //public static PointF ToImageSharpPointF(this IPair<int> pixel)
        //{
        //    return new PointF(pixel.Item1, pixel.Item2);
        //}
        
        //public static PointF ToImageSharpPointF(this IPair<float> pixel)
        //{
        //    return new PointF(pixel.Item1, pixel.Item2);
        //}

        //public static PointF ToImageSharpPointF(this IPair<double> point)
        //{
        //    return new PointF((float)point.Item1, (float)point.Item2);
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF ToImageSharpPointF(this IPair<double> point, WorldToScreenMap2D wtsMap)
        {
            var pixel = wtsMap.MapPoint(point);

            return new PointF(pixel.Item1, pixel.Item2);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF MapPointToImageSharpPointF(this WorldToScreenMap2D wtsMap, IPair<double> point)
        {
            var pixel = wtsMap.MapPoint(point);

            return new PointF(pixel.Item1, pixel.Item2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF MapPointToImageSharpPointF(this WorldToScreenMap2D wtsMap, double x, double y)
        {
            var pixel = wtsMap.MapPoint(x, y);

            return new PointF(pixel.Item1, pixel.Item2);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF MapVectorToImageSharpPointF(this WorldToScreenMap2D wtsMap, IPair<double> vector)
        {
            var pixel = wtsMap.MapVector(vector);

            return new PointF(pixel.Item1, pixel.Item2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF MapVectorToImageSharpPointF(this WorldToScreenMap2D wtsMap, double x, double y)
        {
            var pixel = wtsMap.MapVector(x, y);

            return new PointF(pixel.Item1, pixel.Item2);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeF MapSizeToImageSharpSizeF(this WorldToScreenMap2D wtsMap, double size)
        {
            var pixel = wtsMap.MapSize(size);

            return new SizeF(pixel.Item1, pixel.Item2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeF MapSizeToImageSharpSizeF(this WorldToScreenMap2D wtsMap, double width, double height)
        {
            var pixel = wtsMap.MapSize(width, height);

            return new SizeF(pixel.Item1, pixel.Item2);
        }


        public static LinFloat64PolarAngle GetArrowHeadAngle(this LinFloat64Vector2D direction)
        {
            var unitDirection = 
                direction.VectorDivideByENorm();

            return LinFloat64PolarAngle.CreateFromCosSin(
                unitDirection.Item1, 
                unitDirection.Item2
            );
        }


    }
}
