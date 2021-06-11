using System;
using System.Collections.Generic;

namespace TextComposerLib.WinForms.UserInterface
{
    public sealed class SampleTasksCollection : List<ISampleTasksTreeNode>, ISampleTasksTreeNode
    {
        public string NodeName { get; set; }

        public string NodeLabel { get; set; }

        public string NodeDescription { get; set; }

        public bool IsTask => false;

        public bool IsTasksCollection => true;


        public SampleTasksCollection()
        {
            NodeName = string.Empty;
            NodeLabel = string.Empty;
            NodeDescription = string.Empty;
        }


        public SampleTasksCollection AddCollection(string name, string label, string description)
        {
            var childNode = new SampleTasksCollection
            {
                NodeName = name,
                NodeLabel = label,
                NodeDescription = description
            };

            Add(childNode);

            return childNode;
        }

        public SampleTasksCollection AddCollection(string label, string description)
        {
            var childNode = new SampleTasksCollection
            {
                NodeLabel = label,
                NodeDescription = description
            };

            Add(childNode);

            return childNode;
        }

        public SampleTasksCollection AddCollection(string label)
        {
            var childNode = new SampleTasksCollection
            {
                NodeLabel = label
            };

            Add(childNode);

            return childNode;
        }

        public SampleTask<T> AddTask<T>(string name, string label, string description, Func<T> action)
        {
            var childNode = new SampleTask<T>
            {
                NodeName = name,
                NodeLabel = label,
                NodeDescription = description,
                NodeAction = action
            };

            Add(childNode);

            return childNode;
        }

        public SampleTask<T> AddTask<T>(string label, string description, Func<T> action)
        {
            var childNode = new SampleTask<T>
            {
                NodeLabel = label,
                NodeDescription = description,
                NodeAction = action
            };

            Add(childNode);

            return childNode;
        }

        public SampleTask<T> AddTask<T>(string label, Func<T> action)
        {
            var childNode = new SampleTask<T>
            {
                NodeLabel = label,
                NodeAction = action
            };

            Add(childNode);

            return childNode;
        }
    }
}
