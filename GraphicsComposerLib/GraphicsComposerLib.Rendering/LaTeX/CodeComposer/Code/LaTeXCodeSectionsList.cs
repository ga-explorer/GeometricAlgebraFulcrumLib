using System.Collections;
using GraphicsComposerLib.Rendering.LaTeX.CodeComposer.Code.Commands;
using TextComposerLib.Text.Linear;

namespace GraphicsComposerLib.Rendering.LaTeX.CodeComposer.Code
{
    public class LaTeXCodeSectionsList : ILaTeXCodeSection, IEnumerable<ILaTeXCodeSection>
    {
        protected List<ILaTeXCodeSection> SubSectionsList { get; }


        public IEnumerable<ILaTeXCodeSection> SubSections
            => SubSectionsList;

        public IEnumerable<ILaTeXCommand> SectionCommands
        {
            get
            {
                var stack = new Stack<ILaTeXCodeSection>();

                foreach (var subSection in SubSectionsList)
                    stack.Push(subSection);

                while (stack.Count > 0)
                {
                    var subSection = stack.Pop();

                    var command = subSection as ILaTeXCommand;

                    if (!ReferenceEquals(command, null))
                    {
                        yield return command;
                        continue;
                    }

                    foreach (var subSubSection in subSection.SubSections)
                        stack.Push(subSubSection);
                }
            }
        }

        public int Count 
            => SubSectionsList.Count;

        public int CommandsCount 
            => SectionCommands.Count();

        public bool HasSubSections
            => SubSectionsList.Count > 0;

        public bool HasCommands
            => SectionCommands.Any(command => !ReferenceEquals(command, null));

        public ILaTeXCodeSection this[int index]
        {
            get => SubSectionsList[index];
            set
            {
                if (ReferenceEquals(value, null))
                    throw new ArgumentNullException(nameof(value));

                SubSectionsList[index] = value;
            }
        }

        public ILaTeXCommand FirstCommand
            => SectionCommands.FirstOrDefault();

        public ILaTeXCommand LastCommand
            => SectionCommands.LastOrDefault();


        public LaTeXCodeSectionsList()
        {
            SubSectionsList = new List<ILaTeXCodeSection>();
        }

        public LaTeXCodeSectionsList(int capacity)
        {
            SubSectionsList = new List<ILaTeXCodeSection>(capacity);
        }

        public LaTeXCodeSectionsList(IEnumerable<ILaTeXCommand> items)
        {
            SubSectionsList = new List<ILaTeXCodeSection>(items);
        }

        public LaTeXCodeSectionsList(params ILaTeXCommand[] items)
        {
            SubSectionsList = new List<ILaTeXCodeSection>(items);
        }


        public LaTeXCodeSectionsList Clear()
        {
            SubSectionsList.Clear();

            return this;
        }

        public LaTeXCodeSectionsList Remove(int index)
        {
            SubSectionsList.RemoveAt(index);

            return this;
        }

        public LaTeXCodeSectionsList Add(ILaTeXCodeSection subSection)
        {
            if (ReferenceEquals(subSection, null))
                throw new ArgumentNullException(nameof(subSection));

            SubSectionsList.Add(subSection);

            return this;
        }

        public LaTeXCodeSectionsList AddSubSections(IEnumerable<ILaTeXCodeSection> subSectionsList)
        {
            if (ReferenceEquals(subSectionsList, null))
                throw new ArgumentNullException(nameof(subSectionsList));

            SubSectionsList.AddRange(
                subSectionsList.Where(s => !ReferenceEquals(s, null))
            );

            return this;
        }

        public LaTeXCodeSectionsList AddSubSections(params ILaTeXCodeSection[] subSectionsList)
        {
            if (ReferenceEquals(subSectionsList, null))
                throw new ArgumentNullException(nameof(subSectionsList));

            SubSectionsList.AddRange(
                subSectionsList.Where(s => !ReferenceEquals(s, null))
            );

            return this;
        }

        public LaTeXCodeSectionsList Insert(int index, ILaTeXCodeSection subSection)
        {
            if (ReferenceEquals(subSection, null))
                throw new ArgumentNullException(nameof(subSection));

            SubSectionsList.Insert(index, subSection);

            return this;
        }

        public LaTeXCodeSectionsList InsertSubSections(int index, IEnumerable<ILaTeXCodeSection> subSectionsList)
        {
            if (ReferenceEquals(subSectionsList, null))
                throw new ArgumentNullException(nameof(subSectionsList));

            SubSectionsList.InsertRange(
                index,
                subSectionsList.Where(s => !ReferenceEquals(s, null))
            );

            return this;
        }

        public LaTeXCodeSectionsList InsertSubSections(int index, params ILaTeXCodeSection[] subSectionsList)
        {
            if (ReferenceEquals(subSectionsList, null))
                throw new ArgumentNullException(nameof(subSectionsList));

            SubSectionsList.InsertRange(
                index,
                subSectionsList.Where(s => !ReferenceEquals(s, null))
            );

            return this;
        }


        public int FindIndex(Predicate<ILaTeXCodeSection> predicate)
        {
            return SubSectionsList.FindIndex(predicate);
        }

        public IEnumerable<LaTeXCommandOneArg> GetCommandsOneArg(string commandName)
        {
            return SubSectionsList
                .Select(c => c as LaTeXCommandOneArg)
                .Where(c => !ReferenceEquals(c, null) && c.CommandName == commandName);
        }

        public LaTeXCommandOneArg FindOrAddCommandOneArg(string commandName)
        {
            var command = GetCommandsOneArg(commandName).FirstOrDefault();

            if (!ReferenceEquals(command, null))
                return command;

            command = LaTeXCommandOneArg.Create(commandName);

            SubSectionsList.Add(command);

            return command;
        }


        public bool IsEmpty()
        {
            return CommandsCount == 0;
        }

        public void ToText(LinearTextComposer composer)
        {
            foreach (var command in SectionCommands)
                command.ToText(composer);
        }

        public IEnumerator<ILaTeXCodeSection> GetEnumerator()
        {
            return SubSectionsList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return SubSectionsList.GetEnumerator();
        }
    }
}
