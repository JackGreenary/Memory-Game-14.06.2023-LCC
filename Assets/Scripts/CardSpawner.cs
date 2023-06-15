using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField]
    private List<int> matchValues = new List<int>();
    [SerializeField]
    private GameObject cardPrefab;
    [SerializeField]
    private bool spawnCards;

    [SerializeField]
    private GridManager gridManager;
    [SerializeField]
    private SymbolManager symbolManager;

    void Start()
    {
        if (spawnCards)
        {
            SpawnCards();
            spawnCards = false;
        }
    }

    private void SpawnCards()
    {
        int i = 0;
        List<Card> cards = new List<Card>();
        List<string> symbols = new List<string>();

        // Set match values
        matchValues = new List<int>{ 0, 1, 2, 3, 4, 5 };

        // Double for pairs
        matchValues.AddRange(matchValues);
        // Shuffle list
        matchValues = matchValues.OrderBy(i => Guid.NewGuid()).ToList();

        foreach (int matchVal in matchValues)
        {
            string symbol = GetText(matchVal);
            symbols.Add(symbol);

            Card card = Instantiate(cardPrefab, gridManager.transform).GetComponent<Card>();
            card.Init(i, symbol, symbol, gridManager);
            cards.Add(card);
            i++;
        }
        gridManager.AddCards(cards);
        symbolManager.DisplaySymbols(symbols);
    }

    private void Setup()
    {
        matchValues = new List<int>();
        SpawnCards();
        gridManager.Clear();
    }

    private string GetText(int matchVal)
    {
        switch (matchVal)
        {
            case 0:
                return "A";
            case 1:
                return "B";
            case 2:
                return "C";
            case 3:
                return "D";
            case 4:
                return "E";
            case 5:
                return "F";
            case 6:
                return "G";
            default:
                return "";
        }
    }
}
