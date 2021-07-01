using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TextComposerLib.Loggers.Progress;

namespace TextComposerLib.WinForms.UserInterface.UI
{
    public partial class FormProgress : Form
    {
        /// <summary>
        /// This timer object is used to update the user interface at regular intervals
        /// by reading the progress history and adding new progress events to the interface
        /// </summary>
        private readonly System.Windows.Forms.Timer _dispatcherTimer;

        private int _progressIndex;

        private readonly ProgressComposer _progress;

        private readonly Action _startAction;

        private readonly Action _finishAction;

        private readonly List<ProgressEventArgs> _progressEvents;

        private readonly List<ProgressEventArgs> _filteredProgressEvents;

        private bool _filterFlag;


        private List<ProgressEventArgs> VisibleProgressEvents => _filterFlag ? _filteredProgressEvents : _progressEvents;


        public FormProgress(ProgressComposer progressComposer, Action startAction, Action finishAction)
        {
            InitializeComponent();

            _progress = progressComposer;
            _startAction = startAction;
            _finishAction = finishAction;
            _progressEvents = new List<ProgressEventArgs>();
            _filteredProgressEvents = new List<ProgressEventArgs>();

            buttonFilter.Enabled = false;

            if (ReferenceEquals(_startAction, null) == false)
            {
                //  DispatcherTimer setup
                _dispatcherTimer = new System.Windows.Forms.Timer();
                _dispatcherTimer.Tick += dispatcherTimer_Tick;
                _dispatcherTimer.Interval = 2000; //new TimeSpan(0, 0, 2);
            }
            else
            {
                _dispatcherTimer = null;
            }

            StartOperation();
        }

        public FormProgress(ProgressComposer progressComposer)
            : this(progressComposer, null, null)
        {
        }


        private void StartOperation()
        {
            if (ReferenceEquals(_startAction, null))
            {
                FinishOperation();
                return;
            }

            toolStripStatusLabel.Text = @"Running...";

            buttonFilter.Enabled = false;
            buttonClear.Enabled = false;
            buttonStop.Enabled = true;
            //buttonSave.Enabled = false;
            buttonClose.Enabled = false;

            _dispatcherTimer.Start();

            Task.Factory
                .StartNew(
                    _startAction,
                    TaskCreationOptions.LongRunning
                    )
                .ContinueWith(
                    task =>
                    {
                        //Put any UI interface modifications code to be executed after code generation task 
                        //completes here
                        FinishOperation();
                    },
                    CancellationToken.None,
                    TaskContinuationOptions.None,
                    TaskScheduler.FromCurrentSynchronizationContext()
                    );
        }

        private void FinishOperation()
        {
            UpdateProgressHistory();

            if (ReferenceEquals(_finishAction, null) == false)
                _finishAction();

            buttonFilter.Enabled = true;
            buttonClear.Enabled = true;
            buttonStop.Enabled = false;
            //buttonSave.Enabled = true;
            buttonClose.Enabled = true;

            listBoxSources.Items.Clear();
            listBoxSources.Items.AddRange(
                _progressEvents
                .Select(t => t.Source.ProgressSourceId)
                .Distinct()
                .Cast<object>()
                .ToArray()
                );

            toolStripStatusLabel.Text = @"Ready";
        }


        private void UpdateProgressHistory()
        {
            _progressIndex = _progress.History.ReadHistory(_progressIndex, _progressEvents);

            listViewProgressItems.BeginUpdate();
            
            listViewProgressItems.VirtualListSize = VisibleProgressEvents.Count;

            foreach (var c in listViewProgressItems.Columns)
                ((ColumnHeader)c).AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

            listViewProgressItems.EndUpdate();
        }


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            UpdateProgressHistory();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = @"Requesting Stop Running...";
            buttonStop.Enabled = false;
            _progress.RequestStop();
        }

        private void listViewProgressItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var c in listViewProgressItems.Columns)
                ((ColumnHeader)c).AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

            var details = String.Empty;

            var itemsIndices = listViewProgressItems.SelectedIndices;

            if (itemsIndices.Count > 0)
                details = VisibleProgressEvents[itemsIndices[0]].Details ?? String.Empty;

            textBoxProgressItemDetails.Text = details;
        }

        private void listViewProgressItems_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            var eventArgs = VisibleProgressEvents[e.ItemIndex];

            var item = new ListViewItem(eventArgs.ProgressId.ToString())
            {
                Tag = eventArgs
            };

            switch (eventArgs.Kind)
            {
                case ProgressEventArgsKind.Error:
                    item.BackColor = Color.Teal;
                    item.ForeColor = Color.White;
                    break;
                case ProgressEventArgsKind.Warning:
                    item.BackColor = Color.GreenYellow;
                    item.ForeColor = Color.Black;
                    break;
            }

            item.SubItems.AddRange(
                new[] 
                {
                    eventArgs.Source.ProgressSourceId,
                    eventArgs.Title,
                    eventArgs.StartTimeText,
                    eventArgs.DurationText,
                    eventArgs.KindText,
                    eventArgs.ResultText
                });

            e.Item = item;
        }

        private void UpdateProgressListView()
        {
            listViewProgressItems.BeginUpdate();
            
            listViewProgressItems.Clear();
            listViewProgressItems.Columns.AddRange(
                new[] { "#", "Source", "Title", "Time", "Duration", "Kind", "Result" }
                .Select(t => new ColumnHeader() { Text = t })
                .ToArray()
                );

            listViewProgressItems.VirtualListSize = VisibleProgressEvents.Count;

            foreach (var c in listViewProgressItems.Columns)
                ((ColumnHeader)c).AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

            listViewProgressItems.EndUpdate();
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            _filterFlag = true;

            _filteredProgressEvents.Clear();

            var titlePartFilter = textBoxTitle.Text.Trim().ToLower();

            var sourcesFilter = new List<string>(
                listBoxSources
                .SelectedItems
                .Cast<string>()
                );

            var selectedEvents = (IEnumerable<ProgressEventArgs>) _progressEvents;

            if (sourcesFilter.Count > 0)
            {
                selectedEvents = selectedEvents.Where(p => sourcesFilter.Contains(p.Source.ProgressSourceId));

                if (String.IsNullOrEmpty(titlePartFilter) == false)
                    selectedEvents = selectedEvents.Where(p => p.Title.ToLower().Contains(titlePartFilter));

                _filteredProgressEvents.AddRange(selectedEvents);
            }

            if (_filteredProgressEvents.Count == _progressEvents.Count)
            {
                _filterFlag = false;
                _filteredProgressEvents.Clear();
            }

            UpdateProgressListView();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            _filterFlag = false;
            _filteredProgressEvents.Clear();

            UpdateProgressListView();
        }
    }
}
