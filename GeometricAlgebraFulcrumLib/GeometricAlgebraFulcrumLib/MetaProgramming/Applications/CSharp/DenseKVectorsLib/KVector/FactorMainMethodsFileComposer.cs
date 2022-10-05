using System;
using GeometricAlgebraFulcrumLib.MetaProgramming.Languages;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.DenseKVectorsLib.KVector
{
    internal sealed class FactorMainMethodsFileComposer : 
        GaFuLLibraryFileComposerBase 
    {
        internal FactorMainMethodsFileComposer(GaFuLLibraryComposer libGen)
            : base(libGen)
        {
        }


        private void GenerateMaxCoefIdFunction(uint grade)
        {
            var casesText = new ListTextComposer(Environment.NewLine);

            var maxIndex = this.KVectorSpaceDimension(grade) - 1;

            for (var index = 1UL; index < maxIndex; index++)
                casesText.Add(
                    Templates["maxcoefid_case"],
                    "index", index,
                    "id", GeometricProcessor.BasisBladeId(grade, index)
                );

            TextComposer.Append(
                Templates["maxcoefid"],
                "grade", grade,
                "double", GeoLanguage.ScalarTypeName,
                "initid", GeometricProcessor.BasisBladeId(grade, 0),
                "maxindex", maxIndex,
                "maxid", GeometricProcessor.BasisBladeId(grade, maxIndex),
                "maxcoefid_case", casesText
            );
        }

        private void GenerateFactorGradeFunction(uint grade)
        {
            var casesText = new ListTextComposer(Environment.NewLine);

            for (var index = 1UL; index < GeometricProcessor.KVectorSpaceDimension(grade); index++)
                casesText.Add(
                    Templates["factorgrade_case"].GenerateUsing(
                        GeometricProcessor.BasisBladeId(grade, index)
                        )
                    );

            TextComposer.Append(
                Templates["factorgrade"],
                "signature", CurrentNamespace,
                "grade", grade,
                "double", GeoLanguage.ScalarTypeName,
                "factorgrade_case", casesText
            );
        }

        private void GenerateFactorMainFunction()
        {
            var casesText = new ListTextComposer(Environment.NewLine);

            for (var grade = 2; grade < VSpaceDimension; grade++)
            {
                var methodName =
                    GaFuLLanguageOperationKind.UnaryNorm.GetName(true);

                casesText.Add(
                    Templates["factor_main_case"],
                    "name", methodName,
                    "grade", grade,
                    "signature", CurrentNamespace
                );
            }

            TextComposer.Append(
                Templates["factor_main"],
                "signature", CurrentNamespace,
                "maxgrade", VSpaceDimension,
                "maxid", GeometricProcessor.MaxBasisBladeId,
                "factor_main_case", casesText
            );
        }

        public override void Generate()
        {
            GenerateKVectorFileStartCode();

            for (var grade = 2U; grade < VSpaceDimension; grade++)
            {
                GenerateMaxCoefIdFunction(grade);

                GenerateFactorGradeFunction(grade);
            }

            GenerateFactorMainFunction();

            GenerateKVectorFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
