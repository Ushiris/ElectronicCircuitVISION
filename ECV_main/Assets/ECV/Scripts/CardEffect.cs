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

public class CardEffect
{
    public CardData Source{ get; private set; }
    public CardEffectType Type { get; private set; }
    public Func<EffectedCardData, EffectedCardData> Effect{ get; private set; }

    public string Text { get; private set; }

    public CardEffect(CardData card, CardEffectType effectType, Func<EffectedCardData, EffectedCardData> effect, string text){
        Source = card;
        Type = effectType;
        Effect = effect;
        Text = text;
    }
}
