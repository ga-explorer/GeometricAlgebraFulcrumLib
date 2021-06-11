namespace GeometricAlgebraLib.Storage
{
    public interface IGaScalarStorage<TScalar>
        : IGaKVectorTermStorage<TScalar>
    {
        IGaScalarStorage<TScalar> Add(IGaScalarStorage<TScalar> mv2);

        IGaScalarStorage<TScalar> Subtract(IGaScalarStorage<TScalar> mv2);

        IGaScalarStorage<TScalar> Op(IGaScalarStorage<TScalar> mv2);
    }
}