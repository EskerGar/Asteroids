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
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        var x = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        Rotate(x);
        _movement.ChooseDirection(Input.GetAxis("Vertical") * transform.up);
        if(Input.GetButtonDown("Fire1"))
            _attack.CreateBullet(transform.up);
            
    }

    private void Rotate(float x)
    {
        transform.Rotate(Vector3.forward * -x); 
    }
}
