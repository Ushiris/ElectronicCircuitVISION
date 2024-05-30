using System;
using System.Collections;
using System.Collections.Generic;
using Mono.CompilerServices.SymbolWriter;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public enum CardEffectType{
    None,AutoAlpha,AutoBeta,AutoGamma,ActionAnyTiming,ActionYourTurn,ActionOpponentTurn
}

/// <summary>
/// カードの効果を定義するクラス。効果内容の分類は後で考える
/// </summary>
public class CardEffect
{
    public CardData Source{ get; private set; }
    public CardEffectType Type { get; private set; }
    public Func<EffectedCardData, bool, EffectedCardData> Effect{ get; private set; }

    public string Text { get; private set; }

    public CardEffect(CardData card, CardEffectType effectType, Func<EffectedCardData, bool, EffectedCardData> effect, string text){
        Source = card;
        Type = effectType;
        Effect = effect;
        Text = text;
    }
}
