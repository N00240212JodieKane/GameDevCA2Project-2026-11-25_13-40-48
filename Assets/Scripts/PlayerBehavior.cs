using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    public float MoveSpeed = 10f;
    public float RotateSpeed = 75f;

    private float _vInput;
    private float _hInput;

    private Rigidbody _rb;

    void OnMove(InputValue value)
    {
        Vector2 move = value.Get<Vector2>();
        _vInput = move.y * MoveSpeed;
        _hInput = move.x * RotateSpeed;
    }



    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //this.transform.Translate(Vector3.forward * _vInput * Time.deltaTime);
       // this.transform.Rotate(Vector3.up * _hInput * Time.deltaTime);
    }

    void FixedUpdate() 
    {
        Vector3 rotation = Vector3.up * _hInput;
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        _rb.MovePosition(this.transform.position + this.transform.forward * _vInput * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation * angleRot);
    }
}
