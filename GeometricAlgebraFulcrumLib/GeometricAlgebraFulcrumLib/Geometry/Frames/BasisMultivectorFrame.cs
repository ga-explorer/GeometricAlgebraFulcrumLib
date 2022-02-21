using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Combinations;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Geometry.Frames
{
    public class BasisMultivectorFrame<T> :
        IMultivectorFrame<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static BasisMultivectorFrame<T> Create(IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            var multivectorArray = 
                geometricProcessor.
                    GaSpaceDimension
                    .GetRange()
                    .Select(geometricProcessor.CreateKVectorStorageBasis)
                    .Cast<IMultivectorStorage<T>>()
                    .ToArray();
            
            return new BasisMultivectorFrame<T>(geometricProcessor, multivectorArray);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static BasisMultivectorFrame<T> Create(IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<Multivector<T>> multivectorList)
        {
            var multivectorArray = 
                multivectorList.Select(v => v.MultivectorStorage).ToArray();
            
            return new BasisMultivectorFrame<T>(geometricProcessor, multivectorArray);
        }

        internal static BasisMultivectorFrame<T> CreateFrom(BasisVectorFrame<T> vectorFrame)
        {
            var geometricProcessor = vectorFrame.GeometricProcessor;

            var multivectorArray = new IMultivectorStorage<T>[geometricProcessor.GaSpaceDimension];

            multivectorArray[0] = geometricProcessor.CreateKVectorStorageBasisScalar();

            for (var index = 0; index < vectorFrame.Count; index++)
                multivectorArray[1ul << index] = vectorFrame[index].VectorStorage;

            for (var grade = 2U; grade <= geometricProcessor.VSpaceDimension; grade++)
            {
                var kvSpaceDimension = 
                    geometricProcessor.VSpaceDimension.GetBinomialCoefficient(grade);

                for (var index = 0UL; index < kvSpaceDimension; index++)
                {
                    var id = BasisBladeUtils.BasisBladeGradeIndexToId(grade, index);

                    var (basisVectorId, basisBladeId) = 
                        id.SplitByLargestBasisVectorId();

                    multivectorArray[id] = geometricProcessor.Op(
                        multivectorArray[basisBladeId],
                        multivectorArray[basisVectorId]
                    );
                }
            }

            return new BasisMultivectorFrame<T>(geometricProcessor, multivectorArray); 
        }


        private readonly IMultivectorStorage<T>[] _multivectorArray;


        public int Count 
            => (int) GeometricProcessor.GaSpaceDimension;

        public Multivector<T> this[int index]
        {
            get => _multivectorArray[index].CreateMultivector(GeometricProcessor);
            set => _multivectorArray[index] = value.MultivectorStorage ?? throw new ArgumentNullException(nameof(value));
        }

        public IScalarAlgebraProcessor<T> ScalarProcessor 
            => GeometricProcessor;

        public ILinearAlgebraProcessor<T> LinearProcessor 
            => GeometricProcessor;

        public IGeometricAlgebraProcessor<T> GeometricProcessor { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private BasisMultivectorFrame([NotNull] IGeometricAlgebraProcessor<T> geometricProcessor, [NotNull] params IMultivectorStorage<T>[] multivectorArray)
        {
            GeometricProcessor = geometricProcessor;

            if (multivectorArray.Length != (int) geometricProcessor.GaSpaceDimension)
                throw new ArgumentException(nameof(multivectorArray));

            _multivectorArray = multivectorArray;
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BasisMultivectorFrame<T> MapAsBasisUsing(Func<Multivector<T>, Multivector<T>> vectorMapping)
        {
            var vectorArray = 
                Count
                    .GetRange()
                    .Select(index => vectorMapping(this[index]).MultivectorStorage)
                    .ToArray();
            
            return new BasisMultivectorFrame<T>(GeometricProcessor, vectorArray);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<Multivector<T>> GetEnumerator()
        {
            return _multivectorArray.Select(
                v => v.CreateMultivector(GeometricProcessor)
            ).GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}