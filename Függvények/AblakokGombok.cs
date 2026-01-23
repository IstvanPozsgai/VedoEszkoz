using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;


namespace VédőEszköz
{
    public static class AblakokGombok
    {
        /// <summary>
        /// Kilistázzaa az összes formot a projektben
        /// </summary>
        /// <returns></returns>
        public static List<Type> FormokListázásaType()
        {
            List<Type> formTypes = Assembly.GetExecutingAssembly()
                   .GetTypes()
                   .Where(t => t.IsSubclassOf(typeof(Form))).ToList();
            return formTypes;
        }


        /// <summary>
        /// Egy ablak összes gombját adja vissza egy listában
        /// </summary>
        /// <param name="formNev"></param>
        /// <returns></returns>
        public static List<Button> FormbanlévőGombok(string formNev)
        {
            List<Button> buttons = new List<Button>();
            try
            {
                // Megkeressük a form típusát név alapján
                Type FormKiválasztott = Assembly.GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.IsSubclassOf(typeof(Form)) && t.Name == formNev);

                if (FormKiválasztott == null) return buttons;

                // Példányosítjuk a formot, de nem jelenítjük meg
                if (!(Activator.CreateInstance(FormKiválasztott) is Form form)) return null;

                // Lekérjük a gombokat
                buttons = GetAllButtons(form);

                // A példányt eldobhatod, ha már nincs rá szükség
                form.Dispose();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "FormbanlévőGombok", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return buttons;
        }

        /// <summary>
        ///    Segédfüggvény: összes Button lekérdezése rekurzívan
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static List<Button> GetAllButtons(Control parent)
        {
            List<Button> buttons = new List<Button>();
            if (parent == null) return buttons;
            foreach (Control c in parent.Controls)
            {
                if (c is Button btn)
                    buttons.Add(btn);
                if (c.HasChildren)
                    buttons.AddRange(GetAllButtons(c));
            }
            return buttons;
        }

        /// <summary>
        /// Menü lista készítése a menüsávból
        /// </summary>
        /// <param name="menuStrip"></param>
        /// <returns></returns>
        public static List<ToolStripMenuItem> MenüListaKészítés(MenuStrip menuStrip)
        {
            List<ToolStripMenuItem> items = new List<ToolStripMenuItem>();
            foreach (ToolStripMenuItem item in menuStrip.Items)
            {
                items.Add(item);
                items.AddRange(GetMenuItemsRecursive(item));
            }
            return items;
        }

        /// <summary>
        ///   Menü rekurzív bejárása
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        private static List<ToolStripMenuItem> GetMenuItemsRecursive(ToolStripMenuItem parent)
        {
            List<ToolStripMenuItem> items = new List<ToolStripMenuItem>();
            foreach (ToolStripItem subItem in parent.DropDownItems)
            {
                if (subItem is ToolStripMenuItem menuItem)
                {
                    items.Add(menuItem);
                    items.AddRange(GetMenuItemsRecursive(menuItem));
                }
            }
            return items;
        }

    }
}
