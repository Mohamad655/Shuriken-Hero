using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed;
    

    private Animator anim;
    private Touch touch;

    float Xvel;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

     if (Input.touchCount >= 1)
        {
            touch = Input.GetTouch(0);
           
            
            if (touch.phase == TouchPhase.Stationary && Camera.main.ScreenToWorldPoint(touch.position).x < 3)
            {
                
                Xvel = -1f;

            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Xvel = 0f;
                
            }
        }
        if (Input.touchCount >= 1)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Stationary && Camera.main.ScreenToWorldPoint(touch.position).x > 6)
            {
                Xvel = 1f;

            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Xvel = 0f;

            }
        }
    
     //   Xvel = Input.GetAxisRaw("Horizontal");
        Vector2 playerVelocity = new Vector2(Xvel*speed, rb.velocity.y);
        float restrict = Mathf.Clamp(transform.position.x, 0.5f, 8.5f);
        transform.position = new Vector2(restrict, transform.position.y);
        rb.velocity = playerVelocity;
        if (playerVelocity != Vector2.zero)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
       
    }
    public void Jump()
    {
        anim.SetTrigger("jump");
    }

    public void moveLeft()
    {
        Xvel = -1f;
    }
    public void moveRight()
    {
        Xvel = 1f;
    }
    public void stop()
    {
        Xvel = 0f;
    }


}
