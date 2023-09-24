using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Text
{
    public interface ITextComposer<T>
    {
        IScalarProcessor<T> ScalarProcessor { get; }

        string GetBasisVectorText(ulong index);

        string GetBasisBladeText(ulong id);

        string GetBasisBladeText(uint grade, ulong index);

        string GetBasisBladeText(RGaBasisBlade basisBlade);

        string GetBasisBladeText(IEnumerable<ulong> indexList);

        string GetAngleText(Float64PlanarAngle angle);

        string GetScalarText(Scalar<T> scalar);

        string GetScalarText(T scalar);

        string GetTermText(ulong id, T scalar);

        string GetTermText(uint grade, int index, T scalar);

        string GetTermText(uint grade, ulong index, T scalar);

        string GetTermText(RGaKvIndexScalarRecord<T> idScalarPair);

        string GetTermText(RGaGradeKvIndexScalarRecord<T> idScalarPair);

        string GetTermText(RGaBasisBlade basisBlade, T scalar);

        string GetTermText(KeyValuePair<ulong, T> term);

        string GetTermsText(IEnumerable<RGaKvIndexScalarRecord<T>> idScalarTuples);

        string GetTermsText(IEnumerable<RGaGradeKvIndexScalarRecord<T>> idScalarTuples);

        string GetTermsText(uint grade, IEnumerable<RGaKvIndexScalarRecord<T>> indexScalarTuples);

        string GetTermsText(IEnumerable<KeyValuePair<ulong, T>> terms);

        string GetArrayText(IReadOnlyList<T> array);

        string GetArrayText(T[,] array);

        string GetMultivectorText(IMultivectorStorage<T> storage);
    }
}