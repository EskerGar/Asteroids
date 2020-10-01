using UnityEngine;

public class AttackComponent : MonoBehaviour
{ 
        [SerializeField] private BulletBehaviour bullet;
        
        public void CreateBullet(Vector3 direction)
        {
                var go = Instantiate(bullet, transform.position + direction.normalized, Quaternion.identity);
                go.GetComponent<BulletBehaviour>().Initialize(direction, gameObject);
        }
}