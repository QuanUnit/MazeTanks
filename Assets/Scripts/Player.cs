using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Player : MonoBehaviour
{
    public CustomInput Input { get; private set; }

    [SerializeField] private float speed;
    [SerializeField] private float speedOfRotation;
    [SerializeField] private Rigidbody2D rigidbody2D;

    [Header("Input:")]
    [SerializeField] private KeyCode up;
    [SerializeField] private KeyCode down;
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode right;
    [SerializeField] private KeyCode shot;
    private void Awake()
    {
        Input = new CustomInput(left, right, up, down, shot);
    }
    private void Start()
    {
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
        rigidbody2D.angularVelocity += Input.GetHorisontalAxis() * -speedOfRotation * 5000;
    }
}