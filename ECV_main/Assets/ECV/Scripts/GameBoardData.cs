using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardTruthUser{
    You, Opponent
}

public enum CardArea{
    Outside, Play, Y_hand, Y_deck, Y_nether, Y_side, Y_node, O_hand, O_deck, O_nether, O_side, O_node
}

public class GameBoardDataRow{
    public Card card;
    public CardArea area;
}

public class GameBoardData
{
    public static GameBoardData _instance;
    public static GameBoardData Instance{
        get {
            _instance ??= new GameBoardData();
            return _instance;
        }
    }

    private GameBoardData(){}

    public List<GameBoardDataRow> Field{ get; private set;} = new();
}
