using System;
using System.Security.Cryptography;

class Program
{
    static void Main()
    {
        // Tạo cặp khóa RSA
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            // Lấy khóa công khai dưới dạng số nguyên lớn
            RSAParameters publicKeyParams = rsa.ExportParameters(false);

            // Lấy khóa riêng tư dưới dạng số nguyên lớn
            RSAParameters privateKeyParams = rsa.ExportParameters(true);

            // In khóa công khai và khóa riêng tư
            Console.WriteLine("Khóa công khai (Modulus):");
            Console.WriteLine(BitConverter.ToString(publicKeyParams.Modulus));

            Console.WriteLine("Khóa riêng tư (Modulus):");
            Console.WriteLine(BitConverter.ToString(privateKeyParams.Modulus));
            Console.ReadKey();
        }
    }
}
