using System;

namespace GAPoTNumLib.Text.Structured
{
    public interface IStructuredTextComposer
    {
        string Separator { get; set; }

        string ActiveItemPrefix { get; set; }

        string ActiveItemSuffix { get; set; }

        string FinalPrefix { get; set; }

        string FinalSuffix { get; set; }

        bool ReverseItems { get; set; }

        /// <summary>
        /// Generate the final string using default item conversion method
        /// </summary>
        /// <returns></returns>
        string Generate();

        /// <summary>
        /// Generate the final string using a special conversion function for each item
        /// </summary>
        /// <param name="itemFunc"></param>
        /// <returns></returns>
        string Generate(Func<StructuredTextItem, string> itemFunc);
    }
}