using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.Signals.Interpolators.SavitzkyGolay;
/*
* Copyright [2009] [Marcin Rzeźnicki]

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

//https://github.com/DCHartlen/SavitzkyGolayFilter
// Savitzky-Golay Filter
//
// Written By: Devon C. Hartlen, University of Waterloo, Jan 2022
//
// A smoothing methodology published by Savitzky and Golay (1964). This
// method fits a polynomial of order k to a window of n points around the 
// location of interest. For a 0-order polynomial, this filter is equivalent
// to a standard moving average filter. Additionally, the S-G filter can
// return the derivative over the same window of n points, up to a
// derivative order of k. 
//
// This implimentation of the S-G filter is does not crop or phase shift
// data. This is accomplished by special handling of beginning and end
// points. Performance drops off rapidly beyond fifth order polynomials. 
//
// INPUTS:
//   yy: 1D vector of points to be filtered. 
//   k: Order of the polynomial to be fit. Polynomial order must be less
//        than n-1, but should be much much less than n-1. A 0-order
//        polynomial is a standard moving average/boxcar filter. 
//   n: Number of points to average over (also called window size). Must be
//        an odd number
//   s: derivative order. If 0, the derivative is not computed. s cannot
//        exceed k. Requires dx to be defined to return unnormalized
//        values
//   dx: (optional). The physical spacing of points in the yy array.
//        Required for differentiation. If not provided, the normalized 
//        derivative is return and must be subsequently scaled by (dx^s)^-1  
//
// OUTPUT
//   yyFilt: Data smoothed or differentiated by the S-G filter

//function yyFilt = SGFilter(yy, k, n , s, varargin)
//public static double SGFilter(IReadOnlyList<double> yy, int k, int n, int s, double dx)
//{

//}
/*
* Copyright [2009] [Marcin Rzeźnicki]

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
/*
* Copyright [2009] [Marcin Rzeźnicki]

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
/*
* Copyright [2009] [Marcin Rzeźnicki]

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
/*
* Copyright [2009] [Marcin Rzeźnicki]

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
/*
* Copyright [2009] [Marcin Rzeźnicki]

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
/*
* Copyright [2009] [Marcin Rzeźnicki]

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

/**
 * Savitzky-Golay filter implementation. For more information see
 * http://www.nrbook.com/a/bookcpdf/c14-8.pdf. This implementation,
 * however, does not use FFT
 *
 * @author Marcin Rzeźnicki
 *
 */
public class SgFilter
{

    /**
     * Computes Savitzky-Golay coefficients for given parameters
     *
     * @param nl
     *            numer of past data points filter will use
     * @param nr
     *            number of future data points filter will use
     * @param degree
     *            order of smoothin polynomial
     * @return Savitzky-Golay coefficients
     * @throws IllegalArgumentException
     *             if {@code nl < 0} or {@code nr < 0} or {@code nl + nr <
     *             degree}
     */
    public static double[] ComputeSgCoefficients(int nl, int nr, int degree)
    {
        if (nl < 0 || nr < 0 || nl + nr < degree)
            throw new ArgumentException("Bad arguments");
        var matrix = new DenseMatrix(degree + 1, degree + 1);

        double sum;
        for (var i = 0; i <= degree; i++)
        {
            for (var j = 0; j <= degree; j++)
            {
                sum = i == 0 && j == 0 ? 1 : 0;
                for (var k = 1; k <= nr; k++)
                    sum += Math.Pow(k, i + j);
                for (var k = 1; k <= nl; k++)
                    sum += Math.Pow(-k, i + j);
                matrix[i, j] = sum;
            }
        }
        Matrix<double> b = new DenseMatrix(degree + 1, 1);
        b[0, 0] = 1;
        b = matrix.Solve((Matrix<double>)b);
        var coeffs = new double[nl + nr + 1];
        for (var n = -nl; n <= nr; n++)
        {
            sum = b[0, 0];
            for (var m = 1; m <= degree; m++)
                sum += b[m, 0] * Math.Pow(n, m);
            coeffs[n + nl] = sum;
        }
        return coeffs;
    }

    private static void ConvertDoubleArrayToFloat(double[] inputArray, float[] outputArray)
    {
        for (var i = 0; i < inputArray.Length; i++)
            outputArray[i] = (float)inputArray[i];
    }

    private static void ConvertFloatArrayToDouble(float[] inputArray, double[] outputArray)
    {
        for (var i = 0; i < inputArray.Length; i++)
            outputArray[i] = inputArray[i];
    }

