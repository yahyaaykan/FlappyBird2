using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DataController : MonoBehaviour
{
    // DataController sınıfının tekil (singleton) örneği
    public static DataController ornek;

    // "High_Score" adında bir anahtar tanımlandı, bu yüksek skoru tutacak
    private string High_Score = "High Score";

    // Oyun başladığında bir kez çağrılır
    void Start()
    {
        // Tekil bir nesne oluştur
        TekilNesne();

        // Oyunun ilk kez başlatıldığında gerekli ayarları yap
        Oyunilkdefabasladi();

        // Başlangıç için yüksek skoru sıfır olarak ayarla
        setHighScore(0);
    }

    // Tekil bir nesne oluşturmayı sağlayan fonksiyon
    void TekilNesne()
    {
        // Eğer bir örnek zaten mevcutsa bu nesneyi yok et
        if (ornek != null)
        {
            Destroy(gameObject);
        }
        else
        {
            // Eğer mevcut değilse bu nesneyi örnek olarak ayarla
            ornek = this;

            // Bu nesneyi sahneler arasında yok edilmez hale getir
            DontDestroyOnLoad(ornek);
        }
    }

    // Her bir karede bir kez çağrılır
    void Update()
    {
        // Şu anda bu fonksiyon boş
    }

    // Oyunun ilk kez başlatıldığında çağrılır
    void Oyunilkdefabasladi()
    {
        // PlayerPrefs ile yüksek skoru sıfır olarak ayarla
        PlayerPrefs.SetInt(High_Score, 0);

        // "Oyunilkdefabasladi" anahtarını sıfır olarak ayarla
        PlayerPrefs.SetInt("Oyunilkdefabasladi", 0);
    }

    // Yüksek skoru ayarlayan fonksiyon
    public void setHighScore(int score)
    {
        // PlayerPrefs kullanarak "High Score" anahtarıyla skoru kaydet
        PlayerPrefs.SetInt(High_Score, score);
    }

    // Yüksek skoru döndüren fonksiyon
    public int getHighScore()
    {
        // PlayerPrefs'ten "High Score" anahtarına kayıtlı değeri döndür
        return PlayerPrefs.GetInt(High_Score);
    }
}
