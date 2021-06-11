using System.Collections.Generic;
using TextComposerLib.Text.Parametric;

namespace TextComposerLib.Text.Structured
{
    public static class StructuredTextComposerUtils
    {
        /// <summary>
        /// Create and initialize a StructuredTextItem object from the given inputs
        /// </summary>
        /// <param name="text"></param>
        /// <param name="prefix"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static StructuredTextItem ToTextItem(this string text, string prefix, string suffix)
        {
            return new StructuredTextItem(prefix, text, suffix);
        }

        /// <summary>
        /// Create and initialize a StructuredTextItem object from the given inputs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text"></param>
        /// <param name="prefix"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static StructuredTextItem ToTextItem<T>(this T text, string prefix, string suffix)
        {
            return new StructuredTextItem(prefix, text.ToString(), suffix);
        }

        /// <summary>
        /// Create and initialize a StructuredTextItem object from the given input string using the
        /// active item prefix and suffix of the composer
        /// </summary>
        /// <param name="composer"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static StructuredTextItem ToTextItem(this IStructuredTextComposer composer, string text)
        {
            return new StructuredTextItem(composer.ActiveItemPrefix, text, composer.ActiveItemSuffix);
        }

        /// <summary>
        /// Create and initialize a StructuredTextItem object from the given input string using the
        /// active item prefix and suffix of the composer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="composer"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static StructuredTextItem ToTextItem<T>(this IStructuredTextComposer composer, T text)
        {
            return new StructuredTextItem(composer.ActiveItemPrefix, text.ToString(), composer.ActiveItemSuffix);
        }


        /// <summary>
        /// Create and initialize a list composer from the given inputs
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static ListTextComposer ComposeToList(this IEnumerable<string> items)
        {
            var composer = new ListTextComposer();

            return composer.AddRange(items);
        }

        /// <summary>
        /// Create and initialize a list composer from the given inputs
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static ListTextComposer ComposeToList(this IEnumerable<StructuredTextItem> items)
        {
            var composer = new ListTextComposer();

            return composer.AddRange(items);
        }

        /// <summary>
        /// Create and initialize a list composer from the given inputs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static ListTextComposer ComposeToList<T>(this IEnumerable<T> items)
        {
            var composer = new ListTextComposer();

            return composer.AddRange(items);
        }

        /// <summary>
        /// Create and initialize a list composer from the given inputs
        /// </summary>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static ListTextComposer ComposeToList(this IEnumerable<string> items, string separator)
        {
            var composer = new ListTextComposer()
            {
                Separator = separator
            };

            return composer.AddRange(items);
        }

        /// <summary>
        /// Create and initialize a list composer from the given inputs
        /// </summary>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static ListTextComposer ComposeToList(this IEnumerable<StructuredTextItem> items, string separator)
        {
            var composer = new ListTextComposer()
            {
                Separator = separator
            };

            return composer.AddRange(items);
        }

        /// <summary>
        /// Create and initialize a list composer from the given inputs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static ListTextComposer ComposeToList<T>(this IEnumerable<T> items, string separator)
        {
            var composer = new ListTextComposer()
            {
                Separator = separator
            };

            return composer.AddRange(items);
        }

        /// <summary>
        /// Create and initialize a list composer from the given inputs
        /// </summary>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <param name="finalPrefix"></param>
        /// <param name="finalSuffix"></param>
        /// <returns></returns>
        public static ListTextComposer ComposeToList(this IEnumerable<string> items, string separator, string finalPrefix, string finalSuffix)
        {
            var composer = new ListTextComposer()
            {
                Separator = separator,
                FinalPrefix = finalPrefix,
                FinalSuffix = finalSuffix
            };

            return composer.AddRange(items);
        }

