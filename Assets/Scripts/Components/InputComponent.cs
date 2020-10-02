using System.Collections;
using Player;
using UnityEngine;

namespace Components
{
    [RequireComponent(typeof(AttackComponent), typeof(MovementComponent), typeof(HyperCharge))]
    public class InputComponent : MonoBehaviour
    {
        [SerializeField] private float mouseSensitivity = 100;
        [SerializeField] private float attackDelay = .5f;
        [SerializeField] private JoyButton fireButton;
        [SerializeField] private JoyButton jumpButton;
        [SerializeField] private Joystick rotationJoystick;
        

        private Vector2 _direction;
        private AttackComponent _attack;
        private MovementComponent _movement;
        private HyperCharge _hyperCharge;
        private float _xRotation;
        private Coroutine _attackCoroutine;

        private void Start()
        {
            _movement = GetComponent<MovementComponent>();
            _attack = GetComponent<AttackComponent>();
            _hyperCharge = GetComponent<HyperCharge>();
            jumpButton.SubscribeButtonClick(_hyperCharge.UseHyperCharge);
        }

        private void OnDestroy()
        {
            jumpButton.UnSubscribeAllButtonClick();
        }

        private void Update()
        {
            if (rotationJoystick.Pressed)
            {
                Rotate();
                _movement.ChooseDirection(transform.up);
            }
            
            if(fireButton.Pressed)
                InputFire();
        }

        private void InputFire()
        {
            if(_attackCoroutine == null)
                _attackCoroutine =  StartCoroutine(StartFire());
        }

        private IEnumerator StartFire()
        {
            while (fireButton.Pressed)
            {
                _attack.CreateBullet(transform.up);
                yield return new WaitForSeconds(attackDelay);
            }

            _attackCoroutine = null;
        }

        private void Rotate()
        {
            var horizontal = rotationJoystick.Horizontal * Time.deltaTime * mouseSensitivity;
            var vertical = rotationJoystick.Vertical * Time.deltaTime * mouseSensitivity;
            var angle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg; 
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle));
        }
    }
}
