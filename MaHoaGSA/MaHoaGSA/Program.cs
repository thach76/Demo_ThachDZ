using System;
using System.Text;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace MaHoaRSA
{
    public class Key
    {
        public int k1 { get; set; }
        public int k2 { get; set; }
        public Key() { }

    }
    internal class Program
    {
        public static int ModN(int n, int m)
        {
            if (n >= 0)
            {
                return n % 26;
            }
            else
            {
                return (n + m * ((int)-n/m + 1)) % m;
            }
        }

        public static int EuclidMoRong(int a, int m)
        {
            for (int i = 2; i < m; i++)
            {
                if ((a * i) % m == 1)
                {
                    return i;
                }
            }
            return 0;
        }

        public static int TimUCLN(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }


        // Hàm kiểm tra số nguyên tố
        static bool KiemTraSoNguyenTo(int n)
        {
            // Kiểm tra nếu số n là số nguyên tố
            if (n <= 1)
                return false; // Nếu n là số âm hoặc bằng 1, không phải là số nguyên tố
            if (n <= 3)
                return true;  // Nếu n là 2 hoặc 3, là số nguyên tố

            if (n % 2 == 0 || n % 3 == 0)
                return false; // Nếu n chia hết cho 2 hoặc 3, không phải là số nguyên tố

            for (int i = 5; i * i <= n; i += 6)
            {
                if (n % i == 0 || n % (i + 2) == 0)
                    return false; // Nếu n chia hết cho i hoặc i + 2, không phải là số nguyên tố
            }

            return true; // Nếu không có số nào từ 2 đến căn bậc hai của n chia hết cho n, thì n là số nguyên tố
        }

        public static void TaoKey(Key publicKey, Key privateKey)
        {
            int p = 17, q = 7;
            do
            {
                Console.WriteLine("Nhập vào 2 số p, q: ");
                p = int.Parse(Console.ReadLine());
                q = int.Parse(Console.ReadLine());
            }
            while (!(KiemTraSoNguyenTo(p) && KiemTraSoNguyenTo(q)));

            int N = p * q;
            int OmegaN = (p - 1) * (q - 1);
            int e = 0;

            for (int i = 6; i < OmegaN; i++)
            {
                if (TimUCLN(i, OmegaN) == 1)
                {
                    e = i;
                    break;
                }
            }
            //if (p==17)
            e = 13;

            int d = EuclidMoRong(e, OmegaN);

            //Console.WriteLine("N là: " + N);
            //Console.WriteLine("O(N) là: " + OmegaN);
            Console.WriteLine("e là: " + e);
            Console.WriteLine("d là: " + d);

            
            publicKey.k1 = e;
            publicKey.k2 = N;
            
            privateKey.k1 = d;
            privateKey.k2 = N;
        }

        public static int powMod (int n, int m,int modulus)
        {
            int result = 1;

            for (int i = 0; i < m; i++)
            {
                result = (result * n) % modulus;
            }
            return result;
        }

        public static string MaHoa(Key key, string input)
        {
            string output = "";
            for (int i = 0; i<input.Length; i++)
            {
                int T = input[i];
                int c =  powMod(T, key.k1, key.k2);
                output += " " + c;
            }
            return output;
        }

        public static string GiaiMa(Key privateKey, string input)
        {
            string output = "";
            // Sử dụng phương thức Split để tách chuỗi thành mảng các từ
            string[] words = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Khai báo một mảng số nguyên
            int[] numbers = new int[words.Length];

            // Chuyển đổi từng từ thành số nguyên và lưu vào mảng numbers
            for (int i = 0; i < words.Length; i++)
            {
                if (int.TryParse(words[i], out int num))
                {
                    numbers[i] = num;
                }
                else
                {
                    // Xử lý trường hợp không thể chuyển đổi thành số nguyên
                    Console.WriteLine($"Không thể chuyển đổi từ '{words[i]}' thành số nguyên.");
                }
            }

            
            foreach (int num in numbers)
            {
                char c = (char)powMod(num, privateKey.k1, privateKey.k2);
                output += c;
            }

            return output;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Key publicKey = new Key();
            Key privateKey = new Key();
            TaoKey(publicKey, privateKey);

            Console.WriteLine("Vui lòng chọn mã hóa hoặc giải mã: ");
            Console.WriteLine("1. Mã Hóa");
            Console.WriteLine("2. Giải Mã");
            Console.WriteLine("3. Thoát");
            int chon = 0;
            do
            {
                chon = int.Parse(Console.ReadLine());
                switch (chon)
                {
                    case 1:
                        Console.WriteLine("Vui lòng nhập văn bản để mã hóa: ");
                        string txt = Console.ReadLine();
                        string maHoa = MaHoa(publicKey, txt);
                        Console.WriteLine("văn bản đã được mã hóa thành: " + maHoa);
                        break;
                    case 2:
                        Console.WriteLine("Vui lòng nhập văn bản để giải mã: ");
                        string txt2 = Console.ReadLine();
                        string giaiMa = GiaiMa(privateKey, txt2);
                        Console.WriteLine("văn bản đã được giải mã thành: " + giaiMa);
                        break;
                    default: break;

                }
            }
            while (chon != 1 || chon != 2);


            Console.ReadKey();
        }
    }
}
