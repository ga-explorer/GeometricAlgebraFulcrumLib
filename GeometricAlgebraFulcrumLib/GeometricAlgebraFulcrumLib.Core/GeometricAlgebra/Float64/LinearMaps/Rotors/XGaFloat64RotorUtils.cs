﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Angles;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.LinearMaps.Rotors;

public static class XGaFloat64RotorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEuclideanRotor(this XGaFloat64Multivector mv)
    {
        return mv.IsEven() && (mv.EGp(mv.Reverse()) - 1d).IsZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64PureRotor ComplexEigenPairToPureRotor(this XGaFloat64Processor processor, double realValue, double imagValue, XGaFloat64Vector realVector, XGaFloat64Vector imagVector)
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