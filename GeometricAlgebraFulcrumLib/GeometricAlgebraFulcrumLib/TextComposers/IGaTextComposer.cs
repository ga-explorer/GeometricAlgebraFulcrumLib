using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Terms;

namespace GeometricAlgebraFulcrumLib.TextComposers
{
    public interface IGaTextComposer<T>
    {
        IGaScalarProcessor<T> ScalarProcessor { get; }

        string GetBasisVectorText(ulong index);

        string GetBasisBladeText(ulong id);

        string GetBasisBladeText(uint grade, ulong index);

        string GetBasisBladeText(IGaBasisBlade basisBlade);

        string GetBasisBladeText(IEnumerable<ulong> indexList);

        string GetScalarText(T scalar);

        string GetTermText(ulong id, T scalar);

        string GetTermText(uint grade, int index, T scalar);

        string GetTermText(uint grade, ulong index, T scalar);

        string GetTermText(KeyValuePair<ulong, T> idScalarPair);

        string GetTermText(Tuple<ulong, T> idScalarPair);

        string GetTermText(Tuple<uint, ulong, T> idScalarPair);

        string GetTermText(IGaBasisBlade basisBlade, T scalar);

        string GetTermText(GaTerm<T> term);

        string GetTermsText(IEnumerable<KeyValuePair<ulong, T>> idScalarPairs);

        string GetTermsText(IEnumerable<Tuple<ulong, T>> idScalarTuples);

        string GetTermsText(IEnumerable<Tuple<uint, ulong, T>> idScalarTuples);

        string GetTermsText(uint grade, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs);

        string GetTermsText(uint grade, IEnumerable<Tuple<ulong, T>> indexScalarTuples);

        string GetTermsText(IEnumerable<GaTerm<T>> terms);

        string GetArrayText(T[] array);
        
        string GetArrayText(T[,] array);

        string GetMultivectorText(IGaStorageMultivector<T> storage);
    }
}