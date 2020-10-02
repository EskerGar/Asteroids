using System;
using UnityEngine;
using static CameraProperties;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementComponent : MonoBehaviour
{ 
        [SerializeField] private float speed = 50f;
    
        public Vector2 Direction { get; private set; }
    
        private Rigidbody2D _rigidbody2D;

        public void IncreaseSpeed(float value) => speed += value;
        
        public void Initialize()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void ChooseDirection(Vector2 direction)
        {
            Direction = direction;
            _rigidbody2D.AddForce(direction.normalized * speed);
        }
        
        private void Awake()
        {
            Initialize();
        }
        
        private void OnBecameInvisible()
        {
            ReturnFromOutside();
        }

        private void ReturnFromOutside()
        {
            transform.position =  TryGetNewPosition(transform.position);
        }

        private Vector3 TryGetNewPosition(Vector3 position)
        {
            return new Vector2( 
                TryGetNewCoord(position.x, CameraWidth),
                TryGetNewCoord(position.y, CameraHeight)
            );
        }
    
        private float TryGetNewCoord(float positionCoord, float cameraCoord)
        {
            var newCoord = positionCoord;
            if (CheckCoord(positionCoord, cameraCoord, (posCoord, camCoord) => posCoord >= camCoord))
                newCoord -= 2 * cameraCoord + .5f;
            else if (CheckCoord(positionCoord, cameraCoord, (posCoord, camCoord) => posCoord <= -camCoord))
                newCoord += 2 * cameraCoord + .5f;
            return newCoord;
        }

        private bool CheckCoord(float positionCoord, float cameraCoord, Func<float, float, bool> check)
        {
            return check(positionCoord, cameraCoord);
        }
}