        /// <summary>
        /// Create and initialize a list composer from the given inputs
        /// </summary>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <param name="finalPrefix"></param>
        /// <param name="finalSuffix"></param>
        /// <returns></returns>
        public static ListTextComposer ComposeToList(this IEnumerable<StructuredTextItem> items, string separator, string finalPrefix, string finalSuffix)
        {
            var composer = new ListTextComposer()
            {
                Separator = separator,
                FinalPrefix = finalPrefix,
                FinalSuffix = finalSuffix
            };

            return composer.AddRange(items);
        }

        /// <summary>
        /// Create and initialize a list composer from the given inputs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <param name="finalPrefix"></param>
        /// <param name="finalSuffix"></param>
        /// <returns></returns>
        public static ListTextComposer ComposeToList<T>(this IEnumerable<T> items, string separator, string finalPrefix, string finalSuffix)
        {
            var composer = new ListTextComposer()
            {
                Separator = separator,
                FinalPrefix = finalPrefix,
                FinalSuffix = finalSuffix
            };

            return composer.AddRange(items);
        }

        /// <summary>
        /// Create and initialize a list composer from the given inputs
        /// </summary>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <param name="finalPrefix"></param>
        /// <param name="finalSuffix"></param>
        /// <param name="itemPrefix"></param>
        /// <param name="itemSuffix"></param>
        /// <returns></returns>
        public static ListTextComposer ComposeToList(this IEnumerable<string> items, string separator, string finalPrefix, string finalSuffix, string itemPrefix, string itemSuffix)
        {
            var composer = new ListTextComposer()
            {
                Separator = separator,
                FinalPrefix = finalPrefix,
                FinalSuffix = finalSuffix,
                ActiveItemPrefix = itemPrefix,
                ActiveItemSuffix = itemSuffix
            };

            return composer.AddRange(items);
        }

        /// <summary>
        /// Create and initialize a list composer from the given inputs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <param name="finalPrefix"></param>
        /// <param name="finalSuffix"></param>
        /// <param name="itemPrefix"></param>
        /// <param name="itemSuffix"></param>
        /// <returns></returns>
        public static ListTextComposer ComposeToList<T>(this IEnumerable<T> items, string separator, string finalPrefix, string finalSuffix, string itemPrefix, string itemSuffix)
        {
            var composer = new ListTextComposer()
            {
                Separator = separator,
                FinalPrefix = finalPrefix,
                FinalSuffix = finalSuffix,
                ActiveItemPrefix = itemPrefix,
                ActiveItemSuffix = itemSuffix
            };

            return composer.AddRange(items);
        }


        /// <summary>
        /// Create and initialize a stack composer from the given inputs
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static StackTextComposer ComposeToStack(this IEnumerable<string> items)
        {
            var composer = new StackTextComposer();

            return composer.PushRange(items);
        }

        /// <summary>
        /// Create and initialize a stack composer from the given inputs
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static StackTextComposer ComposeToStack(this IEnumerable<StructuredTextItem> items)
        {
            var composer = new StackTextComposer();

            return composer.PushRange(items);
        }

        /// <summary>
        /// Create and initialize a stack composer from the given inputs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static StackTextComposer ComposeToStack<T>(this IEnumerable<T> items)
        {
            var composer = new StackTextComposer();

            return composer.PushRange(items);
        }

        /// <summary>
        /// Create and initialize a stack composer from the given inputs
        /// </summary>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static StackTextComposer ComposeToStack(this IEnumerable<string> items, string separator)
        {
            var composer = new StackTextComposer()
            {
                Separator = separator
            };

            return composer.PushRange(items);
        }

        /// <summary>
        /// Create and initialize a stack composer from the given inputs
        /// </summary>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static StackTextComposer ComposeToStack(this IEnumerable<StructuredTextItem> items, string separator)
        {
            var composer = new StackTextComposer()
            {
                Separator = separator
            };

            return composer.PushRange(items);
        }

        /// <summary>
        /// Create and initialize a stack composer from the given inputs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static StackTextComposer ComposeToStack<T>(this IEnumerable<T> items, string separator)
        {
            var composer = new StackTextComposer()
            {
                Separator = separator
            };

            return composer.PushRange(items);
        }

