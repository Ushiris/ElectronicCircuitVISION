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
    [SerializeField] GameObject view;
    [SerializeField] TMP_Text Id, Name;

    void Awake(){
        Data = new CardData();
    }

    public void SetData(CardData data){
        Data = data;
    }
}
