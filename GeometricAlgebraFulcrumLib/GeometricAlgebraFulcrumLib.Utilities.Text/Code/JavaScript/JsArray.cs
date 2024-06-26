using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

public class JsArray :
    JsObjectType
{
    internal sealed class JsArrayConstructor :
        JsTypeConstructor
    {
        public JsNumber Size { get; }

        public JsType[] Elements { get; }


        internal JsArrayConstructor()
        {
            Size = null;
            Elements = null;
        }

        internal JsArrayConstructor(JsNumber argSize)
        {
            Size = argSize;
            Elements = null;
        }
            
        internal JsArrayConstructor(JsType[] arrayElements)
        {
            Size = null;
            Elements = arrayElements;
        }

        public override string GetJsCode()
        {
            if (Size is not null)
                return $"new Array({Size.GetJsCode()})";

            if (Elements is not null)
            {
                var elementsCode =
                    Elements.Select(e => e.GetJsCode()).Concatenate(", ");

                return $"[{elementsCode}]";
            }

            return "[]";
        }
    }
        

    public static implicit operator JsArray(JsType[] arrayElements)
    {
        return new JsArray(
            new JsArrayConstructor(arrayElements)
        );
    }

    public static implicit operator JsArray(string jsTextCode)
    {
        return new JsArray(
            new JsTextCodeConstructor(jsTextCode)
        );
    }

    public static implicit operator string(JsArray value)
    {
        return value.GetJsCode();
    }


    private readonly JsArray _jsVariableValue;
    public JsArray JsValue 
        => TypeConstructor.IsVariable ? _jsVariableValue : this;

    public override bool IsVariableWithValue
        => TypeConstructor.IsVariable && _jsVariableValue is not null;

    public override bool IsVariableWithNoValue
        => TypeConstructor.IsVariable && _jsVariableValue is null;


    public JsArray(JsTypeConstructor jsCodeSource, JsArray jsVariableValue = null)
        : base(jsCodeSource)
    {
        if (!(jsCodeSource.IsVariable || jsCodeSource.IsTextCode))
            return;

        _jsVariableValue = jsVariableValue;
    }

    public JsArray()
        : base(new JsArrayConstructor())
    {

    }

    public JsArray(JsNumber argSize)
        : base(new JsArrayConstructor(argSize))
    {

    }

    public JsArray(params JsType[] argElements)
        : base(new JsArrayConstructor(argElements))
    {

    }
}