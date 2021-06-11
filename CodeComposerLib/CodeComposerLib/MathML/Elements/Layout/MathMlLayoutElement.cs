using System.Collections.Generic;

namespace CodeComposerLib.MathML.Elements.Layout
{
    public abstract class MathMlLayoutElement
        : MathMlElement, IMathMlLayoutElement
    {
        public override bool IsToken 
            => false;

        public abstract IEnumerable<IMathMlElement> Contents { get; }
    }
}
