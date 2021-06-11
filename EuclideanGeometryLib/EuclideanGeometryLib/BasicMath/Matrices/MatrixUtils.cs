using System.Collections.Generic;

namespace EuclideanGeometryLib.BasicMath.Matrices
{
    public static class MatrixUtils
    {
        //TODO: Replace Matrices with the System.Numeric versions described here:
        //https://docs.microsoft.com/en-us/dotnet/api/System.Numerics.Matrix4x4?view=netframework-4.6.1
    
        public static IEnumerable<double> GetComponents(this Matrix4X4 matrix, bool columnMajorOrder = true)
        {
            if (columnMajorOrder)
            {
                yield return matrix[0];
                yield return matrix[1];
                yield return matrix[2];
                yield return matrix[3];

                yield return matrix[4];
                yield return matrix[5];
                yield return matrix[6];
                yield return matrix[7];

                yield return matrix[8];
                yield return matrix[9];
                yield return matrix[10];
                yield return matrix[11];

                yield return matrix[12];
                yield return matrix[13];
                yield return matrix[14];
                yield return matrix[15];
            }
            else
            {
                yield return matrix[0];
                yield return matrix[4];
                yield return matrix[8];
                yield return matrix[12];

                yield return matrix[1];
                yield return matrix[5];
                yield return matrix[9];
                yield return matrix[13];

                yield return matrix[2];
                yield return matrix[6];
                yield return matrix[10];
                yield return matrix[14];

                yield return matrix[3];
                yield return matrix[7];
                yield return matrix[11];
                yield return matrix[15];
            }
        }

    }
}
