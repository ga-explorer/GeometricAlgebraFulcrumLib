namespace DataStructuresLib.Extensions;

public static class EFloatExtensions
{
    ///// <summary>
    ///// Compute the integer power on a number using
    ///// exponentiation by squaring method
    ///// https://en.wikipedia.org/wiki/Exponentiation_by_squaring
    ///// </summary>
    ///// <param name="n"></param>
    ///// <param name="exp"></param>
    ///// <returns></returns>
    //public static EFloat Power(this EFloat n, int exp)
    //{
    //    if (exp < 0)
    //        return EFloat.One / n.Power(-exp);

    //    if (exp == 1) 
    //        return n;

    //    if (exp == 2)
    //        return n * n;
            
    //    var result = EFloat.One;

    //    while (exp > 0)
    //    {
    //        if ((exp & 1) != 0)
    //            result *= n;

    //        exp >>= 1;

    //        n *= n;
    //    }

    //    return result; 
    //}

}