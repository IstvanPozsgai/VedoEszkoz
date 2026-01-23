using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace InputForms
{
    class InputForm : Panel
    {
        readonly Dictionary<string, InputField> fields;
        readonly Button button;
        Action clickAction;
        int LeftMax = 0;
        int Ymax = 0;

        public InputForm(Control parent)
        {
            Width = 100;
            Height = 100;
            BackColor = Color.LightGray;

            parent.Controls.Add(this);

            fields = new Dictionary<string, InputField>();
            button = new Button
            {
                Text = "Send",
                Font = new Font("sans-serif", 11f, FontStyle.Bold),
                Width = 150,
                Height = 35
            };

            this.Controls.Add(button);

            button.Left = 25;
            button.Top = 25;

            button.Click += OnClick;

        }

        public string this[string name]
        {
            get
            {
                if (fields.TryGetValue(name, out InputField field) && field.Value != null)
                    return field.Value.ToString();
                return string.Empty;
            }
        }


        public InputForm Add(string name, InputTextbox field)
        {
            int y = 10 + Ymax;

            fields.Add(name, field);
            field.Add(this);
            field.MoveTo(5, y);
            Ymax = Ymax + field.Height + 10;

            //Gomb új pozíció
            y += 80;
            button.Top = y;


            //Panel új magasság
            y += 50;
            Height = y;
            return this;
        }

        public InputForm Add(string name, InputSelect field)
        {
            int y = 10 + (fields.Count * 40);

            fields.Add(name, field);
            field.Add(this);
            field.MoveTo(5, y);
            Ymax = Ymax + field.Height + 10;

            if (button != null)
            {
                //Gomb új pozíció
                y += 80;
                button.Top = y;
            }

            //Panel új magasság
            y += 50;
            Height = y;

            return this;
        }

        public InputForm Add(string name, InputCheckbox field)
        {
            int y = 10 + (fields.Count * 40);

            fields.Add(name, field);
            field.Add(this);
            field.MoveTo(5, y);
            Ymax = Ymax + field.Height + 10;

            //Gomb új pozíció
            y += 80;
            button.Top = y;


            //Panel új magasság
            y += 50;
            Height = y;

            return this;
        }

        public InputForm Add(string name, InputTime field)
        {
            //   int y = 10 + (fields.Count * 40);
            int y = 10 + Ymax;

            fields.Add(name, field);
            field.Add(this);
            field.MoveTo(5, y);
            Ymax = Ymax + field.Height + 10;

            //Gomb új pozíció
            y += 80;
            button.Top = y;


            //Panel új magasság
            y += 50;
            Height = y;

            return this;
        }
        public InputForm Add(string name, InputDate field)
        {
            //   int y = 10 + (fields.Count * 40);
            int y = 10 + Ymax;

            fields.Add(name, field);
            field.Add(this);
            field.MoveTo(5, y);
            Ymax = Ymax + field.Height + 10;

            //Gomb új pozíció
            y += 80;
            button.Top = y;


            //Panel új magasság
            y += 50;
            Height = y;

            return this;
        }

        public InputForm FieldIgazítás()
        {
            int rightmostEdge = this.Controls.OfType<Label>()
                .Max(label => label.Left + label.Width);

            if (rightmostEdge > LeftMax)
            {
                LeftMax = rightmostEdge;

                this.SuspendLayout();

                List<Control> controlsToAlign = this.Controls.OfType<Control>()
                    .Where(c => c is TextBox || c is ComboBox || c is CheckBox || c is DateTimePicker).ToList();

                foreach (Control control in controlsToAlign)
                {
                    control.Left = LeftMax + 10;
                }

                this.ResumeLayout();
            }
            int maxFieldRightEdge = this.Controls.OfType<Control>()
                                    .Where(c => c is TextBox || c is ComboBox || c is CheckBox || c is DateTimePicker)
                                    .Max(control => control.Left + control.Width);
            //Panel szélesség igazítása
            if (maxFieldRightEdge + 10 > Width)
            {
                Width = maxFieldRightEdge + 10;
                button.Left = (Width - button.Width) / 2;
            }
            return this;
        }

        public InputForm MoveTo(int x, int y)
        {
            Left = x;
            Top = y;
            return this;
        }

        public InputForm SetButton(string text)
        {
            button.Text = text;
            return this;
        }

        public InputForm OnSubmit(Action action)
        {
            clickAction += action;
            return this;
        }

        void OnClick(object sender, EventArgs e)
        {
            if (clickAction != null)
            {
                string error = GetError();

                if (error != null)
                {
                    string msg = $"Hibásan kitöltve: {error}!";
                    MessageBox.Show(msg, "Hiba");
                }

                else clickAction();
            }
        }

        string GetError()
        {
            return null;
        }

    }
}
