﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataStructuresLib.Permutations;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors;
using GeometricAlgebraFulcrumLib.Geometry.Subspaces;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Geometry.Frames
{
    public sealed class GeoFreeFrame<T> : 
        IReadOnlyList<VectorStorage<T>>, 
        IGeometricAlgebraElement<T>
    {
        private readonly List<VectorStorage<T>> _vectorStoragesList;


        public int Count 
            => _vectorStoragesList.Count;

        public uint VSpaceDimension 
            => GeometricProcessor.VSpaceDimension;

        public ulong GaSpaceDimension
            => GeometricProcessor.GaSpaceDimension;
        

        public IScalarAlgebraProcessor<T> ScalarProcessor 
            => GeometricProcessor;

        public ILinearAlgebraProcessor<T> LinearProcessor 
            => GeometricProcessor;

        public IGeometricAlgebraProcessor<T> GeometricProcessor { get; }

        public VectorStorage<T> this[int index]
        {
            get => _vectorStoragesList[index];
            set => _vectorStoragesList[index] = 
                value 
                ?? throw new ArgumentNullException(nameof(value));
        }

        public bool IsValid
            => true;

        public bool IsInvalid
            => false;

        public GeoFreeFrameKind FrameKind { get; }


        internal GeoFreeFrame([NotNull] IGeometricAlgebraProcessor<T> processor, GeoFreeFrameKind frameKind)
        {
            _vectorStoragesList = new List<VectorStorage<T>>();

            GeometricProcessor = processor;
            FrameKind = frameKind;
        }

        internal GeoFreeFrame([NotNull] IGeometricAlgebraProcessor<T> processor, GeoFreeFrameKind frameKind, [NotNull] IEnumerable<VectorStorage<T>> vectorStoragesList)
        {
            _vectorStoragesList = vectorStoragesList.ToList();

            GeometricProcessor = processor;
            FrameKind = frameKind;
        }

        
        public GeoFreeFrame<T> AppendVector(VectorStorage<T> vector)
        {
            _vectorStoragesList.Add(vector);

            return this;
        }
        
        public GeoFreeFrame<T> AppendVectors(params VectorStorage<T>[] vectorsList)
        {
            foreach (var vector in vectorsList)
                _vectorStoragesList.Add(vector);

            return this;
        }
        
        public GeoFreeFrame<T> PrependVector(VectorStorage<T> vector)
        {
            _vectorStoragesList.Insert(0, vector);

            return this;
        }
        
        public GeoFreeFrame<T> InsertVector(int index, VectorStorage<T> vector)
        {
            _vectorStoragesList.Insert(index, vector);

            return this;
        }


        public GeoFreeFrame<T> GetSubFrame(int startIndex, int count)
        {
            return new GeoFreeFrame<T>(
                GeometricProcessor,
                FrameKind,
                _vectorStoragesList
                    .Skip(startIndex)
                    .Take(count)
            );
        }

        public GeoFreeFrame<T> GetNegativeFrame()
        {
            return new GeoFreeFrame<T>(
                GeometricProcessor,
                FrameKind,
                _vectorStoragesList.Select(v => v.GetVectorPart(GeometricProcessor.Negative))
            );
        }

        public GeoFreeFrame<T> GetOrthogonalFrame(bool makeUnitVectors)
        {
            if (FrameKind == GeoFreeFrameKind.OrthogonalUnitVectors)
                return this;

            if (FrameKind == GeoFreeFrameKind.Orthogonal && !makeUnitVectors)
                return this;

            var orthogonalVector = _vectorStoragesList[0];
            var vectorStoragesList = new List<VectorStorage<T>>
            {
                makeUnitVectors
                    ? GeometricProcessor.Divide(
                        orthogonalVector, 
                        GeometricProcessor.ENorm(orthogonalVector)
                    )
                    : orthogonalVector
            };

            var mv1 = (IMultivectorStorage<T>) orthogonalVector;
            
            for (var i = 1; i < _vectorStoragesList.Count; i++)
            {
                var mv2 = 
                    GeometricProcessor.Op(mv1, _vectorStoragesList[i]);
                
                orthogonalVector = 
                    GeometricProcessor.EGp(GeometricProcessor.Reverse(mv1), mv2).GetVectorPart();
                
                vectorStoragesList.Add( 
                    makeUnitVectors
                        ? GeometricProcessor.Divide(
                            orthogonalVector, 
                            GeometricProcessor.ENorm(orthogonalVector)
                        )
                        : orthogonalVector
                    );
                
                mv1 = mv2;
            }

            return new GeoFreeFrame<T>(
                GeometricProcessor, 
                makeUnitVectors 
                    ? GeoFreeFrameKind.OrthogonalUnitVectors 
                    : GeoFreeFrameKind.Orthogonal,
                vectorStoragesList
            );
        }

        public GeoFreeFrame<T> GetSwappedPairsFrame()
        {
            var frame = new GeoFreeFrame<T>(GeometricProcessor, FrameKind);

            //Swap each pair of two consecutive vectors in the frame
            for (var i = 0; i < _vectorStoragesList.Count - 1; i += 2)
            {
                frame.AppendVector(_vectorStoragesList[i + 1]);
                frame.AppendVector(_vectorStoragesList[i]);
            }

            if (_vectorStoragesList.Count % 2 == 1)
            {
                //To keep the same handedness we count the number of swaps and
                //negate the final vector if the number is odd

                var numberOfSwaps = (_vectorStoragesList.Count - 1) / 2;

                var lastVector = numberOfSwaps % 2 == 0
                    ? _vectorStoragesList[^1]
                    : _vectorStoragesList[^1].GetVectorPart(GeometricProcessor.Negative);

                frame.AppendVector(lastVector);
            }

            return frame;
        }

        public GeoSubspace<T> GetSubspace()
        {
            //TODO: Modify this to find the outer product of the basis from vectors _vectorStoragesList
            return GeoSubspace<T>.Create(
                GeometricProcessor,
                GeometricProcessor.Op(_vectorStoragesList)
            );
        }

        public bool IsOrthonormal()
        {
            for (var i = 0; i < Count; i++)
            {
                var v1 = _vectorStoragesList[i];

                var dii = GeometricProcessor.Subtract(
                    GeometricProcessor.ESp(v1), 
                    GeometricProcessor.ScalarOne
                );

                if (!GeometricProcessor.IsNearZero(dii))
                    return false;

                for (var j = i + 1; j < Count; j++)
                {
                    var dij = 
                        GeometricProcessor.ESp(v1, _vectorStoragesList[j]);

                    if (!GeometricProcessor.IsNearZero(dij))
                        return false;
                }
            }

            return true;
        }

        public bool HasSameHandedness(GeoFreeFrame<T> targetFrame)
        {
            var ps1 = GetSubspace();
            var ps2 = targetFrame.GetSubspace();
            var s = GeometricProcessor.Subtract(ps1.Blade, ps2.Blade);

            //var s = GetPseudoScalar() - targetFrame.GetPseudoScalar();

            return GeometricProcessor.IsZero(s);
        }

        public IEnumerable<Rotor<T>> GetRotorsToFrame(GeoFreeFrame<T> targetFrame)
        {
            Debug.Assert(targetFrame.Count == Count);
            Debug.Assert(IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(HasSameHandedness(targetFrame));

            var inputFrame = new VectorStorage<T>[Count];

            for (var i = 0; i < Count; i++)
                inputFrame[i] = _vectorStoragesList[i];
            
            for (var i = 0; i < Count - 1; i++)
            {
                var rotor = 
                    Rotor<T>.CreateSimpleRotor(
                        GeometricProcessor, 
                        inputFrame[i], 
                        targetFrame[i]
                    );

                yield return rotor;

                for (var j = i + 1; j < Count; j++)
                    inputFrame[j] = rotor.OmMapVector(inputFrame[j]);
            }
        }

        public IEnumerable<Rotor<T>> GetRotorsToFrame(GeoFreeFrame<T> targetFrame, params int[] basisRotationOrderList)
        {
            Debug.Assert(targetFrame.Count == Count);
            Debug.Assert(IsOrthonormal() && targetFrame.IsOrthonormal());
            Debug.Assert(HasSameHandedness(targetFrame));
            //Debug.Assert(GeoPoTNumUtils.ValidateIndexPermutationList(basisRotationOrderList));

            var inputFrame = new VectorStorage<T>[Count];

            for (var i = 0; i < Count; i++)
                inputFrame[i] = _vectorStoragesList[i];
            
            for (var i = 0; i < Count - 1; i++)
            {
                var vectorIndex = basisRotationOrderList[i];

                var rotor = 
                    Rotor<T>.CreateSimpleRotor(
                        GeometricProcessor,
                        inputFrame[vectorIndex], 
                        targetFrame[vectorIndex]
                    );

                yield return rotor;

                for (var j = i + 1; j < Count; j++)
                {
                    var vectorIndex1 = basisRotationOrderList[j];

                    inputFrame[vectorIndex1] = 
                        rotor.OmMapVector(inputFrame[vectorIndex1]);
                }
            }
        }

        public IEnumerable<T> GetAnglesToFrame(GeoFreeFrame<T> targetFrame)
        {
            Debug.Assert(targetFrame.Count == Count);

            for (var i = 0; i < Count; i++)
            {
                var v1 = _vectorStoragesList[i];
                var v2 = targetFrame[i];

                yield return GeometricProcessor.ArcCos(
                    GeometricProcessor.Divide(
                        GeometricProcessor.ESp(v1, v2),
                        GeometricProcessor.Sqrt(
                            GeometricProcessor.Times(GeometricProcessor.ESp(v1), GeometricProcessor.ESp(v2))
                        )
                    )
                );
            }
        }

        public IEnumerable<GeoFreeFrame<T>> GetFramePermutations()
        {
            var indexPermutationsList = 
                PermutationsUtils.GetIndexPermutations(Count);

            foreach (var indexPermutation in indexPermutationsList)
            {
                var frame = new GeoFreeFrame<T>(GeometricProcessor, FrameKind);

                foreach (var index in indexPermutation)
                    frame.AppendVector(_vectorStoragesList[index]);

                yield return frame;
            }
        }

        public GeoFreeFrame<T> GetProjectionOnFrame(GeoFreeFrame<T> frame)
        {
            var ps = frame.GetSubspace();

            return new GeoFreeFrame<T>(
                GeometricProcessor,
                GeoFreeFrameKind.Undefined, 
                _vectorStoragesList.Select(v => 
                    ps.Project(v).GetVectorPart()
                )
            );
        }

        public GeoFreeFrame<T> Normalize()
        {
            if (FrameKind.IsUnitVectors())
                return this;

            var frameKind = FrameKind == GeoFreeFrameKind.LinearlyIndependent
                ? GeoFreeFrameKind.UnitVectors
                : GeoFreeFrameKind.OrthogonalUnitVectors;

            return new GeoFreeFrame<T>(
                GeometricProcessor,
                frameKind,
                _vectorStoragesList.Select(v => GeometricProcessor.DivideByENorm(v))
            );
        }

        public T[,] GetMatrix()
        {
            return GetMatrix((int) VSpaceDimension);
        }

        public T[,] GetMatrix(int rowsCount)
        {
            var colsCount = Count;
            var itemsArray = 
                GeometricProcessor.CreateArrayZero2D(rowsCount, colsCount);

            for (var j = 0; j < Count; j++)
            {
                var vectorTerms = 
                    _vectorStoragesList[j].GetLinVectorIndexScalarStorage()
                        .GetIndexScalarRecords()
                        .Where(pair => pair.Index < (ulong) rowsCount);

                foreach (var (index, scalar) in vectorTerms)
                    itemsArray[index, j] = scalar;
            }
            
            return itemsArray;
        }

        public T[,] GetInnerProductsMatrix()
        {
            var ipm = new T[Count, Count];

            for (var i = 0; i < Count; i++)
            {
                var v1 = _vectorStoragesList[i];

                ipm[i, i] = GeometricProcessor.Sp(v1);

                for (var j = i + 1; j < Count; j++)
                {
                    var ip = GeometricProcessor.Sp(v1, _vectorStoragesList[j]);

                    ipm[i, j] = ip;
                    ipm[j, i] = ip;
                }
            }

            return ipm;
        }

        public T[,] GetInnerAnglesMatrix()
        {
            var ipm = new T[Count, Count];

            for (var i = 0; i < Count; i++)
            {
                var v1 = _vectorStoragesList[i];

                for (var j = i + 1; j < Count; j++)
                {
                    var ip = GeometricProcessor.GetAngle(v1, _vectorStoragesList[j]);

                    ipm[i, j] = ip;
                    ipm[j, i] = ip;
                }
            }

            return ipm;
        }

        public T[,] GetInnerAnglesInDegreesMatrix()
        {
            var ipm = new T[Count, Count];

            for (var i = 0; i < Count; i++)
            {
                var v1 = _vectorStoragesList[i];

                for (var j = i + 1; j < Count; j++)
                {
                    var ip = GeometricProcessor.GetAngle(v1, _vectorStoragesList[j]);

                    ipm[i, j] = ip;
                    ipm[j, i] = ip;
                }
            }

            return ipm;
        }

        public IEnumerator<VectorStorage<T>> GetEnumerator()
        {
            return _vectorStoragesList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}