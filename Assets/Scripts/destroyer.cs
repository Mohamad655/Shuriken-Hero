using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyer : MonoBehaviour
{

    private AudioSource audioSource;
    public AudioClip[] clips;

    public GameObject particleEffect;
    Animator shake;
    
    Circles circles;
    [HideInInspector]
    public CircleCollider2D collider1;
    Collider2D collider2;

    Boolean inst = true;

   

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
       

        circles = FindObjectOfType<Circles>();
        shake = Camera.main.GetComponent<Animator>();
       

    }
 
    private void OnEnable()
    {
        collider1 = gameObject.transform.GetChild(0).GetComponent<CircleCollider2D>();
        collider2 = gameObject.transform.GetChild(1).GetComponent<Collider2D>();
    }

    private void Update()
    {
      if(transform.position.y < 4f)
        {

            Destroy(gameObject);
            circles.hurt();
            circles.instantiator();
            circles.lifeless();
        }
      



    }
    void OnTriggerStay2D(Collider2D other)
    {
       

        if (collider1.bounds.Contains(other.bounds.max) && collider1.bounds.Contains(other.bounds.min))
       {
            
            if (other.bounds.Contains(collider2.bounds.max) && other.bounds.Contains(collider2.bounds.min))
            {

                if (transform.position.y > 6)
                {
                    int randomNumber = UnityEngine.Random.Range(0, clips.Length);
                    AudioClip clip = clips[randomNumber];
                    AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    Destroy(other.gameObject,0.1f);
                    Destroy(gameObject,0.1f);
                    Instantiate(particleEffect, transform.position, Quaternion.identity);
                    if (inst)
                    {
                        inst = false;
                        Instant();
                        circles.setScore();
                        shake.SetTrigger("shake");
                    }
                    
                    
                   
                }

            }

        }
       
    }

    void Instant()
    {
        circles.instantiator();
    }



}
