using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic.LinearMaps;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Versors
{
    public abstract class XGaVersorBase<T> :
        XGaOutermorphismBase<T>,
        IXGaVersor<T>
    {
        public override XGaProcessor<T> Processor { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected XGaVersorBase(XGaProcessor<T> processor)
        {
            Processor = processor;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract IXGaVersor<T> GetVersorInverse();

        public abstract XGaMultivector<T> GetMultivector();
        
        public abstract XGaMultivector<T> GetMultivectorReverse();
        
        public abstract XGaMultivector<T> GetMultivectorInverse();
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IXGaOutermorphism<T> GetOmAdjoint()
        {
            return GetVersorInverse();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaVector<T> OmMapBasisVector(int index)
        {
            return OmMap(
                Processor.CreateVector(index)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaBivector<T> OmMapBasisBivector(int index1, int index2)
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
        public override XGaKVector<T> OmMapBasisBlade(IIndexSet id)
        {
            return OmMap(
                Processor.CreateKVector(
                    id, 
                    ScalarProcessor.ScalarOne
                )
            );
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<KeyValuePair<IIndexSet, XGaMultivector<T>>> GetMappedBasisBlades(
            int vSpaceDimensions)
        {
            return Processor
                .GetBasisBladeIds(vSpaceDimensions)
                .Select(id => 
                    new KeyValuePair<IIndexSet, XGaMultivector<T>>(
                        id, 
                        OmMapBasisBlade(id)
                    )
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<KeyValuePair<IIndexSet, XGaVector<T>>> GetOmMappedBasisVectors(int vSpaceDimensions)
        {
            return vSpaceDimensions
                .GetRange()
                .Select(index => 
                    new KeyValuePair<IIndexSet, XGaVector<T>>(
                        index.IndexToIndexSet(), 
                        OmMapBasisVector(index)
                    )
                );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override LinUnilinearMap<T> GetVectorMapPart(int vSpaceDimensions)
        {
            return ScalarProcessor.CreateLinUnilinearMap(
                vSpaceDimensions,
                index => OmMapBasisVector(index).ToLinVector()
            );
        }

    }
}