using System;

namespace TextComposerLib.WinForms.UserInterface
{
    public sealed class SampleTask<T> : ISampleTasksTreeNode
    {
        public string NodeName { get; set; }

        public string NodeLabel { get; set; }

        public string NodeDescription { get; set; }

        public bool IsTask => true;

        public bool IsTasksCollection => false;

        public Func<T> NodeAction { get; set; }


        public SampleTask()
        {
            NodeName = string.Empty;
            NodeLabel = string.Empty;
            NodeDescription = string.Empty;
        }


        public override string ToString()
        {
            return NodeLabel;
        }
    }
}
