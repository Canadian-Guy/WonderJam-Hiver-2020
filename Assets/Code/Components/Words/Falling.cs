using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), DisallowMultipleComponent]
public class Falling : MonoBehaviour
{
    public Rigidbody2D rb;

    [Range(0f, 50f)]
    public float speed = .1f;
    public bool frozen = false;
    public bool reversed = false;

    private Vector2 _movement;

    private void OnCollisionEnter()
    {
        Debug.Log("BOOP");
    }

    private void Update()
    {
        _movement = (reversed ? Vector2.up : Vector2.down) * (frozen ? 0f : speed);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + _movement * Time.fixedDeltaTime);
    }
}
