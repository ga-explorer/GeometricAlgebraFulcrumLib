using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Matrices;

public static class Float64ScalarArrayUtils
{
    //
    //public static double GetSum(this IEnumerable<double> vector)
    //{
    //    return vector.GetSum();
    //}

    //
    //public static double GetMin(this IEnumerable<double> vector)
    //{
    //    return vector.GetMin();
    //}

    //
    //public static double GetMax(this IEnumerable<double> vector)
    //{
    //    return vector.GetMax();
    //}
    
    //
    //public static Tuple<double, double> GetMinMax(this IEnumerable<double> vector)
    //{
    //    var (min, max) = 
    //        vector.GetMinMax();

    //    return new Tuple<double, double>(min, max);
    //}
    
    
    public static Float64ScalarRange GetMinMaxRange(this IEnumerable<double> vector)
    {
        return Float64ScalarRange.Create(vector);
    }

}