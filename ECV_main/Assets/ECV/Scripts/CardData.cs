using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using Unity.Mathematics;
using System.Net.NetworkInformation;
using UnityEngine;

public class TempCardData{
    public string Id { get => Prefix + Number.ToString();}
    public string Prefix{ get; set; } = "T";
    public string Name{ get; set; } = "";
    public int Number { get; set; } = 0;
    public string Node { get; set; } = "-";
    public string Cost { get; set; } = "-";
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
    CardRace race1 = CardRace.None;
    CardRace race2 = CardRace.None;

    public bool HasRace(CardRace race){
        if(race == CardRace.None){
            return race1 == race;
        }

        return race1 == race || race2 == race;
    }

    public TempCardData(OriginCardData card){
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
    }
}

public class CardData
{
    //本来の記述
    public OriginCardData origin;

    //装備
    public CardData equipment;

    //呪符
    public List<CardData> enchantments;

    //付与された効果
    public List<CardEffect> effects;

    public string GetName(){
        TempCardData card = new(origin);

        foreach(var effect in effects){
            card = effect.Effect(card);
        }

        return card.Name;
    }

    public int GetNode(){
        return 0;
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
