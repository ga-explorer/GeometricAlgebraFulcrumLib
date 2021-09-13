namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Records
{
    public interface IScalarRecord<out T>
    {
        T Scalar { get; }
    }
}