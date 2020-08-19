using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Upgrade : MonoBehaviour
{
    public GameObject Bldg;
    public Player p;
    public bool isUnit;
    public Button b;
    public Image i;

    public GameObject Building1;
    public GameObject Infantry1;
    public GameObject Tank1;
    public string desc1;
    public float price1;
    public Sprite icon1;      
    public int researchTime1;
    public string ID1;
    public bool started1;
    public float progress1;

    public GameObject Building2;
    public GameObject Infantry2;
    public GameObject Tank2;
    public string desc2;
    public float price2;
    public Sprite icon2;
    public int researchTime2;
    public string ID2;
    public bool started2;
    public float progress2;

    public GameObject Building3;
    public GameObject Infantry3;
    public GameObject Tank3;
    public string desc3;
    public float price3;
    public Sprite icon3;
    public int researchTime3;
    public string ID3;
    public bool started3;
    public float progress3;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {       

    }
    public void Complete(string ID)
    {
        if(ID == "B11" || ID == "B21" || ID == "B31" || ID == "B41" || ID == "B51" || ID == "B61")
        {
            Debug.Log("Okay");
            p.money -= price1;            
            UI.activebuilding = Instantiate(Building1);
            UI.isBuilding = true;
        }
        if (ID == "B12" || ID == "B22" || ID == "B32" || ID == "B42" || ID == "B52" || ID == "B62")
        {

        }
        if (ID == "B13" || ID == "B23" || ID == "B33" || ID == "B43" || ID == "B53" || ID == "B63")
        {

        }
        if (!isUnit)
        {
            p.Researched.Add(ID);
        }
    }
}
