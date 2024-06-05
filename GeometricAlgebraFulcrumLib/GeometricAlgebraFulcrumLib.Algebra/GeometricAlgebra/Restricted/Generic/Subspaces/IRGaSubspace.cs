using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Subspaces;

/// <summary>
/// Initially use OPNS (i.e. direct) representation of subspaces
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRGaSubspace<T> : 
    IRGaElement<T>
{
    int SubspaceDimension { get; }

    ///// <summary>
    ///// The subspace is represented by an Outer-Product Null Space (OPNS) blade
    ///// </summary>
    //bool IsDirect { get; }

    ///// <summary>
    ///// The subspace is represented by an Inner-Product Null Space (IPNS) blade
    ///// </summary>
    //bool IsDual { get; }

    RGaKVector<T> GetBlade();

    RGaKVector<T> GetBladeInverse();

    RGaKVector<T> GetBladePseudoInverse();

    /// <summary>
    /// The scalar product of the subspace blade with itself
    /// </summary>
    Scalar<T> BladeSignature { get; }

    IRGaSubspace<T> Project(IRGaSubspace<T> mv);

    IRGaSubspace<T> Reflect(IRGaSubspace<T> mv);
        
    IRGaSubspace<T> VersorProduct(IRGaSubspace<T> mv);

    IRGaSubspace<T> Complement(IRGaSubspace<T> mv);
}