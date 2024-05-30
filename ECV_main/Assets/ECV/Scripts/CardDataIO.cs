using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class RawCardData
{
    public string prefix = "T";
    public int no = 0;
    public string type = "";
    public string graze = "";
    public string node = "-";
    public string cost = "-";
    public string range = "";
    public string time = "";
    public string user = "";
    public string name = "";
    public string _class = "";
    public string skill = "";
    public string upkeep = "";
    public string ability = "";
    public string attack = "";
    public string toughness;
    public string text;
    public string illustration;
    public string file;
    public List<string> package;
}

public enum CardPlayType
{
    CharacterCard, SpellCard, CommandCard, None,
}
public enum CardEffectRange
{
    Target, AOE, Player, MaltiEffect, Other, None
}
public enum CardEffectDuration
{
    Flash, Long, Equipment, Enchant, Ruler, None
}
public enum CardRace
{
    Human, Yokai, Fairy, Sorcerer, Kappa, Vampire, Makaijin, Ghost, Tengu, Reaper,
    Enma, God, Beast, Ogre, CelestialMaiden, Dragon, Hsien, Dwarf, None
}

public static class CardDataConverter
{
    public static string CardPlayTypeToViewName(CardPlayType type)
    {
        return type switch
        {
            CardPlayType.CharacterCard => "Character",
            CardPlayType.SpellCard => "Spell",
            CardPlayType.CommandCard => "Command",
            CardPlayType.None => "",
            _ => "Error!"
        };
    }

    public static string CardEffectRangeToViewName(CardEffectRange type)
    {
        return type switch
        {
            CardEffectRange.Target => "目標のカードに及ぶ効果（十字架のマーク）",
            CardEffectRange.Player => "プレイヤー、手札、デッキ、冥界に及ぶ効果（星のマーク）",
            CardEffectRange.AOE => "目標を取らず、複数のカードに及ぶ効果（ぐるぐるのマーク）",
            CardEffectRange.MaltiEffect => "複数の効果を持つカード",
            CardEffectRange.None => "",
            _ => "Error!"
        };
    }

    public static string CardEffectDurationToViewName(CardEffectDuration type)
    {
        return type switch
        {
            CardEffectDuration.Flash => "瞬間",
            CardEffectDuration.Long => "持続",
            CardEffectDuration.Equipment => "装備、装備／場",
            CardEffectDuration.Enchant => "呪符",
            CardEffectDuration.Ruler => "世界呪符",
            CardEffectDuration.None => "",
            _ => "Error!"
        };
    }

    public static string CardRaceToViewName(CardRace type)
    {
        return type switch
        {
            CardRace.Human => "人間",
            CardRace.Yokai => "妖怪",
            CardRace.Fairy => "妖精",
            CardRace.Sorcerer => "魔法使い",
            CardRace.Kappa => "河童",
            CardRace.Vampire => "吸血鬼",
            CardRace.Makaijin => "魔界人",
            CardRace.Ghost => "幽霊",
            CardRace.Tengu => "天狗",
            CardRace.Reaper => "死神",
            CardRace.Enma => "閻魔",
            CardRace.God => "神",
            CardRace.Beast => "獣",
            CardRace.Ogre => "鬼",
            CardRace.CelestialMaiden => "天人",
            CardRace.Dragon => "龍",
            CardRace.Hsien => "仙人",
            CardRace.Dwarf => "小人",
            CardRace.None => "なし",
            _ => "Error!"
        };
    }

    public static string CardRacesToViewName(List<CardRace> cardRaces){
        var r = "";
        foreach(var race in cardRaces){
            r += " " + CardRaceToViewName(race);
        }

        return r;
    }

    public static CardPlayType StrToCardPlayType(string str)
    {
        return str switch {
            "Character" => CardPlayType.CharacterCard,
            "Spell" => CardPlayType.SpellCard,
            "Command" => CardPlayType.CommandCard,
            "character" => CardPlayType.CharacterCard,
            "spell" => CardPlayType.SpellCard,
            "command" => CardPlayType.CommandCard,
            "cha" => CardPlayType.CharacterCard,
            "spe" => CardPlayType.SpellCard,
            "com" => CardPlayType.CommandCard,
            _ => CardPlayType.None
        };
    }

    public static CardEffectRange StrToCardEffectRange(string str)
    {
        return str switch{
            "目標のカードに及ぶ効果" => CardEffectRange.Target,
            "プレイヤー、手札、デッキ、冥界に及ぶ効果" => CardEffectRange.Player,
            "目標を取らず、複数のカードに及ぶ効果" => CardEffectRange.AOE,
            "複数の効果を持つカード" => CardEffectRange.MaltiEffect,
            "目標" => CardEffectRange.Target,
            "星" => CardEffectRange.Player,
            "対象" => CardEffectRange.AOE,
            "複数" => CardEffectRange.MaltiEffect,
            _ => CardEffectRange.None
        };
    }

    public static CardEffectDuration StrToCardEffectDuration(string str)
    {
        return str switch{
            "瞬間" => CardEffectDuration.Flash,
            "持続" => CardEffectDuration.Long,
            "装備／場" => CardEffectDuration.Equipment,
            "装備" => CardEffectDuration.Equipment,
            "呪符" => CardEffectDuration.Enchant,
            "世界呪符" => CardEffectDuration.Ruler,
            _ => CardEffectDuration.None
        };
    }

    public static List<CardRace> StrToCardRace(string str)
    {
        var races = new Dictionary<string, CardRace>(){
            {"人間", CardRace.Human},
            {"妖怪", CardRace.Yokai},
            {"妖精", CardRace.Fairy},
            {"魔法使い", CardRace.Sorcerer},
            {"河童", CardRace.Kappa},
            {"吸血鬼", CardRace.Vampire},
            {"魔界人", CardRace.Makaijin},
            {"幽霊", CardRace.Ghost},
            {"天狗", CardRace.Tengu},
            {"死神", CardRace.Reaper},
            {"閻魔", CardRace.Enma},
            {"神", CardRace.God},
            {"獣", CardRace.Beast},
            {"鬼", CardRace.Ogre},
            {"天人", CardRace.CelestialMaiden},
            {"龍", CardRace.Dragon},
            {"仙人", CardRace.Hsien},
            {"小人", CardRace.Dwarf}
        };

        List<CardRace> ret = new(){};
        foreach (var race in races){
            if(str.Contains(race.Key)){
                ret.Add(race.Value);
                str.Replace(race.Key, "");
            }
        }

        return ret;
    }

}

public class CardDataList
{
    static CardDataList instance;
    Dictionary<string, RawCardData> cardList;
    Dictionary<string, RawCardData> CardList{
        get{
            if (cardList == null) Init();
            return cardList;
        }
        set => cardList = value;
    }

    public RawCardData GetCard(string id){
        CardList.TryGetValue(id, out RawCardData data);
        return data;
    }

    public static CardDataList Instance {
        get {
            instance ??= new CardDataList();
            return instance;
        }
    }

    static void Init()
    {
        _ = Instance;

        // JSONファイルの読み込み
        string jsonString = Resources.Load<TextAsset>("CardList").text;

        // JSONデータをデシリアライズしてDictionaryに格納
        instance.CardList = JsonConvert.DeserializeObject<Dictionary<string, RawCardData>>(jsonString);
    }
}