        /// <summary>
        /// Create and initialize a stack composer from the given inputs
        /// </summary>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <param name="finalPrefix"></param>
        /// <param name="finalSuffix"></param>
        /// <returns></returns>
        public static StackTextComposer ComposeToStack(this IEnumerable<string> items, string separator, string finalPrefix, string finalSuffix)
        {
            var composer = new StackTextComposer()
            {
                Separator = separator,
                FinalPrefix = finalPrefix,
                FinalSuffix = finalSuffix
            };

            return composer.PushRange(items);
        }

        /// <summary>
        /// Create and initialize a stack composer from the given inputs
        /// </summary>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <param name="finalPrefix"></param>
        /// <param name="finalSuffix"></param>
        /// <returns></returns>
        public static StackTextComposer ComposeToStack(this IEnumerable<StructuredTextItem> items, string separator, string finalPrefix, string finalSuffix)
        {
            var composer = new StackTextComposer()
            {
                Separator = separator,
                FinalPrefix = finalPrefix,
                FinalSuffix = finalSuffix
            };

            return composer.PushRange(items);
        }

        /// <summary>
        /// Create and initialize a stack composer from the given inputs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <param name="finalPrefix"></param>
        /// <param name="finalSuffix"></param>
        /// <returns></returns>
        public static StackTextComposer ComposeToStack<T>(this IEnumerable<T> items, string separator, string finalPrefix, string finalSuffix)
        {
            var composer = new StackTextComposer()
            {
                Separator = separator,
                FinalPrefix = finalPrefix,
                FinalSuffix = finalSuffix
            };

            return composer.PushRange(items);
        }

        /// <summary>
        /// Create and initialize a stack composer from the given inputs
        /// </summary>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <param name="finalPrefix"></param>
        /// <param name="finalSuffix"></param>
        /// <param name="itemPrefix"></param>
        /// <param name="itemSuffix"></param>
        /// <returns></returns>
        public static StackTextComposer ComposeToStack(this IEnumerable<string> items, string separator, string finalPrefix, string finalSuffix, string itemPrefix, string itemSuffix)
        {
            var composer = new StackTextComposer()
            {
                Separator = separator,
                FinalPrefix = finalPrefix,
                FinalSuffix = finalSuffix,
                ActiveItemPrefix = itemPrefix,
                ActiveItemSuffix = itemSuffix
            };

            return composer.PushRange(items);
        }

        /// <summary>
        /// Create and initialize a stack composer from the given inputs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <param name="finalPrefix"></param>
        /// <param name="finalSuffix"></param>
        /// <param name="itemPrefix"></param>
        /// <param name="itemSuffix"></param>
        /// <returns></returns>
        public static StackTextComposer ComposeToStack<T>(this IEnumerable<T> items, string separator, string finalPrefix, string finalSuffix, string itemPrefix, string itemSuffix)
        {
            var composer = new StackTextComposer()
            {
                Separator = separator,
                FinalPrefix = finalPrefix,
                FinalSuffix = finalSuffix,
                ActiveItemPrefix = itemPrefix,
                ActiveItemSuffix = itemSuffix
            };

            return composer.PushRange(items);
        }


        /// <summary>
        /// Create and initialize a queue composer from the given inputs
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static QueueTextComposer ComposeToQueue(this IEnumerable<string> items)
        {
            var composer = new QueueTextComposer();

            return composer.EnqueueRange(items);
        }

        /// <summary>
        /// Create and initialize a queue composer from the given inputs
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static QueueTextComposer ComposeToQueue(this IEnumerable<StructuredTextItem> items)
        {
            var composer = new QueueTextComposer();

            return composer.EnqueueRange(items);
        }

        /// <summary>
        /// Create and initialize a queue composer from the given inputs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static QueueTextComposer ComposeToQueue<T>(this IEnumerable<T> items)
        {
            var composer = new QueueTextComposer();

            return composer.EnqueueRange(items);
        }

