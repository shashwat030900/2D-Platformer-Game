using UnityEngine;
using System.Runtime.InteropServices;

public class ReloadApplication : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void WebGLReload();

    public void ReloadGame()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
        WebGLReload();
        #else
        Debug.Log("This function only works in WebGL builds.");
        #endif
    }
}
