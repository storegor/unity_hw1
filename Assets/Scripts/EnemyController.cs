using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float speed = 5f;
    private bool faceRight = true;
    public GameObject leftBorder;
    public GameObject rightBorder;
    public Rigidbody2D rb;
    private bool dirRight = true;

    void Update()
    {
        if (transform.position.x > 8)
            dirRight = false;
        else if (transform.position.x < -9)
            dirRight = true;

        if (dirRight)
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, -1);
        else
            transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, -1);

        if (dirRight && faceRight)
            flip();
        else if (!dirRight && !faceRight)
            flip();
        }

        void flip () {
            faceRight = !faceRight;
            transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, -1);
        }
    }
