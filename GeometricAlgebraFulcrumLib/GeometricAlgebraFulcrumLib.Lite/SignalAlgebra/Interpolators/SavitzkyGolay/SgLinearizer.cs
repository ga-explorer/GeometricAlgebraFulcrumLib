namespace GeometricAlgebraFulcrumLib.Lite.SignalAlgebra.Interpolators.SavitzkyGolay
{

    /**
     * Linearizes data by seeking points with relative difference greater than
     * {@link #getTruncateRatio() truncateRatio} and replacing them with points
     * lying on line between the first and the last of such points. Strictly:
     * <p>
     * let <tt>delta(i)</tt> be function which assigns to an element at index
     * <tt>i (data[i])</tt>, for <tt>0
     * <= i < data.Length - 1</tt>, value of
     * <tt>|(data[i] - data[i+1])/data[i]|</tt>. Then for each range <tt>(j,k)</tt>
     * of data, such that
     * <tt>delta(j) > {@link #getTruncateRatio() truncateRatio}</tt> and
     * <tt>delta(k)
     * <= {@link #getTruncateRatio() truncateRatio}</tt>, <tt>data[x] = ((data[k] -
     * data[j])/(k - j)) * (x - k) + data[j])</tt> for <tt>j <= x <= k</tt>.
     * </p>
     * 
     * @author Marcin Rzeźnicki
     * 
     */
    public class SgLinearizer : ISgPreprocessor
    {

        private float _truncateRatio = 0.5f;

        /**
         * Default constructor. {@link #getTruncateRatio() truncateRatio} is 0.5
         */
        public SgLinearizer()
        {

        }

        /**
         * 
         * @param truncateRatio
         *            maximum relative difference of subsequent data points above
         *            which linearization begins
         * @throws IllegalArgumentException
         *             when {@code truncateRatio} < 0
         */
        public SgLinearizer(float truncateRatio)
        {
            if (truncateRatio < 0f)
                throw new ArgumentException("truncateRatio < 0");

            _truncateRatio = truncateRatio;
        }


        public void Apply(double[] data)
        {
            var n = data.Length - 1;
            var deltas = ComputeDeltas(data);
            for (var i = 0; i < n; i++)
            {
                if (deltas[i] <= _truncateRatio) continue;

                var continueFlag = false;
                for (var k = i + 1; k < n; k++)
                {
                    if (deltas[k] > _truncateRatio) continue;

                    Linest(data, i, k);
                    i = k - 1;

                    continueFlag = true;
                    break;
                }

                if (continueFlag) continue;

                Linest(data, i, n);
                break;
            }
        }

        private static double[] ComputeDeltas(IReadOnlyList<double> data)
        {
            var n = data.Count;
            var deltas = new double[n - 1];

            for (var i = 0; i < n - 1; i++)
            {
                deltas[i] = 
                    data[i] == 0 && data[i + 1] == 0 
                        ? 0 
                        : Math.Abs(1 - data[i + 1] / data[i]);
            }

            return deltas;
        }

        /**
         * 
         * @return {@code truncateRatio}
         */
        public float GetTruncateRatio()
        {
            return _truncateRatio;
        }

        protected void Linest(double[] data, int x0, int x1)
        {
            if (x0 + 1 == x1)
                return;
            var slope = (data[x1] - data[x0]) / (x1 - x0);
            var y0 = data[x0];
            for (var x = x0 + 1; x < x1; x++)
            {
                data[x] = slope * (x - x0) + y0;
            }
        }

        /**
         * 
         * @param truncateRatio
         *            maximum relative difference of subsequent data points above
         *            which linearization begins
         * @throws IllegalArgumentException
         *             when {@code truncateRatio} < 0
         */
        public void SetTruncateRatio(float truncateRatio)
        {
            if (truncateRatio < 0f)
                throw new ArgumentException("truncateRatio < 0");

            _truncateRatio = truncateRatio;
        }

    }
}