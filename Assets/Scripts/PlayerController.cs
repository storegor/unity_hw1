using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float speed = 7f;
    private Rigidbody2D rb;
    private bool faceRight = true;
    private float jumpForce = 19f;
    int playerObj, platformObj;
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpsValue = 1;
    void Start()
    {
        rb = GetComponent <Rigidbody2D> ();

        playerObj = LayerMask.NameToLayer("player");
        platformObj =  LayerMask.NameToLayer("platform");

        extraJumps = extraJumpsValue;
    }

    void Update()
    {
        if (isGrounded)
            extraJumps = extraJumpsValue;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        float moveX = Input.GetAxis ("Horizontal");
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
        if (Input.GetKeyDown (KeyCode.UpArrow) && extraJumps > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            extraJumps--;
        } else if (Input.GetKeyDown (KeyCode.UpArrow) && extraJumps == 0 && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (moveX > 0 && !faceRight)
            flip ();
        else if (moveX < 0 && faceRight)
            flip ();

        if (rb.velocity.y > 0)
            Physics2D.IgnoreLayerCollision(playerObj, platformObj, true);
        else
            Physics2D.IgnoreLayerCollision(playerObj, platformObj, false);

    }

    void flip ()
    {
        faceRight = !faceRight;
        transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals ("Enemy")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
