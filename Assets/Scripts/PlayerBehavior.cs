using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    [Header("Movement & Jumping")]
    public float MoveSpeed = 10f;
    public float RotateSpeed = 75f;
    public float JumpVelocity = 5f;
    public float DistanceToGround = 0.1f;
    public LayerMask GroundLayer;

    [Header("Combat")]
    public GameObject Bullet;
    public float BulletSpeed = 100f;
    public Camera mainCamera;

    private float _vInput;
    private float _hInput;
    private bool _isJumping;
    private bool _isShooting;

    private Rigidbody _rb;
    private CapsuleCollider _col;
    private GameBehavior _gameManager;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
        
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnMove(InputValue value)
    {
        Vector2 move = value.Get<Vector2>();
        _vInput = move.y * MoveSpeed;
        _hInput = move.x * RotateSpeed;
    }

    void Update()
    {
        _isJumping |= Keyboard.current.spaceKey.wasPressedThisFrame;
        _isShooting |= Mouse.current.leftButton.wasPressedThisFrame;
    }

    void FixedUpdate()
    {
        // Movement and Rotation
        Vector3 rotation = Vector3.up * _hInput;
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        _rb.MovePosition(this.transform.position + this.transform.forward * _vInput * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation * angleRot);

        // Jumping
        if (IsGrounded() && _isJumping)
        {
            _rb.AddForce(Vector3.up * JumpVelocity, ForceMode.Impulse);
        }
        _isJumping = false;

        // Shooting
        if (_isShooting)
{
    Vector3 spawnPos = this.transform.position + (mainCamera.transform.forward * 1.5f);
    GameObject newBullet = Instantiate(Bullet, spawnPos, mainCamera.transform.rotation);

    // Prevent the bullet from hitting the player
    Physics.IgnoreCollision(newBullet.GetComponent<Collider>(), _col);

    Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
    bulletRB.linearVelocity = mainCamera.transform.forward * BulletSpeed;
}

        _isShooting = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            _gameManager.HP -= 1;
        }
    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);
        return Physics.CheckCapsule(_col.bounds.center, capsuleBottom, DistanceToGround, GroundLayer, QueryTriggerInteraction.Ignore);
    }
}
