using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class CameraSetup
{
    static CameraSetup()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode || state == PlayModeStateChange.EnteredPlayMode)
        {
            // 确保主相机有ThirdPersonCamera脚本
            Camera mainCamera = Camera.main;
            if (mainCamera != null && mainCamera.GetComponent<ThirdPersonCamera>() == null)
            {
                mainCamera.gameObject.AddComponent<ThirdPersonCamera>();
                Debug.Log("已自动为主相机添加ThirdPersonCamera脚本");
            }
        }
    }
}