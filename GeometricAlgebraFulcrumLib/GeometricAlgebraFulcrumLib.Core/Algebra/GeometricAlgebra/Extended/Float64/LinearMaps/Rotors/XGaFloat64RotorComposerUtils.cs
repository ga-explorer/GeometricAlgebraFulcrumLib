using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Frames;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.LinearMaps.Rotors;

public static class XGaFloat64RotorComposerUtils
{
    /// <summary>
    /// Create an identity rotor
    /// </summary>
    /// <param name="metric"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64PureRotor CreateIdentityRotor(this XGaFloat64Processor metric)
    {
        return XGaFloat64PureRotor.Create(
            1d,
            metric.BivectorZero
        );
    }

    /// <summary>
    /// Create an identity rotor
    /// </summary>
    /// <param name="metric"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ScaledPureRotor CreateScaledIdentityRotor(this XGaFloat64Processor metric)
    {
        return XGaFloat64ScaledPureRotor.Create(
            1d,
            metric.BivectorZero
        );
    }

    /// <summary>
    /// Create an identity rotor
    /// </summary>
    /// <param name="metric"></param>
    /// <param name="scalingFactor"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ScaledPureRotor CreateScaledIdentityRotor(this XGaFloat64Processor metric, double scalingFactor)
    {
        return XGaFloat64ScaledPureRotor.Create(
            scalingFactor,
            metric.BivectorZero
        );
    }

    /// <summary>
    /// Create a pure rotor from a 2-blade, the signature of the blade
    /// is computed automatically using the given processor which must
    /// be of numerical type
    /// </summary>
    /// <param name="blade"></param>
    /// <returns></returns>
    public static XGaFloat64PureRotor CreatePureRotor(this XGaFloat64Bivector blade)
    {
        var bladeSignature = blade.SpSquared();

        if (bladeSignature.IsNearZero())
            return XGaFloat64PureRotor.Create(
                1d,
                blade
            );

        if (bladeSignature < 0)
        {
            var alpha = (-bladeSignature).Sqrt();
            var scalar = alpha.Cos().ScalarValue;
            var bivector = alpha.Sin() / alpha * blade;

            return XGaFloat64PureRotor.Create(
                scalar,
                bivector
            );
        }
        else
        {
            var alpha = bladeSignature.Sqrt();
            var scalar = alpha.Cosh().ScalarValue;
            var bivector = alpha.Sinh() / alpha * blade;

            return XGaFloat64PureRotor.Create(
                scalar,
                bivector
            );
        }
    }

    /// <summary>
    /// Create a pure rotor from a 2-blade, the signature of the blade
    /// is given by the user
    /// </summary>
    /// <param name="blade"></param>
    /// <param name="bladeSignatureSign"></param>
    /// <returns></returns>
    public static XGaFloat64PureRotor CreatePureRotor(this XGaFloat64Bivector blade, IntegerSign bladeSignatureSign)
    {
        if (bladeSignatureSign.IsZero)
            return XGaFloat64PureRotor.Create(
                1d,
                blade
            );

        var bladeSignature = blade.SpSquared();

        if (bladeSignatureSign.IsNegative)
        {
            var alpha = (-bladeSignature).Sqrt();
            var scalar = alpha.Cos().ScalarValue;
            var bivector = alpha.Sin() / alpha * blade;

            return XGaFloat64PureRotor.Create(
                scalar,
                bivector
            );
        }
        else
        {
            var alpha = bladeSignature.Sqrt();
            var scalar = alpha.Cosh().ScalarValue;
            var bivector = alpha.Sinh() / alpha * blade;

            return XGaFloat64PureRotor.Create(
                scalar,
                bivector
            );
        }
    }

    /// <summary>
    /// Create a pure rotor from its scalar and bivector parts
    /// </summary>
    /// <param name="rotorMv"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64PureRotor CreatePureRotor(this XGaFloat64Multivector rotorMv)
    {
        return XGaFloat64PureRotor.Create(
            rotorMv.GetScalarPart().ScalarValue,
            rotorMv.GetBivectorPart()
        );
    }
        
