using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace NumericalGeometryLib.BasicMath.Constants
{
    public static class ConstantsUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple2D GetVector2D(this Axis2D axis)
        {
            return axis switch
            {
                Axis2D.PositiveX => Tuple2D.E1,
                Axis2D.NegativeX => Tuple2D.NegativeE1,
                Axis2D.PositiveY => Tuple2D.E2,
                _ => Tuple2D.NegativeE2
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple3D GetVector3D(this Axis2D axis)
        {
            return axis switch
            {
                Axis2D.PositiveX => Tuple3D.E1,
                Axis2D.NegativeX => Tuple3D.NegativeE1,
                Axis2D.PositiveY => Tuple3D.E2,
                _ => Tuple3D.NegativeE2
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple3D GetVector3D(this Axis3D axis)
        {
            return axis switch
            {
                Axis3D.PositiveX => Tuple3D.E1,
                Axis3D.NegativeX => Tuple3D.NegativeE1,
                Axis3D.PositiveY => Tuple3D.E2,
                Axis3D.NegativeY => Tuple3D.NegativeE2,
                Axis3D.PositiveZ => Tuple3D.E3,
                _ => Tuple3D.NegativeE3
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Axis3D GetAxis3D(this AxisPair3D axisPair)
        {
            return axisPair switch
            {
                AxisPair3D.Yz => Axis3D.PositiveX,
                AxisPair3D.Zy => Axis3D.NegativeX,
                AxisPair3D.Zx => Axis3D.PositiveY,
                AxisPair3D.Xz => Axis3D.NegativeY,
                AxisPair3D.Xy => Axis3D.PositiveZ,
                _ => Axis3D.NegativeZ
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AxisPair3D GetAxisPair3D(this Axis3D axis)
        {
            return axis switch
            {
                Axis3D.PositiveX => AxisPair3D.Yz,
                Axis3D.NegativeX => AxisPair3D.Zy,
                Axis3D.PositiveY => AxisPair3D.Zx,
                Axis3D.NegativeY => AxisPair3D.Xz,
                Axis3D.PositiveZ => AxisPair3D.Xy,
                _ => AxisPair3D.Yx
            };
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsXAxis(this Axis3D axis)
        {
            return axis is Axis3D.PositiveX or Axis3D.NegativeX;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsYAxis(this Axis3D axis)
        {
            return axis is Axis3D.PositiveY or Axis3D.NegativeY;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsZAxis(this Axis3D axis)
        {
            return axis is Axis3D.PositiveZ or Axis3D.NegativeZ;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPositive(this Axis3D axis)
        {
            return axis is Axis3D.PositiveX or Axis3D.PositiveY or Axis3D.PositiveZ;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNegative(this Axis3D axis)
        {
            return axis is Axis3D.NegativeX or Axis3D.NegativeY or Axis3D.NegativeZ;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Axis3D SelectNearestAxis(this ITuple3D unitVector)
        {
            return unitVector.GetMaxAbsComponentIndex() switch
            {
                0 => unitVector.X > 0 ? Axis3D.PositiveX : Axis3D.NegativeX,
                1 => unitVector.Y > 0 ? Axis3D.PositiveY : Axis3D.NegativeY,
                _ => unitVector.Z > 0 ? Axis3D.PositiveZ : Axis3D.NegativeZ
            };
        }
    }
}
