using System;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;
using GeometricAlgebraFulcrumLib.Processors;

namespace GeometricAlgebraFulcrumLib.Samples.MetaProgramming
{
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
                context.CreateGeometricAlgebraEuclideanProcessor(63);

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
                processor.CreatePureRotor(u, v);
            
            rotor.SetIsOutput(true);

            rotor.GetMultivectorStorage().SetExternalNamesByTermGradeIndex(
                (grade, index) => $"C[{grade}][{index}]"
            );

            //Define external names for parameters
            v.SetExternalNamesByTermIndex(index => $"v[{index}]");
            u.SetExternalNamesByTermIndex(index => $"u[{index}]");
            
            //Define external names for outputs
            rotor.SetExternalNamesByTermId(id => $"rotor.Scalar{id}");

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