using System.Collections;

namespace WebComposerLib.SvgNew
{
    public sealed class WclSvgElementList :
        IReadOnlyList<WclSvgElement>
    {
        private readonly Dictionary<string, WclSvgElement> _elementDictionary 
            = new Dictionary<string, WclSvgElement>();


        public int Count 
            => _elementDictionary.Count;

        public WclSvgElement this[int index] 
            => _elementDictionary.Skip(index).First().Value;
    
        public WclSvgElement this[string id] 
            => _elementDictionary[id];


        public WclSvgElementList Add(WclSvgElement svgElement)
        {
            _elementDictionary.Add(svgElement.Id, svgElement);

            return this;
        }

        public bool Contains(WclSvgElement svgElement)
        {
            return _elementDictionary.TryGetValue(svgElement.Id, out var svgElement1) && 
                   ReferenceEquals(svgElement, svgElement1);
        }

        public bool TryGetElement(string name, out WclSvgElement? svgElement)
        {
            return _elementDictionary.TryGetValue(name, out svgElement);
        }

        public IEnumerator<WclSvgElement> GetEnumerator()
        {
            return _elementDictionary.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}