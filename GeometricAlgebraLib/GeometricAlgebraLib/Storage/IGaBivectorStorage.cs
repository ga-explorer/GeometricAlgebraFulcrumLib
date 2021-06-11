namespace GeometricAlgebraLib.Storage
{
    public interface IGaBivectorStorage<TScalar> 
        : IGaKVectorStorage<TScalar>
    {
        IGaBivectorStorage<TScalar> Add(IGaBivectorStorage<TScalar> mv2);

        IGaBivectorStorage<TScalar> Subtract(IGaBivectorStorage<TScalar> mv2);
    }
}