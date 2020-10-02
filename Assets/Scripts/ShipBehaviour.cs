using UnityEngine;
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
        if(other.Equals(gameObject.GetComponent<Collider2D>())) return;
        if(health != null)
            other.GetComponent<HealthComponent>().ChangeHealth(-1);
        _health.ChangeHealth(-1);
    }
}