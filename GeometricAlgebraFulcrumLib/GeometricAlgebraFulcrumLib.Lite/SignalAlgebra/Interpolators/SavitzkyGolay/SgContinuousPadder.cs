﻿namespace GeometricAlgebraFulcrumLib.Lite.SignalAlgebra.Interpolators.SavitzkyGolay;

/**
 * Pads data to left and/or right, starting from the first (last) non-zero cell
 * and extending it to the beginning (end) of data. More specifically:
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
 * <tt>0 <= i < l</tt>, <tt>e</tt> is replaced with element <tt>data[l]</tt>
 * (left padding)</li>
 * <li>
 * <tt>r < i < data.Length</tt>, <tt>e</tt> is replaced with element
 * <tt>data[r]</tt> (right padding)</li>
 * </ul>
 * </p>
 * Example:
 * <p>
 * Given data: <tt>[0,0,0,1,2,1,3,1,2,4,0]</tt> result of applying
 * ContinuousPadder is: <tt>[1,1,1,1,2,1,3,1,2,4,0]</tt> in case of
 * {@link #isPaddingLeft() left padding}; <tt>[0,0,0,1,2,1,3,1,2,4,4]</tt> in
 * case of {@link #isPaddingRight() right padding};
 * </p>
 *
 * @author Marcin Rzeźnicki
 *
 */
public class SgContinuousPadder : ISgPreprocessor
{

    private bool _paddingLeft = true;

    private bool _paddingRight = true;

    /**
     * Default construcot. Both left and right padding are turned on
     */
    public SgContinuousPadder()
    {

    }

    /**
     *
     * @param paddingLeft
     *            enables or disables left padding
     * @param paddingRight
     *            enables or disables right padding
     */
    public SgContinuousPadder(bool paddingLeft, bool paddingRight)
    {
        _paddingLeft = paddingLeft;
        _paddingRight = paddingRight;
    }

    public void Apply(double[] data)
    {
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
            var y0 = data[l];
            for (var i = 0; i < l; i++)
            {
                data[i] = y0;
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
            var ynr = data[r];
            for (var i = r + 1; i < n; i++)
            {
                data[i] = ynr;
            }
        }
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

}