using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MovementComponent), typeof(AttackComponent))]
public class UfoBehaviour : MonoBehaviour
{
    [SerializeField] private float attackDelay;
    
    private MovementComponent _movement;
    private AttackComponent _attack;
    
    protected virtual void Start()
    {
        _movement = GetComponent<MovementComponent>();
        _attack = GetComponent<AttackComponent>();
        _movement.ChooseDirection(Camera.main.transform.position - transform.position);
        StartCoroutine(StartAttack());
    }
    
    protected virtual Vector3 GetTarget() => Random.insideUnitCircle;

    private IEnumerator StartAttack()
    {
        while (true)
        {
            _attack.CreateBullet(GetTarget());
            yield return new WaitForSeconds(attackDelay);
        }
    }
    
}