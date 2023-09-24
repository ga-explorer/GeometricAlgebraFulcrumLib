//using DataStructuresLib.Basic;
//using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.SpaceND.Reflection;
//using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Mutable;

//namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.SpaceND.Rotation
//{
//    public interface ISimpleVectorToVectorRotation :
//        ILinearMap
//    {
//        Float64Tuple SourceVector { get; }

//        Float64Tuple TargetOrthogonalVector { get; }

//        Float64Tuple TargetVector { get; }

//        double AngleCos { get; }

//        Float64PlanarAngle Angle { get; }

//        Float64Tuple GetMiddleUnitVector();

//        Pair<HyperPlaneNormalReflection> GetHyperPlaneReflectionPair();

//        Float64Tuple GetRotatedSourceVector(Float64PlanarAngle angle1);

//        Pair<Float64Tuple> GetRotatedSourceVectorPair(Float64PlanarAngle angle1, Float64PlanarAngle angle2);
//    }
//}