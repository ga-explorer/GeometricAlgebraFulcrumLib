//using System.Diagnostics.CodeAnalysis;

//namespace GraphicsComposerLib.Rendering.BabylonJs.Values
//{
//    public abstract class GrBabylonJsValue :
//        IGrBabylonJsCodeElement
//    {
//        public string ValueText { get; }

//        public abstract bool IsEmpty { get; }


//        protected GrBabylonJsValue(string valueText)
//        {
//            ValueText = valueText;
//        }


//        public abstract string GetCode();

//        public override string ToString()
//        {
//            return GetCode();
//        }
//    }

//    public abstract class SparseCodeAttributeValue<T> :
//        GrBabylonJsValue
//    {
//        public T Value { get; }

//        public override bool IsEmpty 
//            => string.IsNullOrEmpty(ValueText) && Value is null;


//        protected GrBabylonJsValue(string valueText) 
//            : base(valueText)
//        {
//        }

//        protected GrBabylonJsValue([NotNull] T value)
//            : base(string.Empty)
//        {
//            Value = value;
//        }



//    }
//}
