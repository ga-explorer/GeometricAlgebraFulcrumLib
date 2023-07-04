using System.Collections;
using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GraphicsComposerLib.Rendering.Colors
{
    public sealed class GrColorLinearGradientList :
        IReadOnlyDictionary<double, Color>
    {
        public static GrColorLinearGradientList Create()
        {
            return new GrColorLinearGradientList();
        }
        
        public static GrColorLinearGradientList Create(Color color)
        {
            return new GrColorLinearGradientList
            {
                { 0d, color },
                { 1d, color }
            };
        }
        
        public static GrColorLinearGradientList Create(Color color1, Color color2)
        {
            return new GrColorLinearGradientList
            {
                { 0d, color1 },
                { 1d, color2 }
            };
        }
        
        public static GrColorLinearGradientList Create(params Color[] colorArray)
        {
            var keyArray = 0d.GetLinearRange(1d, colorArray.Length, false).ToImmutableArray();

            var colorList = new GrColorLinearGradientList();

            for (var i = 0; i < colorArray.Length; i++) 
                colorList.Add(keyArray[i], colorArray[i]);

            return colorList;
        }


        private SortedDictionary<double, Color> _gradientStopValues;


        public int Count
            => _gradientStopValues.Count;

        public Color this[double key]
        {
            get
            {
                if (Count == 0) return Color.Transparent;

                if (_gradientStopValues.TryGetValue(key, out var color))
                    return color;

                var (key1, color1) = _gradientStopValues.First();
                var (key2, color2) = _gradientStopValues.Last();

                if (key <= key1) return color1;
                if (key >= key2) return color2;

                var foundKey1 = false;
                foreach (var (key3, color3) in _gradientStopValues)
                {
                    if (foundKey1)
                    {
                        key2 = key3;
                        color2 = color3;

                        break;
                    }
                    else
                    {
                        if (key < key3) continue;

                        foundKey1 = true;
                        key1 = key3;
                        color1 = color3;
                    }
                }

                var t = (key - key1) / (key2 - key1);
                return t.RgbaLerp(color1, color2);
            }
        }

        public IEnumerable<double> Keys
            => _gradientStopValues.Keys;

        public IEnumerable<Color> Values
            => _gradientStopValues.Values;


        public GrColorLinearGradientList()
        {
            _gradientStopValues = new SortedDictionary<double, Color>();
        }

        
        public bool IsNormalized()
        {
            if (_gradientStopValues.Count < 2) return false;

            var key1 = _gradientStopValues.Keys.First();
            var key2 = _gradientStopValues.Keys.Last();

            return key1.IsZero() && key2.IsOne();
        }

        public GrColorLinearGradientList Clear()
        {
            _gradientStopValues.Clear();

            return this;
        }

        public bool Remove(double key)
        {
            return _gradientStopValues.Remove(key);
        }

        public bool ContainsKey(double key)
        {
            return _gradientStopValues.ContainsKey(key);
        }

        public GrColorLinearGradientList Add(double key, Color color)
        {
            if (_gradientStopValues.ContainsKey(key))
                _gradientStopValues[key] = color;
            else
                _gradientStopValues.Add(key, color);

            return this;
        }

        public bool TryGetValue(double key, out Color value)
        {
            return _gradientStopValues.TryGetValue(key, out value!);
        }


        public GrColorLinearGradientList Normalize()
        {
            var gradientStopValues = new SortedDictionary<double, Color>();

            if (_gradientStopValues.Count == 0)
            {
                gradientStopValues.Add(0, Color.Transparent);
                gradientStopValues.Add(1, Color.Transparent);

                _gradientStopValues = gradientStopValues;

                return this;
            }

            if (_gradientStopValues.Count == 1)
            {
                var color = _gradientStopValues.Values.First();

                gradientStopValues.Add(0, color);
                gradientStopValues.Add(1, color);

                _gradientStopValues = gradientStopValues;

                return this;
            }

            var key1 = _gradientStopValues.Keys.First();
            var key2 = _gradientStopValues.Keys.Last();

            if (key1.IsZero() && key2.IsOne())
                return this;

            foreach (var (key, color) in _gradientStopValues)
            {
                var newKey = (key - key1) / (key2 - key1);

                gradientStopValues.Add(newKey, color);
            }

            _gradientStopValues = gradientStopValues;

            return this;
        }


        public IEnumerator<KeyValuePair<double, Color>> GetEnumerator()
        {
            return _gradientStopValues.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
