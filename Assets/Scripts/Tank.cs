using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class Tank : MonoBehaviour
{
    public GameObject Gun;
    public float speed;
    public float health;
    public float maxHealth;
    public float damage;
    public Vector3 firingPosition;
    public float price;
    public bool selected;
    public int pixelDiameter;
    public bool imageInstantiated;
    public Image healthbar;
    public Image healthBarOutline;
    public Sprite uiImage;
    public Sprite BuyImage;
    private float angularSize;
    public UType type;
    public Side side;
    public float productionTime;

    private void Start()
    {
        imageInstantiated = false;
        foreach (GameObject b in GameObject.FindGameObjectsWithTag("unit"))
        {
            if (b.GetComponent<Tank>().type != UType.Miner)
            {
                b.GetComponent<NavMeshAgent>().isStopped = true;
            }
        }
    }
    private void Update()
    {
        angularSize = ((gameObject.GetComponent<Collider>().bounds.extents.magnitude / Vector3.Distance(gameObject.transform.position, Camera.main.transform.position)) * Mathf.Rad2Deg);
        pixelDiameter = (int)(angularSize * Screen.width / Camera.main.fieldOfView);

    }
}

public enum UType
{
    Tank,
    Miner,
    Infantry
}

