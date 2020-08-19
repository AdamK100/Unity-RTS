using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Player : MonoBehaviour
{
    public float money = 5000;
    public Side side;
    public float rotationMultiplier;
    public GameObject moneyText;
    public Sprite symbol;
    public TechLevel tech;
    public List<string> Researched = new List<string>();
    void Update()
    {
        moneyText.GetComponent<TextMeshProUGUI>().text = money.ToString();
    }
}
public enum TechLevel
{
    t1910,
    t1920,
    t1930,
    t1940
}
public enum Side
{
    Allies,
    Soviets,
    Axis
}