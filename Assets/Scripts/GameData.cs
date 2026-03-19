using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class WordCard
{
    public string word;             // Ana kelime (Örn: Futbol)
    public List<string> forbidden;  // Yasaklý kelimeler listesi
}

[System.Serializable]
public class CardCollection
{
    public List<WordCard> cards;   // Tüm kartlarýn tutulduðu liste
}



