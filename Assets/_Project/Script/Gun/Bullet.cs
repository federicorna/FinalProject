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

        private Vector3 _direction = Vector3.right;

        void Start()
        {
            Destroy(gameObject, _lifeTime);
        }

        public void SetDirection(Vector3 direction)
        {
            _direction = direction;
        }

        void Update()
        {
            transform.Translate(_direction * _speed * Time.deltaTime, Space.World);
            //transform.Translate(Vector3.up * _speed * Time.deltaTime, Space.Self);
        }

        public void SetDamage(int amount)
        {
            _damage = amount;
        }

        private void OnTriggerEnter(Collider other)
        {
            /// Cerca HealthComponent sull'oggetto colpito o parent
            HealthComponent target = other.GetComponentInParent<HealthComponent>();
            Debug.Log($"[Bullet] Colpito: {other.gameObject.name} — tag: {other.tag} — layer: {other.gameObject.layer}");
            
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