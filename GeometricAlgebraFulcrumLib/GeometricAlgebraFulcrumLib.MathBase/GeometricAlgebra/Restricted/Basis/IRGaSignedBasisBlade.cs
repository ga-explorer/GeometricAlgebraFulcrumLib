using DataStructuresLib.Basic;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Basis
{
    /// <summary>
    /// This interface represents a signed basis blade where a sign can be -1, 0, 1
    /// and the basis blade is of arbitrary dimensions
    /// </summary>
    public interface IRGaSignedBasisBlade :
        IEquatable<IRGaSignedBasisBlade>,
        IRGaElement
    {
        /// <summary>
        /// The basis blade identifier (basis vectors set)
        /// </summary>
        ulong Id { get; }
    
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
    
    
        RGaBasisBlade GetBasisBlade();

        IntegerSign Signature();

        IntegerSign NegativeSign();

        IntegerSign ReverseSign();

        IntegerSign GradeInvolutionSign();

        IntegerSign CliffordConjugateSign();

        IntegerSign EConjugateSign();

        IntegerSign ConjugateSign();

        IntegerSign TimesSign(IntegerSign sign);
    

        IntegerSign EGpSign(ulong basisBlade);

        IntegerSign EGpSign(IRGaSignedBasisBlade basisBlade);

    
        IntegerSign GpSign(ulong basisBlade);

        IntegerSign GpSign(IRGaSignedBasisBlade basisBlade);


        IntegerSign OpSign(ulong basisBlade);
    
        IntegerSign OpSign(IRGaSignedBasisBlade basisBlade);
    

        IntegerSign ESpSign(ulong basisBlade);
    
        IntegerSign ESpSign(IRGaSignedBasisBlade basisBlade);
    

        IntegerSign SpSign(ulong basisBlade);
    
        IntegerSign SpSign(IRGaSignedBasisBlade basisBlade);
    

        IntegerSign ELcpSign(ulong basisBlade);
    
        IntegerSign ELcpSign(IRGaSignedBasisBlade basisBlade);
    

        IntegerSign LcpSign(ulong basisBlade);
    
        IntegerSign LcpSign(IRGaSignedBasisBlade basisBlade);
    

        IntegerSign ERcpSign(ulong basisBlade);
    
        IntegerSign ERcpSign(IRGaSignedBasisBlade basisBlade);
    

        IntegerSign RcpSign(ulong basisBlade);
    
        IntegerSign RcpSign(IRGaSignedBasisBlade basisBlade);
    

        IntegerSign EFdpSign(ulong basisBlade);
    
        IntegerSign EFdpSign(IRGaSignedBasisBlade basisBlade);
    

        IntegerSign FdpSign(ulong basisBlade);
    
        IntegerSign FdpSign(IRGaSignedBasisBlade basisBlade);
    

        IntegerSign EHipSign(ulong basisBlade);
    
        IntegerSign EHipSign(IRGaSignedBasisBlade basisBlade);
    

        IntegerSign HipSign(ulong basisBlade);
    
        IntegerSign HipSign(IRGaSignedBasisBlade basisBlade);


        IntegerSign EAcpSign(ulong basisBlade);
    
        IntegerSign EAcpSign(IRGaSignedBasisBlade basisBlade);
    

        IntegerSign AcpSign(ulong basisBlade);
    
        IntegerSign AcpSign(IRGaSignedBasisBlade basisBlade);
    

        IntegerSign ECpSign(ulong basisBlade);
    
        IntegerSign ECpSign(IRGaSignedBasisBlade basisBlade);
    

        IntegerSign CpSign(ulong basisBlade);
    
        IntegerSign CpSign(IRGaSignedBasisBlade basisBlade);


        IRGaSignedBasisBlade ShiftIndices(int offset);

        IRGaSignedBasisBlade Negative();

        IRGaSignedBasisBlade Reverse();

        IRGaSignedBasisBlade GradeInvolution();

        IRGaSignedBasisBlade CliffordConjugate();

        IRGaSignedBasisBlade EConjugate();

        IRGaSignedBasisBlade Conjugate();

        IRGaSignedBasisBlade Times(IntegerSign sign);


        /// <summary>
        /// The Euclidean geometric product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade EGp(ulong basisBlade);
    
        /// <summary>
        /// The Euclidean geometric product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade EGp(IRGaSignedBasisBlade basisBlade);


        /// <summary>
        /// The geometric product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade Gp(ulong basisBlade);
    
        /// <summary>
        /// The geometric product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade Gp(IRGaSignedBasisBlade basisBlade);


        /// <summary>
        /// The outer product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade Op(ulong basisBlade);
    
        /// <summary>
        /// The outer product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade Op(IRGaSignedBasisBlade basisBlade);


        /// <summary>
        /// The Euclidean scalar product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade ESp(ulong basisBlade);
    
        /// <summary>
        /// The Euclidean scalar product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade ESp(IRGaSignedBasisBlade basisBlade);


        /// <summary>
        /// The scalar product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade Sp(ulong basisBlade);
    
        /// <summary>
        /// The scalar product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade Sp(IRGaSignedBasisBlade basisBlade);


        /// <summary>
        /// The Euclidean left-contraction product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade ELcp(ulong basisBlade);
    
        /// <summary>
        /// The Euclidean left-contraction product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade ELcp(IRGaSignedBasisBlade basisBlade);


        /// <summary>
        /// The left-contraction product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade Lcp(ulong basisBlade);
    
        /// <summary>
        /// The left-contraction product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade Lcp(IRGaSignedBasisBlade basisBlade);


        /// <summary>
        /// The Euclidean right-contraction product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade ERcp(ulong basisBlade);
    
        /// <summary>
        /// The Euclidean right-contraction product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade ERcp(IRGaSignedBasisBlade basisBlade);


        /// <summary>
        /// The right-contraction product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade Rcp(ulong basisBlade);
    
        /// <summary>
        /// The right-contraction product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade Rcp(IRGaSignedBasisBlade basisBlade);


        /// <summary>
        /// The Euclidean fat-dot product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade EFdp(ulong basisBlade);
    
        /// <summary>
        /// The Euclidean fat-dot product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade EFdp(IRGaSignedBasisBlade basisBlade);


        /// <summary>
        /// The fat-dot product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade Fdp(ulong basisBlade);
    
        /// <summary>
        /// The fat-dot product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade Fdp(IRGaSignedBasisBlade basisBlade);


        /// <summary>
        /// The Euclidean Hestenes inner product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade EHip(ulong basisBlade);
    
        /// <summary>
        /// The Euclidean Hestenes inner product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade EHip(IRGaSignedBasisBlade basisBlade);


        /// <summary>
        /// The Hestenes inner product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade Hip(ulong basisBlade);
    
        /// <summary>
        /// The Hestenes inner product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade Hip(IRGaSignedBasisBlade basisBlade);


        /// <summary>
        /// The Euclidean anti-commutator product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade EAcp(ulong basisBlade);
    
        /// <summary>
        /// The Euclidean anti-commutator product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade EAcp(IRGaSignedBasisBlade basisBlade);


        /// <summary>
        /// The anti-commutator product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade Acp(ulong basisBlade);
    
        /// <summary>
        /// The anti-commutator product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade Acp(IRGaSignedBasisBlade basisBlade);


        /// <summary>
        /// The Euclidean commutator product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade ECp(ulong basisBlade);
    
        /// <summary>
        /// The Euclidean commutator product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade ECp(IRGaSignedBasisBlade basisBlade);


        /// <summary>
        /// The commutator product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade Cp(ulong basisBlade);
    
        /// <summary>
        /// The commutator product
        /// </summary>
        /// <param name="basisBlade"></param>
        /// <returns></returns>
        IRGaSignedBasisBlade Cp(IRGaSignedBasisBlade basisBlade);
    }
}