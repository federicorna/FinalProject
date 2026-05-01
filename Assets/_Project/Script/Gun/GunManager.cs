
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

        void Update()
        {
            HandleRotation();

            if (Input.GetButton("Fire1") && Time.time >= _nextFireTime)
            {
                Shoot();
                _nextFireTime = Time.time + _fireRate;
            }
        }

        private void HandleRotation()
        {   
            // Raggio da centro camera a posizione mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Piano XY rivolto come monitor e passa per origine
            Plane gamePlane = new Plane(Vector3.forward, Vector3.zero);

            // Se raggio interseca il piano dammi la distanza
            if (gamePlane.Raycast(ray, out float distance))
            {
                // Posizione mirino nel mondo
                Vector3 targetPoint = ray.GetPoint(distance);
                // Punto di mira - posizione arma = vettore arma->mouse
                Vector3 direction = targetPoint - transform.position;

                // Blocca asse Z e rimani in XY
                direction.z = 0;

                if (direction != Vector3.zero)
                {
                    // Z rivolta verso il fondo, vettore arma->mouse come "su" (up)
                    transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
                }
            }
        }


        private void Shoot()
        {
            // Istanziamo il proiettile nella posizione e rotazione del FirePoint
            GameObject bulletObj = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);

            // In futuro qui passeremo il danno al componente Bullet
            // Bullet bullet = bulletObj.GetComponent<Bullet>();
            // bullet.SetDamage(1);
        }
    }
}