using UnityEngine;
using static CameraProperties;
namespace Player
{
     public class HyperCharge : MonoBehaviour
     {
          public void UseHyperCharge()
          {
               transform.position = GetRandomPosition();
          }
     
          private Vector3 GetRandomPosition()
          {
               var randomXPoint = Random.Range(-CameraWidth, CameraWidth);
               var randomYPoint = Random.Range(CameraHeight, -CameraHeight);
               return new Vector2(randomXPoint, randomYPoint);
          }
     }
}