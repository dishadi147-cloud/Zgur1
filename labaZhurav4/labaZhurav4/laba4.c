#define _CRT_SECURE_NO_WARNINGS
#define _CRT_NONSTDC_NO_DEPRECATE
#include <stdio.h>
#include <string.h>
#include <windows.h>

#define SIZE 101
#define CHAIN_MAX 10


char dKeys[SIZE][50];
char dVals[SIZE][100];
int dUsed[SIZE];
int dCnt;

int h1(char* k) {
    if (k == NULL || k[0] == '\0') return 0;
    unsigned int h = 0;
    for (int i = 0; k[i] != '\0' && i < 50; i++) {
        h = (h * 31 + (unsigned char)k[i]) % SIZE;
    }
    return h % SIZE;
}

int h2(char* k) {
    if (k == NULL || k[0] == '\0') return 1;
    unsigned int h = 0;
    for (int i = 0; k[i] != '\0' && i < 50; i++) {
        h = (h * 17 + (unsigned char)k[i]) % (SIZE - 1);
    }
    return (h % (SIZE - 1)) + 1;
}

void dInsert(char* k, char* v) {
    if (k == NULL || v == NULL) return;

    int hash1 = h1(k);
    int hash2 = h2(k);

    for (int i = 0; i < SIZE; i++) {
        int idx = (hash1 + i * hash2) % SIZE;
        if (idx < 0 || idx >= SIZE) continue;

        if (dUsed[idx] == 0) {
            strncpy(dKeys[idx], k, 49);
            dKeys[idx][49] = '\0';
            strncpy(dVals[idx], v, 99);
            dVals[idx][99] = '\0';
            dUsed[idx] = 1;
            dCnt++;
            return;
        }
        else if (strcmp(dKeys[idx], k) == 0) {
            strncpy(dVals[idx], v, 99);
            dVals[idx][99] = '\0';
            return;
        }
    }
}

char* dFind(char* k) {
    if (k == NULL) return NULL;

    int hash1 = h1(k);
    int hash2 = h2(k);

    for (int i = 0; i < SIZE; i++) {
        int idx = (hash1 + i * hash2) % SIZE;
        if (idx < 0 || idx >= SIZE) continue;

        if (dUsed[idx] == 0) return NULL;
        if (strcmp(dKeys[idx], k) == 0) {
            return dVals[idx];
        }
    }
    return NULL;
}


char cKeys[SIZE][CHAIN_MAX][50];
char cVals[SIZE][CHAIN_MAX][100];
int cCounts[SIZE];

int cHash(char* k) {
    if (k == NULL || k[0] == '\0') return 0;
    unsigned int h = 0;
    for (int i = 0; k[i] != '\0' && i < 50; i++) {
        h = (h * 31 + (unsigned char)k[i]) % SIZE;
    }
    return h % SIZE;
}

void cInsert(char* k, char* v) {
    if (k == NULL || v == NULL) return;

    int idx = cHash(k);
    if (idx < 0 || idx >= SIZE) return;


    for (int i = 0; i < cCounts[idx]; i++) {
        if (strcmp(cKeys[idx][i], k) == 0) {
            strncpy(cVals[idx][i], v, 99);
            cVals[idx][i][99] = '\0';
            return;
        }
    }

 
    if (cCounts[idx] < CHAIN_MAX) {
        strncpy(cKeys[idx][cCounts[idx]], k, 49);
        cKeys[idx][cCounts[idx]][49] = '\0';
        strncpy(cVals[idx][cCounts[idx]], v, 99);
        cVals[idx][cCounts[idx]][99] = '\0';
        cCounts[idx]++;
    }
    else {
        printf("Предупреждение: цепочка в ячейке %d переполнена!\n", idx);
    }
}

char* cFind(char* k) {
    if (k == NULL) return NULL;

    int idx = cHash(k);
    if (idx < 0 || idx >= SIZE) return NULL;

    for (int i = 0; i < cCounts[idx]; i++) {
        if (strcmp(cKeys[idx][i], k) == 0) {
            return cVals[idx][i];
        }
    }
    return NULL;
}


char bloom[200][50];
int bCnt;

void bAdd(char* s) {
    if (s == NULL) return;
    if (bCnt >= 0 && bCnt < 200) {
        strncpy(bloom[bCnt], s, 49);
        bloom[bCnt][49] = '\0';
        bCnt++;
    }
}

int bCheck(char* s) {
    if (s == NULL) return 0;
    for (int i = 0; i < bCnt && i < 200; i++) {
        if (strcmp(bloom[i], s) == 0) {
            return 1;
        }
    }
    return 0;
}



typedef struct Node {
    char key[50];
    char val[100];
    struct Node* prev;
    struct Node* next;
} Node;

Node* hashTable[SIZE]; 

