using System;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;

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
            context.ParameterVariablesFactory.Vector(
                "u1",
                "u2",
                "u3"
            );

        var v =
            context.ParameterVariablesFactory.Vector(
                "v1",
                "v2",
                "v3"
            );

        var rotor = u.CreatePureRotor(v);
            
        //Define external names for parameters
        v.SetExternalNamesByTermIndex(index => $"v[{index}]");
        u.SetExternalNamesByTermIndex(index => $"u[{index}]");

        //Set outputs and define their external names
        rotor.Multivector.SetAsOutputByTermId(id => $"rotor.Scalar{id}");

        //rotor.Multivector.SetAsOutputByTermGradeIndex(
        //    (grade, index) => $"C[{grade}][{index}]"
        //);

        //Optimize sequence computations inside context
        context.ContextOptions.ReduceLowLevelRhsSubExpressions = true;

        context.OptimizeContext();

        //Define external names for intermediate variables
        context.SetComputedExternalNamesByOrder(index => $"temp{index}");

        Console.WriteLine("Context Computations:");
        Console.WriteLine(context.ToString());
        Console.WriteLine();
    }
}