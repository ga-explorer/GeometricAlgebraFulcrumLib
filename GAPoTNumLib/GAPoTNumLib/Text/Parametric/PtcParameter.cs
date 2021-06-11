using System;
using System.Text;

namespace GAPoTNumLib.Text.Parametric
{
    internal sealed class PtcParameter
    {
        internal ParametricTextComposer ParentTemplate { get; }

        internal string ParameterName { get; }

        internal string ParameterValue { get; set; }


        /// <summary>
        /// Return the number of characters in the parameter placeholder inside the parent text template
        /// </summary>
        internal int ParameterPlaceholderLength => ParentTemplate.LeftDelimiter.Length + 
                                                   ParameterName.Length + 
                                                   ParentTemplate.RightDelimiter.Length;

        /// <summary>
        /// Return the full string placeholder of this parameter inside the parent text template
        /// </summary>
        internal string ParameterPlaceholder
        {
            get 
            {
                var s = new StringBuilder(ParameterPlaceholderLength);

                s.Append(ParentTemplate.LeftDelimiter);

                s.Append(ParameterName);

                s.Append(ParentTemplate.RightDelimiter);

                return s.ToString();
            }
        }


        internal PtcParameter(ParametricTextComposer parentTemplate, string parameterName)
        {
            ParentTemplate = parentTemplate;

            ParameterName = parameterName;

            ParameterValue = String.Empty;
        }


        internal void ClearValue()
        {
            ParameterValue = String.Empty;
        }
    }
}
