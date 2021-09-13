namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records
{
    public interface IIndexScalarRecord<out T> :
        IIndexRecord,
        IScalarRecord<T>
    {
    }
}