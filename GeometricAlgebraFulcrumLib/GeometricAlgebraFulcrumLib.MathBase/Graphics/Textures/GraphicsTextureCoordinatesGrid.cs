using System.Collections;
using DataStructuresLib.Collections;
using DataStructuresLib.Sequences.Periodic1D;
using DataStructuresLib.Sequences.Periodic2D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Textures
{
    public sealed class GraphicsTextureCoordinatesGrid
        : IPeriodicSequence2D<IFloat64Tuple2D>
    {
        public IReadOnlyList<double> TextureURange { get; }

        public IReadOnlyList<double> TextureVRange { get; }

        public int Count1 
            => TextureURange.Count;

        public int Count2 
            => TextureVRange.Count;

        public int Count 
            => TextureURange.Count * TextureVRange.Count;

        public IFloat64Tuple2D this[int index]
        {
            get
            {
                var (index1, index2) = this.GetItemIndexPair(index);

                return new Float64Vector2D(
                    TextureURange[index1],
                    TextureVRange[index2]
                );
            }
        }

        public IFloat64Tuple2D this[int index1, int index2] 
            => new Float64Vector2D(
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


        public PSeqSlice1D<IFloat64Tuple2D> GetSliceAt(int dimension, int index)
        {
            return new PSeqSlice1D<IFloat64Tuple2D>(this, dimension, index);
        }

        public IEnumerator<IFloat64Tuple2D> GetEnumerator()
        {
            foreach (var value1 in TextureURange)
            foreach (var value2 in TextureVRange)
                yield return new Float64Vector2D(value1, value2);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
