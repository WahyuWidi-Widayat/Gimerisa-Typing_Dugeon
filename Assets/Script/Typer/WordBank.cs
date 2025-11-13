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
    // 3–4 huruf (120 kata)
    "apel","buku","meja","kurs","padi","sapi","kuda","roti","nasi","kaca",
    "bola","tali","paku","jari","kayu","padi","susu","ikan","udang","rusa",
    "uang","mata","hati","pipa","kiri","kanan","atas","baju","tape","cabe",
    "dadu","obat","batu","sapu","toga","sapi","tani","tani","kota","desa",
    "biri","baca","tawa","rasa","ragi","toko","paku","pita","bibi","adik",
    "kiri","satu","tiga","empat","lima","enam","tujuh","delp","nona","ayah",
    "ibuk","gigi","tuli","bisu","ratu","raja","dana","data","tahu","padi",
    "roda","busa","rata","murn","tani","kupu","bisa","satu","baju","topi",
    "baki","kiri","palu","tali","pagi","sore","malam","siap","bang","lucu",
    "batu","kayu","baja","tanah","sapi","ayam","ikan","burung","rant","awan",
    "asap","hujan","laut","pura","seni","paku","pena","kata","suap","sapu",

    // 5–7 huruf (105 kata)
    "mangga","pisang","jeruk","semangka","tomato","mentimun","kompor","kursus","kantor","sekolah",
    "pelajar","pegawai","petani","nelayan","pemuda","penulis","perajin","karyawan","pelatih","seniman",
    "ilmuan","dokter","arsitek","pegawai","wartawan","editor","penyair","pelaut","penguji","murid",
    "tukang","penjahit","pemahat","guru","petugas","pekerja","pemadam","penonton","pelanggan","pemandu",
    "pembaca","petugas","petugas","penulis","penyuluh","pembina","pemimpin","perawat","pengasuh","penyair",
    "pelapor","peneliti","pengurus","pemahat","perakit","perancang","pelukis","penyapu","pengajar","perajin",
    "pengolah","pengirim","penyusun","penerbit","pencatat","penonton","penyimpan","penerjemah","pemasang",
    "pengembang","pengemudi","penyedia","penata","pelatih","penyiar","pengatur","penyemai","pengukur","pembantu",
    "penyusun","pengurus","pengelola","penata","penjaga","pelindung","penebang","penyapu","penambal","pemotong",
    "penyaring","penyusun","penarik","pembina","pengasuh","penyemai","penjilat","penyulam","penjahit","pembersih",
    "penyimpan","penyelam","penebar","pencetak","penambang","pengrajin","penyusun","pengatur","penata",

    // 8–12 huruf (75 kata)
    "pengembangan","komunikasi","konstruksi","transportasi","dokumentasi","implementasi",
    "konfigurasi","integrasi","optimalisasi","virtualisasi","perancangan","pengelolaan",
    "pemrograman","perdagangan","pengendalian","pemeriksaan","pengawasan","penyelidikan",
    "pengukuran","pendaftaran","pengelolaan","penyusunan","penyimpanan","pengumpulan",
    "pengantaran","pembangunan","penyusunan","pengarahan","penyelamatan","penyaluran",
    "pembersihan","pemberdayaan","penerbitan","pengiriman","perawatan","pemeliharaan",
    "pembelajaran","pendidikan","perkantoran","pengelolaan","penyelenggara","penyusunan",
    "pemasaran","pembuatan","penelusuran","penyempurnaan","penggabungan","pemanfaatan",
    "pemantauan","penelitian","peningkatan","pengembangan","pengelolaan","perhitungan",
    "pencatatan","pengendalian","pembukuan","penerjemahan","penyesuaian","pengoperasian",
    "pengoptimalan","pembangunan","pengawasan","penyaringan","penyatuan","penyerahan",
    "penciptaan","pengaturan","penyesuaian","pembekalan","penyebaran","pengendalian",
    "penguasaan","peningkatan","pengujian","penerapan","pengolahan","pemrosesan"
};


    private List<string> mediumWord = new List<string>()
    {
     
    };

    private List<string> hardWord = new List<string>()
    {
      
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
