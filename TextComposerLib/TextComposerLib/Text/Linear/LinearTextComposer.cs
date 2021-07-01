using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TextComposerLib.Text.Linear.LineHeader;

namespace TextComposerLib.Text.Linear
{
    public class LinearTextComposer
    {
        /// <summary>
        /// The list of lines added to the text log
        /// </summary>
        private readonly List<string> _lines = new List<string>();

        /// <summary>
        /// The log line buffer holding the last line to be appended to
        /// </summary>
        private readonly StringBuilder _newLine = new StringBuilder(512);

        /// <summary>
        /// The list of line header objects to be added to each line in the log
        /// </summary>
        private readonly List<LtcLineHeader> _lineHeaders = new List<LtcLineHeader>();

        /// <summary>
        /// The indentation header object
        /// </summary>
        private readonly LtcStackIndentation _indentation = new LtcStackIndentation();


        /// <summary>
        /// The separator of each two header objects text
        /// </summary>
        public string LineHeadersSeparator { get; set; }

        /// <summary>
        /// If set to true a call to ToString() method clears the contents of this text builder
        /// </summary>
        public bool ClearOnRead { get; set; }

        /// <summary>
        /// The current number of lines in the log including the log line buffer if not empty
        /// </summary>
        public int LinesCount => _newLine.Length > 0 ? _lines.Count + 1 : _lines.Count;

        /// <summary>
        /// The current number of lines in the log not including the log line buffer
        /// </summary>
        public int StoredLinesCount => _lines.Count;

        /// <summary>
        /// Get a list of all lines in the log including the contents of the log line buffer if not empty
        /// </summary>
        public IEnumerable<string> LinesText
        {
            get
            {
                foreach (var line in _lines)
                    yield return line;

                if (_newLine.Length > 0)
                    yield return _newLine.ToString();
            }
        }

        /// <summary>
        /// Get a list of all lines in the log not including the contents of the log line buffer
        /// </summary>
        public IEnumerable<string> StoredLinesText => _lines;

        /// <summary>
        /// Get the contents of the log line buffer
        /// </summary>
        public string LineBufferText => _newLine.ToString();

        /// <summary>
        /// Get the current indentation width
        /// </summary>
        public int IndentationWidth => _indentation.IndentationWidth;

        /// <summary>
        /// Get the current indentation level
        /// </summary>
        public int IndentationLevel => _indentation.IndentationLevel;

        /// <summary>
        /// Get or set the default indentation string
        /// </summary>
        public string IndentationDefault
        {
            get
            {
                return _indentation.DefaultIndentation;
            }
            set
            {
                _indentation.DefaultIndentation = value;
            }
        }

        /// <summary>
        /// Get the current full indentation string
        /// </summary>
        public string IndentationString => _indentation.IndentationString;

        /// <summary>
        /// The current text in the log. This does not clear the contents of the log. The ToString() method
        /// returns the same value but clears the log completely.
        /// </summary>
        public string CurrentText
        {
            get
            {
                var s = new StringBuilder();

                foreach (var line in _lines)
                    s.AppendLine(line);

                if (_newLine.Length > 0)
                    s.Append(_newLine);

                return s.ToString();
            }
        }


        /// <summary>
        /// Increase the indentation by one level using the default indentation
        /// </summary>
        /// <returns>The current indentation level</returns>
        public LinearTextComposer IncreaseIndentation() 
        {
            _indentation.PushIndentation();

            return this; 
        }

        /// <summary>
        /// Increase the indentation by one level using the given string
        /// </summary>
        /// <param name="indent"></param>
        /// <returns></returns>
        public LinearTextComposer IncreaseIndentation(string indent)
        {
            if (string.IsNullOrEmpty(indent))
                _indentation.PushIndentation();
            else
                _indentation.PushIndentation(indent);

            return this;
        }

        /// <summary>
        /// Decrease the indentation by one level
        /// </summary>
        /// <returns>The current indentation level</returns>
        public LinearTextComposer DecreaseIndentation() 
        {
            _indentation.PopIndentation();

            return this;
        }

        /// <summary>
        /// Clear the indentation line header object
        /// </summary>
        public LinearTextComposer ClearIndentation()
        {
            _indentation.Reset();

            return this;
        }

        /// <summary>
        /// Clear the log lines and line buffer and reset all header objects without removing any of them
        /// </summary>
        public virtual LinearTextComposer Clear()
        {
            _indentation.Reset();

            _lines.Clear();

            _newLine.Clear();

            _newLine.Capacity = 512;

            foreach (var lineHeader in _lineHeaders)
                lineHeader.Reset();

            return this;
        }

        /// <summary>
        /// Remove all text from log including last line without resetting any of the  indentation or line header objects
        /// </summary>
        public LinearTextComposer ClearText()
        {
            _lines.Clear();

            _newLine.Clear();

            return this;
        }

