using System.Diagnostics.CodeAnalysis;
using Humanizer;

namespace TextComposerLib.Code.JavaScript.LibraryComposers
{
    public class JsValueConstantData :
        JsValueData
    {
        public string JsName { get; }

        public string CsName 
            => JsName.Replace('$', '_').Pascalize();
        
        public JsValueData ValueData { get; }


        internal JsValueConstantData([NotNull] string jsName, [NotNull] JsValueData valueData)
            : base(valueData.ValueType)
        {
            JsName = jsName;
            ValueData = valueData;
        }
        

        public override string GetJsCode()
        {
            return JsName;
        }

        public override string GetCsCode()
        {
            return $"ThreeJsConstants.{CsName}";
        }
    }
}