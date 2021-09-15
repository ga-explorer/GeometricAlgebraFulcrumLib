using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using DataStructuresLib;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Utilities.Composers
{
    public abstract class TextComposerBase<T>
        : ITextComposer<T>
    {
        public IScalarAlgebraProcessor<T> ScalarProcessor { get; }


        protected TextComposerBase([NotNull] IScalarAlgebraProcessor<T> scalarProcessor)
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
            return GaFuLTextComposersUtils.GetBasisBladeText(grade, index);
        }

        public string GetBasisBladeText(BasisBlade basisBlade)
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

        public string GetTermText(IndexScalarRecord<T> idScalarTuple)
        {
            return GetTermText(
                idScalarTuple.Index, 
                idScalarTuple.Scalar
            );
        }

        public string GetTermText(GradeIndexScalarRecord<T> gradeIndexScalarTuple)
        {
            return GetTermText(
                gradeIndexScalarTuple.Grade, 
                gradeIndexScalarTuple.Index,
                gradeIndexScalarTuple.Scalar
            );
        }

        public string GetTermText(BasisBlade basisBlade, T scalar)
        {
            return GetTermText(
                basisBlade.Id,
                scalar
            );
        }

        public string GetTermText(BasisTerm<T> term)
        {
            return GetTermText(
                term.BasisBlade.Id,
                term.Scalar
            );
        }

        public string GetTermsText(IEnumerable<IndexScalarRecord<T>> idScalarTuples)
        {
            return idScalarTuples
                .Select(GetTermText)
                .ConcatenateText(", ");
        }

        public string GetTermsText(IEnumerable<GradeIndexScalarRecord<T>> gradeIndexScalarTuples)
        {
            return gradeIndexScalarTuples
                .Select(GetTermText)
                .ConcatenateText(", ");
        }

        public string GetTermsText(uint grade, IEnumerable<IndexScalarRecord<T>> indexScalarTuples)
        {
            return indexScalarTuples
                .Select(indexScalarPair => GetTermText(grade, indexScalarPair.Index, indexScalarPair.Scalar))
                .ConcatenateText(", ");
        }

        public string GetTermsText(IEnumerable<BasisTerm<T>> terms)
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

        public string GetMultivectorText(IMultivectorStorage<T> storage)
        {
            return GetTermsText(
                ScalarProcessor
                    .GetNotZeroTerms(storage)
                    .OrderByGradeIndex()
            );
        }
    }
}