using System;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;

namespace GeometricAlgebraFulcrumLib.Samples.MetaProgramming;

public static class Sample3
{
    public static void Execute()
    {
        var context = 
            new MetaContext()
            {
                MergeExpressions = false
            };

        var processor = 
            context.CreateEuclideanXGaProcessor();

        var u =
            context.ParameterVariablesFactory.CreateVector(
                "u1",
                "u2",
                "u3"
            );

        var v =
            context.ParameterVariablesFactory.CreateVector(
                "v1",
                "v2",
                "v3"
            );

        var rotor = 
            u.CreatePureRotor(v);
            
        rotor.Multivector.SetIsOutput(true);

        rotor.Multivector.SetExternalNamesByTermGradeIndex(
            (grade, index) => $"C[{grade}][{index}]"
        );

        //Define external names for parameters
        v.SetExternalNamesByTermIndex(index => $"v[{index}]");
        u.SetExternalNamesByTermIndex(index => $"u[{index}]");
            
        //Define external names for outputs
        rotor.Multivector.SetExternalNamesByTermId(id => $"rotor.Scalar{id}");

        //Optimize sequence computations inside context
        context.ContextOptions.ReduceLowLevelRhsSubExpressions = true;

        context.OptimizeContext();

        //Define external names for intermediate variables
        context.SetIntermediateExternalNamesByNameIndex(index => $"temp{index}");

        Console.WriteLine("Context Computations:");
        Console.WriteLine(context.ToString());
        Console.WriteLine();
    }
}