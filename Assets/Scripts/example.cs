using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class example : MonoBehaviour
{
    [Range(0, 50)]
    public int segments = 50;
    [Range(0, 5)]
    public float xradius = 1;
    [Range(0, 5)]
    public float yradius = 1;
    LineRenderer line;
    LineRenderer redLine;
    public GameObject redLineGameObj;

    public bool inCollider = false;
    public GameObject player;
    public float orbitSpeed = 10.0f;

    void Start()
    {
        CircleCollider2D cc2d = gameObject.AddComponent(typeof(CircleCollider2D)) as CircleCollider2D;
        cc2d.radius = 1f;
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
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition(i, new Vector3(x, y, 0));

            angle += (360f / segments);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        inCollider = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inCollider = false;
        redLineGameObj.SetActive(false);
    }

    void Update()
    {
        if (inCollider == true/* && Input.GetMouseButtonDown(0)*/)
        {
            player.transform.Rotate(Vector2.up * orbitSpeed * Time.deltaTime);

            List<Vector3> pos = new List<Vector3>();
            pos.Add(this.transform.position);
            pos.Add(player.transform.position);
            redLine.startWidth = 0.02f;
            redLine.endWidth = 0.02f;


            redLine.useWorldSpace = false;
            redLine.startColor = Color.red;
            redLine.endColor = Color.red;

            redLine.SetPositions(pos.ToArray());
        }

        Debug.Log(AngleBetweenVector2(player.transform.position, this.transform.position));
        if (AngleBetweenVector2(player.transform.position, this.transform.position) >= 90)
        {
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 difference = vec2 - vec1;
        //float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        //return Vector2.Angle(Vector2.right, difference);
        return Vector2.Angle(Vector2.up, difference);
    }
}