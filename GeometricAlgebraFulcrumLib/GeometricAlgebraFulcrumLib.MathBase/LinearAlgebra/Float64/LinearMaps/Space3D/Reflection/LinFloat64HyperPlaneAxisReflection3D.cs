using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Space3D.Scaling;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Space3D.Reflection
{
    public sealed class LinFloat64HyperPlaneAxisReflection3D :
        LinFloat64ReflectionBase3D,
        ILinFloat64HyperPlaneNormalReflectionLinearMap3D
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64HyperPlaneAxisReflection3D Create(int axisIndex)
        {
            return new LinFloat64HyperPlaneAxisReflection3D(axisIndex);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinFloat64HyperPlaneAxisReflection3D Create(LinSignedBasisVector axis)
        {
            return new LinFloat64HyperPlaneAxisReflection3D(axis.Index
            );
        }


        public LinUnitBasisVector3D ReflectionNormalAxis { get; }

        public int ReflectionNormalAxisIndex { get; }

        public Float64Vector3D ReflectionNormal 
            => ReflectionNormalAxis.ToVector3D();
        
        public override bool SwapsHandedness 
            => true;

        public double ScalingFactor 
            => -1d;

        public Float64Vector3D ScalingVector 
            => ReflectionNormalAxis.ToVector3D();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private LinFloat64HyperPlaneAxisReflection3D(int basisIndex)
        {
            ReflectionNormalAxis = basisIndex switch
            {
                0 => LinUnitBasisVector3D.PositiveX,
                1 => LinUnitBasisVector3D.PositiveY,
                2 => LinUnitBasisVector3D.PositiveZ,
                _ => throw new IndexOutOfRangeException()
            };

            ReflectionNormalAxisIndex = basisIndex;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsIdentity()
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsNearIdentity(double epsilon = 1E-12)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector3D MapBasisVector(int basisIndex)
        {
            Debug.Assert(basisIndex >= 0);

            return Float64Vector3DComposer
                .Create()
                .SetTerm(
                    basisIndex,
                    ReflectionNormalAxisIndex == basisIndex ? -1d : 1d
                ).GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Float64Vector3D MapVector(IFloat64Tuple3D vector)
        {
            var composer = 
                Float64Vector3DComposer
                    .Create()
                    .SetVector(vector);
            
            composer[ReflectionNormalAxisIndex] *= -1d;

            return composer.GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64ReflectionBase3D GetReflectionLinearMapInverse()
        {
            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64HyperPlaneAxisReflection3D GetHyperPlaneAxisReflectionInverse()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinFloat64HyperPlaneNormalReflectionLinearMap3D GetHyperPlaneNormalReflectionLinearMapInverse()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64HyperPlaneNormalReflection3D ToHyperPlaneNormalReflection()
        {
            return LinFloat64HyperPlaneNormalReflection3D.Create(ReflectionNormal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ILinFloat64DirectionalScalingLinearMap3D GetDirectionalScalingInverse()
        {
            return this;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64HyperPlaneNormalReflectionSequence3D ToHyperPlaneReflectionSequence()
        {
            return LinFloat64HyperPlaneNormalReflectionSequence3D.Create(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinFloat64VectorDirectionalScaling3D ToVectorDirectionalScaling()
        {
            return LinFloat64VectorDirectionalScaling3D.Create(
                -1d, 
                ReflectionNormal
            );
        }
    }
}