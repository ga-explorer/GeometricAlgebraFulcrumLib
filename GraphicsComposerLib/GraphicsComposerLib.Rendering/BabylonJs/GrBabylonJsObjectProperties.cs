using System.Text;
using DataStructuresLib.Basic;

namespace GraphicsComposerLib.Rendering.BabylonJs
{
    public abstract class GrBabylonJsObjectProperties :
        IGrBabylonJsCodeElement
    {
        public string? ObjectName { get; set; }


        protected abstract IEnumerable<Pair<string>?> GetNameValuePairs();

        public string GetCode(string objectName)
        {
            ObjectName = objectName;

            return GetCode();
        }

        public string GetCode()
        {
            if (string.IsNullOrEmpty(ObjectName))
                throw new InvalidOperationException();

            var composer = new StringBuilder();

            var valuePairs = 
                GetNameValuePairs().Where(p => p is not null).ToArray();

            if (valuePairs.Length > 0)
            {
                foreach (var (name, value) in valuePairs)
                    composer.AppendLine($"{ObjectName}.{name} = {value};");

                composer.Length -= Environment.NewLine.Length;
            }

            return composer.ToString();
        }
    
        public override string ToString()
        {
            return GetCode();
        }
    }
}