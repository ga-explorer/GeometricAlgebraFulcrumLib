using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Subspaces;

/// <summary>
/// Initially use OPNS (i.e. direct) representation of subspaces
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IXGaSubspace<T> : 
    IXGaElement<T>
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

    XGaKVector<T> GetBlade();

    XGaKVector<T> GetBladeInverse();

    XGaKVector<T> GetBladePseudoInverse();

    /// <summary>
    /// The scalar product of the subspace blade with itself
    /// </summary>
    Scalar<T> BladeSignature { get; }

    IXGaSubspace<T> Project(IXGaSubspace<T> mv);

    IXGaSubspace<T> Reflect(IXGaSubspace<T> mv);
        
    IXGaSubspace<T> VersorProduct(IXGaSubspace<T> mv);

    IXGaSubspace<T> Complement(IXGaSubspace<T> mv);
}