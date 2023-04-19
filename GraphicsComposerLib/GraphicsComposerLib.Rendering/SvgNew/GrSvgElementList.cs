using System.Collections;

namespace GraphicsComposerLib.Rendering.SvgNew
{
    public sealed class GrSvgElementList :
        IReadOnlyList<GrSvgElement>
    {
        private readonly Dictionary<string, GrSvgElement> _elementDictionary 
            = new Dictionary<string, GrSvgElement>();


        public int Count 
            => _elementDictionary.Count;

        public GrSvgElement this[int index] 
            => _elementDictionary.Skip(index).First().Value;
    
        public GrSvgElement this[string id] 
            => _elementDictionary[id];


        public GrSvgElementList Add(GrSvgElement svgElement)
        {
            _elementDictionary.Add(svgElement.Id, svgElement);

            return this;
        }

        public bool Contains(GrSvgElement svgElement)
        {
            return _elementDictionary.TryGetValue(svgElement.Id, out var svgElement1) && 
                   ReferenceEquals(svgElement, svgElement1);
        }

        public bool TryGetElement(string name, out GrSvgElement? svgElement)
        {
            return _elementDictionary.TryGetValue(name, out svgElement);
        }

        public IEnumerator<GrSvgElement> GetEnumerator()
        {
            return _elementDictionary.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}