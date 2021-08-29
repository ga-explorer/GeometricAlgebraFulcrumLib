using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    public interface ITextComposer<T>
    {
        IScalarProcessor<T> ScalarProcessor { get; }

        string GetBasisVectorText(ulong index);

        string GetBasisBladeText(ulong id);

        string GetBasisBladeText(uint grade, ulong index);

        string GetBasisBladeText(GaBasisBlade basisBlade);

        string GetBasisBladeText(IEnumerable<ulong> indexList);

        string GetScalarText(T scalar);

        string GetTermText(ulong id, T scalar);

        string GetTermText(uint grade, int index, T scalar);

        string GetTermText(uint grade, ulong index, T scalar);

        string GetTermText(IndexScalarRecord<T> idScalarPair);

        string GetTermText(GradeIndexScalarRecord<T> idScalarPair);

        string GetTermText(GaBasisBlade basisBlade, T scalar);

        string GetTermText(GaBasisTerm<T> term);

        string GetTermsText(IEnumerable<IndexScalarRecord<T>> idScalarTuples);

        string GetTermsText(IEnumerable<GradeIndexScalarRecord<T>> idScalarTuples);

        string GetTermsText(uint grade, IEnumerable<IndexScalarRecord<T>> indexScalarTuples);

        string GetTermsText(IEnumerable<GaBasisTerm<T>> terms);

        string GetArrayText(IReadOnlyList<T> array);

        string GetArrayText(T[,] array);

        string GetMultivectorText(IGaMultivectorStorage<T> storage);
    }
}