using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND.Reflection;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.SubSpaces.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND.Rotation
{
    public interface ILinFloat64PlanarRotation :
        ILinFloat64UnilinearMap,
        ILinFloat64Subspace
    {
        Float64Vector BasisVector1 { get; }

        Float64Vector BasisVector2 { get; }

        Float64PlanarAngle RotationAngle { get; }

        double RotationAngleCos { get; }

        double RotationAngleSin { get; }
        
        Pair<double> BasisESp(int axisIndex);

        Pair<double> BasisESp(ILinSignedBasisVector axis);

        Pair<double> BasisESp(Float64Vector vector);

        Float64Vector MapBasisVector1();

        Float64Vector MapBasisVector2();

        Float64Vector MapBasisVector1(Float64PlanarAngle angle1);

        Float64Vector MapBasisVector2(Float64PlanarAngle angle1);

        Pair<Float64Vector> MapBasisVector1(Float64PlanarAngle angle1, Float64PlanarAngle angle2);

        Pair<Float64Vector> MapBasisVector2(Float64PlanarAngle angle1, Float64PlanarAngle angle2);
        
        Float64Vector GetMiddleUnitVector1();

        Float64Vector GetMiddleUnitVector2();
        
        
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
        LinFloat64PlanarRotation SetRotationAngle(Float64PlanarAngle rotationAngle, bool invertRotation = false);

        /// <summary>
        /// Get all planar rotations with the same plane of rotation and different angles
        /// </summary>
        /// <param name="rotationAngleList"></param>
        /// <param name="invertRotation"></param>
        /// <returns></returns>
        IEnumerable<LinFloat64PlanarRotation> SetRotationAngle(IEnumerable<Float64PlanarAngle> rotationAngleList, bool invertRotation = false);

        /// <summary>
        /// Create the same planar rotation by rotating both basis vectors with
        /// the given angle in the plane of rotation
        /// </summary>
        /// <param name="rotationAngle"></param>
        /// <returns></returns>
        LinFloat64PlanarRotation RotateBasisVectors(Float64PlanarAngle rotationAngle);

        /// <summary>
        /// Create the same planar rotation by rotating both basis vectors so
        /// that the first basis vector is aligned with the projection of the
        /// given vector on the plane of rotation
        /// remains the same.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="assumeProjected"></param>
        /// <returns></returns>
        LinFloat64PlanarRotation AlignBasisVector1(Float64Vector vector, bool assumeProjected = false);

        /// <summary>
        /// Create the same planar rotation by rotating both basis vectors so
        /// that the second basis vector is aligned with the projection of the
        /// given vector on the plane of rotation
        /// remains the same.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="assumeProjected"></param>
        /// <returns></returns>
        LinFloat64PlanarRotation AlignBasisVector2(Float64Vector vector, bool assumeProjected = false);


        Pair<LinFloat64HyperPlaneNormalReflection> GetHyperPlaneReflectionPair();
    }
}