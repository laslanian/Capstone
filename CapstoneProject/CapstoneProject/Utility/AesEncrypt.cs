using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

class AesEncrpyt
{
    private static string _KEY = "RaMPbxVnd1B7oLXZ15cGO30mkMww2BLt";
    private static string _IV = "dfuPgltqJg72BGgX";

    public string Encrypt(string decrypted)
    {
        byte[] textbytes = ASCIIEncoding.ASCII.GetBytes(decrypted);
        AesCryptoServiceProvider encdec = new AesCryptoServiceProvider();
        encdec.BlockSize = 128;
        encdec.KeySize = 256;
        encdec.Key = ASCIIEncoding.ASCII.GetBytes(_KEY);
        encdec.IV = ASCIIEncoding.ASCII.GetBytes(_IV);
        encdec.Padding = PaddingMode.PKCS7;
        encdec.Mode = CipherMode.CBC;

        ICryptoTransform icrypt = encdec.CreateEncryptor(encdec.Key, encdec.IV);

        byte[] encrypted = icrypt.TransformFinalBlock(textbytes, 0, textbytes.Length);
        icrypt.Dispose();

        return Convert.ToBase64String(encrypted);
    }

    public string Decrypt(string encrypted)
    {
        byte[] encbytes = ASCIIEncoding.ASCII.GetBytes(encrypted);
        AesCryptoServiceProvider encdec = new AesCryptoServiceProvider();
        encdec.BlockSize = 128;
        encdec.KeySize = 256;
        encdec.Key = ASCIIEncoding.ASCII.GetBytes(_KEY);
        encdec.IV = ASCIIEncoding.ASCII.GetBytes(_IV);
        encdec.Padding = PaddingMode.PKCS7;
        encdec.Mode = CipherMode.CBC;

        ICryptoTransform icrypt = encdec.CreateEncryptor(encdec.Key, encdec.IV);

        byte[] decrypted = icrypt.TransformFinalBlock(encbytes, 0, encbytes.Length);
        icrypt.Dispose();

        return ASCIIEncoding.ASCII.GetString(decrypted);

    }

}
