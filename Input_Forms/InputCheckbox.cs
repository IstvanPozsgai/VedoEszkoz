using System.Drawing;
using System.Windows.Forms;

namespace InputForms
{
    public class InputCheckbox : InputField
    {
        private readonly string Text;
        private readonly bool ChecKed;
        public int Height => input.Height;

        public InputCheckbox(string text, bool isChecked, Control parent = null) : base(parent) // ← először inicializáljuk az ős osztályt
        {
            Text = text;
            ChecKed = isChecked;

            input = CreateField();

            if (parent != null) Add(parent);
        }

        public override InputField Add(Control parent)
        {
            parent.Controls.Add(input);
            return this;
        }

        protected override Control CreateField()
        {
            CheckBox checkbox = new CheckBox
            {
                Font = new Font("sans-serif", 12f),
                Text = Text,
                Checked = ChecKed
            };
            return checkbox;
        }

        public override object Value
        {
            get => ((CheckBox)input).Checked;
            set => ((CheckBox)input).Checked = (bool)value;
        }

        public bool IsValid() => true;

        public InputCheckbox MoveTo(int x, int y)
        {
            input.Top = y;
            input.Left = x;
            return this;
        }
    }
}
