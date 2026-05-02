
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 8f;

    [Header("Jump")]
    [SerializeField] private float _jumpHeight = 3f;
    [SerializeField] private int _maxJumps = 2;

    private Rigidbody _rb;
    private GroundChecker _groundChecker;
    private PlayerAnimatorController _animatorController;

    private int _jumpsDone;
    private float _h;
    private bool _jumpRequested;



    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _groundChecker = GetComponentInChildren<GroundChecker>();
        GetComponent<HealthComponent>().Initialize(1);  /// Qua perche in HealthCondition crea problemi
        _animatorController = GetComponentInChildren<PlayerAnimatorController>();
    }

    void Update()
    {
        _h = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && CanJump())
        {
            _jumpRequested = true;
        }

        HandleRotation();
    }


    void FixedUpdate()
    {
        /// Applichiamo la velocità orizzontale mantenendo quella verticale
        _rb.velocity = new Vector3(_h * _moveSpeed, _rb.velocity.y, 0);
      
        if (_jumpRequested)
        {
            Jump();
        }      
    }


    //--[f.ni]--

    public bool IsGrounded => _groundChecker.IsGrounded;

    private bool CanJump()
    {
        if (_groundChecker.IsGrounded)
        {
            _jumpsDone = 0;
            return true;
        }

        if (_jumpsDone < _maxJumps)
        {
            return true;
        }

        return false;
    }

    private void HandleRotation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.forward, Vector3.zero);

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 mousePoint = ray.GetPoint (distance);
            float targetAngle = (mousePoint.x > transform.position.x) ? 90f : -90f;
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);
        }
    }

    private void Jump()
    {
        /// Reset della velocità verticale prima della spinta. Senza questo, se salto 2 volte, il salto
        /// non e' sempre uguale. 2 veloci salta sulla luna, 2 lenti il secondo rallenta la caduta
        _rb.velocity = new Vector3(_rb.velocity.x, 0, 0);

        /// Formula per calcolare la forza per arrivare alla _jumpHeight, indipendentemente dalla gravita'
        float jumpForce = Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y);

        _rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);

        _jumpsDone++;
        _animatorController?.TriggerJump();
        _jumpRequested = false;
    }

    public void AddSpeed(int amount)
    {
        _moveSpeed += amount;
    }
}