        /// <summary>
        /// Create and initialize a queue composer from the given inputs
        /// </summary>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static QueueTextComposer ComposeToQueue(this IEnumerable<string> items, string separator)
        {
            var composer = new QueueTextComposer()
            {
                Separator = separator
            };

            return composer.EnqueueRange(items);
        }

        /// <summary>
        /// Create and initialize a queue composer from the given inputs
        /// </summary>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static QueueTextComposer ComposeToQueue(this IEnumerable<StructuredTextItem> items, string separator)
        {
            var composer = new QueueTextComposer()
            {
                Separator = separator
            };

            return composer.EnqueueRange(items);
        }

        /// <summary>
        /// Create and initialize a queue composer from the given inputs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static QueueTextComposer ComposeToQueue<T>(this IEnumerable<T> items, string separator)
        {
            var composer = new QueueTextComposer()
            {
                Separator = separator
            };

            return composer.EnqueueRange(items);
        }

        /// <summary>
        /// Create and initialize a queue composer from the given inputs
        /// </summary>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <param name="finalPrefix"></param>
        /// <param name="finalSuffix"></param>
        /// <returns></returns>
        public static QueueTextComposer ComposeToQueue(this IEnumerable<string> items, string separator, string finalPrefix, string finalSuffix)
        {
            var composer = new QueueTextComposer()
            {
                Separator = separator,
                FinalPrefix = finalPrefix,
                FinalSuffix = finalSuffix
            };

            return composer.EnqueueRange(items);
        }

        /// <summary>
        /// Create and initialize a queue composer from the given inputs
        /// </summary>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <param name="finalPrefix"></param>
        /// <param name="finalSuffix"></param>
        /// <returns></returns>
        public static QueueTextComposer ComposeToQueue(this IEnumerable<StructuredTextItem> items, string separator, string finalPrefix, string finalSuffix)
        {
            var composer = new QueueTextComposer()
            {
                Separator = separator,
                FinalPrefix = finalPrefix,
                FinalSuffix = finalSuffix
            };

            return composer.EnqueueRange(items);
        }

        /// <summary>
        /// Create and initialize a queue composer from the given inputs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <param name="finalPrefix"></param>
        /// <param name="finalSuffix"></param>
        /// <returns></returns>
        public static QueueTextComposer ComposeToQueue<T>(this IEnumerable<T> items, string separator, string finalPrefix, string finalSuffix)
        {
            var composer = new QueueTextComposer()
            {
                Separator = separator,
                FinalPrefix = finalPrefix,
                FinalSuffix = finalSuffix
            };

            return composer.EnqueueRange(items);
        }

        /// <summary>
        /// Create and initialize a queue composer from the given inputs
        /// </summary>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <param name="finalPrefix"></param>
        /// <param name="finalSuffix"></param>
        /// <param name="itemPrefix"></param>
        /// <param name="itemSuffix"></param>
        /// <returns></returns>
        public static QueueTextComposer ComposeToQueue(this IEnumerable<string> items, string separator, string finalPrefix, string finalSuffix, string itemPrefix, string itemSuffix)
        {
            var composer = new QueueTextComposer()
            {
                Separator = separator,
                FinalPrefix = finalPrefix,
                FinalSuffix = finalSuffix,
                ActiveItemPrefix = itemPrefix,
                ActiveItemSuffix = itemSuffix
            };

            return composer.EnqueueRange(items);
        }

        /// <summary>
        /// Create and initialize a queue composer from the given inputs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <param name="finalPrefix"></param>
        /// <param name="finalSuffix"></param>
        /// <param name="itemPrefix"></param>
        /// <param name="itemSuffix"></param>
        /// <returns></returns>
        public static QueueTextComposer ComposeToQueue<T>(this IEnumerable<T> items, string separator, string finalPrefix, string finalSuffix, string itemPrefix, string itemSuffix)
        {
            var composer = new QueueTextComposer()
            {
                Separator = separator,
                FinalPrefix = finalPrefix,
                FinalSuffix = finalSuffix,
                ActiveItemPrefix = itemPrefix,
                ActiveItemSuffix = itemSuffix
            };

            return composer.EnqueueRange(items);
        }