int listHash(char* k) {
    unsigned int h = 0;
    for (int i = 0; k[i]; i++) h = (h * 31 + k[i]) % SIZE;
    return h % SIZE;
}

Node* createNode(char* k, char* v) {
    Node* new = (Node*)malloc(sizeof(Node));
    strcpy(new->key, k);
    strcpy(new->val, v);
    new->prev = new->next = NULL;
    return new;
}

void listInsert(char* k, char* v) {
    int idx = listHash(k);


    for (Node* cur = hashTable[idx]; cur; cur = cur->next)
        if (strcmp(cur->key, k) == 0) { strcpy(cur->val, v); return; }


    Node* new = createNode(k, v);
    new->next = hashTable[idx];
    if (hashTable[idx]) hashTable[idx]->prev = new;
    hashTable[idx] = new;
}

char* listFind(char* k) {
    int idx = listHash(k);
    for (Node* cur = hashTable[idx]; cur; cur = cur->next)
        if (strcmp(cur->key, k) == 0) return cur->val;
    return NULL;
}

void listDelete(char* k) {
    int idx = listHash(k);
    for (Node* cur = hashTable[idx]; cur; cur = cur->next) {
        if (strcmp(cur->key, k) == 0) {
            if (cur->prev) cur->prev->next = cur->next;
            else hashTable[idx] = cur->next;
            if (cur->next) cur->next->prev = cur->prev;
            free(cur);
            return;
        }
    }
}






int main1() {
    SetConsoleOutputCP(1251);
    SetConsoleCP(1251);

 
    dCnt = 0;
    for (int i = 0; i < SIZE; i++) {
        dUsed[i] = 0;
        cCounts[i] = 0;
        memset(dKeys[i], 0, sizeof(dKeys[i]));
        memset(dVals[i], 0, sizeof(dVals[i]));
        for (int j = 0; j < CHAIN_MAX; j++) {
            memset(cKeys[i][j], 0, sizeof(cKeys[i][j]));
            memset(cVals[i][j], 0, sizeof(cVals[i][j]));
        }
    }

    memset(bloom, 0, sizeof(bloom));
    bCnt = 0;



    printf("1. ДВОЙНОЕ ХЭШИРОВАНИЕ\n");
    dInsert("бетон", "4500 руб");
    dInsert("арматура", "60000 руб");
    dInsert("кирпич", "12000 руб");

    char* res1 = dFind("бетон");
    if (res1 != NULL) printf("Поиск бетон: %s\n", res1);
    else printf("Поиск бетон: не найдено\n");

    char* res2 = dFind("арматура");
    if (res2 != NULL) printf("Поиск арматура: %s\n", res2);
    else printf("Поиск арматура: не найдено\n");

    char* res3 = dFind("доска");
    if (res3 != NULL) printf("Поиск доска: %s\n", res3);
    else printf("Поиск доска: не найдено\n");

    printf("\n");

    printf("2. МЕТОД ЦЕПОЧЕК (элемент - массив)\n");
    cInsert("фундамент", "120000 руб");
    cInsert("стены", "80000 руб");
    cInsert("крыша", "150000 руб");

    char* res4 = cFind("фундамент");
    if (res4 != NULL) printf("Поиск фундамент: %s\n", res4);
    else printf("Поиск фундамент: не найдено\n");

    char* res5 = cFind("стены");
    if (res5 != NULL) printf("Поиск стены: %s\n", res5);
    else printf("Поиск стены: не найдено\n");

    char* res6 = cFind("окна");
    if (res6 != NULL) printf("Поиск окна: %s\n", res6);
    else printf("Поиск окна: не найдено\n");

    printf("\n");

    printf("3. ФИЛЬТР БЛУМА (доп)\n");
    bAdd("смета1");
    bAdd("смета2");
    printf("Проверка смета1: %s\n", bCheck("смета1") ? "возможно есть" : "нет");
    printf("Проверка смета2: %s\n", bCheck("смета2") ? "возможно есть" : "нет");
    printf("Проверка смета3: %s\n", bCheck("смета3") ? "возможно есть" : "нет");

    for (int i = 0; i < SIZE; i++) hashTable[i] = NULL;

    printf("\n4. МЕТОД ЦЕПОЧЕК (ДВУСВЯЗНЫЙ СПИСОК)\n");
    listInsert("бетон", "4500 руб");
    listInsert("арматура", "60000 руб");
    listInsert("кирпич", "12000 руб");

    printf("Поиск бетон: %s\n", listFind("бетон") ? listFind("бетон") : "не найдено");
    printf("Поиск доска: %s\n", listFind("доска") ? listFind("доска") : "не найдено");

    listDelete("бетон");
    printf("После удаления бетон: %s\n", listFind("бетон") ? "есть" : "нет");




    return 0;
}
