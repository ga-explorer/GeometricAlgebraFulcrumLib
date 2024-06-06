using System.Text;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.LibraryGenerators.CSharp.DenseKVectorsLib;

internal static class GaFuLLibraryComposerUtils
{
    internal static string ScalarItem(this string name, object row, object column)
    {
        return
            new StringBuilder()
                .Append(name)
                .Append("[")
                .Append(row)
                .Append(", ")
                .Append(column)
                .Append("]")
                .ToString();
    }


}