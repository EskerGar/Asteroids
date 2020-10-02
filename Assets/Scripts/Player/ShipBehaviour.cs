using Components;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(HealthComponent))]
    public class ShipBehaviour : MonoBehaviour
    {
        private HealthComponent _health;

        private void Start()
        {
            _health = GetComponent<HealthComponent>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var health = other.GetComponent<HealthComponent>();
            if(other.GetComponent<BulletBehaviour>() != null) return;
            if(health != null)
                other.GetComponent<HealthComponent>().ChangeHealth(-1);
            _health.ChangeHealth(-1);
        }
    }
}