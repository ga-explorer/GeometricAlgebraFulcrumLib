using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.LinearMaps.Rotors;

public static class RGaRotorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEuclideanRotor<T>(this RGaMultivector<T> mv)
    {
        return mv.IsEven() && (mv.EGp(mv.Reverse()) - 1d).IsZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaPureRotor<T> ComplexEigenPairToPureRotor<T>(this RGaProcessor<T> processor, T realValue, T imagValue, RGaVector<T> realVector, RGaVector<T> imagVector)
    {
        //var scalar = scalarProcessor.Add(
        //    scalarProcessor.Times(realValue, realValue),
        //    scalarProcessor.Times(imagValue, imagValue)
        //);

        var angle = processor.ScalarProcessor.ArcTan2(
            realValue,
            imagValue
        );

        return realVector.Op(imagVector).CreatePureRotor(angle);

        //Console.WriteLine($"Eigen value real part: {realValue.GetLaTeXDisplayEquation()}");
        //Console.WriteLine();

        //Console.WriteLine($"Eigen value imag part: {imagValue.GetLaTeXDisplayEquation()}");
        //Console.WriteLine();

        //Console.WriteLine($"Eigen value length: {scalar.GetLaTeXDisplayEquation()}");
        //Console.WriteLine();

        //Console.WriteLine($"Eigen value angle: {angle.GetLaTeXDisplayEquation()}");
        //Console.WriteLine();

        //Console.WriteLine("Eigen vector real part:");
        //Console.WriteLine(realVector.TermsToLaTeX().GetLaTeXDisplayEquation());
        //Console.WriteLine();

        //Console.WriteLine("Eigen vector imag part:");
        //Console.WriteLine(imagVector.TermsToLaTeX().GetLaTeXDisplayEquation());
        //Console.WriteLine();

        //Console.WriteLine("Blade:");
        //Console.WriteLine(blade.ToLaTeXEquationsArray("B", @"\mu"));
        //Console.WriteLine();

        //Console.WriteLine("Final rotor:");
        //Console.WriteLine(rotor.ToLaTeXEquationsArray("R", @"\mu"));
        //Console.WriteLine();

        //Console.WriteLine($"Is simple rotor? {rotor.IsSimpleRotor()}");
        //Console.WriteLine();

        //Console.WriteLine();

        //return rotor;
    }
}