        /// <summary>
        /// Create and initialize a sequence composer from the given inputs
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static SequenceTextComposer ComposeToSequence(this IEnumerable<string> items)
        {
            var composer = new SequenceTextComposer();

            return composer.Append(items);
        }

        /// <summary>
        /// Create and initialize a sequence composer from the given inputs
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static SequenceTextComposer ComposeToSequence(this IEnumerable<StructuredTextItem> items)
        {
            var composer = new SequenceTextComposer();

            return composer.Append(items);
        }

        /// <summary>
        /// Create and initialize a sequence composer from the given inputs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static SequenceTextComposer ComposeToSequence<T>(this IEnumerable<T> items)
        {
            var composer = new SequenceTextComposer();

            return composer.Append(items);
        }

        /// <summary>
        /// Create and initialize a sequence composer from the given inputs
        /// </summary>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static SequenceTextComposer ComposeToSequence(this IEnumerable<string> items, string separator)
        {
            var composer = new SequenceTextComposer()
            {
                Separator = separator
            };

            return composer.Append(items);
        }

        /// <summary>
        /// Create and initialize a sequence composer from the given inputs
        /// </summary>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static SequenceTextComposer ComposeToSequence(this IEnumerable<StructuredTextItem> items, string separator)
        {
            var composer = new SequenceTextComposer()
            {
                Separator = separator
            };

            return composer.Append(items);
        }

        /// <summary>
        /// Create and initialize a sequence composer from the given inputs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static SequenceTextComposer ComposeToSequence<T>(this IEnumerable<T> items, string separator)
        {
            var composer = new SequenceTextComposer()
            {
                Separator = separator
            };

            return composer.Append(items);
        }

        /// <summary>
        /// Create and initialize a sequence composer from the given inputs
        /// </summary>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <param name="finalPrefix"></param>
        /// <param name="finalSuffix"></param>
        /// <returns></returns>
        public static SequenceTextComposer ComposeToSequence(this IEnumerable<string> items, string separator, string finalPrefix, string finalSuffix)
        {
            var composer = new SequenceTextComposer()
            {
                Separator = separator,
                FinalPrefix = finalPrefix,
                FinalSuffix = finalSuffix
            };

            return composer.Append(items);
        }

        /// <summary>
        /// Create and initialize a sequence composer from the given inputs
        /// </summary>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <param name="finalPrefix"></param>
        /// <param name="finalSuffix"></param>
        /// <returns></returns>
        public static SequenceTextComposer ComposeToSequence(this IEnumerable<StructuredTextItem> items, string separator, string finalPrefix, string finalSuffix)
        {
            var composer = new SequenceTextComposer()
            {
                Separator = separator,
                FinalPrefix = finalPrefix,
                FinalSuffix = finalSuffix
            };

            return composer.Append(items);
        }

        /// <summary>
        /// Create and initialize a sequence composer from the given inputs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <param name="finalPrefix"></param>
        /// <param name="finalSuffix"></param>
        /// <returns></returns>
        public static SequenceTextComposer ComposeToSequence<T>(this IEnumerable<T> items, string separator, string finalPrefix, string finalSuffix)
        {
            var composer = new SequenceTextComposer()
            {
                Separator = separator,
                FinalPrefix = finalPrefix,
                FinalSuffix = finalSuffix
            };

            return composer.Append(items);
        }

        /// <summary>
        /// Create and initialize a sequence composer from the given inputs
        /// </summary>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <param name="finalPrefix"></param>
        /// <param name="finalSuffix"></param>
        /// <param name="itemPrefix"></param>
        /// <param name="itemSuffix"></param>
        /// <returns></returns>
        public static SequenceTextComposer ComposeToSequence(this IEnumerable<string> items, string separator, string finalPrefix, string finalSuffix, string itemPrefix, string itemSuffix)
        {
            var composer = new SequenceTextComposer()
            {
                Separator = separator,
                FinalPrefix = finalPrefix,
                FinalSuffix = finalSuffix,
                ActiveItemPrefix = itemPrefix,
                ActiveItemSuffix = itemSuffix
            };

            return composer.Append(items);
        }

