using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using VédőEszköz;


public class ScreenshotHelper
{

    public static Image GetBitmapScreenshot(string processName)
    {
        Image img = null;

        try
        {
            IntPtr handle = GetWindowHandle(processName);

            //Check if window is minimized and show it if needed
            if (User32.IsIconic(handle)) User32.ShowWindowAsync(handle, User32.SHOWNORMAL);

            User32.SetForegroundWindow(handle);

            //ALT + PRINT SCREEN gets screenshot of focused window
            //See this article for key list
            //https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.sendkeys?view=windowsdesktop-6.0#remarks
            SendKeys.SendWait("%({PRTSC})");
            Thread.Sleep(200);

            //The GetImage function in WPF gets a bitmapsource image
            //This could be replaced with the Winforms getimage since that returns an image

            img = Clipboard.GetImage();

            //Uses the user32.dll to make sure the clipboard is empty and closed 
            //Without this you might get errors that the clipboard is already open
            IntPtr clipWindow = User32.GetOpenClipboardWindow();
            User32.OpenClipboard(clipWindow);
            User32.EmptyClipboard();
            User32.CloseClipboard();
            //Thread.Sleep(100);

        }
        catch (HibásBevittAdat ex)
        {
            MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, "", ex.StackTrace, ex.Source, ex.HResult);
            MessageBox.Show("\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return img;
    }
    /*
     public static Image GetBitmapScreenshot(string processName)
    {
        Image img = null;

        //https://ourcodeworld.com/articles/read/890/how-to-solve-csharp-exception-current-thread-must-be-set-to-single-thread-apartment-sta-mode-before-ole-calls-can-be-made-ensure-that-your-main-function-has-stathreadattribute-marked-on-it
        Thread t = new Thread(() =>
        {
            IntPtr handle = GetWindowHandle(processName);

            //Check if window is minimized and show it if needed
            if (User32.IsIconic(handle))
                User32.ShowWindowAsync(handle, User32.SHOWNORMAL);

            User32.SetForegroundWindow(handle);

            //ALT + PRINT SCREEN gets screenshot of focused window
            //See this article for key list
            //https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.sendkeys?view=windowsdesktop-6.0#remarks
            SendKeys.SendWait("%({PRTSC})");
            Thread.Sleep(500);

            //The GetImage function in WPF gets a bitmapsource image
            //This could be replaced with the Winforms getimage since that returns an image

            img = Clipboard.GetImage();


            //Uses the user32.dll to make sure the clipboard is empty and closed 
            //Without this you might get errors that the clipboard is already open
            IntPtr clipWindow = User32.GetOpenClipboardWindow();
            User32.OpenClipboard(clipWindow);
            User32.EmptyClipboard();
            User32.CloseClipboard();
            Thread.Sleep(100);
        });

        //Run your code from a thread that joins the STA Thread
        //If this is not done, clipboard functions won't work
        t.SetApartmentState(ApartmentState.STA);
        t.Start();
        t.Join();

        return img;

    }

       */



    private static IntPtr GetWindowHandle(string name)
    {
        Process process = Process.GetProcessesByName(name).FirstOrDefault();
        if (process != null && process.MainWindowHandle != IntPtr.Zero)
            return process.MainWindowHandle;

        return IntPtr.Zero;
    }

    //Functions utillizing the user32.dll 
    //Documentation on user32.dll - http://www.pinvoke.net/index.aspx
    private class User32
    {
        public const int SHOWNORMAL = 1;
        public const int SHOWMINIMIZED = 2;
        public const int SHOWMAXIMIZED = 3;

        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool CloseClipboard();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("user32.dll")]
        public static extern bool EmptyClipboard();

        [DllImport("user32.dll")]
        public static extern IntPtr GetOpenClipboardWindow();

    }

}
