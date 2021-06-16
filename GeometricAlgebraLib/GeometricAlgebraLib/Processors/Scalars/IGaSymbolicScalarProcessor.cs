namespace GeometricAlgebraLib.Processors.Scalars
{
    public interface IGaSymbolicScalarProcessor<TScalar> :
        IGaScalarProcessor<TScalar>
    {
        TScalar GetSymbol(string symbolNameText);
    }
}