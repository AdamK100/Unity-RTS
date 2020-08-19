using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
public class Manager : MonoBehaviour
{
    public Text b_text;
    public GameObject desc_panel;
    public Button[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (b_text.enabled)
        {
            desc_panel.SetActive(true);
        }
        if (!b_text.enabled)
        {
            desc_panel.SetActive(false);
        }
        if (Selection.selected.ToArray().Length > 0)
        {            
            if(Selection.selected[0].GetComponent<Building>() != null)
            {
                b_text.enabled = Toolbox.isOverButtons(buttons);
                foreach (Button bu in buttons)
                {
                    if (Toolbox.isOverButton(bu))
                    {
                        Upgrade up = Selection.selected[0].GetComponent<Base>().BldgMenu[buttons.ToList().IndexOf(bu)];
                        if (!up.p.Researched.Contains(up.ID1))
                        {
                            b_text.text = up.desc1;
                        }
                        if (up.p.Researched.Contains(up.ID1) && (up.p.tech == TechLevel.t1920 || up.p.tech == TechLevel.t1930))
                        {
                            b_text.text = up.desc2;
                        }
                        if (up.p.Researched.Contains(up.ID2) && up.p.tech == TechLevel.t1930)
                        {
                            b_text.text = up.desc3;
                        }
                    }
                }
                foreach(Upgrade up in Selection.selected[0].GetComponent<Base>().BldgMenu)
                {
                    Button b = buttons[Selection.selected[0].GetComponent<Base>().BldgMenu.ToList().IndexOf(up)];
                    Image i = b.GetComponent<Image>();
                    if (Selection.selected[0] == up.Bldg)
                    {                        
                        if (up.ID1 != string.Empty || (up.p.Researched.Contains(up.ID1) && up.ID2 != string.Empty) || (up.p.Researched.Contains(up.ID2) && up.ID3 != string.Empty))
                        {
                            b.enabled = true;
                            i.enabled = true;
                        }                        
                        if (!up.p.Researched.Contains(up.ID1) && i.sprite != up.icon1)
                        {
                            i.sprite = up.icon1;
                        }
                        else
                        {
                            if (up.p.Researched.Contains(up.ID1) && (up.p.tech == TechLevel.t1920 || up.p.tech == TechLevel.t1930))
                            {
                                i.sprite = up.icon2;
                            }
                            if (up.p.Researched.Contains(up.ID2) && up.p.tech == TechLevel.t1930)
                            {
                                i.sprite = up.icon3;
                            }
                        }
                    }
                    else
                    {
                        b.enabled = false;
                        i.enabled = false;
                    }
                }

            }
        }
        else
        {
            b_text.enabled = false;
            foreach(Button b in buttons)
            {
                b.enabled = false;
                b.GetComponent<Image>().enabled = false;
            }
        }
        foreach(Building b in FindObjectsOfType<Building>())
        {
            foreach(Upgrade u in b.GetComponent<Base>().BldgMenu)
            {
                if (u.started1)
                {
                    u.progress1 += Time.deltaTime;
                    if (u.progress1 >= u.researchTime1)
                    {
                        u.Complete(u.ID1);
                        u.started1 = false;
                    }
                }
                if (u.started2)
                {
                    u.progress2 += Time.deltaTime;
                    if (u.progress2 >= u.researchTime1)
                    {
                        u.Complete(u.ID2);
                        u.started2 = false;
                    }
                }
            }
        }
    }
}
