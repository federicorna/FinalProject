
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimatorController : MonoBehaviour
{
    private Animator _animator;
    private PlayerMovement _playerMovement;
    private HealthComponent _health;
    private Rigidbody _rb;

    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int IsGroundedHash = Animator.StringToHash("IsGrounded");
    private static readonly int JumpHash = Animator.StringToHash("Jump");
    private static readonly int IsDeadHash = Animator.StringToHash("IsDead");
    private static readonly int IsShootingHash = Animator.StringToHash("IsShooting");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponentInParent<PlayerMovement>();
        _health = GetComponentInParent<HealthComponent>();
        _rb = GetComponentInParent<Rigidbody>();

        _health.OnDeath.AddListener(HandleDeath);
    }

    private void OnDestroy()
    {
        _health.OnDeath.RemoveListener(HandleDeath);
    }

    private void Update()
    {
        /// Velocità orizzontale per idle o run
        _animator.SetFloat(SpeedHash, Mathf.Abs(_rb.velocity.x));

        /// A terra o in aria
        _animator.SetBool(IsGroundedHash, _playerMovement.IsGrounded);

        /// Sta sparando
        _animator.SetBool(IsShootingHash, Input.GetButton("Fire1"));
    }

    public void TriggerJump()
    {
        _animator.SetTrigger(JumpHash);
    }

    private void HandleDeath()
    {
        _animator.SetBool(IsDeadHash, true);
    }
}