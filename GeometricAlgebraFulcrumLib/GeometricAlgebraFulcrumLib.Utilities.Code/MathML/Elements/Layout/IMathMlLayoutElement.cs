using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Elements.Layout;

public interface IMathMlLayoutElement : IMathMlElement
{
    IEnumerable<IMathMlElement> Contents { get; }
}

public interface IMathMlLayoutElement<out T> 
    : IMathMlLayoutElement, IReadOnlyList<T> where T : IMathMlElement
{
        
}