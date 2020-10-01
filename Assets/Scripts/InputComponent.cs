using UnityEngine;

[RequireComponent(typeof(AttackComponent), typeof(MovementComponent), typeof(HyperCharge))]
public class InputComponent : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100;

    private Vector2 _direction;
    private AttackComponent _attack;
    private MovementComponent _movement;
    private HyperCharge _hyperCharge;
    private float _xRotation;

    private void Start()
    {
        _movement = GetComponent<MovementComponent>();
        _attack = GetComponent<AttackComponent>();
        _hyperCharge = GetComponent<HyperCharge>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        var x = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        Rotate(x);
        var verticalAxe = Mathf.Clamp(Input.GetAxis("Vertical"), 0, 1);
        _movement.ChooseDirection(verticalAxe * transform.up);
        if(Input.GetButtonDown("Fire1"))
            _attack.CreateBullet(transform.up);
        if(Input.GetButtonDown("Fire2"))
            _hyperCharge.UseHyperCharge();
            
    }

    private void Rotate(float x)
    {
        transform.Rotate(Vector3.forward * -x); 
    }
}
