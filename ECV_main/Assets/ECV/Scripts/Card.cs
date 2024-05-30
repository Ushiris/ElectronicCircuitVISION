using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Scripting;

public class Card : MonoBehaviour, IPointerClickHandler
{
    //カードのデータ
    public CardData Data { get; private set; }

    //カードの見え方
    [SerializeField] TMP_Text Id, Name;

    bool reversed = true;

    [SerializeField] string id = "0";

    void Awake(){
        if(id != "0"){
            Init(id);
        }
    }

    public void Init(string id){
        Data = new(id);
        Id.text = Data.CardId;
        Name.text = Data.origin.Name;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameBoard.Instance.ShowCardDataView();
        GameBoard.Instance.cardDataView.SetData(Data);
    }
}
