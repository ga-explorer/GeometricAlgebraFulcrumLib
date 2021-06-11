namespace CodeComposerLib.Irony.DSLInterpreter
{
    public interface ILanguageInterpreter<TSymbolData>
    {
        /// <summary>
        /// The step number for evaluation process
        /// </summary>
        int StepNumber { get; }

        /// <summary>
        /// A helper object used for performing basic access operations on values using LanguageValueAccess objects
        /// </summary>
        LanguageValueAccessPrecessor ValueAccessProcessor { get; }

        /// <summary>
        /// The current active activation record
        /// </summary>
        ActivationRecord<TSymbolData> ActiveAr { get; }
    }
}
