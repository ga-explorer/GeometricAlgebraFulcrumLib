using GeometricAlgebraFulcrumLib.MetaProgramming.Context;
using GeometricAlgebraFulcrumLib.MetaProgramming.Utilities.Code;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;
using GeometricAlgebraFulcrumLib.MetaProgramming.Composers;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Expressions;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.CGa.Generic.Encoding;

namespace GeometricAlgebraFulcrumLib.Samples.Symbolic.MetaProgramming
{
    public static class ModelingVsAlgebraSamples
    {
        public static void AlgebraExample()
        {
            // Stage 1: Define the meta-programming context which
            // is a special kind of symbolic processor for code generation
            var context = new MetaContext();

            // Use this if you want Wolfram Mathematica symbolic processor
            // instead of the default AngouriMath symbolic processor
            context.AttachMathematicaExpressionEvaluator();

            // Define a CGA geometric space for the context
            var cga = context.CreateCGaGeometricSpace5D();

            // The default scalar processor of the context
            var sp = context.ScalarProcessor;


            // Stage 2: Define the input parameters for the computational
            // subroutine. The input parameters are named variables created
            // as scalar components of multivectors and used for later
            // processing to compute some outputs
            
            // desired position components of end-effector
            var pX = context["Px"];
            var pY = context["Py"];
            var pZ = context["Pz"];

            var qX = context["Qx"];
            var qY = context["Qy"];
            var qZ = context["Qz"];

            context.MergeExpressions = true;

            // Stage 3: Perform actual computations using algebraic objects
            var cgaP = cga.EncodeIpnsRound.Point(pX, pY, pZ);
            var cgaQ = cga.EncodeIpnsRound.Point(qX, qY, qZ);

            var cgaLine = cgaP.Op(cgaQ).Op(cga.Ei);

            context.MergeExpressions = false;

            // Stage 3: Set target language input and output variable names
            // then optimize internal expression tree and set target language
            // intermediate variable names after optimization.
            pX.SetExternalName("px");
            pY.SetExternalName("py");
            pZ.SetExternalName("pz");

            qX.SetExternalName("qx");
            qY.SetExternalName("qy");
            qZ.SetExternalName("qz");

            cgaLine.InternalKVector.SetAsOutputByTermId(id => $"L[{id}]");

            context = context.OptimizeContext();

            //context = context.OptimizeContext(
            //    new McGOptParameters()
            //    {
            //        GenerationCount = 300,
            //        CodeFilePath = @"D:\Projects\Study\Surveying\Hansen Problem\CGACode"
            //    }
            //);

            context.SetComputedExternalNamesByOrder(index => $"temp{index}");


            // Stage 4: Define a MATLAB code composer and Generate the final code
            var codeComposer = context.CreateContextCodeComposer(
                GaFuLLanguageServerBase.CSharpFloat64()
            );

            var code = codeComposer.Generate();

            Console.WriteLine(code);
            Console.WriteLine();
        }

        public static void ModelingExample()
        {
            // Stage 1: Define the meta-programming context which
            // is a special kind of symbolic processor for code generation
            var context = new MetaContext();

            // Use this if you want Wolfram Mathematica symbolic processor
            // instead of the default AngouriMath symbolic processor
            context.AttachMathematicaExpressionEvaluator();

            // Define a CGA geometric space for the context
            var cga = context.CreateCGaGeometricSpace5D();

            // The default scalar processor of the context
            var sp = context.ScalarProcessor;


            // Stage 2: Define the input parameters for the computational
            // subroutine. The input parameters are named variables created
            // as scalar components of multivectors and used for later
            // processing to compute some outputs
            
            // desired position components of end-effector
            var pX = context["Px"];
            var pY = context["Py"];
            var pZ = context["Pz"];

            var qX = context["Qx"];
            var qY = context["Qy"];
            var qZ = context["Qz"];

            context.MergeExpressions = true;

            // Stage 3: Perform actual computations using algebraic objects
            // define position vector of end-effector (a linear algebra vector)
            var p = sp.Vector3D(pX, pY, pZ);
            var q = sp.Vector3D(qX, qY, qZ);
            
            // auxiliary lines l1, l2, l3 (geometric algebra blades)
            var cgaLine = cga.EncodeOpnsFlat.LineFromPoints(p, q);

            context.MergeExpressions = false;

            // Stage 3: Set target language input and output variable names
            // then optimize internal expression tree and set target language
            // intermediate variable names after optimization.
            pX.SetExternalName("px");
            pY.SetExternalName("py");
            pZ.SetExternalName("pz");

            qX.SetExternalName("qx");
            qY.SetExternalName("qy");
            qZ.SetExternalName("qz");

            cgaLine.InternalKVector.SetAsOutputByTermId(id => $"L[{id}]");

            context = context.OptimizeContext();

            //context = context.OptimizeContext(
            //    new McGOptParameters()
            //    {
            //        GenerationCount = 300,
            //        CodeFilePath = @"D:\Projects\Study\Surveying\Hansen Problem\CGACode"
            //    }
            //);

            context.SetComputedExternalNamesByOrder(index => $"temp{index}");


            // Stage 4: Define a MATLAB code composer and Generate the final code
            var codeComposer = context.CreateContextCodeComposer(
                GaFuLLanguageServerBase.CSharpFloat64()
            );

            var code = codeComposer.Generate();

            Console.WriteLine(code);
            Console.WriteLine();
        }
    }
}
