using System;
using System.Collections;
using System.Collections.Generic;
using Mono.CompilerServices.SymbolWriter;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public enum CardEffectType{
    None,AutoAlpha,AutoBeta,AutoGamma,Action
}

public class CardEffect
{
    public CardData Source{ get; private set; }
    public CardEffectType Type { get; private set; }
    public Func<bool> Condition{ get; private set; }
    public UnityEvent<CardData> Effect{ get; private set; }
}
