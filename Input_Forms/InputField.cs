using System.Windows.Forms;

namespace InputForms
{
    public abstract class InputField
    {
        public Control input;

        protected InputField(Control parent = null)
        {
            input = CreateField(); // Ez már a leszármazott CreateField-jét hívja meg!
            if (parent != null)
                Add(parent);
        }

        protected abstract Control CreateField(); // ← most már absztrakt!

        public virtual InputField Add(Control parent)
        {
            parent.Controls.Add(input);
            return this;
        }

        // Absztrakt Value – minden típus saját logikával implementálja
        public abstract object Value { get; set; }

    }
}
