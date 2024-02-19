using DataStructuresLib.Basic;
using DataStructuresLib.IndexSets;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Basis;

/// <summary>
/// This interface represents a signed basis blade where a sign can be -1, 0, 1
/// and the basis blade is of arbitrary dimensions
/// </summary>
public interface IXGaSignedBasisBlade :
    IEquatable<IXGaSignedBasisBlade>,
    IXGaElement
{
    /// <summary>
    /// The basis blade identifier (basis vectors set)
    /// </summary>
    IIndexSet Id { get; }
    
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
    
    bool IsScalar { get; }

    bool IsVector { get; }

    bool IsBivector { get; }

    /// <summary>
    /// The dimensions of the smallest vector space that holds all
    /// basis vector indices in the basis blade
    /// </summary>
    int VSpaceDimensions { get; }

    /// <summary>
    /// The grade of the basis blade
    /// </summary>
    int Grade { get; }

    /// <summary>
    /// The dimensions of the smallest k-vector space that holds the
    /// basis blade
    /// </summary>
    ulong KvSpaceDimensions { get; }
    
    
    XGaBasisBlade GetBasisBlade();

    IntegerSign Signature();

    IntegerSign NegativeSign();

    IntegerSign ReverseSign();

    IntegerSign GradeInvolutionSign();

    IntegerSign CliffordConjugateSign();

    IntegerSign EConjugateSign();

    IntegerSign ConjugateSign();

    IntegerSign TimesSign(IntegerSign sign);
    

    IntegerSign EGpSign(IIndexSet basisBlade);

    IntegerSign EGpSign(IXGaSignedBasisBlade basisBlade);

    
    IntegerSign GpSign(IIndexSet basisBlade);

    IntegerSign GpSign(IXGaSignedBasisBlade basisBlade);


    IntegerSign OpSign(IIndexSet basisBlade);
    
    IntegerSign OpSign(IXGaSignedBasisBlade basisBlade);
    

    IntegerSign ESpSign(IIndexSet basisBlade);
    
    IntegerSign ESpSign(IXGaSignedBasisBlade basisBlade);
    

    IntegerSign SpSign(IIndexSet basisBlade);
    
    IntegerSign SpSign(IXGaSignedBasisBlade basisBlade);
    

    IntegerSign ELcpSign(IIndexSet basisBlade);
    
    IntegerSign ELcpSign(IXGaSignedBasisBlade basisBlade);
    

    IntegerSign LcpSign(IIndexSet basisBlade);
    
    IntegerSign LcpSign(IXGaSignedBasisBlade basisBlade);
    

    IntegerSign ERcpSign(IIndexSet basisBlade);
    
    IntegerSign ERcpSign(IXGaSignedBasisBlade basisBlade);
    

    IntegerSign RcpSign(IIndexSet basisBlade);
    
    IntegerSign RcpSign(IXGaSignedBasisBlade basisBlade);
    

    IntegerSign EFdpSign(IIndexSet basisBlade);
    
    IntegerSign EFdpSign(IXGaSignedBasisBlade basisBlade);
    

    IntegerSign FdpSign(IIndexSet basisBlade);
    
    IntegerSign FdpSign(IXGaSignedBasisBlade basisBlade);
    

    IntegerSign EHipSign(IIndexSet basisBlade);
    
    IntegerSign EHipSign(IXGaSignedBasisBlade basisBlade);
    

    IntegerSign HipSign(IIndexSet basisBlade);
    
    IntegerSign HipSign(IXGaSignedBasisBlade basisBlade);


    IntegerSign EAcpSign(IIndexSet basisBlade);
    
    IntegerSign EAcpSign(IXGaSignedBasisBlade basisBlade);
    

    IntegerSign AcpSign(IIndexSet basisBlade);
    
    IntegerSign AcpSign(IXGaSignedBasisBlade basisBlade);
    

    IntegerSign ECpSign(IIndexSet basisBlade);
    
    IntegerSign ECpSign(IXGaSignedBasisBlade basisBlade);
    

    IntegerSign CpSign(IIndexSet basisBlade);
    
    IntegerSign CpSign(IXGaSignedBasisBlade basisBlade);


    IXGaSignedBasisBlade ShiftIndices(int offset);

    IXGaSignedBasisBlade Negative();

    IXGaSignedBasisBlade Reverse();

    IXGaSignedBasisBlade GradeInvolution();

    IXGaSignedBasisBlade CliffordConjugate();

    IXGaSignedBasisBlade EConjugate();

    IXGaSignedBasisBlade Conjugate();

    IXGaSignedBasisBlade Times(IntegerSign sign);


    /// <summary>
    /// The Euclidean geometric product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade EGp(IIndexSet basisBlade);
    
    /// <summary>
    /// The Euclidean geometric product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade EGp(IXGaSignedBasisBlade basisBlade);


    /// <summary>
    /// The geometric product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade Gp(IIndexSet basisBlade);
    
    /// <summary>
    /// The geometric product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade Gp(IXGaSignedBasisBlade basisBlade);


    /// <summary>
    /// The outer product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade Op(IIndexSet basisBlade);
    
    /// <summary>
    /// The outer product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade Op(IXGaSignedBasisBlade basisBlade);


    /// <summary>
    /// The Euclidean scalar product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade ESp(IIndexSet basisBlade);
    
    /// <summary>
    /// The Euclidean scalar product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade ESp(IXGaSignedBasisBlade basisBlade);


    /// <summary>
    /// The scalar product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade Sp(IIndexSet basisBlade);
    
    /// <summary>
    /// The scalar product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade Sp(IXGaSignedBasisBlade basisBlade);


    /// <summary>
    /// The Euclidean left-contraction product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade ELcp(IIndexSet basisBlade);
    
    /// <summary>
    /// The Euclidean left-contraction product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade ELcp(IXGaSignedBasisBlade basisBlade);


    /// <summary>
    /// The left-contraction product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade Lcp(IIndexSet basisBlade);
    
    /// <summary>
    /// The left-contraction product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade Lcp(IXGaSignedBasisBlade basisBlade);


    /// <summary>
    /// The Euclidean right-contraction product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade ERcp(IIndexSet basisBlade);
    
    /// <summary>
    /// The Euclidean right-contraction product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade ERcp(IXGaSignedBasisBlade basisBlade);


    /// <summary>
    /// The right-contraction product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade Rcp(IIndexSet basisBlade);
    
    /// <summary>
    /// The right-contraction product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade Rcp(IXGaSignedBasisBlade basisBlade);


    /// <summary>
    /// The Euclidean fat-dot product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade EFdp(IIndexSet basisBlade);
    
    /// <summary>
    /// The Euclidean fat-dot product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade EFdp(IXGaSignedBasisBlade basisBlade);


    /// <summary>
    /// The fat-dot product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade Fdp(IIndexSet basisBlade);
    
    /// <summary>
    /// The fat-dot product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade Fdp(IXGaSignedBasisBlade basisBlade);


    /// <summary>
    /// The Euclidean Hestenes inner product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade EHip(IIndexSet basisBlade);
    
    /// <summary>
    /// The Euclidean Hestenes inner product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade EHip(IXGaSignedBasisBlade basisBlade);


    /// <summary>
    /// The Hestenes inner product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade Hip(IIndexSet basisBlade);
    
    /// <summary>
    /// The Hestenes inner product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade Hip(IXGaSignedBasisBlade basisBlade);


    /// <summary>
    /// The Euclidean anti-commutator product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade EAcp(IIndexSet basisBlade);
    
    /// <summary>
    /// The Euclidean anti-commutator product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade EAcp(IXGaSignedBasisBlade basisBlade);


    /// <summary>
    /// The anti-commutator product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade Acp(IIndexSet basisBlade);
    
    /// <summary>
    /// The anti-commutator product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade Acp(IXGaSignedBasisBlade basisBlade);


    /// <summary>
    /// The Euclidean commutator product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade ECp(IIndexSet basisBlade);
    
    /// <summary>
    /// The Euclidean commutator product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade ECp(IXGaSignedBasisBlade basisBlade);


    /// <summary>
    /// The commutator product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade Cp(IIndexSet basisBlade);
    
    /// <summary>
    /// The commutator product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade Cp(IXGaSignedBasisBlade basisBlade);
}