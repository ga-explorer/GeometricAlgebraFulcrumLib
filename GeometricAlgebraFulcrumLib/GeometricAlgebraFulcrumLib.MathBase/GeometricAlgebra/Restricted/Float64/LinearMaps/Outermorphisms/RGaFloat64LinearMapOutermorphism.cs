using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.SpaceND;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms
{
    public sealed class RGaFloat64LinearMapOutermorphism 
        : RGaFloat64OutermorphismBase
    {
        public override RGaFloat64Processor Processor { get; }
        
        public LinFloat64UnilinearMap LinearMap { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal RGaFloat64LinearMapOutermorphism(RGaFloat64Processor metric, LinFloat64UnilinearMap linearMap)
        {
            Processor = metric;
            LinearMap = linearMap;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool IsValid()
        {
            return LinearMap.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IRGaFloat64Outermorphism GetOmAdjoint()
        {
            return new RGaFloat64LinearMapOutermorphism(
                Processor, 
                LinearMap.GetAdjoint()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Vector OmMapBasisVector(int index)
        {
            return LinearMap.MapBasisVector(index).ToRGaFloat64Vector(Processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Bivector OmMapBasisBivector(int index1, int index2)
        {
            if (index1 < 0 || index1 > index2)
                throw new InvalidOperationException();

            return OmMapBasisVector(index1).Op(OmMapBasisVector(index2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64KVector OmMapBasisBlade(ulong id)
        {
            if (id == 0UL)
                return Processor.CreateZeroScalar();

            return id.IsBasisVector() 
                ? OmMapBasisVector(id.FirstOneBitPosition())
                : id.PatternToPositions().Select(OmMapBasisVector).Op();
        }
        

        public override RGaFloat64Vector OmMap(RGaFloat64Vector vector)
        {
            var composer = Processor.CreateComposer();

            foreach (var (id, scalar) in vector.IdScalarPairs)
                composer.AddMultivector(
                    OmMapBasisVector(id.FirstOneBitPosition()),
                    scalar
                );
            
            return composer.GetVector();
        }

        public override RGaFloat64Bivector OmMap(RGaFloat64Bivector bivector)
        {
            var composer = Processor.CreateComposer();

            foreach (var (id, scalar) in bivector.IdScalarPairs)
                composer.AddMultivector(
                    OmMapBasisBivector(id.FirstOneBitPosition(), id.LastOneBitPosition()),
                    scalar
                );
            
            return composer.GetBivector();
        }
        
        public override RGaFloat64HigherKVector OmMap(RGaFloat64HigherKVector kVector)
        {
            var composer = Processor.CreateComposer();

            foreach (var (id, scalar) in kVector.IdScalarPairs)
                composer.AddMultivector(
                    OmMapBasisBlade(id),
                    scalar
                );
            
            return composer.GetHigherKVector(kVector.Grade);
        }

        public override RGaFloat64KVector OmMap(RGaFloat64KVector kVector)
        {
            return kVector switch
            {
                RGaFloat64Scalar => Processor.CreateOneScalar(),
                RGaFloat64Vector v => OmMap(v),
                RGaFloat64Bivector bv => OmMap(bv),
                RGaFloat64HigherKVector kv => OmMap(kv),
                _ => throw new InvalidOperationException()
            };
        }

        public override RGaFloat64Multivector OmMap(RGaFloat64Multivector multivector)
        {
            var composer = Processor.CreateComposer();

            foreach (var (id, scalar) in multivector.IdScalarPairs)
                composer.AddMultivector(
                    OmMapBasisBlade(id),
                    scalar
                );
            
            return composer.GetMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<KeyValuePair<ulong, RGaFloat64Vector>> GetOmMappedBasisVectors(int vSpaceDimensions)
        {
            return LinearMap
                .GetMappedBasisVectors(vSpaceDimensions)
                .Select(r => 
                    new KeyValuePair<ulong, RGaFloat64Vector>(
                        r.Key.BasisVectorIndexToId(), 
                        r.Value.ToRGaFloat64Vector(Processor)
                    )
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
        {
            return LinearMap.GetSubMap(vSpaceDimensions);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<KeyValuePair<ulong, RGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions)
        {
            return Processor
                .GetBasisBladeIds(vSpaceDimensions)
                .Select(id => 
                    new KeyValuePair<ulong, RGaFloat64Multivector>(
                        id, 
                        OmMapBasisBlade(id)
                    )
                );
        }
    }
}