using System.Linq;
using System.Text;

namespace TextComposerLib.Text.Region
{
    internal sealed class RcTemplateMarkers
    {
        /// <summary>
        /// The marker at the beginning of a slot region
        /// </summary>
        public RcLineMarker RegionBeginMarker { get; }

        /// <summary>
        /// The marker at the end of a slot region
        /// </summary>
        public RcLineMarker RegionEndMarker { get; }

        /// <summary>
        /// The marker of a slot tag inside a slot region
        /// </summary>
        public RcLineMarker SlotTagMarker { get; }

        /// <summary>
        /// The marker of begin joining several slot tags into a single multi-line slot tag 
        /// inside a slot region
        /// </summary>
        public RcLineMarker JoinSlotTagsBeginMarker { get; }

        /// <summary>
        /// The marker of end joining several slot tags into a single multi-line slot tag 
        /// inside a slot region
        /// </summary>
        public RcLineMarker JoinSlotTagsEndMarker { get; }

        /// <summary>
        /// The marker of a fixed tag inside a slot region
        /// </summary>
        public RcLineMarker FixedTagMarker { get; }


        internal RcTemplateMarkers()
        {
            RegionBeginMarker = new RcLineMarker("Begin Slot Region");
            RegionEndMarker = new RcLineMarker("End Slot Region");
            SlotTagMarker = new RcLineMarker("Slot Tag");
            JoinSlotTagsBeginMarker = new RcLineMarker("Begin Join Slot Tags");
            JoinSlotTagsEndMarker = new RcLineMarker("End Join Slot Tags");
            FixedTagMarker = new RcLineMarker("Fixed Tag");
        }


        private void TestMarkerOverlap(StringBuilder s, RcLineMarker marker1, RcLineMarker marker2)
        {
            if (marker1.MarkerRegex.IsMatch(marker2.MarkerText))
                s.Append(marker2.MarkerName)
                    .Append(" marker is matched by ")
                    .Append(marker1.MarkerName)
                    .AppendLine(" marker; miss-detection is possible");
        }

        /// <summary>
        /// Verify that all line markers are well defined and have no conflicts
        /// </summary>
        /// <returns></returns>
        public string VerifyMarkers()
        {
            var s = new StringBuilder();

            var markersList = new[]
            {
                RegionBeginMarker, RegionEndMarker, 
                FixedTagMarker, SlotTagMarker, 
                JoinSlotTagsBeginMarker, JoinSlotTagsEndMarker
            };

            foreach (var marker in markersList.Where(m => m.IsReady == false))
                s.Append(marker.MarkerName).AppendLine(" marker not defined");

            if (s.Length > 0)
                return s.ToString();

            for (var i = 0; i < markersList.Length - 1; i++)
            {
                var marker1 = markersList[i];
                for (var j = i + 1; j < markersList.Length; j++)
                {
                    var marker2 = markersList[j];
                    TestMarkerOverlap(s, marker1, marker2);
                }
            }

            return s.ToString();
        }

        /// <summary>
        /// Verify that all tag line markers are well defined and have no conflicts
        /// </summary>
        /// <returns></returns>
        public string VerifyTagMarkers()
        {
            var s = new StringBuilder();

            var markersList = new[]
            {
                FixedTagMarker, SlotTagMarker, 
                JoinSlotTagsBeginMarker, JoinSlotTagsEndMarker
            };

            foreach (var marker in markersList.Where(m => m.IsReady == false))
                s.Append(marker.MarkerName).AppendLine(" marker not defined");

            if (s.Length > 0)
                return s.ToString();

            for (var i = 0; i < markersList.Length - 1; i++)
            {
                var marker1 = markersList[i];
                for (var j = i + 1; j < markersList.Length; j++)
                {
                    var marker2 = markersList[j];
                    TestMarkerOverlap(s, marker1, marker2);
                }
            }

            return s.ToString();
        }
    }
}