        /// <summary>
        /// Create and initialize a sequence composer from the given inputs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="separator"></param>
        /// <param name="finalPrefix"></param>
        /// <param name="finalSuffix"></param>
        /// <param name="itemPrefix"></param>
        /// <param name="itemSuffix"></param>
        /// <returns></returns>
        public static SequenceTextComposer ComposeToSequence<T>(this IEnumerable<T> items, string separator, string finalPrefix, string finalSuffix, string itemPrefix, string itemSuffix)
        {
            var composer = new SequenceTextComposer()
            {
                Separator = separator,
                FinalPrefix = finalPrefix,
                FinalSuffix = finalSuffix,
                ActiveItemPrefix = itemPrefix,
                ActiveItemSuffix = itemSuffix
            };

            return composer.Append(items);
        }


        /// <summary>
        /// Generate the given template text from the supplied parameters values and add its output to 
        /// the given text builder
        /// </summary>
        /// <param name="textBuilder"></param>
        /// <param name="template"></param>
        /// <param name="paramsValues"></param>
        public static void Add(this ListTextComposer textBuilder, ParametricTextComposer template, params object[] paramsValues)
        {
            template.SetParametersValues(paramsValues);

            var text = template.GenerateText();

            textBuilder.Add(text);
        }

        /// <summary>
        /// Generate the given template text from the supplied parameters values and add its output to 
        /// the given text builder
        /// </summary>
        /// <param name="textBuilder"></param>
        /// <param name="template"></param>
        /// <param name="paramsValues"></param>
        public static void Add(this ListTextComposer textBuilder, ParametricTextComposer template, params string[] paramsValues)
        {
            template.SetParametersValues(paramsValues);

            var text = template.GenerateText();

            textBuilder.Add(text);
        }

        /// <summary>
        /// Generate the given template text from the supplied parameters values and add its output to 
        /// the given text builder
        /// </summary>
        /// <param name="textBuilder"></param>
        /// <param name="template"></param>
        /// <param name="paramsValues"></param>
        public static void Add(this ListTextComposer textBuilder, ParametricTextComposer template, IDictionary<string, string> paramsValues)
        {
            template.SetParametersValues(paramsValues);

            var text = template.GenerateText();

            textBuilder.Add(text);
        }

        /// <summary>
        /// Generate the given template text from the supplied parameters values and add its output to 
        /// the given text builder
        /// </summary>
        /// <param name="textBuilder"></param>
        /// <param name="template"></param>
        /// <param name="paramsValues"></param>
        public static void Add(this ListTextComposer textBuilder, ParametricTextComposer template, IParametricTextComposerValueSource paramsValues)
        {
            template.SetParametersValues(paramsValues);

            var text = template.GenerateText();

            textBuilder.Add(text);
        }

        /// <summary>
        /// Generate the given template text from the supplied parameters values and add its output to 
        /// the given text builder at the given index
        /// </summary>
        /// <param name="textBuilder"></param>
        /// <param name="index"></param>
        /// <param name="template"></param>
        /// <param name="paramsValues"></param>
        public static void Insert(this ListTextComposer textBuilder, int index, ParametricTextComposer template, params object[] paramsValues)
        {
            template.SetParametersValues(paramsValues);

            var text = template.GenerateText();

            textBuilder.Insert(index, new StructuredTextItem(text));
        }

