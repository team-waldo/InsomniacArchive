#include "pch.h"

using namespace proxy;

extern "C"
{
	FARPROC PA = NULL;
	int RunASM();

	void PROXY_DbgHelpCreateUserDump() {
		PA = p[0];
		RunASM();
	}
	void PROXY_DbgHelpCreateUserDumpW() {
		PA = p[1];
		RunASM();
	}
	void PROXY_EnumDirTree() {
		PA = p[2];
		RunASM();
	}
	void PROXY_EnumDirTreeW() {
		PA = p[3];
		RunASM();
	}
	void PROXY_EnumerateLoadedModules() {
		PA = p[4];
		RunASM();
	}
	void PROXY_EnumerateLoadedModules64() {
		PA = p[5];
		RunASM();
	}
	void PROXY_EnumerateLoadedModulesEx() {
		PA = p[6];
		RunASM();
	}
	void PROXY_EnumerateLoadedModulesExW() {
		PA = p[7];
		RunASM();
	}
	void PROXY_EnumerateLoadedModulesW64() {
		PA = p[8];
		RunASM();
	}
	void PROXY_ExtensionApiVersion() {
		PA = p[9];
		RunASM();
	}
	void PROXY_FindDebugInfoFile() {
		PA = p[10];
		RunASM();
	}
	void PROXY_FindDebugInfoFileEx() {
		PA = p[11];
		RunASM();
	}
	void PROXY_FindDebugInfoFileExW() {
		PA = p[12];
		RunASM();
	}
	void PROXY_FindExecutableImage() {
		PA = p[13];
		RunASM();
	}
	void PROXY_FindExecutableImageEx() {
		PA = p[14];
		RunASM();
	}
	void PROXY_FindExecutableImageExW() {
		PA = p[15];
		RunASM();
	}
	void PROXY_FindFileInPath() {
		PA = p[16];
		RunASM();
	}
	void PROXY_FindFileInSearchPath() {
		PA = p[17];
		RunASM();
	}
	void PROXY_GetSymLoadError() {
		PA = p[18];
		RunASM();
	}
	void PROXY_GetTimestampForLoadedLibrary() {
		PA = p[19];
		RunASM();
	}
	void PROXY_ImageDirectoryEntryToData() {
		PA = p[20];
		RunASM();
	}
	void PROXY_ImageDirectoryEntryToDataEx() {
		PA = p[21];
		RunASM();
	}
	void PROXY_ImageNtHeader() {
		PA = p[22];
		RunASM();
	}
	void PROXY_ImageRvaToSection() {
		PA = p[23];
		RunASM();
	}
	void PROXY_ImageRvaToVa() {
		PA = p[24];
		RunASM();
	}
	void PROXY_ImagehlpApiVersion() {
		PA = p[25];
		RunASM();
	}
	void PROXY_ImagehlpApiVersionEx() {
		PA = p[26];
		RunASM();
	}
	void PROXY_MakeSureDirectoryPathExists() {
		PA = p[27];
		RunASM();
	}
	void PROXY_MiniDumpReadDumpStream() {
		PA = p[28];
		RunASM();
	}
	void PROXY_MiniDumpWriteDump() {
		PA = p[29];
		RunASM();
	}
	void PROXY_RangeMapAddPeImageSections() {
		PA = p[30];
		RunASM();
	}
	void PROXY_RangeMapCreate() {
		PA = p[31];
		RunASM();
	}
	void PROXY_RangeMapFree() {
		PA = p[32];
		RunASM();
	}
	void PROXY_RangeMapRead() {
		PA = p[33];
		RunASM();
	}
	void PROXY_RangeMapRemove() {
		PA = p[34];
		RunASM();
	}
	void PROXY_RangeMapWrite() {
		PA = p[35];
		RunASM();
	}
	void PROXY_RemoveInvalidModuleList() {
		PA = p[36];
		RunASM();
	}
	void PROXY_ReportSymbolLoadSummary() {
		PA = p[37];
		RunASM();
	}
	void PROXY_SearchTreeForFile() {
		PA = p[38];
		RunASM();
	}
	void PROXY_SearchTreeForFileW() {
		PA = p[39];
		RunASM();
	}
	void PROXY_SetCheckUserInterruptShared() {
		PA = p[40];
		RunASM();
	}
	void PROXY_SetSymLoadError() {
		PA = p[41];
		RunASM();
	}
	void PROXY_StackWalk() {
		PA = p[42];
		RunASM();
	}
	void PROXY_StackWalk64() {
		PA = p[43];
		RunASM();
	}
	void PROXY_StackWalkEx() {
		PA = p[44];
		RunASM();
	}
	void PROXY_SymAddSourceStream() {
		PA = p[45];
		RunASM();
	}
	void PROXY_SymAddSourceStreamA() {
		PA = p[46];
		RunASM();
	}
	void PROXY_SymAddSourceStreamW() {
		PA = p[47];
		RunASM();
	}
	void PROXY_SymAddSymbol() {
		PA = p[48];
		RunASM();
	}
	void PROXY_SymAddSymbolW() {
		PA = p[49];
		RunASM();
	}
	void PROXY_SymAddrIncludeInlineTrace() {
		PA = p[50];
		RunASM();
	}
	void PROXY_SymAllocDiaString() {
		PA = p[51];
		RunASM();
	}
	void PROXY_SymCleanup() {
		PA = p[52];
		RunASM();
	}
	void PROXY_SymCompareInlineTrace() {
		PA = p[53];
		RunASM();
	}
	void PROXY_SymDeleteSymbol() {
		PA = p[54];
		RunASM();
	}
	void PROXY_SymDeleteSymbolW() {
		PA = p[55];
		RunASM();
	}
	void PROXY_SymEnumLines() {
		PA = p[56];
		RunASM();
	}
	void PROXY_SymEnumLinesW() {
		PA = p[57];
		RunASM();
	}
	void PROXY_SymEnumProcesses() {
		PA = p[58];
		RunASM();
	}
	void PROXY_SymEnumSourceFileTokens() {
		PA = p[59];
		RunASM();
	}
	void PROXY_SymEnumSourceFiles() {
		PA = p[60];
		RunASM();
	}
	void PROXY_SymEnumSourceFilesW() {
		PA = p[61];
		RunASM();
	}
	void PROXY_SymEnumSourceLines() {
		PA = p[62];
		RunASM();
	}
	void PROXY_SymEnumSourceLinesW() {
		PA = p[63];
		RunASM();
	}
	void PROXY_SymEnumSym() {
		PA = p[64];
		RunASM();
	}
	void PROXY_SymEnumSymbols() {
		PA = p[65];
		RunASM();
	}
	void PROXY_SymEnumSymbolsEx() {
		PA = p[66];
		RunASM();
	}
	void PROXY_SymEnumSymbolsExW() {
		PA = p[67];
		RunASM();
	}
	void PROXY_SymEnumSymbolsForAddr() {
		PA = p[68];
		RunASM();
	}
	void PROXY_SymEnumSymbolsForAddrW() {
		PA = p[69];
		RunASM();
	}
	void PROXY_SymEnumSymbolsW() {
		PA = p[70];
		RunASM();
	}
	void PROXY_SymEnumTypes() {
		PA = p[71];
		RunASM();
	}
	void PROXY_SymEnumTypesByName() {
		PA = p[72];
		RunASM();
	}
	void PROXY_SymEnumTypesByNameW() {
		PA = p[73];
		RunASM();
	}
	void PROXY_SymEnumTypesW() {
		PA = p[74];
		RunASM();
	}
	void PROXY_SymEnumerateModules() {
		PA = p[75];
		RunASM();
	}
	void PROXY_SymEnumerateModules64() {
		PA = p[76];
		RunASM();
	}
	void PROXY_SymEnumerateModulesW64() {
		PA = p[77];
		RunASM();
	}
	void PROXY_SymEnumerateSymbols() {
		PA = p[78];
		RunASM();
	}
	void PROXY_SymEnumerateSymbols64() {
		PA = p[79];
		RunASM();
	}
	void PROXY_SymEnumerateSymbolsW() {
		PA = p[80];
		RunASM();
	}
	void PROXY_SymEnumerateSymbolsW64() {
		PA = p[81];
		RunASM();
	}
	void PROXY_SymFindDebugInfoFile() {
		PA = p[82];
		RunASM();
	}
	void PROXY_SymFindDebugInfoFileW() {
		PA = p[83];
		RunASM();
	}
	void PROXY_SymFindExecutableImage() {
		PA = p[84];
		RunASM();
	}
	void PROXY_SymFindExecutableImageW() {
		PA = p[85];
		RunASM();
	}
	void PROXY_SymFindFileInPath() {
		PA = p[86];
		RunASM();
	}
	void PROXY_SymFindFileInPathW() {
		PA = p[87];
		RunASM();
	}
	void PROXY_SymFreeDiaString() {
		PA = p[88];
		RunASM();
	}
	void PROXY_SymFromAddr() {
		PA = p[89];
		RunASM();
	}
	void PROXY_SymFromAddrW() {
		PA = p[90];
		RunASM();
	}
	void PROXY_SymFromIndex() {
		PA = p[91];
		RunASM();
	}
	void PROXY_SymFromIndexW() {
		PA = p[92];
		RunASM();
	}
	void PROXY_SymFromInlineContext() {
		PA = p[93];
		RunASM();
	}
	void PROXY_SymFromInlineContextW() {
		PA = p[94];
		RunASM();
	}
	void PROXY_SymFromName() {
		PA = p[95];
		RunASM();
	}
	void PROXY_SymFromNameW() {
		PA = p[96];
		RunASM();
	}
	void PROXY_SymFromToken() {
		PA = p[97];
		RunASM();
	}
	void PROXY_SymFromTokenW() {
		PA = p[98];
		RunASM();
	}
	void PROXY_SymFunctionTableAccess() {
		PA = p[99];
		RunASM();
	}
	void PROXY_SymFunctionTableAccess64() {
		PA = p[100];
		RunASM();
	}
	void PROXY_SymFunctionTableAccess64AccessRoutines() {
		PA = p[101];
		RunASM();
	}
	void PROXY_SymGetDiaSession() {
		PA = p[102];
		RunASM();
	}
	void PROXY_SymGetExtendedOption() {
		PA = p[103];
		RunASM();
	}
	void PROXY_SymGetFileLineOffsets64() {
		PA = p[104];
		RunASM();
	}
	void PROXY_SymGetHomeDirectory() {
		PA = p[105];
		RunASM();
	}
	void PROXY_SymGetHomeDirectoryW() {
		PA = p[106];
		RunASM();
	}
	void PROXY_SymGetLineFromAddr() {
		PA = p[107];
		RunASM();
	}
	void PROXY_SymGetLineFromAddr64() {
		PA = p[108];
		RunASM();
	}
	void PROXY_SymGetLineFromAddrEx() {
		PA = p[109];
		RunASM();
	}
	void PROXY_SymGetLineFromAddrW64() {
		PA = p[110];
		RunASM();
	}
	void PROXY_SymGetLineFromInlineContext() {
		PA = p[111];
		RunASM();
	}
	void PROXY_SymGetLineFromInlineContextW() {
		PA = p[112];
		RunASM();
	}
	void PROXY_SymGetLineFromName() {
		PA = p[113];
		RunASM();
	}
	void PROXY_SymGetLineFromName64() {
		PA = p[114];
		RunASM();
	}
	void PROXY_SymGetLineFromNameEx() {
		PA = p[115];
		RunASM();
	}
	void PROXY_SymGetLineFromNameW64() {
		PA = p[116];
		RunASM();
	}
	void PROXY_SymGetLineNext() {
		PA = p[117];
		RunASM();
	}
	void PROXY_SymGetLineNext64() {
		PA = p[118];
		RunASM();
	}
	void PROXY_SymGetLineNextEx() {
		PA = p[119];
		RunASM();
	}
	void PROXY_SymGetLineNextW64() {
		PA = p[120];
		RunASM();
	}
	void PROXY_SymGetLinePrev() {
		PA = p[121];
		RunASM();
	}
	void PROXY_SymGetLinePrev64() {
		PA = p[122];
		RunASM();
	}
	void PROXY_SymGetLinePrevEx() {
		PA = p[123];
		RunASM();
	}
	void PROXY_SymGetLinePrevW64() {
		PA = p[124];
		RunASM();
	}
	void PROXY_SymGetModuleBase() {
		PA = p[125];
		RunASM();
	}
	void PROXY_SymGetModuleBase64() {
		PA = p[126];
		RunASM();
	}
	void PROXY_SymGetModuleInfo() {
		PA = p[127];
		RunASM();
	}
	void PROXY_SymGetModuleInfo64() {
		PA = p[128];
		RunASM();
	}
	void PROXY_SymGetModuleInfoW() {
		PA = p[129];
		RunASM();
	}
	void PROXY_SymGetModuleInfoW64() {
		PA = p[130];
		RunASM();
	}
	void PROXY_SymGetOmapBlockBase() {
		PA = p[131];
		RunASM();
	}
	void PROXY_SymGetOmaps() {
		PA = p[132];
		RunASM();
	}
	void PROXY_SymGetOptions() {
		PA = p[133];
		RunASM();
	}
	void PROXY_SymGetScope() {
		PA = p[134];
		RunASM();
	}
	void PROXY_SymGetScopeW() {
		PA = p[135];
		RunASM();
	}
	void PROXY_SymGetSearchPath() {
		PA = p[136];
		RunASM();
	}
	void PROXY_SymGetSearchPathW() {
		PA = p[137];
		RunASM();
	}
	void PROXY_SymGetSourceFile() {
		PA = p[138];
		RunASM();
	}
	void PROXY_SymGetSourceFileChecksum() {
		PA = p[139];
		RunASM();
	}
	void PROXY_SymGetSourceFileChecksumW() {
		PA = p[140];
		RunASM();
	}
	void PROXY_SymGetSourceFileFromToken() {
		PA = p[141];
		RunASM();
	}
	void PROXY_SymGetSourceFileFromTokenW() {
		PA = p[142];
		RunASM();
	}
	void PROXY_SymGetSourceFileToken() {
		PA = p[143];
		RunASM();
	}
	void PROXY_SymGetSourceFileTokenW() {
		PA = p[144];
		RunASM();
	}
	void PROXY_SymGetSourceFileW() {
		PA = p[145];
		RunASM();
	}
	void PROXY_SymGetSourceVarFromToken() {
		PA = p[146];
		RunASM();
	}
	void PROXY_SymGetSourceVarFromTokenW() {
		PA = p[147];
		RunASM();
	}
	void PROXY_SymGetSymFromAddr() {
		PA = p[148];
		RunASM();
	}
	void PROXY_SymGetSymFromAddr64() {
		PA = p[149];
		RunASM();
	}
	void PROXY_SymGetSymFromName() {
		PA = p[150];
		RunASM();
	}
	void PROXY_SymGetSymFromName64() {
		PA = p[151];
		RunASM();
	}
	void PROXY_SymGetSymNext() {
		PA = p[152];
		RunASM();
	}
	void PROXY_SymGetSymNext64() {
		PA = p[153];
		RunASM();
	}
	void PROXY_SymGetSymPrev() {
		PA = p[154];
		RunASM();
	}
	void PROXY_SymGetSymPrev64() {
		PA = p[155];
		RunASM();
	}
	void PROXY_SymGetSymbolFile() {
		PA = p[156];
		RunASM();
	}
	void PROXY_SymGetSymbolFileW() {
		PA = p[157];
		RunASM();
	}
	void PROXY_SymGetTypeFromName() {
		PA = p[158];
		RunASM();
	}
	void PROXY_SymGetTypeFromNameW() {
		PA = p[159];
		RunASM();
	}
	void PROXY_SymGetTypeInfo() {
		PA = p[160];
		RunASM();
	}
	void PROXY_SymGetTypeInfoEx() {
		PA = p[161];
		RunASM();
	}
	void PROXY_SymGetUnwindInfo() {
		PA = p[162];
		RunASM();
	}
	void PROXY_SymInitialize() {
		PA = p[163];
		RunASM();
	}
	void PROXY_SymInitializeW() {
		PA = p[164];
		RunASM();
	}
	void PROXY_SymLoadModule() {
		PA = p[165];
		RunASM();
	}
	void PROXY_SymLoadModule64() {
		PA = p[166];
		RunASM();
	}
	void PROXY_SymLoadModuleEx() {
		PA = p[167];
		RunASM();
	}
	void PROXY_SymLoadModuleExW() {
		PA = p[168];
		RunASM();
	}
	void PROXY_SymMatchFileName() {
		PA = p[169];
		RunASM();
	}
	void PROXY_SymMatchFileNameW() {
		PA = p[170];
		RunASM();
	}
	void PROXY_SymMatchString() {
		PA = p[171];
		RunASM();
	}
	void PROXY_SymMatchStringA() {
		PA = p[172];
		RunASM();
	}
	void PROXY_SymMatchStringW() {
		PA = p[173];
		RunASM();
	}
	void PROXY_SymNext() {
		PA = p[174];
		RunASM();
	}
	void PROXY_SymNextW() {
		PA = p[175];
		RunASM();
	}
	void PROXY_SymPrev() {
		PA = p[176];
		RunASM();
	}
	void PROXY_SymPrevW() {
		PA = p[177];
		RunASM();
	}
	void PROXY_SymQueryInlineTrace() {
		PA = p[178];
		RunASM();
	}
	void PROXY_SymRefreshModuleList() {
		PA = p[179];
		RunASM();
	}
	void PROXY_SymRegisterCallback() {
		PA = p[180];
		RunASM();
	}
	void PROXY_SymRegisterCallback64() {
		PA = p[181];
		RunASM();
	}
	void PROXY_SymRegisterCallbackW64() {
		PA = p[182];
		RunASM();
	}
	void PROXY_SymRegisterFunctionEntryCallback() {
		PA = p[183];
		RunASM();
	}
	void PROXY_SymRegisterFunctionEntryCallback64() {
		PA = p[184];
		RunASM();
	}
	void PROXY_SymSearch() {
		PA = p[185];
		RunASM();
	}
	void PROXY_SymSearchW() {
		PA = p[186];
		RunASM();
	}
	void PROXY_SymSetContext() {
		PA = p[187];
		RunASM();
	}
	void PROXY_SymSetDiaSession() {
		PA = p[188];
		RunASM();
	}
	void PROXY_SymSetExtendedOption() {
		PA = p[189];
		RunASM();
	}
	void PROXY_SymSetHomeDirectory() {
		PA = p[190];
		RunASM();
	}
	void PROXY_SymSetHomeDirectoryW() {
		PA = p[191];
		RunASM();
	}
	void PROXY_SymSetOptions() {
		PA = p[192];
		RunASM();
	}
	void PROXY_SymSetParentWindow() {
		PA = p[193];
		RunASM();
	}
	void PROXY_SymSetScopeFromAddr() {
		PA = p[194];
		RunASM();
	}
	void PROXY_SymSetScopeFromIndex() {
		PA = p[195];
		RunASM();
	}
	void PROXY_SymSetScopeFromInlineContext() {
		PA = p[196];
		RunASM();
	}
	void PROXY_SymSetSearchPath() {
		PA = p[197];
		RunASM();
	}
	void PROXY_SymSetSearchPathW() {
		PA = p[198];
		RunASM();
	}
	void PROXY_SymSrvDeltaName() {
		PA = p[199];
		RunASM();
	}
	void PROXY_SymSrvDeltaNameW() {
		PA = p[200];
		RunASM();
	}
	void PROXY_SymSrvGetFileIndexInfo() {
		PA = p[201];
		RunASM();
	}
	void PROXY_SymSrvGetFileIndexInfoW() {
		PA = p[202];
		RunASM();
	}
	void PROXY_SymSrvGetFileIndexString() {
		PA = p[203];
		RunASM();
	}
	void PROXY_SymSrvGetFileIndexStringW() {
		PA = p[204];
		RunASM();
	}
	void PROXY_SymSrvGetFileIndexes() {
		PA = p[205];
		RunASM();
	}
	void PROXY_SymSrvGetFileIndexesW() {
		PA = p[206];
		RunASM();
	}
	void PROXY_SymSrvGetSupplement() {
		PA = p[207];
		RunASM();
	}
	void PROXY_SymSrvGetSupplementW() {
		PA = p[208];
		RunASM();
	}
	void PROXY_SymSrvIsStore() {
		PA = p[209];
		RunASM();
	}
	void PROXY_SymSrvIsStoreW() {
		PA = p[210];
		RunASM();
	}
	void PROXY_SymSrvStoreFile() {
		PA = p[211];
		RunASM();
	}
	void PROXY_SymSrvStoreFileW() {
		PA = p[212];
		RunASM();
	}
	void PROXY_SymSrvStoreSupplement() {
		PA = p[213];
		RunASM();
	}
	void PROXY_SymSrvStoreSupplementW() {
		PA = p[214];
		RunASM();
	}
	void PROXY_SymUnDName() {
		PA = p[215];
		RunASM();
	}
	void PROXY_SymUnDName64() {
		PA = p[216];
		RunASM();
	}
	void PROXY_SymUnloadModule() {
		PA = p[217];
		RunASM();
	}
	void PROXY_SymUnloadModule64() {
		PA = p[218];
		RunASM();
	}
	void PROXY_UnDecorateSymbolName() {
		PA = p[219];
		RunASM();
	}
	void PROXY_UnDecorateSymbolNameW() {
		PA = p[220];
		RunASM();
	}
	void PROXY_WinDbgExtensionDllInit() {
		PA = p[221];
		RunASM();
	}
	void PROXY__EFN_DumpImage() {
		PA = p[222];
		RunASM();
	}
	void PROXY_block() {
		PA = p[223];
		RunASM();
	}
	void PROXY_chksym() {
		PA = p[224];
		RunASM();
	}
	void PROXY_dbghelp() {
		PA = p[225];
		RunASM();
	}
	void PROXY_dh() {
		PA = p[226];
		RunASM();
	}
	void PROXY_fptr() {
		PA = p[227];
		RunASM();
	}
	void PROXY_homedir() {
		PA = p[228];
		RunASM();
	}
	void PROXY_inlinedbg() {
		PA = p[229];
		RunASM();
	}
	void PROXY_itoldyouso() {
		PA = p[230];
		RunASM();
	}
	void PROXY_lmi() {
		PA = p[231];
		RunASM();
	}
	void PROXY_lminfo() {
		PA = p[232];
		RunASM();
	}
	void PROXY_omap() {
		PA = p[233];
		RunASM();
	}
	void PROXY_optdbgdump() {
		PA = p[234];
		RunASM();
	}
	void PROXY_optdbgdumpaddr() {
		PA = p[235];
		RunASM();
	}
	void PROXY_srcfiles() {
		PA = p[236];
		RunASM();
	}
	void PROXY_stack_force_ebp() {
		PA = p[237];
		RunASM();
	}
	void PROXY_stackdbg() {
		PA = p[238];
		RunASM();
	}
	void PROXY_sym() {
		PA = p[239];
		RunASM();
	}
	void PROXY_symsrv() {
		PA = p[240];
		RunASM();
	}
	void PROXY_vc7fpo() {
		PA = p[241];
		RunASM();
	}
}

