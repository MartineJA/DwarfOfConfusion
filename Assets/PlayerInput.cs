using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Rigidbody2D rgB;
    private Vector2 direction;
    private bool isJumping = false;
    private int nbJump = 0;

    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float jumpForce = 150f;
    [SerializeField]
    private int maxJump = 2;
    [SerializeField]
    private float gravityMod = 3f;

    [Header("Graphics")]
    [SerializeField]
    private GameObject graphics;
    private Animator animator;


    // commentaire pour montrer à Pierre:
    /* direction.x = Input.GetAxis("Horizontal") * speed;*/



    // Start is called before the first frame update
    void Awake()
    {
        rgB = GetComponent<Rigidbody2D>();
        animator = graphics.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxis("Horizontal") * speed;

        if (Input.GetButtonDown("Jump")&& nbJump < maxJump)
        {  
            isJumping = true;
            Debug.Log("saut");
        }

        animator.SetFloat("SpeedX", Mathf.Abs(direction.x));
            
        if (direction.x < 0f)
        {
            graphics.transform.localScale = new Vector3 (-1, 1, 1);
        }
        else if (direction.x > 0f)
        {
            graphics.transform.localScale = new Vector3(1, 1, 1);
        }


        

    }

    private void FixedUpdate()
    {   
        direction.y = rgB.velocity.y;
        
        if (isJumping)
        {
            direction.y = jumpForce*Time.deltaTime;
            isJumping= false;
            nbJump++;
        }
        rgB.velocity = direction;

        if (rgB.velocity.y < 0)
        {
            rgB.gravityScale = gravityMod;
        }
        else
        {
            rgB.gravityScale = 1f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            nbJump = 0;
        }
        if (collision.collider.CompareTag("Death"))
        {
            Debug.Log("tu es mort");
        }

    }



}
