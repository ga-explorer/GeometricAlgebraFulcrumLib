using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Structured;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.DenseKVectorsLib.VectorClass;

internal class VectorClassFileComposer : 
    GaFuLLibraryFileComposerBase 
{
    internal VectorClassFileComposer(GaFuLLibraryComposer libGen)
        : base(libGen)
    {
    }


    public override void Generate()
    {
        var textCollection = new ListComposerCollection(
            "basis_vectors",
            "members_declare",
            "init_inputs",
            "init_assign",
            "init_assign_array",
            "members_list",
            "normalize",
            "enorm2"
        )
        {
            ["basis_vectors"] = {Separator = "," + Environment.NewLine},
            ["members_declare"] = {Separator = Environment.NewLine},
            ["init_inputs"] = {Separator = ", "},
            ["init_assign"] = {Separator = Environment.NewLine},
            ["init_assign_array"] = {Separator = Environment.NewLine},
            ["members_list"] = {Separator = ", "},
            ["normalize"] = {Separator = Environment.NewLine},
            ["enorm2"] = {Separator = " + "}
        };


        var basisVectorsCoefsText = new ListTextComposer(", ");

        for (var idx = 1U; idx <= VSpaceDimensions; idx++)
        {
            basisVectorsCoefsText.Clear();
            basisVectorsCoefsText.AddRange(
                (1 << (int) (idx - 1)).PatternToSequence(VSpaceDimensions, "0.0D", "1.0D")
            );

            textCollection["basis_vectors"].Add("new " + CurrentNamespace + "Vector(" + basisVectorsCoefsText + ")");
            textCollection["members_declare"].Add("public double C" + idx + " { get; set; }");
            textCollection["init_inputs"].Add(GeoLanguage.ScalarTypeName + " c" + idx);
            textCollection["init_assign"].Add("C" + idx + " = c" + idx + ";");
            textCollection["init_assign_array"].Add("C" + idx + " = c[" + (idx - 1) + "];");
            textCollection["members_list"].Add("C" + idx);
            textCollection["normalize"].Add("C" + idx + " *= invScalar;");
            textCollection["enorm2"].Add("C" + idx + " * C" + idx);
        }

        Templates["vector"].SetParametersValues(textCollection);

        TextComposer.Append(Templates["vector"],
            "signature", CurrentNamespace,
            "double", GeoLanguage.ScalarTypeName,
            "norm2", textCollection["enorm2"].ToString() //TODO: This must be computed from the signature
        );

        FileComposer.FinalizeText();
    }
}