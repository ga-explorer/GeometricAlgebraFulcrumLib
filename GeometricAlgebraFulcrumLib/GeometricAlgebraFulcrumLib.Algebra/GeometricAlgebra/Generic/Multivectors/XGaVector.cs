using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Frames;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Reflectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Subspaces;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

public sealed partial class XGaVector<T> :
    XGaKVector<T>
{
    private readonly IReadOnlyDictionary<IndexSet, T> _idScalarDictionary;


    public override string MultivectorClassName
        => "Generic Vector";

    public override int Count
        => _idScalarDictionary.Count;

    public override IEnumerable<int> KVectorGrades
    {
        get
        {
            if (!IsZero) yield return 1;
        }
    }

    public override int Grade
        => 1;

    public override bool IsZero
        => _idScalarDictionary.Count == 0;

    public override IEnumerable<IndexSet> Ids
        => _idScalarDictionary.Keys;

    public override IEnumerable<T> Scalars
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _idScalarDictionary.Values;
    }
        
    public IEnumerable<KeyValuePair<int, T>> IndexScalarPairs
        => _idScalarDictionary.Select(p => 
            new KeyValuePair<int, T>(p.Key.FirstIndex, p.Value)
        );

    public override IEnumerable<KeyValuePair<IndexSet, T>> IdScalarPairs
        => _idScalarDictionary;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaVector(XGaProcessor<T> processor)
        : base(processor)
    {
        _idScalarDictionary = new EmptyDictionary<IndexSet, T>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaVector(XGaProcessor<T> processor, KeyValuePair<IndexSet, T> basisScalarPair)
        : base(processor)
    {
        _idScalarDictionary =
            new SingleItemDictionary<IndexSet, T>(basisScalarPair);

        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaVector(XGaProcessor<T> processor, IReadOnlyDictionary<IndexSet, T> idScalarDictionary)
        : base(processor)
    {
        _idScalarDictionary = idScalarDictionary;

        Debug.Assert(IsValid());
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return _idScalarDictionary.IsValidVectorDictionary(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IReadOnlyDictionary<IndexSet, T> GetIdScalarDictionary()
    {
        return _idScalarDictionary;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsKey(IndexSet key)
    {
        return !IsZero && _idScalarDictionary.ContainsKey(key);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetTermScalarByIndex(int index)
    {
        var id = index.ToUnitIndexSet();

        return GetBasisBladeScalar(id);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> GetScalarPart()
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T> GetVectorPart()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> GetBivectorPart()
    {
        return Processor.BivectorZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> GetHigherKVectorPart(int grade)
    {
        return Processor.HigherKVectorZero(grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T> GetVectorPart(Func<int, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = 
            _idScalarDictionary.Where(term => 
                filterFunc(term.Key.FirstIndex)
            ).ToDictionary();

        return Processor.Vector(idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T> GetPart(Func<IndexSet, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Key)
            ).ToDictionary();

        return Processor.Vector(idScalarDictionary);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T> GetPart(Func<T, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Value)
            ).ToDictionary();

        return Processor.Vector(idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T> GetPart(Func<IndexSet, T, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Key, p.Value)
            ).ToDictionary();

        return Processor.Vector(idScalarDictionary);
    }


    public override IEnumerable<XGaBasisBlade> BasisBlades
        => _idScalarDictionary.Keys.Select(Processor.BasisBlade);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Scalar<T> Scalar()
    {
        return ScalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Scalar<T> GetBasisBladeScalar(IndexSet basisBladeId)
    {
        return _idScalarDictionary.TryGetValue(basisBladeId, out var scalar)
            ? ScalarProcessor.ScalarFromValue(scalar)
            : ScalarProcessor.Zero;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool TryGetScalarValue(out T scalar)
    {
        scalar = ScalarProcessor.ZeroValue;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool TryGetBasisBladeScalarValue(IndexSet basisBladeId, out T scalar)
    {
        if (basisBladeId.IsUnitSet && _idScalarDictionary.TryGetValue(basisBladeId, out scalar))
            return true;

        scalar = ScalarProcessor.ZeroValue;
        return false;
    }


    public override IEnumerable<KeyValuePair<XGaBasisBlade, T>> BasisScalarPairs
    {
        get
        {
            return _idScalarDictionary.Select(p =>
                new KeyValuePair<XGaBasisBlade, T>(
                    Processor.BasisBlade(p.Key),
                    p.Value
                )
            );
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Simplify()
    {
        return IsZero
            ? Processor.ScalarZero
            : this;
    }
        
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivector<T> ToGradedMultivector()
    {
        if (IsZero)
            return Processor.GradedMultivectorZero;

        var gradeKVectorDictionary = 
            new SingleItemDictionary<int, XGaKVector<T>>(1, this);

        return new XGaGradedMultivector<T>(
            Processor,
            gradeKVectorDictionary
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivector<T> ToUniformMultivector()
    {
        return ToComposer().GetUniformMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Vector Convert(XGaFloat64Processor metric)
    {
        if (IsZero)
            return metric.VectorZero;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, double>(
                    term.Key,
                    ScalarProcessor.ToFloat64(term.Value)
                )
            );

        return metric
            .CreateVectorComposer()
            .SetTerms(termList)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Vector Convert(XGaFloat64Processor metric, Func<T, double> scalarMapping)
    {
        if (IsZero)
            return metric.VectorZero;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, double>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return metric
            .CreateVectorComposer()
            .SetTerms(termList)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T2> Convert<T2>(XGaProcessor<T2> metric, Func<T, T2> scalarMapping)
    {
        if (IsZero)
            return metric.VectorZero;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T2>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return metric
            .CreateVectorComposer()
            .SetTerms(termList)
            .GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T> MapScalars(ScalarTransformer<T> transformer)
    {
        return MapScalars(transformer.MapScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector<T> ToLinVector()
    {
        var indexScalarDictionary = IdScalarPairs.ToDictionary(
            p => p.Key.FirstIndex,
            p => p.Value
        );

        return ScalarProcessor.CreateLinVector(indexScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T> ReflectOn(XGaKVector<T> subspace)
    {
        Debug.Assert(subspace.IsNearBlade());

        return subspace
            .Gp(this)
            .Gp(subspace.Inverse())
            .GetVectorPart();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T> ReflectDirectOnDirect(XGaKVector<T> subspace)
    {
        var mv1 = ReflectOn(subspace);

        return subspace.IsEven() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T> ReflectDirectOnDual(XGaKVector<T> subspace)
    {
        var mv1 = ReflectOn(subspace);
        
        return subspace.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T> ReflectDualOnDirect(XGaKVector<T> subspace, int vSpaceDimensions)
    {
        var mv1 = ReflectOn(subspace);

        var n = vSpaceDimensions - 1;

        return n.IsOdd() ? -mv1 : mv1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T> ReflectDualOnDual(XGaKVector<T> subspace)
    {
        return ReflectOn(subspace);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T> ProjectOn(XGaKVector<T> subspace, bool useSubspaceInverse = false)
    {
        Debug.Assert(subspace.IsNearBlade());
        
        var subspaceInverse = 
            useSubspaceInverse 
                ? subspace.PseudoInverse() 
                : subspace;

        return Fdp(subspaceInverse).Gp(subspace).GetVectorPart();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaPureReflector<T> ToPureReflector()
    {
        return XGaPureReflector<T>.Create(this);
    }

    
    /// <summary>
    /// Create a pure Euclidean rotor that rotates the given source vector into the target vector
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="targetVector"></param>
    /// <param name="assumeUnitVectors"></param>
    /// <returns></returns>
    public XGaPureRotor<T> CreatePureRotor(XGaVector<T> targetVector, bool assumeUnitVectors = false)
    {
        var cosAngle =
            assumeUnitVectors
                ? targetVector.ESp(this)
                : targetVector.ESp(this) / (targetVector.ENormSquared() * ENormSquared()).Sqrt();

        if (cosAngle.IsOne)
            return Processor.CreateIdentityRotor();
            
        var rotationBlade = 
            cosAngle.IsMinusOne
                ? throw new InvalidOperationException()//sourceVector.GetNormalVector().Op(sourceVector)
                : targetVector.Op(this);
                
        var unitRotationBlade =
            rotationBlade / (-rotationBlade.ESpSquared()).Sqrt();

        var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
        var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();
            
        var scalarPart = cosHalfAngle.ScalarValue;
        var bivectorPart = sinHalfAngle * unitRotationBlade;

        return XGaPureRotor<T>.Create(
            scalarPart,
            bivectorPart
        );
    }

    /// <summary>
    /// Create a pure Euclidean rotor that rotates the given source vector
    /// into the target vector
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="targetVector"></param>
    /// <returns></returns>
    public XGaPureScalingRotor<T> CreatePureScalingRotor(XGaVector<T> targetVector)
    {
        var uNorm = ENorm();
        var vNorm = targetVector.ENorm();
        var scalingFactor = (vNorm / uNorm).Sqrt().ScalarValue;
        var cosAngle = targetVector.ESp(this) / (uNorm * vNorm);

        if (cosAngle.IsOne)
            return XGaPureScalingRotor<T>.Create(Processor, scalingFactor);
            
        var rotationBlade = 
            cosAngle.IsMinusOne
                ? throw new InvalidOperationException()//sourceVector.GetNormalVector().Op(sourceVector)
                : targetVector.Op(this);
                
        var unitRotationBlade =
            rotationBlade / (-rotationBlade.ESpSquared()).Sqrt();

        var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
        var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

        Debug.Assert(scalingFactor != null, nameof(scalingFactor) + " != null");
        
        var scalarPart =
            scalingFactor * cosHalfAngle;

        var bivectorPart =
            scalingFactor * sinHalfAngle * unitRotationBlade;

        return XGaPureScalingRotor<T>.Create(
            scalarPart.ScalarValue,
            bivectorPart
        );
    }

    /// <summary>
    /// Create one rotor from the parametric family of pure rotors taking
    /// sourceVector to targetVector in 3D Euclidean space
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="targetVector"></param>
    /// <param name="angleTheta"></param>
    /// <returns></returns>
    public XGaPureRotor<T> CreateParametricPureRotor3D(XGaVector<T> targetVector, LinPolarAngle<T> angleTheta)
    {
        // Compute inverse of 3D pseudo-scalar = -e123
        var pseudoScalarInverse =
            Processor.PseudoScalarInverse(3);

        // Compute the smallest angle between source and target vectors
        var cosAngle0 =
            ESp(targetVector);

        // Define a rotor S with angle theta in the plane orthogonal to targetVector - sourceVector
        var rotorSBlade =
            (targetVector - this).EGp(
                pseudoScalarInverse
            ).GetBivectorPart();

        var rotorS = rotorSBlade.CreatePureRotor(angleTheta);

        // Define parametric 2-blade of rotation
        // The actual plane of rotation is made by rotating the plane containing
        // sourceVector and targetVector by angle theta in the plane orthogonal to
        // targetVector - sourceVector using rotor S
        var rotorBlade =
            rotorS.OmMap(targetVector.Op(this));

        var sinAngleThetaSquare = angleTheta.Sin().Square();

        // Define parametric angle of rotation
        var rotorAngle =
            (1 + 2 * (cosAngle0 - 1) / (2 - sinAngleThetaSquare * (cosAngle0 + 1))).ArcCos();

        // Math.Acos(1 + 2 * (cosAngle0 - 1) / (2 - Math.Pow(Math.Sin(angleTheta), 2) * (cosAngle0 + 1)));

        // Return the final rotor taking v1 into v2
        return rotorBlade.CreatePureRotor(rotorAngle);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaPureScalingRotor<T> CreateScaledParametricPureRotor3D(XGaVector<T> targetVector, LinPolarAngle<T> angleTheta, T scalingFactor)
    {
        return CreateParametricPureRotor3D(targetVector, angleTheta)
            .CreatePureScalingRotor(scalingFactor);
    }


    /// <summary>
    /// Create a pure Euclidean rotor that rotates the given source basis vector
    /// into the target vector
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sourceAxis"></param>
    /// <param name="assumeUnitVector"></param>
    /// <returns></returns>
    public XGaPureScalingRotor<T> CreatePureScalingRotorFromAxis(LinBasisVector sourceAxis, bool assumeUnitVector = false)
    {
        var k = sourceAxis.Index;
        var vNorm = assumeUnitVector
            ? Processor.ScalarProcessor.OneValue
            : ENorm().ScalarValue;

        var ek = Processor.VectorTerm(k);

        var vk1 = vNorm! + (sourceAxis.IsPositive ? Scalar(k) : -Scalar(k));
        var vOpAxis = sourceAxis.IsPositive ? Op(ek) : ek.Op(this);

        return XGaPureScalingRotor<T>.Create(
            (vk1 / 2).Sqrt().ScalarValue,
            vOpAxis / (vk1 * 2).Sqrt()
        );
    }

    /// <summary>
    /// Create a pure Euclidean rotor that rotates the given source basis vector
    /// into the target vector
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="targetAxis"></param>
    /// <param name="assumeUnitVector"></param>
    /// <returns></returns>
    public XGaPureScalingRotor<T> CreatePureScalingRotorToAxis(LinBasisVector targetAxis, bool assumeUnitVector = false)
    {
        var scalarProcessor = ScalarProcessor;

        var k = targetAxis.Index;
        var vNorm =
            assumeUnitVector
                ? scalarProcessor.OneValue
                : ENorm().ScalarValue;

        var vNorm2 =
            assumeUnitVector
                ? scalarProcessor.Two
                : scalarProcessor.Times(2, ENormSquared().ScalarValue);

        var ek = Processor.VectorTerm(k);

        Debug.Assert(vNorm != null, nameof(vNorm) + " != null");

        var vk1 = vNorm + (targetAxis.IsPositive ? Scalar(k) : -Scalar(k));
        var vOpAxis = targetAxis.IsPositive ? ek.Op(this) : Op(ek);

        return XGaPureScalingRotor<T>.Create(
            (vk1 / vNorm2).Sqrt().ScalarValue,
            vOpAxis / (vk1 * vNorm2).Sqrt()
        );
    }

    /// <summary>
    /// Create a pure Euclidean rotor that rotates the given source basis vector
    /// into the target vector
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sourceAxis"></param>
    /// <param name="assumeUnitVector"></param>
    /// <returns></returns>
    public XGaPureRotor<T> CreatePureRotorFromAxis(LinBasisVector sourceAxis, bool assumeUnitVector = false)
    {
        var k = sourceAxis.Index;

        var v =
            assumeUnitVector
                ? this
                : DivideByENorm();

        var ek = Processor.VectorTerm(k);

        var vk1 = 1 + (sourceAxis.IsPositive ? v.Scalar(k) : -v.Scalar(k));
        var vOpAxis = sourceAxis.IsPositive ? v.Op(ek) : ek.Op(v);

        return XGaPureRotor<T>.Create(
            (vk1 / 2).Sqrt().ScalarValue,
            vOpAxis / (vk1 * 2).Sqrt()
        );
    }

    /// <summary>
    /// Create a pure Euclidean rotor that rotates the given source vector
    /// into the target basis vector
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="targetAxis"></param>
    /// <param name="assumeUnitVector"></param>
    /// <returns></returns>
    public XGaPureRotor<T> CreatePureRotorToAxis(LinBasisVector targetAxis, bool assumeUnitVector = false)
    {
        var k = targetAxis.Index;

        var v =
            assumeUnitVector
                ? this
                : DivideByENorm();

        var ek = Processor.VectorTerm(k);

        var vk1 = 1 + (targetAxis.IsPositive ? v.Scalar(k) : -v.Scalar(k));
        var vOpAxis = targetAxis.IsPositive ? ek.Op(v) : v.Op(ek);

        return XGaPureRotor<T>.Create(
            (vk1 / 2).Sqrt().ScalarValue,
            vOpAxis / (vk1 * 2).Sqrt()
        );
    }

    /// <summary>
    /// Create a pure Euclidean rotor that rotates the given source vector
    /// into the target basis vector
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="targetAxis"></param>
    /// <param name="assumeUnitVector"></param>
    /// <returns></returns>
    public XGaPureRotor<T> CreatePureRotor(LinBasisVector targetAxis, bool assumeUnitVector = false)
    {
        var k = targetAxis.Index;

        var v =
            assumeUnitVector
                ? this
                : DivideByENorm();

        var ek = Processor.VectorTerm(k);

        var vk1 = 1 + (targetAxis.IsPositive ? v.Scalar(k) : -v.Scalar(k));
        var vOpAxis = targetAxis.IsPositive ? ek.Op(v) : v.Op(ek);

        return XGaPureRotor<T>.Create(
            (vk1 / 2).Sqrt().ScalarValue,
            vOpAxis / (vk1 * 2).Sqrt()
        );
    }
    
    public XGaPureRotorSequence<T> CreatePureRotorSequence(XGaVector<T> sourceVector2, XGaVector<T> targetVector1, XGaVector<T> targetVector2, bool assumeUnitVectors = false)
    {
        var rotor1 =
            CreatePureRotor(
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

        return XGaPureRotorSequence<T>.Create(rotor1, rotor2);
    }

    public XGaPureRotor<T> CreatePureRotor(XGaVector<T> inputVector2, XGaVector<T> rotatedVector1, XGaVector<T> rotatedVector2, int baseSpaceDimensions)
    {
        var inputFrame = XGaVectorFrameSpecs
            .CreateLinearlyIndependentSpecs()
            .CreateVectorFrame(
                this,
                inputVector2
            );

        var rotatedFrame = XGaVectorFrameSpecs
            .CreateLinearlyIndependentSpecs()
            .CreateVectorFrame(
                rotatedVector1,
                rotatedVector2
            );

        var rotor = XGaPureRotorSequence<T>.CreateFromEuclideanFrames(
            baseSpaceDimensions,
            inputFrame,
            rotatedFrame
        ).GetFinalRotor();

        var (scalar, bivector) = rotor.Multivector.GetScalarBivectorParts();

        return XGaPureRotor<T>.Create(scalar.ScalarValue, bivector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaDiagonalOutermorphism<T> CreateDiagonalAutomorphism()
    {
        return new XGaDiagonalOutermorphism<T>(this);
    }

    public XGaEuclideanScalingRotor2D<T> CreateEuclideanScalingRotor2D(XGaVector<T> targetVector)
    {
        Debug.Assert(
            ReferenceEquals(
                ScalarProcessor,
                targetVector.ScalarProcessor
            )
        );

        Debug.Assert(
            Metric.HasSameSignature(targetVector.Metric)
        );

        var u1 = Scalar(0);
        var u2 = Scalar(1);

        var v1 = targetVector.Scalar(0);
        var v2 = targetVector.Scalar(1);

        var vuDot = v1 * u1 + v2 * u2;
        var uNormSquared = u1 * u1 + u2 * u2;
        var vNormSquared = v1 * v1 + v2 * v2;

        var t1 = (vNormSquared / uNormSquared).Sqrt();
        var t2 = vuDot / uNormSquared;

        var vuWedgeScalar = (v1 * u2 - v2 * u1).Sign();

        var a0 = ((t1 + t2) / 2).Sqrt();
        var a12 = ((t1 - t2) / 2).Sqrt() * vuWedgeScalar;

        return XGaEuclideanScalingRotor2D<T>.Create(Processor, a0, a12);
    }

    public XGaEuclideanScalingRotorSquared2D<T> CreateEuclideanScalingRotorSquared2D(XGaVector<T> targetVector)
    {
        var u1 = Scalar(0);
        var u2 = Scalar(1);

        var v1 = targetVector.Scalar(0);
        var v2 = targetVector.Scalar(1);

        var uNormSquared = u1 * u1 + u2 * u2;

        var a0 = (v1 * u1 + v2 * u2) / uNormSquared;
        var a12 = (v1 * u2 - v2 * u1) / uNormSquared;

        return XGaEuclideanScalingRotorSquared2D<T>.Create(Processor, a0, a12);
    }
    
    public Pair<XGaVector<T>> ApplyGramSchmidt(XGaVector<T> v2, bool makeUnitVectors)
    {
        var vectorsList = new[] { this, v2 }.ApplyGramSchmidt(
            makeUnitVectors
        );

        var zeroVector = Processor.VectorZero;

        return vectorsList.Count switch
        {
            0 => new Pair<XGaVector<T>>(zeroVector, zeroVector),
            1 => new Pair<XGaVector<T>>(vectorsList[0], zeroVector),
            _ => new Pair<XGaVector<T>>(vectorsList[0], vectorsList[1])
        };
    }

    public Triplet<XGaVector<T>> ApplyGramSchmidt(XGaVector<T> v2, XGaVector<T> v3, bool makeUnitVectors)
    {
        var vectorsList = new[] { this, v2, v3 }.ApplyGramSchmidt(
            makeUnitVectors
        );

        var zeroVector = Processor.VectorZero;

        return vectorsList.Count switch
        {
            0 => new Triplet<XGaVector<T>>(zeroVector, zeroVector, zeroVector),
            1 => new Triplet<XGaVector<T>>(vectorsList[0], zeroVector, zeroVector),
            2 => new Triplet<XGaVector<T>>(vectorsList[0], vectorsList[1], zeroVector),
            _ => new Triplet<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2])
        };
    }

    public Quad<XGaVector<T>> ApplyGramSchmidt(XGaVector<T> v2, XGaVector<T> v3, XGaVector<T> v4, bool makeUnitVectors)
    {
        var vectorsList = new[] { this, v2, v3, v4 }.ApplyGramSchmidt(
            makeUnitVectors
        );

        var zeroVector = Processor.VectorZero;

        return vectorsList.Count switch
        {
            0 => new Quad<XGaVector<T>>(zeroVector, zeroVector, zeroVector, zeroVector),
            1 => new Quad<XGaVector<T>>(vectorsList[0], zeroVector, zeroVector, zeroVector),
            2 => new Quad<XGaVector<T>>(vectorsList[0], vectorsList[1], zeroVector, zeroVector),
            3 => new Quad<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], zeroVector),
            _ => new Quad<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3])
        };
    }

    public Quint<XGaVector<T>> ApplyGramSchmidt(XGaVector<T> v2, XGaVector<T> v3, XGaVector<T> v4, XGaVector<T> v5, bool makeUnitVectors)
    {
        var vectorsList = new[] { this, v2, v3, v4, v5 }.ApplyGramSchmidt(
            makeUnitVectors
        );

        var zeroVector = Processor.VectorZero;

        return vectorsList.Count switch
        {
            0 => new Quint<XGaVector<T>>(zeroVector, zeroVector, zeroVector, zeroVector, zeroVector),
            1 => new Quint<XGaVector<T>>(vectorsList[0], zeroVector, zeroVector, zeroVector, zeroVector),
            2 => new Quint<XGaVector<T>>(vectorsList[0], vectorsList[1], zeroVector, zeroVector, zeroVector),
            3 => new Quint<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], zeroVector, zeroVector),
            4 => new Quint<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], zeroVector),
            _ => new Quint<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], vectorsList[4])
        };
    }

    public Hexad<XGaVector<T>> ApplyGramSchmidt(XGaVector<T> v2, XGaVector<T> v3, XGaVector<T> v4, XGaVector<T> v5, XGaVector<T> v6, bool makeUnitVectors)
    {
        var vectorsList = new[] { this, v2, v3, v4, v5, v6 }.ApplyGramSchmidt(
            makeUnitVectors
        );

        var zeroVector = Processor.VectorZero;

        return vectorsList.Count switch
        {
            0 => new Hexad<XGaVector<T>>(zeroVector, zeroVector, zeroVector, zeroVector, zeroVector, zeroVector),
            1 => new Hexad<XGaVector<T>>(vectorsList[0], zeroVector, zeroVector, zeroVector, zeroVector, zeroVector),
            2 => new Hexad<XGaVector<T>>(vectorsList[0], vectorsList[1], zeroVector, zeroVector, zeroVector, zeroVector),
            3 => new Hexad<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], zeroVector, zeroVector, zeroVector),
            4 => new Hexad<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], zeroVector, zeroVector),
            5 => new Hexad<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], vectorsList[4], zeroVector),
            _ => new Hexad<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], vectorsList[4], vectorsList[5])
        };
    }
        
    public Pair<XGaVector<T>> ApplyGramSchmidtByProjections(XGaVector<T> v2, bool makeUnitVectors)
    {
        var vectorsList = new[] { this, v2 }.ApplyGramSchmidtByProjections(
            makeUnitVectors
        );

        var zeroVector = Processor.VectorZero;

        return vectorsList.Count switch
        {
            0 => new Pair<XGaVector<T>>(zeroVector, zeroVector),
            1 => new Pair<XGaVector<T>>(vectorsList[0], zeroVector),
            _ => new Pair<XGaVector<T>>(vectorsList[0], vectorsList[1])
        };
    }

    public Triplet<XGaVector<T>> ApplyGramSchmidtByProjections(XGaVector<T> v2, XGaVector<T> v3, bool makeUnitVectors)
    {
        var vectorsList = new[] { this, v2, v3 }.ApplyGramSchmidtByProjections(
            makeUnitVectors
        );

        var zeroVector = Processor.VectorZero;

        return vectorsList.Count switch
        {
            0 => new Triplet<XGaVector<T>>(zeroVector, zeroVector, zeroVector),
            1 => new Triplet<XGaVector<T>>(vectorsList[0], zeroVector, zeroVector),
            2 => new Triplet<XGaVector<T>>(vectorsList[0], vectorsList[1], zeroVector),
            _ => new Triplet<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2])
        };
    }

    public Quad<XGaVector<T>> ApplyGramSchmidtByProjections(XGaVector<T> v2, XGaVector<T> v3, XGaVector<T> v4, bool makeUnitVectors)
    {
        var vectorsList = new[] { this, v2, v3, v4 }.ApplyGramSchmidtByProjections(
            makeUnitVectors
        );

        var zeroVector = Processor.VectorZero;

        return vectorsList.Count switch
        {
            0 => new Quad<XGaVector<T>>(zeroVector, zeroVector, zeroVector, zeroVector),
            1 => new Quad<XGaVector<T>>(vectorsList[0], zeroVector, zeroVector, zeroVector),
            2 => new Quad<XGaVector<T>>(vectorsList[0], vectorsList[1], zeroVector, zeroVector),
            3 => new Quad<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], zeroVector),
            _ => new Quad<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3])
        };
    }

    public Quint<XGaVector<T>> ApplyGramSchmidtByProjections(XGaVector<T> v2, XGaVector<T> v3, XGaVector<T> v4, XGaVector<T> v5, bool makeUnitVectors)
    {
        var vectorsList = new[] { this, v2, v3, v4, v5 }.ApplyGramSchmidtByProjections(
            makeUnitVectors
        );

        var zeroVector = Processor.VectorZero;

        return vectorsList.Count switch
        {
            0 => new Quint<XGaVector<T>>(zeroVector, zeroVector, zeroVector, zeroVector, zeroVector),
            1 => new Quint<XGaVector<T>>(vectorsList[0], zeroVector, zeroVector, zeroVector, zeroVector),
            2 => new Quint<XGaVector<T>>(vectorsList[0], vectorsList[1], zeroVector, zeroVector, zeroVector),
            3 => new Quint<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], zeroVector, zeroVector),
            4 => new Quint<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], zeroVector),
            _ => new Quint<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], vectorsList[4])
        };
    }

    public Hexad<XGaVector<T>> ApplyGramSchmidtByProjections(XGaVector<T> v2, XGaVector<T> v3, XGaVector<T> v4, XGaVector<T> v5, XGaVector<T> v6, bool makeUnitVectors)
    {
        var vectorsList = new[] { this, v2, v3, v4, v5, v6 }.ApplyGramSchmidtByProjections(
            makeUnitVectors
        );

        var zeroVector = Processor.VectorZero;

        return vectorsList.Count switch
        {
            0 => new Hexad<XGaVector<T>>(zeroVector, zeroVector, zeroVector, zeroVector, zeroVector, zeroVector),
            1 => new Hexad<XGaVector<T>>(vectorsList[0], zeroVector, zeroVector, zeroVector, zeroVector, zeroVector),
            2 => new Hexad<XGaVector<T>>(vectorsList[0], vectorsList[1], zeroVector, zeroVector, zeroVector, zeroVector),
            3 => new Hexad<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], zeroVector, zeroVector, zeroVector),
            4 => new Hexad<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], zeroVector, zeroVector),
            5 => new Hexad<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], vectorsList[4], zeroVector),
            _ => new Hexad<XGaVector<T>>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], vectorsList[4], vectorsList[5])
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVectorFrameFixed<T> CreateBasisVectorFrameFixed(int vSpaceDimensions)
    {
        return Processor
            .CreateFreeFrameOfBasis(vSpaceDimensions)
            .CreateFixedFrame(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVectorFrameFixed<T> CreateFixedFrameOfScaledBasis(int vSpaceDimensions, T scalingFactor)
    {
        return Processor
            .CreateFreeFrameOfScaledBasis(vSpaceDimensions, scalingFactor)
            .CreateFixedFrame(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVectorFrameFixed<T> CreateFixedFrameOfSimplex(int vSpaceDimensions, T scalingFactor)
    {
        return Processor
            .CreateFreeFrameOfSimplex(vSpaceDimensions, scalingFactor)
            .CreateFixedFrame(this);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double[] VectorToFloat64Array1D()
    {
        var array = new double[VSpaceDimensions];

        foreach (var (id, scalar) in IdScalarPairs)
            array[id.FirstIndex] = ScalarProcessor.ToFloat64(scalar);

        return array;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T[] VectorToArray1D()
    {
        var array = ScalarProcessor.CreateArrayZero1D(VSpaceDimensions);

        foreach (var (id, scalar) in IdScalarPairs)
            array[id.FirstIndex] = scalar;

        return array;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T[] VectorToArray1D(int vectorSize)
    {
        if (vectorSize < VSpaceDimensions)
            throw new InvalidOperationException();

        var array = ScalarProcessor.CreateArrayZero1D(vectorSize);

        foreach (var (id, scalar) in IdScalarPairs)
            array[id.FirstIndex] = scalar;

        return array;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T[,] VectorToRowArray2D(int vectorSize)
    {
        if (vectorSize < VSpaceDimensions)
            throw new InvalidOperationException();

        var array = ScalarProcessor.CreateArrayZero2D(1, vectorSize);

        foreach (var (id, scalar) in IdScalarPairs)
            array[0, id.FirstIndex] = scalar;

        return array;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T[,] VectorToColumnArray2D(int vectorSize)
    {
        if (vectorSize < VSpaceDimensions)
            throw new InvalidOperationException();

        var array = ScalarProcessor.CreateArrayZero2D(vectorSize, 1);

        foreach (var (id, scalar) in IdScalarPairs)
            array[id.FirstIndex, 0] = scalar;

        return array;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D ToFloat64Vector2D()
    {
        return LinFloat64Vector2D.Create(
            Scalar(0).ToFloat64(),
            Scalar(1).ToFloat64()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D ToFloat64Vector3D()
    {
        return LinFloat64Vector3D.Create(
            Scalar(0).ToFloat64(),
            Scalar(1).ToFloat64(),
            Scalar(2).ToFloat64()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4D ToFloat64Vector4D()
    {
        return LinFloat64Vector4D.Create(
            Scalar(0).ToFloat64(),
            Scalar(1).ToFloat64(),
            Scalar(2).ToFloat64(),
            Scalar(3).ToFloat64()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector ToLinFloat64Vector()
    {
        return LinFloat64Vector.Create(
            VectorToFloat64Array1D()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinPolarAngle<T> GetEuclideanAngle(XGaVector<T> vector2, bool assumeUnitVectors = false)
    {
        var angle = ESp(vector2).Scalar();

        if (!assumeUnitVectors)
            angle /= ENorm() * vector2.ENorm();

        return angle.ArcCos();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> GetUnitBisector(XGaVector<T> vector2, bool assumeEqualNormVectors = false)
    {
        var v = assumeEqualNormVectors
            ? this + vector2
            : DivideByENorm() + vector2.DivideByENorm();

        return v.DivideByENorm();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> OmMapUsing(IXGaOutermorphism<T> om)
    {
        return om.OmMap(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> ProjectOnVector(XGaVector<T> subspace)
    {
        return ProjectOn(subspace.ToSubspace());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> ProjectOnBivector(XGaBivector<T> subspace)
    {
        return ProjectOn(subspace.ToSubspace());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> ProjectOnKVector(XGaKVector<T> subspace)
    {
        return ProjectOn(subspace.ToSubspace());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> ProjectOn(XGaSubspace<T> subspace)
    {
        return subspace.Project(this);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> RejectOnVector(XGaVector<T> subspace)
    {
        return this - ProjectOn(subspace.ToSubspace());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> RejectOnBivector(XGaBivector<T> subspace)
    {
        return this - ProjectOn(subspace.ToSubspace());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> RejectOnKVector(XGaKVector<T> subspace)
    {
        return this - ProjectOn(subspace.ToSubspace());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> RejectOn(XGaSubspace<T> subspace)
    {
        return this - subspace.Project(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaPureRotor<T> GetEuclideanRotorFromBasis(int index)
    {
        return Processor
            .VectorTerm(index)
            .CreatePureRotor(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaPureRotor<T> GetEuclideanRotorFrom(XGaVector<T> vector1)
    {
        return vector1.CreatePureRotor(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaPureRotor<T> GetEuclideanRotorFrom(XGaVector<T> vector1, bool assumeUnitVectors)
    {
        return vector1.CreatePureRotor(
            this,
            assumeUnitVectors
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaPureRotor<T> GetEuclideanRotorToBasis(int index)
    {
        return CreatePureRotor(
            Processor.VectorTerm(index)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaPureRotor<T> GetEuclideanRotorTo(XGaVector<T> vector2)
    {
        return CreatePureRotor(vector2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaPureRotor<T> GetEuclideanRotorTo(XGaVector<T> vector2, bool assumeUnitVectors)
    {
        return CreatePureRotor(
            vector2,
            assumeUnitVectors
        );
    }

    /// <summary>
    /// Find a Euclidean rotor from vector1 to its projection on subspace
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="subspace"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaPureRotor<T> GetEuclideanRotorTo(XGaSubspace<T> subspace)
    {
        return CreatePureRotor(
            subspace.Project(this)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        if (IsZero)
            return $"'{ScalarProcessor.Zero.ToText()}'<>";

        return BasisScalarPairs
            .OrderBy(p => p.Key.Id.Count)
            .ThenBy(p => p.Key.Id)
            .Select(p => $"'{ScalarProcessor.ToText(p.Value)}'{p.Key}")
            .Concatenate(" + ");
    }
}