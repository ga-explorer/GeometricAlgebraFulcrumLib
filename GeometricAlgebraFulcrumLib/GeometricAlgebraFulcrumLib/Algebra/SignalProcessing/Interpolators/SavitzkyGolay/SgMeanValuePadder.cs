using System;

namespace GeometricAlgebraFulcrumLib.Algebra.SignalProcessing.Interpolators.SavitzkyGolay
{
    /**
     * Pads data to left and/or right.:
     * 
     * <p>
     * <ul>
     * <li>
     * Let <tt>l</tt> be the index of the first non-zero element in data (for left
     * padding),</li>
     * <li>let <tt>r</tt> be the index of the last non-zero element in data (for
     * right padding)</li>
     * </ul>
     * then for every element <tt>e</tt> which index is <tt>i</tt> such that:
     * <ul>
     * <li>
     * <tt>0 <= i < l</tt>, <tt>e</tt> is replaced with arithmetic mean of
     * <tt>data[l]..data[l + window_length/2 - 1]</tt> (left padding)</li>
     * <li>
     * <tt>r < i < data.Length</tt>, <tt>e</tt> is replaced with arithmetic mean of
     * <tt>data[r - window_length/2 + 1]..data[r]</tt> (right padding)</li>
     * </ul>
     * </p>
     * Example:
     * <p>
     * Given data: <tt>[0,0,0,1,2,1,3,1,2,4,0]</tt> result of applying
     * MeanValuePadder with {@link #getWindowLength() window_length} = 4 is:
     * <tt>[1.5,1.5,1.5,1,2,1,3,1,2,4,0]</tt> in case of {@link #isPaddingLeft()
     * left padding}; <tt>[0,0,0,1,2,1,3,1,2,4,3]</tt> in case of
     * {@link #isPaddingRight() right padding};
     * </p>
     * 
     * @author Marcin Rzeźnicki
     * 
     */
    public class SgMeanValuePadder : ISgPreprocessor
    {

        private bool _paddingLeft = true;

        private bool _paddingRight = true;

        private int _windowLength;

        /**
         * 
         * @param windowLength
         *            window length of filter which will be used to smooth data.
         *            Padding will use half of {@code windowLength} length. In this
         *            way padding will be suited to smoothing operation
         * @throws IllegalArgumentException
         *             if {@code windowLength} < 0
         */
        public SgMeanValuePadder(int windowLength)
        {
            if (windowLength < 0)
                throw new ArgumentException("windowLength < 0");
            _windowLength = windowLength;
        }

        /**
         * 
         * @param windowLength
         *            window length of filter which will be used to smooth data.
         *            Padding will use half of {@code windowLength} length. In this
         *            way padding will be suited to smoothing operation
         * @param paddingLeft
         *            enables or disables left padding
         * @param paddingRight
         *            enables or disables left padding
         * @throws IllegalArgumentException
         *             if {@code windowLength} < 0
         */
        public SgMeanValuePadder(int windowLength, bool paddingLeft,
            bool paddingRight)
        {
            if (windowLength < 0)
                throw new ArgumentException("windowLength < 0");
            _windowLength = windowLength;
            _paddingLeft = paddingLeft;
            _paddingRight = paddingRight;
        }


        public void Apply(double[] data)
        {
            // padding values with average of last (WINDOW_LENGTH / 2) points
            var n = data.Length;
            if (_paddingLeft)
            {
                var l = 0;
                // seek first non-zero cell
                for (var i = 0; i < n; i++)
                {
                    if (data[i] != 0)
                    {
                        l = i;
                        break;
                    }
                }
                double avg = 0;
                var m = Math.Min(l + _windowLength / 2, n);
                for (var i = l; i < m; i++)
                {
                    avg += data[i];
                }
                avg /= (m - l);
                for (var i = 0; i < l; i++)
                {
                    data[i] = avg;
                }
            }
            if (_paddingRight)
            {
                var r = 0;
                // seek last non-zero cell
                for (var i = n - 1; i >= 0; i--)
                {
                    if (data[i] != 0)
                    {
                        r = i;
                        break;
                    }
                }
                double avg = 0;
                var m = Math.Min(_windowLength / 2, r + 1);
                for (var i = 0; i < m; i++)
                {
                    avg += data[r - i];
                }
                avg /= m;
                for (var i = r + 1; i < n; i++)
                {
                    data[i] = avg;
                }
            }
        }

        /**
         * 
         * @return {@code windowLength}
         */
        public int GetWindowLength()
        {
            return _windowLength;
        }

        /**
         * 
         * @return {@code paddingLeft}
         */
        public bool IsPaddingLeft()
        {
            return _paddingLeft;
        }

        /**
         * 
         * @return {@code paddingRight}
         */
        public bool IsPaddingRight()
        {
            return _paddingRight;
        }

        /**
         * 
         * @param paddingLeft
         *            enables or disables left padding
         */
        public void SetPaddingLeft(bool paddingLeft)
        {
            _paddingLeft = paddingLeft;
        }

        /**
         * 
         * @param paddingRight
         *            enables or disables right padding
         */
        public void SetPaddingRight(bool paddingRight)
        {
            _paddingRight = paddingRight;
        }

        /**
         * 
         * @param windowLength
         * @throws IllegalArgumentException
         *             if {@code windowLength} < 0
         */
        public void SetWindowLength(int windowLength)
        {
            if (windowLength < 0)
                throw new ArgumentException("windowLength < 0");
            _windowLength = windowLength;
        }

    }
}