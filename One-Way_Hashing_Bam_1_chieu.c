#include <stdio.h>
#include <stdint.h>
#include <string.h>

// Định nghĩa các hàm và hằng số
#define ROTRIGHT(word, shift) ((word >> shift) | (word << (32 - shift)))
#define CH(x, y, z) ((x & y) ^ (~x & z))
#define MAJ(x, y, z) ((x & y) ^ (x & z) ^ (y & z))
#define EP0(x) (ROTRIGHT(x, 2) ^ ROTRIGHT(x, 13) ^ ROTRIGHT(x, 22))
#define EP1(x) (ROTRIGHT(x, 6) ^ ROTRIGHT(x, 11) ^ ROTRIGHT(x, 25))
#define SIG0(x) (ROTRIGHT(x, 7) ^ ROTRIGHT(x, 18) ^ (x >> 3))
#define SIG1(x) (ROTRIGHT(x, 17) ^ ROTRIGHT(x, 19) ^ (x >> 10))

// Khởi tạo các giá trị hash ban đầu
uint32_t initialHashValues[] = {
    0x6a09e667, 0xbb67ae85, 0x3c6ef372, 0xa54ff53a,
    0x510e527f, 0x9b05688c, 0x1f83d9ab, 0x5be0cd19
};

uint32_t kConstants[64] = {
    0x428a2f98, 0x71374491, 0xb5c0fbcf, 0xe9b5dba5,
    0x3956c25b, 0x59f111f1, 0x923f82a4, 0xab1c5ed5,
    0xd807aa98, 0x12835b01, 0x243185be, 0x550c7dc3,
    // ... Các giá trị khác cho kConstants ...
    0x6a09e667, 0xbb67ae85, 0x3c6ef372, 0xa54ff53a,
    0x510e527f, 0x9b05688c, 0x1f83d9ab, 0x5be0cd19
};

// Độ dài tối đa của chuỗi dữ liệu
#define MAX_MESSAGE_LENGTH 1024

void sha256(const char *message, uint32_t hash[8]) {
    uint32_t w[64];
    uint32_t a, b, c, d, e, f, g, h, i, t1, t2;

    // Tiền xử lý: Tính độ dài của chuỗi dữ liệu
    size_t messageLen = strlen(message);
    size_t paddedLen = messageLen + 1;
    while (paddedLen % 64 != 56) {
        paddedLen++;
    }
    size_t numChunks = paddedLen / 64;

    // Xử lý từng khối
    for (size_t chunk = 0; chunk < numChunks; chunk++) {
        // Chia khối thành các từ 32-bit
        for (i = 0; i < 16; i++) {
             w[i] = (message[chunk * 64 + i * 4] << 24) |
                    (message[chunk * 64 + i * 4 + 1] << 16) |
                    (message[chunk * 64 + i * 4 + 2] << 8) |
                    (message[chunk * 64 + i * 4 + 3]); // Chia khối thành các từ 32-bit
        }

        // Đặt các từ còn lại trong bản tin
        for (i = 16; i < 64; i++) {
            w[i] = SIG1(w[i-2]) + w[i-7] + SIG0(w[i-15]) + w[i-16];
        }

        // Khởi tạo giá trị hash
        for (i = 0; i < 8; i++) {
            hash[i] = initialHashValues[i];
        }

        // Quá trình nén
        for (i = 0; i < 64; i++) {
            t1 = h + EP1(e) + CH(e, f, g) + kConstants[i] + w[i];
            t2 = EP0(a) + MAJ(a, b, c);

            h = g;
            g = f;
            f = e;
            e = d + t1;
            d = c;
            c = b;
            b = a;
            a = t1 + t2;
        }

        // Cập nhật giá trị hash
        hash[0] += a;
        hash[1] += b;
        hash[2] += c;
        hash[3] += d;
        hash[4] += e;
        hash[5] += f;
        hash[6] += g;
        hash[7] += h;
    }
}

//0061ffcc 771ae170 943eb527 fffffffe 0061ff08 771a826d 00401d70 0061ff84
//0061ffcc 771ae170 536d708e fffffffe 0061ff08 771a826d 00401d70 0061ff84


int main() {
    char message[] = "Hello, worlg";
    uint32_t hash[8];
    sha256(message, hash);

    printf("SHA-256 Hash: ");
    for (int i = 0; i < 8; i++) {
        printf("%08x ", hash[i]);
    }
    printf("\n");

    return 0;
}
