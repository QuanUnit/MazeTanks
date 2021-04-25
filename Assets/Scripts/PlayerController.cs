using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CustomInput Input { get; private set; }

    [SerializeField] private float speed;
    [SerializeField] private float speedOfRotation;

    [Header("Input:")]
    [SerializeField] private KeyCode up;
    [SerializeField] private KeyCode down;
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode right;
    [SerializeField] private KeyCode shot;

    private Rigidbody2D rigidbody2D;

    private void Awake()
    {
        Input = new CustomInput(left, right, up, down, shot);
        if (TryGetComponent<Rigidbody2D>(out rigidbody2D) == false)
            throw new MissingReferenceException("Component not found");
    }
    private void FixedUpdate()
    {
        Move();
        Rotate();
    }
    private void Move()
    {
        rigidbody2D.velocity = transform.up * Input.GetVerticalAxis() * speed;
    }
    private void Rotate()
    {
        rigidbody2D.angularVelocity = Input.GetHorisontalAxis() * -speedOfRotation * 4000;
    }
}