    private readonly List<ISgDataFilter> _dataFilters = new List<ISgDataFilter>();

    private int _nl;

    private int _nr;

    private readonly List<ISgPreprocessor> _preprocessors = new List<ISgPreprocessor>();

    /**
     * Constructs Savitzky-Golay filter which uses specified numebr of
     * surrounding data points
     *
     * @param nl
     *            numer of past data points filter will use
     * @param nr
     *            numer of future data points filter will use
     * @throws IllegalArgumentException
     *             of {@code nl < 0} or {@code nr < 0}
     */
    public SgFilter(int nl, int nr)
    {
        if (nl < 0 || nr < 0)
            throw new ArgumentException("Bad arguments");
        _nl = nl;
        _nr = nr;
    }

    /**
     * Appends data filter
     *
     * @param dataFilter
     *            dataFilter
     * @see DataFilter
     */
    public void AppendDataFilter(ISgDataFilter dataFilter)
    {
        _dataFilters.Add(dataFilter);
    }

    /**
     * Appends data preprocessor
     *
     * @param p
     *            preprocessor
     * @see Preprocessor
     */
    public void AppendPreprocessor(ISgPreprocessor p)
    {
        _preprocessors.Add(p);
    }

    /**
     *
     * @return number of past data points that this filter uses
     */
    public int GetNl()
    {
        return _nl;
    }

    /**
     *
     * @return number of future data points that this filter uses
     */
    public int GetNr()
    {
        return _nr;
    }

    /**
     * Inserts data filter
     *
     * @param dataFilter
     *            data filter
     * @param index
     *            where it should be placed in data filters queue
     * @see DataFilter
     */
    public void InsertDataFilter(ISgDataFilter dataFilter, int index)
    {
        _dataFilters.Insert(index, dataFilter);
    }

    /**
     * Inserts preprocessor
     *
     * @param p
     *            preprocessor
     * @param index
     *            where it should be placed in preprocessors queue
     * @see Preprocessor
     */
    public void InsertPreprocessor(ISgPreprocessor p, int index)
    {
        _preprocessors.Insert(index, p);
    }

    /**
     * Removes data filter
     *
     * @param dataFilter
     *            data filter to be removed
     * @return {@code true} if data filter existed and was removed, {@code
     *         false} otherwise
     */
    public bool RemoveDataFilter(ISgDataFilter dataFilter)
    {
        return _dataFilters.Remove(dataFilter);
    }

    /**
     * Removes data filter
     *
     * @param index
     *            which data filter to remove
     * @return removed data filter
     */
    public ISgDataFilter RemoveDataFilter(int index)
    {
        var dataFilter = _dataFilters[index];

        _dataFilters.RemoveAt(index);

        return dataFilter;
    }

    /**
     * Removes preprocessor
     *
     * @param index
     *            which preprocessor to remove
     * @return removed preprocessor
     */
    public ISgPreprocessor RemovePreprocessor(int index)
    {
        var preprocessor = _preprocessors[index];

        _preprocessors.RemoveAt(index);

        return preprocessor;
    }

    /**
     * Removes preprocessor
     *
     * @param p
     *            preprocessor to be removed
     * @return {@code true} if preprocessor existed and was removed, {@code
     *         false} otherwise
     */
    public bool RemovePreprocessor(ISgPreprocessor p)
    {
        return _preprocessors.Remove(p);
    }

    /**
     * Sets number of past data points for this filter
     *
     * @param nl
     *            number of past data points
     * @throws IllegalArgumentException
     *             if {@code nl < 0}
     */
    public void SetNl(int nl)
    {
        if (nl < 0)
            throw new ArgumentException("nl < 0");
        _nl = nl;
    }

    /**
     * Sets number of future data points for this filter
     *
     * @param nr
     *            number of future data points
     * @throws IllegalArgumentException
     *             if {@code nr < 0}
     */
    public void SetNr(int nr)
    {
        if (nr < 0)
            throw new ArgumentException("nr < 0");
        _nr = nr;
    }

    /**
     * Smooths data by using Savitzky-Golay filter. This method will use 0 for
     * any element beyond {@code data} which will be needed for computation (you
     * may want to use some {@link Preprocessor})
     *
     * @param data
     *            data for filter
     * @param coeffs
     *            filter coefficients
     * @return filtered data
     * @throws NullPointerException
     *             when any array passed as parameter is null
     */
    public double[] Smooth(double[] data, double[] coeffs)
    {
        return Smooth(data, 0, data.Length, coeffs);
    }

