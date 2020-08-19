using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthManager : MonoBehaviour
{
    public int healthBarHeight;
    public Canvas can;
    public Image hb;
    public Image hbOut;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Selection.selected.ToArray().Length != 0)
        {
            foreach(GameObject unit in Selection.selected)
            {
                if (unit.tag == "unit")
                {
                    Tank tank = unit.GetComponent<Tank>();
                    if (!tank.imageInstantiated)
                    {
                        tank.healthbar = Instantiate(hb);
                        tank.healthbar.transform.SetParent(can.transform);
                        tank.healthBarOutline = Instantiate(hbOut);
                        tank.healthBarOutline.transform.SetParent(can.transform);
                        tank.healthbar.enabled = true;
                        tank.healthBarOutline.enabled = true;
                        tank.imageInstantiated = true;

                    }
                    tank.healthBarOutline.rectTransform.position = new Vector2(Camera.main.WorldToScreenPoint(unit.transform.position).x, Camera.main.WorldToScreenPoint(unit.transform.position).y + healthBarHeight);
                    tank.healthBarOutline.rectTransform.sizeDelta = new Vector2(tank.pixelDiameter / 2, 5);
                    tank.healthbar.rectTransform.sizeDelta = new Vector2(((tank.pixelDiameter / 2) - 2) * tank.health / tank.maxHealth, 3);
                    tank.healthbar.rectTransform.position = new Vector2(Camera.main.WorldToScreenPoint(unit.transform.position).x - ((tank.pixelDiameter / 2) - 2) / 2 + tank.healthbar.rectTransform.sizeDelta.x / 2, tank.healthBarOutline.rectTransform.position.y);
                }
                if(unit.GetComponent<Building>() != null)
                {
                    Building b = unit.GetComponent<Building>();
                    if (!b.imageInstantiated)
                    {
                        b.healthbar = Instantiate(hb);
                        b.healthbar.transform.SetParent(can.transform);
                        b.healthBarOutline = Instantiate(hbOut);
                        b.healthBarOutline.transform.SetParent(can.transform);
                        b.healthbar.enabled = true;
                        b.healthBarOutline.enabled = true;
                        b.imageInstantiated = true;

                    }
                    b.healthBarOutline.rectTransform.position = new Vector2(Camera.main.WorldToScreenPoint(unit.transform.position).x, Camera.main.WorldToScreenPoint(unit.transform.position).y + healthBarHeight);
                    b.healthBarOutline.rectTransform.sizeDelta = new Vector2(b.pixelDiameter / 2, 5);
                    b.healthbar.rectTransform.sizeDelta = new Vector2(((b.pixelDiameter / 2) - 2) * b.health / b.maxHealth, 3);
                    b.healthbar.rectTransform.position = new Vector2(Camera.main.WorldToScreenPoint(unit.transform.position).x - ((b.pixelDiameter / 2) - 2) / 2 + b.healthbar.rectTransform.sizeDelta.x / 2, b.healthBarOutline.rectTransform.position.y);
                }
            }
        }  
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("unit"))
        {
            if(obj.GetComponent<Tank>().imageInstantiated && !Selection.selected.Contains(obj))
            {
                Destroy(obj.GetComponent<Tank>().healthbar);
                Destroy(obj.GetComponent<Tank>().healthBarOutline);
                obj.GetComponent<Tank>().imageInstantiated = false;
            }
            if (obj.GetComponent<Tank>().healthbar != null)
            {
                if (obj.GetComponent<Tank>().health > 0.5 * obj.GetComponent<Tank>().maxHealth)
                {
                    obj.GetComponent<Tank>().healthbar.color = Color.green;
                }
                if (obj.GetComponent<Tank>().health < 0.25 * obj.GetComponent<Tank>().maxHealth)
                {
                    obj.GetComponent<Tank>().healthbar.color = Color.red;
                }
                if (obj.GetComponent<Tank>().health > 0.25 * obj.GetComponent<Tank>().maxHealth && obj.GetComponent<Tank>().health < 0.5 * obj.GetComponent<Tank>().maxHealth)
                {
                    obj.GetComponent<Tank>().healthbar.color = Color.yellow;
                }
            }
        }
        foreach(Building obj in FindObjectsOfType<Building>())
        {
            if (obj.imageInstantiated && !Selection.selected.Contains(obj.gameObject))
            {
                Destroy(obj.healthbar);
                Destroy(obj.healthBarOutline);
                obj.imageInstantiated = false;
            }
            if (obj.healthbar != null)
            {
                if (obj.health > 0.5 * obj.maxHealth)
                {
                    obj.healthbar.color = Color.green;
                }
                if (obj.health < 0.25 * obj.maxHealth)
                {
                    obj.healthbar.color = Color.red;
                }
                if (obj.health > 0.25 * obj.maxHealth && obj.health < 0.5 * obj.maxHealth)
                {
                    obj.healthbar.color = Color.yellow;
                }
            }
        }
    }
}
