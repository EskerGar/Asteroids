using Components;
using UnityEngine;

namespace Ufo
{
        public class SmartUfoBehaviour : UfoBehaviour
        {
                private Transform _playerShip;
        
                protected override void Start()
                {
                        _playerShip = FindObjectOfType<InputComponent>().transform;
                        base.Start();
                }

                protected override Vector3 GetTarget()
                {
                        return _playerShip.position - transform.position;
                }
        }
}