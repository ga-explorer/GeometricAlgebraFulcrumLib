﻿using GeometricAlgebraFulcrumLib.Mathematica.Algebra;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.PowerSystems.GAPoT;

public static class BasicOperationsSymbolicSample
{
    public static void Execute()
    {
        var v1 = XGaMathematicaUtils.Vector(
            "a1", "b1", "c1", "d1"
        );

        var v2 = XGaMathematicaUtils.Vector(
            "a2", "b2", "c2", "d2"
        );

        var bv = v1.Op(v2);

        Console.WriteLine($"v1 = {v1.GetText()}");
        Console.WriteLine();

        Console.WriteLine($"v1 = {v1.GetLaTeX()}");
        Console.WriteLine();

        Console.WriteLine($"v2 = {v2.GetText()}");
        Console.WriteLine();

        Console.WriteLine($"v2 = {v2.GetLaTeX()}");
        Console.WriteLine();

        Console.WriteLine($"v1 op v2 = {bv.GetText()}");
        Console.WriteLine();

        Console.WriteLine($"v1 op v2 = {bv.GetLaTeX()}");
        Console.WriteLine();
    }
}