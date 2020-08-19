using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class ButtonScript : MonoBehaviour
{
    public Player p;
    public void OnClick()
    {
        if (!UI.isBuilding)
        {
            if(p.tech == TechLevel.t1910)
            {
                Selection.selected[0].GetComponent<Base>().BldgMenu[p.GetComponent<Manager>().buttons.ToList().IndexOf(this.gameObject.GetComponent<Button>())].started1 = true;               
            }
            if ((p.tech == TechLevel.t1920 || p.tech ==  TechLevel.t1930) && p.Researched.Contains(Selection.selected[0].GetComponent<Base>().BldgMenu[p.GetComponent<Manager>().buttons.ToList().IndexOf(this.gameObject.GetComponent<Button>())].ID1))
            {
                Selection.selected[0].GetComponent<Base>().BldgMenu[p.GetComponent<Manager>().buttons.ToList().IndexOf(this.gameObject.GetComponent<Button>())].started2 = true;

            }
            if (p.tech == TechLevel.t1930 && p.Researched.Contains(Selection.selected[0].GetComponent<Base>().BldgMenu[p.GetComponent<Manager>().buttons.ToList().IndexOf(this.gameObject.GetComponent<Button>())].ID2))
            {
                Selection.selected[0].GetComponent<Base>().BldgMenu[p.GetComponent<Manager>().buttons.ToList().IndexOf(this.gameObject.GetComponent<Button>())].started3 = true;
            }
        }
    }
}
