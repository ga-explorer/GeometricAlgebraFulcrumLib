using System;
using TextComposerLib.Text.Linear;
using TextComposerLib.Text.Structured;
using TextComposerLib.Text.Parametric;

namespace GeometricAlgebraLib.CodeComposer.Applications.CSharp.DenseKVectorsLib.KVector
{
    /// <summary>
    /// This class generates a static members file for the k-vector class
    /// </summary>
    internal sealed class StaticCodeFileComposer : 
        GaLibraryFileComposerBase 
    {
        internal StaticCodeFileComposer(GaLibraryComposer libGen)
            : base(libGen)
        {
        }


        private string GenerateDeclarations(int grade)
        {
            var kvDim = MultivectorProcessor.BasisSet.KvSpaceDimension(grade);

            var template = Templates["static_basisblade_declare"];

            var declaresText = new ListTextComposer(Environment.NewLine);

            var coefsText = new ListTextComposer(", ");

            for (var index = 0UL; index < kvDim; index++)
            {
                coefsText.Clear();

                for (var i = 0UL; i < kvDim; i++)
                    coefsText.Add((i == index) ? "1.0D" : "0.0D");

                declaresText.Add(
                    template,
                    "frame", CurrentNamespace,
                    "id", MultivectorProcessor.BasisSet.BasisBladeId(grade, index),
                    "grade", grade,
                    "scalars", coefsText
                );
            }

            declaresText.Add("");

            return declaresText.ToString();
        }

        private string GenerateBasisBladesNames(int grade)
        {
            var namesText = new ListTextComposer(", ") { ActiveItemSuffix = "\"", ActiveItemPrefix = "\"" };

            for (var index = 0UL; index < MultivectorProcessor.BasisSet.KvSpaceDimension(grade); index++)
            {
                var id = MultivectorProcessor.BasisSet.BasisBladeId(grade, index);

                namesText.Add($"E{id}");
            }

            return Templates["static_basisblade_name"].GenerateText(
                "names", namesText
            );
        }

        public override void Generate()
        {
            GenerateKVectorFileStartCode();

            var kvdimsText = new ListTextComposer(", ");
            var basisnamesText = new ListTextComposer("," + Environment.NewLine);
            var basisbladesText = new ListTextComposer(Environment.NewLine);

            foreach (var grade in MultivectorProcessor.BasisSet.Grades)
            {
                kvdimsText.Add(MultivectorProcessor.BasisSet.KvSpaceDimension(grade));

                basisnamesText.Add(GenerateBasisBladesNames(grade));

                basisbladesText.Add(GenerateDeclarations(grade));
            }

            TextComposer.Append(
                Templates["static"],
                "frame", CurrentNamespace,
                "vspacedim", VSpaceDimension,
                "double", GaClcLanguage.ScalarTypeName,
                "kvector_sizes_lookup_table", kvdimsText,
                "basisnames", basisnamesText,
                "basisblades", basisbladesText
            );

            GenerateKVectorFileFinishCode();

            FileComposer.FinalizeText();
        }
    }
}
