using System;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.CodeComposer.Languages;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.KVector
{
    internal sealed class InvolutionMethodsFileComposer : 
        GaFuLLibraryFileComposerBase
    {
        internal InvolutionMethodsFileComposer(GaFuLLibraryComposer libGen)
            : base(libGen)
        {
        }


        private void GenerateNegativeFunction(ulong kvSpaceDim)
        {
            var caseTemplate = Templates["negative_case"];

            var casesText = new ListTextComposer("," + Environment.NewLine);

            for (var i = 0UL; i < kvSpaceDim; i++)
                casesText.Add(caseTemplate, "num", i);

            TextComposer.AppendAtNewLine(
                Templates["negative"],
                "num", kvSpaceDim,
                "double", GeoLanguage.ScalarTypeName,
                "cases", casesText
            );
        }

        private void GenerateMainInvolutionFunction(GaFuLLanguageOperationSpecs opSpecs, Func<uint, bool> useNegative)
        {
            var caseTemplate1 = Templates["main_negative_case"];
            var caseTemplate2 = Templates["main_negative_case2"];

            var casesText = new ListTextComposer(Environment.NewLine);

            foreach (var grade in GeometricProcessor.Grades)
                if (useNegative(grade))
                    casesText.Add(caseTemplate1,
                        "signature", CurrentNamespace,
                        "grade", grade,
                        "num", this.KVectorSpaceDimension(grade)
                    );
                else
                    casesText.Add(caseTemplate2,
                        "grade", grade
                        );

            TextComposer.AppendAtNewLine(
                Templates["main_involution"],
                "signature", CurrentNamespace,
                "name", opSpecs.GetName(),
                "cases", casesText
            );
        }

        public override void Generate()
        {
            GenerateKVectorFileStartCode();

            var kvSpaceDimList =
                VSpaceDimension
                    .GetRange()
                    .Select(grade => GeometricProcessor.KVectorSpaceDimension(grade))
                    .Distinct();

            foreach (var kvSpaceDim in kvSpaceDimList)
                GenerateNegativeFunction(kvSpaceDim);

            GenerateMainInvolutionFunction(
                GaFuLLanguageOperationKind.UnaryNegative.CreateEuclideanOperationSpecs(), 
                _ => true
            );

            GenerateMainInvolutionFunction(
                GaFuLLanguageOperationKind.UnaryReverse.CreateEuclideanOperationSpecs(), 
                BasisBladeUtils.ReverseIsNegativeOfGrade
            );

            GenerateMainInvolutionFunction(
                GaFuLLanguageOperationKind.UnaryGradeInvolution.CreateEuclideanOperationSpecs(), 
                BasisBladeUtils.GradeInvolutionIsNegativeOfGrade
            );

            GenerateMainInvolutionFunction(
                GaFuLLanguageOperationKind.UnaryCliffordConjugate.CreateEuclideanOperationSpecs(), 
                BasisBladeUtils.CliffordConjugateIsNegativeOfGrade
            );

            GenerateKVectorFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
