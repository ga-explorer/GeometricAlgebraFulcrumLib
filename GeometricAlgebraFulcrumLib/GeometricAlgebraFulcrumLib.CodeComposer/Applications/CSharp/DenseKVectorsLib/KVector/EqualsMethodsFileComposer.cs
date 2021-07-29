using System;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.KVector
{
    internal sealed class EqualsMethodsFileComposer : 
        GaLibraryFileComposerBase
    {
        internal EqualsMethodsFileComposer(GaLibraryComposer libGen)
            : base(libGen)
        {
        }


        private void GenerateEqualsFunction(ulong kvSpaceDim)
        {
            var caseTemplate = Templates["equals_case"];

            var casesText = new ListTextComposer(Environment.NewLine);

            for (var i = 0UL; i < kvSpaceDim; i++)
                casesText.Add(caseTemplate, "num", i);

            TextComposer.AppendAtNewLine(
                Templates["equals"],
                "num", kvSpaceDim,
                "double", GaLanguage.ScalarTypeName,
                "cases", casesText
            );
        }

        private void GenerateMainEqualsFunction()
        {
            var caseTemplate = Templates["main_equals_case"];

            var casesText = new ListTextComposer(Environment.NewLine);

            foreach (var grade in Processor.Grades)
                casesText.Add(caseTemplate,
                    "grade", grade,
                    "num", this.KvSpaceDimension(grade)
                );

            TextComposer.AppendAtNewLine(
                Templates["main_equals"],
                "signature", CurrentNamespace,
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
                GenerateEqualsFunction(kvSpaceDim);

            GenerateMainEqualsFunction();

            GenerateKVectorFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
