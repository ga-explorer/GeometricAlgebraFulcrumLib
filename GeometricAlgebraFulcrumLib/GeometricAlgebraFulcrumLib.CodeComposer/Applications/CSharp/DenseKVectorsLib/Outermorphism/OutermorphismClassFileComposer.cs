using System;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.CodeComposer.Composers;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Context;
using GeometricAlgebraFulcrumLib.Processing.SymbolicExpressions.Variables;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using TextComposerLib.Text.Structured;
using TextComposerLib.Text.Linear;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.Outermorphism
{
    internal class OutermorphismClassFileComposer : 
        GaFuLLibraryFileComposerBase
    {
        internal OutermorphismClassFileComposer(GaFuLLibraryComposer libGen)
            : base(libGen)
        {
        }


        private string GenerateOutermorphismTransposeCode()
        {
            var codeText = new ListTextComposer(Environment.NewLine);

            for (var i = 0; i < VSpaceDimension; i++)
                for (var j = 0; j < VSpaceDimension; j++)
                    codeText.Add(
                        "scalars".ScalarItem(i, j) + " = " + "Scalars".ScalarItem(j, i) + ";"
                    );

            return codeText.ToString();
        }

        private string GenerateOutermorphismDeterminantCode()
        {
            var context = 
                new SymbolicContext(DenseKVectorsLibraryComposer.DefaultContextOptions);

            var linearMapArray = context.ParameterVariablesFactory.CreateDenseArray(
                (int) VSpaceDimension,
                (int) VSpaceDimension,
                (row, col) => $"omScalarR{row}C{col}"
            );
            
            var outermorphism = 
                Processor.CreateComputedOutermorphism(linearMapArray);

            var determinant = 
                (SymbolicVariableComputed) outermorphism.GetDeterminant(Processor);

            determinant.IsOutputVariable = true;

            context.OptimizeContext();

            linearMapArray.SetExternalNamesByRowColIndex(
                (row, col) => $"Scalars[{row}, {col}]"
            );

            determinant.ExternalName = "det";

            context.SetIntermediateExternalNamesByNameIndex(
                DenseKVectorsLibraryComposer.MaxTargetLocalVars,
                index => $"tempVar{index:X4}",
                index => $"tempArray[{index}]"
            );

            var macroComposer = 
                new GaFuLSymbolicContextCodeComposer(
                    CodeComposer.GaLanguage, 
                    context, 
                    DenseKVectorsLibraryComposer.DefaultContextCodeComposerOptions
                );

            return macroComposer.Generate();
        }

        private string GenerateOutermorphismPlusCode()
        {
            var codeText = new ListTextComposer(Environment.NewLine);

            for (var i = 0; i < VSpaceDimension; i++)
                for (var j = 0; j < VSpaceDimension; j++)
                    codeText.Add(
                        "scalars".ScalarItem(i, j) + " = " +
                        "om1.Scalars".ScalarItem(i, j) + " + " +
                        "om2.Scalars".ScalarItem(i, j) + ";"
                    );

            return codeText.ToString();
        }

        private string GenerateOutermorphismSubtractCode()
        {
            var codeText = new ListTextComposer(Environment.NewLine);

            for (var i = 0; i < VSpaceDimension; i++)
                for (var j = 0; j < VSpaceDimension; j++)
                    codeText.Add(
                        "scalars".ScalarItem(i, j) + " = " +
                        "om1.Scalars".ScalarItem(i, j) + " - " +
                        "om2.Scalars".ScalarItem(i, j) + ";"
                        );

            return codeText.ToString();
        }

        private string GenerateOutermorphismComposeCode()
        {
            var codeText = new ListTextComposer(Environment.NewLine);

            var sumText = new ListTextComposer(" + ");

            for (var i = 0; i < VSpaceDimension; i++)
            {
                for (var j = 0; j < VSpaceDimension; j++)
                {
                    sumText.Clear();

                    for (var k = 0; k < VSpaceDimension; k++)
                        sumText.Add(
                            "om1.Scalars".ScalarItem(i, k) + " * " + "om2.Scalars".ScalarItem(k, j)
                        );

                    codeText.Add(
                        "scalars".ScalarItem(i, j) + " = " + sumText + ";"
                    );
                }
            }

            return codeText.ToString();
        }

        private string GenerateOutermorphismTimesCode()
        {
            var codeText = new ListTextComposer(Environment.NewLine);

            for (var i = 0; i < VSpaceDimension; i++)
                for (var j = 0; j < VSpaceDimension; j++)
                    codeText.Add(
                        "scalars".ScalarItem(i, j) + " = " +
                        "om.Scalars".ScalarItem(i, j) + " * scalar;"
                    );

            return codeText.ToString();
        }

        private string GenerateOutermorphismDivideCode()
        {
            var codeText = new ListTextComposer(Environment.NewLine);

            for (var i = 0; i < VSpaceDimension; i++)
                for (var j = 0; j < VSpaceDimension; j++)
                    codeText.Add(
                        "scalars".ScalarItem(i, j) + " = " +
                        "om.Scalars".ScalarItem(i, j) + " / scalar;"
                    );

            return codeText.ToString();
        }

        private string GenerateOutermorphismNegativesCode()
        {
            var codeText = new ListTextComposer(Environment.NewLine);

            for (var i = 0; i < VSpaceDimension; i++)
                for (var j = 0; j < VSpaceDimension; j++)
                    codeText.Add(
                        "scalars".ScalarItem(i, j) + " = " +
                        "-om.Scalars".ScalarItem(i, j) + ";"
                    );

            return codeText.ToString();
        }

        private string GenerateOutermorphismApplyCasesCode()
        {
            var codeText = new ListTextComposer(Environment.NewLine);

            for (var inGrade = 1; inGrade <= VSpaceDimension; inGrade++)
                codeText.Add(
                    Templates["om_apply_code_case"],
                    "grade", inGrade,
                    "signature", CurrentNamespace
                    );

            return codeText.ToString();
        }

        public override void Generate()
        {
            GenerateOutermorphismFileStartCode();

            var omTransposeCode =
                GenerateOutermorphismTransposeCode();

            var omDeterminantCode =
                GenerateOutermorphismDeterminantCode();

            var omPlusCode =
                GenerateOutermorphismPlusCode();

            var omSubtractCode =
                GenerateOutermorphismSubtractCode();

            var omComposeCode =
                GenerateOutermorphismComposeCode();

            var omTimesCode =
                GenerateOutermorphismTimesCode();

            var omDivideCode =
                GenerateOutermorphismDivideCode();

            var omNegativeCode =
                GenerateOutermorphismNegativesCode();

            var omApplyCasesCode =
                GenerateOutermorphismApplyCasesCode();

            TextComposer.Append(
                Templates["outermorphism"],
                "signature", CurrentNamespace,
                "double", GaLanguage.ScalarTypeName,
                "transpose_code", omTransposeCode,
                "metric_det_code", omDeterminantCode,
                "plus_code", omPlusCode,
                "subt_code", omSubtractCode,
                "compose_code", omComposeCode,
                "times_code", omTimesCode,
                "divide_code", omDivideCode,
                "negative_code", omNegativeCode,
                "apply_cases_code", omApplyCasesCode
            );

            GenerateOutermorphismFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
