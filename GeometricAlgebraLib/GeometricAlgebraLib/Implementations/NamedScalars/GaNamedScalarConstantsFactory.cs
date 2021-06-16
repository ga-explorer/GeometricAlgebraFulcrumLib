namespace GeometricAlgebraLib.Implementations.NamedScalars
{
    public sealed class GaNamedScalarConstantsFactory<TScalar> :
        GaNamedScalarsFactory<TScalar>
    {
        internal GaNamedScalarConstantsFactory(GaNamedScalarsCollection<TScalar> namedScalarsCollection)
            : base(namedScalarsCollection)
        {
        }
    }
}