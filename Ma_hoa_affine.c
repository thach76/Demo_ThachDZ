//mã hóa affine
#include <stdio.h>
#include <string.h>

#define MAX_NUMBER_OF_LETTERS 256
#define NUMBER_OF_NO_VALUE 0

int ReadFileTxt(char path[], char fileData[]){
    FILE *f;
    char c;
    int indexFileData = 0;
    if ((f = fopen(path, "r")) == NULL){
        printf("Loi mo file!");
        return 0;
    }

    while ((c = fgetc(f)) != EOF){
        fileData[indexFileData++] = c;
    }

    fclose(f);
    return indexFileData;
}

void WriteFileTxt(char path[], char fileData[]){
    FILE *f = fopen(path, "w");

    fputs(fileData, f);

    fclose(f);
}

int extendedGCD(int a, int b, int *x, int *y) {
    int x0 = 1, x1 = 0;
    int y0 = 0, y1 = 1;

    while (b != 0) {
        int q = a / b;
        int temp = b;

        b = a % b;
        a = temp;

        temp = x1;
        x1 = x0 - q * x1;
        x0 = temp;

        temp = y1;
        y1 = y0 - q * y1;
        y0 = temp;
    }

    *x = x0;
    *y = y0;

    return a;
}

int modInverse(int a, int m) {
    int x, y;
    int gcd = extendedGCD(a, m, &x, &y);

    if (gcd != 1) {
        printf("Inverse does not exist.\n");
        return -1; // Inverse doesn't exist
    }

    return (x % m + m) % m;
}

char Ma_Hoa_Affine(int a, int b, char c){
    if (c == ' ')
        return c;
    return (char) (((a * c + b) % MAX_NUMBER_OF_LETTERS) + NUMBER_OF_NO_VALUE);
}

char Giai_Ma_Affine(int a, int b, char c){
    if (c == ' ')
        return c;
    return (char) (((c - NUMBER_OF_NO_VALUE - b) * modInverse(a, MAX_NUMBER_OF_LETTERS) ) % MAX_NUMBER_OF_LETTERS);
}

int Ma_Hoa(){
    char chuoiBanDau[10000];
    char chuoiMaHoa[10000];
    char chuoiGhiMaHoa[10000];
    char chuoiGiaiMa[10000];
    char pathFileRead[] = "bandau.txt";
    char pathFileWriteMaHoa[] = "mahoa.txt";
    char pathFileWriteGiaiMa[] = "giaima.txt";


    int a = 103;
    int b = 3;
    if (modInverse(a, MAX_NUMBER_OF_LETTERS) != -1){
        int n = ReadFileTxt(pathFileRead, chuoiBanDau);

        for (int i = 0; i < n ; i++ ){
            chuoiMaHoa[i] = Ma_Hoa_Affine(a, b, chuoiBanDau[i]);
        }

        WriteFileTxt(pathFileWriteMaHoa, chuoiMaHoa);

        n = ReadFileTxt(pathFileWriteMaHoa, chuoiGhiMaHoa);

        for (int i = 0; i < n ; i++ ){
            chuoiGiaiMa[i] = Giai_Ma_Affine(a, b, chuoiGhiMaHoa[i]);
        }

        WriteFileTxt(pathFileWriteGiaiMa, chuoiGiaiMa);
        puts(chuoiMaHoa);
        puts(chuoiGiaiMa);
        return 1;
    }
    else{
        printf("a khong hop le, a va 256 phai la 2 so nguyen to cung nhau!");
        return 0;
    }

}

int main(){
    //puts(chuoiMaHoa);
    Ma_Hoa();

    return 0;
}
