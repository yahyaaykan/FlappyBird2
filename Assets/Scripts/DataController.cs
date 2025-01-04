using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DataController : MonoBehaviour
{
    public static DataController ornek;
    private string High_Score = "High Score";
    // Start is called before the first frame update
    void Start()
    {

        TekilNesne();
        Oyunilkdefabasladi();
        setHighScore(0);
    }
    void TekilNesne()
    {
        if (ornek != null)
        {
            Destroy(gameObject);
        }
        else
        {
            ornek = this;
            DontDestroyOnLoad(ornek);

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Oyunilkdefabasladi()
    {
        PlayerPrefs.SetInt(High_Score, 0);
        PlayerPrefs.SetInt("Oyunilkdefabasladi", 0);
    }
    public void setHighScore(int score)
    {
        PlayerPrefs.SetInt(High_Score, score);
    }
    public int getHighScore()
    {
        return PlayerPrefs.GetInt(High_Score);
    }
}