        /// <summary>
        /// Generate the given template text from the supplied parameters values and add its output to 
        /// the given text builder at the given index
        /// </summary>
        /// <param name="textBuilder"></param>
        /// <param name="index"></param>
        /// <param name="template"></param>
        /// <param name="paramsValues"></param>
        public static void Insert(this ListTextComposer textBuilder, int index, ParametricTextComposer template, params string[] paramsValues)
        {
            template.SetParametersValues(paramsValues);

            var text = template.GenerateText();

            textBuilder.Insert(index, new StructuredTextItem(text));
        }

        /// <summary>
        /// Generate the given template text from the supplied parameters values and add its output to 
        /// the given text builder at the given index
        /// </summary>
        /// <param name="textBuilder"></param>
        /// <param name="index"></param>
        /// <param name="template"></param>
        /// <param name="paramsValues"></param>
        public static void Insert(this ListTextComposer textBuilder, int index, ParametricTextComposer template, IDictionary<string, string> paramsValues)
        {
            template.SetParametersValues(paramsValues);

            var text = template.GenerateText();

            textBuilder.Insert(index, new StructuredTextItem(text));
        }

        /// <summary>
        /// Generate the given template text from the supplied parameters values and add its output to 
        /// the given text builder at the given index
        /// </summary>
        /// <param name="textBuilder"></param>
        /// <param name="index"></param>
        /// <param name="template"></param>
        /// <param name="paramsValues"></param>
        public static void Insert(this ListTextComposer textBuilder, int index, ParametricTextComposer template, IParametricTextComposerValueSource paramsValues)
        {
            template.SetParametersValues(paramsValues);

            var text = template.GenerateText();

            textBuilder.Insert(index, new StructuredTextItem(text));
        }


        /// <summary>
        /// Generate a dictionary of names-values and add the values in the dictionary to the given text 
        /// builders collection
        /// </summary>
        /// <param name="textBuilder"></param>
        /// <param name="templates"></param>
        /// <param name="paramsValues"></param>
        public static ListComposerCollection AddTextItems(this ListComposerCollection textBuilder, ParametricTextComposerCollection templates, params object[] paramsValues)
        {
            foreach (var pair in textBuilder)
            {
                if (templates.TryGetValue(pair.Key, out var template))
                    pair.Value.Add(template, paramsValues);
            }

            return textBuilder;
        }

        /// <summary>
        /// Generate a dictionary of names-values and add the values in the dictionary to the given text 
        /// builders collection
        /// </summary>
        /// <param name="textBuilder"></param>
        /// <param name="templates"></param>
        /// <param name="paramsValues"></param>
        public static ListComposerCollection AddTextItems(this ListComposerCollection textBuilder, ParametricTextComposerCollection templates, params string[] paramsValues)
        {
            foreach (var pair in textBuilder)
            {
                if (templates.TryGetValue(pair.Key, out var template))
                    pair.Value.Add(template, paramsValues);
            }

            return textBuilder;
        }

        /// <summary>
        /// Generate a dictionary of names-values and add the values in the dictionary to the given text 
        /// builders collection
        /// </summary>
        /// <param name="textBuilder"></param>
        /// <param name="templates"></param>
        /// <param name="paramsValues"></param>
        public static ListComposerCollection AddTextItems(this ListComposerCollection textBuilder, ParametricTextComposerCollection templates, IDictionary<string, string> paramsValues)
        {
            foreach (var pair in textBuilder)
            {
                if (templates.TryGetValue(pair.Key, out var template))
                    pair.Value.Add(template, paramsValues);
            }

            return textBuilder;
        }

        /// <summary>
        /// Generate a dictionary of names-values and add the values in the dictionary to the given text 
        /// builders collection
        /// </summary>
        /// <param name="textBuilder"></param>
        /// <param name="templates"></param>
        /// <param name="paramsValues"></param>
        public static ListComposerCollection AddTextItems(this ListComposerCollection textBuilder, ParametricTextComposerCollection templates, IParametricTextComposerValueSource paramsValues)
        {
            foreach (var pair in textBuilder)
            {
                if (templates.TryGetValue(pair.Key, out var template))
                    pair.Value.Add(template, paramsValues);
            }

            return textBuilder;
        }
    }
}