        /// <summary>
        /// Clear the log line buffer only
        /// </summary>
        public LinearTextComposer ClearLastLine()
        {
            _newLine.Clear();

            return this;
        }

        /// <summary>
        /// Remove i characters from the right of the log string
        /// </summary>
        /// <param name="i">The number of characters to be removed</param>
        public LinearTextComposer Trim(int i)
        {
            _newLine.Length = Math.Max(_newLine.Length - i, 0);

            return this;
        }


        /// <summary>
        /// Append the line header text using the header objects to the log line buffer
        /// </summary>
        private void AppendLineHeader()
        {
            foreach (var lineHeader in _lineHeaders)
            {
                _newLine.Append(lineHeader.GetHeaderText());
                _newLine.Append(LineHeadersSeparator);
            }

            _newLine.Append(_indentation.IndentationString);
        }


        /// <summary>
        /// Append a full empty line to the log line buffer and add the buffer to the log lines then clear the buffer
        /// </summary>
        public LinearTextComposer AppendLine()
        {
            if (_newLine.Length == 0)
                AppendLineHeader();

            _lines.Add(_newLine.ToString());

            _newLine.Clear();

            return this;
        }

        /// <summary>
        /// Append a full line of text to the log line buffer and add the buffer to the log lines then clear the buffer
        /// </summary>
        public LinearTextComposer AppendNewLine()
        {
            return AppendLine();
        }

        public LinearTextComposer AppendSpaces(int n = 1)
        {
            return Append("".PadLeft(n, ' '));
        }

        public LinearTextComposer AppendCharacters(char c, int n = 1)
        {
            return Append("".PadLeft(n, c));
        }

        /// <summary>
        /// Make sure the log line buffer is currently empty by adding it to the log lines if it has text
        /// </summary>
        public LinearTextComposer AppendAtNewLine()
        {
            if (_newLine.Length > 0)
                AppendLine();

            return this;
        }

        /// <summary>
        /// If the log line buffer is empty this function just appends a full empty new line.
        /// If the log line buffer is not empty this function first adds the buffer to the log lines then appends a full empty new line.
        /// </summary>
        public LinearTextComposer AppendLineAtNewLine()
        {
            if (_newLine.Length > 0)
                AppendLine();

            return AppendLine();
        }

        /// <summary>
        /// Append a number of empty lines at the end of this linear text composer
        /// If the buffer is not empty it's contents are added to the text before
        /// adding the empty lines
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public LinearTextComposer AppendEmptyLines(int n)
        {
            if (n < 1) return this;

            if (_newLine.Length > 0)
                AppendLine();

            while (n > 0)
            {
                AppendLine();
                n--;
            }

            return this;
        }

        /// <summary>
        /// Append text to the log line buffer and add more lines if needed (if text is multi-line)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text"></param>
        /// <returns></returns>
        public LinearTextComposer Append<T>(T text)
        {
            if (ReferenceEquals(text, null)) return this;

            return Append(text.ToString());
        }

        /// <summary>
        /// Append text to the log line buffer and add more lines if needed (if text is multi-line)
        /// </summary>
        /// <param name="text">The text to be added</param>
        public LinearTextComposer Append(string text)
        {
            if (string.IsNullOrEmpty(text)) return this;

            //Separate the input text into lines
            var lines = text.SplitLines();

            //Each line will be added separately
            for (var i = 0; i < lines.Length; i++)
            {
                //For the first line of added text, if the log line buffer is empty add the line header
                //Do the same for all following added text lines.
                if (i > 0 || (i == 0 && _newLine.Length == 0))//(i > 0 && i < lines.Length - 1))
                    AppendLineHeader();

                //Append the added text line to the log line buffer
                _newLine.Append(lines[i]);

                //For all added lined except the last one, add the log line buffer to the log and clear the buffer
                if (i >= lines.Length - 1) 
                    continue;

                _lines.Add(_newLine.ToString());

                _newLine.Clear();
            }

            return this;
        }

        /// <summary>
        /// Append a full line of text to the log line buffer and add the buffer to the log lines then clear the buffer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text"></param>
        /// <returns></returns>
        public LinearTextComposer AppendLine<T>(T text)
        {
            if (ReferenceEquals(text, null)) return AppendLine();

            return AppendLine(text.ToString());
        }

        /// <summary>
        /// Append a full line of text to the log line buffer and add the buffer to the log lines then clear the buffer
        /// </summary>
        /// <param name="text">The text to be appended</param>
        public LinearTextComposer AppendLine(string text)
        {
            Append(text);

            return AppendLine();
        }

        /// <summary>
        /// Append a full empty line to the log line buffer and then append text to the buffer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text"></param>
        /// <returns></returns>
        public LinearTextComposer AppendNewLine<T>(T text)
        {
            if (ReferenceEquals(text, null)) return AppendNewLine();

            return AppendNewLine(text.ToString());
        }

