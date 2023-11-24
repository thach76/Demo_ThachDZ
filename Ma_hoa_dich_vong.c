//mã hóa dịch vòng
#include <stdio.h>
#include <string.h>

char MaHoaCaesar(int key, char c){
    if (c == ' ')
        c = ' ';
    else if (c >= 'A' && c <= 'Z') {
        c = 'A' + (c - 'A' + key) % 26;
    } else if (c >= 'a' && c <= 'z') {
        c = 'a' + (c - 'a' + key) % 26;
    }
    return c;
}

char GiaiMaCaesar(int key, char c){
    if (c == ' ')
        c = ' ';
    else if (c >= 'A' && c <= 'Z') {
        c = 'A' + (c - 'A' - key) % 26;
    } else if (c >= 'a' && c <= 'z') {
        c = 'a' + (c - 'a' - key) % 26;
    }
    return c;
}


char MaHoaDichVong(int key, char c){
    if (c == ' ')
        return c;
    else if (c + key > 'z')
        return c +  key - 26;
    return (char) (c + key);
}

char GiaiMaDichVong(int key, char c){
    if (c == ' ')
        return c;
    else if (c - key < 'a')
        return c -  key + 26;
    return (char) (c-key);
}

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

int main(){
    char chuoiBanDau[10000];
    char chuoiMaHoaDichVong[10000];
    char chuoiGiaiMaDichVong[10000];
    //char chuoiCCaesar[10000]
    char pathFileRead[] = "bandau.txt";
    char pathFileWriteMaHoa[] = "mahoa.txt";
    char pathFileWriteGiaiMa[] = "giaima.txt";

    int key = 9;
    int n = ReadFileTxt(pathFileRead, chuoiBanDau);

    for (int i = 0; i < n ; i++ ){
        chuoiMaHoaDichVong[i] = MaHoaDichVong(key, chuoiBanDau[i]);
    }


    for (int i = 0; i < n ; i++ ){
        chuoiGiaiMaDichVong[i] = GiaiMaDichVong(key, chuoiMaHoaDichVong[i]);
    }

    for


    //WriteFileTxt(pathFileWriteMaHoa, chuo);
    //WriteFileTxt(pathFileWriteGiaiMa, chuoiGiaiMaDichVong);

    printf("Chuoi ban dau:                      %s\n", chuoiBanDau);
    printf("Chuoi ma hoa theo dich vong:        %s\n", chuoiMaHoaDichVong);
    printf("Chuoi giai ma theo dich vong:       %s\n\n", chuoiGiaiMaDichVong);



    return 0;
}
