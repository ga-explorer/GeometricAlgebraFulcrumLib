namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records
{
    public interface IGradeScalarRecord<out T> :
        IGradeRecord,
        IScalarRecord<T>
    {
    }
}