using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TextComposerLib.WinForms.UserInterface
{
    public partial class FormSamples : Form
    {
        public FormSamples(ISampleTasksTreeNode node)
        {
            InitializeComponent();

            AddTreeViewNode(node);
        }

        public FormSamples(params ISampleTasksTreeNode[] rootTasksTreeNodesList)
        {
            InitializeComponent();

            foreach (var node in rootTasksTreeNodesList)
                AddTreeViewNode(node);
        }

        public FormSamples(IEnumerable<ISampleTasksTreeNode> rootTasksTreeNodesList)
        {
            InitializeComponent();

            foreach (var node in rootTasksTreeNodesList)
                AddTreeViewNode(node);
        }


        private void AddTreeViewNode(ISampleTasksTreeNode taskNode, TreeNode parentNode = null)
        {
            var tvNodesCollection =
                ReferenceEquals(parentNode, null)
                    ? treeViewSamples.Nodes
                    : parentNode.Nodes;

            var childNode = tvNodesCollection.Add(
                string.IsNullOrEmpty(taskNode.NodeName) ? taskNode.NodeLabel : taskNode.NodeName, 
                taskNode.NodeLabel
                );

            childNode.Tag = taskNode;

            if (taskNode.IsTask) 
                return;

            var taskCollection = (SampleTasksCollection) taskNode;

            foreach (var childTaskNode in taskCollection)
                AddTreeViewNode(childTaskNode, childNode);
        }

        private ISampleTasksTreeNode GetSelectedTaskTreeNode()
        {
            var node = treeViewSamples.SelectedNode;

            return
                ReferenceEquals(node, null)
                ? null
                : node.Tag as ISampleTasksTreeNode;
        }

        private SampleTask<string> GetSelectedTask()
        {
            var node = treeViewSamples.SelectedNode;

            return
                ReferenceEquals(node, null)
                ? null
                : node.Tag as SampleTask<string>;
        }


        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonExec_Click(object sender, EventArgs e)
        {
            textBoxResults.Text = string.Empty;

            var task = GetSelectedTask();

            if (ReferenceEquals(task, null))
            {
                MessageBox.Show(
                    @"Please select a task that can return a string value to execute",
                    @"Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                    );

                return;
            }

            string taskResult;

            statusLabel.Text = @"Executing Task...";
            Application.DoEvents();
            var startTime = DateTime.Now;
            try
            {
                taskResult = task.NodeAction();
            }
            catch (Exception error)
            {
                taskResult = error.Message;
            }
            var finishTime = DateTime.Now;
            var taskTime = finishTime - startTime;
            var startTimeText = startTime.ToString("O");
            var separatorLine = "".PadLeft(startTimeText.Length + 20, '-');
            statusLabel.Text = @"Ready";

            textBoxResults.Text =
                new StringBuilder()
                    .Append("Task started at: ")
                    .AppendLine(startTimeText)
                    .AppendLine(separatorLine)
                    .AppendLine()
                    .AppendLine(taskResult)
                    .AppendLine()
                    .AppendLine(separatorLine)
                    .Append("Task finished at: ")
                    .AppendLine(finishTime.ToString("O"))
                    .Append("Task total time: ")
                    .AppendLine(taskTime.ToString("G"))
                    .ToString();
        }

        private void treeViewSamples_AfterSelect(object sender, TreeViewEventArgs e)
        {
            textBoxResults.Text = string.Empty;
            textBoxTaskDescription.Text = string.Empty;

            var task = GetSelectedTaskTreeNode();

            if (ReferenceEquals(task, null))
                return;

            textBoxTaskDescription.Text = task.NodeDescription;
        }
    }
}
