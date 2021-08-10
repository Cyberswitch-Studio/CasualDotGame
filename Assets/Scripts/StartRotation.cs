using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRotation : MonoBehaviour
{
    public float speed = 2f;
    public float maxRotation = 45f;
    public Rigidbody2D rb;
    public float shootSpeed;
    public GameObject directionDots;
    bool shoot = false;

    void Start()
    {

    }

    void Update()
    {
        if (shoot == false)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, maxRotation * Mathf.Sin(Time.time * speed));
        }


        if (Input.GetMouseButtonDown(0))
        {
            shoot = true;
            directionDots.SetActive(false);
            rb.velocity = transform.up * speed;
        }
    }
}