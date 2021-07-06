using System;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GeometricAlgebraLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.KVector
{
    internal sealed class FactorMainMethodsFileComposer : 
        GaLibraryFileComposerBase 
    {
        internal FactorMainMethodsFileComposer(GaLibraryComposer libGen)
            : base(libGen)
        {
        }


        private void GenerateMaxCoefIdFunction(int grade)
        {
            var casesText = new ListTextComposer(Environment.NewLine);

            var maxIndex = MultivectorProcessor.BasisSet.KvSpaceDimension(grade) - 1;

            for (var index = 1UL; index < maxIndex; index++)
                casesText.Add(
                    Templates["maxcoefid_case"],
                    "index", index,
                    "id", MultivectorProcessor.BasisSet.BasisBladeId(grade, index)
                );

            TextComposer.Append(
                Templates["maxcoefid"],
                "grade", grade,
                "double", GaClcLanguage.ScalarTypeName,
                "initid", MultivectorProcessor.BasisSet.BasisBladeId(grade, 0),
                "maxindex", maxIndex,
                "maxid", MultivectorProcessor.BasisSet.BasisBladeId(grade, maxIndex),
                "maxcoefid_case", casesText
            );
        }

        private void GenerateFactorGradeFunction(int grade)
        {
            var casesText = new ListTextComposer(Environment.NewLine);

            for (var index = 1UL; index < MultivectorProcessor.BasisSet.KvSpaceDimension(grade); index++)
                casesText.Add(
                    Templates["factorgrade_case"].GenerateUsing(
                        MultivectorProcessor.BasisSet.BasisBladeId(grade, index)
                        )
                    );

            TextComposer.Append(
                Templates["factorgrade"],
                "frame", CurrentNamespace,
                "grade", grade,
                "double", GaClcLanguage.ScalarTypeName,
                "factorgrade_case", casesText
            );
        }

        private void GenerateFactorMainFunction()
        {
            var casesText = new ListTextComposer(Environment.NewLine);

            for (var grade = 2; grade < VSpaceDimension; grade++)
            {
                var methodName =
                    GaClcOperationKind.UnaryNorm.GetName(true);

                casesText.Add(
                    Templates["factor_main_case"],
                    "name", methodName,
                    "grade", grade,
                    "frame", CurrentNamespace
                );
            }

            TextComposer.Append(
                Templates["factor_main"],
                "frame", CurrentNamespace,
                "maxgrade", VSpaceDimension,
                "maxid", MultivectorProcessor.BasisSet.MaxBasisBladeId,
                "factor_main_case", casesText
            );
        }

        public override void Generate()
        {
            GenerateKVectorFileStartCode();

            for (var grade = 2; grade < VSpaceDimension; grade++)
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
