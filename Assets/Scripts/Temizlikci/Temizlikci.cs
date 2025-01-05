using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temizlikci : MonoBehaviour
{
    // Arka plan (Background) ve zemin (Ground) nesnelerini tutan diziler
    private GameObject[] Background;
    private GameObject[] Ground;

    // Arka plan ve zemin nesnelerinin en son x pozisyonlarını tutar
    private float LastBGX;
    private float LastGroundX;

    // Awake, nesne oluşturulduğunda (Start'tan önce) bir kez çağrılır
    void Awake()
    {
        // "Background" etiketi ile işaretlenmiş tüm nesneleri bul ve diziye ata
        Background = GameObject.FindGameObjectsWithTag("Background");

        // "Groung" etiketi ile işaretlenmiş tüm nesneleri bul ve diziye ata
        Ground = GameObject.FindGameObjectsWithTag("Groung");

        // İlk arka plan nesnesinin x pozisyonunu al
        LastBGX = Background[0].transform.position.x;

        // İlk zemin nesnesinin x pozisyonunu al
        LastGroundX = Ground[0].transform.position.x;

        // Tüm arka plan nesneleri arasında en sağdaki (x değeri en büyük olan) nesneyi bul
        for (int i = 1; i < Background.Length; i++)
        {
            if (LastBGX < Background[i].transform.position.x)
            {
                LastBGX = Background[i].transform.position.x;
            }
        }

        // Tüm zemin nesneleri arasında en sağdaki (x değeri en büyük olan) nesneyi bul
        for (int i = 1; i < Ground.Length; i++)
        {
            if (LastGroundX < Ground[i].transform.position.x)
            {
                LastGroundX = Ground[i].transform.position.x;
            }
        }
    }

    // Bu fonksiyon, bir nesne tetikleyiciyle (trigger) çarpıştığında çağrılır
    void OnTriggerEnter2D(Collider2D hedef)
    {
        // Eğer çarpışan nesne "Background" etiketi taşıyorsa
        if (hedef.tag == "Background")
        {
            // Çarpışan nesnenin pozisyonunu al
            Vector3 temp = hedef.transform.position;

            // Nesnenin genişliğini hesapla (BoxCollider2D boyutunu alarak)
            float genislik = ((BoxCollider2D)hedef).size.x;

            // Nesneyi en sağdaki pozisyona taşı (genişliğine göre)
            temp.x = genislik + LastBGX;

            // Çarpışan nesnenin pozisyonunu güncelle
            hedef.transform.position = temp;

            // Yeni en sağdaki x pozisyonunu güncelle
            LastBGX = temp.x;
        }

        // Eğer çarpışan nesne "Groung" etiketi taşıyorsa
        if (hedef.tag == "Groung")
        {
            // Çarpışan nesnenin pozisyonunu al
            Vector3 temp = hedef.transform.position;

            // Nesnenin genişliğini hesapla (BoxCollider2D boyutunu alarak)
            float genislik = ((BoxCollider2D)hedef).size.x;

            // Nesneyi en sağdaki pozisyona taşı (genişliğine göre)
            temp.x = genislik + LastGroundX;

            // Çarpışan nesnenin pozisyonunu güncelle
            hedef.transform.position = temp;

            // Yeni en sağdaki x pozisyonunu güncelle
            LastGroundX = temp.x;
        }
    }

    // Start, oyun başlatıldığında bir kez çağrılır
    void Start()
    {
        // Şu an için boş bir işlem
    }

    // Update, her karede bir kez çağrılır
    void Update()
    {
        // Şu an için boş bir işlem
    }
}
