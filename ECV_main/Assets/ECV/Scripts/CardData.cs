using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using Unity.Mathematics;
using System.Net.NetworkInformation;
using UnityEngine;

/// <summary>
/// 効果を適用した後のカードデータ。
/// </summary>
public class EffectedCardData{
    public string Id { get => Prefix + Number.ToString();}
    public string Prefix{ get; set; } = "T";
    public string Name{ get; set; } = "";
    public int Number { get; set; } = 0;
    public string Node { get; set; } = "";
    public string Cost { get; set; } = "";
    public string Attack { get; set; } = "";
    public string Toughness { get; set; } = "";
    public string Graze { get; set; } = "";
    public string Text { get; set; } = "";
    public string User { get; set; } = "";
    public string Comment { get; set; } = "";
    public List<CardRace> Solidarity { get; set; } = new List<CardRace>();
    public string Popularity4 { get; set; } = "";
    public string Popularity2 { get; set; } = "";
    public string Popularity1 { get; set; } = "";
    public string Popularity0 { get; set; } = "";

    public CardPlayType PlayType { get; set; } = CardPlayType.None;
    public CardEffectDuration Duration = CardEffectDuration.None;
    public CardEffectRange Range = CardEffectRange.None;

    public List<CardRace> races = new();

    public bool HasRace(CardRace race){
        return races.Contains(race);
    }

    public EffectedCardData(OriginCardData card){
        Prefix = card.Prefix;
        Name = card.Name;
        Number = card.Number;
        Node = card.Node;
        Cost = card.Cost;
        Attack = card.Attack;
        Toughness = card.Toughness;
        Graze = card.Graze;
        Text = card.Text;
        User = card.User;
        Comment = Comment;
        Popularity4 = card.Popularity4;
        Popularity2 = card.Popularity2;
        Popularity1 = card.Popularity1;
        Popularity0 = card.Popularity0;
        Solidarity = card.Solidarity;
        PlayType = card.PlayType;
        Duration = card.Duration;
        Range = card.Range;
        races = card.GetCardRaces();
    }
}

/// <summary>
/// 本来の記述とカードの効果、かかっている効果やセットされたカードなどの、1つのカードに関わるデータすべてを記録するクラス。
/// </summary>
public class CardData
{
    //本来の記述
    public OriginCardData origin;

    //装備
    public CardData equipment = null;

    //呪符
    public List<CardData> enchantments = new();

    //その他セットされたカード
    public List<CardData> otherSetCards = new();

    //付与された効果
    public List<CardEffect> effects = new();

    public EffectedCardData EffectedCard{
        get {
            EffectedCardData card = new(origin);

            foreach(var effect in effects){
                card = effect.Effect(card, false);
            }

            return card;
        }
    }

    int Vstr2Int(string vText, int NaN){
        if(vText == "X" || vText == "-" || vText == ""){
            return NaN;
        }

        return int.Parse(vText);
    }

    public int GetNodeNumber(int NaN = 0){
        return Vstr2Int(EffectedCard.Node, NaN);
    }

    public int GetCostNumber(int NaN = 0){
        return Vstr2Int(EffectedCard.Cost, NaN);
    }

    public int GetGrazeNumber(int NaN = 0){
        return Vstr2Int(EffectedCard.Graze, NaN);
    }

    public int GetAttackNumber(int NaN = 0){
        return Vstr2Int(EffectedCard.Attack, NaN);
    }

    public int GetToughnessNumber(int NaN = 0){
        return Vstr2Int(EffectedCard.Toughness, NaN);
    }

    public string Name {
        get {
            return EffectedCard.Name;
        }
    }

    public string Text{
        get {
            return EffectedCard.Text;
        }
    }

    public string CardId { get => origin.Id;}

    public CardData(int no, string prefix){
        origin = new OriginCardData(no, prefix);
    }

    public CardData(string id){
        string prefix = id[..1];
        int no = int.Parse(id[1..]);
        origin = new OriginCardData(no, prefix);
    }
}
