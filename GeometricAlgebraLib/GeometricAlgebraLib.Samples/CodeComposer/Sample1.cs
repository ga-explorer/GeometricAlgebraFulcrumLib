using System;
using GeometricAlgebraLib.Processing.SymbolicExpressions;
using GeometricAlgebraLib.Processing.SymbolicExpressions.Context;

namespace GeometricAlgebraLib.Samples.CodeComposer
{
    public static class Sample1
    {
        public static void Execute()
        {
            var context = 
                new SymbolicContext()
                {
                    MergeExpressions = false
                };

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
                context.ComputedVariablesFactory.CreateEuclideanSimpleRotor(
                    u, 
                    v
                );
            
            rotor.Storage.SetIsOutput(true);

            rotor.Storage.SetExternalNamesByTermGradeIndex(
                (grade, index) => $"C[{grade}][{index}]"
            );

            //Define external names for parameters
            v.SetExternalNamesByTermIndex(index => $"v[{index}]");
            u.SetExternalNamesByTermIndex(index => $"u[{index}]");
            
            //Define external names for outputs
            rotor.Storage.SetExternalNamesByTermId(id => $"rotor.Scalar{id}");

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
}
