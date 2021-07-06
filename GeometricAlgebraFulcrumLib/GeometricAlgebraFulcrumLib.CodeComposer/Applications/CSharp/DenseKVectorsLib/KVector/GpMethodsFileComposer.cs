using System;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.KVector
{
    internal sealed class GpMethodsFileComposer : 
        GaLibraryFileComposerBase 
    {
        internal GaClcOperationSpecs OperationSpecs { get; }


        internal GpMethodsFileComposer(GaLibraryComposer libGen, GaClcOperationSpecs opSpecs)
            : base(libGen)
        {
            OperationSpecs = opSpecs;
        }


        internal void GenerateMainMethod()
        {
            var caseTemplate = Templates["gp_main_case"];

            var casesText = new ListTextComposer(Environment.NewLine);

            foreach (var inGrade1 in MultivectorProcessor.BasisSet.Grades)
                foreach (var inGrade2 in MultivectorProcessor.BasisSet.Grades)
                {
                    var id = inGrade1 + inGrade2 * MultivectorProcessor.BasisSet.GradesCount;

                    var name = OperationSpecs.GetName(
                        inGrade1, inGrade2
                    );

                    casesText.Add(
                        caseTemplate,
                        "name", name,
                        "id", id,
                        "g1", inGrade1,
                        "g2", inGrade2,
                        "frame", CurrentNamespace
                        );
                }

            TextComposer.AppendAtNewLine(
                Templates["gp_main"],
                "name", OperationSpecs,
                "frame", CurrentNamespace,
                "cases", casesText
            );
        }

        internal void GenerateIntermediateMethod(string gpCaseText, string name)
        {
            TextComposer.AppendAtNewLine(
                Templates["gp"],
                "frame", CurrentNamespace,
                "name", name,
                "double", GaClcLanguage.ScalarTypeName,
                "gp_case", gpCaseText
            );
        }


        public override void Generate()
        {
            throw new NotImplementedException();
        }
    }
}
