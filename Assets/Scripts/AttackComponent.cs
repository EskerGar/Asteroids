using UnityEngine;

public class AttackComponent : MonoBehaviour
{ 
        [SerializeField] private Bullet bullet;


        public void CreateBullet()
        {
                var go = Instantiate(bullet, transform.up , Quaternion.identity);
                go.GetComponent<Bullet>().Initialize(transform.up);
        }
}