using System;
using System.Collections;
using System.Collections.Generic;
using DataStructuresLib.Collections;
using DataStructuresLib.Sequences.Periodic1D;
using DataStructuresLib.Sequences.Periodic2D;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.GraphicsGeometry.Textures
{
    public sealed class GraphicsTextureCoordinatesGrid
        : IPeriodicSequence2D<ITuple2D>
    {
        public IReadOnlyList<double> TextureURange { get; }

        public IReadOnlyList<double> TextureVRange { get; }

        public int Count1 
            => TextureURange.Count;

        public int Count2 
            => TextureVRange.Count;

        public int Count 
            => TextureURange.Count * TextureVRange.Count;

        public ITuple2D this[int index]
        {
            get
            {
                var indexPair = this.GetItemIndexPair(index);

                return new Tuple2D(
                    TextureURange[indexPair.Item1],
                    TextureVRange[indexPair.Item2]
                );
            }
        }

        public ITuple2D this[int index1, int index2] 
            => new Tuple2D(
                TextureURange[index1],
                TextureVRange[index2]
            );

        public bool IsBasic 
            => true;

        public bool IsOperator 
            => false;


        public GraphicsTextureCoordinatesGrid(IReadOnlyList<double> textureURange, IReadOnlyList<double> textureVRange)
        {
            TextureURange = textureURange ?? throw new ArgumentNullException(nameof(textureURange));
            TextureVRange = textureVRange ?? throw new ArgumentNullException(nameof(textureVRange));
        }


        public PSeqSlice1D<ITuple2D> GetSliceAt(int dimension, int index)
        {
            return new PSeqSlice1D<ITuple2D>(this, dimension, index);
        }

        public IEnumerator<ITuple2D> GetEnumerator()
        {
            foreach (var value1 in TextureURange)
            foreach (var value2 in TextureVRange)
                yield return new Tuple2D(value1, value2);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