    /**
     * Smooths data by using Savitzky-Golay filter. Smoothing uses {@code
     * leftPad} and/or {@code rightPad} if you want to augment data on
     * boundaries to achieve smoother results for your purpose. If you do not
     * need this feature you may pass empty arrays (filter will use 0s in this
     * place, so you may want to use appropriate preprocessor)
     *
     * @param data
     *            data for filter
     * @param leftPad
     *            left padding
     * @param rightPad
     *            right padding
     * @param coeffs
     *            filter coefficients
     * @return filtered data
     * @throws NullPointerException
     *             when any array passed as parameter is null
     */
    public double[] Smooth(double[] data, double[] leftPad, double[] rightPad,
        double[] coeffs)
    {
        return Smooth(data, leftPad, rightPad, 0, new double[][] { coeffs });
    }

    /**
     * Smooths data by using Savitzky-Golay filter. Smoothing uses {@code
     * leftPad} and/or {@code rightPad} if you want to augment data on
     * boundaries to achieve smoother results for your purpose. If you do not
     * need this feature you may pass empty arrays (filter will use 0s in this
     * place, so you may want to use appropriate preprocessor). If you want to
     * use different (probably non-symmetrical) filter near both ends of
     * (padded) data, you will be using {@code bias} and {@code coeffs}. {@code
     * bias} essentially means
     * "how many points of pad should be left out when smoothing". Filters
     * taking this condition into consideration are passed in {@code coeffs}.
     * <tt>coeffs[0]</tt> is used for unbiased data (that is, for
     * <tt>data[bias]..data[data.Length-bias-1]</tt>). Its length has to be
     * <tt>nr + nl + 1</tt>. Filters from range
     * <tt>coeffs[coeffs.Length - 1]</tt> to
     * <tt>coeffs[coeffs.Length - bias]</tt> are used for smoothing first
     * {@code bias} points (that is, from <tt>data[0]</tt> to
     * <tt>data[bias]</tt>) correspondingly. Filters from range
     * <tt>coeffs[1]</tt> to <tt>coeffs[bias]</tt> are used for smoothing last
     * {@code bias} points (that is, for
     * <tt>data[data.Length-bias]..data[data.Length-1]</tt>). For example, if
     * you use 5 past points and 5 future points for smoothing, but have only 3
     * meaningful padding points - you would use {@code bias} equal to 2 and
     * would pass in {@code coeffs} param filters taking 5-5 points (for regular
     * smoothing), 5-4, 5-3 (for rightmost range of data) and 3-5, 4-5 (for
     * leftmost range). If you do not wish to use pads completely for
     * symmetrical filter then you should pass <tt>bias = nl = nr</tt>
     *
     * @param data
     *            data for filter
     * @param leftPad
     *            left padding
     * @param rightPad
     *            right padding
     * @param bias
     *            how many points of pad should be left out when smoothing
     * @param coeffs
     *            array of filter coefficients
     * @return filtered data
     * @throws IllegalArgumentException
     *             when <tt>bias < 0</tt> or <tt>bias > min(nr, nl)</tt>
     * @throws IndexOutOfBoundsException
     *             when {@code coeffs} has less than <tt>2*bias + 1</tt>
     *             elements
     * @throws NullPointerException
     *             when any array passed as parameter is null
     */
    public double[] Smooth(double[] data, double[] leftPad, double[] rightPad,
        int bias, double[][] coeffs)
    {
        if (bias < 0 || bias > _nr || bias > _nl)
            throw new ArgumentException(
                "bias < 0 or bias > nr or bias > nl");
        foreach (var dataFilter in _dataFilters)
        {
            data = dataFilter.Filter(data);
        }
        var dataLength = data.Length;
        if (dataLength == 0)
            return data;
        var n = dataLength + _nl + _nr;
        var dataCopy = new double[n];
        // copy left pad reversed
        var leftPadOffset = _nl - leftPad.Length;
        if (leftPadOffset >= 0)
            for (var i = 0; i < leftPad.Length; i++)
            {
                dataCopy[leftPadOffset + i] = leftPad[i];
            }
        else
            for (var i = 0; i < _nl; i++)
            {
                dataCopy[i] = leftPad[i - leftPadOffset];
            }
        // copy actual data
        for (var i = 0; i < dataLength; i++)
        {
            dataCopy[i + _nl] = data[i];
        }
        // copy right pad
        var rightPadOffset = _nr - rightPad.Length;
        if (rightPadOffset >= 0)
            for (var i = 0; i < rightPad.Length; i++)
            {
                dataCopy[i + dataLength + _nl] = rightPad[i];
            }
        else
            for (var i = 0; i < _nr; i++)
            {
                dataCopy[i + dataLength + _nl] = rightPad[i];
            }
        foreach (var p in _preprocessors)
        {
            p.Apply(dataCopy);
        }
        // convolution (with savitzky-golay coefficients)
        var sdata = new double[dataLength];
        double[] sg;
        for (var b = bias; b > 0; b--)
        {
            sg = coeffs[coeffs.Length - b];
            var x = _nl + bias - b;
            double sum = 0;
            for (var i = -_nl + b; i <= _nr; i++)
            {
                sum += dataCopy[x + i] * sg[_nl - b + i];
            }
            sdata[x - _nl] = sum;
        }
        sg = coeffs[0];
        for (var x = _nl + bias; x < n - _nr - bias; x++)
        {
            double sum = 0;
            for (var i = -_nl; i <= _nr; i++)
            {
                sum += dataCopy[x + i] * sg[_nl + i];
            }
            sdata[x - _nl] = sum;
        }
        for (var b = 1; b <= bias; b++)
        {
            sg = coeffs[b];
            var x = n - _nr - bias + (b - 1);
            double sum = 0;
            for (var i = -_nl; i <= _nr - b; i++)
            {
                sum += dataCopy[x + i] * sg[_nl + i];
            }
            sdata[x - _nl] = sum;
        }
        return sdata;
    }

