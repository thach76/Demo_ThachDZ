#include <iostream>
#include <bitset>
#include <string>
#include <cmath>

//i - k mod 26

char BinToDec(char bin[]){
    int c = 0;

    for (int i = 7; i >= 0 ; i-- ){
        if (bin[i] == '1'){
            c +=  pow(2, 7- i);
        }
    }
    return  c;
}

std::string binaryToAscii(const char* binary) {
    std::string asciiString;

    while (*binary) {
        int asciiChar = 0;
        for (int i = 0; i < 8; ++i) {
            asciiChar = (asciiChar << 1) | (*binary - '0');
            ++binary;
        }
        asciiString += static_cast<char>(asciiChar);
    }

    return asciiString;
}


char Xos(char a, char b){
    if (a == '1' && b == '1' || a == '0' && b == '0')
        return '0';
    return '1';
}

int main() {
    std::string input = "thanh"; // Chuỗi đầu vào
    std::string binaryString;    // Chuỗi nhị phân
    std::string key = "deptr";
    std::string binaryStringKey;    // Chuỗi nhị phân
    char daura[1000];

    for (char c : input) {
        std::bitset<8> binaryChar(c); // Chuyển ký tự thành nhị phân
        binaryString += binaryChar.to_string(); // Ghép chuỗi nhị phân vào chuỗi tổng
    }

    for (char c : key) {
        std::bitset<8> binaryChar(c); // Chuyển ký tự thành nhị phân
        binaryStringKey += binaryChar.to_string(); // Ghép chuỗi nhị phân vào chuỗi tổng
    }

    std::cout << "Binary string:            " << binaryString << std::endl;
    std::cout << "Key Binary string:        " << binaryStringKey << std::endl;

    int n = binaryString.length();
    std::cout << n << std::endl;
    for (int i = 0; i < n ; i++ ){
        daura[i] = Xos(binaryString[i],binaryStringKey[i]);
    }
    std::cout << "Dau ra:                   " << daura << std::endl;

    std::cout << (int)BinToDec("01100001") << std::endl;

     std::string stringFromChar(daura);

    for (int i = 0; i < 8 ; i++ ){
        std::string subString = stringFromChar.substr(i, i+8);
        char charData[8] = subString.c_str();
        std::cout << BinToDec(charData)  << std::endl;
    }


    return 0;
}
