using System.Security.Cryptography;
using System.Text;

public static class Jelszó
{
    public static string HashPassword(string password)
    {
        using (SHA256 sha = SHA256.Create())
        {
            byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }

    //string hash = HashPassword(felhasznaloJelszo);
    //  string sql = $"INSERT INTO Users (UserName, [Password]) VALUES ('{userName}', '{hash}')";
}

