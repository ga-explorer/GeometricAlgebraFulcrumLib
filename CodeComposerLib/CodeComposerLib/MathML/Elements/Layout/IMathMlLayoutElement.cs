using System.Collections.Generic;

namespace CodeComposerLib.MathML.Elements.Layout
{
    public interface IMathMlLayoutElement : IMathMlElement
    {
        IEnumerable<IMathMlElement> Contents { get; }
    }

    public interface IMathMlLayoutElement<out T> 
        : IMathMlLayoutElement, IReadOnlyList<T> where T : IMathMlElement
    {
        
    }
}