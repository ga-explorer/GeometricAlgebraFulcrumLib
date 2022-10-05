using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Versors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Geometry.Frames
{
    public class BasisVectorFrame<T> :
        IVectorFrame<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static BasisVectorFrame<T> Create(IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            var vectorArray = 
                geometricProcessor.
                    VSpaceDimension
                    .GetRange()
                    .Select(index => geometricProcessor.CreateVectorStorageBasis(index))
                    .ToArray();

            return new BasisVectorFrame<T>(geometricProcessor, vectorArray);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static BasisVectorFrame<T> Create(IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<GaVector<T>> vectorList)
        {
            var vectorArray = 
                vectorList.Select(v => v.VectorStorage).ToArray();
            
            return new BasisVectorFrame<T>(geometricProcessor, vectorArray);
        }


        private readonly VectorStorage<T>[] _vectorArray;


        public IScalarAlgebraProcessor<T> ScalarProcessor 
            => GeometricProcessor;

        public ILinearAlgebraProcessor<T> LinearProcessor 
            => GeometricProcessor;

        public IGeometricAlgebraProcessor<T> GeometricProcessor { get; }

        public int Count 
            => (int) GeometricProcessor.VSpaceDimension;

        public GaVector<T> this[int index]
        {
            get => _vectorArray[index].CreateVector(GeometricProcessor);
            set => _vectorArray[index] = value ?? throw new ArgumentNullException(nameof(value));
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private BasisVectorFrame([NotNull] IGeometricAlgebraProcessor<T> geometricProcessor, [NotNull] params VectorStorage<T>[] vectorArray)
        {
            GeometricProcessor = geometricProcessor;

            if (vectorArray.Length != (int) geometricProcessor.VSpaceDimension)
                throw new ArgumentException(nameof(vectorArray));

            _vectorArray = vectorArray;
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// See "Geometric Algebra for Computer Science" section 3.8
        /// </summary>
        /// <returns></returns>
        public BasisVectorFrame<T> GetReciprocalVectorFrame()
        {
            var pseudoScalarInv =
                GeometricProcessor.BladeInverse(GeometricProcessor.Op(_vectorArray));

            var vectorArray = new VectorStorage<T>[Count];

            for (var i = 0; i < Count; i++)
            {
                //TODO: This can be made more efficient
                var vectorList = _vectorArray.ToList();
                vectorList.RemoveAt(i);

                var b = 
                    GeometricProcessor.Lcp(
                        GeometricProcessor.Op(vectorList),
                        pseudoScalarInv
                    ).GetVectorPart();

                vectorArray[i] = i.IsEven() ? b : GeometricProcessor.Negative(b);
            }

            return new BasisVectorFrame<T>(GeometricProcessor, vectorArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BasisVectorFrame<T> MapAsBasisUsing(Func<GaVector<T>, GaVector<T>> vectorMapping)
        {
            var vectorArray = 
                Count
                    .GetRange()
                    .Select(index => vectorMapping(this[index]).VectorStorage)
                    .ToArray();
            
            return new BasisVectorFrame<T>(GeometricProcessor, vectorArray);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BasisVectorFrame<T> MapAsBasisUsing(Func<int, GaVector<T>, GaVector<T>> vectorMapping)
        {
            var vectorArray = 
                Count
                    .GetRange()
                    .Select(index => vectorMapping(index, this[index]).VectorStorage)
                    .ToArray();
            
            return new BasisVectorFrame<T>(GeometricProcessor, vectorArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BasisVectorFrame<T> MapUsing(IVersor<T> versor)
        {
            var vectorArray = 
                Count
                    .GetRange()
                    .Select(index => versor.OmMap(this[index]).VectorStorage)
                    .ToArray();
            
            return new BasisVectorFrame<T>(GeometricProcessor, vectorArray);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<GaVector<T>> GetEnumerator()
        {
            return _vectorArray.Select(
                v => v.CreateVector(GeometricProcessor)
            ).GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}