#pragma once

namespace proxy {
	static HINSTANCE hLThis = NULL;
	static FARPROC p[242];
	static HINSTANCE hL = NULL;

	void InitProxy();
}
