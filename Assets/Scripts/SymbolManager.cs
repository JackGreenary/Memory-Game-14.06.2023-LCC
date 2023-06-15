using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SymbolManager : MonoBehaviour
{
    [SerializeField]
    private Symbol symbolPrefab;
    private List<Symbol> symbols;

    public void DisplaySymbols(List<string> symbolStrings)
    {
        symbols = new List<Symbol>();

        // Get unique values (cannot be repeated characters)
        symbolStrings = symbolStrings.Distinct().ToList();

        foreach (string symbol in symbolStrings)
        {
            Symbol symbolObj = Instantiate(symbolPrefab.gameObject, gameObject.transform).GetComponent<Symbol>();
            symbolObj.value.text = symbol;
            symbols.Add(symbolObj);
        }
    }

    public void UpdateSymbol(string symbol)
    {
        Symbol symbolFound = symbols.Where(i => i.value.text == symbol).FirstOrDefault();
        symbolFound.value.color = Color.red;
    }
}
