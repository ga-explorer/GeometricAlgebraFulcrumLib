using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Basis;

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
    IndexSet Id { get; }
    
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
    

    IntegerSign EGpSign(IndexSet basisBlade);

    IntegerSign EGpSign(IXGaSignedBasisBlade basisBlade);

    
    IntegerSign GpSign(IndexSet basisBlade);

    IntegerSign GpSign(IXGaSignedBasisBlade basisBlade);


    IntegerSign OpSign(IndexSet basisBlade);
    
    IntegerSign OpSign(IXGaSignedBasisBlade basisBlade);
    

    IntegerSign ESpSign(IndexSet basisBlade);
    
    IntegerSign ESpSign(IXGaSignedBasisBlade basisBlade);
    

    IntegerSign SpSign(IndexSet basisBlade);
    
    IntegerSign SpSign(IXGaSignedBasisBlade basisBlade);
    

    IntegerSign ELcpSign(IndexSet basisBlade);
    
    IntegerSign ELcpSign(IXGaSignedBasisBlade basisBlade);
    

    IntegerSign LcpSign(IndexSet basisBlade);
    
    IntegerSign LcpSign(IXGaSignedBasisBlade basisBlade);
    

    IntegerSign ERcpSign(IndexSet basisBlade);
    
    IntegerSign ERcpSign(IXGaSignedBasisBlade basisBlade);
    

    IntegerSign RcpSign(IndexSet basisBlade);
    
    IntegerSign RcpSign(IXGaSignedBasisBlade basisBlade);
    

    IntegerSign EFdpSign(IndexSet basisBlade);
    
    IntegerSign EFdpSign(IXGaSignedBasisBlade basisBlade);
    

    IntegerSign FdpSign(IndexSet basisBlade);
    
    IntegerSign FdpSign(IXGaSignedBasisBlade basisBlade);
    

    IntegerSign EHipSign(IndexSet basisBlade);
    
    IntegerSign EHipSign(IXGaSignedBasisBlade basisBlade);
    

    IntegerSign HipSign(IndexSet basisBlade);
    
    IntegerSign HipSign(IXGaSignedBasisBlade basisBlade);


    IntegerSign EAcpSign(IndexSet basisBlade);
    
    IntegerSign EAcpSign(IXGaSignedBasisBlade basisBlade);
    

    IntegerSign AcpSign(IndexSet basisBlade);
    
    IntegerSign AcpSign(IXGaSignedBasisBlade basisBlade);
    

    IntegerSign ECpSign(IndexSet basisBlade);
    
    IntegerSign ECpSign(IXGaSignedBasisBlade basisBlade);
    

    IntegerSign CpSign(IndexSet basisBlade);
    
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
    IXGaSignedBasisBlade EGp(IndexSet basisBlade);
    
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
    IXGaSignedBasisBlade Gp(IndexSet basisBlade);
    
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
    IXGaSignedBasisBlade Op(IndexSet basisBlade);
    
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
    IXGaSignedBasisBlade ESp(IndexSet basisBlade);
    
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
    IXGaSignedBasisBlade Sp(IndexSet basisBlade);
    
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
    IXGaSignedBasisBlade ELcp(IndexSet basisBlade);
    
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
    IXGaSignedBasisBlade Lcp(IndexSet basisBlade);
    
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
    IXGaSignedBasisBlade ERcp(IndexSet basisBlade);
    
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
    IXGaSignedBasisBlade Rcp(IndexSet basisBlade);
    
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
    IXGaSignedBasisBlade EFdp(IndexSet basisBlade);
    
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
    IXGaSignedBasisBlade Fdp(IndexSet basisBlade);
    
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
    IXGaSignedBasisBlade EHip(IndexSet basisBlade);
    
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
    IXGaSignedBasisBlade Hip(IndexSet basisBlade);
    
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
    IXGaSignedBasisBlade EAcp(IndexSet basisBlade);
    
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
    IXGaSignedBasisBlade Acp(IndexSet basisBlade);
    
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
    IXGaSignedBasisBlade ECp(IndexSet basisBlade);
    
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
    IXGaSignedBasisBlade Cp(IndexSet basisBlade);
    
    /// <summary>
    /// The commutator product
    /// </summary>
    /// <param name="basisBlade"></param>
    /// <returns></returns>
    IXGaSignedBasisBlade Cp(IXGaSignedBasisBlade basisBlade);
}