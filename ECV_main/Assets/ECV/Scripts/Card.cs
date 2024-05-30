using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Scripting;

/// <summary>
/// ゲーム内におけるカードそのものを表現するクラス。
/// 裏向きや本来のユーザー、このターンにプレイされたかどうかなど、カードから見てメタ的な情報はここに保持される。
/// </summary>
public class Card : MonoBehaviour, IPointerClickHandler
{
    //カードのデータ
    public CardData Data { get; private set; }

    //カードの情報を表示する部分
    [SerializeField] TMP_Text Id, Name;

    bool reversed = true;

    bool isYourCard = true;

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
