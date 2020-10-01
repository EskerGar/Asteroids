using UnityEngine;
using static CameraProperties;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementComponent : MonoBehaviour
{ 
        [SerializeField] private float speed = 50f;
    
        public Vector2 Direction { get; private set; }
    
        private Rigidbody2D _rigidbody2D;

    
        private void Awake()
        {
            Initialize();
        }
    
        public void Initialize()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
    
    
        public void ChooseDirection(Vector2 direction)
        {
            Direction = direction;
            _rigidbody2D.AddForce(direction.normalized * speed);
        }
        
        private void OnBecameInvisible()
        {
            ReturnFromOutside();
        }

        private void ReturnFromOutside()
        {
            transform.position =  TryGetNewPosition(transform.position);
        }
}
