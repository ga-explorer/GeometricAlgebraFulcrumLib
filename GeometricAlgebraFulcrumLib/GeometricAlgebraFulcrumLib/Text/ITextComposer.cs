using System.Collections.Generic;
using NumericalGeometryLib.BasicMath;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Text
{
    public interface ITextComposer<T>
    {
        IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        string GetBasisVectorText(ulong index);

        string GetBasisBladeText(ulong id);

        string GetBasisBladeText(uint grade, ulong index);

        string GetBasisBladeText(BasisBlade basisBlade);

        string GetBasisBladeText(IEnumerable<ulong> indexList);

        string GetAngleText(PlanarAngle angle);

        string GetScalarText(T scalar);

        string GetTermText(ulong id, T scalar);

        string GetTermText(uint grade, int index, T scalar);

        string GetTermText(uint grade, ulong index, T scalar);

        string GetTermText(IndexScalarRecord<T> idScalarPair);

        string GetTermText(GradeIndexScalarRecord<T> idScalarPair);

        string GetTermText(BasisBlade basisBlade, T scalar);

        string GetTermText(BasisTerm<T> term);

        string GetTermsText(IEnumerable<IndexScalarRecord<T>> idScalarTuples);

        string GetTermsText(IEnumerable<GradeIndexScalarRecord<T>> idScalarTuples);

        string GetTermsText(uint grade, IEnumerable<IndexScalarRecord<T>> indexScalarTuples);

        string GetTermsText(IEnumerable<BasisTerm<T>> terms);

        string GetArrayText(IReadOnlyList<T> array);

        string GetArrayText(T[,] array);

        string GetMultivectorText(IMultivectorStorage<T> storage);
    }
}