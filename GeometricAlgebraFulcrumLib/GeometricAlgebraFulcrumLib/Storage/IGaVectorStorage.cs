namespace GeometricAlgebraFulcrumLib.Storage
{
    public interface IGaVectorStorage<TScalar> 
        : IGaKVectorStorage<TScalar>
    {
        IGaVectorStorage<TScalar> Add(IGaVectorStorage<TScalar> mv2);

        IGaVectorStorage<TScalar> Subtract(IGaVectorStorage<TScalar> mv2);

        IGaBivectorStorage<TScalar> Op(IGaVectorStorage<TScalar> mv2);
    }
}