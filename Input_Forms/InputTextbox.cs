using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace InputForms
{
    class InputTextbox : InputField
    {
        readonly Label label;
        string rule;
        readonly string Tartalom;
        readonly int MaxLength;
        bool Többsor;

        public int Height => ((TextBox)input).Height;


        ScrollBars Görgetés = ScrollBars.None;   // görgetés beállítások
        bool WordWrap = true;                   // görgetés beállítások

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LabelSzöveg">Label felirat</param>
        /// <param name="parent"></param>
        /// <param name="MaxLength"></param>
        public InputTextbox(string LabelSzöveg, string tartalom, int maxLength = 15, Control parent = null) : base(parent)
        {
            MaxLength = maxLength;
            Tartalom = tartalom;
            Többsor = false;
            label = new Label
            {
                Text = LabelSzöveg,
                Font = new Font("sans-serif", 12f),
                AutoSize = true
            };

            input = CreateField();

            if (parent != null) Add(parent);
        }

        public InputTextbox TöbbSoros(bool ertek = true)
        {
            Többsor = ertek;
            var tb = (TextBox)input;
            tb.Multiline = Többsor;
            // ha több sorosra váltunk, legyen értelmes magasság
            if (Többsor && tb.Height < 78) tb.Height = 78;
            ApplyScrollBarSettings(tb);
            return this;
        }

        public InputTextbox FüggőlegesGörgetés()
        {
            Görgetés = ScrollBars.Vertical;
            ApplyScrollBarSettings((TextBox)input);
            return this;
        }

        public InputTextbox VízszintesGörgetés()
        {
            Görgetés = ScrollBars.Horizontal;
            WordWrap = false; // vízszintes scroll csak WordWrap = false mellett
            ApplyScrollBarSettings((TextBox)input);
            return this;
        }

        private void ApplyScrollBarSettings(TextBox tb)
        {
            tb.ScrollBars = Görgetés;
            tb.WordWrap = WordWrap;
            // Görgetősáv CSAK Multiline mellett él:
            if (!tb.Multiline && Görgetés != ScrollBars.None)
            {
                tb.Multiline = true;
                if (tb.Height < 78) tb.Height = 78;
            }
        }

        public InputTextbox MindkétGörgetés()
        {
            Görgetés = ScrollBars.Both;
            WordWrap = false;
            ApplyScrollBarSettings((TextBox)input);
            return this;
        }



        public override InputField Add(Control parent)
        {
            parent.Controls.Add(label);
            parent.Controls.Add(input);
            return this;
        }

        public InputTextbox MoveTo(int x, int y)
        {
            label.Top = y;
            input.Top = y;
            label.Left = x;
            input.Left = label.Left + label.Width + 10;

            return this;
        }

        public InputTextbox AddRule(string rule)
        {
            this.rule = rule;
            return this;
        }


        public bool IsValid()
        {
            string magyar = @"[aábcdeéfghiíjklmnoóöőpqrstuúüűvwxyzAÁBCDEÉFGHIÍJKLMNOÓÖŐPQRSTUÚÜŰVWXYZ ]";
            if (rule == null) rule = magyar;
            return Regex.IsMatch((string)Value, "^" + rule + "+$");
        }
        public bool IsValidNum()
        {
            string magyar = @"[0123456789,]";
            if (rule == null) rule = magyar;
            return Regex.IsMatch((string)Value, "^" + rule + "+$");
        }

        protected override Control CreateField()
        {
            TextBox textBox = new TextBox
            {
                Font = new Font("sans-serif", 12f),
                Width = Szélesség(),
                MaxLength = MaxLength,
                Text = Tartalom,
                Multiline = Többsor,
                Height = Többsor ? 78 : 26,
                ScrollBars = Görgetés,
                WordWrap = WordWrap
            };
            return textBox;
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
            Többsor = true;
            return válasz;
        }

        public override object Value
        {
            get => ((TextBox)input).Text;
            set => ((TextBox)input).Text = value?.ToString() ?? "";
        }

        public InputTextbox SetHeight(int magas)
        {
            input.Height = magas;
            return this;
        }

        public InputTextbox SetWidth(int széles)
        {
            input.Width = széles;
            return this;
        }


    }
}
