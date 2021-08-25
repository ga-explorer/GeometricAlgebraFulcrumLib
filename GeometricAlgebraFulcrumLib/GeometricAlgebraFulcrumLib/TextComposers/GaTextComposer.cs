using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using DataStructuresLib;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.Utils;
using GeometricAlgebraFulcrumLib.Structures;

namespace GeometricAlgebraFulcrumLib.TextComposers
{
    public abstract class GaTextComposer<T>
        : IGaTextComposer<T>
    {
        public IGaScalarProcessor<T> ScalarProcessor { get; }


        protected GaTextComposer([NotNull] IGaScalarProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;
        }


        public string GetBasisVectorText(ulong index)
        {
            return index.GetBasisVectorText();
        }

        public string GetBasisBladeText(ulong id)
        {
            return id.GetBasisBladeText();
        }

        public string GetBasisBladeText(uint grade, ulong index)
        {
            return GaTextComposersUtils.GetBasisBladeText(grade, index);
        }

        public string GetBasisBladeText(GaBasisBlade basisBlade)
        {
            return basisBlade.GetBasisBladeText();
        }

        public string GetBasisBladeText(IEnumerable<ulong> indexList)
        {
            return indexList.GetBasisBladeText();

            //var composer = new StringBuilder();

            //composer.Append('<');

            //var firstItemFlag = true;
            //foreach (var index in indexList)
            //{
            //    if (firstItemFlag)
            //        firstItemFlag = false;
            //    else
            //        composer.Append(", ");

            //    composer.Append(index + 1);
            //}

            //composer.Append('>');

            //return composer.ToString();
        }

        public abstract string GetScalarText(T scalar);

        public string GetTermText(ulong id, T scalar)
        {
            return new StringBuilder()
                .Append($"'{GetScalarText(scalar)}'")
                .Append(GetBasisBladeText(id))
                .ToString();
        }

        public string GetTermText(uint grade, int index, T scalar)
        {
            return GetTermText(grade, (ulong) index, scalar);
        }

        public string GetTermText(uint grade, ulong index, T scalar)
        {
            return new StringBuilder()
                .Append($"'{GetScalarText(scalar)}'")
                .Append(GetBasisBladeText(grade, index))
                .ToString();
        }

        public string GetTermText(GaRecordKeyValue<T> idScalarTuple)
        {
            return GetTermText(
                idScalarTuple.Key, 
                idScalarTuple.Value
            );
        }

        public string GetTermText(GaRecordGradeKeyValue<T> gradeIndexScalarTuple)
        {
            return GetTermText(
                gradeIndexScalarTuple.Grade, 
                gradeIndexScalarTuple.Key,
                gradeIndexScalarTuple.Value
            );
        }

        public string GetTermText(GaBasisBlade basisBlade, T scalar)
        {
            return GetTermText(
                basisBlade.Id,
                scalar
            );
        }

        public string GetTermText(GaBasisTerm<T> term)
        {
            return GetTermText(
                term.BasisBlade.Id,
                term.Scalar
            );
        }

        public string GetTermsText(IEnumerable<GaRecordKeyValue<T>> idScalarTuples)
        {
            return idScalarTuples
                .Select(GetTermText)
                .ConcatenateText(", ");
        }

        public string GetTermsText(IEnumerable<GaRecordGradeKeyValue<T>> gradeIndexScalarTuples)
        {
            return gradeIndexScalarTuples
                .Select(GetTermText)
                .ConcatenateText(", ");
        }

        public string GetTermsText(uint grade, IEnumerable<GaRecordKeyValue<T>> indexScalarTuples)
        {
            return indexScalarTuples
                .Select(indexScalarPair => GetTermText(grade, indexScalarPair.Key, indexScalarPair.Value))
                .ConcatenateText(", ");
        }

        public string GetTermsText(IEnumerable<GaBasisTerm<T>> terms)
        {
            return terms
                .Select(GetTermText)
                .ConcatenateText(", ");
        }

        public string GetArrayText(IReadOnlyList<T> array)
        {
            var composer = new StringBuilder();

            var scalarsCount = array.Count;

            for (var i = 0; i < scalarsCount; i++)
            {
                if (i > 0)
                    composer.Append(", ");

                composer.Append(GetScalarText(array[i]));
            }

            return composer.ToString();
        }

        public virtual string GetArrayText(T[,] array)
        {
            var composer = new StringBuilder();

            var rowsCount = array.GetLength(0);
            var colsCount = array.GetLength(1);

            for (var i = 0; i < rowsCount; i++)
            {
                if (i > 0)
                    composer.AppendLine();

                for (var j = 0; j < colsCount; j++)
                {
                    if (j > 0)
                        composer.Append(", ");

                    composer.Append(GetScalarText(array[i, j]));
                }
            }

            return composer.ToString();
        }

        public string GetMultivectorText(IGaStorageMultivector<T> storage)
        {
            return GetTermsText(
                ScalarProcessor
                    .GetNotZeroTerms(storage)
                    .OrderByGradeIndex()
            );
        }
    }
}