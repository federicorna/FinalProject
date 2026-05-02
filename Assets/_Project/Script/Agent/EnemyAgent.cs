using UnityEngine;
using UnityEngine.AI;

public class EnemyAgent : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private NavMeshAgent _agent;

    /// Per ultima posizione valida a terra x NavMesh
    private Vector3 _lastGroundedTargetPos;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            _target = player.transform;
        else
            Debug.LogWarning("[EnemyAgent] Nessun GameObject con tag 'Player' trovato in scena!");
    }

    private void Update()
    {
        SetAgentPosition();
    }


    //--[f.ni]--

    private void SetAgentPosition()
    {
        if (_target == null) return;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(_target.position, out hit, 1.0f, NavMesh.AllAreas))
            _lastGroundedTargetPos = hit.position;

        _agent.SetDestination(_lastGroundedTargetPos);

        // Ruota il nemico verso il player — solo sinistra o destra
        float directionX = _target.position.x - transform.position.x;
        if (Mathf.Abs(directionX) > 0.1f)
        {
            float targetAngle = directionX > 0 ? 90f : -90f;
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);
        }
    }
}