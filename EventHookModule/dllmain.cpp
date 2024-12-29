// dllmain.cpp : DLL アプリケーションのエントリ ポイントを定義します。
#include "pch.h"

#pragma data_seg(".HShared") // メモリ共有
HWND g_hWnd = NULL;
HHOOK g_hHook = NULL;
#pragma data_seg()
#pragma comment(linker, "/Section:.HShared,rws")

HINSTANCE g_hInst; // インスタンスハンドル

#define WM_HOOK_ACTIVATE (WM_APP + 1) // フックメッセージ アクティブ状態変化があったかどうか (wparam: マウスフラグ, lparam: アクティブウィンドウハンドル)

/**
 * DLLエントリポイント
 * @param hModule モジュールハンドル
 * @param ul_reason_for_call コール理由
 * @param lpReserved 予約済み
 * @return 成否
 */
BOOL APIENTRY DllMain( HMODULE hModule, DWORD ul_reason_for_call, LPVOID lpReserved)
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
        g_hInst = (HINSTANCE)hModule;
		break;
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

/**
* フックプロシージャ
* @param nCode フックコード
* @param wParam メッセージの種類
* @param lParam メッセージの内容
* @return メッセージ処理の成否
*/
static LRESULT CALLBACK HookProc(int nCode, WPARAM wParam, LPARAM lParam)
{
    if (nCode == HCBT_ACTIVATE) {
        CBTACTIVATESTRUCT* pParam = (CBTACTIVATESTRUCT*)lParam;
        SendMessage(g_hWnd, WM_HOOK_ACTIVATE, pParam->fMouse, (LPARAM)pParam->hWndActive);
    }
    return (CallNextHookEx(g_hHook, nCode, wParam, lParam));
}

/*
* フックを開始する
* @param hWnd メッセージを送るウィンドウのハンドル
* @return フック開始の成否
*/
DllExport BOOL StartHook(HWND hWnd)
{
    g_hWnd = hWnd;
    g_hHook = SetWindowsHookEx(WH_CBT, (HOOKPROC)HookProc, g_hInst, 0);// 0=グローバルフック
    return (g_hHook != NULL);
}

/*
* フックを解除する
* @return フック解除の成否
*/
DllExport BOOL EndHook()
{
    return UnhookWindowsHookEx(g_hHook);
}