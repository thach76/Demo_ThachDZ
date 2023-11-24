using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace MaHoaHiu
{
    class MaTran
    {
        public int[] phanTu = new int[10];
        public int capMaTran = new int();
        public MaTran()
        {
            capMaTran = 2;
            for (int i = 0; i < 10; i++)
            {
                phanTu[i] = 0;
            }
        }

        public MaTran(int capMaTran)
        {
            this.capMaTran = capMaTran;
            for (int i = 0; i < 10; i++)
            {
                phanTu[i] = 0;
            }
        }

        

        public int Det()
        {
            if (capMaTran == 2)
            {
                return phanTu[0] * phanTu[3] - phanTu[1] * phanTu[2];
            }
            else if (capMaTran == 3)
            {
                return phanTu[0] * phanTu[4] * phanTu[8] + phanTu[1] * phanTu[5] * phanTu[6] + phanTu[2] * phanTu[3] * phanTu[7]
                        - phanTu[2] * phanTu[4] * phanTu[6] - phanTu[0] * phanTu[5] * phanTu[7] - phanTu[1] * phanTu[3] * phanTu[8];
            }
            else
            {
                return 0;
            }
        }

        public MaTran NhanVoiSo(int num)
        {
            MaTran B = new MaTran(3);
            B.capMaTran = capMaTran;
            for (int i = 0; i < capMaTran * capMaTran; i++)
            {
                B.phanTu[i] = phanTu[i] * num;
            }
            return B;

        }

        public MaTran NhanVoi12(MaTran A)
        {
            MaTran B = new MaTran(2);
            if (capMaTran == 2)
            {
                B.phanTu[0] = A.phanTu[0] * phanTu[0] + A.phanTu[1] * phanTu[1];
                B.phanTu[1] = A.phanTu[0] * phanTu[2] + A.phanTu[1] * phanTu[3];
            }
            
            return B;
        }

        public MaTran NhanVoi13(MaTran A)
        {
            MaTran B = new MaTran(3);
            if (capMaTran == 3)
            {
                B.phanTu[0] = A.phanTu[0] * phanTu[0] + A.phanTu[1] * phanTu[3] + A.phanTu[2] * phanTu[6];
                B.phanTu[1] = A.phanTu[0] * phanTu[1] + A.phanTu[1] * phanTu[4] + A.phanTu[2] * phanTu[7];
                B.phanTu[2] = A.phanTu[0] * phanTu[2] + A.phanTu[1] * phanTu[5] + A.phanTu[2] * phanTu[8];
            }
            return B;
        }

        public MaTran Adj()
        {
            MaTran B = new MaTran(3);
            if (capMaTran == 2)
            {
                B.phanTu[0] = phanTu[3];
                B.phanTu[1] = -phanTu[1];
                B.phanTu[2] = -phanTu[2];
                B.phanTu[3] = phanTu[0];
                B.capMaTran = 2;
            }
            else if (capMaTran == 3)
            {
                B.phanTu[0] = phanTu[4] * phanTu[8] - phanTu[5] * phanTu[7];
                B.phanTu[3] = phanTu[5] * phanTu[6] - phanTu[3] * phanTu[8];
                B.phanTu[6] = phanTu[3] * phanTu[7] - phanTu[4] * phanTu[6];
                B.phanTu[1] = phanTu[7] * phanTu[2] - phanTu[8] * phanTu[1];
                B.phanTu[4] = phanTu[8] * phanTu[0] - phanTu[6] * phanTu[2];
                B.phanTu[7] = phanTu[6] * phanTu[1] - phanTu[7] * phanTu[0];
                B.phanTu[2] = phanTu[1] * phanTu[5] - phanTu[2] * phanTu[4];
                B.phanTu[5] = phanTu[2] * phanTu[3] - phanTu[0] * phanTu[5];
                B.phanTu[8] = phanTu[0] * phanTu[4] - phanTu[1] * phanTu[3];
            }
            return B;
        }

        public void inMaTran(int number)
        {
            for (int i = 0; i< number; i++)
            {
                Console.Write(phanTu[i] + "\t");
                if ((i+1) % capMaTran  == 0)
                {
                    Console.WriteLine();
                }
            }
        }

        public void nhapMaTran()
        {
            for (int i = 0; i < capMaTran * capMaTran; i++)
            {
                Console.Write("Nhap Phan Tu " + (i+1) + ": " );
                phanTu[i] = int.Parse(Console.ReadLine());
            }
        }

        public static int ModToB(int a, int b)
        {
            if (a >= 0)
                return a % b;
            else
            {
                int c = (int)-a / b;
                return a + c * b + b;
            }
        }

        public MaTran Mod(int num)
        {
            MaTran B = new MaTran(3);
            B.capMaTran = capMaTran;
            for (int i = 0; i < capMaTran * capMaTran; i++)
            {
                B.phanTu[i] = ModToB(phanTu[i], num);
            }
            return B;
        }

        public string ChuyenTxt()
        {
            string txt = "";
            for (int i = 0; i < capMaTran; i++)
            {
                char character = (char)phanTu[i];
                character += 'a';
                txt += character.ToString();
            }
            return txt;
        }


    }
    internal class Program
    {
        public static MaTran[] chiaMaTran(MaTran[] maTran, string text, int soMT, int capMaTran)
        {
            //int soMT = (int) text.Length / capMaTran;
            int leng = 0;

            //MaTran[] maTran = new MaTran[1000];

            for (int i = 0; i < soMT; i++)
            {
                //maTran[i].capMaTran = capMaTran;
                maTran[i] = new MaTran(capMaTran);
                if (capMaTran == 2)
                {
                    maTran[i].phanTu[0] = text[leng++] - 'a';
                    maTran[i].phanTu[1] = text[leng++] - 'a';
                }
                else if (capMaTran == 3)
                {
                    maTran[i].phanTu[0] = text[leng++] - 'a';
                    maTran[i].phanTu[1] = text[leng++] - 'a';
                    maTran[i].phanTu[2] = text[leng++] - 'a';
                }
            }

            if (text.Length % capMaTran != 0)
            {
                //maTran[soMT].capMaTran = capMaTran;
                maTran[soMT] = new MaTran(capMaTran);
                if (capMaTran == 2)
                {
                    maTran[soMT].phanTu[0] = text[leng++] - 'a';
                    maTran[soMT].phanTu[1] = 'x' - 'a';
                }
                else if (capMaTran == 3)
                {
                    maTran[soMT].phanTu[0] = text[leng++] - 'a';
                    if (text.Length % capMaTran == 1)
                    {
                        maTran[soMT].phanTu[1] = 'x' - 'a';
                        maTran[soMT].phanTu[2] = 'x' - 'a';
                    }
                    else
                    {
                        maTran[soMT].phanTu[1] = text[leng++] - 'a';
                        maTran[soMT].phanTu[2] = 'x' - 'a';
                    }

                }
            }

            return maTran;
        }

        public static int ModToB(int a, int b)
        {
            if (a >= 0)
                return a % b;
            else
            {
                int c = (int)-a / b;
                return a + c * b + b;
            }
        }
        public static int EuclidMoRong(int a, int m)
        {
            for (int i = 1; i < m; i++)
            {
                if (ModToB(a * i, m) == 1)
                {
                    return i;
                }
            }
            return 0;
        }




        public static void MaHoaHiu(MaTran key, int capMaTran)
        {
            

        }

        public static void GiaiMaHiu()
        {

        }

        static string InsertCharacter(string input, char characterToAdd, int positionToAdd)
        {
            if (positionToAdd < 0 || positionToAdd > input.Length)
            {
                throw new ArgumentOutOfRangeException("positionToAdd", "Vị trí thêm không hợp lệ.");
            }

            // Sử dụng Substring để chia chuỗi thành hai phần và sau đó nối ký tự vào giữa
            string output = input.Substring(0, positionToAdd) + characterToAdd + input.Substring(positionToAdd);

            return output;
        }

        static string CombineWithSpaces(string input1, string input2)
        {

            // Tạo một chuỗi mới để lưu trữ kết quả
            int k = 0;

            for (int i = 0; i < input1.Length; i++)
            {
                if (input1[i] == ' ')
                {
                    input2 = InsertCharacter(input2, ' ', i );
                    k++;
                }
            }

            return input2;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Mã hóa Hiu vui lòng chọn mã hóa hoặc giải mã");
            Console.WriteLine("1. Mã Hóa   ");
            Console.WriteLine("2. Giải Mã  ");
            int luaChon = int.Parse(Console.ReadLine());
            Console.WriteLine("Vui lòng nhâp vào 1 chuỗi: ");
            string input = Console.ReadLine();
            string input2 = input.Replace(" ", "");
            Console.WriteLine("Vui lòng nhập vào cấp ma trận: ");
            int capMaTran = int.Parse(Console.ReadLine());
            Console.WriteLine("Vui lòng nhập key: ");
            MaTran key = new MaTran(capMaTran);
            MaTran keyGiaiMa = new MaTran(capMaTran);
            key.nhapMaTran();
            /*            key.phanTu[0] = 17;
                        key.phanTu[1] = 17;
                        key.phanTu[2] = 5;
                        key.phanTu[3] = 21;

                        key.phanTu[4] = 18;
                        key.phanTu[5] = 21;
                        key.phanTu[6] = 2;
                        key.phanTu[7] = 2;
                        key.phanTu[8] = 19;*/

            /*key.phanTu[0] = 16;
            key.phanTu[1] = 15;
            key.phanTu[2] = 5;
            key.phanTu[3] = 20;

            key.phanTu[4] = 19;
            key.phanTu[5] = 21;
            key.phanTu[6] = 3;
            key.phanTu[7] = 4;
            key.phanTu[8] = 18;*/
            int soMT = input2.Length / capMaTran;
            MaTran[] chiaMT = new MaTran[soMT + 1];
            MaTran[] maHoa = new MaTran[soMT + 1];
            MaTran[] giaiMa = new MaTran[soMT + 1];

            for (int i = 0; i < soMT + 1; i++)
            {
                chiaMT[i] = new MaTran(capMaTran);
                maHoa[i] = new MaTran(capMaTran);
                giaiMa[i] = new MaTran(capMaTran);
            }
            
            chiaMaTran(chiaMT, input2, soMT, capMaTran);
            int num = EuclidMoRong(ModToB(key.Det(), 26), 26);
            //Console.WriteLine(ModToB(key.Det(), 26));
            keyGiaiMa = key.Adj().NhanVoiSo(num).Mod(26);
            if (num == 0)
            {
                Console.WriteLine("Key bạn nhập có vấn đề vui lòng kiểm tra lại!!");
                
            }
            string strMaHoa = "";
            string strGiaiMa = "";
            do
            {
                switch (luaChon)
                {

                    //mã hóa
                    case 1:
                        strMaHoa = "";
                        if (input2.Length % capMaTran != 0)
                            soMT++;
                        for (int i = 0; i < soMT; i++)
                        {

                            if (capMaTran == 2)
                                maHoa[i] = key.NhanVoi12(chiaMT[i]);
                            if (capMaTran == 3)
                                maHoa[i] = key.NhanVoi13(chiaMT[i]);

                            maHoa[i] = maHoa[i].Mod(26);
                            strMaHoa += maHoa[i].ChuyenTxt();

                        }
                        strMaHoa = CombineWithSpaces(input, strMaHoa);
                        Console.WriteLine("mã hóa: " + strMaHoa);
                        break;
                    case 2:
                        strGiaiMa = "";
                        if (input2.Length % capMaTran != 0)
                            soMT++;
                        for (int i = 0; i < soMT; i++)
                        {

                            if (capMaTran == 2)
                                maHoa[i] = keyGiaiMa.NhanVoi12(chiaMT[i]);
                            if (capMaTran == 3)
                                maHoa[i] = keyGiaiMa.NhanVoi13(chiaMT[i]);

                            maHoa[i] = maHoa[i].Mod(26);
                            strGiaiMa += maHoa[i].ChuyenTxt();
                            maHoa[i].inMaTran(3);
                        }
                        strGiaiMa = CombineWithSpaces(input, strGiaiMa);
                        Console.WriteLine("giải mã: " +strGiaiMa);
                        break;

                    default: break;
                }
            }
            while (!(luaChon == 1 || luaChon == 2));
            
            


            //MaHoaHiu();
            Console.ReadKey();
        }
    }
}
