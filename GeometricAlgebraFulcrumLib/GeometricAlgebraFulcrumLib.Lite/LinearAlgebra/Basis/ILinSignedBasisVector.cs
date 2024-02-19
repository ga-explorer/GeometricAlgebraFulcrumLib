using DataStructuresLib.Basic;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;

/// <summary>
/// This interface represents a signed basis blade where a sign can be -1, 0, 1
/// and the basis blade is of arbitrary dimensions
/// </summary>
public interface ILinSignedBasisVector :
    IEquatable<ILinSignedBasisVector>,
    ILinearElement
{
    /// <summary>
    /// The basis vector index
    /// </summary>
    int Index { get; }
    
    /// <summary>
    /// The sign, can only be -1, 0, 1
    /// </summary>
    IntegerSign Sign { get; }

    bool IsNegative { get; }

    bool IsZero { get; }

    bool IsPositive { get; }
    
    bool IsNonNegative { get; }

    bool IsNonZero { get; }
    
    bool IsNonPositive { get; }
    
    /// <summary>
    /// The dimensions of the smallest vector space that holds all
    /// basis vector indices in the basis blade
    /// </summary>
    int VSpaceDimensions { get; }
    
    
    LinBasisVector GetBasisVector();
    
    IntegerSign NegativeSign();
    
    IntegerSign TimesSign(IntegerSign sign);
    

    IntegerSign EGpSign(int basisVectorIndex);

    IntegerSign EGpSign(ILinSignedBasisVector basisVector);
    
    IntegerSign OpSign(int basisVectorIndex);
    
    IntegerSign OpSign(ILinSignedBasisVector basisVector);
    
    IntegerSign ESpSign(int basisVectorIndex);
    
    IntegerSign ESpSign(ILinSignedBasisVector basisVector);
    

    ILinSignedBasisVector ShiftIndex(int offset);

    ILinSignedBasisVector Negative();
    
    ILinSignedBasisVector Times(IntegerSign sign);

    bool IsSame(ILinSignedBasisVector axis);

    bool IsOpposite(ILinSignedBasisVector axis);
}