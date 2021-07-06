using System.Text;

namespace GeometricAlgebraLib.CodeComposer.Applications.CSharp.DenseKVectorsLib
{
    internal static class GaLibraryComposerUtils
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
}
