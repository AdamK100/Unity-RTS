

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 
public class Miner : MonoBehaviour
{
    public Player p;
    private NavMeshAgent agent;
    public float currentCapacity;
    public float maxCapacity;
    public float miningSpeed;
    public bool mining = false;
    public bool emptying = false;
    private GameObject target;
    public GameObject weld;
    public GameObject spin;
    public GameObject carried;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true;
    }
    private void FixedUpdate()
    {
        Debug.Log(p.money);
        if (agent.isOnNavMesh && agent.isStopped && !mining && !emptying && currentCapacity < maxCapacity)
        {
            target = Toolbox.FindNearest(transform.position, Toolbox.GoldToGameObjects(FindObjectsOfType<Gold>()));
            agent.isStopped = false;
            agent.SetDestination(target.transform.position);
        }
       if(!agent.isStopped && !mining && !emptying && Vector3.Distance(transform.position, target.transform.position) <= 3)
        {
            agent.isStopped = true;
            mining = true;
            weld.SetActive(true);
            spin.GetComponent<Animator>().SetBool("Mines", true);
        }
        if (mining)
        {
            if (target.GetComponent<Gold>().value < miningSpeed * Time.fixedDeltaTime)
            {
                currentCapacity += target.GetComponent<Gold>().value;
                target.GetComponent<Gold>().value = 0;
                Destroy(target);
                weld.SetActive(false);
                spin.GetComponent<Animator>().SetBool("Mines", false);
                mining = false;
            }
            if ((maxCapacity - currentCapacity) < miningSpeed * Time.fixedDeltaTime)
            {
                target.GetComponent<Gold>().value -= (maxCapacity - currentCapacity);
                currentCapacity = maxCapacity;
                emptying = true;
                mining = false;
                List<GameObject> refineries = new List<GameObject>();
                foreach (Building b in FindObjectsOfType<Building>())
                {
                    if (b.type == BuildingType.ResourceDepot)
                        refineries.Add(b.gameObject);
                }
                agent.SetDestination(Toolbox.FindNearest(transform.position, refineries.ToArray()).GetComponent<Building>().rallyPoint.transform.position);
                agent.isStopped = false;
                weld.SetActive(false);
                carried.SetActive(true);
                spin.GetComponent<Animator>().SetBool("Mines", false);
            }
            if (currentCapacity < maxCapacity - miningSpeed * Time.fixedDeltaTime)
            {
                currentCapacity += miningSpeed * Time.fixedDeltaTime;
                target.GetComponent<Gold>().value -= miningSpeed * Time.fixedDeltaTime;
            }
        }
            if(emptying == true && Vector3.Distance(transform.position, agent.destination) < 0.5f)
            {
                if (!agent.isStopped)
                {
                    agent.isStopped = true;
                }
                if (currentCapacity < miningSpeed * 2 * Time.deltaTime)
                {
                    p.money += currentCapacity;
                    currentCapacity = 0;
                    carried.SetActive(false);
                    emptying = false;
                }
                else
                {
                    p.money += miningSpeed * 2 * Time.fixedDeltaTime;
                    currentCapacity -= miningSpeed * 2 * Time.fixedDeltaTime;
                }
            }

    }

}