    /// <summary>
    /// Create a pure Euclidean rotor that rotates the given source vector into the target vector
    /// </summary>
    /// <param name="sourceVector"></param>
    /// <param name="targetVector"></param>
    /// <param name="assumeUnitVectors"></param>
    /// <returns></returns>
    public static XGaFloat64PureRotor CreatePureRotor(this XGaFloat64Vector sourceVector, XGaFloat64Vector targetVector, bool assumeUnitVectors = false)
    {
        var metric = sourceVector.Processor;

        var cosAngle =
            assumeUnitVectors
                ? targetVector.ESp(sourceVector)
                : targetVector.ESp(sourceVector) / (targetVector.ENormSquared() * sourceVector.ENormSquared()).Sqrt();

        if (cosAngle.IsOne)
            return metric.CreateIdentityRotor();

        var rotationBlade = 
            cosAngle.IsMinusOne
                ? sourceVector.GetNormalVector().Op(sourceVector)
                : targetVector.Op(sourceVector);
                
        var unitRotationBlade =
            rotationBlade / (-rotationBlade.ESpSquared()).Sqrt();

        var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
        var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();
            
        var scalarPart = cosHalfAngle.ScalarValue;
        var bivectorPart = sinHalfAngle * unitRotationBlade;

        return XGaFloat64PureRotor.Create(
            scalarPart,
            bivectorPart
        );
    }

    /// <summary>
    /// Create a pure Euclidean rotor that rotates the given source vector
    /// into the target vector
    /// </summary>
    /// <param name="sourceVector"></param>
    /// <param name="targetVector"></param>
    /// <returns></returns>
    public static XGaFloat64ScaledPureRotor CreateScaledPureRotor(this XGaFloat64Vector sourceVector, XGaFloat64Vector targetVector)
    {
        var metric = sourceVector.Processor;

        var uNorm = sourceVector.ENorm();
        var vNorm = targetVector.ENorm();
        var scalingFactor = (vNorm / uNorm).Sqrt().ScalarValue;
        var cosAngle = targetVector.ESp(sourceVector) / (uNorm * vNorm);

        if (cosAngle.IsOne)
            return XGaFloat64ScaledPureRotor.Create(metric, scalingFactor);

        var rotationBlade = 
            cosAngle.IsMinusOne
                ? sourceVector.GetNormalVector().Op(sourceVector)
                : targetVector.Op(sourceVector);

        var unitRotationBlade =
            rotationBlade / (-rotationBlade.ESpSquared()).Sqrt();
            
        var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
        var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

        var scalarPart =
            scalingFactor * cosHalfAngle;

        var bivectorPart =
            scalingFactor * sinHalfAngle * unitRotationBlade;

        return XGaFloat64ScaledPureRotor.Create(
            scalarPart.ScalarValue,
            bivectorPart
        );
    }

