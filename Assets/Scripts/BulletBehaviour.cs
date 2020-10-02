using Components;
using Player;
using Ufo;
using UnityEngine;

[RequireComponent(typeof(MovementComponent))]
public class BulletBehaviour : MonoBehaviour
{
        [SerializeField] private float timeToDestroy;
        
        private Collider2D _ownerCollider;
        private float _startBulletTime;

        public void Initialize(Vector2 direction, GameObject owner)
        {
                var movement = GetComponent<MovementComponent>();
                _ownerCollider = owner.GetComponent<Collider2D>();
                movement.Initialize();
                _startBulletTime = Time.time;
                movement.ChooseDirection(direction);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
                var health = other.GetComponent<HealthComponent>();
                if (_ownerCollider == null || _ownerCollider.GetComponent<UfoBehaviour>() != null &&
                    other.GetComponent<ShipBehaviour>() == null) return;
                
                if (health == null || _ownerCollider == other) return;
                
                health.ChangeHealth(-1);
                Destroy(gameObject);
        }
        
        
        private void Update()
        {
                if((Time.time - _startBulletTime) >= timeToDestroy)
                        Destroy(gameObject);
        }
}