using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxOnConveyor : MonoBehaviour
{
    private Transform _endPoint;
    private float _speed;
    private Rigidbody2D _rb2d;

    public void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.bodyType = RigidbodyType2D.Kinematic;
    }

    public void Update()
    {
        if (transform.position == _endPoint.position) DetachFromConveyor();
        transform.position = Vector3.MoveTowards(
            transform.position,
            _endPoint.position,
            _speed * Time.deltaTime
        );
    }

    private void DetachFromConveyor()
    {
        _rb2d.bodyType = RigidbodyType2D.Dynamic;
        enabled = false;
    }

    public void Init(Transform endPoint, float speed)
    {
        _endPoint = endPoint;
        _speed = speed;
    }
}