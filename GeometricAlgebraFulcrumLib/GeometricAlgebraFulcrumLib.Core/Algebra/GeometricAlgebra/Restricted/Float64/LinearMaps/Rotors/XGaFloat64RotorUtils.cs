using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Rotors;

public static class RGaFloat64RotorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEuclideanRotor(this RGaFloat64Multivector mv)
    {
        return mv.IsEven() && (mv.EGp(mv.Reverse()) - 1d).IsZero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64PureRotor ComplexEigenPairToPureRotor(this RGaFloat64Processor processor, double realValue, double imagValue, RGaFloat64Vector realVector, RGaFloat64Vector imagVector)
    {
        //var scalar = scalarProcessor.Add(
        //    scalarProcessor.Times(realValue, realValue),
        //    scalarProcessor.Times(imagValue, imagValue)
        //);

        var angle = LinFloat64PolarAngle.CreateFromVector(realValue, imagValue);

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