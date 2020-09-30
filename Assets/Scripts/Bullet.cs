using UnityEngine;

public class Bullet : MonoBehaviour
{
        public void Initialize(Vector2 direction)
        {
                var movement = GetComponent<MovementComponent>();
                movement.Initialize();
                movement.AddForce(direction);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
                var health = other.GetComponent<HealthComponent>();
                if(health != null)
                        health.ChangeHealth(-1);
        }
}