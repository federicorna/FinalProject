using UnityEngine;

namespace MyGame.Weapons
{
    public class Bullet : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float _speed = 20f;
        [SerializeField] private float _lifeTime = 3f;

        [Header("Combat Settings")]
        private int _damage = 1;

        void Start()
        {
            Destroy(gameObject, _lifeTime);
        }

        void Update()
        {
            // Vector3.up 
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }

        // Metodo pubblico per permettere al GunManager di impostare il danno
        public void SetDamage(int amount)
        {
            _damage = amount;
        }

        private void OnTriggerEnter(Collider other)
        {
            // Se colpisce un nemico (aggiungeremo il tag "Enemy" più avanti)
            if (other.CompareTag("Enemy"))
            {
                // Qui chiameremo la funzione per togliere vita al nemico
                // other.GetComponent<EnemyHealth>().TakeDamage(_damage);

                Destroy(gameObject); // Distruggi il proiettile all'impatto
            }

            // Se colpisce il muro o il pavimento (Tag "Environment")
            if (other.CompareTag("Ground"))
            {
                Destroy(gameObject);
            }
        }
    }
}