using UnityEngine;

namespace MyGame.Weapons
{
    public class Bullet : MonoBehaviour
    {
        [Header("Movimento")]
        [SerializeField] private float _speed = 20f;
        [SerializeField] private float _lifeTime = 3f;

        [Header("Combattimento")]
        private int _damage = 1;

        void Start()
        {
            Destroy(gameObject, _lifeTime);
        }

        void Update()
        {
            // Space.Self: si muove nella direzione in cui il proiettile è ruotato
            transform.Translate(Vector3.up * _speed * Time.deltaTime, Space.Self);
        }

        public void SetDamage(int amount)
        {
            _damage = amount;
        }

        private void OnTriggerEnter(Collider other)
        {
            // Cerca HealthComponent sull'oggetto colpito (o nel parent)
            HealthComponent target = other.GetComponentInParent<HealthComponent>();

            if (target != null && !target.IsDead)
            {
                target.TakeDamage(_damage);
                Destroy(gameObject);
                return;
            }

            // Distruggi il proiettile se colpisce l'ambiente
            if (other.CompareTag("Ground"))
            {
                Destroy(gameObject);
            }
        }
    }
}