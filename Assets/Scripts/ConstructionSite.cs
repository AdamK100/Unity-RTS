using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class ConstructionSite : MonoBehaviour
{
    public GameObject Building;
    public float duration = 0;
    private bool started;
    public Text text;
    private Text percentage;
    private float initialduration;
    public Canvas can;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(duration > 0) 
        {
            if (!started)
            {
                started = true;
                initialduration = duration;
                percentage = Instantiate(text);
                percentage.gameObject.transform.SetParent(can.transform);
                percentage.enabled = true;
            }

            percentage.rectTransform.position = new Vector2(Camera.main.WorldToScreenPoint(transform.position).x, Camera.main.WorldToScreenPoint(transform.position).y + 10);
            percentage.text = ((int)(100f - (duration * 100f / initialduration))).ToString() + " %";
            duration -= Time.deltaTime;
        }
        if (duration <= 0)
        {
            Building.SetActive(true);
            Destroy(percentage.gameObject);
            Destroy(gameObject);
        }
    }
}
