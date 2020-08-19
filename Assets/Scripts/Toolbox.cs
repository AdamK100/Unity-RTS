using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using System.Linq;
using UnityEngine.UI;
public class Toolbox : MonoBehaviour
{
    public static bool isOverButton(Button b)
    {
        Vector2 p1 = new Vector2(b.GetComponent<RectTransform>().position.x - b.GetComponent<RectTransform>().sizeDelta.x / 2, b.GetComponent<RectTransform>().position.y - b.GetComponent<RectTransform>().sizeDelta.y / 2);
        Vector2 p2 = new Vector2(b.GetComponent<RectTransform>().position.x + b.GetComponent<RectTransform>().sizeDelta.x / 2, b.GetComponent<RectTransform>().position.y + b.GetComponent<RectTransform>().sizeDelta.y / 2);
        return (RectContains(p1, p2, Input.mousePosition) && b.GetComponent<Image>().enabled);
    }
    public static bool isOverButtons(Button[] b)
    {
        int c = 0;
        foreach(Button bt in b)
        {
            if (isOverButton(bt))
            {
                c += 1;
            }
        }
        return c > 0;
    }
    public static bool RectContains(Vector2 firstCorner, Vector2 secondCorner, Vector2 point)
    {
        bool value = false;
        if( 
            (point.x > firstCorner.x && point.x < secondCorner.x && point.y > firstCorner.y && point.y < secondCorner.y) ||
            (point.x > firstCorner.x && point.x < secondCorner.x && point.y < firstCorner.y && point.y > secondCorner.y) ||
            (point.x < firstCorner.x && point.x > secondCorner.x && point.y > firstCorner.y && point.y < secondCorner.y) ||
            (point.x < firstCorner.x && point.x > secondCorner.x && point.y < firstCorner.y && point.y > secondCorner.y) 
          )
        {
            value = true;
        }
        return value;
    }
    public static GameObject ReturnMostCommon(List<GameObject> selected)
    {
        GameObject returnvalue = selected.ToArray()[0];
        List<GameObject> OriginalSelected = selected.ToArray().ToList();
        List<int> occurences = new List<int>();
        List<string> substrings = new List<string>();
        foreach(GameObject g in OriginalSelected.ToArray().ToList())
        {
            int oc = 1;
            foreach(GameObject obj in OriginalSelected.ToArray().ToList())
            {
                if(g.name.Substring(0,3) == obj.name.Substring(0, 3))
                {
                    oc += 1;
                    OriginalSelected.Remove(obj);
                }
            }
            substrings.Add(g.name.Substring(0, 3));
            OriginalSelected.Remove(g);
            occurences.Add(oc);
        }
        bool tookvalue = false;
        foreach(GameObject unit in selected)
        {
            if(unit.name.Substring(0,3) == substrings.ToArray()[occurences.IndexOf(occurences.Max())] && tookvalue == false)
            {
                returnvalue = unit;
                tookvalue = true;
            }
        }
        return returnvalue;
    }
    public static GameObject FindNearest(Vector3 Position, GameObject[] objects)
    {
        List<float> distances = new List<float>();
        foreach (GameObject obj in objects)
        {
            distances.Add(Vector3.Distance(Position, obj.transform.position));
        }
        return objects[distances.IndexOf(distances.Min())];
    }
    public static GameObject[] GoldToGameObjects(Gold[] gold)
    {
        List<GameObject> objects = new List<GameObject>();
        foreach(Gold ore in gold)
        {
            objects.Add(ore.gameObject);
        }
        return objects.ToArray();
    }
    public static void ChangeAlpha(Material mat, float alphaValue)
    {
        Color oldColor = mat.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaValue);
        mat.SetColor("_Color", newColor);
    }
}
