using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
