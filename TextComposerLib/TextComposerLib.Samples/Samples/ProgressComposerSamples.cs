using System;
using TextComposerLib.Logs.Progress;

namespace TextComposerLib.Samples.Samples
{
    public static class ProgressComposerSamples
    {
        internal class ProgressClient : IProgressReportSource
        {
            public string ProgressSourceId 
            {
                get { return "Progress Client"; }
            }

            public ProgressComposer Progress { get; private set; }


            public ProgressClient()
            {
                Progress = new ProgressComposer();
            }
        }

        internal static string Task1()
        {
            var client = new ProgressClient();

            var task1Id = client.ReportStart("Task 1");

            Sleep(1);

            client.ReportNormal("Progress event 1");

            Sleep(1);

            client.ReportNormal("Progress event 2");

            Sleep(1);

            var task2Id = client.ReportStart("Task 2");

            Sleep(1);

            client.ReportFinish(task2Id);

            Sleep(1);

            client.ReportFinish(task1Id);

            return client.Progress.History.ToString();
        }

        private static void Sleep(int seconds)
        {
            var time = DateTime.Now;

            while ((DateTime.Now - time).Seconds < seconds) ;
        }


    }
}
