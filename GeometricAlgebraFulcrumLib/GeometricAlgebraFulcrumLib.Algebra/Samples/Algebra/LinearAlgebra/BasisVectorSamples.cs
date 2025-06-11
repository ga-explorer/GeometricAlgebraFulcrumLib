using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Algebra.Samples.Algebra.LinearAlgebra
{
    public static class BasisVectorSamples
    {
        public static IReadOnlyList<LinBasisVector> BasisVectors { get; }
            =
            [
                LinBasisVector.Px,
                LinBasisVector.Nx,
                LinBasisVector.Py,
                LinBasisVector.Ny,
                LinBasisVector.Pz,
                LinBasisVector.Nz
            ];

        public static IReadOnlyList<string> BasisVectorNames { get; }
            =
            [
                "Px",
                "Nx",
                "Py",
                "Ny",
                "Pz",
                "Nz"
            ];

        public static void ValidateVectorPairsNormals()
        {
            foreach (var v1 in BasisVectors)
            foreach (var v2 in BasisVectors)
            {
                if (v1.IsParallelTo(v2)) continue;

                var normal1 = LinBasisVectorPair3D.Create(v1, v2).RightNormal.ToLinVector3D();
                var normal2 = v1.ToLinVector3D().VectorCross(v2.ToLinVector3D());

                Debug.Assert(
                    (normal1 - normal2).IsZero()
                );

                normal1 = LinBasisVectorPair3D.Create(v1, v2).LeftNormal.ToLinVector3D();
                normal2 = v2.ToLinVector3D().VectorCross(v1.ToLinVector3D());
                
                Debug.Assert(
                    (normal1 - normal2).IsZero()
                );

            }
        }
    }
}
