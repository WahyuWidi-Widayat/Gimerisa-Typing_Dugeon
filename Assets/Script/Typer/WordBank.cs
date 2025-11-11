using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WordBank : MonoBehaviour
{

    void Start()
    {
        
    }

    private List<string> easyWord = new List<string>()
    {
        "apel",
        "mangga",
        "pisang",
        "jeruk",
        "semangka",
        "titit",
        "kontol",
        "memek",
        "bangsat",
        "anjing",
        "babi",
        "monyet",
        "kuda",
        "ular",
        "sapi",


    };

    private List<string> mediumWord = new List<string>()
    {
        "komputer",
        "laptop",
        "keyboard",
        "monitor",
        "printer",
        "speaker",
        "headphone",
        "charger",
        "baterai",
        "prosesor"
    };

    private List<string> hardWord = new List<string>()
    {
        "pengembangan",
        "komunikasi",
        "konstruksi",
        "transportasi",
        "dokumentasi",
        "implementasi",
        "konfigurasi",
        "integrasi",
        "optimalisasi",
        "virtualisasi"
    };

private List<string> workingWords = new List<string>();
    

    // Update is called once per frame
    void Update()
    {

    }

    private void Shuffle(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int random = Random.Range(0, list.Count);
            string temp = list[i];

            list[i] = list[random];
            list[random] = temp;
        }
    }

    private void ConvertToLower(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i] = list[i].ToLower();
        }
    }
    
   public string GetWord()
{
    // Jika belum ada word di workingWords, isi ulang dari easyWord (bisa juga dari medium/hard)
    if (workingWords.Count == 0)
    {
        // Pilih salah satu tingkat kesulitan yang kamu mau
        workingWords.AddRange(easyWord); 

        // Ubah semua ke lowercase agar seragam
        ConvertToLower(workingWords);

        // Acak kata-katanya
        Shuffle(workingWords);
    }

    // Ambil kata terakhir
    string newWord = workingWords.Last();

    // Hapus kata yang sudah diambil
    workingWords.Remove(newWord);

    return newWord;
}

}
