using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel
{
    using Binding;
    using Model;

    public class ViewModel : ObserableObject
    {
        Model _model = new Model();

        public ViewModel()
        {
            Func<int> numberGetter = () => { return Number; };
            Action<int> numberSetter = (x) => Number = x;

            RegisterProperty("Number", () => { return Number; }, (x) => Number = x);
            RegisterProperty("Text", () => { return Text; }, (x) => Text = x );
            RegisterProperty("Computed", () => { return Computed; });
        }

        public int Number
        {
            get { return _model.Number; }
            set { _model.Number = value; }
        }

        public string Text
        {
            get { return _model.Text; }
            set { _model.Text = value; }
        }

        public string Computed {
            get { return _model.Text + ": " + _model.Number; }
        }
    }
}