    /**
     * Runs filter on data from {@code from} (including) to {@code to}
     * (excluding). Data beyond range spanned by {@code from} and {@code to}
     * will be used for padding
     *
     * @param data
     *            data for filter
     * @param from
     *            inedx of the first element of data
     * @param to
     *            index of the first element omitted
     * @param coeffs
     *            filter coefficients
     * @return filtered data
     * @throws ArrayIndexOutOfBoundsException
     *             if <tt>to > data.Length</tt>
     * @throws IllegalArgumentException
     *             if <tt>from < 0</tt> or <tt>to > data.Length</tt>
     * @throws NullPointerException
     *             if {@code data} is null or {@code coeffs} is null
     */
    public double[] Smooth(double[] data, int from, int to, double[] coeffs)
    {
        return Smooth(data, from, to, 0, new double[][] { coeffs });
    }

    /**
     * Runs filter on data from {@code from} (including) to {@code to}
     * (excluding). Data beyond range spanned by {@code from} and {@code to}
     * will be used for padding. See
     * {@link #smooth(double[], double[], double[], int, double[][])} for usage
     * of {@code bias}
     *
     * @param data
     *            data for filter
     * @param from
     *            inedx of the first element of data
     * @param to
     *            index of the first element omitted
     * @param bias
     *            how many points of pad should be left out when smoothing
     * @param coeffs
     *            filter coefficients
     * @return filtered data
     * @throws ArrayIndexOutOfBoundsException
     *             if <tt>to > data.Length</tt> or when {@code coeffs} has less
     *             than <tt>2*bias + 1</tt> elements
     * @throws IllegalArgumentException
     *             if <tt>from < 0</tt> or <tt>to > data.Length</tt> or
     *             <tt>from > to</tt> or when <tt>bias < 0</tt> or
     *             <tt>bias > min(nr, nl)</tt>
     * @throws NullPointerException
     *             if {@code data} is null or {@code coeffs} is null
     */
    public double[] Smooth(double[] data, int from, int to, int bias, double[][] coeffs)
    {
        var leftPad = data.CopyOfRange(0, from);
        var rightPad = data.CopyOfRange(to, data.Length);
        var dataCopy = data.CopyOfRange(from, to);

        return Smooth(dataCopy, leftPad, rightPad, bias, coeffs);
    }

    /**
     * See {@link #smooth(double[], double[])}. This method converts {@code
     * data} to double for computation and then converts it back to float
     *
     * @param data
     *            data for filter
     * @param coeffs
     *            filter coefficients
     * @return filtered data
     * @throws NullPointerException
     *             when any array passed as parameter is null
     */
    public float[] Smooth(float[] data, double[] coeffs)
    {
        return Smooth(data, 0, data.Length, coeffs);
    }