        /// <summary>
        /// Append a full empty line to the log line buffer and then append text to the buffer
        /// </summary>
        /// <param name="text">The text to be appended</param>
        public LinearTextComposer AppendNewLine(string text)
        {
            AppendLine();

            return Append(text);
        }

        ///// <summary>
        ///// If the log line buffer is empty this function just appends the text to the buffer.
        ///// If the log line buffer is not empty this function first adds the buffer to the log lines then appends the text
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="text"></param>
        ///// <returns></returns>
        //public LinearTextComposer AppendAtNewLine<T>(T text)
        //{
        //    if (ReferenceEquals(text, null)) return AppendAtNewLine();

        //    return AppendAtNewLine(text.ToString());
        //}

        /// <summary>
        /// If the log line buffer is empty this function just appends the text to the buffer.
        /// If the log line buffer is not empty this function first adds the buffer to the log lines then appends the text
        /// </summary>
        /// <param name="text">The text to be appended</param>
        public LinearTextComposer AppendAtNewLine(string text)
        {
            if (_newLine.Length > 0)
                AppendLine();

            return Append(text);
        }

        ///// <summary>
        ///// If the log line buffer is empty this function just appends the text to the buffer as a full new line.
        ///// If the log line buffer is not empty this function first adds the buffer to the log lines then appends the text as a full new line.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="text"></param>
        ///// <returns></returns>
        //public LinearTextComposer AppendLineAtNewLine<T>(T text)
        //{
        //    if (ReferenceEquals(text, null)) return AppendLineAtNewLine();

        //    return AppendLineAtNewLine(text.ToString());
        //}

        /// <summary>
        /// If the log line buffer is empty this function just appends the text to the buffer as a full new line.
        /// If the log line buffer is not empty this function first adds the buffer to the log lines then appends the text as a full new line.
        /// </summary>
        /// <param name="text"></param>
        public LinearTextComposer AppendLineAtNewLine(string text)
        {
            if (_newLine.Length > 0)
                AppendLine();

            return AppendLine(text);
        }


        /// <summary>
        /// Create and add a line counter header object to the header objects of the log
        /// </summary>
        /// <returns>The added header object</returns>
        public LtcLineCount AddLineCountHeader()
        {
            var lineHeader = new LtcLineCount(this);

            _lineHeaders.Add(lineHeader);

            return lineHeader;
        }

        /// <summary>
        /// Create and add a line counter header object to the header objects of the log
        /// </summary>
        /// <param name="formatString"></param>
        /// <returns>The added header object</returns>
        public LtcLineCount AddLineCountHeader(string formatString)
        {
            var lineHeader = new LtcLineCount(this, formatString);

            _lineHeaders.Add(lineHeader);

            return lineHeader;
        }

        public LtcTimeStamp AddTimeStampHeader()
        {
            var lineHeader = new LtcTimeStamp();

            _lineHeaders.Add(lineHeader);

            return lineHeader;
        }

        public LtcTimeStamp AddTimeStampHeader(string formatString)
        {
            var lineHeader = new LtcTimeStamp(formatString);

            _lineHeaders.Add(lineHeader);

            return lineHeader;
        }

        public LtcStopWatch AddStopWatchHeader()
        {
            var lineHeader = new LtcStopWatch();

            _lineHeaders.Add(lineHeader);

            return lineHeader;
        }

        public LtcStopWatch AddStopWatchHeader(bool resetOnRead)
        {
            var lineHeader = new LtcStopWatch() { ResetOnRead = resetOnRead };

            _lineHeaders.Add(lineHeader);

            return lineHeader;
        }

        public LtcStopWatch AddStopWatchHeader(string formatString)
        {
            var lineHeader = new LtcStopWatch(formatString);

            _lineHeaders.Add(lineHeader);

            return lineHeader;
        }

        public LtcStopWatch AddStopWatchHeader(bool resetOnRead, string formatString)
        {
            var lineHeader = new LtcStopWatch(formatString) { ResetOnRead = resetOnRead };

            _lineHeaders.Add(lineHeader);

            return lineHeader;
        }

        public LtcLineHeader AddLineHeader(LtcLineHeader lineHeader)
        {
            _lineHeaders.Add(lineHeader);

            return lineHeader;
        }


        /// <summary>
        /// Save the full text of the log into a text file
        /// </summary>
        /// <param name="fileName">The path of the text file to be saved</param>
        public void SaveToFile(string fileName)
        {
            File.WriteAllText(fileName, ToString());
        }

        /// <summary>
        /// Append the full text of the log into a text file
        /// </summary>
        /// <param name="fileName"></param>
        public void AppendToFile(string fileName)
        {
            File.AppendAllText(fileName, ToString());
        }

        /// <summary>
        /// Append the full text of the log into a text stream
        /// </summary>
        /// <param name="stream"></param>
        public void AppendToStream(TextWriter stream)
        {
            stream.Write(ToString());
        }

        public override string ToString()
        {
            var curText = CurrentText;

            if (ClearOnRead)
                Clear();

            return curText;
        }
    }
}
