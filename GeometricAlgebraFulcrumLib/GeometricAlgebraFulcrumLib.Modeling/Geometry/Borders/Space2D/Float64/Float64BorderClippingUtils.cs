using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space2D.Float64
{
    public static class Float64BorderClippingUtils
    {
        private static LinFloat64Vector3D PointCross(LinFloat64Vector2D a, LinFloat64Vector2D b)
        {
            return LinFloat64Vector3D.Create(
                a.Item2 - b.Item2, 
                b.Item1 - a.Item1, 
                a.Item1 * b.Item2 - a.Item2 * b.Item1
            );
        }

        private static LinFloat64Vector2D Cross(LinFloat64Vector3D u, LinFloat64Vector3D v)
        {
            var z = u.Item1 * v.Item2 - u.Item2 * v.Item1;

            return LinFloat64Vector2D.Create(
                (u.Item2 * v.Item3 - u.Item3 * v.Item2) / z, 
                (u.Item3 * v.Item1 - u.Item1 * v.Item3) / z
            );
        }


        private static readonly int[] Mask = [0, 1, 2, 2, 4, 0, 4, 4, 8, 1, 0, 2, 8, 1, 8, 0];
        private static readonly int[] Tab1 = [4, 3, 0, 3, 1, 4, 0, 3, 2, 2, 4, 2, 1, 1, 0, 4];
        private static readonly int[] Tab2 = [4, 0, 1, 1, 2, 4, 2, 2, 3, 0, 4, 1, 3, 0, 3, 4];


        private static Pair<LinFloat64Vector3D[]> GetBorderAffineData(this IFloat64BoundingBox2D box)
        {
            var x = new[]
            {
                LinFloat64Vector3D.Create(box.MinX, box.MinY, 1),
                LinFloat64Vector3D.Create(box.MaxX, box.MinY, 1),
                LinFloat64Vector3D.Create(box.MaxX, box.MaxY, 1),
                LinFloat64Vector3D.Create(box.MinX, box.MaxY, 1)
            };

            var e = new[]
            {
                LinFloat64Vector3D.Create(0, 1, -box.MinY),
                LinFloat64Vector3D.Create(1, 0, -box.MaxX),
                LinFloat64Vector3D.Create(0, 1, -box.MaxY),
                LinFloat64Vector3D.Create(1, 0, -box.MinX)
            };

            return new Pair<LinFloat64Vector3D[]>(x, e);
        }

        private static int GetCode(this IFloat64BoundingBox2D box, LinFloat64Vector2D point)
        {
            var c = 0;

            if (point.Item1 < box.MinX)
                c = 8;

            else if (point.Item1 > box.MaxX) 
                c = 2;

            if (point.Item2 < box.MinY)
                c |= 1;

            else if (point.Item2 > box.MaxY) 
                c |= 4;

            return c;
        }

        /// <summary>
        /// Line intersection with Axis-Aligned-Box using Skala algorithm
        /// https://en.wikipedia.org/wiki/Line_clipping
        /// https://link.springer.com/article/10.1007/s00371-005-0305-3
        /// </summary>
        /// <param name="box"></param>
        /// <param name="xA"></param>
        /// <param name="xB"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static Tuple<int, LinFloat64Vector2D, LinFloat64Vector2D> ClipLine(this IFloat64BoundingBox2D box, LinFloat64Vector2D xA, LinFloat64Vector2D xB)
        {
            var (x, e) = GetBorderAffineData(box);

            var cA = box.GetCode(xA);
            var cB = box.GetCode(xB);

            if ((cA | cB) == 0)
                return new Tuple<int, LinFloat64Vector2D, LinFloat64Vector2D>(0, xA, xB);

            if ((cA & cB) != 0)
                return new Tuple<int, LinFloat64Vector2D, LinFloat64Vector2D>(-1, xA, xB);

            var p = PointCross(xA, xB);

            var c = 0;

            for (var k = 0; k < 4; k += 1)
            {
                if (p.VectorESp(x[k]) <= 0)
                {
                    c |= (1 << k);
                }
            }

            if (c is 0 or 15)
                return new Tuple<int, LinFloat64Vector2D, LinFloat64Vector2D>(-1, xA, xB);

            var i = Tab1[c];
            var j = Tab2[c];

            if (cA != 0 && cB != 0)
            {
                var xANew = Cross(p, e[i]);
                var xBNew = Cross(p, e[j]);

                return new Tuple<int, LinFloat64Vector2D, LinFloat64Vector2D>(3, xANew, xBNew);
            }

            if (cA == 0)
            {
                var xBNew = 
                    Cross(p, (cB & Mask[c]) == 0 ? e[i] : e[j]);

                return new Tuple<int, LinFloat64Vector2D, LinFloat64Vector2D>(2, xA, xBNew);
            }

            if (cB == 0)
            {
                var xANew = 
                    Cross(p, (cA & Mask[c]) == 0 ? e[i] : e[j]);

                return new Tuple<int, LinFloat64Vector2D, LinFloat64Vector2D>(1, xANew, xB);
            }

            throw new InvalidOperationException();
        }


    }
}
