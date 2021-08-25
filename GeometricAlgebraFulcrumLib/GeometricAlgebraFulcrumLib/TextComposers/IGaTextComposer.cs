using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Structures;

namespace GeometricAlgebraFulcrumLib.TextComposers
{
    public interface IGaTextComposer<T>
    {
        IGaScalarProcessor<T> ScalarProcessor { get; }

        string GetBasisVectorText(ulong index);

        string GetBasisBladeText(ulong id);

        string GetBasisBladeText(uint grade, ulong index);

        string GetBasisBladeText(GaBasisBlade basisBlade);

        string GetBasisBladeText(IEnumerable<ulong> indexList);

        string GetScalarText(T scalar);

        string GetTermText(ulong id, T scalar);

        string GetTermText(uint grade, int index, T scalar);

        string GetTermText(uint grade, ulong index, T scalar);

        string GetTermText(GaRecordKeyValue<T> idScalarPair);

        string GetTermText(GaRecordGradeKeyValue<T> idScalarPair);

        string GetTermText(GaBasisBlade basisBlade, T scalar);

        string GetTermText(GaBasisTerm<T> term);

        string GetTermsText(IEnumerable<GaRecordKeyValue<T>> idScalarTuples);

        string GetTermsText(IEnumerable<GaRecordGradeKeyValue<T>> idScalarTuples);

        string GetTermsText(uint grade, IEnumerable<GaRecordKeyValue<T>> indexScalarTuples);

        string GetTermsText(IEnumerable<GaBasisTerm<T>> terms);

        string GetArrayText(IReadOnlyList<T> array);

        string GetArrayText(T[,] array);

        string GetMultivectorText(IGaStorageMultivector<T> storage);
    }
}