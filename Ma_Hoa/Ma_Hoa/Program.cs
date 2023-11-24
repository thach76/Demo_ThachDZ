using Ma_Hoa;
using System;
using System.Text;
using System.IO;

namespace Ma_Hoa
{
    static class MaHoa
    {
        public static string ReadFile(string filePath)
        {

            string text = "";
            // Nếu tệp không tồn tại, tạo tệp mới và viết chuỗi vào
            if (!File.Exists(filePath))
            {
                string contentToWrite = "this is a sample text to write to the file";
                File.WriteAllText(filePath, contentToWrite);
                Console.WriteLine($"Đang tạo file\n.\n.\n.\n.");
                Console.WriteLine("Đã tạo file");

            }


            try
            {
                text = File.ReadAllText(filePath);
                //Console.WriteLine(filePath);
                string currentDirectory = Directory.GetCurrentDirectory();
                //Console.WriteLine("Current Directory: " + currentDirectory);
                Console.WriteLine(text);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error reading or processing the input file: " + e.Message);
            }
            return text;
        }
        public static int FindGCD(int a, int b)
        {
            if (a < 1) return -1;
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        //Mã hóa Affine
        public static int modInverse(int a, int m)
        {
            a = a % m;
            for (int x = 1; x < m; x++)
                if ((a * x) % m == 1)
                    return x;
            return 1;
        }

        public static string Encrypt(string plainText, int a, int b)
        {
            string cipherText = "";
            foreach (char character in plainText)
            {
                if (char.IsLetter(character))
                {
                    char encryptedChar = (char)(((a * (character - 'a') + b) % 26) + 'a');
                    cipherText += encryptedChar;
                }
                else
                {
                    cipherText += character;
                }
            }
            return cipherText;
        }

        public static string Decrypt(string cipherText, int a, int b)
        {
            int aInverse = modInverse(a, 26);
            string plainText = "";
            foreach (char character in cipherText)
            {
                if (char.IsLetter(character))
                {
                    char decryptedChar = (char)(((aInverse * ((character - 'a') - b + 26)) % 26) + 'a');
                    plainText += decryptedChar;
                }
                else
                {
                    plainText += character;
                }
            }
            return plainText;
        }

        public static void Affine()
        {
            Console.WriteLine("Mã hóa Affine");
            Console.Write("Nhập một chuỗi ký tự: ");
            //string plaintext = Console.ReadLine().ToLower();
            string plaintext = ReadFile("input.txt");
            int a = 0, b= 0;
            do
            {
                Console.Write("Nhập vào key a (có UCLN(a, 26) = 1): ");
                a = int.Parse(Console.ReadLine());
            }
            while(FindGCD(a, 26) != 1);

            do
            {
                Console.Write("Nhập vào key b: ");
                b = int.Parse(Console.ReadLine());
            }
            while (b > 26 || b < 0);

            /*string encryptedText = Encrypt(plaintext, a, b);
            Console.WriteLine($"Mã hóa: {encryptedText}");
*/
            string decryptedText = Decrypt(plaintext, a, b);
            Console.WriteLine($"Giải mã: {decryptedText}");
            Console.ReadKey();
        }

        public static int Mod(int a, int m)
        {
            if (a >= 0)
            {
                return a % m;
            }
            else
            {
                int n = -a/m + 1;
                
                return (a + m * n ) % m;
            }
        }

        //mã hóa Vingenere
        public static string Encrypt(string plainText, string key)
        {
            string encryptedText = "";
            plainText = plainText.ToLower();
            int keyIndex = 0;

            foreach (char character in plainText)
            {
                if (char.IsLetter(character))
                {
                    int keyShift = key[keyIndex] - 'a';
                    char encryptedChar = (char)(Mod(character - 'a' + keyShift, 26) + 'a');
                    encryptedText += encryptedChar;

                    keyIndex = (keyIndex + 1) % key.Length;
                }
                else
                {
                    encryptedText += character;
                }
            }

            return encryptedText;
        }

        public static string Decrypt(string encryptedText, string key)
        {
            string decryptedText = "";
            encryptedText = encryptedText.ToLower();
            int keyIndex = 0;

            foreach (char character in encryptedText)
            {
                if (char.IsLetter(character))
                {
                    int keyShift = key[keyIndex] - 'a';
                    char decryptedChar = (char)(Mod(character - 'a' - keyShift + 26, 26) + 'a');
                    decryptedText += decryptedChar;

                    keyIndex = (keyIndex + 1) % key.Length;
                }
                else
                {
                    decryptedText += character;
                }
            }

            return decryptedText;
        }

        public static void Vigenere()
        {
            Console.WriteLine("Mã Vingenere");
            Console.Write("Nhập một chuỗi ký tự: ");
            //
            //string plaintext = Console.ReadLine().ToLower();
            string plaintext = ReadFile("input.txt");

            Console.WriteLine("Nhập Key :");
            string key = Console.ReadLine().ToLower();

            string encryptedText_1 = Encrypt(plaintext, key);
            Console.WriteLine($"Mã hóa text: {encryptedText_1}");

            string decryptedText_1 = Decrypt(encryptedText_1, key);
            Console.WriteLine($"Giải mã text: {decryptedText_1}");
            Console.ReadKey();
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.UTF8;
            MaHoa.Affine();
            Console.Clear();
            MaHoa.Vigenere();
            Console.WriteLine();
            
            Console.ReadKey ();

        }
    }
}
