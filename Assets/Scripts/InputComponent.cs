using System;
using UnityEngine;

[RequireComponent(typeof(AttackComponent), typeof(MovementComponent), typeof(HyperCharge))]
public class InputComponent : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100;
    [SerializeField] private JoyButton moveButton;
    [SerializeField] private JoyButton fireButton;
    [SerializeField] private JoyButton jumpButton;
    [SerializeField] private Joystick rotationJoystick;

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
        fireButton.SubscribeButtonClick(FireInput);
        jumpButton.SubscribeButtonClick(_hyperCharge.UseHyperCharge);
    }

    private void OnDestroy()
    {
        fireButton.UnSubscribeAllButtonClick();
        jumpButton.UnSubscribeAllButtonClick();
    }

    private void Update()
    {
        var x = rotationJoystick.Vertical * mouseSensitivity * Time.deltaTime;
        if(rotationJoystick.Pressed)
            Rotate(x);
        if(moveButton.Pressed)
            _movement.ChooseDirection(transform.up);

    }

    private void FireInput() => _attack.CreateBullet(transform.up);

    private void Rotate(float x)
    {
        var horizontal = rotationJoystick.Horizontal * Time.deltaTime * mouseSensitivity;
        var vertical = rotationJoystick.Vertical * Time.deltaTime * mouseSensitivity;
        var angle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg; 
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle));
    }
}
