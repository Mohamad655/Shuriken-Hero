using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    Circles circle;
    public Animator animTransition;
    // Start is called before the first frame update
    void Start()
    {
        circle = FindObjectOfType<Circles>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void loadLevl(string levelName)
    {
        if (levelName == "Game" && circle!=null)
        {
            
            circle.resetGame();
        }
      StartCoroutine(Transition(levelName));
    }
    IEnumerator Transition(string levelName)
    {
        animTransition.SetTrigger("end");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(levelName);
    }
}
