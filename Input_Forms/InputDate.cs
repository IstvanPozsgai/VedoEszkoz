using System;
using System.Drawing;
using System.Windows.Forms;

namespace InputForms
{
    class InputDate : InputField
    {
        readonly Label label;
        readonly DateTime Dátum;

        public int Height => input.Height;


        public InputDate(string LabelSzöveg, DateTime dátum, Control parent = null) : base(parent)
        {
            Dátum = dátum;
            label = new Label
            {
                Text = LabelSzöveg,
                Font = new Font("sans-serif", 12f),
                AutoSize = true
            };

            DateTimePicker datetimepicker = (DateTimePicker)input;
            datetimepicker.Value = Dátum;

            if (parent != null) Add(parent);
        }

        protected override Control CreateField()
        {
            DateTimePicker datetimepicker = new DateTimePicker
            {
                Font = new Font("sans-serif", 12f),
                Width = 120,
                Format = DateTimePickerFormat.Short,

            };
            return datetimepicker;
        }

        public override object Value
        {
            get => ((DateTimePicker)input).Value;
            set => ((DateTimePicker)input).Value = (DateTime)value;
        }

        public override InputField Add(Control parent)
        {
            parent.Controls.Add(label);
            parent.Controls.Add(input);
            return this;
        }

        public InputDate MoveTo(int x, int y)
        {
            label.Top = y;
            input.Top = y;
            label.Left = x;
            input.Left = label.Left + label.Width + 10;
            return this;
        }

        public InputDate SetHeight(int magas)
        {
            input.Height = magas;
            return this;
        }

        public InputDate SetWidth(int széles)
        {
            input.Width = széles;
            return this;
        }

    }
}
