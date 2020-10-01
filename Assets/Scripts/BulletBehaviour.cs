using System;
using UnityEngine;

[RequireComponent(typeof(MovementComponent))]
public class BulletBehaviour : MonoBehaviour
{
        [SerializeField] private float distanceToDestroy;
        
        private Collider2D _ownerCollider;
        private Vector3 _startPos;

        public void Initialize(Vector2 direction, GameObject owner)
        {
                var movement = GetComponent<MovementComponent>();
                _ownerCollider = owner.GetComponent<Collider2D>();
                movement.Initialize();
                _startPos = transform.position;
                movement.ChooseDirection(direction);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
                var health = other.GetComponent<HealthComponent>();
                if (health == null || _ownerCollider == other) return;
                health.ChangeHealth(-1);
                Destroy(gameObject);
        }
        
        
        private void Update()
        {
                if((transform.position - _startPos).magnitude >= distanceToDestroy)
                        Destroy(gameObject);
        }
}