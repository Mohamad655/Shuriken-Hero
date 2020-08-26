using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class shuriken : MonoBehaviour
{
    [SerializeField] GameObject star;
    GameObject starhero;
    public Transform player;
    Player players;
    public Text instruction;

    LevelManager levelManager;

    public Image[] starNo;
    int noOfStars = 0;

    Rigidbody2D rb;
    public float sizeChange;

    
    float starSize = 0.2f;

    Boolean incOrDec = false;
    Boolean increase = true;

    private Touch touch;
    // Start is called before the first frame update
    void Start()
    {
        players = FindObjectOfType<Player>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {

       


        if (increase && incOrDec)
        {
            if (starSize > 1.2f)
            {
                increase = false;
            }
            starSize += sizeChange;
        }
        if (incOrDec && increase == false)
        {
            starSize -= sizeChange;
            if (starSize < 0.2f)
            {
                increase = true;
            }
        }
        if (Input.GetKeyDown("space"))
             {


                 starSize = 0.2f;
                 Vector2 starPos = new Vector2(player.position.x, player.position.y);
                 transform.position = starPos;
                 starhero = GameObject.Instantiate(star, transform.position, Quaternion.identity,transform.parent) as GameObject;

                 rb = starhero.GetComponent<Rigidbody2D>();
                 rb.angularVelocity = 200f;
                 incOrDec = true;


             }
    
         
        if (Input.touchCount >= 1)
        {
            touch = Input.GetTouch(0);
            
            print(Camera.main.ScreenToWorldPoint(touch.position).x);
            if(touch.phase == TouchPhase.Began && Camera.main.ScreenToWorldPoint(touch.position).x>2 && Camera.main.ScreenToWorldPoint(touch.position).x < 7)
            {
                Destroy(instruction,1f);
              
                starSize = 0.2f;
                Vector2 starPos = new Vector2(player.position.x, player.position.y);
                transform.position = starPos;
                starhero = GameObject.Instantiate(star, transform.position, Quaternion.identity, transform.parent) as GameObject;

                rb = starhero.GetComponent<Rigidbody2D>();
                rb.angularVelocity = 200f;
                incOrDec = true;

            }else if(touch.phase == TouchPhase.Ended )
            {
                
                if (rb != null)
                {
                    players.Jump();
                    rb.velocity = new Vector2(0, 5f);
                    rb.angularVelocity = 1000f;
                    incOrDec = false;
                }
            }
        }




          if (Input.GetKeyUp("space"))
             {
                 players.Jump();
                 if (rb != null)
                 {
                     rb.velocity = new Vector2(0, 5f);
                     rb.angularVelocity = 1000f;
                     incOrDec = false;
                 }
             }
     
         
        if (starhero != null)
        {
            starhero.transform.localScale = new Vector2(starSize, starSize);
        }

    }
    public void starless()
    {
        if (noOfStars < 3)
        {
            Destroy(starNo[noOfStars]);
        }
      
        
        noOfStars += 1;

       
        if (noOfStars == 3)
        {
         levelManager.loadLevl("GameOver");
            
        }
    }
}
