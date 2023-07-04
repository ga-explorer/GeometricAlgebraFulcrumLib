using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D
{
    public static class Float64Vector3DComposerUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3DComposer ToComposer(this IFloat64Vector3D mv)
        {
            return Float64Vector3DComposer.Create().SetVector(mv);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3DComposer NegativeToComposer(this IFloat64Vector3D mv)
        {
            return Float64Vector3DComposer.Create().SetVectorNegative(mv);
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3DComposer ToComposer(this IFloat64Vector3D mv, double scalingFactor)
        {
            return Float64Vector3DComposer.Create().SetVector(mv, scalingFactor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D ToVector3D(this LinUnitBasisVector2D axis)
        {
            return axis switch
            {
                LinUnitBasisVector2D.PositiveX => Float64Vector3D.E1,
                LinUnitBasisVector2D.NegativeX => Float64Vector3D.NegativeE1,
                LinUnitBasisVector2D.PositiveY => Float64Vector3D.E2,
                _ => Float64Vector3D.NegativeE2
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D ToVector3D(this LinUnitBasisVector3D axis)
        {
            return axis switch
            {
                LinUnitBasisVector3D.PositiveX => Float64Vector3D.E1,
                LinUnitBasisVector3D.NegativeX => Float64Vector3D.NegativeE1,
                LinUnitBasisVector3D.PositiveY => Float64Vector3D.E2,
                LinUnitBasisVector3D.NegativeY => Float64Vector3D.NegativeE2,
                LinUnitBasisVector3D.PositiveZ => Float64Vector3D.E3,
                _ => Float64Vector3D.NegativeE3
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D ToVector3D(this LinUnitBasisVector3D axis, Float64Scalar scalingFactor)
        {
            return axis switch
            {
                LinUnitBasisVector3D.PositiveX => Float64Vector3D.Create(scalingFactor, 0, 0),
                LinUnitBasisVector3D.NegativeX => Float64Vector3D.Create(-scalingFactor, 0, 0),
                LinUnitBasisVector3D.PositiveY => Float64Vector3D.Create(0, scalingFactor, 0),
                LinUnitBasisVector3D.NegativeY => Float64Vector3D.Create(0, -scalingFactor, 0),
                LinUnitBasisVector3D.PositiveZ => Float64Vector3D.Create(0, 0, scalingFactor),
                _ => Float64Vector3D.Create(0, 0, -scalingFactor)
            };
        }
            
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D ToUnitVector(double vectorX, double vectorY, double vectorZ, bool zeroAsSymmetric = true)
        {
            var s = Float64Vector3DUtils.ENorm(vectorX, vectorY, vectorZ);

            if (s.IsZero())
                return zeroAsSymmetric
                    ? Float64Vector3D.UnitSymmetric
                    : Float64Vector3D.Zero;

            s = 1.0d / s;
            return Float64Vector3D.Create(vectorX * s, vectorY * s, vectorZ * s);
        }

        public static Float64Vector3D ToVector3D(this IEnumerable<double> scalarList, bool makeUnit = false)
        {
            var scalarArray = new double[3];

            var i = 0;
            foreach (var scalar in scalarList)
                scalarArray[i++] = scalar;

            var tuple = Float64Vector3D.Create(scalarArray[0],
                scalarArray[1],
                scalarArray[2]);

            return makeUnit ? tuple.ToUnitVector() : tuple;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D ToVector3D(this Float64SphericalUnitVector3D sphericalPosition)
        {
            var sinTheta = 
                sphericalPosition.Theta.Sin();

            var cosTheta = 
                sphericalPosition.Theta.Cos();

            return Float64Vector3D.Create(
                sinTheta * sphericalPosition.Phi.Cos(),
                sinTheta * sphericalPosition.Phi.Sin(),
                cosTheta
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D ToVector3D(this Float64SphericalUnitVector3D sphericalPosition, double length)
        {
            var rSinTheta = 
                length * sphericalPosition.Theta.Sin();

            var rCosTheta = 
                length * sphericalPosition.Theta.Cos();

            return Float64Vector3D.Create(
                rSinTheta * sphericalPosition.Phi.Cos(),
                rSinTheta * sphericalPosition.Phi.Sin(),
                rCosTheta
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D ToVector3D(this IFloat64SphericalVector3D sphericalPosition)
        {
            var rSinTheta = 
                sphericalPosition.R * sphericalPosition.Theta.Sin();

            var rCosTheta = 
                sphericalPosition.R * sphericalPosition.Theta.Cos();

            return Float64Vector3D.Create(
                rSinTheta * sphericalPosition.Phi.Cos(),
                rSinTheta * sphericalPosition.Phi.Sin(),
                rCosTheta
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64SphericalVector3D ToSphericalVector(this IFloat64Vector3D position)
        {
            var r = Math.Sqrt(
                position.X * position.X +
                position.Y * position.Y +
                position.Z * position.Z
            );

            return new Float64SphericalVector3D(
                Math.Acos(r / position.Z),
                Math.Atan2(position.Y, position.X),
                r
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64SphericalUnitVector3D ToSphericalUnitVector(this IFloat64Vector3D position)
        {
            var r = Math.Sqrt(
                position.X * position.X +
                position.Y * position.Y +
                position.Z * position.Z
            );

            return new Float64SphericalUnitVector3D(
                Math.Acos(r / position.Z),
                Math.Atan2(position.Y, position.X)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64SphericalUnitVector3D ToSphericalUnitVector(this IFloat64SphericalVector3D sphericalPosition)
        {
            return new Float64SphericalUnitVector3D(
                sphericalPosition.Theta,
                sphericalPosition.Phi
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64SphericalVector3D ToSphericalVector(this Float64SphericalUnitVector3D sphericalPosition, double r)
        {
            return new Float64SphericalVector3D(
                sphericalPosition.Theta,
                sphericalPosition.Phi,
                r
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D GetUnitVectorR(this IFloat64SphericalVector3D sphericalPosition)
        {
            var sinTheta = Math.Sin(sphericalPosition.Theta);
            var cosTheta = Math.Cos(sphericalPosition.Theta);

            var sinPhi = Math.Sin(sphericalPosition.Phi);
            var cosPhi = Math.Cos(sphericalPosition.Phi);

            return Float64Vector3D.Create(sinTheta * cosPhi,
                sinTheta * sinPhi,
                cosTheta);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D GetUnitVectorR(this IFloat64Vector3D vector)
        {
            var r = vector.ENorm();

            var cosTheta = r / vector.Z.Value;
            var sinTheta = Math.Sqrt(1 - cosTheta * cosTheta);

            var phi = Math.Atan2(vector.Y, vector.X);
            var cosPhi = Math.Cos(phi);
            var sinPhi = Math.Sin(phi);

            return Float64Vector3D.Create(
                sinTheta * cosPhi,
                sinTheta * sinPhi,
                cosTheta
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D GetUnitVectorTheta(this IFloat64SphericalVector3D sphericalPosition)
        {
            var sinTheta = Math.Sin(sphericalPosition.Theta);
            var cosTheta = Math.Cos(sphericalPosition.Theta);

            var sinPhi = Math.Sin(sphericalPosition.Phi);
            var cosPhi = Math.Cos(sphericalPosition.Phi);

            return Float64Vector3D.Create(cosTheta * cosPhi,
                cosTheta * sinPhi,
                -sinTheta);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D GetUnitVectorTheta(this IFloat64Vector3D vector)
        {
            var r = vector.ENorm();

            var cosTheta = vector.Z.Value / r;
            var sinTheta = Math.Sqrt(1 - cosTheta * cosTheta);

            var phi = Math.Atan2(vector.Y, vector.X);
            var cosPhi = Math.Cos(phi);
            var sinPhi = Math.Sin(phi);

            return Float64Vector3D.Create(
                cosTheta * cosPhi,
                cosTheta * sinPhi,
                -sinTheta
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D GetUnitVectorPhi(this IFloat64SphericalVector3D sphericalPosition)
        {
            var sinPhi = Math.Sin(sphericalPosition.Phi);
            var cosPhi = Math.Cos(sphericalPosition.Phi);

            return Float64Vector3D.Create(-sinPhi, cosPhi, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D GetUnitVectorPhi(this IFloat64Vector3D vector)
        {
            var phi = Math.Atan2(vector.Y, vector.X);
            var cosPhi = Math.Cos(phi);
            var sinPhi = Math.Sin(phi);

            return Float64Vector3D.Create(-sinPhi, cosPhi, 0);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D ToVector3D(this ITriplet<double> tuple)
        {
            return Float64Vector3D.Create(
                tuple.Item1,
                tuple.Item2,
                tuple.Item3
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D ToVector3D<T>(this ITriplet<T> tuple, Func<T, double> scalarMapping)
        {
            return Float64Vector3D.Create(
                scalarMapping(tuple.Item1),
                scalarMapping(tuple.Item2),
                scalarMapping(tuple.Item3)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3DComposer ToMutableTuple3D(this IFloat64Vector3D tuple)
        {
            return Float64Vector3DComposer.Create(tuple.X, tuple.Y, tuple.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D XyToTuple3D(this IFloat64Vector2D tuple)
        {
            return Float64Vector3D.Create(tuple.X, tuple.Y, Float64Scalar.Zero);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D XyToTuple3D(this IntTuple2D tuple)
        {
            return Float64Vector3D.Create(tuple.X, tuple.Y, 0.0d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D ToTuple3D(this IntTuple3D tuple)
        {
            return Float64Vector3D.Create(tuple.ItemX, tuple.ItemY, tuple.ItemZ);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D XyzToTuple3D(this IFloat64Vector4D tuple)
        {
            return Float64Vector3D.Create(tuple.X, tuple.Y, tuple.Z);
        }

    }
}