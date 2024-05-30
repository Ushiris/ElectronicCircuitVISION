using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDataView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] TMP_Text id, node, nameText, text, range, reverse, state;

    public static CardDataView Instance;

    void Awake(){
        Instance = this;
    }

    void Clear(){
        id.text = "";
        node.text = "";
        nameText.text = "";
        reverse.text = "";
        state.text = "";
        range.text = "";
        text.text = "";
    }

    public void SetData(CardData newCard){
        Clear();
        Debug.Log("set data");
        var EffectedCard = newCard.EffectedCard;
        id.text = newCard.CardId + " " + CardDataConverter.CardPlayTypeToViewName(EffectedCard.PlayType);

        node.text = "Node:" + newCard.GetNodeNumber() + " Cost:" + newCard.GetCostNumber();
        if(EffectedCard.Graze != ""){
            node.text += " Graze:" + newCard.GetGrazeNumber() + " " + newCard.GetAttackNumber() + "/" + newCard.GetToughnessNumber();
        }

        nameText.text = newCard.Name;
        text.text = newCard.Text;
        if(EffectedCard.Popularity0 != ""){
            text.text += "\n<color=orange>人気4</color>:" + EffectedCard.Popularity4
            + "\n<color=yellow>人気2</color>:" + EffectedCard.Popularity2
            + "\n<color=white>人気1</color>:" + EffectedCard.Popularity1
            + "\n<color=#00ffffff>人気0</color>:" + EffectedCard.Popularity0;
        }

        if(EffectedCard.HasRace(CardRace.None)){
            range.text = "種族:" + CardDataConverter.CardRacesToViewName(EffectedCard.races);
        }

        if(EffectedCard.Range != CardEffectRange.None){
            range.text += CardDataConverter.CardEffectDurationToViewName(EffectedCard.Duration) + "/" + CardDataConverter.CardEffectRangeToViewName(EffectedCard.Range);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameBoard.Instance.HideCardDataView();
    }
}
