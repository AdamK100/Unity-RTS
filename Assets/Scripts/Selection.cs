using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Selection : MonoBehaviour
{
    public static List<GameObject> selected = new List<GameObject>();
    RaycastHit hit;
    Vector2 oldPos = new Vector2(0, 0);
    Vector2 newPos = new Vector2(0,0);
    public Image selectionBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*string s = string.Empty;
        foreach(GameObject g in selected.ToArray())
        {
            s += " " + g.GetComponent<Tank>().pixelDiameter;
        }
        Debug.Log(s);*/
        

        //Selection
        if (Input.GetMouseButtonUp(1) && !CameraScript.scrolling && !UI.isBuilding)
        {
            selected.Clear();
        }
        if (Input.GetMouseButtonDown(0))
        {
            oldPos = Input.mousePosition;
            newPos = Input.mousePosition;
     
        }
        if (Input.GetMouseButton(0) && !UI.isBuilding)
        {
            if (!selectionBox.enabled)
            {
                selectionBox.enabled = true;
            }
            selectionBox.rectTransform.sizeDelta = new Vector2(Mathf.Abs(oldPos.x - Input.mousePosition.x), Mathf.Abs(oldPos.y - Input.mousePosition.y));
            selectionBox.rectTransform.position = new Vector2((oldPos.x + Input.mousePosition.x)/2 , (oldPos.y + Input.mousePosition.y)/2);
        }
        if (Input.GetMouseButtonUp(0)
            )
        {
            selectionBox.enabled = false;
            Vector2 a = new Vector2(0, 0);
            newPos = Input.mousePosition;           
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.collider.gameObject.tag == "unit")
                {
                    if (!Input.GetKey(KeyCode.LeftShift))
                    {
                        selected.Clear();
                    }                                                                                                              
                    if (Input.GetKey(KeyCode.LeftShift)) 
                    {
                        if (selected.Contains(hit.collider.gameObject))
                        {
                            selected.Remove(hit.collider.gameObject);
                        }
                        else
                        {
                            selected.Add(hit.collider.gameObject);
                        }
                    }
                    else
                    {
                        selected.Clear();
                        selected.Add(hit.collider.gameObject);
                    }
                }
                else
                {
                    if(hit.collider.GetComponent<Building>() != null && !UI.isBuilding)
                    {
                        selected.Clear();
                        selected.Add(hit.collider.gameObject);
                    }
                }
            }
            foreach (Tank tank in GameObject.FindObjectsOfType<Tank>())
            {
                if (oldPos != a && Toolbox.RectContains(oldPos, newPos, Camera.main.WorldToScreenPoint(tank.transform.position)))
                {
                    if (!selected.Contains(tank.gameObject))
                    {
                        selected.Add(tank.gameObject);
                    }
                }
            }
            oldPos = a;
            newPos = a;
        }
    }
}
