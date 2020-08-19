using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Movement : MonoBehaviour
{
    public Terrain map;
    private RaycastHit hit;
    float rotationSpeed;
    public float spacing;
    public float spacingY;
    public float StoppingDistance;
    // Start is called before the first frame update
    void Start()
    {

    }
    void DistributeDestination(GameObject[] selected, Vector3 destination, float spacing)
    {
        if (selected.Length > 1)
        {
            List<float> xs = new List<float>();
            foreach (GameObject obj in selected)
            {
                NavMeshAgent agent = obj.GetComponent<NavMeshAgent>();
                xs.Add(obj.transform.position.x);
            }
            xs.Sort();
            List<GameObject> selectedSorted = new List<GameObject>();
            foreach (float x in xs)
            {
                foreach (GameObject s in selected)
                {
                    if (s.transform.position.x == x)
                    {
                        selectedSorted.Add(s);
                    }
                }
            }
            if (selectedSorted.ToArray().Length == 2)
            {
                selectedSorted.ToArray()[0].GetComponent<NavMeshAgent>().SetDestination(new Vector3(destination.x, destination.y, destination.z + spacingY / 2));
                selectedSorted.ToArray()[1].GetComponent<NavMeshAgent>().SetDestination(new Vector3(destination.x, destination.y, destination.z - spacingY / 2));
                selectedSorted.ToArray()[0].GetComponent<NavMeshAgent>().isStopped = false;
                selectedSorted.ToArray()[1].GetComponent<NavMeshAgent>().isStopped = false;
            }
            if (selectedSorted.ToArray().Length > 2)
            {
                for (int k = 0; k < selectedSorted.ToArray().Length; k++)
                {
                    if (k <= (float)((selectedSorted.ToArray().Length - 1) / 3))
                    {
                        selectedSorted.ToArray()[k].GetComponent<NavMeshAgent>().SetDestination(new Vector3(destination.x + (k - selectedSorted.ToArray().Length / 6) * spacing, destination.y, destination.z - spacingY));
                    }
                    if (k > (float)((selectedSorted.ToArray().Length - 1) / 3) && k <= (float)((2 * (selectedSorted.ToArray().Length - 1)) / 3))
                    {
                        selectedSorted.ToArray()[k].GetComponent<NavMeshAgent>().SetDestination(new Vector3(destination.x + (k - (float)(selectedSorted.ToArray().Length / 3) - selectedSorted.ToArray().Length / 6) * spacing, destination.y, destination.z));
                    }
                    if (k > (float)((2 * (selectedSorted.ToArray().Length - 1)) / 3))
                    {
                        selectedSorted.ToArray()[k].GetComponent<NavMeshAgent>().SetDestination(new Vector3(destination.x + (k - (float)(2 * selectedSorted.ToArray().Length / 3) - selectedSorted.ToArray().Length / 6) * spacing, destination.y, destination.z + spacingY));
                    }
                    selectedSorted.ToArray()[k].GetComponent<NavMeshAgent>().isStopped = false;
                }
            }
        }
        else
        {
            selected[0].GetComponent<NavMeshAgent>().SetDestination(destination);
            selected[0].GetComponent<NavMeshAgent>().isStopped = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Debug
        foreach(GameObject g in Selection.selected)
        {
            if (g.tag == "unit")
            {
                NavMeshAgent agent = g.GetComponent<NavMeshAgent>();
                if (!agent.isStopped)
                {
                    Debug.DrawRay(g.transform.position, (agent.destination - g.transform.position), Color.green);
                }
            }  
        }
        foreach(NavMeshAgent agent in FindObjectsOfType<NavMeshAgent>())
        {
            if(!agent.isStopped && Vector3.Distance(agent.gameObject.transform.position, agent.destination) <= StoppingDistance)
            {
                agent.isStopped = true;
            }
               
        }

        if (Selection.selected.ToArray().Length != 0)
        {
                if (Input.GetMouseButtonDown(0))
                {
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity) && hit.transform.gameObject.tag != "unit")
                    {
                    if (hit.collider.gameObject.GetComponent<Terrain>() != null)
                    {
                        if (Selection.selected.ToArray()[0].GetComponent<Building>() == null)
                        {
                            DistributeDestination(Selection.selected.ToArray(), hit.point, spacing);
                        }
                        else
                        {
                            Selection.selected.ToArray()[0].GetComponent<Building>().rallyPoint.transform.position = hit.point;
                        }
                    }
                }
                }          
        }
    }
}
