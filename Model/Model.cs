using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Model
    {
        int _number;

        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }

        string _text;

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
    }
}
