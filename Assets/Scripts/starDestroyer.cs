using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class starDestroyer : MonoBehaviour
{

    private AudioSource audioSource;
    public AudioClip[] clips;

    shuriken shur;
    destroyer dest;

   public GameObject starParticleEffect;
   
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        

        shur = FindObjectOfType<shuriken>();
        dest = FindObjectOfType<destroyer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 16)
        {
            Destroy(gameObject);
            shur.starless();

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (dest.collider1 != null)
        {
            if (collision.gameObject.GetComponent<CircleCollider2D>() == dest.collider1 && transform.position.y>6)
            {
                Destroy(gameObject);
                shur.starless();
                Instantiate(starParticleEffect, transform.position, Quaternion.identity);
                int randomNumber = UnityEngine.Random.Range(0, clips.Length);
                AudioClip clip = clips[randomNumber];
                AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
            }
        }
      
    }
}
