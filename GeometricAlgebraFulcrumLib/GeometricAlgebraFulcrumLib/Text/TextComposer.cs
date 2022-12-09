using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using DataStructuresLib;
using NumericalGeometryLib.BasicMath;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Text
{
    public class TextComposer<T>
        : ITextComposer<T>
    {
        public IScalarAlgebraProcessor<T> ScalarProcessor { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TextComposer([NotNull] IScalarAlgebraProcessor<T> scalarProcessor)
        {
            ScalarProcessor = scalarProcessor;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetBasisVectorText(ulong index)
        {
            return index.GetBasisVectorText();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetBasisBladeText(ulong id)
        {
            return id.GetBasisBladeText();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetBasisBladeText(uint grade, ulong index)
        {
            return TextComposerUtils.GetBasisBladeText(grade, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetBasisBladeText(BasisBlade basisBlade)
        {
            return basisBlade.GetBasisBladeText();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetScalarText(Scalar<T> scalar)
        {
            return GetScalarText(scalar.ScalarValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual string GetAngleText(PlanarAngle angle)
        {
            return $"{GetScalarText(ScalarProcessor.GetScalarFromNumber(angle.Degrees))} degrees";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual string GetScalarText(T scalar)
        {
            return ScalarProcessor.ToText(scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetTermText(ulong id, T scalar)
        {
            return new StringBuilder()
                .Append($"'{GetScalarText(scalar)}'")
                .Append(GetBasisBladeText(id))
                .ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetTermText(uint grade, int index, T scalar)
        {
            return GetTermText(grade, (ulong) index, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetTermText(uint grade, ulong index, T scalar)
        {
            return new StringBuilder()
                .Append($"'{GetScalarText(scalar)}'")
                .Append(GetBasisBladeText(grade, index))
                .ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetTermText(IndexScalarRecord<T> idScalarTuple)
        {
            return GetTermText(
                idScalarTuple.Index, 
                idScalarTuple.Scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetTermText(GradeIndexScalarRecord<T> gradeIndexScalarTuple)
        {
            return GetTermText(
                gradeIndexScalarTuple.Grade, 
                gradeIndexScalarTuple.Index,
                gradeIndexScalarTuple.Scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetTermText(BasisBlade basisBlade, T scalar)
        {
            return GetTermText(
                basisBlade.Id,
                scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetTermText(BasisTerm<T> term)
        {
            return GetTermText(
                term.BasisBlade.Id,
                term.Scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetTermsText(IEnumerable<IndexScalarRecord<T>> idScalarTuples)
        {
            return idScalarTuples
                .Select(GetTermText)
                .ConcatenateText(", ");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetTermsText(IEnumerable<GradeIndexScalarRecord<T>> gradeIndexScalarTuples)
        {
            return gradeIndexScalarTuples
                .Select(GetTermText)
                .ConcatenateText(", ");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetTermsText(uint grade, IEnumerable<IndexScalarRecord<T>> indexScalarTuples)
        {
            return indexScalarTuples
                .Select(indexScalarPair => GetTermText(grade, indexScalarPair.Index, indexScalarPair.Scalar))
                .ConcatenateText(", ");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetMultivectorText(IMultivectorStorage<T> storage)
        {
            return GetTermsText(
                ScalarProcessor
                    .GetNotZeroTerms(storage)
                    .OrderByGradeIndex()
            );
        }
        
        //public string GetMultivectorText(IMultivectorStorageContainer<T> storage)
        //{
        //    return GetTermsText(
        //        ScalarProcessor
        //            .GetNotZeroTerms(storage.GetMultivectorStorage())
        //            .OrderByGradeIndex()
        //    );
        //}
    }
}