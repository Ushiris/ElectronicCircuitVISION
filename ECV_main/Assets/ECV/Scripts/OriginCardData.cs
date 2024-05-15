using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;


public class OriginCardData
{
    public string Id { get => Prefix + Number.ToString();}
    public string Prefix{ get; private set; } = "N";
    public string Name{ get; private set; } = "";
    public int Number { get; private set; } = 0;
    public string Node { get; private set; } = "-";
    public string Cost { get; private set; } = "-";
    public string Attack { get; private set; } = "";
    public string Toughness { get; private set; } = "";
    public string Graze { get; private set; } = "";
    public string Text { get; private set;} = "";
    public string User { get; private set;} = "";
    public string Comment { get; private set;} = "";
    public List<CardRace> Solidarity { get; private set; } = new List<CardRace>();
    public string Popularity4 { get; private set; } = "";
    public string Popularity2 { get; private set; } = "";
    public string Popularity1 { get; private set; } = "";
    public string Popularity0 { get; private set; } = "";

    public CardPlayType PlayType{ get; private set;} = CardPlayType.None;
    public CardEffectDuration Duration = CardEffectDuration.None;
    public CardEffectRange Range = CardEffectRange.None;
    CardRace race1 = CardRace.None;
    CardRace race2 = CardRace.None;

    public OriginCardData(int num, string prefix = "T"){
        LoadData(num, prefix);
    }

    void LoadData(int num, string prefix = "T"){
        var data = CardDataList.Instance.GetCard(prefix + num.ToString());

        Name = Regex.Replace(data.name, @"PR\.\d+", "").Trim();
        Number = data.no;
        Prefix = data.prefix;
        Node = data.node;
        Cost = data.cost;
        Attack = data.attack;
        Toughness = data.toughness;
        Graze = data.graze;
        Comment = data.text;
        User = data.user;

        PlayType = CardDataCoverter.StrToCardPlayType(data.type);
        Duration = CardDataCoverter.StrToCardEffectDuration(data.time);
        Range = CardDataCoverter.StrToCardEffectRange(data.range);
        SetRaces(data._class);

        if(data.ability.StartsWith("《代替レース》")){
            var temp = data.ability.Split("\n")[0];
            temp = temp.Replace("《代替レース》", "");
            var races = temp.Split("　");
            var result = new List<CardRace>();
            foreach(var race in races){
                int amount = 1;
                string r = "";
                if(race.Contains("×３")){
                    amount = 3;
                    r = race.Replace("×３", "");
                }else if(race.Contains("×２")){
                    amount = 2;
                    r = race.Replace("×２", "");
                }
                for(int i = 0;i < amount;i++){
                    result.Add(CardDataCoverter.StrToCardRace(r)[0]);
                }
            }
            data.ability = data.ability.Replace(temp + "\\n", "");
        }

        data.text = data.skill.Replace("Union", "").Trim();
        if(data.upkeep != ""){
            data.text += " 維持コスト：" + data.upkeep;
        }
        data.text += "\n";

        if(data.ability.Contains("（人気爆発）")){
            string[] texts = data.ability.Split(new string[] { "\\n" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < texts.Length; i++)
            {
                if (texts[i].Contains("人気：４"))
                    Popularity4 += texts[i + 1];
                else if (texts[i].Contains("人気２～４"))
                    Popularity2 += texts[i + 1];
                else if (texts[i].Contains("人気：０"))
                    Popularity0 += texts[i + 1];
                else if (texts[i].Contains("人気：１～４"))
                    Popularity1 += texts[i + 1];
                else if(!texts[i-1].Contains("人気：４")&&!texts[i-1].Contains("人気２～４")&&!texts[i-1].Contains("人気：０")&&!texts[i-1].Contains("人気：１～４"))
                    data.text += texts[i];
            }
        }
        else
        {
            data.text += data.ability;
        }
    }

    public bool HasRace(CardRace race){
        if(race == CardRace.None){
            return race1 == race;
        }

        return race1 == race || race2 == race;
    }

    public List<CardRace> GetCardRaces(){
        if(race1 == CardRace.None){
            return new List<CardRace>();
        }

        if(race2 == CardRace.None){
            return new List<CardRace>(){race1};
        }

        return new List<CardRace>(){race1, race2};
    }

    public void SetRaces(string races){
        var temp = CardDataCoverter.StrToCardRace(races);

        if (temp.Count == 0){
            race1 = CardRace.None;
            race2 = CardRace.None;
            return;
        }
        if (temp.Count == 1){
            race1 = temp[0];
            race2 = CardRace.None;
            return;
        }
        race1 = temp[0];
        race2 = temp[1];
    }
    
}