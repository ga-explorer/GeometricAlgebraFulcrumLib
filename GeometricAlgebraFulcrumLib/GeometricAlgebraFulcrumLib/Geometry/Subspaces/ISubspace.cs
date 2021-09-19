using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Subspaces
{
    /// <summary>
    /// Initially use OPNS (i.e. direct) representation of subspaces
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISubspace<T> : 
        IGeometricAlgebraElement<T>
    {
        uint SubspaceDimension { get; }

        /// <summary>
        /// The subspace is represented by an Outer-Product Null Space (OPNS) blade
        /// </summary>
        bool IsDirect { get; }

        /// <summary>
        /// The subspace is represented by an Inner-Product Null Space (IPNS) blade
        /// </summary>
        bool IsDual { get; }

        KVectorStorage<T> Blade { get; }

        KVectorStorage<T> BladeInverse { get; }

        /// <summary>
        /// The scalar product of the subspace blade with itself
        /// </summary>
        T BladeSignature { get; }

        ISubspace<T> Project(ISubspace<T> mv);

        ISubspace<T> Reflect(ISubspace<T> mv);
        
        ISubspace<T> VersorProduct(ISubspace<T> mv);

        ISubspace<T> Complement(ISubspace<T> mv);
    }
}