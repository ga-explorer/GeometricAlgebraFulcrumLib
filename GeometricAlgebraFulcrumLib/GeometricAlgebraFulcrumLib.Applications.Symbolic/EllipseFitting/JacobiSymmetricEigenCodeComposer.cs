using System.Diagnostics;
using System.Text;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;
using GeometricAlgebraFulcrumLib.MetaProgramming.Composers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Utilities.Code;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.EllipseFitting
{
    public static class JacobiSymmetricEigenCodeComposer
    {
        /// <summary>
        /// The Rotate function performs a Givens rotation to reduce one off-diagonal
        /// element of the matrix to (nearly) zero. It is used iteratively in the
        /// Jacobi method to diagonalize a symmetric matrix, which allows us to extract
        /// eigenvalues from the diagonal and eigenvectors from the accumulated rotations.
        /// </summary>
        /// <param name="inputA"></param>
        /// <param name="inputV"></param>
        /// <param name="p"></param>
        /// <param name="q"></param>
        private static (Scalar<IMetaExpressionAtomic>[,], Scalar<IMetaExpressionAtomic>[,]) Rotate(Scalar<IMetaExpressionAtomic>[,] inputA, Scalar<IMetaExpressionAtomic>[,] inputV, int p, int q)
        {
            var n = inputA.GetLength(0);

            Debug.Assert(n == inputA.GetLength(1));
            Debug.Assert(p >= 0 && p < q);

            var context = (MetaContext)inputA[0, 0].ScalarProcessor;
            var zero = context.Zero;
            
            // The off-diagonal element we want to eliminate
            var apq = inputA[p, q];

            // The corresponding diagonal entries
            var app = inputA[p, p];
            var aqq = inputA[q, q];

            // This computes the tangent of the rotation angle t = tan(θ) using a
            // numerically stable formula derived from solving the zeroing condition
            var tau = (aqq - app) / (2 * apq);
            var t = tau.Sign() / ((1 + tau * tau).Sqrt() + tau.Abs());

            // Compute Cosine and Sine of θ
            var c = 1 / (1 + t * t).Sqrt();
            var s = t * c;

            var oa = new Scalar<IMetaExpressionAtomic>[n, n];
            var ov = new Scalar<IMetaExpressionAtomic>[n, n];
            
            for (var i = 0; i < n; i++)
            for (var j = 0; j < n; j++)
            {
                oa[i, j] = inputA[i, j];
                ov[i, j] = inputV[i, j];
            }

            // Zero out off-diagonal elements
            oa[p, q] = zero;
            oa[q, p] = zero;

            // Update diagonal entries
            oa[p, p] = app - t * apq;
            oa[q, q] = aqq + t * apq;

            // Apply rotation to other rows/columns
            for (var r = 0; r < n; r++)
            {
                if (r == p || r == q) continue;

                // Row updates
                var arp = oa[r, p];
                var arq = oa[r, q];

                oa[r, p] = arp * c - arq * s;
                oa[r, q] = arq * c + arp * s;

                // Symmetric column updates
                oa[p, r] = oa[r, p];
                oa[q, r] = oa[r, q];
            }

            // Update eigenvectors
            for (var r = 0; r < n; r++)
            {
                var vrp = ov[r, p];
                var vrq = ov[r, q];

                ov[r, p] = vrp * c - vrq * s;
                ov[r, q] = vrq * c + vrp * s;
            }

            return (oa, ov);
        }

        private static string ComposeRotationCode(int n)
        {
            // Stage 1: Define the meta-programming context
            // The meta-programming context is a special kind
            // of symbolic processor for code generation
            var context =
                new MetaContext()
                {
                    MergeExpressions = false,
                    ContextOptions =
                    {
                        ContextName = $"JacobiSymmetricEigenDecomposer{n}x{n}",
                        AllowGenerateComments = true,
                        PropagateConstants = true,
                        ReduceLowLevelRhsSubExpressions = true,
                        //ReUseIntermediateVariables = false
                    }
                };

            // Use this if you want Wolfram Mathematica symbolic processor
            // instead of the default AngouriMath symbolic processor
            context.AttachMathematicaExpressionEvaluator();


            // Stage 2: Define the input parameters of the context
            // The input parameters are named variables created as
            // scalar parts of multivectors and used for later
            // processing to compute some outputs
            var ia = new Scalar<IMetaExpressionAtomic>[n, n];
            for (var colIndex = 0; colIndex < n; colIndex++)
            for (var rowIndex = colIndex; rowIndex < n; rowIndex++)
            {
                var scalar = 
                    context.ScalarProcessor.ScalarFromValue(
                        context
                            .ParameterVariablesFactory
                            .CreateScalarValue($"iaR{rowIndex}C{colIndex}")
                    );

                ia[rowIndex, colIndex] = scalar;
                ia[colIndex, rowIndex] = scalar;
            }

            var iv = 
                context.ParameterVariablesFactory.CreateDenseArray2D(
                    n, 
                    n, 
                    (i, j) => $"ivR{i}C{j}"
                ).MapItems(i => i.ScalarFromValue(context));

            // Stage 3: Perform algebraic computations on input parameters
            // A symbolic expression tree for elementary operations on scalar
            // components is created automatically in this stage

            // Apply all pairs of rotations
            var oa = ia;
            var ov = iv;

            for (var p = 0; p < n - 1; p++)
            for (var q = p + 1; q < n; q++)
                (oa, ov) = Rotate(oa, ov, p, q);

            for (var rowIndex = 0; rowIndex < n; rowIndex++)
            for (var colIndex = 0; colIndex < n; colIndex++)
            {
                if (rowIndex >= colIndex)
                {
                    ia[rowIndex, colIndex].SetExternalName($"_oaR{rowIndex}C{colIndex}");
                    oa[rowIndex, colIndex].SetAsOutput($"var oaR{rowIndex}C{colIndex}");
                }
                
                iv[rowIndex, colIndex].SetExternalName($"_ovR{rowIndex}C{colIndex}");
                ov[rowIndex, colIndex].SetAsOutput($"var ovR{rowIndex}C{colIndex}");
            }
            

            // Stage 4: Optimize symbolic computations in the meta-programming context
            context.OptimizeContext();

            //context = MetaContextGeneticOptimizer.Process(
            //    new McGOptParameters()
            //    {
            //        GenerationCount = 300,
            //        CodeFilePath = @"D:\Projects\Papers\Active\2023-Conic Fitting\FittingBenchmark"
            //    },
            //    context
            //);


            // Define code generated variable names for intermediate variables
            context.SetComputedExternalNamesByOrder(index => $"temp{index}");


            // Stage 6: Define a C# code composer with AngouriMath symbolic expressions converter
            var contextCodeComposer = context.CreateContextCodeComposer(
                GaFuLLanguageServerBase.CSharpFloat64()
            );

            contextCodeComposer.ComposerOptions.AllowGenerateComputationComments = false;

            // Stage 7: Generate the final C# code
            var code = 
                contextCodeComposer
                    .Generate()
                    .Replace("Math.Sign", "BinaryStep");

            //Console.WriteLine("Generated Code:");
            //Console.WriteLine(code);
            //Console.WriteLine();


            //var dotCode = contextCodeComposer.GenerateGraphVizCode();

            //Console.WriteLine("GraphViz Code:");
            //Console.WriteLine(dotCode);
            //Console.WriteLine();

            var codeComposer = new LinearTextComposer();

            codeComposer.AppendLine(code);
            
            for (var colIndex = 0; colIndex < n; colIndex++)
            for (var rowIndex = colIndex; rowIndex < n; rowIndex++)
            {
                codeComposer.AppendLine($"_oaR{rowIndex}C{colIndex} = oaR{rowIndex}C{colIndex};");
            }

            codeComposer.AppendLine();

            for (var colIndex = 0; colIndex < n; colIndex++)
            for (var rowIndex = 0; rowIndex < n; rowIndex++)
            {
                codeComposer.AppendLine($"_ovR{rowIndex}C{colIndex} = ovR{rowIndex}C{colIndex};");
            }

            return codeComposer.ToString();
        }

        private static string ComposeMatrixUpdateCode(int n)
        {
            var composer = new StringBuilder();

            composer
                .AppendLine($"DiagonalMatrix = new double[{n}, {n}];")
                .AppendLine($"EigenVectors = new double[{n}, {n}];")
                .AppendLine($"EigenValues = new double[{n}];")
                .AppendLine();

            composer.AppendLine("// Update diagonal matrix");

            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    var (p, q) = i >= j ? (i, j) : (j, i);

                    composer.AppendLine($"DiagonalMatrix[{i}, {j}] = _oaR{p}C{q};");
                }

                composer.AppendLine();
            }

            composer.AppendLine("// Update eigen vectors matrix");
            
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++) 
                {
                    composer.AppendLine($"EigenVectors[{i}, {j}] = _ovR{i}C{j};");
                }

                composer.AppendLine();
            }
            
            composer.AppendLine("// Update eigen values");

            for (var i = 0; i < n; i++) 
            {
                composer.AppendLine($"EigenValues[{i}] = _oaR{i}C{i};");
            }

            return composer.ToString();
        }

        public static string ComposeCode(int n)
        {
            var rotationCode = ComposeRotationCode(n);
            var updateCode = ComposeMatrixUpdateCode(n);

            var composer = new LinearTextComposer();

            composer
                .AppendLine($"private void EigenDecompose{n}()")
                .AppendLine("{")
                .IncreaseIndentation()
                .AppendLine($"Initialize{n}();")
                .AppendLine()
                .AppendLine("for (var sweep = 0; sweep < MaxSweeps; sweep++)")
                .AppendLine("{")
                .IncreaseIndentation()
                .AppendLine($"if (GetOffDiagonalNorm{n}() < NormTolerance) break;")
                .AppendLine()
                .AppendLine(rotationCode)
                .DecreaseIndentation()
                .AppendLineAtNewLine("}")
                .AppendLine()
                .AppendLine(updateCode)
                .DecreaseIndentation()
                .AppendLine("}");

            return composer.ToString();
        }

        public static string ComposeCode()
        {
            var composer = new StringBuilder();

            for (var n = 2; n <= 6; n++)
            {
                Console.Write($"Composing code for n = {n} .. ");

                composer.AppendLine(ComposeCode(n));

                Console.WriteLine("done.");
            }

            return composer.ToString();
        }
    }
}
