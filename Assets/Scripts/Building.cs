using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class Building : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float price;
    public float buildTime;
    public bool isGrid;
    public Sprite uiImage;
    public GameObject rallyPoint;
    public bool imageInstantiated;
    public Image healthbar;
    public Image healthBarOutline;
    private float angularSize;
    public int pixelDiameter;
    public GameObject SpawnPoint;
    public BuildingType type;
    public GameObject created;
    public float timer = 0;
    public Vector3 rotationFix;
    public List<GameObject> queue = new List<GameObject>();
    public Animator doorAnim;
    public bool temp = false;

    private void Update()
    {
        /* if(timer == 0 && ButtonScript.gameManager.GetComponent<UI>().production.enabled)
         {
             ButtonScript.gameManager.GetComponent<UI>().production.enabled = false;
             ButtonScript.gameManager.GetComponent<UI>().productionOut.enabled = false;
             ButtonScript.gameManager.GetComponent<UI>().productionText.enabled = false;
         }
         if(timer > 0 && !ButtonScript.gameManager.GetComponent<UI>().production.enabled)
         {
             ButtonScript.gameManager.GetComponent<UI>().production.enabled = true;
             ButtonScript.gameManager.GetComponent<UI>().productionOut.enabled = true;
             ButtonScript.gameManager.GetComponent<UI>().productionText.enabled = true;
         }
         Debug.Log(timer);*/
         angularSize = ((gameObject.GetComponent<Collider>().bounds.extents.magnitude / Vector3.Distance(gameObject.transform.position, Camera.main.transform.position)) * Mathf.Rad2Deg);
         pixelDiameter = (int)(angularSize * Screen.width / Camera.main.fieldOfView);
        /*
         if(queue.ToArray().Length != 0)
         {
             timer += Time.deltaTime;
             if (timer >= queue[0].GetComponent<Tank>().productionTime)
             {
                 doorAnim.SetBool("open", true);
                 created = Instantiate(queue[0], SpawnPoint.transform.position, Quaternion.Euler(transform.rotation.eulerAngles + rotationFix));
                 created.GetComponent<NavMeshAgent>().isStopped = false;
                 created.GetComponent<NavMeshAgent>().SetDestination(rallyPoint.transform.position);
                 timer = 0;
                 queue.Remove(queue[0]);
             }
         }
         if (created != null)
         {
             if (Vector3.Distance(created.transform.position, SpawnPoint.transform.position) > 2)
             {
                 doorAnim.SetBool("open", false);
             }
         }*/
    }
}
public enum BuildingType
{
    CommandCenter,
    WarFactory,
    ResourceDepot
}
