using System;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.CodeComposer.Composers;
using GeometricAlgebraFulcrumLib.CodeComposer.Languages;
using GeometricAlgebraFulcrumLib.Geometry.Rotors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Symbolic;

namespace GeometricAlgebraFulcrumLib.Samples.CodeComposer
{
    public static class Sample1
    {
        public static void Execute()
        {
            // The number of dimensions
            const int n = 3;

            // Stage 1: Define the symbolic context
            // The symbolic context is a special kind of processor for symbolic multivector
            // assignments
            var context = 
                new SymbolicContext()
                {
                    MergeExpressions = false,
                    ContextOptions = { ContextName = "TestCode" }
                };

            context.AttachMathematicaExprSimplifier();

            // Define a Euclidean multivectors processor for the context
            var processor = 
                context.CreateEuclideanProcessor(n);

            // Stage 2: Define the input parameters of the context
            // The input parameters are named variables created as scalar parts of multivectors
            // and used for later processing to compute some outputs

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

            // Define the 3rd vector with a given set of scalar components x1, x2, ...
            var x =
                context.ParameterVariablesFactory.CreateDenseVector(
                    n, 
                    index => $"x{index + 1}"
                );

            // Stage 3: Define computations and specify which variables are outputs
            var rotor = processor.CreateEuclideanRotor(u, v, true);
            
            // Here is another method for making the same computation using a rotation matrix
            //var rotor = u.CreateRotationMatrixToVector(v, n);

            var xRotated = 
                rotor.MapVector(x);

            // Define the final outputs for the computations for proper code generation
            xRotated.SetIsOutput(true);

            // Stage 4: Optimize computations in the symbolic context
            context.OptimizeContext();

            // Stage 5: Assign code generated variable names for all variables
            // Define code generated variable names for input variables
            v.SetExternalNamesByTermId(id => $"v.Scalar{id.PatternToString(n)}");
            u.SetExternalNamesByTermId(id => $"u.Scalar{id.PatternToString(n)}");
            x.SetExternalNamesByTermId(id => $"x.Scalar{id.PatternToString(n)}");
            
            // Define code generated variable names for output variables
            xRotated.SetExternalNamesByTermId(id => $"xRotated.Scalar{id.PatternToString(n)}");

            // Define code generated variable names for intermediate variables
            context.SetIntermediateExternalNamesByNameIndex(index => $"temp{index}");

            // Stage 6: Define a C# code composer with Wolfram Mathematica expressions converter
            var contextCodeComposer = context.CreateContextCodeComposer(
                GaLanguageServer.CSharpWithMathematica()
            );

            // Stage 7: Generate the final C# code
            var code = contextCodeComposer.Generate();

            Console.WriteLine("Generated Code:");
            Console.WriteLine(code);
            Console.WriteLine();
        }
    }
}
