using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Shop : MonoBehaviour
{
    static Shop instance;
    public List<ShopItem> possibleItems = new List<ShopItem>();

    private readonly Image[] itemIcons = new Image[3];
    private readonly TextMeshProUGUI[] itemEffects = new TextMeshProUGUI[3];
    private readonly Action[] effects = new Action[3];

    private bool choosen = false;

    public bool Choosen
    {
        get => choosen;
    }
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        itemIcons[0] = GameObject.Find("Shop/ShopItem/Image").GetComponent<Image>();
        itemIcons[1] = GameObject.Find("Shop/ShopItem1/Image").GetComponent<Image>();
        itemIcons[2] = GameObject.Find("Shop/ShopItem2/Image").GetComponent<Image>();

        itemEffects[0] = GameObject.Find("Shop/ShopItem/Text").GetComponent<TextMeshProUGUI>();
        itemEffects[1] = GameObject.Find("Shop/ShopItem1/Text").GetComponent<TextMeshProUGUI>();
        itemEffects[2] = GameObject.Find("Shop/ShopItem2/Text").GetComponent<TextMeshProUGUI>();

        gameObject.SetActive(false);
    }

    public void ShowShop()
    {
        int[] items = DrawThreeRandom();
        PutItemInSlot(0, possibleItems[items[0]]);
        PutItemInSlot(1, possibleItems[items[1]]);
        PutItemInSlot(2, possibleItems[items[2]]);
        choosen = false;
        gameObject.SetActive(true);
    }

    private int[] DrawThreeRandom()
    {
        int[] rand = new int[3];
        rand[0] = Random.Range(0, possibleItems.Count);
        do
        {
            rand[1] = Random.Range(0, possibleItems.Count);
        } while (rand[1] == rand[0]);
        do
        {
            rand[2] = Random.Range(0, possibleItems.Count);
        } while (rand[1] == rand[2] || rand[0] == rand[2]);
        return rand;
    }

    private void PutItemInSlot(uint slot, ShopItem item)
    {
        itemIcons[slot].sprite = item.texture;
        itemEffects[slot].text = item.text;
        effects[slot] = item.ApplyUpgrade;
    }

    public void OnButtonPressed(uint button)
    {
        effects[button].Invoke();
        gameObject.SetActive(false);
        choosen = true;
    }
    public void OnButtonPressed0()
    {
        OnButtonPressed(0);
    }
    public void OnButtonPressed1()
    {
        OnButtonPressed(1);
    }
    public void OnButtonPressed2()
    {
        OnButtonPressed(2);

    }

    public static Shop GetShop()
    {
        return instance;
    }
}
