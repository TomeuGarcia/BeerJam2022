using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{

    public float speed;
    public int startingPoint;
    public Transform[] points;

    private int index;
    void Start()
    {
        transform.position = points[startingPoint].position;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, points[index].position) < 0.02f)
        {
            index++;
            if (index == points.Length)
            {
                index = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, points[index].position, speed * Time.deltaTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        collision.transform.SetParent(null);
    }
}