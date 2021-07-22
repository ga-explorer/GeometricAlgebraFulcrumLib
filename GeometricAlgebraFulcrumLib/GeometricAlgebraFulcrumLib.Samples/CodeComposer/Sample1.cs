using System;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Processing.Products;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;

namespace GeometricAlgebraFulcrumLib.Samples.CodeComposer
{
    public static class Sample1
    {
        public static void Execute()
        {
            // The number of dimensions
            const int n = 3;

            // The context (a special kind of processor) for symbolic multivector
            // assignments
            var context = 
                new SymbolicContext()
                {
                    MergeExpressions = false
                };

            // Define the first vector with a given set of scalar components u1, u2, ...
            var u =
                context.ParameterVariablesFactory.CreateDenseVector(
                    n, 
                    index => $"u{index + 1}"
                );

            // Define the second vector with a given set of scalar components v1, v2, ...
            var v =
                context.ParameterVariablesFactory.CreateDenseVector(
                    n, 
                    index => $"v{index + 1}"
                );

            // Perform any required GA computations
            var bv = u.Op(v);

            // Define the final outputs for the computations for proper code generation
            bv.SetIsOutput(true);

            // Define code generated variable names for input parameters
            v.SetExternalNamesByTermId(id => $"v.Scalar{id.PatternToString(n)}");
            u.SetExternalNamesByTermId(id => $"u.Scalar{id.PatternToString(n)}");
            
            // Define code generated variable names for outputs
            bv.SetExternalNamesByTermId(id => $"bv.Scalar{id.PatternToString(n)}");

            // Optimize sequence computations inside context
            context.OptimizeContext();

            // Define code generated variable names for intermediate variables
            context.SetIntermediateExternalNamesByNameIndex(index => $"temp{index}");

            // Display an internal representation of the computations
            Console.WriteLine("Context Computations:");
            Console.WriteLine(context.ToString());
            Console.WriteLine();
        }
    }
}
