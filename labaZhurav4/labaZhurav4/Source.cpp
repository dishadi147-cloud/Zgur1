#include <iostream>
#include <vector>
#include <stack>
#include <queue>
#include <unordered_map>
#include <algorithm>
#include <chrono>
#include <fstream>
#include <random>
#include <list>
#include <iomanip>
#include <string>
#include <windows.h>
using namespace std;
using namespace chrono;




class MyStack {
    vector<int> arr;
public:
    void push(int x) { arr.push_back(x); }
    void pop() { if (!arr.empty()) arr.pop_back(); }
    int top() { return arr.empty() ? -1 : arr.back(); }
    bool empty() { return arr.empty(); }
    size_t size() { return arr.size(); }
};


class MyQueue {
    vector<int> arr;
    size_t frontIdx = 0;
public:
    void push(int x) { arr.push_back(x); }
    void pop() { if (!empty()) frontIdx++; }
    int front() { return empty() ? -1 : arr[frontIdx]; }
    bool empty() { return frontIdx >= arr.size(); }
    size_t size() { return arr.size() - frontIdx; }
};


class MyList {
    struct Node {
        int data;
        Node* next;
        Node(int d) : data(d), next(nullptr) {}
    };
    Node* head;
    size_t sz;
public:
    MyList() : head(nullptr), sz(0) {}
    void push_front(int x) {
        Node* n = new Node(x);
        n->next = head;
        head = n;
        sz++;
    }
    void pop_front() {
        if (head) {
            Node* t = head;
            head = head->next;
            delete t;
            sz--;
        }
    }
    int front() { return head ? head->data : -1; }
    size_t size() { return sz; }
    bool empty() { return sz == 0; }
    ~MyList() { while (head) pop_front(); }
};


class MyHashMap {
    vector<vector<int>> table;
    int capacity;
    int numElements;
    int hash(int key) { return key % capacity; }
public:
    MyHashMap(int cap = 16) : capacity(cap), numElements(0) {
        table.resize(capacity);
    }
    void put(int key) {
        int idx = hash(key);
        for (int x : table[idx]) if (x == key) return;
        table[idx].push_back(key);
        numElements++;
    }
    bool contains(int key) {
        int idx = hash(key);
        for (int x : table[idx]) if (x == key) return true;
        return false;
    }
    void remove(int key) {
        int idx = hash(key);
        auto& bucket = table[idx];
        auto it = find(bucket.begin(), bucket.end(), key);
        if (it != bucket.end()) {
            bucket.erase(it);
            numElements--;
        }
    }
    size_t size() { return numElements; }
};


void quickSort(vector<int>& arr, int l, int r) {
    if (l >= r) return;
    int pivot = arr[(l + r) / 2];
    int i = l, j = r;
    while (i <= j) {
        while (arr[i] < pivot) i++;
        while (arr[j] > pivot) j--;
        if (i <= j) swap(arr[i++], arr[j--]);
    }
    quickSort(arr, l, j);
    quickSort(arr, i, r);
}

void bubbleSort(vector<int>& arr) {
    int n = arr.size();
    for (int i = 0; i < n - 1; i++)
        for (int j = 0; j < n - i - 1; j++)
            if (arr[j] > arr[j + 1]) swap(arr[j], arr[j + 1]);
}


vector<int> generateArray(int size) {
    vector<int> arr(size);
    random_device rd;
    mt19937 gen(rd());
    uniform_int_distribution<> dis(1, 100000);
    for (int i = 0; i < size; i++) arr[i] = dis(gen);
    return arr;
}

template<typename Func>
long long measureTime(Func func) {
    auto start = high_resolution_clock::now();
    func();
    auto end = high_resolution_clock::now();
    return duration_cast<microseconds>(end - start).count();
}

int main() {
    SetConsoleOutputCP(1251);



    vector<int> sizes = { 1000, 5000, 10000, 20000, 50000 };

    for (int n : sizes) {
        cout << "n=" << n << "\n";

        MyStack myS;
        stack<int> stlS;
        cout << "  Stack:   My=" << measureTime([&]() { for (int i = 0; i < n; i++) myS.push(i); })
            << " STL=" << measureTime([&]() { for (int i = 0; i < n; i++) stlS.push(i); }) << "\n";

        MyQueue myQ;
        queue<int> stlQ;
        cout << "  Queue:   My=" << measureTime([&]() { for (int i = 0; i < n; i++) myQ.push(i); })
            << " STL=" << measureTime([&]() { for (int i = 0; i < n; i++) stlQ.push(i); }) << "\n";

        MyList myL;
        list<int> stlL;
        cout << "  List:    My=" << measureTime([&]() { for (int i = 0; i < n; i++) myL.push_front(i); })
            << " STL=" << measureTime([&]() { for (int i = 0; i < n; i++) stlL.push_front(i); }) << "\n";

        MyHashMap myH;
        unordered_map<int, int> stlH;
        cout << "  Hash:    My=" << measureTime([&]() { for (int i = 0; i < n; i++) myH.put(i); })
            << " STL=" << measureTime([&]() { for (int i = 0; i < n; i++) stlH[i] = i; }) << "\n";

        auto a1 = generateArray(n), a2 = a1;
        cout << "  Sort:    Quick=" << measureTime([&]() { quickSort(a1, 0, n - 1); })
            << " STD=" << measureTime([&]() { sort(a2.begin(), a2.end()); }) << "\n\n";
    }

    system("pause");
    return 0;
}