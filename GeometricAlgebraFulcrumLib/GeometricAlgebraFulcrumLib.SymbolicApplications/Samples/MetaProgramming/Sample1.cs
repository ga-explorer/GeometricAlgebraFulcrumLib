using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.MetaProgramming.Composers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Languages;

namespace GeometricAlgebraFulcrumLib.SymbolicApplications.Samples.MetaProgramming;

public static class Sample1
{
    /// <summary>
    /// A special case for generating code for simple rotors in 3D
    /// </summary>
    public static void Execute1()
    {
        // The number of dimensions
        const int n = 3;
        const string basisNames = "XYZ";

        // Stage 1: Define the meta-programming context
        // The meta-programming context is a special kind of symbolic linear processor for code generation
        var context = 
            new MetaContext()
            {
                MergeExpressions = false,
                ContextOptions = { ContextName = "TestCode" }
            };

        // Use this if you want Wolfram Mathematica symbolic processor
        // instead of the default AngouriMath symbolic processor
        context.AttachMathematicaExpressionEvaluator();

        // Define a Euclidean multivectors processor for the context
        var processor = 
            context.CreateEuclideanXGaProcessor();

        // Stage 2: Define the input parameters of the context
        // The input parameters are named variables created as scalar parts of multivectors
        // and used for later processing to compute some outputs

        // Define the first vector for constructing the rotor with a given
        // set of scalar components u1, u2, ...
        var x =
            context.ParameterVariablesFactory.CreateDenseVector(
                n, 
                index => $"x{index + 1}"
            );

        // Define the second vector for constructing the rotor with a given
        // set of scalar components v1, v2, ...
        var xRotated =
            context.ParameterVariablesFactory.CreateDenseVector(
                n, 
                index => $"xRotated{index + 1}"
            );

        // Define 2 vectors to be rotated
        var y =
            context.ParameterVariablesFactory.CreateDenseVector(
                n, 
                index => $"y{index + 1}"
            );
            
        // Define 2 vectors to be rotated
        var z =
            context.ParameterVariablesFactory.CreateDenseVector(
                n, 
                index => $"z{index + 1}"
            );

        // Stage 3: Define computations and specify which variables are required outputs
        //Define a Euclidean rotor which takes input unit vector u to input unit vector v
        var rotor = x.CreatePureRotor(xRotated, true);
            
        //Find the rotation of an arbitrary input vector x using this rotor
        var yRotated = rotor.OmMap(y);
        var zRotated = rotor.OmMap(z);

        // Define the final outputs for the computations for proper code generation
        yRotated.SetIsOutput(true);
        zRotated.SetIsOutput(true);

        // Stage 4: Optimize symbolic computations in the meta-programming context
        context.OptimizeContext();

        // Stage 5: Assign code generated variable names for all variables
        // Define code generated variable names for input variables
        x.SetExternalNamesByTermId(id => $"x.{id.PatternToString(basisNames)}");
        y.SetExternalNamesByTermId(id => $"y.{id.PatternToString(basisNames)}");
        z.SetExternalNamesByTermId(id => $"z.{id.PatternToString(basisNames)}");
        xRotated.SetExternalNamesByTermId(id => $"xRotated.{id.PatternToString(basisNames)}");

        // Define code generated variable names for output variables
        yRotated.SetExternalNamesByTermId(id => $"yRotated.{id.PatternToString(basisNames)}");
        zRotated.SetExternalNamesByTermId(id => $"zRotated.{id.PatternToString(basisNames)}");

        // Define code generated variable names for intermediate variables
        context.SetIntermediateExternalNamesByNameIndex(index => $"temp{index}");

        // Stage 6: Define a C# code composer with AngouriMath symbolic expressions converter
        var contextCodeComposer = context.CreateContextCodeComposer(
            GaFuLLanguageServerBase.CSharpFloat64()
        );

        // Stage 7: Generate the final C# code
        var code = contextCodeComposer.Generate();

        Console.WriteLine("Generated Code:");
        Console.WriteLine(code);
        Console.WriteLine();
    }

