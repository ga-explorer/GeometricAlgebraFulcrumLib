using System.Collections.Generic;

namespace GAPoTNumLib.Text.Parametric
{
    /// <summary>
    /// Any class implementing this interface can be used to fill the values of parameters of a parametric
    /// text composer object
    /// </summary>
    public interface IParametricTextComposerValueSource
    {
        bool ContainsParameter(string paramName);

        bool TryGetParameterValue(string paramName, out string paramValue);

        string GetParameterValue(string paramName);

        Dictionary<string, string> ToParametersDictionary();
    }
}
