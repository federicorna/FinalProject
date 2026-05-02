
using UnityEngine;

namespace MyGame.Weapons
{
    public class GunManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _firePoint;

        [Header("Settings")]
        [SerializeField] private float _fireRate = 1f;
        private float _nextFireTime;

        private Vector3 _shootDirection = Vector3.right;
        private int _bulletDamage = 1;

        void Update()
        {
            HandleRotation();

            if (Input.GetButton("Fire1") && Time.time >= _nextFireTime)
            {
                Shoot();
                _nextFireTime = Time.time + _fireRate;
            }
        }


        //--[f.ni]--

        private void HandleRotation()
        {
            /// Raggio da centro camera a posizione mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            /// Piano XY rivolto come monitor e passa per origine
            Plane gamePlane = new Plane(Vector3.forward, Vector3.zero);

            /// Se raggio interseca il piano dammi la distanza
            if (gamePlane.Raycast(ray, out float distance))
            {
                /// Posizione mirino nel mondo
                Vector3 mouseWorldPos = ray.GetPoint(distance);
                /// Punto di mira - posizione arma = vettore arma->mouse
                Vector3 direction = mouseWorldPos - transform.position;
                /// Blocca asse Z e rimani in XY
                direction.z = 0;

                if (direction != Vector3.zero)
                {
                    _shootDirection = direction.normalized;
                }
            }
        }

        private void Shoot()
        {
            GameObject bulletObj = Instantiate(_bulletPrefab, _firePoint.position, Quaternion.identity);
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            bullet.SetDirection(_shootDirection);
            bullet.SetDamage(_bulletDamage);
        }

        public void SetFireRate(float value)
        {
            _fireRate = value;
        }

        public void AddBulletDamage(int amount)
        {
            _bulletDamage += amount;
        }
    }
}