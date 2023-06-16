using System.Collections;
using DataStructuresLib.Collections;
using DataStructuresLib.Sequences.Periodic1D;
using DataStructuresLib.Sequences.Periodic2D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.Textures
{
    public abstract class GraphicsColorsGrid
        : IPeriodicSequence2D<Color>
    {
        public IReadOnlyList<Color> ColorRange1 { get; }

        public IReadOnlyList<Color> ColorRange2 { get; }

        public abstract Color MappingFunction(Color color1, Color color2);

        public int Count1 
            => ColorRange1.Count;

        public int Count2 
            => ColorRange2.Count;

        public int Count 
            => ColorRange1.Count * ColorRange2.Count;

        public Color this[int index]
        {
            get
            {
                var indexPair = this.GetItemIndexPair(index);

                return MappingFunction(
                    ColorRange1[indexPair.Item1],
                    ColorRange2[indexPair.Item2]
                );
            }
        }

        public Color this[int index1, int index2] 
            => MappingFunction(
                ColorRange1[index1],
                ColorRange2[index2]
            );

        public bool IsBasic 
            => true;

        public bool IsOperator 
            => false;


        public GraphicsColorsGrid(IReadOnlyList<Color> colorRange1, IReadOnlyList<Color> colorRange2)
        {
            ColorRange1 = colorRange1 ?? throw new ArgumentNullException(nameof(colorRange1));
            ColorRange2 = colorRange2 ?? throw new ArgumentNullException(nameof(colorRange2));
        }


        public PSeqSlice1D<Color> GetSliceAt(int dimension, int index)
        {
            return new PSeqSlice1D<Color>(this, dimension, index);
        }

        public IEnumerator<Color> GetEnumerator()
        {
            foreach (var value1 in ColorRange1)
            foreach (var value2 in ColorRange2)
                yield return MappingFunction(value1, value2);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}