namespace proxy {
	void InitProxy()
	{
		char dllPath[MAX_PATH];
		GetSystemDirectoryA(dllPath, MAX_PATH);
		strcat_s(dllPath, "\\dbghelp.dll");
		hL = LoadLibraryA(dllPath);

		TCHAR buf[200];
		memset(buf, 0, sizeof(buf));
		wsprintf(buf, TEXT("Cannot load original dbghelp.dll library\nError code: %d"), GetLastError());

		if (hL == NULL)
		{
			MessageBox(0, buf, TEXT("Proxy"), MB_ICONERROR);
			ExitProcess(0);
		}
		p[0] = GetProcAddress(hL, "DbgHelpCreateUserDump");
		p[1] = GetProcAddress(hL, "DbgHelpCreateUserDumpW");
		p[2] = GetProcAddress(hL, "EnumDirTree");
		p[3] = GetProcAddress(hL, "EnumDirTreeW");
		p[4] = GetProcAddress(hL, "EnumerateLoadedModules");
		p[5] = GetProcAddress(hL, "EnumerateLoadedModules64");
		p[6] = GetProcAddress(hL, "EnumerateLoadedModulesEx");
		p[7] = GetProcAddress(hL, "EnumerateLoadedModulesExW");
		p[8] = GetProcAddress(hL, "EnumerateLoadedModulesW64");
		p[9] = GetProcAddress(hL, "ExtensionApiVersion");
		p[10] = GetProcAddress(hL, "FindDebugInfoFile");
		p[11] = GetProcAddress(hL, "FindDebugInfoFileEx");
		p[12] = GetProcAddress(hL, "FindDebugInfoFileExW");
		p[13] = GetProcAddress(hL, "FindExecutableImage");
		p[14] = GetProcAddress(hL, "FindExecutableImageEx");
		p[15] = GetProcAddress(hL, "FindExecutableImageExW");
		p[16] = GetProcAddress(hL, "FindFileInPath");
		p[17] = GetProcAddress(hL, "FindFileInSearchPath");
		p[18] = GetProcAddress(hL, "GetSymLoadError");
		p[19] = GetProcAddress(hL, "GetTimestampForLoadedLibrary");
		p[20] = GetProcAddress(hL, "ImageDirectoryEntryToData");
		p[21] = GetProcAddress(hL, "ImageDirectoryEntryToDataEx");
		p[22] = GetProcAddress(hL, "ImageNtHeader");
		p[23] = GetProcAddress(hL, "ImageRvaToSection");
		p[24] = GetProcAddress(hL, "ImageRvaToVa");
		p[25] = GetProcAddress(hL, "ImagehlpApiVersion");
		p[26] = GetProcAddress(hL, "ImagehlpApiVersionEx");
		p[27] = GetProcAddress(hL, "MakeSureDirectoryPathExists");
		p[28] = GetProcAddress(hL, "MiniDumpReadDumpStream");
		p[29] = GetProcAddress(hL, "MiniDumpWriteDump");
		p[30] = GetProcAddress(hL, "RangeMapAddPeImageSections");
		p[31] = GetProcAddress(hL, "RangeMapCreate");
		p[32] = GetProcAddress(hL, "RangeMapFree");
		p[33] = GetProcAddress(hL, "RangeMapRead");
		p[34] = GetProcAddress(hL, "RangeMapRemove");
		p[35] = GetProcAddress(hL, "RangeMapWrite");
		p[36] = GetProcAddress(hL, "RemoveInvalidModuleList");
		p[37] = GetProcAddress(hL, "ReportSymbolLoadSummary");
		p[38] = GetProcAddress(hL, "SearchTreeForFile");
		p[39] = GetProcAddress(hL, "SearchTreeForFileW");
		p[40] = GetProcAddress(hL, "SetCheckUserInterruptShared");
		p[41] = GetProcAddress(hL, "SetSymLoadError");
		p[42] = GetProcAddress(hL, "StackWalk");
		p[43] = GetProcAddress(hL, "StackWalk64");
		p[44] = GetProcAddress(hL, "StackWalkEx");
		p[45] = GetProcAddress(hL, "SymAddSourceStream");
		p[46] = GetProcAddress(hL, "SymAddSourceStreamA");
		p[47] = GetProcAddress(hL, "SymAddSourceStreamW");
		p[48] = GetProcAddress(hL, "SymAddSymbol");
		p[49] = GetProcAddress(hL, "SymAddSymbolW");
		p[50] = GetProcAddress(hL, "SymAddrIncludeInlineTrace");
		p[51] = GetProcAddress(hL, "SymAllocDiaString");
		p[52] = GetProcAddress(hL, "SymCleanup");
		p[53] = GetProcAddress(hL, "SymCompareInlineTrace");
		p[54] = GetProcAddress(hL, "SymDeleteSymbol");
		p[55] = GetProcAddress(hL, "SymDeleteSymbolW");
		p[56] = GetProcAddress(hL, "SymEnumLines");
		p[57] = GetProcAddress(hL, "SymEnumLinesW");
		p[58] = GetProcAddress(hL, "SymEnumProcesses");
		p[59] = GetProcAddress(hL, "SymEnumSourceFileTokens");
		p[60] = GetProcAddress(hL, "SymEnumSourceFiles");
		p[61] = GetProcAddress(hL, "SymEnumSourceFilesW");
		p[62] = GetProcAddress(hL, "SymEnumSourceLines");
		p[63] = GetProcAddress(hL, "SymEnumSourceLinesW");
		p[64] = GetProcAddress(hL, "SymEnumSym");
		p[65] = GetProcAddress(hL, "SymEnumSymbols");
		p[66] = GetProcAddress(hL, "SymEnumSymbolsEx");
		p[67] = GetProcAddress(hL, "SymEnumSymbolsExW");
		p[68] = GetProcAddress(hL, "SymEnumSymbolsForAddr");
		p[69] = GetProcAddress(hL, "SymEnumSymbolsForAddrW");
		p[70] = GetProcAddress(hL, "SymEnumSymbolsW");
		p[71] = GetProcAddress(hL, "SymEnumTypes");
		p[72] = GetProcAddress(hL, "SymEnumTypesByName");
		p[73] = GetProcAddress(hL, "SymEnumTypesByNameW");
		p[74] = GetProcAddress(hL, "SymEnumTypesW");
		p[75] = GetProcAddress(hL, "SymEnumerateModules");
		p[76] = GetProcAddress(hL, "SymEnumerateModules64");
		p[77] = GetProcAddress(hL, "SymEnumerateModulesW64");
		p[78] = GetProcAddress(hL, "SymEnumerateSymbols");
		p[79] = GetProcAddress(hL, "SymEnumerateSymbols64");
		p[80] = GetProcAddress(hL, "SymEnumerateSymbolsW");
		p[81] = GetProcAddress(hL, "SymEnumerateSymbolsW64");
		p[82] = GetProcAddress(hL, "SymFindDebugInfoFile");
		p[83] = GetProcAddress(hL, "SymFindDebugInfoFileW");
		p[84] = GetProcAddress(hL, "SymFindExecutableImage");
		p[85] = GetProcAddress(hL, "SymFindExecutableImageW");
		p[86] = GetProcAddress(hL, "SymFindFileInPath");
		p[87] = GetProcAddress(hL, "SymFindFileInPathW");
		p[88] = GetProcAddress(hL, "SymFreeDiaString");
		p[89] = GetProcAddress(hL, "SymFromAddr");
		p[90] = GetProcAddress(hL, "SymFromAddrW");
		p[91] = GetProcAddress(hL, "SymFromIndex");
		p[92] = GetProcAddress(hL, "SymFromIndexW");
		p[93] = GetProcAddress(hL, "SymFromInlineContext");
		p[94] = GetProcAddress(hL, "SymFromInlineContextW");
		p[95] = GetProcAddress(hL, "SymFromName");
		p[96] = GetProcAddress(hL, "SymFromNameW");
		p[97] = GetProcAddress(hL, "SymFromToken");
		p[98] = GetProcAddress(hL, "SymFromTokenW");
		p[99] = GetProcAddress(hL, "SymFunctionTableAccess");
		p[100] = GetProcAddress(hL, "SymFunctionTableAccess64");
		p[101] = GetProcAddress(hL, "SymFunctionTableAccess64AccessRoutines");
		p[102] = GetProcAddress(hL, "SymGetDiaSession");
		p[103] = GetProcAddress(hL, "SymGetExtendedOption");
		p[104] = GetProcAddress(hL, "SymGetFileLineOffsets64");
		p[105] = GetProcAddress(hL, "SymGetHomeDirectory");
		p[106] = GetProcAddress(hL, "SymGetHomeDirectoryW");
		p[107] = GetProcAddress(hL, "SymGetLineFromAddr");
		p[108] = GetProcAddress(hL, "SymGetLineFromAddr64");
		p[109] = GetProcAddress(hL, "SymGetLineFromAddrEx");
		p[110] = GetProcAddress(hL, "SymGetLineFromAddrW64");
		p[111] = GetProcAddress(hL, "SymGetLineFromInlineContext");
		p[112] = GetProcAddress(hL, "SymGetLineFromInlineContextW");
		p[113] = GetProcAddress(hL, "SymGetLineFromName");
		p[114] = GetProcAddress(hL, "SymGetLineFromName64");
		p[115] = GetProcAddress(hL, "SymGetLineFromNameEx");
		p[116] = GetProcAddress(hL, "SymGetLineFromNameW64");
		p[117] = GetProcAddress(hL, "SymGetLineNext");
		p[118] = GetProcAddress(hL, "SymGetLineNext64");
		p[119] = GetProcAddress(hL, "SymGetLineNextEx");
		p[120] = GetProcAddress(hL, "SymGetLineNextW64");
		p[121] = GetProcAddress(hL, "SymGetLinePrev");
		p[122] = GetProcAddress(hL, "SymGetLinePrev64");
		p[123] = GetProcAddress(hL, "SymGetLinePrevEx");
		p[124] = GetProcAddress(hL, "SymGetLinePrevW64");
		p[125] = GetProcAddress(hL, "SymGetModuleBase");
		p[126] = GetProcAddress(hL, "SymGetModuleBase64");
		p[127] = GetProcAddress(hL, "SymGetModuleInfo");
		p[128] = GetProcAddress(hL, "SymGetModuleInfo64");
		p[129] = GetProcAddress(hL, "SymGetModuleInfoW");
		p[130] = GetProcAddress(hL, "SymGetModuleInfoW64");
		p[131] = GetProcAddress(hL, "SymGetOmapBlockBase");
		p[132] = GetProcAddress(hL, "SymGetOmaps");
		p[133] = GetProcAddress(hL, "SymGetOptions");
		p[134] = GetProcAddress(hL, "SymGetScope");
		p[135] = GetProcAddress(hL, "SymGetScopeW");
		p[136] = GetProcAddress(hL, "SymGetSearchPath");
		p[137] = GetProcAddress(hL, "SymGetSearchPathW");
		p[138] = GetProcAddress(hL, "SymGetSourceFile");
		p[139] = GetProcAddress(hL, "SymGetSourceFileChecksum");
		p[140] = GetProcAddress(hL, "SymGetSourceFileChecksumW");
		p[141] = GetProcAddress(hL, "SymGetSourceFileFromToken");
		p[142] = GetProcAddress(hL, "SymGetSourceFileFromTokenW");
		p[143] = GetProcAddress(hL, "SymGetSourceFileToken");
		p[144] = GetProcAddress(hL, "SymGetSourceFileTokenW");
		p[145] = GetProcAddress(hL, "SymGetSourceFileW");
		p[146] = GetProcAddress(hL, "SymGetSourceVarFromToken");
		p[147] = GetProcAddress(hL, "SymGetSourceVarFromTokenW");
		p[148] = GetProcAddress(hL, "SymGetSymFromAddr");
		p[149] = GetProcAddress(hL, "SymGetSymFromAddr64");
		p[150] = GetProcAddress(hL, "SymGetSymFromName");
		p[151] = GetProcAddress(hL, "SymGetSymFromName64");
		p[152] = GetProcAddress(hL, "SymGetSymNext");
		p[153] = GetProcAddress(hL, "SymGetSymNext64");
		p[154] = GetProcAddress(hL, "SymGetSymPrev");
		p[155] = GetProcAddress(hL, "SymGetSymPrev64");
		p[156] = GetProcAddress(hL, "SymGetSymbolFile");
		p[157] = GetProcAddress(hL, "SymGetSymbolFileW");
		p[158] = GetProcAddress(hL, "SymGetTypeFromName");
		p[159] = GetProcAddress(hL, "SymGetTypeFromNameW");
		p[160] = GetProcAddress(hL, "SymGetTypeInfo");
		p[161] = GetProcAddress(hL, "SymGetTypeInfoEx");
		p[162] = GetProcAddress(hL, "SymGetUnwindInfo");
		p[163] = GetProcAddress(hL, "SymInitialize");
		p[164] = GetProcAddress(hL, "SymInitializeW");
		p[165] = GetProcAddress(hL, "SymLoadModule");
		p[166] = GetProcAddress(hL, "SymLoadModule64");
		p[167] = GetProcAddress(hL, "SymLoadModuleEx");
		p[168] = GetProcAddress(hL, "SymLoadModuleExW");
		p[169] = GetProcAddress(hL, "SymMatchFileName");
		p[170] = GetProcAddress(hL, "SymMatchFileNameW");
		p[171] = GetProcAddress(hL, "SymMatchString");
		p[172] = GetProcAddress(hL, "SymMatchStringA");
		p[173] = GetProcAddress(hL, "SymMatchStringW");
		p[174] = GetProcAddress(hL, "SymNext");
		p[175] = GetProcAddress(hL, "SymNextW");
		p[176] = GetProcAddress(hL, "SymPrev");
		p[177] = GetProcAddress(hL, "SymPrevW");
		p[178] = GetProcAddress(hL, "SymQueryInlineTrace");
		p[179] = GetProcAddress(hL, "SymRefreshModuleList");
		p[180] = GetProcAddress(hL, "SymRegisterCallback");
		p[181] = GetProcAddress(hL, "SymRegisterCallback64");
		p[182] = GetProcAddress(hL, "SymRegisterCallbackW64");
		p[183] = GetProcAddress(hL, "SymRegisterFunctionEntryCallback");
		p[184] = GetProcAddress(hL, "SymRegisterFunctionEntryCallback64");
		p[185] = GetProcAddress(hL, "SymSearch");
		p[186] = GetProcAddress(hL, "SymSearchW");
		p[187] = GetProcAddress(hL, "SymSetContext");
		p[188] = GetProcAddress(hL, "SymSetDiaSession");
		p[189] = GetProcAddress(hL, "SymSetExtendedOption");
		p[190] = GetProcAddress(hL, "SymSetHomeDirectory");
		p[191] = GetProcAddress(hL, "SymSetHomeDirectoryW");
		p[192] = GetProcAddress(hL, "SymSetOptions");
		p[193] = GetProcAddress(hL, "SymSetParentWindow");
		p[194] = GetProcAddress(hL, "SymSetScopeFromAddr");
		p[195] = GetProcAddress(hL, "SymSetScopeFromIndex");
		p[196] = GetProcAddress(hL, "SymSetScopeFromInlineContext");
		p[197] = GetProcAddress(hL, "SymSetSearchPath");
		p[198] = GetProcAddress(hL, "SymSetSearchPathW");
		p[199] = GetProcAddress(hL, "SymSrvDeltaName");
		p[200] = GetProcAddress(hL, "SymSrvDeltaNameW");
		p[201] = GetProcAddress(hL, "SymSrvGetFileIndexInfo");
		p[202] = GetProcAddress(hL, "SymSrvGetFileIndexInfoW");
		p[203] = GetProcAddress(hL, "SymSrvGetFileIndexString");
		p[204] = GetProcAddress(hL, "SymSrvGetFileIndexStringW");
		p[205] = GetProcAddress(hL, "SymSrvGetFileIndexes");
		p[206] = GetProcAddress(hL, "SymSrvGetFileIndexesW");
		p[207] = GetProcAddress(hL, "SymSrvGetSupplement");
		p[208] = GetProcAddress(hL, "SymSrvGetSupplementW");
		p[209] = GetProcAddress(hL, "SymSrvIsStore");
		p[210] = GetProcAddress(hL, "SymSrvIsStoreW");
		p[211] = GetProcAddress(hL, "SymSrvStoreFile");
		p[212] = GetProcAddress(hL, "SymSrvStoreFileW");
		p[213] = GetProcAddress(hL, "SymSrvStoreSupplement");
		p[214] = GetProcAddress(hL, "SymSrvStoreSupplementW");
		p[215] = GetProcAddress(hL, "SymUnDName");
		p[216] = GetProcAddress(hL, "SymUnDName64");
		p[217] = GetProcAddress(hL, "SymUnloadModule");
		p[218] = GetProcAddress(hL, "SymUnloadModule64");
		p[219] = GetProcAddress(hL, "UnDecorateSymbolName");
		p[220] = GetProcAddress(hL, "UnDecorateSymbolNameW");
		p[221] = GetProcAddress(hL, "WinDbgExtensionDllInit");
		p[222] = GetProcAddress(hL, "_EFN_DumpImage");
		p[223] = GetProcAddress(hL, "block");
		p[224] = GetProcAddress(hL, "chksym");
		p[225] = GetProcAddress(hL, "dbghelp");
		p[226] = GetProcAddress(hL, "dh");
		p[227] = GetProcAddress(hL, "fptr");
		p[228] = GetProcAddress(hL, "homedir");
		p[229] = GetProcAddress(hL, "inlinedbg");
		p[230] = GetProcAddress(hL, "itoldyouso");
		p[231] = GetProcAddress(hL, "lmi");
		p[232] = GetProcAddress(hL, "lminfo");
		p[233] = GetProcAddress(hL, "omap");
		p[234] = GetProcAddress(hL, "optdbgdump");
		p[235] = GetProcAddress(hL, "optdbgdumpaddr");
		p[236] = GetProcAddress(hL, "srcfiles");
		p[237] = GetProcAddress(hL, "stack_force_ebp");
		p[238] = GetProcAddress(hL, "stackdbg");
		p[239] = GetProcAddress(hL, "sym");
		p[240] = GetProcAddress(hL, "symsrv");
		p[241] = GetProcAddress(hL, "vc7fpo");
	}
}
