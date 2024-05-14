using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardTruthUser{
    You, Opponent
}

public enum CardPossition{
    Outside, Play, Y_hand, Y_deck, Y_nether, Y_side, Y_node, O_hand, O_deck, O_nether, O_side, O_node
}

public class GameBoardDataRow{
    public CardData card;
    public CardTruthUser truthUser;
    public CardPossition possition;
}

public class GameBoard
{
    public static GameBoard _instance;
    public static GameBoard Instance{
        get {
            _instance ??= new GameBoard();
            return _instance;
        }
    }

    private GameBoard(){}

    public List<GameBoardDataRow> Field{ get; private set;} = new();

#pragma warning disable CS8632 // '#nullable' 注釈コンテキスト内のコードでのみ、Null 許容参照型の注釈を使用する必要があります。
    public List<CardData> GetCards(Func<GameBoardDataRow, bool>? filter = null){
        filter ??= card => true;

        List<CardData> result = new();
        foreach (GameBoardDataRow card in Field){
            if (filter(card)){
                result.Add(card.card);
            }
        }

        return result;
    }
#pragma warning restore CS8632 // '#nullable' 注釈コンテキスト内のコードでのみ、Null 許容参照型の注釈を使用する必要があります。
}
