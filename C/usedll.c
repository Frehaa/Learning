#include "windows.h" // Definitions for LoadLibrary + setting the target architecture
#include "stdio.h"  // printf + OutputDebugStringA
#include <io.h>

typedef int IntToInt(int);

// LoadLibrary does not care if correct type
// typedef char IntToInt(char);         

// LoadLibrary does not even care if correct signature
// typedef char IntToInt(char, char);

// Even this works if you still pass it a value
// But the compiler does not mind if you do not pass it a value
// typedef int IntToInt();

#include <stdint.h>

typedef struct {
    char* name;
    int age;
    int type;
} Cow;

typedef struct {
    char* name;
    int age;
    int type;
} Cat;

int main(int argc, char* argv) {
// int WinMain(HINSTANCE instance, HINSTANCE prevInstance, LPSTR commandLine, int showCode){

    Cow betty;
    betty.name = "Betty";
    betty.age = 12;
    betty.type = 1;

    int32_t t = ('a' << 24) + ('b' << 16) + ('c' << 8) + 'd';
    char *test = (char*) &t;
    printf("t = %d\n", t);
    printf(test);

    // This does not work for some reason :'<
    // Cat test2 = (Cat) betty;

    // printf("Write name of dll to load: ");

    // char inbuf[50] = {0};
    // int c;
    // for (size_t i = 0; i < 40; i++)
    // {
    //     c = fgetc(stdin);
    //     if (c == 10) break;
    //     inbuf[i] = c;
    // }
    
    // // HANDLE STDIN = GetStdHandle(STD_INPUT_HANDLE);
    // // int bytesRead = _read((int)STDIN, inbuf, 40);
    // // if (bytesRead <= 0) {
    // //     printf("Problem reading");
    // //     return 0;
    // // }

    // HMODULE add2dll = LoadLibraryA(inbuf);
    // if (add2dll == NULL) {
    //     printf("Failed to load dll: ");
    //     printf(inbuf);
    //     // OutputDebugStringA("Failed to load dll");
    //     return 0;
    // }
    // printf("\nSuccessfully loaded ");
    // printf(inbuf);

    // printf("\nWrite name of procedure to load: ");
    // char inbufproc[50] = {0};
    // for (size_t i = 0; i < 40; i++)
    // {
    //     c = fgetc(stdin);
    //     if (c == 10) break;
    //     inbuf[i] = c;
    // }

    // IntToInt *proc = (IntToInt *)GetProcAddress(add2dll, "add2");
    // if (proc == NULL) {
    //     printf("Failed to find procedure: ");
    //     printf(inbufproc);
    //     // OutputDebugStringA("Failed to find procedure");
    //     return 0;
    // }
    // printf("\nSuccessfully loaded ");
    // printf(inbufproc);

    // int x = 4;

    // char buf[50];
    // sprintf(buf, "x = %d - proc(x) = %d", x, proc(x));
    // printf(buf);
    // // OutputDebugStringA(buf);
    // return 0;
}