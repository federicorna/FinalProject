using UnityEngine;
using UnityEngine.AI; // Aggiunto per gestire la NavMesh

public class EnemyAgent : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private NavMeshAgent _agent;

    // Variabile per memorizzare l'ultima posizione valida a terra
    private Vector3 _lastGroundedTargetPos;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        SetAgentPosition();
    }

    private void SetAgentPosition()
    {
        if (_target == null) return;

        // Creiamo un "colpo" per la NavMesh
        NavMeshHit hit;

        // Controlliamo se c'è della NavMesh calpestabile vicino alla posizione del target
        // Il raggio (1.0f) definisce quanto "in alto" o "lontano" può essere il player rispetto al suolo
        if (NavMesh.SamplePosition(_target.position, out hit, 1.0f, NavMesh.AllAreas))
        {
            // Se il player è sulla NavMesh (o molto vicino), aggiorniamo la posizione
            _lastGroundedTargetPos = hit.position;
        }

        // Diamo all'agent l'ultima posizione valida. 
        // Se il player sta saltando (fuori dalla NavMesh), l'agent continuerà ad andare 
        // verso il punto in cui il player è "sparito" dalla NavMesh (il bordo).
        _agent.SetDestination(_lastGroundedTargetPos);
    }
}