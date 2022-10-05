namespace GraphicsComposerLib.Rendering.LaTeX.CodeComposer.Code.Commands
{
    /// <summary>
    /// This abstract class represents a single LaTeX command block possible having inner commands,
    /// and may contain arguments
    /// </summary>
    public abstract class LaTeXCommandBlock : LaTeXCommand, ILaTeXCommandBlock
    {
        public LaTeXCodeSectionsList SubSectionsList { get; }
            = new LaTeXCodeSectionsList();

        public string ClosingName { get; }

        public override IEnumerable<ILaTeXCodeSection> SubSections
            => SubSectionsList;

        public IEnumerable<ILaTeXCommand> InnerCommands 
            => SubSectionsList.SectionCommands;


        protected LaTeXCommandBlock(string commandName) 
            : base(commandName) 
        {
            ClosingName = string.Empty;
        }

        protected LaTeXCommandBlock(string commandName, string closingName)
            : base(commandName)
        {
            ClosingName = closingName;
        }
    }
}