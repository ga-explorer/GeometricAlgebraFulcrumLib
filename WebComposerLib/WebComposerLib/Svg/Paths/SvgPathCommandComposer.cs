using TextComposerLib.Text;

namespace WebComposerLib.Svg.Paths
{
    public sealed class SvgPathCommandComposer : SvgPathCommand
    {
        public static SvgPathCommandComposer Create()
        {
            return new SvgPathCommandComposer();
        }

        public static SvgPathCommandComposer Create(SvgPathCommand command)
        {
            return new SvgPathCommandComposer().AddCommand(command);
        }

        public static SvgPathCommandComposer Create(params SvgPathCommand[] commandsList)
        {
            return new SvgPathCommandComposer().AddCommands(commandsList);
        }

        public static SvgPathCommandComposer Create(IEnumerable<SvgPathCommand> commandsList)
        {
            return new SvgPathCommandComposer().AddCommands(commandsList);
        }


        private readonly List<SvgPathCommand> _commandsList
            = new List<SvgPathCommand>();


        public SvgPathCommand this[int index]
        {
            get => _commandsList[index];
            set
            {
                if (ReferenceEquals(value, null))
                    throw new ArgumentNullException(nameof(value));

                _commandsList[index] = value;
            }
        }

        public IEnumerable<SvgPathCommand> Commands 
            => _commandsList;

        public override string ValueText
            => _commandsList.Select(c => c.ToString()).Concatenate(Environment.NewLine);


        private SvgPathCommandComposer()
        {
        }


        public SvgPathCommandComposer ClearCommands()
        {
            _commandsList.Clear();

            return this;
        }

        public SvgPathCommandComposer AddCommand(SvgPathCommand command)
        {
            if (ReferenceEquals(command, null))
                throw new ArgumentNullException(nameof(command));

            _commandsList.Add(command);

            return this;
        }

        public SvgPathCommandComposer AddCommands(params SvgPathCommand[] commandsList)
        {
            _commandsList.AddRange(
                commandsList.Where(c => !ReferenceEquals(c, null))
            );

            return this;
        }

        public SvgPathCommandComposer AddCommands(IEnumerable<SvgPathCommand> commandsList)
        {
            _commandsList.AddRange(
                commandsList.Where(c => !ReferenceEquals(c, null))
            );

            return this;
        }

        public SvgPathCommandComposer InsertCommand(int index, SvgPathCommand command)
        {
            if (ReferenceEquals(command, null))
                throw new ArgumentNullException(nameof(command));

            _commandsList.Insert(index, command);

            return this;
        }

        public SvgPathCommandComposer InsertCommands(int index, params SvgPathCommand[] commandsList)
        {
            _commandsList.InsertRange(
                index,
                commandsList.Where(c => !ReferenceEquals(c, null))
            );

            return this;
        }

        public SvgPathCommandComposer InsertCommands(int index, IEnumerable<SvgPathCommand> commandsList)
        {
            _commandsList.InsertRange(
                index,
                commandsList.Where(c => !ReferenceEquals(c, null))
            );

            return this;
        }
    }
}
