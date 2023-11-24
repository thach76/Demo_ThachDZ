using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp_SHA256_SHA512
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string MaHoaSHA256(string input)
        {
            // Initialize hash values
            uint h0 = 0x6a09e667;
            uint h1 = 0xbb67ae85;
            uint h2 = 0x3c6ef372;
            uint h3 = 0xa54ff53a;
            uint h4 = 0x510e527f;
            uint h5 = 0x9b05688c;
            uint h6 = 0x1f83d9ab;
            uint h7 = 0x5be0cd19;

            // Initialize array of round constants
            uint[] k = new uint[]
            {
            0x428a2f98, 0x71374491, 0xb5c0fbcf, 0xe9b5dba5, 0x3956c25b, 0x59f111f1, 0x923f82a4, 0xab1c5ed5,
            0xd807aa98, 0x12835b01, 0x243185be, 0x550c7dc3, 0x72be5d74, 0x80deb1fe, 0x9bdc06a7, 0xc19bf174,
            0xe49b69c1, 0xefbe4786, 0x0fc19dc6, 0x240ca1cc, 0x2de92c6f, 0x4a7484aa, 0x5cb0a9dc, 0x76f988da,
            0x983e5152, 0xa831c66d, 0xb00327c8, 0xbf597fc7, 0xc6e00bf3, 0xd5a79147, 0x06ca6351, 0x14292967,
            0x27b70a85, 0x2e1b2138, 0x4d2c6dfc, 0x53380d13, 0x650a7354, 0x766a0abb, 0x81c2c92e, 0x92722c85,
            0xa2bfe8a1, 0xa81a664b, 0xc24b8b70, 0xc76c51a3, 0xd192e819, 0xd6990624, 0xf40e3585, 0x106aa070,
            0x19a4c116, 0x1e376c08, 0x2748774c, 0x34b0bcb5, 0x391c0cb3, 0x4ed8aa4a, 0x5b9cca4f, 0x682e6ff3,
            0x748f82ee, 0x78a5636f, 0x84c87814, 0x8cc70208, 0x90befffa, 0xa4506ceb, 0xbef9a3f7, 0xc67178f2
            };

            // Sample message (you should replace this with your actual message data)
            byte[] message = Encoding.ASCII.GetBytes(input);

            // Pre-processing (Padding)
            int originalLength = message.Length * 8;
            int appendedLength = originalLength + 1 + 64; // 1 bit '1' and 64 bits for the original message length
            int extraBits = 512 - (appendedLength % 512);
            int totalBits = appendedLength + extraBits;

            byte[] paddedMessage = new byte[totalBits / 8];
            message.CopyTo(paddedMessage, 0);
            paddedMessage[message.Length] = 0x80; // Append '1' bit
            BitConverter.GetBytes((ulong)originalLength).CopyTo(paddedMessage, totalBits / 8 - 8);

            // Process the message in successive 512-bit chunks
            for (int chunkStart = 0; chunkStart < totalBits; chunkStart += 512)
            {
                byte[] chunk = new byte[64];
                Array.Copy(paddedMessage, chunkStart / 8, chunk, 0, 64);

                // Create a 64-entry message schedule array w[0..63] of 32-bit words
                uint[] w = new uint[64];

                // Copy the chunk into the first 16 words of the message schedule array
                for (int i = 0; i < 16; i++)
                {
                    w[i] = BitConverter.ToUInt32(chunk, i * 4);
                }

                // Extend the first 16 words into the remaining 48 words of the message schedule array
                for (int i = 16; i < 64; i++)
                {
                    uint s0 = (w[i - 15] >> 7 | w[i - 15] << 25) ^ (w[i - 15] >> 18 | w[i - 15] << 14) ^ (w[i - 15] >> 3);
                    uint s1 = (w[i - 2] >> 17 | w[i - 2] << 15) ^ (w[i - 2] >> 19 | w[i - 2] << 13) ^ (w[i - 2] >> 10);
                    w[i] = w[i - 16] + s0 + w[i - 7] + s1;
                }

                // Initialize working variables to current hash value
                uint a = h0;
                uint b = h1;
                uint c = h2;
                uint d = h3;
                uint e = h4;
                uint f = h5;
                uint g = h6;
                uint h = h7;

                // Compression function main loop
                for (int i = 0; i < 64; i++)
                {
                    uint S1 = (e >> 6 | e << 26) ^ (e >> 11 | e << 21) ^ (e >> 25 | e << 7);
                    uint ch = (e & f) ^ (~e & g);
                    uint temp1 = h + S1 + ch + k[i] + w[i];
                    uint S0 = (a >> 2 | a << 30) ^ (a >> 13 | a << 19) ^ (a >> 22 | a << 10);
                    uint maj = (a & b) ^ (a & c) ^ (b & c);
                    uint temp2 = S0 + maj;

                    h = g;
                    g = f;
                    f = e;
                    e = d + temp1;
                    d = c;
                    c = b;
                    b = a;
                    a = temp1 + temp2;
                }

                // Add the compressed chunk to the current hash value
                h0 += a;
                h1 += b;
                h2 += c;
                h3 += d;
                h4 += e;
                h5 += f;
                h6 += g;
                h7 += h;
            }

            // Produce the final hash value (big-endian)
            byte[] digest = new byte[32];
            BitConverter.GetBytes(h0).CopyTo(digest, 0);
            BitConverter.GetBytes(h1).CopyTo(digest, 4);
            BitConverter.GetBytes(h2).CopyTo(digest, 8);
            BitConverter.GetBytes(h3).CopyTo(digest, 12);
            BitConverter.GetBytes(h4).CopyTo(digest, 16);
            BitConverter.GetBytes(h5).CopyTo(digest, 20);
            BitConverter.GetBytes(h6).CopyTo(digest, 24);
            BitConverter.GetBytes(h7).CopyTo(digest, 28);

            // Display the final hash value as a hexadecimal string
            string hashString = BitConverter.ToString(digest).Replace("-", "").ToLower();
            Console.WriteLine("SHA-256 Hash: " + hashString);
            return hashString;
        }



        private void btMaHoa_Click(object sender, EventArgs e)
        {
            string input = txtInput.Text;
            string output = MaHoaSHA256(input);
            lbOutput.Text = output;
        }
    }
    
}
