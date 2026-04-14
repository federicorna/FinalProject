
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private float _mouseSensitivity = 2f;
    [SerializeField] private float _jumpHeight = 2;

    private Rigidbody _rb;
    private GroundChecker _groundChecker;

    private float _v, _h;
    private float _mouseX;
    private bool _isJumping;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _groundChecker = GetComponentInChildren<GroundChecker>();
    }

    void Update()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");

        _mouseX += Input.GetAxis("Mouse X") * _mouseSensitivity;

        _isJumping = Input.GetButtonDown("Jump");

        if (_isJumping && _groundChecker.IsGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        Quaternion playerRotation = Quaternion.Euler(0, _mouseX, 0);
        _rb.MoveRotation(playerRotation);

        Vector3 moveDir = (transform.forward * _v) + (transform.right * _h);

        Vector3 targetPosition = _rb.position + moveDir.normalized * (_moveSpeed * Time.fixedDeltaTime);
        _rb.MovePosition(targetPosition);

    }

    private void Jump()
    {
        _rb.AddForce(Vector3.up * Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        _isJumping = false;
    }

}