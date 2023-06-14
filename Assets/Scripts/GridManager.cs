using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private Card revealedCard;
    [SerializeField]
    private float cardDeleteTimer;
    [SerializeField]
    private float cardHideTimer;
    [SerializeField]
    private List<Card> cardsToPair;

    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private AudioController audioController;

    private bool canClick;
    private float internalTimer;

    private void Start()
    {
        canClick = true;
    }

    private void Update()
    {
        if (!canClick)
        {
            internalTimer -= Time.deltaTime;
            if (internalTimer <= 0.0f)
            {
                canClick = true;
            }
        }
    }

    // Set card as revealed
    public void RevealCard(Card cardToReveal)
    {
        if (canClick)
        {
            if (revealedCard == null)
            {
                // Show card
                cardToReveal.RevealCard(true);
                revealedCard = cardToReveal;
            }
            else
            {
                // Show card
                cardToReveal.RevealCard(true);

                // Compare cards
                CompareRevealedCards(cardToReveal);
            }
        }
    }

    public void AddCards(List<Card> cards)
    {
        cardsToPair = cards;
    }

    private void CompareRevealedCards(Card newCard)
    {
        if (revealedCard.matchValue == newCard.matchValue)
        {
            // Is a match
            newCard.DeleteAfterXTime(cardDeleteTimer);
            revealedCard.DeleteAfterXTime(cardDeleteTimer);
            internalTimer = cardDeleteTimer;
            canClick = false;

            // Update cardsToPair
            cardsToPair.Remove(newCard);
            cardsToPair.Remove(revealedCard);

            // Are all cards matched?
            if (cardsToPair.Count == 0)
            {
                // Win
                gameManager.GameOver(true);
                audioController.Play("Click");
            }
        }
        else
        {
            // Not a match
            newCard.HideAfterXTimeCard(cardHideTimer);
            revealedCard.HideAfterXTimeCard(cardHideTimer);
            internalTimer = cardHideTimer;
            canClick = false;
            audioController.Play("Error");
        }
        revealedCard = null;
    }
}
