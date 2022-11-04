using System.Diagnostics;
using System.Runtime.CompilerServices;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace NumericalGeometryLib.BasicMath.Maps
{
    public sealed class VectorToVectorRotation :
        SimpleVectorRotation
    {
        private readonly Float64DenseTuple _uVector;
        private readonly Float64DenseTuple _vVector;
        private readonly Float64DenseTuple _vuProjVector;
        private readonly double _vuDotPlus1Inv;


        public VectorToVectorRotation(Float64DenseTuple u, Float64DenseTuple v)
        {
            Debug.Assert(
                u.GetLengthSquared().IsNearOne() &&
                v.GetLengthSquared().IsNearOne()
            );

            var vuDot = v.VectorDot(u);

            Debug.Assert(
                !(vuDot + 1d).IsNearZero()
            );

            _uVector = u;
            _vVector = v;

            _vuDotPlus1Inv = 1d / (1d + vuDot);
            _vuProjVector = v - vuDot * u;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64DenseTuple Rotate(Float64DenseTuple x)
        {
            var r = x.VectorDot(_vuProjVector) * _vuDotPlus1Inv;
            var s = x.VectorDot(_uVector);
            
            return x - (r + s) * _uVector - (r - s) * _vVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64SparseTuple Rotate(Float64SparseTuple x)
        {
            var r = x.VectorDot(_vuProjVector) * _vuDotPlus1Inv;
            var s = x.VectorDot(_uVector);
            
            return x - (r + s) * _uVector - (r - s) * _vVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override SimpleVectorRotation GetInverse()
        {
            return new VectorToVectorRotation(
                _vVector,
                _uVector
            );
        }
    }
}
