using GeometricAlgebraFulcrumLib.Structures.Grids.Even;
using GeometricAlgebraFulcrumLib.Structures.Grids.Graded;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;
using GeometricAlgebraFulcrumLib.Structures.Lists.Graded;

namespace GeometricAlgebraFulcrumLib.Structures
{
    public sealed record GaRecordGradeKey(uint Grade, ulong Key);

    public sealed record GaRecordGradeKeyValue<T>(uint Grade, ulong Key, T Value);


    public sealed record GaRecordKeyValue<T>(ulong Key, T Value);

    public sealed record GaRecordGradeValue<T>(uint Grade, T Value);


    public sealed record GaRecordGradeKeyPair(uint Grade, ulong Key1, ulong Key2);

    public sealed record GaRecordGradeKeyPairValue<T>(uint Grade, ulong Key1, ulong Key2, T Value);


    public sealed record GaRecordKeyPair(ulong Key1, ulong Key2);

    public sealed record GaRecordKeyPairValue<T>(ulong Key1, ulong Key2, T Value);


    public sealed record GaRecordGradeEvenList<T>(uint Grade, IGaListEven<T> List);

    public sealed record GaRecordGradeEvenGrid<T>(uint Grade, IGaGridEven<T> Grid);


    public sealed record GaRecordGradeEvenListValue<T>(uint Grade, IGaListEven<T> List, T Value);

    public sealed record GaRecordGradeEvenGridValue<T>(uint Grade, IGaGridEven<T> Grid, T Value);


    public sealed record GaRecordEvenListValue<T>(IGaListEven<T> List, T Value);

    public sealed record GaRecordEvenGridValue<T>(IGaGridEven<T> Grid, T Value);


    public sealed record GaRecordGradedListValue<T>(IGaListGraded<T> List, T Value);

    public sealed record GaRecordGradedGridValue<T>(IGaGridGraded<T> Grid, T Value);
}