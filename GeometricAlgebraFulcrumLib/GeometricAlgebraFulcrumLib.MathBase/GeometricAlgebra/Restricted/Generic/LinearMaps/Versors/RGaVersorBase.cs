using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Versors
{
    public abstract class RGaVersorBase<T> :
        RGaOutermorphismBase<T>,
        IRGaVersor<T>
    {
        public override RGaProcessor<T> Processor { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected RGaVersorBase(RGaProcessor<T> processor)
        {
            Processor = processor;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract IRGaVersor<T> GetVersorInverse();

        public abstract RGaMultivector<T> GetMultivector();
        
        public abstract RGaMultivector<T> GetMultivectorReverse();
        
        public abstract RGaMultivector<T> GetMultivectorInverse();
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IRGaOutermorphism<T> GetOmAdjoint()
        {
            return GetVersorInverse();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaVector<T> OmMapBasisVector(int index)
        {
            return OmMap(
                Processor.CreateVector(index)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaBivector<T> OmMapBasisBivector(int index1, int index2)
        {
            if (index1 < 0 || index1 >= index2)
                throw new InvalidOperationException();

            return OmMap(
                Processor.CreateBivector(
                    index1, 
                    index2, 
                    ScalarProcessor.ScalarOne
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaKVector<T> OmMapBasisBlade(ulong id)
        {
            return OmMap(
                Processor.CreateKVector(
                    id, 
                    ScalarProcessor.ScalarOne
                )
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<KeyValuePair<ulong, RGaMultivector<T>>> GetMappedBasisBlades(int vSpaceDimensions)
        {
            return Processor
                .GetBasisBladeIds(vSpaceDimensions)
                .Select(id => 
                    new KeyValuePair<ulong, RGaMultivector<T>>(
                        id, 
                        OmMapBasisBlade(id)
                    )
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<RGaIdVectorRecord<T>> GetOmMappedBasisVectors(int vSpaceDimensions)
        {
            return vSpaceDimensions
                .GetRange()
                .Select(index => 
                    new RGaIdVectorRecord<T>(
                        index.BasisVectorIndexToId(), 
                        OmMapBasisVector(index)
                    )
                );
        }


    }
}