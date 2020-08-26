using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Circles : MonoBehaviour
{
    public Text instruction;

    private Material currentMat1;
    private Material currentMat2;
    public Image[] hearts;
    public Text text;
    public Animator hurtAnim;

    LevelManager levelManager;

   public int score=0;
    int lifeGone = 0;

    [SerializeField] float Xmax;
    [SerializeField] float Xmin;
 

    [SerializeField] float outsideMaxsize;
    [SerializeField] float outsideMinsize;
    float insideMaxsize;
    float insideMinsize;


    public GameObject[] enemy;
    GameObject enemyObject;

    int enemyNo;

    
    float Xpos;
   
    float outsideSize;
    float insideSize;

   

    private void Awake()
    {
       
        int scorer = FindObjectsOfType<Circles>().Length;
             if (scorer > 1)
             {
                 gameObject.SetActive(false);
                 Destroy(gameObject);
            
        }
             else
             {
                 DontDestroyOnLoad(gameObject);
        
        }
    
    }
 /*  void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // here you can use scene.buildIndex or scene.name to check which scene was loaded
        if (scene.name == "GameOver")
        {
            
            // Destroy the gameobject this script is attached to
            Destroy(gameObject);
        }
    }
 */
    // Start is called before the first frame update
    void Start()
    {
        
        print("start");
        enemyNo = Random.Range(0, 4);
        Invoke("instantiator", 2.5f);
        Destroy(instruction, 10f);
        text.text = score.ToString();
        levelManager = FindObjectOfType<LevelManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyObject != null)
        {
            if (enemyObject.transform.position.y < 6)
            {
                Color old = currentMat1.color;
                Color newC = new Color(old.r, old.g, old.b, 0.5f);
                currentMat1.SetColor("_Color", newC);
                print("1");
            }
            if (enemyObject.transform.position.y < 6)
            {
                Color oldColor = currentMat2.color;
                Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, 0.5f);
                currentMat2.SetColor("_Color", newColor);
                print("1");

            }
        }
      
    }
    public void instantiator()
    {
        Xpos = Random.Range(Xmin, Xmax);
       

        outsideSize = Random.Range(outsideMinsize, outsideMaxsize);

        Vector2 circlePos = new Vector2(Xpos, 16f);
        transform.position = circlePos;
        
        
        enemyObject = GameObject.Instantiate(enemy[enemyNo], transform.position, transform.rotation,transform.parent) as GameObject;
        enemyObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1.5f);
            
        var enemies = enemyObject.transform;
        var outsideComponent = enemies.GetChild(0);
        currentMat1 = outsideComponent.GetComponent<Renderer>().material;
        var insideComponent = enemies.GetChild(1);
        currentMat2 = insideComponent.GetComponent<Renderer>().material;
        outsideComponent.transform.localScale=new Vector2(outsideSize, outsideSize);
        insideMaxsize = outsideSize * 0.7f;
        insideMinsize = 0.25f;
        insideSize = Random.Range(insideMinsize, insideMaxsize);
        insideComponent.transform.localScale = new Vector2(insideSize, insideSize);

      





        enemyNo = Random.Range(0, 4);

    }
    public void lifeless()
    {
        if (lifeGone < 3)
        {
            Destroy(hearts[lifeGone]);
        }
      

        lifeGone += 1;


        if (lifeGone == 3)
        {
            levelManager.loadLevl("GameOver");
           
        }
    }
    public void setScore()
    {
        score += 5;
        text.text = score.ToString();
    }
    public void hurt()
    {
        hurtAnim.SetTrigger("hurt");
    }
    public void resetGame()
    {
        Destroy(gameObject);
    }

}
