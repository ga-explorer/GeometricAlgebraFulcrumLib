using System;
using System.Linq;
using GAPoTNumLib.Structures;

namespace GAPoTNumLib.Text.Structured
{
    public sealed class PriorityQueueTextComposer<TPriority> : PriorityQueue<TPriority, StructuredTextItem>, IStructuredTextComposer
    {
        public string Separator { get; set; }

        public string ActiveItemPrefix { get; set; }

        public string ActiveItemSuffix { get; set; }

        public string FinalPrefix { get; set; }

        public string FinalSuffix { get; set; }

        public bool ReverseItems { get; set; }


        public PriorityQueueTextComposer()
        {
            Separator = string.Empty;
            ActiveItemPrefix = string.Empty;
            ActiveItemSuffix = string.Empty;
            FinalPrefix = string.Empty;
            FinalSuffix = string.Empty;
        }

        public PriorityQueueTextComposer(string separator)
        {
            Separator = separator ?? string.Empty;
            ActiveItemPrefix = string.Empty;
            ActiveItemSuffix = string.Empty;
            FinalPrefix = string.Empty;
            FinalSuffix = string.Empty;
        }


        public PriorityQueueTextComposer<TPriority> Enqueue(TPriority priority)
        {
            base.Enqueue(priority, this.ToTextItem(string.Empty));

            return this;
        }

        public PriorityQueueTextComposer<TPriority> Enqueue(TPriority priority, string item)
        {
            base.Enqueue(priority, this.ToTextItem(item));

            return this;
        }

        public PriorityQueueTextComposer<TPriority> Enqueue<T>(TPriority priority, T item)
        {
            base.Enqueue(priority, this.ToTextItem(item));

            return this;
        }


        public string Generate()
        {
            var items = ReverseItems ? this.Reverse() : this;

            return items.Select(item => item.Value).Concatenate(Separator, FinalPrefix, FinalSuffix);
        }

        public string Generate(Func<StructuredTextItem, string> itemFunc)
        {
            var items = ReverseItems ? this.Reverse() : this;

            return items.Select(item => itemFunc(item.Value)).Concatenate(Separator, FinalPrefix, FinalSuffix);
        }

        public override string ToString()
        {
            return Generate();
        }
    }
}
