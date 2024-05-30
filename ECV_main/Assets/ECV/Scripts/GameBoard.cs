using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームのUI、カードの移動などグラフィックに強く影響を与える操作を行うクラス。
/// </summary>
public class GameBoard : MonoBehaviour
{
    public static GameBoard Instance;

    [SerializeField] GameObject cardPrefab;
    public CardDataView cardDataView;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public void HideCardDataView()
    {
        cardDataView.gameObject.SetActive(false);
    }

    public void ShowCardDataView()
    {
        cardDataView.gameObject.SetActive(true);
    }
}
