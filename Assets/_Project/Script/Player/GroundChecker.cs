
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private float _groundDistance = 0.2f;
    [SerializeField] private LayerMask _groundLayer;
    public bool IsGrounded;


    private void Update()
    {
        IsGrounded = Physics.CheckSphere (transform.position, _groundDistance, _groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = IsGrounded ? Color.green : Color.blue;
        Gizmos.DrawWireSphere (transform.position, _groundDistance);
    }
}