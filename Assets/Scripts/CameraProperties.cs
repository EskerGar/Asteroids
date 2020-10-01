using System;
using UnityEngine;

public static class CameraProperties
{
    
    private static readonly Camera MainCamera;
    private static float Height => MainCamera.orthographicSize;
    private static float Width => MainCamera.orthographicSize * MainCamera.aspect;

    
    static CameraProperties()
    {
        MainCamera = Camera.main;
    }

    public static Vector3 TryGetNewPosition(Vector3 position)
    {
        return new Vector2( 
            TryGetNewCoord(position.x, Width),
            TryGetNewCoord(position.y, Height)
            );
    }
    
    private static float TryGetNewCoord(float positionCoord, float cameraCoord)
    {
        var newCoord = positionCoord;
        if (CheckCoord(positionCoord, cameraCoord, (posCoord, camCoord) => posCoord >= camCoord))
            newCoord -= 2 * cameraCoord + .5f;
        else if (CheckCoord(positionCoord, cameraCoord, (posCoord, camCoord) => posCoord <= -camCoord))
            newCoord += 2 * cameraCoord + .5f;
        return newCoord;
    }

    private static bool CheckCoord(float positionCoord, float cameraCoord, Func<float, float, bool> check)
    {
        return check(positionCoord, cameraCoord);
    }
        
}