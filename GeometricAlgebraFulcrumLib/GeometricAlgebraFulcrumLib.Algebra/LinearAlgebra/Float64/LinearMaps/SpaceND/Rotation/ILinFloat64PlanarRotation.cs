using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Reflection;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.SubSpaces.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Rotation;

public interface ILinFloat64PlanarRotation :
    ILinFloat64UnilinearMap,
    ILinFloat64Subspace
{
    LinFloat64Vector BasisVector1 { get; }

    LinFloat64Vector BasisVector2 { get; }

    LinFloat64PolarAngle RotationAngle { get; }

    double RotationAngleCos { get; }

    double RotationAngleSin { get; }

    Pair<double> BasisESp(int axisIndex);

    Pair<double> BasisESp(ILinSignedBasisVector axis);

    Pair<double> BasisESp(LinFloat64Vector vector);

    LinFloat64Vector MapBasisVector1();

    LinFloat64Vector MapBasisVector2();

    LinFloat64Vector MapBasisVector1(LinFloat64Angle angle1);

    LinFloat64Vector MapBasisVector2(LinFloat64Angle angle1);

    Pair<LinFloat64Vector> MapBasisVector1(LinFloat64Angle angle1, LinFloat64Angle angle2);

    Pair<LinFloat64Vector> MapBasisVector2(LinFloat64Angle angle1, LinFloat64Angle angle2);

    LinFloat64Vector GetMiddleUnitVector1();

    LinFloat64Vector GetMiddleUnitVector2();


    LinFloat64PlanarRotation InterpolateTo(LinFloat64PlanarRotation targetRotation, double tValue);

    IEnumerable<LinFloat64PlanarRotation> InterpolateTo(LinFloat64PlanarRotation targetRotation, int count, bool isPeriodicRange = false);

    LinFloat64PlanarRotation InterpolateRotationAngle(double tValue, bool invertRotation = false);

    /// <summary>
    /// Get all planar rotations with the same plane of rotation and different angles
    /// evenly spaced in the range [0, this.RotationAngle]
    /// </summary>
    /// <param name="count"></param>
    /// <param name="isPeriodicRange"></param>
    /// <param name="invertRotation"></param>
    /// <returns></returns>
    IEnumerable<LinFloat64PlanarRotation> InterpolateRotationAngle(int count, bool isPeriodicRange = false, bool invertRotation = false);

    /// <summary>
    /// Create a new planar rotation with the same plane and different angle.
    /// If the flag invertRotation is true, this also swaps the two basis vectors
    /// of rotation
    /// </summary>
    /// <param name="rotationAngle"></param>
    /// <param name="invertRotation"></param>
    /// <returns></returns>
    LinFloat64PlanarRotation SetRotationAngle(LinFloat64PolarAngle rotationAngle, bool invertRotation = false);

    /// <summary>
    /// Get all planar rotations with the same plane of rotation and different angles
    /// </summary>
    /// <param name="rotationAngleList"></param>
    /// <param name="invertRotation"></param>
    /// <returns></returns>
    IEnumerable<LinFloat64PlanarRotation> SetRotationAngle(IEnumerable<LinFloat64PolarAngle> rotationAngleList, bool invertRotation = false);

    /// <summary>
    /// Create the same planar rotation by rotating both basis vectors with
    /// the given angle in the plane of rotation
    /// </summary>
    /// <param name="rotationAngle"></param>
    /// <returns></returns>
    LinFloat64PlanarRotation RotateBasisVectors(LinFloat64Angle rotationAngle);

    /// <summary>
    /// Create the same planar rotation by rotating both basis vectors so
    /// that the first basis vector is aligned with the projection of the
    /// given vector on the plane of rotation
    /// remains the same.
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="assumeProjected"></param>
    /// <returns></returns>
    LinFloat64PlanarRotation AlignBasisVector1(LinFloat64Vector vector, bool assumeProjected = false);

    /// <summary>
    /// Create the same planar rotation by rotating both basis vectors so
    /// that the second basis vector is aligned with the projection of the
    /// given vector on the plane of rotation
    /// remains the same.
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="assumeProjected"></param>
    /// <returns></returns>
    LinFloat64PlanarRotation AlignBasisVector2(LinFloat64Vector vector, bool assumeProjected = false);


    Pair<LinFloat64HyperPlaneNormalReflection> GetHyperPlaneReflectionPair();
}