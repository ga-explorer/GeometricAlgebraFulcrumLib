namespace GeometricAlgebraFulcrumLib.MathBase.Signals.Interpolators.SavitzkyGolay
{
    /**
     * Filters data using Ramer-Douglas-Peucker algorithm with specified tolerance
     * 
     * @author Rzeźnik
     * @see <a href="http://en.wikipedia.org/wiki/Ramer-Douglas-Peucker_algorithm">Ramer-Douglas-Peucker algorithm</a>
     */
    public class SgRamerDouglasPeuckerFilter : ISgDataFilter
    {

        private double _epsilon;

        /**
         * 
         * @param epsilon
         *            epsilon in Ramer-Douglas-Peucker algorithm (maximum distance
         *            of a point in data between original curve and simplified
         *            curve)
         * @throws IllegalArgumentException
         *             when {@code epsilon <= 0}
         */
        public SgRamerDouglasPeuckerFilter(double epsilon)
        {
            if (epsilon <= 0)
            {
                throw new ArgumentException("Epsilon nust be > 0");
            }
            _epsilon = epsilon;
        }

        public double[] Filter(double[] data)
        {
            return RamerDouglasPeuckerFunction(data, 0, data.Length - 1);
        }

        /**
         * 
         * @return {@code epsilon}
         */
        public double GetEpsilon()
        {
            return _epsilon;
        }

        protected double[] RamerDouglasPeuckerFunction(double[] points,
            int startIndex, int endIndex)
        {
            double dmax = 0;
            var idx = 0;
            double a = endIndex - startIndex;
            var b = points[endIndex] - points[startIndex];
            var c = -(b * startIndex - a * points[startIndex]);
            var norm = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
            for (var i = startIndex + 1; i < endIndex; i++)
            {
                var distance = Math.Abs(b * i - a * points[i] + c) / norm;
                if (distance > dmax)
                {
                    idx = i;
                    dmax = distance;
                }
            }
            if (dmax >= _epsilon)
            {
                var recursiveResult1 = RamerDouglasPeuckerFunction(points, startIndex, idx);
                var recursiveResult2 = RamerDouglasPeuckerFunction(points, idx, endIndex);
                var result = new double[recursiveResult1.Length - 1 + recursiveResult2.Length];
                Array.Copy(recursiveResult1, 0, result, 0, recursiveResult1.Length - 1);
                Array.Copy(recursiveResult2, 0, result, recursiveResult1.Length - 1, recursiveResult2.Length);

                return result;
            }
            else
            {
                return new double[] { points[startIndex], points[endIndex] };
            }
        }

        /**
         * 
         * @param epsilon
         *            maximum distance of a point in data between original curve and
         *            simplified curve
         */
        public void SetEpsilon(double epsilon)
        {
            if (epsilon <= 0)
            {
                throw new ArgumentException("Epsilon nust be > 0");
            }
            _epsilon = epsilon;
        }

    }
}