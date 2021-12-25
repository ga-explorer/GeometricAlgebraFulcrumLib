using System;
using System.Collections.Generic;
using System.Linq;

namespace TextComposerLib.Text.Structured
{
    public sealed class QueueTextComposer : Queue<StructuredTextItem>, IStructuredTextComposer
    {
        public string Separator { get; set; }

        public string ActiveItemPrefix { get; set; }

        public string ActiveItemSuffix { get; set; }

        public string FinalPrefix { get; set; }

        public string FinalSuffix { get; set; }

        public bool ReverseItems { get; set; }


        public QueueTextComposer()
        {
            Separator = string.Empty;
            ActiveItemPrefix = string.Empty;
            ActiveItemSuffix = string.Empty;
            FinalPrefix = string.Empty;
            FinalSuffix = string.Empty;
        }

        public QueueTextComposer(string separator)
        {
            Separator = separator ?? string.Empty;
            ActiveItemPrefix = string.Empty;
            ActiveItemSuffix = string.Empty;
            FinalPrefix = string.Empty;
            FinalSuffix = string.Empty;
        }


        public QueueTextComposer Enqueue()
        {
            base.Enqueue(this.ToTextItem(string.Empty));

            return this;
        }

        public QueueTextComposer Enqueue(string item)
        {
            base.Enqueue(this.ToTextItem(item));

            return this;
        }

        public QueueTextComposer Enqueue<T>(T item)
        {
            base.Enqueue(this.ToTextItem(item));

            return this;
        }

        public QueueTextComposer EnqueueRange(IEnumerable<string> items)
        {
            foreach (var item in items)
                base.Enqueue(this.ToTextItem(item));

            return this;
        }

        public QueueTextComposer EnqueueRange<T>(IEnumerable<T> items)
        {
            foreach (var item in items)
                base.Enqueue(this.ToTextItem(item));

            return this;
        }

        public QueueTextComposer EnqueueRange(params string[] items)
        {
            foreach (var item in items)
                base.Enqueue(this.ToTextItem(item));

            return this;
        }

        public QueueTextComposer EnqueueRange<T>(params T[] items)
        {
            foreach (var item in items)
                base.Enqueue(this.ToTextItem(item));

            return this;
        }

        public QueueTextComposer Dequeue(int n)
        {
            while (n > 0 && Count > 0)
            {
                base.Dequeue();
                n--;
            }

            return this;
        }


        public string Generate()
        {
            var items = ReverseItems ? this.Reverse() : this;

            return items.Concatenate(Separator, FinalPrefix, FinalSuffix);
        }

        public string Generate(Func<StructuredTextItem, string> itemFunc)
        {
            var items = ReverseItems ? this.Reverse() : this;

            return items.Select(itemFunc).Concatenate(Separator, FinalPrefix, FinalSuffix);
        }

        public override string ToString()
        {
            return Generate();
        }
    }
}
