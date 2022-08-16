#include "pch.h"

#include <iostream>
#include <fstream>
#include <string>
#include <stdint.h>
#include <set>
#include <vector>

static char* exe_base = nullptr;

static std::fstream output_file;
static std::set<uint64_t> name_hash_set;

typedef uint64_t (*t_crc64_path)(const char* path, uint64_t hash);

t_crc64_path orig_crc64_path;
uint64_t hook_crc64_path(const char* path, uint64_t hash)
{
    hash = orig_crc64_path(path, hash);
    if (output_file.is_open() && name_hash_set.find(hash) == name_hash_set.end())
    {
        output_file << path << std::endl;
        name_hash_set.insert(hash);
    }
    return hash;
}

std::uint8_t* PatternScan(void* module, const char* signature)
{
    static auto pattern_to_byte = [](const char* pattern) {
        auto bytes = std::vector<int>{};
        auto start = const_cast<char*>(pattern);
        auto end = const_cast<char*>(pattern) + strlen(pattern);

        for (auto current = start; current < end; ++current) {
            if (*current == '?') {
                ++current;
                if (*current == '?')
                    ++current;
                bytes.push_back(-1);
            }
            else {
                bytes.push_back(strtoul(current, &current, 16));
            }
        }
        return bytes;
    };

    auto dosHeader = (PIMAGE_DOS_HEADER)module;
    auto ntHeaders = (PIMAGE_NT_HEADERS)((std::uint8_t*)module + dosHeader->e_lfanew);

    auto sizeOfImage = ntHeaders->OptionalHeader.SizeOfImage;
    auto patternBytes = pattern_to_byte(signature);
    auto scanBytes = reinterpret_cast<std::uint8_t*>(module);

    auto s = patternBytes.size();
    auto d = patternBytes.data();

    for (auto i = 0ul; i < sizeOfImage - s; ++i) {
        bool found = true;
        for (auto j = 0ul; j < s; ++j) {
            if (scanBytes[i + j] != d[j] && d[j] != -1) {
                found = false;
                break;
            }
        }
        if (found) {
            return &scanBytes[i];
        }
    }
    return nullptr;
}

void InitHook()
{
    exe_base = (char*)GetModuleHandleA("Spider-Man.exe");
    if (!exe_base)
    {
        MessageBoxA(0, "Failed to find exe base.", "Spiderman filename hook", 0);
        return;
    }

    MH_Initialize();

    const char* pattern_crc64_path =
        "4C 8B CA "
        "4C 8B D1 "
        "48 85 C9 "
        "0F 84 ?? ?? ?? ?? "
        "0F B6 01 "
        "84 C0 "
        "74 ?? "
        "4C 8D 41 01 ";

    void* crc64_addr = PatternScan(exe_base, pattern_crc64_path);

    MH_CreateHook(crc64_addr, hook_crc64_path, (LPVOID*)&orig_crc64_path);
    MH_EnableHook(crc64_addr);

    output_file = std::fstream("filenames.txt", std::fstream::in | std::fstream::out | std::fstream::app);
    std::string line;
    while (std::getline(output_file, line))
    {
        name_hash_set.insert(orig_crc64_path(line.data(), 0xC96C5795D7870F42));
    }
    output_file.clear();
}

BOOL APIENTRY DllMain(HMODULE hModule, DWORD ul_reason_for_call, LPVOID lpReserved)
{
    switch (ul_reason_for_call)
    {
        case DLL_PROCESS_ATTACH:
        {
            proxy::InitProxy();
            // MessageBox(NULL, TEXT("Alert"), TEXT("DLL Proxy initialized"), MB_OK);
            InitHook();
            break;
        }
        case DLL_THREAD_ATTACH:
        case DLL_THREAD_DETACH:
        case DLL_PROCESS_DETACH:
        {
            FreeLibrary(proxy::hL);
            break;
        }
    }
    return TRUE;
}

