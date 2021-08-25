using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.ScalarsGrids;
using GeometricAlgebraFulcrumLib.Processing.ScalarsGrids.Unary;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;
using GeometricAlgebraFulcrumLib.Structures.Utils;

namespace GeometricAlgebraFulcrumLib.Algebra.Arrays
{
    public sealed record GaLaArray<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaLaArray<T> operator -(GaLaArray<T> array)
        {
            var processor = array.ScalarsGridProcessor;

            return new GaLaArray<T>(
                processor,
                processor.Negative(array.ArrayStorage)
            );
        }


        public IGaScalarsGridProcessor<T> ScalarsGridProcessor { get; }

        public IGaGridEven<T> ArrayStorage { get; }

        public int RowsCount 
            => ArrayStorage.GetDenseCount1();

        public int ColumnsCount 
            => ArrayStorage.GetDenseCount2();

        public T this[int i, int j] 
            => ArrayStorage.GetValue((ulong) i, (ulong) j, ScalarsGridProcessor.GetZeroScalar);


        internal GaLaArray([NotNull] IGaScalarsGridProcessor<T> arrayProcessor, [NotNull] IGaGridEven<T> arrayStorage)
        {
            ScalarsGridProcessor = arrayProcessor;
            ArrayStorage = arrayStorage;
        }
    }
}