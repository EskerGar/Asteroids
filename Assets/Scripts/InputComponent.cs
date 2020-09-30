using UnityEngine;

public class InputComponent : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100;

    private Vector2 _direction;
    private AttackComponent _attack;
    private MovementComponent _movement;
    private float _xRotation;

    private void Start()
    {
        _movement = GetComponent<MovementComponent>();
        _attack = GetComponent<AttackComponent>();
    }

    private void Update()
    {
        var x = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        Rotate(x);
        _movement.AddForce(Input.GetAxis("Vertical") * transform.up);
        if(Input.GetButtonDown("Fire1"))
            _attack.CreateBullet();
            
    }

    private void Rotate(float x)
    {
        transform.Rotate(Vector3.forward * -x); 
    }
}
