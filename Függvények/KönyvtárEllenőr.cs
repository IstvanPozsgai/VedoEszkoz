using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;


namespace VédőEszköz
{
    public class KönyvtárEllenőr
    {

        readonly string ikonNév = $"{Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName)}.lnk";
        readonly string Program = AppDomain.CurrentDomain.FriendlyName;
        readonly string AsztalElérésiÚt = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string DriveName;
        string BetűJel = "";

        public void Megfelelő(string hely)
        {
            try
            {
                if (NincsMeghajtó(hely))
                {
                    BetűJel = SzabadMeghajtóBetű();
                    DriveName = ÚjMeghajtó(hely);
                }
                IkonVizsgálat(hely);
                JóProgramIndítása();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool NincsMeghajtó(string hely)
        {
            bool Válasz = true;
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (Pathing.GetUNCPath(drive.Name) == hely)
                {
                    DriveName = drive.Name;
                    Válasz = false;
                }

            }
            return Válasz;
        }

        public string SzabadMeghajtóBetű()
        {
            string Válasz = "";
            string ElsőBetű = "V";
            char KezdőBetű = 'G';
            HashSet<string> FoglaltMeghajtók = new HashSet<string>(Environment.GetLogicalDrives());

            if (!FoglaltMeghajtók.Contains($@"{ElsőBetű}:\"))
                Válasz = ElsőBetű;
            else
                for (char betű = KezdőBetű; betű <= 'Z'; betű++)
                    if (!FoglaltMeghajtók.Contains($@"{betű}:\"))
                    {
                        Válasz = betű.ToString();
                        break;
                    }
            return Válasz;
        }

        public string ÚjMeghajtó(string hely)
        {
            string Válasz = "";
            try
            {
                Pathing.MapNetworkDrive(BetűJel, hely);
                Válasz = $@"{BetűJel}:\";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt a hálózati meghajtó létrehozásakor: {ex.Message}",
                                "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Válasz;
        }

        public void IkonVizsgálat(string hely)
        {
            DirectoryInfo di = new DirectoryInfo(AsztalElérésiÚt);
            FileInfo[] Fájlok = di.GetFiles("*.lnk");

            foreach (FileInfo Elem in Fájlok)
                if (IkonVan(hely, Elem.ToString()))
                    IkonMód(hely);
                else
                    IkonLétrehoz(hely);
        }

        public bool IkonVan(string hely, string Ikon)
        {
            bool válasz = false;
            string ParancsikonÚt = Path.Combine(AsztalElérésiÚt, ikonNév);

            WshShell shell = new WshShell();
            IWshShortcut lnk = shell.CreateShortcut(Ikon) as IWshShortcut;
            //Van olyan ikon ami a program.exe  és az UNC
            if (lnk.TargetPath.Contains(Program) && lnk.WorkingDirectory == hely) válasz = true;
            return válasz;
        }

        public void IkonMód(string hely)
        {
            string ParancsikonÚt = Path.Combine(AsztalElérésiÚt, ikonNév);

            WshShell shell = new WshShell();
            IWshShortcut shortcut = shell.CreateShortcut(ParancsikonÚt) as IWshShortcut;

            // Parancsikon módosítása
            shortcut.TargetPath = Path.Combine(DriveName, Program);
            shortcut.WorkingDirectory = DriveName;
            shortcut.Description = $"Hely: {DriveName}";
            if (!string.IsNullOrWhiteSpace(DriveName))
                shortcut.IconLocation = Path.Combine(DriveName, Program);
            shortcut.Save();
        }

        public void IkonLétrehoz(string hely)
        {
            string ParancsikonÚt = Path.Combine(AsztalElérésiÚt, ikonNév);

            WshShell shell = new WshShell();
            IWshShortcut shortcut = shell.CreateShortcut(ParancsikonÚt) as IWshShortcut;
            shortcut.TargetPath = Path.Combine(DriveName, Program);
            shortcut.Description = $"Hely: {DriveName}";
            shortcut.IconLocation = DriveName;
            shortcut.Save();
        }

        public void JóProgramIndítása()
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = Path.Combine(DriveName, Program),
                    UseShellExecute = true

                });
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }

    public static class Pathing
    {
        [DllImport("mpr.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int WNetGetConnection(
            [MarshalAs(UnmanagedType.LPTStr)] string localName,
            [MarshalAs(UnmanagedType.LPTStr)] StringBuilder remoteName,
            ref int length);


        //Visszaadja az UNC utat
        //https://www.c-sharpcorner.com/UploadFile/sri85kanth/accessing-network-drive-in-C-Sharp/
        /// <summary>
        /// Given a path, returns the UNC path or the original. (No exceptions
        /// are raised by this function directly). For example, "P:\2008-02-29"
        /// might return: "\\networkserver\Shares\Photos\2008-02-09"
        /// </summary>
        /// <param name="originalPath">The path to convert to a UNC Path</param>
        /// <returns>A UNC path. If a network drive letter is specified, the
        /// drive letter is converted to a UNC or network path. If the
        /// originalPath cannot be converted, it is returned unchanged.</returns>


        public static string GetUNCPath(string originalPath)
        {
            StringBuilder sb = new StringBuilder(512);
            int size = sb.Capacity;
            // look for the {LETTER}: combination ...
            if (originalPath.Length > 2 && originalPath[1] == ':')
            {
                // don't use char.IsLetter here - as that can be misleading
                // the only valid drive letters are a-z && A-Z.
                char c = originalPath[0];
                if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))
                {
                    int error = WNetGetConnection(originalPath.Substring(0, 2), sb, ref size);
                    if (error == 0)
                    {
                        //   DirectoryInfo dir = new DirectoryInfo(originalPath);
                        string path = Path.GetFullPath(originalPath).Substring(Path.GetPathRoot(originalPath).Length);
                        return Path.Combine(sb.ToString().TrimEnd(), path);
                    }
                }
            }
            return originalPath;
        }

        private enum ResourceScope
        {
            RESOURCE_CONNECTED = 1,
            RESOURCE_GLOBALNET,
            RESOURCE_REMEMBERED,
            RESOURCE_RECENT,
            RESOURCE_CONTEXT
        }
        private enum ResourceType
        {
            RESOURCETYPE_ANY,
            RESOURCETYPE_DISK,
            RESOURCETYPE_PRINT,
            RESOURCETYPE_RESERVED
        }
        private enum ResourceUsage
        {
            RESOURCEUSAGE_CONNECTABLE = 0x00000001,
            RESOURCEUSAGE_CONTAINER = 0x00000002,
            RESOURCEUSAGE_NOLOCALDEVICE = 0x00000004,
            RESOURCEUSAGE_SIBLING = 0x00000008,
            RESOURCEUSAGE_ATTACHED = 0x00000010
        }
        private enum ResourceDisplayType
        {
            RESOURCEDISPLAYTYPE_GENERIC,
            RESOURCEDISPLAYTYPE_DOMAIN,
            RESOURCEDISPLAYTYPE_SERVER,
            RESOURCEDISPLAYTYPE_SHARE,
            RESOURCEDISPLAYTYPE_FILE,
            RESOURCEDISPLAYTYPE_GROUP,
            RESOURCEDISPLAYTYPE_NETWORK,
            RESOURCEDISPLAYTYPE_ROOT,
            RESOURCEDISPLAYTYPE_SHAREADMIN,
            RESOURCEDISPLAYTYPE_DIRECTORY,
            RESOURCEDISPLAYTYPE_TREE,
            RESOURCEDISPLAYTYPE_NDSCONTAINER
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct NETRESOURCE
        {
            public ResourceScope oResourceScope;
            public ResourceType oResourceType;
            public ResourceDisplayType oDisplayType;
            public ResourceUsage oResourceUsage;
            public string sLocalName;
            public string sRemoteName;
            public string sComments;
            public string sProvider;
        }

        [DllImport("mpr.dll")]
        private static extern int WNetAddConnection2(ref NETRESOURCE oNetworkResource, string sPassword, string sUserName, int iFlags);

        [DllImport("mpr.dll")]
        private static extern int WNetCancelConnection2(string sLocalName, uint iFlags, int iForce);

        public static void MapNetworkDrive(string sDriveLetter, string sNetworkPath)
        {
            //Checks if the last character is \ as this causes error on mapping a drive.
            if (sNetworkPath.Substring(sNetworkPath.Length - 1, 1) == @"\")
                sNetworkPath = sNetworkPath.Substring(0, sNetworkPath.Length - 1);

            NETRESOURCE oNetworkResource = new NETRESOURCE
            {
                oResourceType = ResourceType.RESOURCETYPE_DISK,
                sLocalName = sDriveLetter + ":",
                sRemoteName = sNetworkPath
            };

            //If Drive is already mapped disconnect the current 
            //mapping before adding the new mapping
            if (IsDriveMapped(sDriveLetter))
                DisconnectNetworkDrive(sDriveLetter, true);

            WNetAddConnection2(ref oNetworkResource, null, null, 0);
        }

        public static int DisconnectNetworkDrive(string sDriveLetter, bool bForceDisconnect)
        {
            if (bForceDisconnect)
                return WNetCancelConnection2(sDriveLetter + ":", 0, 1);
            else
                return WNetCancelConnection2(sDriveLetter + ":", 0, 0);
        }

        public static bool IsDriveMapped(string sDriveLetter)
        {
            string[] DriveList = Environment.GetLogicalDrives();
            for (int i = 0; i < DriveList.Length; i++)
                if (sDriveLetter + ":\\" == DriveList[i].Trim())
                    return true;

            return false;
        }
    }

}
