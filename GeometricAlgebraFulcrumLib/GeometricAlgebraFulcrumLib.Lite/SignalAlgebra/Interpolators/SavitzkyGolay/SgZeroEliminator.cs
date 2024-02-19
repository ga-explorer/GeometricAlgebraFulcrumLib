namespace GeometricAlgebraFulcrumLib.Lite.SignalAlgebra.Interpolators.SavitzkyGolay;

/**
 * Eliminates zeros from data - starting from the first non-zero element, ending
 * at the last non-zero element. More specifically:
 * <p>
 * <ul>
 * <li>
 * Let <tt>l</tt> be the index of the first non-zero element in data,</li>
 * <li>let <tt>r</tt> be the index of the last non-zero element in data</li>
 * </ul>
 * then for every element <tt>e</tt> which index is <tt>i</tt> such that:
 * <tt>l < i < r</tt> and <tt>e == 0</tt>, <tt>e</tt> is replaced with element <tt>e'</tt>
 * with index <tt>j</tt> such that:
 * <ul>
 * <li><tt>l <= j < i</tt> and <tt>e' <> 0</tt> and for all indexes
 * <tt>k: j < k < i; e[k] == 0</tt> - when {@link #isAlignToLeft() alignToLeft}
 * is true</li>
 * <li><tt>i < j <= r</tt> and <tt>e' <> 0</tt> and for all indexes
 * <tt>k: i < k < j;e[k] == 0</tt> - otherwise</li>
 * </ul>
 * </p>
 * Example:
 * <p>
 * Given data: <tt>[0,0,0,1,2,0,3,0,0,4,0]</tt> result of applying
 * ZeroEliminator is: <tt>[0,0,0,1,2,2,3,3,3,4,0]</tt> if
 * {@link #isAlignToLeft() alignToLeft} is true;
 * <tt>[0,0,0,1,2,3,3,4,4,4,0]</tt> - otherwise
 * </p>
 *
 * @author Marcin Rzeźnicki
 *
 */
public class SgZeroEliminator : ISgPreprocessor
{

    private bool _alignToLeft;

    /**
     * Default constructor: {@code alignToLeft} is {@code false}
     *
     * @see #ZeroEliminator(boolean)
     */
    public SgZeroEliminator()
    {

    }

    /**
     *
     * @param alignToLeft
     *            if {@code true} zeros will be replaced with non-zero element
     *            to the left, if {@code false} - to the right
     */
    public SgZeroEliminator(bool alignToLeft)
    {
        _alignToLeft = alignToLeft;
    }

    public void Apply(double[] data)
    {
        var n = data.Length;
        int l = 0, r = 0;
        // seek first non-zero cell
        for (var i = 0; i < n; i++)
        {
            if (data[i] != 0)
            {
                l = i;
                break;
            }
        }
        // seek last non-zero cell
        for (var i = n - 1; i >= 0; i--)
        {
            if (data[i] != 0)
            {
                r = i;
                break;
            }
        }
        // eliminate 0s
        if (_alignToLeft)
            for (var i = l + 1; i < r; i++)
            {
                if (data[i] == 0)
                {
                    data[i] = data[i - 1];
                }
            }
        else
            for (var i = r - 1; i > l; i--)
            {
                if (data[i] == 0)
                {
                    data[i] = data[i + 1];
                }
            }
    }

    /**
     *
     * @return {@code alignToLeft}
     */
    public bool IsAlignToLeft()
    {
        return _alignToLeft;
    }

    /**
     *
     * @param alignToLeft
     *            if {@code true} zeros will be replaced with non-zero element
     *            to the left, if {@code false} - to the right
     */
    public void SetAlignToLeft(bool alignToLeft)
    {
        _alignToLeft = alignToLeft;
    }

}