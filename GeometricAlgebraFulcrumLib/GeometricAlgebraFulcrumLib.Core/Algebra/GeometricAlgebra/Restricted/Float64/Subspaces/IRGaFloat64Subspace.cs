using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Subspaces;

/// <summary>
/// Initially use OPNS (i.e. direct) representation of subspaces
/// </summary>
public interface IRGaFloat64Subspace : 
    IRGaFloat64Element
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

    RGaFloat64KVector GetBlade();

    RGaFloat64KVector GetBladeInverse();

    RGaFloat64KVector GetBladePseudoInverse();

    /// <summary>
    /// The scalar product of the subspace blade with itself
    /// </summary>
    double BladeSignature { get; }

    IRGaFloat64Subspace Project(IRGaFloat64Subspace mv);

    IRGaFloat64Subspace Reflect(IRGaFloat64Subspace mv);
        
    IRGaFloat64Subspace VersorProduct(IRGaFloat64Subspace mv);

    IRGaFloat64Subspace Complement(IRGaFloat64Subspace mv);
}