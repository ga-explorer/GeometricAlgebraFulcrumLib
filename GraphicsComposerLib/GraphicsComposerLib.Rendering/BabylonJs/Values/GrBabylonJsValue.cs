using System.Diagnostics.CodeAnalysis;

namespace GraphicsComposerLib.Rendering.BabylonJs.Values
{
    public abstract class GrBabylonJsValue :
        IGrBabylonJsCodeElement
    {
        public string ValueText { get; }

        public abstract bool IsEmpty { get; }


        protected GrBabylonJsValue([NotNull] string valueText)
        {
            ValueText = valueText;
        }


        public abstract string GetCode();
    }

    public abstract class GrBabylonJsValue<T> :
        GrBabylonJsValue
    {
        public T Value { get; }

        public override bool IsEmpty 
            => string.IsNullOrEmpty(ValueText) && Value is null;


        protected GrBabylonJsValue(string valueText) 
            : base(valueText)
        {
        }

        protected GrBabylonJsValue([NotNull] T value)
            : base(string.Empty)
        {
            Value = value;
        }



    }
}
