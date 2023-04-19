using System.Text;
using DataStructuresLib.Basic;

namespace GraphicsComposerLib.Rendering.BabylonJs
{
    public abstract class GrBabylonJsObjectOptions :
        IGrBabylonJsCodeElement
    {
        protected abstract IEnumerable<Pair<string>?> GetNameValuePairs();

        public string GetCode()
        {
            var composer = new StringBuilder();

            composer.Append("{");

            var valuePairs = 
                GetNameValuePairs().Where(p => p is not null);

            foreach (var (name, value) in valuePairs)
                composer.Append($"{name}: {value}, ");

            if (composer.Length > 2)
                composer.Length -= 2;

            composer.Append("}");

            return composer.ToString();
        }
    
        public override string ToString()
        {
            return GetCode();
        }
    }
}