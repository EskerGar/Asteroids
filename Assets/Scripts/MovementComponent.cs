using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementComponent : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;

    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }


    public void AddForce(Vector2 direction) => _rigidbody2D.AddForce(direction * speed);
}
