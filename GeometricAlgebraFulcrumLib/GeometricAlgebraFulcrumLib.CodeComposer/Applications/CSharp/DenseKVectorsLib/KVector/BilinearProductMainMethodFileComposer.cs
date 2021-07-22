using System;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;

namespace GeometricAlgebraFulcrumLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.KVector
{
    internal sealed class BilinearProductMainMethodFileComposer : 
        GaLibraryFileComposerBase
    {
        internal GaClcOperationSpecs OperationSpecs { get; }

        internal string ZeroCondition { get; }

        internal Func<uint, uint, uint> GetFinalGrade { get; }

        internal Func<uint, uint, bool> IsLegalGrade { get; }


        internal BilinearProductMainMethodFileComposer(GaLibraryComposer libGen, GaClcOperationSpecs opSpecs, string zeroCondition, Func<uint, uint, uint> getFinalGrade, Func<uint, uint, bool> isLegalGrade)
            : base(libGen)
        {
            OperationSpecs = opSpecs;

            ZeroCondition = zeroCondition;

            GetFinalGrade = getFinalGrade;

            IsLegalGrade = isLegalGrade;
        }


        public string GetCasesText()
        {
            var t2 = Templates["bilinearproduct_main_case"];

            var casesText = new ListTextComposer(Environment.NewLine);

            foreach (var grade1 in Processor.Grades)
                foreach (var grade2 in Processor.Grades)
                {
                    if (IsLegalGrade(grade1, grade2) == false)
                        continue;

                    var grade = 
                        GetFinalGrade(grade1, grade2);

                    var id = 
                        grade1 + grade2 * Processor.GradesCount;

                    var name = OperationSpecs.GetName(
                        grade1, grade2, grade
                    );

                    casesText.Add(t2,
                        "name", name,
                        "id", id,
                        "g1", grade1,
                        "g2", grade2,
                        "grade", grade,
                        "signature", CurrentNamespace
                    );
                }

            return casesText.ToString();
        }

        public override void Generate()
        {
            GenerateKVectorFileStartCode();

            var casesText = GetCasesText();

            TextComposer.AppendAtNewLine(
                Templates["bilinearproduct_main"],
                "name", OperationSpecs,
                "signature", CurrentNamespace,
                "zerocond", ZeroCondition,
                "cases", casesText
            );

            GenerateKVectorFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
