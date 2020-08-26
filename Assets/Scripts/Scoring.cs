using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class Scoring : MonoBehaviour
{

    private string PlayStoreId ="3759143";
    private string AppStoreId = "3759142";
    private string interstitialAd = "video";
    public bool isTargetPlaystore;
    public bool isTestAd;
   public Text score;
    Circles circle;
    public Text highScore;
    int playerHighScore;
    // Start is called before the first frame update
    void Start()
    {
        InitializeAdvertisement();

        playerHighScore = PlayerPrefs.GetInt("Player High Score");
        circle = FindObjectOfType<Circles>();
        score.text = circle.score.ToString();
        if (circle.score >= playerHighScore)
        {
            playerHighScore = circle.score;
            PlayerPrefs.SetInt("Player High Score", playerHighScore);
        }
       
        highScore.text = playerHighScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void InitializeAdvertisement()
    {
        if (isTargetPlaystore)
        {
            
            Advertisement.Initialize(PlayStoreId, isTestAd);
            Invoke("PlayInterstitialAd", 2f);
            return;
            
        }
        Advertisement.Initialize(AppStoreId, isTestAd);
        
        PlayInterstitialAd();
    }
    public void PlayInterstitialAd()
    {
        if (!Advertisement.IsReady(interstitialAd))
        {
           
            return;
        }
        Advertisement.Show(interstitialAd);
       
    }
}
