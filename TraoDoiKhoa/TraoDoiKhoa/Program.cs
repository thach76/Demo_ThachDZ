using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraoDoiKhoa
{

    

    internal class Program
    {
        static Random random = new Random();

        static bool IsPrime(long number)
        {
            if (number <= 1)
                return false;

            if (number <= 3)
                return true;

            if (number % 2 == 0 || number % 3 == 0)
                return false;

            for (long i = 5; i * i <= number; i += 6)
            {
                if (number % i == 0 || number % (i + 2) == 0)
                    return false;
            }

            return true;
        }

        static int GenerateRandomPrime(int min, int max)
        {
            int randomPrime;
            do
            {
                randomPrime = random.Next(min, max);
            } while (!IsPrime(randomPrime));

            return randomPrime;
        }

        static long ModuloPower(long a, long b, long c)
        {
            if (c == 1)
                return 0;

            long result = 1;

            for (int i = 0; i < b; i++)
            {
                result = (result * a) % c;
            }

            return result;
        }


        static bool IsPrimitiveRoot(int g, int p)
        {
            if (g <= 1 || p <= 1)
                return false;

            int phi = p - 1; // Tính số Euler phi(p)

            for (int i = 2; i <= Math.Sqrt(phi); i++)
            {
                if (phi % i == 0)
                {
                    // Nếu phi(p) chia hết cho i, kiểm tra xem g^i mod p có bằng 1
                    if (ModuloPower(g, i, p) == 1)
                        return false;

                    // Kiểm tra xem g^(phi(p)/i) mod p có bằng 1
                    if (ModuloPower(g, phi / i, p) == 1)
                        return false;
                }
            }

            // Nếu không có điều kiện nào thỏa mãn, g là căn nguyên thủy modulo p
            return true;
        }


        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            //chọn ngẫu nhiên p,g,a,b
            long p = GenerateRandomPrime(2000, 5000);
            long g = GenerateRandomPrime(2000, 5000);
            long a = random.Next(1, 1000);
            long b = random.Next(1, 1000);

            Console.WriteLine($"a = {ModuloPower(7873,5 , 7873)}");
            //Nhập từ bàn phím
            /*            Console.Write("Nhập p: ");
                        int p = int.Parse(Console.ReadLine());
                        Console.Write("Nhập g: ");
                        int g = int.Parse(Console.ReadLine());
            *//*            if (IsPrimitiveRoot(g, p))
                        {
                            Console.WriteLine("g không phải căn nguyên thủy của p!!");
                            Console.ReadKey();
                        }*/
            /*if (!IsPrime(p))
            {
                Console.WriteLine("p không phải số nguyên tố!");
                Console.ReadKey();
            }
            if (!IsPrime(g))
            {
                Console.WriteLine("g không phải số nguyên tố!!");
                Console.ReadKey();
            }*/
            /*            if (p < g)
                        {
                            Console.WriteLine("g không thể lớn hơn p!!!");
                            Console.ReadKey();
                        }*//*
                        Console.Write("Nhập a: ");
                        long a = long.Parse(Console.ReadLine());
                        Console.Write("Nhập b: ");
                        long b = long.Parse(Console.ReadLine());*/




            //Tính công khai A và B
            long A = ModuloPower(g, a, p);
            long B = ModuloPower(g, b, p);
            
            //tính s
            long sA = ModuloPower(B, a, p);
            long sB = ModuloPower(A, b, p);

            //in ra màn hình
            Console.WriteLine($"Pritave\np = {p}\ng = {g}\na = {a}\nb = {b}\n");
            Console.WriteLine($"Public\nA = {A}\nB = {B}\n\n");
            //Console.WriteLine($"c = {t}, C = {T} , {ModuloPower(T, t, p)}");
            Console.WriteLine("sA = " + sA);
            Console.WriteLine("sB = " + sB);

            //long t = random.Next(1, 100000);
            Console.Write("Kiểm tra\nNhập t: ");
            long t = long.Parse(Console.ReadLine());
            //long T = ModuloPower(g, t, p);
            long sTA = ModuloPower(A, t, p);
            long sTB = ModuloPower(B, t, p);
            Console.WriteLine($"sT tính theo A = {sTA}\nsT tính theo B = {sTB}\n");
            Console.ReadKey();
        }

    }
    
}
