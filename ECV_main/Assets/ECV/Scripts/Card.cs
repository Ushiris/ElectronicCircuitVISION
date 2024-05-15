using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Scripting;

public class Card : MonoBehaviour
{
    //カードのデータ
    public CardData Data { get; private set; }

    //カードの見え方
    [SerializeField] TMP_Text Id, Name;

    bool reversed = true;

    public void Init(string id){
        Data = new(id);
        Id.text = Data.CardId;
        Name.text = Data.origin.Name;
    }
}
