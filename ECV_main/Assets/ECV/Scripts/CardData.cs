using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

public class CardData
{
    public OriginCardData origin = new();
    public CardData equipment;
    public string Id { get => origin.Id;}
    
}
