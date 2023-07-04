using System;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GAPoTNumLib.Text.Linear;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.Samples.PowerSystems.GAPoT
{
    public static class SimpleKirchhoffRotationSamples
    {
        public static void CodeGenerationExample1()
        {
            var composer = new LinearTextComposer();

            for (var n = 3; n <= 48; n++)
            {
                var text1 = n.GetRange()
                    .Select(i => $"uVector[{i}]")
                    .Concatenate(" +" + Environment.NewLine, "", ";");

                var text2 = (n - 1).GetRange()
                    .Select(i => $"vVector[{i}] = uVector[{i}] + k;")
                    .Concatenate(Environment.NewLine);

                var text3 = $"vVector[{n - 1}] = uVector[{n - 1}] + k - m;";

                composer
                    .AppendLine($"public double[] SkrRotate{n}D(double[] uVector)")
                    .AppendLine("{")
                    .IncreaseIndentation()
                    .AppendLine($"const int n = {n};")
                    .AppendLine()
                    .AppendLine("var nSqrt = Math.Sqrt(n);")
                    .AppendLine("var vVector = new double[n];")
                    .AppendLine()
                    .AppendLine("var a = ")
                    .IncreaseIndentation()
                    .AppendLine(text1)
                    .DecreaseIndentation()
                    .AppendLine("a /= 1d + nSqrt;")
                    .AppendLine()
                    .AppendLine("var un = uVector[n - 1];")
                    .AppendLine("var k = (un - a) / nSqrt;")
                    .AppendLine("var m = un + a;")
                    .AppendLine()
                    .AppendLine(text2)
                    .AppendLine(text3)
                    .AppendLine()
                    .AppendLine("return vVector;")
                    .DecreaseIndentation()
                    .AppendLineAtNewLine("}")
                    .AppendLine();
            }

            Console.WriteLine(composer.ToString());
        }


        //private static bool GeneratePreComputationsCode(GaFuLMetaContextCodeComposer contextCodeComposer)
        //{
        //    //Generate comments
        //    GaFuLMetaContextCodeComposer.DefaultGenerateCommentsBeforeComputations(contextCodeComposer);

        //    //Temp variables declaration
        //    //Add array declaration code
        //    contextCodeComposer.SyntaxList.Add(
        //        contextCodeComposer.GeoLanguage.SyntaxFactory.DeclareLocalArray(
        //            "double",
        //            "tempArray",
        //            contextCodeComposer.Context.GetTargetTempVarsCount().ToString()
        //        )
        //    );

        //    contextCodeComposer.SyntaxList.AddEmptyLine();

        //    return true;
        //}
        
    }
}
