using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;
using GeometricAlgebraFulcrumLib.MetaProgramming.Composers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;
using GeometricAlgebraFulcrumLib.MetaProgramming.Utilities.Code;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.HGa.Generic;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.Electromagnetics.ImageMethod;

public static class ImageMethodCodeComposers
{
    public static string GenerateIntersectionCode()
    {
        // The number of dimensions
        const int n = 3;
        const string basisNames = "XYZ";

        // Stage 1: Define the meta-programming context
        // The meta-programming context is a special kind
        // of symbolic processor for code generation
        var context =
            new MetaContext()
            {
                MergeExpressions = false,
                ContextOptions =
                {
                    ContextName = "Line segment-triangle intersection",
                    AllowGenerateCode = true,
                    ReduceLowLevelRhsSubExpressions = true
                }
            };

        // Use this if you want Wolfram Mathematica symbolic processor
        // instead of the default AngouriMath symbolic processor
        context.AttachMathematicaExpressionEvaluator();

        // Define a Homogeneous geometry object using the context
        var geometry = 
            new HGaGeometricSpace4D<IMetaExpressionAtomic>(context);
            
        // Stage 2: Define the input parameters of the context
        // The input parameters are named variables created as
        // scalar parts of multivectors and used for later
        // processing to compute some outputs

        // Define the line and plane scalar input parameters
        var line = context.ParameterVariablesFactory.CreateE3DLineSegment(
            (pointIndex, axisIndex) => 
                $"linePoint{pointIndex + 1}{basisNames[axisIndex]}"
        );

        var plane = context.ParameterVariablesFactory.CreateE3DPlaneSegment(
            (pointIndex, axisIndex) => 
                $"planePoint{pointIndex + 1}{basisNames[axisIndex]}"
        );

        // Stage 3: Perform algebraic computations on input parameters
        // A symbolic expression tree for elementary operations on scalar
        // components is created automatically in this stage
        
        // Compute intersection data from line and plane
        var intersectionRecord = geometry.GetIntersection(line, plane);

        // Stage 5: Assign code generated variable names for all variables
        // Define code generated variable names for input variables
        line.SetExternalNames(
            (pointIndex, axisIndex) => 
                $"line.Point{pointIndex + 1}.{basisNames[axisIndex]}"
        );

        plane.SetExternalNames(
            (pointIndex, axisIndex) => 
                $"plane.Point{pointIndex + 1}.{basisNames[axisIndex]}"
        );
        
        // Define code generated variable names for output variables
        intersectionRecord.LinePoint1Distance.SetAsOutput("var linePoint1Distance");
        intersectionRecord.LinePoint2Distance.SetAsOutput("var linePoint2Distance");
        intersectionRecord.PlaneLine12Distance.SetAsOutput("var planeLine12Distance");
        intersectionRecord.PlaneLine23Distance.SetAsOutput("var planeLine23Distance");
        intersectionRecord.PlaneLine31Distance.SetAsOutput("var planeLine31Distance");
        
        Console.WriteLine("Meta Context before optimization:");
        Console.WriteLine(context.ToString());
        Console.WriteLine();

        // Stage 4: Optimize symbolic computations in the meta-programming context
        context.OptimizeContext();

        Console.WriteLine("Meta Context after optimization:");
        Console.WriteLine(context.ToString());
        Console.WriteLine();

        // Define code generated variable names for intermediate variables
        context.SetComputedExternalNamesByOrder(index => $"temp{index}");
        
        // Stage 6: Define a C# code composer with AngouriMath symbolic expressions converter
        var contextCodeComposer = context.CreateContextCodeComposer(
            GaFuLLanguageServerBase.CSharpScalarProcessor()
        );
            
        contextCodeComposer.ComposerOptions.AllowGenerateComputationComments = true;

        // Stage 7: Generate the final C# code
        var code = contextCodeComposer.Generate();

        Console.WriteLine("Generated Code:");
        Console.WriteLine(code);
        Console.WriteLine();

        return code;
    }
}