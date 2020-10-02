using UnityEngine;

public static class CameraProperties
{
    public static readonly float CameraHeight;
    public static readonly float CameraWidth;

    
    static CameraProperties()
    {
        var mainCamera = Camera.main;
        CameraHeight = mainCamera.orthographicSize;
        CameraWidth = mainCamera.orthographicSize * mainCamera.aspect;
    }


        
}