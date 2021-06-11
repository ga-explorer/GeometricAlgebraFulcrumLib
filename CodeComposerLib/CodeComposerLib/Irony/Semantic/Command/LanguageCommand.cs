using CodeComposerLib.Irony.Semantic.Scope;

namespace CodeComposerLib.Irony.Semantic.Command
{
    /// <summary>
    /// this is the base class for all language commands
    /// </summary>
    public abstract class LanguageCommand : IronyAstObjectNamed
    {
        public override string ObjectName => "command_" + ObjectId;


        protected LanguageCommand(LanguageScope parentScope)
            : base(parentScope)
        {
        }
    }
}