    /**
     * See {@link #smooth(double[], double[], double[], double[])}. This method
     * converts {@code data} {@code leftPad} and {@code rightPad} to double for
     * computation and then converts back to float
     *
     * @param data
     *            data for filter
     * @param leftPad
     *            left padding
     * @param rightPad
     *            right padding
     * @param coeffs
     *            filter coefficients
     * @return filtered data
     * @throws NullPointerException
     *             when any array passed as parameter is null
     */
    public float[] Smooth(float[] data, float[] leftPad, float[] rightPad,
        double[] coeffs)
    {
        return Smooth(data, leftPad, rightPad, 0, new double[][] { coeffs });
    }

    /**
     * See {@link #smooth(double[], double[], double[], int, double[][])}. This
     * method converts {@code data} {@code leftPad} and {@code rightPad} to
     * double for computation and then converts back to float
     *
     * @param data
     *            data for filter
     * @param leftPad
     *            left padding
     * @param rightPad
     *            right padding
     * @param bias
     *            how many points of pad should be left out when smoothing
     * @param coeffs
     *            array of filter coefficients
     * @return filtered data
     * @throws IllegalArgumentException
     *             when <tt>bias < 0</tt> or <tt>bias > min(nr, nl)</tt>
     * @throws IndexOutOfBoundsException
     *             when {@code coeffs} has less than <tt>2*bias + 1</tt>
     *             elements
     * @throws NullPointerException
     *             when any array passed as parameter is null
     */
    public float[] Smooth(float[] data, float[] leftPad, float[] rightPad, int bias, double[][] coeffs)
    {
        var dataAsDouble = new double[data.Length];
        var leftPadAsDouble = new double[leftPad.Length];
        var rightPadAsDouble = new double[rightPad.Length];

        ConvertFloatArrayToDouble(data, dataAsDouble);
        ConvertFloatArrayToDouble(leftPad, leftPadAsDouble);
        ConvertFloatArrayToDouble(rightPad, rightPadAsDouble);

        var results = Smooth(dataAsDouble, leftPadAsDouble, rightPadAsDouble, bias, coeffs);
        var resultsAsFloat = new float[results.Length];

        ConvertDoubleArrayToFloat(results, resultsAsFloat);
            
        return resultsAsFloat;
    }

    /**
     * See {@link #smooth(double[], int, int, double[])}. This method converts
     * {@code data} to double for computation and then converts it back to float
     *
     * @param data
     *            data for filter
     * @param from
     *            inedx of the first element of data
     * @param to
     *            index of the first element omitted
     * @param coeffs
     *            filter coefficients
     * @return filtered data
     * @throws ArrayIndexOutOfBoundsException
     *             if <tt>to > data.Length</tt>
     * @throws IllegalArgumentException
     *             if <tt>from < 0</tt> or <tt>to > data.Length</tt>
     * @throws NullPointerException
     *             if {@code data} is null or {@code coeffs} is null
     */
    public float[] Smooth(float[] data, int from, int to, double[] coeffs)
    {
        return Smooth(data, from, to, 0, new double[][] { coeffs });
    }

    /**
     * See {@link #smooth(double[], int, int, int, double[][])}. This method
     * converts {@code data} to double for computation and then converts it back
     * to float
     *
     * @param data
     *            data for filter
     * @param from
     *            inedx of the first element of data
     * @param to
     *            index of the first element omitted
     * @param bias
     *            how many points of pad should be left out when smoothing
     * @param coeffs
     *            filter coefficients
     * @return filtered data
     * @throws ArrayIndexOutOfBoundsException
     *             if <tt>to > data.Length</tt> or when {@code coeffs} has less
     *             than <tt>2*bias + 1</tt> elements
     * @throws IllegalArgumentException
     *             if <tt>from < 0</tt> or <tt>to > data.Length</tt> or
     *             <tt>from > to</tt> or when <tt>bias < 0</tt> or
     *             <tt>bias > min(nr, nl)</tt>
     * @throws NullPointerException
     *             if {@code data} is null or {@code coeffs} is null
     */
    public float[] Smooth(float[] data, int from, int to, int bias, double[][] coeffs)
    {
        //Array.co
        var leftPad = data.CopyOfRange(0, from);
        var rightPad = data.CopyOfRange(to, data.Length);
        var dataCopy = data.CopyOfRange(from, to);

        return Smooth(dataCopy, leftPad, rightPad, bias, coeffs);
    }
}