    /// <summary>
    /// A special case for generating code for simple rotors in 3D
    /// </summary>
    public static void Execute2()
    {
        // The number of dimensions
        const int n = 3;
        const string basisNames = "XYZ";

        // Stage 1: Define the meta-programming context
        // The meta-programming context is a special kind of symbolic linear processor for code generation
        var context = 
            new MetaContext()
            {
                MergeExpressions = false,
                ContextOptions = { ContextName = "TestCode" }
            };

        // Use this if you want Wolfram Mathematica symbolic processor
        // instead of the default AngouriMath symbolic processor
        context.AttachMathematicaExpressionEvaluator();

        // Define a Euclidean multivectors processor for the context
        var processor = 
            context.CreateEuclideanXGaProcessor();

        // Stage 2: Define the input parameters of the context
        // The input parameters are named variables created as scalar parts of multivectors
        // and used for later processing to compute some outputs
            
        // Define basis vectors to compute rotation matrix
        var e1 = processor.CreateTermVector(0);
        var e2 = processor.CreateTermVector(1);
        var e3 = processor.CreateTermVector(2);

        // Define the first vector for constructing the rotor with a given
        // set of scalar components u1, u2, ...
        //var x = 
        //    context.ParameterVariablesFactory.CreateDenseVector(
        //        n, 
        //        index => $"x{index + 1}"
        //    );

        // Define the second vector for constructing the rotor with a given
        // set of scalar components v1, v2, ...
        var xRotated =
            context.ParameterVariablesFactory.CreateDenseVector(
                n, 
                index => $"xRotated{index + 1}"
            );
            
        // Stage 3: Define computations and specify which variables are required outputs
        //Define a Euclidean rotor which takes input unit vector u to input unit vector v
        var rotor = xRotated.CreatePureRotor(e3, true);

        //Find the rotation of 3 basis vectors using this rotor
        var e1Rotated = rotor.OmMap(e1);
        var e2Rotated = rotor.OmMap(e2);
        var e3Rotated = rotor.OmMap(e3);

        // Define the final outputs for the computations for proper code generation
        e1Rotated.SetIsOutput(true);
        e2Rotated.SetIsOutput(true);
        e3Rotated.SetIsOutput(true);

        // Stage 4: Optimize symbolic computations in the meta-programming context
        context.OptimizeContext();

        // Stage 5: Assign code generated variable names for all variables
        // Define code generated variable names for input variables
        //x.SetExternalNamesByTermIndex(index => $"_unitVector1.{basisNames[(int) index]}");
        xRotated.SetExternalNamesByTermIndex(index => $"_unitVector.{basisNames[(int) index]}");

        // Define code generated variable names for output variables
        e1Rotated.SetExternalNamesByTermIndex(index => $"RotationMatrix.Scalar{index}0");
        e2Rotated.SetExternalNamesByTermIndex(index => $"RotationMatrix.Scalar{index}1");
        e3Rotated.SetExternalNamesByTermIndex(index => $"RotationMatrix.Scalar{index}2");

        // Define code generated variable names for intermediate variables
        context.SetIntermediateExternalNamesByNameIndex(index => $"temp{index}");

        // Stage 6: Define a C# code composer with AngouriMath symbolic expressions converter
        var contextCodeComposer = context.CreateContextCodeComposer(
            GaFuLLanguageServerBase.CSharpFloat64()
        );

        // Stage 7: Generate the final C# code
        var code = contextCodeComposer.Generate();

        Console.WriteLine("Generated Code:");
        Console.WriteLine(code);
        Console.WriteLine();
    }

    public static void Execute()
    {
        // The number of dimensions
        const int n = 3;

        // Stage 1: Define the meta-programming context
        // The meta-programming context is a special kind of symbolic linear processor for code generation
        var context = 
            new MetaContext()
            {
                MergeExpressions = false,
                ContextOptions = { ContextName = "TestCode" }
            };

        // Use this if you want Wolfram Mathematica symbolic processor
        // instead of the default AngouriMath symbolic processor
        context.AttachMathematicaExpressionEvaluator();

        // Define a Euclidean multivectors processor for the context
        var processor = 
            context.CreateEuclideanXGaProcessor();

        // Stage 2: Define the input parameters of the context
        // The input parameters are named variables created as scalar parts of multivectors
        // and used for later processing to compute some outputs

        // Define the first vector with a given set of scalar components u1, u2, ...
        var u =
            context.ParameterVariablesFactory.CreateDenseVector(
                n, 
                index => $"u_{index + 1}"
            );

        // Define the second vector with a given set of scalar components v1, v2, ...
        var v =
            context.ParameterVariablesFactory.CreateDenseVector(
                n, 
                index => $"v_{index + 1}"
            );

        // Define the 3rd vector with a given set of scalar components x1, x2, ...
        var x =
            context.ParameterVariablesFactory.CreateDenseVector(
                n, 
                index => $"x_{index + 1}"
            );

        // Stage 3: Define computations and specify which variables are required outputs
        //Define a Euclidean rotor which takes input unit vector u to input unit vector v
        var rotor = u.CreatePureRotor(v, true);
            
        //Find the rotation of an arbitrary input vector x using this rotor
        var xRotated = rotor.OmMap(x);

        // Define the final outputs for the computations for proper code generation
        xRotated.SetIsOutput(true);

        // Stage 4: Optimize symbolic computations in the meta-programming context
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

        // Stage 6: Define a C# code composer with AngouriMath symbolic expressions converter
        var contextCodeComposer = context.CreateContextCodeComposer(
            GaFuLLanguageServerBase.CSharpFloat64()
        );

        // Stage 7: Generate the final C# code
        var code = contextCodeComposer.Generate();

        Console.WriteLine("Generated Code:");
        Console.WriteLine(code);
        Console.WriteLine();
    }
}