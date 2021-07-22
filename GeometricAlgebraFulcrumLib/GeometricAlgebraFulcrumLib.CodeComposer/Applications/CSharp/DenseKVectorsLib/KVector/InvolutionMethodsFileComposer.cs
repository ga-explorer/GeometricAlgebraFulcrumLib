using System;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.KVector
{
    internal sealed class InvolutionMethodsFileComposer : 
        GaLibraryFileComposerBase
    {
        internal InvolutionMethodsFileComposer(GaLibraryComposer libGen)
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
                "double", GaClcLanguage.ScalarTypeName,
                "cases", casesText
            );
        }

        private void GenerateMainInvolutionFunction(GaClcOperationSpecs opSpecs, Func<uint, bool> useNegative)
        {
            var caseTemplate1 = Templates["main_negative_case"];
            var caseTemplate2 = Templates["main_negative_case2"];

            var casesText = new ListTextComposer(Environment.NewLine);

            foreach (var grade in Processor.Grades)
                if (useNegative(grade))
                    casesText.Add(caseTemplate1,
                        "signature", CurrentNamespace,
                        "grade", grade,
                        "num", this.KvSpaceDimension(grade)
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
                    .Select(grade => Processor.KvSpaceDimension(grade))
                    .Distinct();

            foreach (var kvSpaceDim in kvSpaceDimList)
                GenerateNegativeFunction(kvSpaceDim);

            GenerateMainInvolutionFunction(
                GaClcOperationKind.UnaryNegative.CreateEuclideanOperationSpecs(), 
                _ => true
            );

            GenerateMainInvolutionFunction(
                GaClcOperationKind.UnaryReverse.CreateEuclideanOperationSpecs(), 
                GaBasisUtils.GradeHasNegativeReverse
            );

            GenerateMainInvolutionFunction(
                GaClcOperationKind.UnaryGradeInvolution.CreateEuclideanOperationSpecs(), 
                GaBasisUtils.GradeHasNegativeGradeInvolution
            );

            GenerateMainInvolutionFunction(
                GaClcOperationKind.UnaryCliffordConjugate.CreateEuclideanOperationSpecs(), 
                GaBasisUtils.GradeHasNegativeCliffordConjugate
            );

            GenerateKVectorFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
