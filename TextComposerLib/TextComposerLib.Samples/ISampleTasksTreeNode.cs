namespace TextComposerLib.WinForms.UserInterface
{
    public interface ISampleTasksTreeNode
    {
        string NodeName { get; }

        string NodeLabel { get; }

        string NodeDescription { get; }

        bool IsTask { get; }

        bool IsTasksCollection { get; }
    }
}