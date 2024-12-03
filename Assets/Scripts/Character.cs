using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Vector3 mouse_downposition;
    private Vector3 mouse_upposition;
    public float slingForce;
    private bool stroke;
    private float distance;
    private bool isDragging;
    public LineRenderer lr;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        lr.enabled = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouse_downposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse_downposition.z = 0;
            if (Vector3.Distance(mouse_downposition, transform.position) < 0.5f)
            {
                isDragging = true;
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            mouse_upposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse_upposition.z = 0;
            if (isDragging)
            {
                Movement();
            }
            isDragging = false;
        }

        if (isDragging)
        {
            lr.enabled = true;
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x)
            {
                spriteRenderer.flipX = true;
            }
            else
            { spriteRenderer.flipX = false; }
        }

        else
        { lr.enabled = false; }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            stroke = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            stroke = false;
        }
    }

    void Movement()
    {
        if (stroke == true)
        {
            Vector3 direction = mouse_upposition - mouse_downposition;
            _rigidbody2D.AddForce(-direction * slingForce, ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        _rigidbody2D.velocity = Vector3.zero;
        if (collision.gameObject.CompareTag("Platform"))
        {
            stroke = true;
        }
    }
}
