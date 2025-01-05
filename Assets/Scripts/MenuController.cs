using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Başlangıçta bir kez çağrılır (oyun başlatıldığında).
    void Start()
    {
        // Şu an bu fonksiyon içinde bir işlem yapılmıyor.
    }

    // "Play" butonuna tıklanıldığında çağrılan fonksiyon.
    public void PlayButton()
    {
        // "SampleScene" adlı sahneyi yükler ve oyunu başlatır.
        SceneManager.LoadScene("SampleScene");
    }

    // Her bir karede bir kez çağrılır.
    void Update()
    {
        // Şu an bu fonksiyon içinde bir işlem yapılmıyor.
    }
}
