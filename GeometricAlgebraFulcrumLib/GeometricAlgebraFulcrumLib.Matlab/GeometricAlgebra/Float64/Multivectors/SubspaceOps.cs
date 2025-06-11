using System;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Frames;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Reflectors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Subspaces;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors
{
    public abstract partial class XGaFloat64Multivector
    {
        
        public XGaFloat64Multivector ProjectOn(XGaFloat64KVector subspace, bool useSubspaceInverse = false)
        {
            Debug.Assert(subspace.IsNearBlade());

            var subspaceInverse =
                useSubspaceInverse
                    ? subspace.PseudoInverse()
                    : subspace;

            return Fdp(subspaceInverse).Gp(subspace);
        }


        
        public XGaFloat64Multivector ReflectOn(XGaFloat64KVector subspace)
        {
            Debug.Assert(subspace.IsNearBlade());

            return subspace
                .Gp(this)
                .Gp(subspace.Inverse());
        }

        
        public XGaFloat64Multivector ReflectDirectOnDirect(XGaFloat64KVector subspace)
        {
            return GetKVectorParts()
                .Select(kv => kv.ReflectDirectOnDirect(subspace))
                .Aggregate(
                    (XGaFloat64Multivector)Processor.ScalarZero,
                    (a, b) => a.Add(b)
                );
        }

        
        public XGaFloat64Multivector ReflectDirectOnDual(XGaFloat64KVector subspace)
        {
            return GetKVectorParts()
                .Select(kv => kv.ReflectDirectOnDual(subspace))
                .Aggregate(
                    (XGaFloat64Multivector)Processor.ScalarZero,
                    (a, b) => a.Add(b)
                );
        }

        
        public XGaFloat64Multivector ReflectDualOnDirect(XGaFloat64KVector subspace, int vSpaceDimensions)
        {
            return GetKVectorParts()
                .Select(kv => kv.ReflectDualOnDirect(subspace, vSpaceDimensions))
                .Aggregate(
                    (XGaFloat64Multivector)Processor.ScalarZero,
                    (a, b) => a.Add(b)
                );
        }

        
        public XGaFloat64Multivector ReflectDualOnDual(XGaFloat64KVector subspace)
        {
            return GetKVectorParts()
                .Select(kv => kv.ReflectDualOnDual(subspace))
                .Aggregate(
                    (XGaFloat64Multivector)Processor.ScalarZero,
                    (a, b) => a.Add(b)
                );
        }

    }

    public abstract partial class XGaFloat64KVector
    {

        
        public XGaFloat64Subspace ToSubspace()
        {
            return new XGaFloat64Subspace(this);
        }

        
        public XGaFloat64Subspace DualToSubspace(int vSpaceDimensions)
        {
            return new XGaFloat64Subspace(
                Dual(vSpaceDimensions)
            );
        }

        
        public XGaFloat64Subspace UnDualToSubspace(int vSpaceDimensions)
        {
            return new XGaFloat64Subspace(
                UnDual(vSpaceDimensions)
            );
        }


        
        public new XGaFloat64KVector ReflectOn(XGaFloat64KVector subspace)
        {
            Debug.Assert(subspace.IsNearBlade());

            return subspace
                .Gp(this)
                .Gp(subspace.Inverse())
                .GetKVectorPart(Grade);
        }

        
        public new XGaFloat64KVector ReflectDirectOnDirect(XGaFloat64KVector subspace)
        {
            var mv1 = ReflectOn(subspace);

            var n = Grade * (subspace.Grade + 1);

            return n.IsOdd() ? -mv1 : mv1;
        }

        
        public new XGaFloat64KVector ReflectDirectOnDual(XGaFloat64KVector subspace)
        {
            var mv1 = ReflectOn(subspace);

            var n = Grade * subspace.Grade;

            return n.IsOdd() ? -mv1 : mv1;
        }

        
        public new XGaFloat64KVector ReflectDualOnDirect(XGaFloat64KVector subspace, int vSpaceDimensions)
        {
            var mv1 = ReflectOn(subspace);

            var n = (Grade + 1) * (subspace.Grade + 1) + vSpaceDimensions - 1;

            return n.IsOdd() ? -mv1 : mv1;
        }

        
        public new XGaFloat64KVector ReflectDualOnDual(XGaFloat64KVector subspace)
        {
            var mv1 = ReflectOn(subspace);

            var n = (Grade + 1) * subspace.Grade;

            return n.IsOdd() ? -mv1 : mv1;
        }


        
        public new XGaFloat64KVector ProjectOn(XGaFloat64KVector subspace, bool useSubspaceInverse = false)
        {
            Debug.Assert(subspace.IsNearBlade());

            var subspaceInverse =
                useSubspaceInverse
                    ? subspace.PseudoInverse()
                    : subspace;

            return Fdp(subspaceInverse).Gp(subspace).GetKVectorPart(Grade);
        }

    }

    public sealed partial class XGaFloat64Scalar
    {

        
        public new XGaFloat64Scalar ReflectOn(XGaFloat64KVector subspace)
        {
            Debug.Assert(subspace.IsNearBlade());

            return this;
        }

        
        public new XGaFloat64Scalar ReflectDirectOnDirect(XGaFloat64KVector subspace)
        {
            return this;
        }

        
        public new XGaFloat64Scalar ReflectDirectOnDual(XGaFloat64KVector subspace)
        {
            return this;
        }

        
        public new XGaFloat64Scalar ReflectDualOnDirect(XGaFloat64KVector subspace, int vSpaceDimensions)
        {
            var n = subspace.Grade + vSpaceDimensions;

            return n.IsOdd() ? -this : this;
        }

        
        public new XGaFloat64Scalar ReflectDualOnDual(XGaFloat64KVector subspace)
        {
            return subspace.IsOdd() ? -this : this;
        }


        
        public new XGaFloat64Scalar ProjectOn(XGaFloat64KVector subspace, bool useSubspaceInverse = false)
        {
            Debug.Assert(subspace.IsNearBlade());

            var subspaceInverse =
                useSubspaceInverse
                    ? subspace.PseudoInverse()
                    : subspace;

            return Fdp(subspaceInverse).Gp(subspace).GetScalarPart();
        }
    }

    public sealed partial class XGaFloat64Vector
    {

        
        public XGaFloat64VectorFrameFixed CreateBasisVectorFrameFixed(int vSpaceDimensions)
        {
            return Processor
                .CreateFreeFrameOfBasis(vSpaceDimensions)
                .CreateFixedFrame(this);
        }

        
        public XGaFloat64VectorFrameFixed CreateFixedFrameOfScaledBasis(int vSpaceDimensions, double scalingFactor)
        {
            return Processor
                .CreateFreeFrameOfScaledBasis(vSpaceDimensions, scalingFactor)
                .CreateFixedFrame(this);
        }

        
        public XGaFloat64VectorFrameFixed CreateFixedFrameOfSimplex(int vSpaceDimensions, double scalingFactor)
        {
            return Processor
                .CreateFreeFrameOfSimplex(vSpaceDimensions, scalingFactor)
                .CreateFixedFrame(this);
        }


        
        public XGaFloat64Vector ProjectOnVector(XGaFloat64Vector subspace)
        {
            return ProjectOn(subspace.ToSubspace());
        }

        
        public XGaFloat64Vector ProjectOnBivector(XGaFloat64Bivector subspace)
        {
            return ProjectOn(subspace.ToSubspace());
        }

        
        public XGaFloat64Vector ProjectOnKVector(XGaFloat64KVector subspace)
        {
            return ProjectOn(subspace.ToSubspace());
        }

        
        public XGaFloat64Vector ProjectOn(XGaFloat64Subspace subspace)
        {
            return subspace.Project(this);
        }

        
        public XGaFloat64Vector RejectOnVector(XGaFloat64Vector subspace)
        {
            return this - ProjectOn(subspace.ToSubspace());
        }

        
        public XGaFloat64Vector RejectOnBivector(XGaFloat64Bivector subspace)
        {
            return this - ProjectOn(subspace.ToSubspace());
        }

        
        public XGaFloat64Vector RejectOnKVector(XGaFloat64KVector subspace)
        {
            return this - ProjectOn(subspace.ToSubspace());
        }

        
        public XGaFloat64Vector RejectOn(XGaFloat64Subspace subspace)
        {
            return this - subspace.Project(this);
        }


        public Pair<XGaFloat64Vector> ApplyGramSchmidt(XGaFloat64Vector v2, bool makeUnitVectors)
        {
            var vectorsList = new[] { this, v2 }.ApplyGramSchmidt(
                makeUnitVectors
            );

            var zeroVector = Processor.VectorZero;

            return vectorsList.Count switch
            {
                0 => new Pair<XGaFloat64Vector>(zeroVector, zeroVector),
                1 => new Pair<XGaFloat64Vector>(vectorsList[0], zeroVector),
                _ => new Pair<XGaFloat64Vector>(vectorsList[0], vectorsList[1])
            };
        }

        public Triplet<XGaFloat64Vector> ApplyGramSchmidt(XGaFloat64Vector v2, XGaFloat64Vector v3, bool makeUnitVectors)
        {
            var vectorsList = new[] { this, v2, v3 }.ApplyGramSchmidt(
                makeUnitVectors
            );

            var zeroVector = Processor.VectorZero;

            return vectorsList.Count switch
            {
                0 => new Triplet<XGaFloat64Vector>(zeroVector, zeroVector, zeroVector),
                1 => new Triplet<XGaFloat64Vector>(vectorsList[0], zeroVector, zeroVector),
                2 => new Triplet<XGaFloat64Vector>(vectorsList[0], vectorsList[1], zeroVector),
                _ => new Triplet<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2])
            };
        }

        public Quad<XGaFloat64Vector> ApplyGramSchmidt(XGaFloat64Vector v2, XGaFloat64Vector v3, XGaFloat64Vector v4, bool makeUnitVectors)
        {
            var vectorsList = new[] { this, v2, v3, v4 }.ApplyGramSchmidt(
                makeUnitVectors
            );

            var zeroVector = Processor.VectorZero;

            return vectorsList.Count switch
            {
                0 => new Quad<XGaFloat64Vector>(zeroVector, zeroVector, zeroVector, zeroVector),
                1 => new Quad<XGaFloat64Vector>(vectorsList[0], zeroVector, zeroVector, zeroVector),
                2 => new Quad<XGaFloat64Vector>(vectorsList[0], vectorsList[1], zeroVector, zeroVector),
                3 => new Quad<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], zeroVector),
                _ => new Quad<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3])
            };
        }

        public Quint<XGaFloat64Vector> ApplyGramSchmidt(XGaFloat64Vector v2, XGaFloat64Vector v3, XGaFloat64Vector v4, XGaFloat64Vector v5, bool makeUnitVectors)
        {
            var vectorsList = new[] { this, v2, v3, v4, v5 }.ApplyGramSchmidt(
                makeUnitVectors
            );

            var zeroVector = Processor.VectorZero;

            return vectorsList.Count switch
            {
                0 => new Quint<XGaFloat64Vector>(zeroVector, zeroVector, zeroVector, zeroVector, zeroVector),
                1 => new Quint<XGaFloat64Vector>(vectorsList[0], zeroVector, zeroVector, zeroVector, zeroVector),
                2 => new Quint<XGaFloat64Vector>(vectorsList[0], vectorsList[1], zeroVector, zeroVector, zeroVector),
                3 => new Quint<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], zeroVector, zeroVector),
                4 => new Quint<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], zeroVector),
                _ => new Quint<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], vectorsList[4])
            };
        }

        public Hexad<XGaFloat64Vector> ApplyGramSchmidt(XGaFloat64Vector v2, XGaFloat64Vector v3, XGaFloat64Vector v4, XGaFloat64Vector v5, XGaFloat64Vector v6, bool makeUnitVectors)
        {
            var vectorsList = new[] { this, v2, v3, v4, v5, v6 }.ApplyGramSchmidt(
                makeUnitVectors
            );

            var zeroVector = Processor.VectorZero;

            return vectorsList.Count switch
            {
                0 => new Hexad<XGaFloat64Vector>(zeroVector, zeroVector, zeroVector, zeroVector, zeroVector, zeroVector),
                1 => new Hexad<XGaFloat64Vector>(vectorsList[0], zeroVector, zeroVector, zeroVector, zeroVector, zeroVector),
                2 => new Hexad<XGaFloat64Vector>(vectorsList[0], vectorsList[1], zeroVector, zeroVector, zeroVector, zeroVector),
                3 => new Hexad<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], zeroVector, zeroVector, zeroVector),
                4 => new Hexad<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], zeroVector, zeroVector),
                5 => new Hexad<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], vectorsList[4], zeroVector),
                _ => new Hexad<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], vectorsList[4], vectorsList[5])
            };
        }

        public Pair<XGaFloat64Vector> ApplyGramSchmidtByProjections(XGaFloat64Vector v2, bool makeUnitVectors)
        {
            var vectorsList = new[] { this, v2 }.ApplyGramSchmidtByProjections(
                makeUnitVectors
            );

            var zeroVector = Processor.VectorZero;

            return vectorsList.Count switch
            {
                0 => new Pair<XGaFloat64Vector>(zeroVector, zeroVector),
                1 => new Pair<XGaFloat64Vector>(vectorsList[0], zeroVector),
                _ => new Pair<XGaFloat64Vector>(vectorsList[0], vectorsList[1])
            };
        }

        public Triplet<XGaFloat64Vector> ApplyGramSchmidtByProjections(XGaFloat64Vector v2, XGaFloat64Vector v3, bool makeUnitVectors)
        {
            var vectorsList = new[] { this, v2, v3 }.ApplyGramSchmidtByProjections(
                makeUnitVectors
            );

            var zeroVector = Processor.VectorZero;

            return vectorsList.Count switch
            {
                0 => new Triplet<XGaFloat64Vector>(zeroVector, zeroVector, zeroVector),
                1 => new Triplet<XGaFloat64Vector>(vectorsList[0], zeroVector, zeroVector),
                2 => new Triplet<XGaFloat64Vector>(vectorsList[0], vectorsList[1], zeroVector),
                _ => new Triplet<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2])
            };
        }

        public Quad<XGaFloat64Vector> ApplyGramSchmidtByProjections(XGaFloat64Vector v2, XGaFloat64Vector v3, XGaFloat64Vector v4, bool makeUnitVectors)
        {
            var vectorsList = new[] { this, v2, v3, v4 }.ApplyGramSchmidtByProjections(
                makeUnitVectors
            );

            var zeroVector = Processor.VectorZero;

            return vectorsList.Count switch
            {
                0 => new Quad<XGaFloat64Vector>(zeroVector, zeroVector, zeroVector, zeroVector),
                1 => new Quad<XGaFloat64Vector>(vectorsList[0], zeroVector, zeroVector, zeroVector),
                2 => new Quad<XGaFloat64Vector>(vectorsList[0], vectorsList[1], zeroVector, zeroVector),
                3 => new Quad<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], zeroVector),
                _ => new Quad<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3])
            };
        }

        public Quint<XGaFloat64Vector> ApplyGramSchmidtByProjections(XGaFloat64Vector v2, XGaFloat64Vector v3, XGaFloat64Vector v4, XGaFloat64Vector v5, bool makeUnitVectors)
        {
            var vectorsList = new[] { this, v2, v3, v4, v5 }.ApplyGramSchmidtByProjections(
                makeUnitVectors
            );

            var zeroVector = Processor.VectorZero;

            return vectorsList.Count switch
            {
                0 => new Quint<XGaFloat64Vector>(zeroVector, zeroVector, zeroVector, zeroVector, zeroVector),
                1 => new Quint<XGaFloat64Vector>(vectorsList[0], zeroVector, zeroVector, zeroVector, zeroVector),
                2 => new Quint<XGaFloat64Vector>(vectorsList[0], vectorsList[1], zeroVector, zeroVector, zeroVector),
                3 => new Quint<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], zeroVector, zeroVector),
                4 => new Quint<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], zeroVector),
                _ => new Quint<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], vectorsList[4])
            };
        }

        public Hexad<XGaFloat64Vector> ApplyGramSchmidtByProjections(XGaFloat64Vector v2, XGaFloat64Vector v3, XGaFloat64Vector v4, XGaFloat64Vector v5, XGaFloat64Vector v6, bool makeUnitVectors)
        {
            var vectorsList = new[] { this, v2, v3, v4, v5, v6 }.ApplyGramSchmidtByProjections(
                makeUnitVectors
            );

            var zeroVector = Processor.VectorZero;

            return vectorsList.Count switch
            {
                0 => new Hexad<XGaFloat64Vector>(zeroVector, zeroVector, zeroVector, zeroVector, zeroVector, zeroVector),
                1 => new Hexad<XGaFloat64Vector>(vectorsList[0], zeroVector, zeroVector, zeroVector, zeroVector, zeroVector),
                2 => new Hexad<XGaFloat64Vector>(vectorsList[0], vectorsList[1], zeroVector, zeroVector, zeroVector, zeroVector),
                3 => new Hexad<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], zeroVector, zeroVector, zeroVector),
                4 => new Hexad<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], zeroVector, zeroVector),
                5 => new Hexad<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], vectorsList[4], zeroVector),
                _ => new Hexad<XGaFloat64Vector>(vectorsList[0], vectorsList[1], vectorsList[2], vectorsList[3], vectorsList[4], vectorsList[5])
            };
        }


        
        public new XGaFloat64Vector ReflectOn(XGaFloat64KVector subspace)
        {
            Debug.Assert(subspace.IsNearBlade());

            return subspace
                .Gp(this)
                .Gp(subspace.Inverse())
                .GetVectorPart();
        }

        
        public new XGaFloat64Vector ReflectDirectOnDirect(XGaFloat64KVector subspace)
        {
            var mv1 = ReflectOn(subspace);

            return subspace.IsEven() ? -mv1 : mv1;
        }

        
        public new XGaFloat64Vector ReflectDirectOnDual(XGaFloat64KVector subspace)
        {
            var mv1 = ReflectOn(subspace);

            return subspace.IsOdd() ? -mv1 : mv1;
        }

        
        public new XGaFloat64Vector ReflectDualOnDirect(XGaFloat64KVector subspace, int vSpaceDimensions)
        {
            var mv1 = ReflectOn(subspace);

            var n = vSpaceDimensions - 1;

            return n.IsOdd() ? -mv1 : mv1;
        }

        
        public new XGaFloat64Vector ReflectDualOnDual(XGaFloat64KVector subspace)
        {
            return ReflectOn(subspace);
        }


        
        public new XGaFloat64Vector ProjectOn(XGaFloat64KVector subspace, bool useSubspaceInverse = false)
        {
            Debug.Assert(subspace.IsNearBlade());

            var subspaceInverse =
                useSubspaceInverse
                    ? subspace.PseudoInverse()
                    : subspace;

            return Fdp(subspaceInverse).Gp(subspace).GetVectorPart();
        }



        
        public XGaFloat64Vector GetNormalVector()
        {
            return ToLinVector().GetUnitNormal().ToXGaFloat64Vector(Processor);
        }

        public XGaFloat64Vector GetUnitNormalVector()
        {
            var (minValueId, minValue) =
                GetMinScalarMagnitudeIdScalar();

            var composer = ToComposer();

            var sum = 0d;
            foreach (var (id, scalar) in IdScalarTuples)
            {
                if (id == minValueId) continue;

                var signature = Processor.Signature(id);

                if (signature.IsPositive)
                {
                    sum += scalar;
                    composer.SetTerm(id, minValue);
                }
                else if (signature.IsNegative)
                {
                    sum -= scalar;
                    composer.SetTerm(id, -minValue);
                }
            }

            composer.SetTerm(minValueId, -sum);
            composer.Divide(composer.Norm());

            return composer.GetVector();
        }

        public XGaFloat64Vector GetEUnitNormalVector()
        {
            var (minValueId, minValue) =
                GetMinScalarMagnitudeIdScalar();

            var composer = ToComposer();

            var sum = 0d;
            foreach (var (id, scalar) in IdScalarTuples)
            {
                if (id == minValueId) continue;

                sum += scalar;
                composer.SetTerm(id, minValue);
            }

            composer.SetTerm(minValueId, -sum);
            composer.Divide(composer.Norm());

            return composer.GetVector();
        }


        
        public LinFloat64Angle GetEuclideanAngle(XGaFloat64Vector vector2, bool assumeUnitVectors = false)
        {
            var angleCos = ESp(vector2).Scalar();

            if (!assumeUnitVectors)
                angleCos /= ENorm() * vector2.ENorm();

            return angleCos.CosToPolarAngle();
        }

        
        public XGaFloat64Vector GetUnitBisector(XGaFloat64Vector vector2, bool assumeEqualNormVectors = false)
        {
            var v = assumeEqualNormVectors
                ? this + vector2
                : DivideByENorm() + vector2.DivideByENorm();

            return v.DivideByENorm();
        }


        
        public XGaFloat64Vector OmMapUsing(IXGaFloat64Outermorphism om)
        {
            return om.OmMap(this);
        }


        
        public XGaFloat64PureRotor GetEuclideanRotorFromBasis(int index)
        {
            return Processor
                .VectorTerm(index)
                .CreatePureRotor(this);
        }

        
        public XGaFloat64PureRotor GetEuclideanRotorFrom(XGaFloat64Vector vector1)
        {
            return vector1.CreatePureRotor(this);
        }

        
        public XGaFloat64PureRotor GetEuclideanRotorFrom(XGaFloat64Vector vector1, bool assumeUnitVectors)
        {
            return vector1.CreatePureRotor(
                this,
                assumeUnitVectors
            );
        }


        
        public XGaFloat64PureRotor GetEuclideanRotorToBasis(int index)
        {
            return CreatePureRotor(
                Processor.VectorTerm(index)
            );
        }

        
        public XGaFloat64PureRotor GetEuclideanRotorTo(XGaFloat64Vector vector2)
        {
            return CreatePureRotor(vector2);
        }

        
        public XGaFloat64PureRotor GetEuclideanRotorTo(XGaFloat64Vector vector2, bool assumeUnitVectors)
        {
            return CreatePureRotor(
                vector2,
                assumeUnitVectors
            );
        }

        /// <summary>
        /// Find a Euclidean rotor from vector1 to its projection on subspace
        /// </summary>
        /// <param name="subspace"></param>
        /// <returns></returns>
        
        public XGaFloat64PureRotor GetEuclideanRotorTo(XGaFloat64Subspace subspace)
        {
            return CreatePureRotor(
                subspace.Project(this)
            );
        }


        /// <summary>
        /// Create a pure Euclidean rotor that rotates the given source vector into the target vector
        /// </summary>
        /// <param name="targetVector"></param>
        /// <param name="assumeUnitVectors"></param>
        /// <returns></returns>
        public XGaFloat64PureRotor CreatePureRotor(XGaFloat64Vector targetVector, bool assumeUnitVectors = false)
        {
            var cosAngle =
                assumeUnitVectors
                    ? targetVector.ESp(this)
                    : targetVector.ESp(this) / (targetVector.ENormSquared() * ENormSquared()).Sqrt();

            if (cosAngle.IsOne)
                return Processor.IdentityRotor();

            var rotationBlade =
                cosAngle.IsMinusOne
                    ? GetNormalVector().Op(this)
                    : targetVector.Op(this);

            var unitRotationBlade =
                rotationBlade / (-rotationBlade.ESpSquared()).Sqrt();

            var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
            var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

            var scalarPart = cosHalfAngle;
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
        /// <param name="targetVector"></param>
        /// <returns></returns>
        public XGaFloat64PureScalingRotor CreatePureScalingRotor(XGaFloat64Vector targetVector)
        {
            var uNorm = ENorm();
            var vNorm = targetVector.ENorm();
            var scalingFactor = (vNorm / uNorm).Sqrt();
            var cosAngle = targetVector.ESp(this) / (uNorm * vNorm);

            if (cosAngle.IsOne)
                return XGaFloat64PureScalingRotor.Create(Processor, scalingFactor);

            var rotationBlade =
                cosAngle.IsMinusOne
                    ? GetNormalVector().Op(this)
                    : targetVector.Op(this);

            var unitRotationBlade =
                rotationBlade / (-rotationBlade.ESpSquared()).Sqrt();

            var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
            var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

            var scalarPart =
                scalingFactor * cosHalfAngle;

            var bivectorPart =
                scalingFactor * sinHalfAngle * unitRotationBlade;

            return XGaFloat64PureScalingRotor.Create(
                scalarPart,
                bivectorPart
            );
        }

        /// <summary>
        /// Create one rotor from the parametric family of pure rotors taking
        /// this to targetVector in 3D Euclidean space
        /// </summary>
        /// <param name="targetVector"></param>
        /// <param name="angleTheta"></param>
        /// <returns></returns>
        public XGaFloat64PureRotor CreateParametricPureRotor3D(XGaFloat64Vector targetVector, LinFloat64Angle angleTheta)
        {
            // Compute inverse of 3D pseudo-scalar = -e123
            var pseudoScalarInverse =
                Processor.PseudoScalarInverse(3);

            // Compute the smallest angle between source and target vectors
            var cosAngle0 =
                ESp(targetVector);

            // Define a rotor S with angle theta in the plane orthogonal to targetVector - this
            var rotorSBlade =
                (targetVector - this).EGp(
                    pseudoScalarInverse
                ).GetBivectorPart();

            var rotorS = rotorSBlade.ToPureRotor(angleTheta);

            // Define parametric 2-blade of rotation
            // The actual plane of rotation is made by rotating the plane containing
            // this and targetVector by angle theta in the plane orthogonal to
            // targetVector - this using rotor S
            var rotorBlade =
                rotorS.OmMap(targetVector.Op(this));

            var sinAngleThetaSquare = angleTheta.Sin().Square();

            // Define parametric angle of rotation
            var rotorAngle =
                (1 + 2 * (cosAngle0.ScalarValue - 1) / (2 - sinAngleThetaSquare * (cosAngle0.ScalarValue + 1))).ArcCos().RadiansToPolarAngle();

            // Math.Acos(1 + 2 * (cosAngle0 - 1) / (2 - Math.Pow(Math.Sin(angleTheta), 2) * (cosAngle0 + 1)));

            // Return the final rotor taking v1 into v2
            return rotorBlade.ToPureRotor(rotorAngle);
        }

        
        public XGaFloat64PureScalingRotor CreateScaledParametricPureRotor3D(XGaFloat64Vector targetVector, LinFloat64Angle angleTheta, double scalingFactor)
        {
            return CreateParametricPureRotor3D(targetVector, angleTheta)
                .CreatePureScalingRotor(scalingFactor);
        }


        /// <summary>
        /// Create a pure Euclidean rotor that rotates the given source basis vector
        /// into the target vector
        /// </summary>
        /// <param name="sourceAxis"></param>
        /// <param name="assumeUnitVector"></param>
        /// <returns></returns>
        public XGaFloat64PureScalingRotor CreatePureScalingRotorFromAxis(LinBasisVector sourceAxis, bool assumeUnitVector = false)
        {
            var k = sourceAxis.Index;
            var vNorm = assumeUnitVector
                ? 1d
                : ENorm();

            var ek = Processor.VectorTerm(k);

            var vk1 = vNorm + (sourceAxis.IsPositive ? this[k] : -this[k]);
            var vOpAxis = sourceAxis.IsPositive ? Op(ek) : ek.Op(this);

            return XGaFloat64PureScalingRotor.Create(
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
        /// <returns></returns>
        public XGaFloat64PureScalingRotor CreatePureScalingRotorToAxis(LinBasisVector targetAxis, bool assumeUnitVector = false)
        {
            var k = targetAxis.Index;
            var vNorm =
                assumeUnitVector
                    ? 1d
                    : ENorm();

            var vNorm2 =
                assumeUnitVector
                    ? 2d
                    : 2d * ENormSquared();

            var ek = Processor.VectorTerm(k);

            var vk1 = vNorm + (targetAxis.IsPositive ? this[k] : -this[k]);
            var vOpAxis = targetAxis.IsPositive ? ek.Op(this) : Op(ek);

            return XGaFloat64PureScalingRotor.Create(
                (vk1 / vNorm2).Sqrt(),
                vOpAxis / (vk1 * vNorm2).Sqrt()
            );
        }

        /// <summary>
        /// Create a pure Euclidean rotor that rotates the given source basis vector
        /// into the target vector
        /// </summary>
        /// <param name="sourceAxis"></param>
        /// <param name="assumeUnitVector"></param>
        /// <returns></returns>
        public XGaFloat64PureRotor CreatePureRotorFromAxis(LinBasisVector sourceAxis, bool assumeUnitVector = false)
        {
            var k = sourceAxis.Index;

            var v =
                assumeUnitVector
                    ? this
                    : DivideByENorm();

            var ek = Processor.VectorTerm(k);

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
        /// <returns></returns>
        public XGaFloat64PureRotor CreatePureRotorToAxis(LinBasisVector targetAxis, bool assumeUnitVector = false)
        {
            var k = targetAxis.Index;

            var v =
                assumeUnitVector
                    ? this
                    : DivideByENorm();

            var ek = Processor.VectorTerm(k);

            var vk1 = 1 + (targetAxis.IsPositive ? v[k] : -v[k]);
            var vOpAxis = targetAxis.IsPositive ? ek.Op(v) : v.Op(ek);

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
        /// <returns></returns>
        public XGaFloat64PureRotor CreatePureRotor(LinBasisVector targetAxis, bool assumeUnitVector = false)
        {
            var k = targetAxis.Index;

            var v =
                assumeUnitVector
                    ? this
                    : DivideByENorm();

            var ek = Processor.VectorTerm(k);

            var vk1 = 1 + (targetAxis.IsPositive ? v[k] : -v[k]);
            var vOpAxis = targetAxis.IsPositive ? ek.Op(v) : v.Op(ek);

            return XGaFloat64PureRotor.Create(
                (vk1 / 2).Sqrt(),
                vOpAxis / (vk1 * 2).Sqrt()
            );
        }

        public XGaFloat64PureRotorSequence CreatePureRotorSequence(XGaFloat64Vector sourceVector2, XGaFloat64Vector targetVector1, XGaFloat64Vector targetVector2, bool assumeUnitVectors = false)
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

            return XGaFloat64PureRotorSequence.Create(rotor1, rotor2);
        }

        public XGaFloat64PureRotor CreatePureRotor(XGaFloat64Vector inputVector2, XGaFloat64Vector rotatedVector1, XGaFloat64Vector rotatedVector2, int baseSpaceDimensions)
        {
            var inputFrame = XGaFloat64VectorFrameSpecs
                .CreateLinearlyIndependentSpecs()
                .CreateVectorFrame(
                    this,
                    inputVector2
                );

            var rotatedFrame = XGaFloat64VectorFrameSpecs
                .CreateLinearlyIndependentSpecs()
                .CreateVectorFrame(
                    rotatedVector1,
                    rotatedVector2
                );

            var rotor = XGaFloat64PureRotorSequence.CreateFromEuclideanFrames(
                baseSpaceDimensions,
                inputFrame,
                rotatedFrame
            ).GetFinalRotor();

            var (scalar, bivector) = rotor.Multivector.GetScalarBivectorParts();

            return XGaFloat64PureRotor.Create(scalar.ScalarValue, bivector);
        }


        public XGaFloat64EuclideanScalingRotor2D CreateEuclideanScalingRotor2D(XGaFloat64Vector targetVector)
        {
            Debug.Assert(
                Processor.HasSameSignature(targetVector.Processor)
            );

            var u1 = this[0];
            var u2 = this[1];

            var v1 = targetVector[0];
            var v2 = targetVector[1];

            var vuDot = v1 * u1 + v2 * u2;
            var uNormSquared = u1 * u1 + u2 * u2;
            var vNormSquared = v1 * v1 + v2 * v2;

            var t1 = (vNormSquared / uNormSquared).Sqrt();
            var t2 = vuDot / uNormSquared;

            var vuWedgeScalar = (v1 * u2 - v2 * u1).Sign();

            var a0 = ((t1 + t2) / 2).Sqrt();
            var a12 = ((t1 - t2) / 2).Sqrt() * vuWedgeScalar;

            return XGaFloat64EuclideanScalingRotor2D.Create(Processor, a0, a12);
        }

        public XGaFloat64EuclideanScalingRotorSquared2D CreateEuclideanScalingRotorSquared2D(XGaFloat64Vector targetVector)
        {
            var u1 = this[0];
            var u2 = this[1];

            var v1 = targetVector[0];
            var v2 = targetVector[1];

            var uNormSquared = u1 * u1 + u2 * u2;

            var a0 = (v1 * u1 + v2 * u2) / uNormSquared;
            var a12 = (v1 * u2 - v2 * u1) / uNormSquared;

            return XGaFloat64EuclideanScalingRotorSquared2D.Create(Processor, a0, a12);
        }


        /// <summary>
        /// Create a pure Euclidean rotor that rotates the given source vector into the target vector
        /// </summary>
        /// <param name="targetVector"></param>
        /// <param name="assumeUnitVectors"></param>
        /// <returns></returns>
        public XGaFloat64PureScalingRotor CreateEuclideanPureRotor(XGaFloat64Vector targetVector, bool assumeUnitVectors = false)
        {
            var cosAngle =
                assumeUnitVectors
                    ? targetVector.ESp(this)
                    : targetVector.ESp(this) /
                      (targetVector.ENormSquared() * ENormSquared()).Sqrt();

            if (cosAngle == 1d)
                return Processor.IdentityPureScalingRotor();

            var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
            var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

            var rotationBlade =
                cosAngle == -1
                    ? GetEUnitNormalVector().Op(this)
                    : targetVector.Op(this);

            var unitRotationBlade =
                rotationBlade.Divide((-rotationBlade.ESpSquared()).Sqrt());

            var bivectorPart =
                unitRotationBlade.Times(sinHalfAngle);

            return XGaFloat64PureScalingRotor.Create(
                cosHalfAngle + bivectorPart,
                cosHalfAngle - bivectorPart
            );
        }

        /// <summary>
        /// Create a scaled pure Euclidean rotor that rotates and
        /// scales the given source vector into the target vector
        /// </summary>
        /// <param name="targetVector"></param>
        /// <returns></returns>
        public XGaFloat64PureScalingRotor CreateEuclideanPureScalingRotor(XGaFloat64Vector targetVector)
        {
            var uNorm = ENorm();
            var vNorm = targetVector.ENorm();
            var scalingFactor = (vNorm / uNorm).Sqrt();
            var cosAngle = targetVector.ESp(this).Divide(uNorm * vNorm);

            if (cosAngle == 1d)
                return Processor.IdentityScalingRotor(scalingFactor);

            var cosHalfAngle = ((1 + cosAngle) / 2).Sqrt();
            var sinHalfAngle = ((1 - cosAngle) / 2).Sqrt();

            var rotationBlade =
                cosAngle == -1d
                    ? GetEUnitNormalVector().Op(this)
                    : targetVector.Op(this);

            var unitRotationBlade =
                rotationBlade / (-rotationBlade.ESpSquared()).Sqrt();

            var scalarPart =
                scalingFactor * cosHalfAngle;

            var bivectorPart =
                scalingFactor * sinHalfAngle * unitRotationBlade;

            return XGaFloat64PureScalingRotor.Create(
                scalarPart + bivectorPart
            );
        }

        /// <summary>
        /// Create a pure Euclidean rotor that rotates the given source basis vector
        /// into the target vector
        /// </summary>
        /// <param name="targetBasisVectorIndex"></param>
        /// <param name="assumeUnitVector"></param>
        /// <returns></returns>
        public XGaFloat64PureScalingRotor CreatePureScalingRotorToBasisVector(int targetBasisVectorIndex, bool assumeUnitVector = false)
        {
            var processor = Metric;
            var k = targetBasisVectorIndex;
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
                (vk1 / vNorm / 2).Sqrt() + ek.Op(v) / (vNorm * vk1 * 2).Sqrt()
            );
        }

        /// <summary>
        /// Create a pure Euclidean rotor that rotates the given source vector
        /// into the target basis vector
        /// </summary>
        /// <param name="targetBasisVectorIndex"></param>
        /// <param name="assumeUnitVector"></param>
        /// <returns></returns>
        public XGaFloat64PureScalingRotor CreatePureRotorToBasisVector(int targetBasisVectorIndex, bool assumeUnitVector = false)
        {
            var processor = Metric;
            var k = targetBasisVectorIndex;

            var v =
                assumeUnitVector
                    ? this
                    : Divide(ENorm());

            var ek = processor.BasisVector(k).ToKVector();

            var vk = v.Scalar(k);
            var vk1 = 1 + vk;

            return XGaFloat64PureScalingRotor.Create(
                (vk1 / 2).Sqrt() + ek.Op(v) / (2 * vk1).Sqrt()
            );
        }

        public XGaFloat64PureScalingRotor CreateEuclideanPureRotor(XGaFloat64Vector sourceVector2, XGaFloat64Vector targetVector1, XGaFloat64Vector targetVector2, bool assumeUnitVectors = false)
        {
            var rotor1 =
                CreateEuclideanPureRotor(
                    targetVector1,
                    assumeUnitVectors
                );

            var rotor2 =
                rotor1.OmMap(sourceVector2)
                    .GetVectorPart()
                    .CreateEuclideanPureRotor(
                        targetVector2,
                        assumeUnitVectors
                    );

            var multivector =
                rotor2.Multivector.EGp(rotor1.Multivector);

            return XGaFloat64PureScalingRotor.Create(multivector);
        }


        
        public XGaFloat64PureReflector ToPureReflector()
        {
            return XGaFloat64PureReflector.Create(this);
        }

    }

    public sealed partial class XGaFloat64Bivector
    {

        
        public new XGaFloat64Bivector ReflectOn(XGaFloat64KVector subspace)
        {
            Debug.Assert(subspace.IsNearBlade());

            return subspace
                .Gp(this)
                .Gp(subspace.Inverse())
                .GetBivectorPart();
        }

        
        public new XGaFloat64Bivector ReflectDirectOnDirect(XGaFloat64KVector subspace)
        {
            return ReflectOn(subspace);
        }

        
        public new XGaFloat64Bivector ReflectDirectOnDual(XGaFloat64KVector subspace)
        {
            return ReflectOn(subspace);
        }

        
        public new XGaFloat64Bivector ReflectDualOnDirect(XGaFloat64KVector subspace, int vSpaceDimensions)
        {
            var mv1 = ReflectOn(subspace);

            var n = (Grade + 1) * (subspace.Grade + 1) + vSpaceDimensions - 1;

            return n.IsOdd() ? -mv1 : mv1;
        }

        
        public new XGaFloat64Bivector ReflectDualOnDual(XGaFloat64KVector subspace)
        {
            var mv1 = ReflectOn(subspace);

            return subspace.IsOdd() ? -mv1 : mv1;
        }


        
        public new XGaFloat64Bivector ProjectOn(XGaFloat64KVector subspace, bool useSubspaceInverse = false)
        {
            Debug.Assert(subspace.IsNearBlade());

            var subspaceInverse =
                useSubspaceInverse
                    ? subspace.PseudoInverse()
                    : subspace;

            return Fdp(subspaceInverse).Gp(subspace).GetBivectorPart();
        }

    }

    public sealed partial class XGaFloat64HigherKVector
    {

        
        public new XGaFloat64HigherKVector ReflectOn(XGaFloat64KVector subspace)
        {
            Debug.Assert(subspace.IsNearBlade());

            return subspace
                .Gp(this)
                .Gp(subspace.Inverse())
                .GetHigherKVectorPart(Grade);
        }

        
        public new XGaFloat64HigherKVector ReflectDirectOnDirect(XGaFloat64KVector subspace)
        {
            var mv1 = ReflectOn(subspace);

            var n = Grade * (subspace.Grade + 1);

            return n.IsOdd() ? -mv1 : mv1;
        }

        
        public new XGaFloat64HigherKVector ReflectDirectOnDual(XGaFloat64KVector subspace)
        {
            var mv1 = ReflectOn(subspace);

            var n = Grade * subspace.Grade;

            return n.IsOdd() ? -mv1 : mv1;
        }

        
        public new XGaFloat64HigherKVector ReflectDualOnDirect(XGaFloat64KVector subspace, int vSpaceDimensions)
        {
            var mv1 = ReflectOn(subspace);

            var n = (Grade + 1) * (subspace.Grade + 1) + vSpaceDimensions - 1;

            return n.IsOdd() ? -mv1 : mv1;
        }

        
        public new XGaFloat64HigherKVector ReflectDualOnDual(XGaFloat64KVector subspace)
        {
            var mv1 = ReflectOn(subspace);

            var n = (Grade + 1) * subspace.Grade;

            return n.IsOdd() ? -mv1 : mv1;
        }


        
        public new XGaFloat64HigherKVector ProjectOn(XGaFloat64KVector subspace, bool useSubspaceInverse = false)
        {
            Debug.Assert(subspace.IsNearBlade());

            var subspaceInverse =
                useSubspaceInverse
                    ? subspace.PseudoInverse()
                    : subspace;

            return Fdp(subspaceInverse).Gp(subspace).GetHigherKVectorPart(Grade);
        }

    }
}
