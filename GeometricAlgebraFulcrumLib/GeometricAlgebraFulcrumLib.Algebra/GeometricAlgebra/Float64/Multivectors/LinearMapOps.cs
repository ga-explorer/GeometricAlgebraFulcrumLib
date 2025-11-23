using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors
{
    public abstract partial class XGaFloat64Multivector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64ComputedUnilinearMap LeftOpMap()
        {
            return Processor.UnilinearMap(
                id => Op(Processor.KVectorTerm(id))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64ComputedUnilinearMap RightOpMap()
        {
            return Processor.UnilinearMap(
                id => Processor.KVectorTerm(id).Op(this)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> LeftOpMapMatrix(int vSpaceDimensions)
        {
            if (vSpaceDimensions < VSpaceDimensions || vSpaceDimensions > 12)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            var size = 1 << vSpaceDimensions;

            return LeftOpMap().GetMultivectorMapMatrix(size, size);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> RightOpMapMatrix(int vSpaceDimensions)
        {
            if (vSpaceDimensions < VSpaceDimensions || vSpaceDimensions > 12)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            var size = 1 << vSpaceDimensions;

            return RightOpMap().GetMultivectorMapMatrix(size, size);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64ComputedUnilinearMap LeftELcpMap()
        {
            return Processor.UnilinearMap(
                id => ELcp(Processor.KVectorTerm(id))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64ComputedUnilinearMap RightELcpMap()
        {
            return Processor.UnilinearMap(
                id => Processor.KVectorTerm(id).ELcp(this)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> LeftELcpMapMatrix(int vSpaceDimensions)
        {
            if (vSpaceDimensions < VSpaceDimensions || vSpaceDimensions > 12)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            var size = 1 << vSpaceDimensions;

            return LeftELcpMap().GetMultivectorMapMatrix(size, size);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> RightELcpMapMatrix(int vSpaceDimensions)
        {
            if (vSpaceDimensions < VSpaceDimensions || vSpaceDimensions > 12)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            var size = 1 << vSpaceDimensions;

            return RightELcpMap().GetMultivectorMapMatrix(size, size);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64ComputedUnilinearMap LeftLcpMap()
        {
            return Processor.UnilinearMap(
                id => Lcp(Processor.KVectorTerm(id))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64ComputedUnilinearMap RightLcpMap()
        {
            return Processor.UnilinearMap(
                id => Processor.KVectorTerm(id).Lcp(this)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> LeftLcpMapMatrix(int vSpaceDimensions)
        {
            if (vSpaceDimensions < VSpaceDimensions || vSpaceDimensions > 12)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            var size = 1 << vSpaceDimensions;

            return LeftLcpMap().GetMultivectorMapMatrix(size, size);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> RightLcpMapMatrix(int vSpaceDimensions)
        {
            if (vSpaceDimensions < VSpaceDimensions || vSpaceDimensions > 12)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            var size = 1 << vSpaceDimensions;

            return RightLcpMap().GetMultivectorMapMatrix(size, size);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64ComputedUnilinearMap LeftERcpMap()
        {
            return Processor.UnilinearMap(
                id => ERcp(Processor.KVectorTerm(id))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64ComputedUnilinearMap RightERcpMap()
        {
            return Processor.UnilinearMap(
                id => Processor.KVectorTerm(id).ERcp(this)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> LeftERcpMapMatrix(int vSpaceDimensions)
        {
            if (vSpaceDimensions < VSpaceDimensions || vSpaceDimensions > 12)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            var size = 1 << vSpaceDimensions;

            return LeftERcpMap().GetMultivectorMapMatrix(size, size);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> RightERcpMapMatrix(int vSpaceDimensions)
        {
            if (vSpaceDimensions < VSpaceDimensions || vSpaceDimensions > 12)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            var size = 1 << vSpaceDimensions;

            return RightERcpMap().GetMultivectorMapMatrix(size, size);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64ComputedUnilinearMap LeftRcpMap()
        {
            return Processor.UnilinearMap(
                id => Rcp(Processor.KVectorTerm(id))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64ComputedUnilinearMap RightRcpMap()
        {
            return Processor.UnilinearMap(
                id => Processor.KVectorTerm(id).Rcp(this)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> LeftRcpMapMatrix(int vSpaceDimensions)
        {
            if (vSpaceDimensions < VSpaceDimensions || vSpaceDimensions > 12)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            var size = 1 << vSpaceDimensions;

            return LeftRcpMap().GetMultivectorMapMatrix(size, size);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> RightRcpMapMatrix(int vSpaceDimensions)
        {
            if (vSpaceDimensions < VSpaceDimensions || vSpaceDimensions > 12)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            var size = 1 << vSpaceDimensions;

            return RightRcpMap().GetMultivectorMapMatrix(size, size);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64ComputedUnilinearMap LeftEFdpMap()
        {
            return Processor.UnilinearMap(
                id => EFdp(Processor.KVectorTerm(id))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64ComputedUnilinearMap RightEFdpMap()
        {
            return Processor.UnilinearMap(
                id => Processor.KVectorTerm(id).EFdp(this)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> LeftEFdpMapMatrix(int vSpaceDimensions)
        {
            if (vSpaceDimensions < VSpaceDimensions || vSpaceDimensions > 12)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            var size = 1 << vSpaceDimensions;

            return LeftEFdpMap().GetMultivectorMapMatrix(size, size);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> RightEFdpMapMatrix(int vSpaceDimensions)
        {
            if (vSpaceDimensions < VSpaceDimensions || vSpaceDimensions > 12)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            var size = 1 << vSpaceDimensions;

            return RightEFdpMap().GetMultivectorMapMatrix(size, size);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64ComputedUnilinearMap LeftFdpMap()
        {
            return Processor.UnilinearMap(
                id => Fdp(Processor.KVectorTerm(id))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64ComputedUnilinearMap RightFdpMap()
        {
            return Processor.UnilinearMap(
                id => Processor.KVectorTerm(id).Fdp(this)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> LeftFdpMapMatrix(int vSpaceDimensions)
        {
            if (vSpaceDimensions < VSpaceDimensions || vSpaceDimensions > 12)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            var size = 1 << vSpaceDimensions;

            return LeftFdpMap().GetMultivectorMapMatrix(size, size);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> RightFdpMapMatrix(int vSpaceDimensions)
        {
            if (vSpaceDimensions < VSpaceDimensions || vSpaceDimensions > 12)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            var size = 1 << vSpaceDimensions;

            return RightFdpMap().GetMultivectorMapMatrix(size, size);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64ComputedUnilinearMap LeftEGpMap()
        {
            return Processor.UnilinearMap(
                id => EGp(Processor.KVectorTerm(id))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64ComputedUnilinearMap RightEGpMap()
        {
            return Processor.UnilinearMap(
                id => Processor.KVectorTerm(id).EGp(this)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> LeftEGpMapMatrix(int vSpaceDimensions)
        {
            if (vSpaceDimensions < VSpaceDimensions || vSpaceDimensions > 12)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            var size = 1 << vSpaceDimensions;

            return LeftEGpMap().GetMultivectorMapMatrix(size, size);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> RightEGpMapMatrix(int vSpaceDimensions)
        {
            if (vSpaceDimensions < VSpaceDimensions || vSpaceDimensions > 12)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            var size = 1 << vSpaceDimensions;

            return RightEGpMap().GetMultivectorMapMatrix(size, size);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64ComputedUnilinearMap LeftGpMap()
        {
            return Processor.UnilinearMap(
                id => Gp(Processor.KVectorTerm(id))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64ComputedUnilinearMap RightGpMap()
        {
            return Processor.UnilinearMap(
                id => Processor.KVectorTerm(id).Gp(this)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> LeftGpMapMatrix(int vSpaceDimensions)
        {
            if (vSpaceDimensions < VSpaceDimensions || vSpaceDimensions > 12)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            var size = 1 << vSpaceDimensions;

            return LeftGpMap().GetMultivectorMapMatrix(size, size);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> RightGpMapMatrix(int vSpaceDimensions)
        {
            if (vSpaceDimensions < VSpaceDimensions || vSpaceDimensions > 12)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            var size = 1 << vSpaceDimensions;

            return RightGpMap().GetMultivectorMapMatrix(size, size);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64ComputedUnilinearMap LeftECpMap()
        {
            return Processor.UnilinearMap(
                id => ECp(Processor.KVectorTerm(id))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64ComputedUnilinearMap RightECpMap()
        {
            return Processor.UnilinearMap(
                id => Processor.KVectorTerm(id).ECp(this)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> LeftECpMapMatrix(int vSpaceDimensions)
        {
            if (vSpaceDimensions < VSpaceDimensions || vSpaceDimensions > 12)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            var size = 1 << vSpaceDimensions;

            return LeftECpMap().GetMultivectorMapMatrix(size, size);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> RightECpMapMatrix(int vSpaceDimensions)
        {
            if (vSpaceDimensions < VSpaceDimensions || vSpaceDimensions > 12)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            var size = 1 << vSpaceDimensions;

            return RightECpMap().GetMultivectorMapMatrix(size, size);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64ComputedUnilinearMap LeftCpMap()
        {
            return Processor.UnilinearMap(
                id => Cp(Processor.KVectorTerm(id))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64ComputedUnilinearMap RightCpMap()
        {
            return Processor.UnilinearMap(
                id => Processor.KVectorTerm(id).Cp(this)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> LeftCpMapMatrix(int vSpaceDimensions)
        {
            if (vSpaceDimensions < VSpaceDimensions || vSpaceDimensions > 12)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            var size = 1 << vSpaceDimensions;

            return LeftCpMap().GetMultivectorMapMatrix(size, size);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> RightCpMapMatrix(int vSpaceDimensions)
        {
            if (vSpaceDimensions < VSpaceDimensions || vSpaceDimensions > 12)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            var size = 1 << vSpaceDimensions;

            return RightCpMap().GetMultivectorMapMatrix(size, size);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64ComputedUnilinearMap LeftEAcpMap()
        {
            return Processor.UnilinearMap(
                id => EAcp(Processor.KVectorTerm(id))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64ComputedUnilinearMap RightEAcpMap()
        {
            return Processor.UnilinearMap(
                id => Processor.KVectorTerm(id).EAcp(this)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> LeftEAcpMapMatrix(int vSpaceDimensions)
        {
            if (vSpaceDimensions < VSpaceDimensions || vSpaceDimensions > 12)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            var size = 1 << vSpaceDimensions;

            return LeftEAcpMap().GetMultivectorMapMatrix(size, size);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> RightEAcpMapMatrix(int vSpaceDimensions)
        {
            if (vSpaceDimensions < VSpaceDimensions || vSpaceDimensions > 12)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            var size = 1 << vSpaceDimensions;

            return RightEAcpMap().GetMultivectorMapMatrix(size, size);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64ComputedUnilinearMap LeftAcpMap()
        {
            return Processor.UnilinearMap(
                id => Acp(Processor.KVectorTerm(id))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64ComputedUnilinearMap RightAcpMap()
        {
            return Processor.UnilinearMap(
                id => Processor.KVectorTerm(id).Acp(this)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> LeftAcpMapMatrix(int vSpaceDimensions)
        {
            if (vSpaceDimensions < VSpaceDimensions || vSpaceDimensions > 12)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            var size = 1 << vSpaceDimensions;

            return LeftAcpMap().GetMultivectorMapMatrix(size, size);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> RightAcpMapMatrix(int vSpaceDimensions)
        {
            if (vSpaceDimensions < VSpaceDimensions || vSpaceDimensions > 12)
                throw new ArgumentOutOfRangeException(nameof(vSpaceDimensions));

            var size = 1 << vSpaceDimensions;

            return RightAcpMap().GetMultivectorMapMatrix(size, size);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEuclideanRotor()
        {
            return IsEven() && (EGp(Reverse()) - 1d).IsZero;
        }


        /// <summary>
        /// Create a pure rotor from its scalar and bivector parts
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64PureRotor ToEuclideanPureRotor()
        {
            return XGaFloat64PureRotor.Create(
                GetScalarPart().ScalarValue,
                GetBivectorPart()
            );
        }

        /// <summary>
        /// Create a pure rotor from its scalar and bivector parts
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64PureScalingRotor ToEuclideanPureScalingRotor()
        {
            return XGaFloat64PureScalingRotor.Create(this);
        }

        ///// <summary>
        ///// Create a pure Euclidean rotor that rotates the given source basis vector
        ///// into the target vector
        ///// </summary>
        ///// <param name="sourceBasisVectorIndex"></param>
        ///// <param name="assumeUnitVector"></param>
        ///// <returns></returns>
        //public XGaFloat64PureScalingRotor CreatePureScalingRotorFromBasisVector(int sourceBasisVectorIndex, bool assumeUnitVector = false)
        //{
        //    var processor = Metric;
        //    var k = sourceBasisVectorIndex;

        //    var v =
        //        assumeUnitVector
        //            ? this
        //            : Divide(ENorm());

        //    var ek = processor.BasisVector(k).ToKVector();

        //    var vk = v.Scalar(k);
        //    var vk1 = 1 + vk;

        //    return XGaFloat64PureScalingRotor.Create(
        //        (vk1 / 2).Sqrt() + v.Op(ek) / (2 * vk1).Sqrt()
        //    );
        //}

        /// <summary>
        /// Create a pure Euclidean rotor that rotates the given source basis vector
        /// into the target vector
        /// </summary>
        /// <param name="sourceBasisVectorIndex"></param>
        /// <param name="assumeUnitVector"></param>
        /// <returns></returns>
        public XGaFloat64PureScalingRotor CreatePureScalingRotorFromBasisVector(int sourceBasisVectorIndex, bool assumeUnitVector = false)
        {
            var processor = Metric;
            var k = sourceBasisVectorIndex;
            var vNorm =
                assumeUnitVector
                    ? 1d
                    : ENorm();

            var v =
                assumeUnitVector
                    ? this
                    : this / vNorm;

            var ek = processor.BasisVector(k).ToKVector();

            var vk = v.Scalar(k);
            var vk1 = 1 + vk;

            return XGaFloat64PureScalingRotor.Create(
                (vNorm * vk1 / 2).Sqrt() + v.Op(ek) * (vNorm / (vk1 * 2)).Sqrt()
            );
        }

    }

    public sealed partial class XGaFloat64Vector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64DiagonalOutermorphism ToDiagonalAutomorphism()
        {
            return new XGaFloat64DiagonalOutermorphism(this);
        }
    }

    public sealed partial class XGaFloat64Bivector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> LeftECpBivectorMapMatrix(int vSpaceDimensions)
        {
            return LeftECpMap().GetBivectorToBivectorMapMatrix(vSpaceDimensions);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Matrix<double> RightECpBivectorMapMatrix(int vSpaceDimensions)
        {
            return RightECpMap().GetBivectorToBivectorMapMatrix(vSpaceDimensions);
        }


        /// <summary>
        /// Create a pure rotor from a 2-blade, the signature of the blade
        /// is computed automatically using the given processor which must
        /// be of numerical type
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64PureScalingRotor ExpToPureScalingRotor()
        {
            return XGaFloat64PureScalingRotor.Create(BladeExp());
        }

        /// <summary>
        /// Create a pure rotor from a 2-blade, the signature of the blade
        /// is given by the user
        /// </summary>
        /// <param name="bladeSignatureSign"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64PureScalingRotor ExpToPureScalingRotor(IntegerSign bladeSignatureSign)
        {
            return XGaFloat64PureScalingRotor.Create(BladeExp(bladeSignatureSign));
        }

        /// <summary>
        /// Create a simple rotor from an angle and a 2-blade
        /// </summary>
        /// <param name="rotationAngle"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64PureRotor ToEuclideanPureRotor(LinFloat64Angle rotationAngle)
        {
            var (cosHalfAngle, sinHalfAngle) = 
                rotationAngle.HalfPolarAngle();

            var rotationBladeScalar =
                sinHalfAngle / (-ESpSquared()).Sqrt();

            return XGaFloat64PureRotor.Create(
                cosHalfAngle,
                rotationBladeScalar * this
            );
        }

        /// <summary>
        /// Create a simple rotor from an angle and a 2-blade
        /// </summary>
        /// <param name="rotationAngle"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64PureScalingRotor ToEuclideanPureScalingRotor(LinFloat64Angle rotationAngle)
        {
            var (sinHalfAngle, cosHalfAngle) =
                (0.5d * rotationAngle.RadiansValue).SinCos();

            var bivectorPart =
                sinHalfAngle / (-ESpSquared()).Sqrt() * this;

            return XGaFloat64PureScalingRotor.Create(
                cosHalfAngle + bivectorPart,
                cosHalfAngle - bivectorPart
            );
        }

    }
}
