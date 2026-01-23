using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using VédőEszköz;
using static System.IO.File;


public static partial class Függvénygyűjtemény
{
    public static object misValue = System.Reflection.Missing.Value;
    public static void Megnyitás(string Fájlhelye)
    {
        try
        {
            if (!Exists(Fájlhelye)) return;
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = Fájlhelye,
                UseShellExecute = true, // Fontos: ezzel a rendszer alapértelmezett alkalmazását használja
                Verb = "open"           // Explicit "megnyitás" parancs
            };
            Process.Start(psi);
        }
        catch (Exception ex)
        {
            StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
            string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
            HibaNapló.Log(ex.Message, $"Megnyitás(Fájlhelye {Fájlhelye}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
            MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }

    public static void Megnyitások(List<string> Fájlok)
    {
        string Fájlhelye = "";
        try
        {
            foreach (string Fájl in Fájlok)
            {
                Fájlhelye = Fájl;
                if (!Exists(Fájlhelye)) return;
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = Fájlhelye,
                    UseShellExecute = true, // Fontos: ezzel a rendszer alapértelmezett alkalmazását használja
                    Verb = "open"           // Explicit "megnyitás" parancs
                };
                Process.Start(psi);
            }
        }
        catch (Exception ex)
        {
            StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
            string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
            HibaNapló.Log(ex.Message, $"Megnyitás(Fájlhelye {Fájlhelye}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
            MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }

    public static void ExcelNyomtatás(List<string> Fájlok, string munkalap, bool törlés = false, int kezdőoldal = 1, int példányszám = 1)
    {
        //foreach (string Fájl in Fájlok)
        //{
        //    MyE.Application excelApp = null;
        //    MyE.Workbook workbook = null;
        //    if (!File.Exists(Fájl))
        //    {
        //        MessageBox.Show($"Fájl nem található: {Fájl}", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        continue;
        //    }

        //    try
        //    {
        //        // Excel alkalmazás példányosítása (láthatatlan módban)
        //        excelApp = new MyE.Application
        //        {
        //            Visible = false,
        //            DisplayAlerts = false
        //        };

        //        // Munkafüzet megnyitása
        //        workbook = excelApp.Workbooks.Open(Fájl);

        //        MyE.Worksheet Munkalap = (MyE.Worksheet)workbook.Worksheets[munkalap];


        //        Munkalap.PrintOutEx(kezdőoldal, misValue, példányszám, false);

        //        //// Nyomtatás az alapértelmezett nyomtatóra (minden munkalap)
        //        //workbook.PrintOut(); // Esetleg megadhatsz From, To paramétereket is

        //        // Munkafüzet bezárása mentés nélkül
        //        workbook.Close(SaveChanges: false);
        //        Marshal.ReleaseComObject(workbook);
        //        workbook = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Hiba a(z) {Fájl} fájl nyomtatása közben: {ex.Message}", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }

        //    finally
        //    {
        //        // Excel alkalmazás bezárása
        //        if (excelApp != null)
        //        {
        //            excelApp.Quit();
        //            Marshal.ReleaseComObject(excelApp);
        //            excelApp = null;
        //        }

        //        // Fájl törlése (akár sikeres, akár sikertelen volt a nyomtatás – ízlés szerint)
        //        try
        //        {
        //            if (törlés)
        //            {
        //                File.Delete(Fájl);
        //            }
        //        }
        //        catch (Exception delEx)
        //        {
        //            StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
        //            string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
        //            HibaNapló.Log(delEx.Message, $"Megnyitás(Fájlhelye {Fájl}) \n Hívó: {hívóInfo}", delEx.StackTrace, delEx.Source, delEx.HResult);
        //            MessageBox.Show(delEx.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //}
    }

    public static void FájlTörlés(List<string> Fájlok)
    {
        foreach (string Fájl in Fájlok)
        {
            if (!File.Exists(Fájl))
            {
                MessageBox.Show($"Fájl nem található: {Fájl}", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                continue;
            }
            // Fájl törlése (akár sikeres, akár sikertelen volt a nyomtatás – ízlés szerint)
            try
            {
                File.Delete(Fájl);
                // MessageBox.Show($"Törölve: {filePath}", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception delEx)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(delEx.Message, $"FájlTörlés(Fájlhelye {Fájl}) \n Hívó: {hívóInfo}", delEx.StackTrace, delEx.Source, delEx.HResult);
                MessageBox.Show(delEx.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

