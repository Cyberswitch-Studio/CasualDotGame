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

        if ( Input.GetMouseButtonDown(0))
        {
            shoot = true;
            directionDots.SetActive(false);
            rb.velocity = transform.up * shootSpeed;
        }
    }

    public void choiceDirecion()
    {
        if (shoot == false)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, maxRotation * Mathf.Sin(Time.time * speed));
        }
        else
        {
            //transform.Rotate(0, 0, 10 * speed * Time.deltaTime);
            //rb.velocity = transform.up * speed;
        }
    }
}