    /// <summary>
    /// Create one rotor from the parametric family of pure rotors taking
    /// sourceVector to targetVector in 3D Euclidean space
    /// </summary>
    /// <param name="sourceVector"></param>
    /// <param name="targetVector"></param>
    /// <param name="angleTheta"></param>
    /// <returns></returns>
    public static XGaFloat64PureRotor CreateParametricPureRotor3D(this XGaFloat64Vector sourceVector, XGaFloat64Vector targetVector, LinFloat64Angle angleTheta)
    {
        var metric = sourceVector.Processor;

        // Compute inverse of 3D pseudo-scalar = -e123
        var pseudoScalarInverse =
            metric.PseudoScalarInverse(3);

        // Compute the smallest angle between source and target vectors
        var cosAngle0 =
            sourceVector.ESp(targetVector);

        // Define a rotor S with angle theta in the plane orthogonal to targetVector - sourceVector
        var rotorSBlade =
            (targetVector - sourceVector).EGp(
                pseudoScalarInverse
            ).GetBivectorPart();

        var rotorS = rotorSBlade.CreatePureRotor(angleTheta);

        // Define parametric 2-blade of rotation
        // The actual plane of rotation is made by rotating the plane containing
        // sourceVector and targetVector by angle theta in the plane orthogonal to
        // targetVector - sourceVector using rotor S
        var rotorBlade =
            rotorS.OmMap(targetVector.Op(sourceVector));

        var sinAngleThetaSquare = angleTheta.Sin().Square();

        // Define parametric angle of rotation
        var rotorAngle =
            (1 + 2 * (cosAngle0.ScalarValue - 1) / (2 - sinAngleThetaSquare * (cosAngle0.ScalarValue + 1))).ArcCos();

        // Math.Acos(1 + 2 * (cosAngle0 - 1) / (2 - Math.Pow(Math.Sin(angleTheta), 2) * (cosAngle0 + 1)));

        // Return the final rotor taking v1 into v2
        return rotorBlade.CreatePureRotor(rotorAngle);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ScaledPureRotor CreateScaledParametricPureRotor3D(this XGaFloat64Vector sourceVector, XGaFloat64Vector targetVector, LinFloat64Angle angleTheta, double scalingFactor)
    {
        return sourceVector
            .CreateParametricPureRotor3D(targetVector, angleTheta)
            .CreateScaledPureRotor(scalingFactor);
    }


    /// <summary>
    /// Create a pure Euclidean rotor that rotates the given source basis vector
    /// into the target vector
    /// </summary>
    /// <param name="sourceAxis"></param>
    /// <param name="targetVector"></param>
    /// <param name="assumeUnitVector"></param>
    /// <returns></returns>
    public static XGaFloat64ScaledPureRotor CreateScaledPureRotorFromAxis(this XGaFloat64Vector targetVector, LinSignedBasisVector sourceAxis, bool assumeUnitVector = false)
    {
        var metric = targetVector.Processor;
            
        var k = sourceAxis.Index;
        var vNorm = assumeUnitVector
            ? 1d
            : targetVector.ENorm().ScalarValue;

        var ek = metric.VectorTerm(k);

        var vk1 = vNorm + (sourceAxis.IsPositive ? targetVector[k] : -targetVector[k]);
        var vOpAxis = sourceAxis.IsPositive ? targetVector.Op(ek) : ek.Op(targetVector);

        return XGaFloat64ScaledPureRotor.Create(
            (vk1 / 2).Sqrt(),
            vOpAxis / (vk1 * 2).Sqrt()
        );
    }

    /// <summary>
    /// Create a pure Euclidean rotor that rotates the given source basis vector
    /// into the target vector
    /// </summary>
    /// <param name="targetAxis"></param>
    /// <param name="assumeUnitVector"></param>
    /// <param name="sourceVector"></param>
    /// <returns></returns>
    public static XGaFloat64ScaledPureRotor CreateScaledPureRotorToAxis(this XGaFloat64Vector sourceVector, LinSignedBasisVector targetAxis, bool assumeUnitVector = false)
    {
        var metric = sourceVector.Processor;

        var k = targetAxis.Index;
        var vNorm =
            assumeUnitVector
                ? 1d
                : sourceVector.ENorm().ScalarValue;

        var vNorm2 =
            assumeUnitVector
                ? 2d
                : 2d * sourceVector.ENormSquared().ScalarValue;

        var ek = metric.VectorTerm(k);

        var vk1 = vNorm + (targetAxis.IsPositive ? sourceVector[k] : -sourceVector[k]);
        var vOpAxis = targetAxis.IsPositive ? ek.Op(sourceVector) : sourceVector.Op(ek);

        return XGaFloat64ScaledPureRotor.Create(
            (vk1 / vNorm2).Sqrt(),
            vOpAxis / (vk1 * vNorm2).Sqrt()
        );
    }

    /// <summary>
    /// Create a pure Euclidean rotor that rotates the given source basis vector
    /// into the target vector
    /// </summary>
    /// <param name="sourceAxis"></param>
    /// <param name="targetVector"></param>
    /// <param name="assumeUnitVector"></param>
    /// <returns></returns>
    public static XGaFloat64PureRotor CreatePureRotorFromAxis(this XGaFloat64Vector targetVector, LinSignedBasisVector sourceAxis, bool assumeUnitVector = false)
    {
        var metric = targetVector.Processor;

        var k = sourceAxis.Index;

        var v =
            assumeUnitVector
                ? targetVector
                : targetVector.DivideByENorm();

        var ek = metric.VectorTerm(k);

        var vk1 = 1 + (sourceAxis.IsPositive ? v[k] : -v[k]);
        var vOpAxis = sourceAxis.IsPositive ? v.Op(ek) : ek.Op(v);

        return XGaFloat64PureRotor.Create(
            (vk1 / 2).Sqrt(),
            vOpAxis / (vk1 * 2).Sqrt()
        );
    }

    /// <summary>
    /// Create a pure Euclidean rotor that rotates the given source vector
    /// into the target basis vector
    /// </summary>
    /// <param name="targetAxis"></param>
    /// <param name="assumeUnitVector"></param>
    /// <param name="sourceVector"></param>
    /// <returns></returns>
    public static XGaFloat64PureRotor CreatePureRotorToAxis(this XGaFloat64Vector sourceVector, LinSignedBasisVector targetAxis, bool assumeUnitVector = false)
    {
        var metric = sourceVector.Processor;

        var k = targetAxis.Index;

        var v =
            assumeUnitVector
                ? sourceVector
                : sourceVector.DivideByENorm();

        var ek = metric.VectorTerm(k);

        var vk1 = 1 + (targetAxis.IsPositive ? v[k] : -v[k]);
        var vOpAxis = targetAxis.IsPositive ? ek.Op(v) : v.Op(ek);

        return XGaFloat64PureRotor.Create(
            (vk1 / 2).Sqrt(),
            vOpAxis / (vk1 * 2).Sqrt()
        );
    }

    /// <summary>
    /// Create a pure Euclidean rotor that rotates the given source basis vector
    /// into the target vector
    /// </summary>
    /// <param name="sourceAxis"></param>
    /// <param name="targetVector"></param>
    /// <param name="assumeUnitVector"></param>
    /// <returns></returns>
    public static XGaFloat64PureRotor CreatePureRotor(this LinSignedBasisVector sourceAxis, XGaFloat64Vector targetVector, bool assumeUnitVector = false)
    {
        var metric = targetVector.Processor;

        var k = sourceAxis.Index;

        var v =
            assumeUnitVector
                ? targetVector
                : targetVector.DivideByENorm();

        var ek = metric.VectorTerm(k);

        var vk1 = 1 + (sourceAxis.IsPositive ? v[k] : -v[k]);
        var vOpAxis = sourceAxis.IsPositive ? v.Op(ek) : ek.Op(v);

        return XGaFloat64PureRotor.Create(
            (vk1 / 2).Sqrt(),
            vOpAxis / (vk1 * 2).Sqrt()
        );
    }

    /// <summary>
    /// Create a pure Euclidean rotor that rotates the given source vector
    /// into the target basis vector
    /// </summary>
    /// <param name="targetAxis"></param>
    /// <param name="assumeUnitVector"></param>
    /// <param name="sourceVector"></param>
    /// <returns></returns>
    public static XGaFloat64PureRotor CreatePureRotor(this XGaFloat64Vector sourceVector, LinSignedBasisVector targetAxis, bool assumeUnitVector = false)
    {
        var metric = sourceVector.Processor;

        var k = targetAxis.Index;

        var v =
            assumeUnitVector
                ? sourceVector
                : sourceVector.DivideByENorm();

        var ek = metric.VectorTerm(k);

        var vk1 = 1 + (targetAxis.IsPositive ? v[k] : -v[k]);
        var vOpAxis = targetAxis.IsPositive ? ek.Op(v) : v.Op(ek);

        return XGaFloat64PureRotor.Create(
            (vk1 / 2).Sqrt(),
            vOpAxis / (vk1 * 2).Sqrt()
        );
    }

    /// <summary>
    /// Create a simple rotor from an angle and a 2-blade
    /// </summary>
    /// <param name="rotationAngle"></param>
    /// <param name="rotationBlade"></param>
    /// <returns></returns>
    public static XGaFloat64PureRotor CreatePureRotor(this XGaFloat64Bivector rotationBlade, LinFloat64Angle rotationAngle)
    {
        var (cosHalfAngle, sinHalfAngle) = rotationAngle.HalfPolarAngle();

        var rotationBladeScalar =
            sinHalfAngle / (-rotationBlade.ESp(rotationBlade)).Sqrt();

        return XGaFloat64PureRotor.Create(
            cosHalfAngle,
            rotationBladeScalar * rotationBlade
        );
    }

    public static XGaFloat64PureRotorsSequence CreatePureRotorSequence(this XGaFloat64Vector sourceVector1, XGaFloat64Vector sourceVector2, XGaFloat64Vector targetVector1, XGaFloat64Vector targetVector2, bool assumeUnitVectors = false)
    {
        var rotor1 =
            sourceVector1.CreatePureRotor(
                targetVector1,
                assumeUnitVectors
            );

        var rotor2 =
            rotor1.OmMap(sourceVector2).CreatePureRotor(
                targetVector2,
                assumeUnitVectors
            );

        //var rotor = 
        //    rotor2.Multivector.EGp(rotor1.Multivector);

        //var (scalar, bivector) = rotor.GetScalarBivectorParts();

        return XGaFloat64PureRotorsSequence.Create(rotor1, rotor2);
    }

    public static XGaFloat64PureRotor CreatePureRotor(this XGaFloat64Vector inputVector1, XGaFloat64Vector inputVector2, XGaFloat64Vector rotatedVector1, XGaFloat64Vector rotatedVector2, int baseSpaceDimensions)
    {
        var inputFrame = XGaFloat64VectorFrameSpecs
            .CreateLinearlyIndependentSpecs()
            .CreateVectorFrame(
                inputVector1,
                inputVector2
            );

        var rotatedFrame = XGaFloat64VectorFrameSpecs
            .CreateLinearlyIndependentSpecs()
            .CreateVectorFrame(
                rotatedVector1,
                rotatedVector2
            );

        var rotor = XGaFloat64PureRotorsSequence.CreateFromEuclideanFrames(
            baseSpaceDimensions,
            inputFrame,
            rotatedFrame
        ).GetFinalRotor();

        var (scalar, bivector) = rotor.Multivector.GetScalarBivectorParts();

        return XGaFloat64PureRotor.Create(scalar.ScalarValue, bivector);
    }

    /// <summary>
    /// Construct a rotor in the e_i-e_j plane with the given angle where i is less than j
    /// See: Computational Methods in Engineering by S.P. Venkateshan and Prasanna Swaminathan
    /// </summary>
    /// <param name="processor"></param>
    /// <param name="i"></param>
    /// <param name="j"></param>
    /// <param name="rotationAngle"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64PureRotor CreateGivensRotor(this XGaFloat64Processor processor, int i, int j, LinFloat64PolarAngle rotationAngle)
    {
        Debug.Assert(i >= 0 && j != i);
            
        var (cosHalfAngle, sinHalfAngle) = 
            rotationAngle.HalfPolarAngle();

        return XGaFloat64PureRotor.Create(
            cosHalfAngle,
            processor.BivectorTerm(i, j, sinHalfAngle)
        );
    }

    /// <summary>
    /// Construct a scaled rotor in the e_i-e_j plane with the given angle where i is less than j
    /// See: Computational Methods in Engineering by S.P. Venkateshan and Prasanna Swaminathan
    /// </summary>
    /// <param name="processor"></param>
    /// <param name="i"></param>
    /// <param name="j"></param>
    /// <param name="rotationAngle"></param>
    /// <param name="scalingFactor"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ScaledPureRotor CreateScaledGivensRotor(this XGaFloat64Processor processor, int i, int j, LinFloat64PolarAngle rotationAngle, double scalingFactor)
    {
        Debug.Assert(i >= 0 && j != i);
            
        var (cosHalfAngle, sinHalfAngle) = 
            rotationAngle.HalfPolarAngle();

        var s = scalingFactor.Sqrt();
        var scalarPart = s * cosHalfAngle;
        var bivectorPart = s * processor.BivectorTerm(i, j, sinHalfAngle);

        return XGaFloat64ScaledPureRotor.Create(scalarPart, bivectorPart);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ScaledPureRotor CreateScaledPureRotor(this XGaFloat64PureRotor rotor, double scalingFactor)
    {
        var s = scalingFactor.Sqrt();
        var scalarPart = s * rotor.Multivector.Scalar();
        var bivectorPart = s * rotor.Multivector.GetBivectorPart();

        return XGaFloat64ScaledPureRotor.Create(
            scalarPart,
            bivectorPart
        );
    }

    /// <summary>
    /// Create a scaled pure rotor in 2D Euclidean space directly using its scalar components
    /// </summary>
    /// <param name="metric"></param>
    /// <param name="r0"></param>
    /// <param name="r12"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ScaledPureRotor CreateScaledPureRotor2D(this XGaFloat64Processor metric, float r0, float r12)
    {
        return XGaFloat64ScaledPureRotor.Create(
            metric
                .CreateComposer()
                .SetTerm(0, r0)
                .SetTerm(3, r12)
                .GetMultivector()
        );
    }

    /// <summary>
    /// Create a scaled pure rotor in 3D Euclidean space directly using its scalar components
    /// </summary>
    /// <param name="metric"></param>
    /// <param name="r0"></param>
    /// <param name="r12"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ScaledPureRotor CreateScaledPureRotor2D(this XGaFloat64Processor metric, double r0, double r12)
    {
        return XGaFloat64ScaledPureRotor.Create(
            metric
                .CreateComposer()
                .SetTerm(0, r0)
                .SetTerm(3, r12)
                .GetMultivector()
        );
    }
        
    /// <summary>
    /// Create a scaled pure rotor in 3D Euclidean space directly using its scalar components
    /// </summary>
    /// <param name="metric"></param>
    /// <param name="r0"></param>
    /// <param name="r12"></param>
    /// <param name="r13"></param>
    /// <param name="r23"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ScaledPureRotor CreateScaledPureRotor3D(this XGaFloat64Processor metric, float r0, float r12, float r13, float r23)
    {
        return XGaFloat64ScaledPureRotor.Create(
            metric
                .CreateComposer()
                .SetTerm(0, r0)
                .SetTerm(3, r12)
                .SetTerm(5, r13)
                .SetTerm(6, r23)
                .GetMultivector()
        );
    }

    /// <summary>
    /// Create a scaled pure rotor in 3D Euclidean space directly using its scalar components
    /// </summary>
    /// <param name="metric"></param>
    /// <param name="r0"></param>
    /// <param name="r12"></param>
    /// <param name="r13"></param>
    /// <param name="r23"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64ScaledPureRotor CreateScaledPureRotor3D(this XGaFloat64Processor metric, double r0, double r12, double r13, double r23)
    {
        return XGaFloat64ScaledPureRotor.Create(
            metric
                .CreateComposer()
                .SetTerm(0, r0)
                .SetTerm(3, r12)
                .SetTerm(5, r13)
                .SetTerm(6, r23)
                .GetMultivector()
        );
    }
        

    /// <summary>
    /// See paper "Calculation of Elements of Spin Groups Using Method
    /// of Averaging in Clifford’s Geometric Algebra"
    /// </summary>
    /// <param name="sourceFrame"></param>
    /// <param name="targetFrame"></param>
    /// <returns></returns>
    public static XGaFloat64Rotor CreateRotorToFrame(this XGaFloat64BasisVectorFrame sourceFrame, XGaFloat64BasisVectorFrame targetFrame)
    {
        var metric = sourceFrame.Processor;

        var mvFrame1 = sourceFrame.CreateBasisKVectorFrame().MapAsBasisUsing(mv => mv.Inverse());
        var mvFrame2 = targetFrame.CreateBasisKVectorFrame();

        var rotorMultivector =
            mvFrame2
                .Zip(mvFrame1)
                .Select(vectorPair => vectorPair.First.Gp(vectorPair.Second))
                .Aggregate(
                    (XGaFloat64Multivector) metric.MultivectorZero,
                    (mv1, mv2) => mv1.Add(mv2)
                );

        rotorMultivector /= rotorMultivector.Sp(rotorMultivector.Reverse()).Sqrt().ScalarValue;

        return XGaFloat64Rotor.Create(rotorMultivector);
    }
}