using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using NumericalGeometryLib.Borders.Space3D;

namespace GraphicsComposerLib.Geometry.SdfShapes.RayMarching
{
    public sealed class SdfRay3D :
        IGeometricElement
    {
        public Tuple3D Origin { get; }

        public Tuple3D Direction { get; }

        public Tuple3D DirectionInv { get; }

        public Triplet<int> DirectionInvSign { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SdfRay3D([NotNull] ITuple3D origin, [NotNull] ITuple3D direction)
        {
            Origin = origin.ToTuple3D();
            Direction = direction.ToTuple3D();

            DirectionInv = new Tuple3D(
                1d / direction.X,
                1d / direction.Y,
                1d / direction.Z
            );

            DirectionInvSign = new Triplet<int>(
                DirectionInv.X < 0 ? 1 : 0,
                DirectionInv.Y < 0 ? 1 : 0,
                DirectionInv.Z < 0 ? 1 : 0
            );

            Debug.Assert(IsValid());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D GetPoint(double t)
        {
            return new Tuple3D(
                Origin.X + t * Direction.X,
                Origin.Y + t * Direction.Y,
                Origin.Z + t * Direction.Z
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return Origin.IsValid() && Direction.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Intersect(IBoundingBox3D box, out double tMin, out double tMax)
        {
            var bounds = new Tuple3D[]
            {
                new Tuple3D(box.MinX, box.MinY, box.MinZ),
                new Tuple3D(box.MinX, box.MinY, box.MinZ)
            };

            tMin = (bounds[DirectionInvSign.Item1].X - Origin.X) * DirectionInv.X; 
            tMax = (bounds[1 - DirectionInvSign.Item1].X - Origin.X) * DirectionInv.X; 

            var tYMin = (bounds[DirectionInvSign.Item2].Y - Origin.Y) * DirectionInv.Y; 
            var tYMax = (bounds[1 - DirectionInvSign.Item2].Y - Origin.Y) * DirectionInv.Y; 
 
            if (tMin > tYMax || tYMin > tMax) 
                return false;

            if (tYMin > tMin) 
                tMin = tYMin; 

            if (tYMax < tMax) 
                tMax = tYMax; 
 
            var tZMin = (bounds[DirectionInvSign.Item3].Z - Origin.Z) * DirectionInv.Z; 
            var tZMax = (bounds[1 - DirectionInvSign.Item3].Z - Origin.Z) * DirectionInv.Z; 
 
            if (tMin > tZMax || tZMin > tMax) 
                return false; 

            if (tZMin > tMin) 
                tMin = tZMin; 

            if (tZMax < tMax) 
                tMax = tZMax; 
 
            return true; 
        }
    }
}