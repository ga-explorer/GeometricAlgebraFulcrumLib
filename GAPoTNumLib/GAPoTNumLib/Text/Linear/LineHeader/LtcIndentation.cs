using System;

namespace GAPoTNumLib.Text.Linear.LineHeader
{
    public sealed class LtcIndentation : LtcLineHeader
    {
        private int _indentationWidth = 3;
        
        private int _indentationLevel;
        
        private char _indentationCharacter = ' ';
        
        private string _indentationString = "";


        public int IndentationWidth
        {
            get
            {
                return _indentationWidth;
            }
            set
            {
                if (value > 0 && value < 11)
                {
                    _indentationWidth = value;

                    UpdateIndentationString();
                }
                else
                    throw new InvalidOperationException("Indentation width must be a number between 2 and 10");
            }
        }

        public int IndentationLevel
        {
            get
            {
                return _indentationLevel;
            }
            set
            {
                if (value < 0) 
                    return;

                _indentationLevel = value;

                UpdateIndentationString();
            }
        }

        public char IndentationCharacter
        {
            get
            {
                return _indentationCharacter;
            }
            set
            {
                _indentationCharacter = value;

                UpdateIndentationString();
            }
        }

        public string IndentationString => _indentationString;


        private void UpdateIndentationString()
        {
            _indentationString = "".PadLeft(_indentationLevel * _indentationWidth, _indentationCharacter);
        }


        public override string GetHeaderText()
        {
            return _indentationString;
        }

        public override void Reset()
        {
            IndentationLevel = 0;
        }
    }
}
