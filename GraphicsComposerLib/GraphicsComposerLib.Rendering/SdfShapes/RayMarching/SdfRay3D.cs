using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GraphicsComposerLib.Rendering.SdfShapes.RayMarching
{
    public sealed class SdfRay3D :
        IGeometricElement
    {
        public Float64Vector3D Origin { get; }

        public Float64Vector3D Direction { get; }

        public Float64Vector3D DirectionInv { get; }

        public Triplet<int> DirectionInvSign { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SdfRay3D(IFloat64Vector3D origin, IFloat64Vector3D direction)
        {
            Origin = origin.ToVector3D();
            Direction = direction.ToVector3D();

            DirectionInv = Float64Vector3D.Create(1d / direction.X,
                1d / direction.Y,
                1d / direction.Z);

            DirectionInvSign = new Triplet<int>(
                DirectionInv.X.IsNegative() ? 1 : 0,
                DirectionInv.Y.IsNegative() ? 1 : 0,
                DirectionInv.Z.IsNegative() ? 1 : 0
            );

            Debug.Assert(IsValid());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D GetPoint(double t)
        {
            return Float64Vector3D.Create(Origin.X + t * Direction.X,
                Origin.Y + t * Direction.Y,
                Origin.Z + t * Direction.Z);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return Origin.IsValid() && Direction.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Intersect(IBoundingBox3D box, out double tMin, out double tMax)
        {
            var bounds = new Float64Vector3D[]
            {
                Float64Vector3D.Create(box.MinX, box.MinY, box.MinZ),
                Float64Vector3D.Create(box.MinX, box.MinY, box.MinZ)
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