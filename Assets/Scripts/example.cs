using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class example : MonoBehaviour
{
    [Range(0, 50)]
    public int segments = 50;
    [Range(0, 5)]
    public float radius = 1;
    public float spiralAngle = 85;
    LineRenderer line;
    LineRenderer redLine;
    public GameObject redLineGameObj;

    Vector3 pointIn;

    public bool inCollider = false;
    public GameObject player;
    public float orbitSpeed = 10.0f;
    bool[] rotate = new bool[] { false, false };    //0- czy siê obraca, 1- w któr¹ stronê: true- prawo, false- lewo

    void Start()
    {
        CircleCollider2D cc2d = gameObject.AddComponent(typeof(CircleCollider2D)) as CircleCollider2D;
        cc2d.radius = radius;
        cc2d.isTrigger = true;

        line = gameObject.GetComponent<LineRenderer>();
        redLine = redLineGameObj.GetComponent<LineRenderer>();

        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        line.startWidth = 0.02f;
        line.endWidth = 0.02f;
        line.startColor = Color.blue;
        line.endColor = Color.blue;
        line.loop = true;
        CreatePoints();
    }

    void CreatePoints()
    {
        float x;
        float y;
        float z;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            line.SetPosition(i, new Vector3(x, y, 0));

            angle += (360f / segments);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        inCollider = true;
        Debug.DrawLine(transform.position, player.transform.position, Color.green, 100);
        pointIn = player.transform.position;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inCollider = false;
        redLineGameObj.SetActive(false);
    }

    private void Update()
    {
        if (inCollider == true/* && Input.GetMouseButtonDown(0)*/)
        {
            //player.transform.Rotate(Vector2.up * orbitSpeed * Time.deltaTime);

            //Debug.DrawLine(this.transform.position, player.transform.position, Color.red);

            //List<Vector3> pos = new List<Vector3>();
            //pos.Add(this.transform.position);
            //pos.Add(player.transform.position);
            //redLine.startWidth = 0.02f;
            //redLine.endWidth = 0.02f;

            //redLine.useWorldSpace = false;
            //redLine.startColor = Color.red;
            //redLine.endColor = Color.red;

            //redLine.SetPositions(pos.ToArray());

            if (Vector2.Angle(player.transform.position - transform.position, pointIn - player.transform.position) >= 89)
            {
                if (!rotate[0] && player.transform.position.x - transform.position.x > transform.position.x - player.transform.position.x)
                {
                    rotate[0] = true;
                    //rotateAround(Vector3.back);
                }
                else if (!rotate[0] && transform.position.x - player.transform.position.x > player.transform.position.x - transform.position.x)
                {
                    rotate[0] = true;
                    rotate[1] = true;
                    //rotateAround(Vector3.forward);
                }
                //rotate[0] = true;
            }
            if (rotate[0])
            {
                //rotateAround(Vector3.back);
                Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
                rb.velocity = Vector2.zero;

                Vector3 directionTo = transform.position - player.transform.position;
                if (rotate[1])
                {
                    directionTo = Quaternion.Euler(0, 0, spiralAngle) * directionTo;
                    float distanceThisFrame = orbitSpeed * Time.deltaTime;
                    player.transform.Translate(directionTo.normalized * distanceThisFrame, Space.World);
                }
                else if (!rotate[1])
                {
                    directionTo = Quaternion.Euler(0, 0, -spiralAngle) * directionTo;
                    float distanceThisFrame = orbitSpeed * Time.deltaTime;
                    player.transform.Translate(directionTo.normalized * distanceThisFrame, Space.World);
                }
            }
            Debug.Log(rotate[0].ToString() + "   " + rotate[1].ToString());
        }

        Vector3 direction = player.transform.position - transform.position;
        Debug.DrawLine(transform.position, player.transform.position, Color.red);
        Debug.DrawLine(pointIn, player.transform.position, Color.yellow);

        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Debug.Log("Angle:" + angle);
        //Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.rotation = Quaternion.Slerp(transform.rotation, angleAxis, Time.deltaTime * 50);

        //Debug.Log(AngleBetweenVector2(player.transform.position, this.transform.position));
        //Debug.Log(Vector2.Angle(player.transform.position - transform.position, pointIn - player.transform.position));
    }

    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 difference = vec2 - vec1;
        //float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        //return Vector2.Angle(Vector2.right, difference);
        return Vector2.Angle(Vector2.up, difference);
    }

    void rotateAround(Vector3 rotateVec)
    {
        player.transform.RotateAround(transform.position, rotateVec, orbitSpeed * Time.deltaTime);
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        //player.transform.rotation = Quaternion.Euler(0f, 0f, 500f * Time.deltaTime);
    }
}