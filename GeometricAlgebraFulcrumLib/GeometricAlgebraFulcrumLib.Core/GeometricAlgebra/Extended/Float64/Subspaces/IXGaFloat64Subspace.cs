using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.Subspaces;

/// <summary>
/// Initially use OPNS (i.e. direct) representation of subspaces
/// </summary>
public interface IXGaFloat64Subspace : 
    IXGaFloat64Element
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

    XGaFloat64KVector GetBlade();

    XGaFloat64KVector GetBladeInverse();

    XGaFloat64KVector GetBladePseudoInverse();

    /// <summary>
    /// The scalar product of the subspace blade with itself
    /// </summary>
    double BladeSignature { get; }

    IXGaFloat64Subspace Project(IXGaFloat64Subspace mv);

    IXGaFloat64Subspace Reflect(IXGaFloat64Subspace mv);
        
    IXGaFloat64Subspace VersorProduct(IXGaFloat64Subspace mv);

    IXGaFloat64Subspace Complement(IXGaFloat64Subspace mv);
}