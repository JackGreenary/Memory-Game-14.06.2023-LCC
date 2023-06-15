using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int id;
    public string matchValue;
    public GridManager gridManager;

    [SerializeField]
    private Image background;

    [SerializeField]
    private Image front;

    [SerializeField]
    private Image highlight;

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private float hideTimer;
    [SerializeField]
    private bool hiding;
    [SerializeField]
    private float pairTimer;
    [SerializeField]
    private bool pairing;
    [SerializeField]
    private float revealTimer;
    [SerializeField]
    private bool revealing;
    [SerializeField]
    private float unRevealTimer;
    [SerializeField]
    private bool unRevealing;

    [SerializeField]
    private bool revealed;
    [SerializeField]
    private bool paired;

    public void Init(int Id, string Text, string MatchValue, GridManager GridManager)
    {
        id = Id;
        text.text = Text;
        matchValue = MatchValue;
        gridManager = GridManager;
    }

    private void Start()
    {
        text.gameObject.SetActive(false);
        front.gameObject.SetActive(false);
        highlight.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (pairing)
        {
            pairTimer -= Time.deltaTime;
            if (pairTimer <= 0.0f)
            {
                pairing = false;
                paired = true;
                //highlight.gameObject.SetActive(false);
                var tempColor = background.color;
                tempColor.a = .5f;
                background.color = tempColor;
            }
        }
        if (hiding)
        {
            hideTimer -= Time.deltaTime;
            if (hideTimer <= 0.0f)
            {
                hiding = false;
                HideCard();
                RevealCard(false);
            }
        }
        if (revealing)
        {
            revealTimer -= Time.deltaTime;
            if (revealTimer <= 0.0f)
            {
                revealing = false;
                front.gameObject.SetActive(true);
                text.gameObject.SetActive(true);
                revealed = true;
            }
        }
        if (!revealing && unRevealing)
        {
            unRevealTimer -= Time.deltaTime;
            if (unRevealTimer <= 0.0f)
            {
                unRevealing = false;
                front.gameObject.SetActive(false);
                text.gameObject.SetActive(false);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!revealed && !revealing && !paired)
            gridManager.RevealCard(this);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!revealing && !paired)
            background.color = Color.red;
        //background.color = new Color32(255, 174, 174, 255);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //if (!revealing && !paired)
            background.color = Color.black;
    }

    public void RevealCard(bool isRevealing)
    {
        if (isRevealing)
        {
            iTween.RotateTo(gameObject, iTween.Hash(
                "rotation", new Vector3(0, 180, 0),
                "time", .1f,
                "easetype", iTween.EaseType.linear
                ));
            revealTimer = .05f;
            revealing = true;
        }
        else
        {
            iTween.RotateTo(gameObject, iTween.Hash(
                "rotation", new Vector3(0, 0, 0),
                "time", .1f,
                "easetype", iTween.EaseType.linear
                ));
            unRevealTimer = .05f;
            unRevealing = true;
        }
    }

    public void HideCard()
    {
        highlight.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
        background.color = Color.black;
        revealed = false;
    }

    public void HideAfterXTimeCard(float setTimerTo)
    {
        hiding = true;
        hideTimer = setTimerTo;

        highlight.color = Color.red;
        //var tempColor = highlight.color;
        //tempColor.a = .3f;
        //highlight.color = tempColor;

        highlight.gameObject.SetActive(true);
    }

    public void DeleteAfterXTime(float setTimerTo)
    {
        pairing = true;
        pairTimer = setTimerTo;

        highlight.color = Color.green;
        //var tempColor = highlight.color;
        //tempColor.a = .3f;
        //highlight.color = tempColor;

        highlight.gameObject.SetActive(true);
    }
}
