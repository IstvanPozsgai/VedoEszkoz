using System.Drawing;
using System.Windows.Forms;


namespace InputForms
{
    class InputSelect : InputField
    {
        readonly string[] Options;
        readonly Label label;
        readonly int MaxLength;
        public int Height => input.Height;

        public InputSelect(string LabelSzöveg, string[] options, int maxLength = 15, Control parent = null) : base(parent)
        {
            label = new Label
            {
                Text = LabelSzöveg,
                Font = new Font("sans-serif", 12f),
                AutoSize = true
            };

            Options = options;
            MaxLength = maxLength;

            ComboBox comboBox = (ComboBox)input;
            comboBox.MaxLength = maxLength;
            comboBox.Width = Szélesség();
            comboBox.Items.Clear();
            comboBox.Items.AddRange(Options);
            if (Options.Length > 0)
                comboBox.SelectedIndex = 0;

            if (parent != null) Add(parent);
        }

        public override InputField Add(Control parent)
        {
            parent.Controls.Add(label);
            parent.Controls.Add(input);
            return this;
        }

        protected override Control CreateField()
        {
            ComboBox combobox = new ComboBox
            {
                Font = new Font("sans-serif", 12f),
                Width = Szélesség(),
                MaxLength = MaxLength
            };
            return combobox;
        }

        public InputSelect MoveTo(int x, int y)
        {
            label.Top = y;
            input.Top = y;
            label.Left = x;
            input.Left = label.Left + label.Width + 10;
            return this;
        }

        public override object Value
        {
            get => ((ComboBox)input).Text;
            set
            {
                var combo = (ComboBox)input;
                string textValue = value?.ToString() ?? "";
                if (combo.Items.Contains(textValue))
                {
                    combo.Text = textValue;
                }
                // Ha nem szerepel a listában, nem változik (DropDownList miatt)
            }
        }

        public int Szélesség()
        {
            int válasz = 10;
            using (Font font = new Font("Microsoft Sans Serif", 12f))
            {
                string worstCase = new string('W', MaxLength);
                Size textSize = TextRenderer.MeasureText(
                    worstCase,
                    font,
                    Size.Empty,
                    TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix
                );

                // Margó hozzáadása (a kurzor, belső padding miatt)
                válasz = textSize.Width + 8;
            }
            return válasz;
        }
    }
}
