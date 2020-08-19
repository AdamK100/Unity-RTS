using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class UI : MonoBehaviour
{
    public Player p;
    public Image UImage;
    public static GameObject activebuilding;
    public static Shader shader;
    public static List<Material> mats = new List<Material>();
    public static bool isBuilding;
    public Button[] activeButtons;
    public Terrain terrain;
    RaycastHit hit;
    public GameObject constructionSite;
    private GameObject activeSite;
    public float rotationMultiplier;
    public static bool rotating = false;
    private float leftHeld = 0;
    public Text text;
    public Canvas can;
    public Image[] queueImages;
    public Image production;
    public Image productionOut;
    public Text productionText;
    private Vector3 oldpos;
    public float diminution;
    public float radius;
    public Vector3 corrections;
    public static List<GameObject> walls = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        shader = Shader.Find("Transparent/Diffuse");
    }
    // Update is called once per frame
    void Update()
    {
        if (Selection.selected.ToArray().Length == 0)
        {
            if (productionText.enabled)
            {
                foreach (Image img in queueImages)
                {
                    img.enabled = false;
                    img.GetComponent<Button>().enabled = false;
                }
               // UImage.enabled = true;
                productionText.enabled = false;
                production.enabled = false;
                productionOut.enabled = false;
            }
          /*  if (UImage.sprite != p.symbol)
            {
                UImage.sprite = p.symbol;
            }*/
            foreach (Button b in activeButtons)
            {
                b.enabled = false;
                b.GetComponent<Image>().enabled = false;
            }
        }
        if (Selection.selected.ToArray().Length > 0)
        {
            if (Selection.selected.ToArray().Length > 1)
            {
                if (!UImage.enabled)
                {
                    foreach (Image img in queueImages)
                    {
                        img.enabled = false;
                        img.GetComponent<Button>().enabled = false;
                    }
                    UImage.enabled = true;
                    productionText.enabled = false;
                    production.enabled = false;
                    productionOut.enabled = false;
                }
            }
            GameObject mostCommon = Toolbox.ReturnMostCommon(Selection.selected);
            if (mostCommon.GetComponent<Tank>() != null)
            {
                UImage.sprite = mostCommon.GetComponent<Tank>().uiImage;
            }
            else
            {
               // UImage.sprite = mostCommon.GetComponent<Building>().uiImage;
            }
        }
        if (Selection.selected.ToArray().Length == 1)
        {
            if (Selection.selected[0].GetComponent<Tank>() != null)
            {
                if (!UImage.enabled)
                {
                    foreach (Image img in queueImages)
                    {
                        img.enabled = false;
                    }
                    UImage.enabled = true;
                    productionText.enabled = false;
                    production.enabled = false;
                    productionOut.enabled = false;
                }
            }
            if (Selection.selected[0].GetComponent<Building>() != null)
            {
             /*   if (Selection.selected[0].GetComponent<Building>().type == BuildingType.CommandCenter)
                {
                    if (!activeButtons[0].GetComponent<Image>().enabled)
                    {
                        for (int k = 0; k < 1; k++)
                        {
                            activeButtons[k].enabled = true;
                            activeButtons[k].GetComponent<Image>().enabled = true;
                           // activeButtons[k].GetComponent<Image>().sprite = activeButtons[k].GetComponent<ButtonScript>().builderImage;
                        }
                    }
                }*/
                /*if (Selection.selected[0].GetComponent<Building>().queue.ToArray().Length != 0)
                {
                    if (!queueImages[0].enabled || Selection.selected[0].GetComponent<Building>().timer == 0)
                    {
                        UImage.enabled = false;
                        for (int k = 0; k < Selection.selected[0].GetComponent<Building>().queue.ToArray().Length; k++)
                        {
                            queueImages[k].enabled = true;
                            queueImages[k].GetComponent<Button>().enabled = true;
                            queueImages[k].sprite = Selection.selected[0].GetComponent<Building>().queue[k].GetComponent<Tank>().BuyImage;
                        }
                        productionText.enabled = true;
                        production.enabled = true;
                        productionOut.enabled = true;
                    }
                    production.GetComponent<RectTransform>().sizeDelta = new Vector2((Selection.selected[0].GetComponent<Building>().timer * productionOut.GetComponent<RectTransform>().rect.width) / Selection.selected[0].GetComponent<Building>().queue[0].GetComponent<Tank>().productionTime, productionOut.GetComponent<RectTransform>().rect.height);
                    production.GetComponent<RectTransform>().position = new Vector2(productionOut.GetComponent<RectTransform>().position.x - (productionOut.GetComponent<RectTransform>().rect.width / 2) + production.GetComponent<RectTransform>().rect.width / 2, productionOut.GetComponent<RectTransform>().position.y);
                    productionText.text = "Production: " + (int)(Selection.selected[0].GetComponent<Building>().timer * 100 / Selection.selected[0].GetComponent<Building>().queue[0].GetComponent<Tank>().productionTime) + "%";
                }
                */
               /* else
                {
                    if (!UImage.enabled)
                    {
                        UImage.enabled = true;
                        foreach (Image img in queueImages)
                        {
                            img.enabled = false;
                            img.GetComponent<Button>().enabled = false;
                        }
                        productionText.enabled = true;
                        production.enabled = true;
                        productionOut.enabled = true;
                    }
                }*/
                for (int k = 0; k < queueImages.Length; k++)
                {
                    if (k >= Selection.selected[0].GetComponent<Building>().queue.ToArray().Length)
                    {
                        queueImages[k].enabled = false;
                    }
                }
            }

            if (isBuilding)
            {
                if (activebuilding.GetComponentsInChildren<MeshRenderer>()[0].material.color.a == 1)
                {
                    foreach (MeshRenderer r in activebuilding.GetComponentsInChildren<MeshRenderer>())
                    {
                        List<Material> colors = new List<Material>();
                    foreach (Material k in r.materials)
                    {
                        Material mat = new Material(shader);
                        mat.mainTexture = k.mainTexture;
                        mat.SetColor("_Color", new Color(mat.color.r, mat.color.g, mat.color.b, 0.5f));
                        colors.Add(mat);
                    }
                    r.materials = colors.ToArray();
                    }
                }                
                if (Input.GetMouseButtonUp(1) && !CameraScript.scrolling)
                {
                    Debug.Log("Work");
                    rotating = false;
                    p.money += activebuilding.GetComponent<Building>().price;
                    Destroy(activebuilding);
                    isBuilding = false;
                }
                if (activebuilding.GetComponent<Building>().isGrid)
                {
                    if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && terrain.GetComponent<TerrainCollider>().Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity) && leftHeld == 0)
                    {
                        activebuilding.transform.position = GridSystem.SnapToGrid(hit.point, GridSystem.myGrid);
                        if (Input.GetMouseButtonDown(0))
                        {
                            oldpos = GridSystem.SnapToGrid(hit.point, GridSystem.myGrid);
                            walls.Clear();
                        }
                        if (Input.GetMouseButton(0))
                        {
                            Vector3[] path = GridSystem.drawPath(oldpos, GridSystem.SnapToGrid(hit.point, GridSystem.myGrid), activebuilding.transform.rotation.eulerAngles.y);
                            walls.Clear();
                            float[] angles = GridSystem.angles.ToArray();
                            foreach(Building b in FindObjectsOfType<Building>())
                            {
                                if (b.temp)
                                {
                                    Destroy(b.gameObject);
                                }
                            }
                            for (int k = 0; k < path.Length; k++)
                            {
                                if (k != path.Length - 1)
                                {
                                    Debug.DrawLine(path[k], path[k + 1]);
                                }
                                walls.Add(Instantiate(activebuilding));
                            }
                            for (int k = 0; k < walls.ToArray().Length; k++)
                            {
                                walls[k].transform.position = path[k];
                                walls[k].transform.rotation = Quaternion.Euler(walls[k].transform.rotation.eulerAngles.x, angles[k], walls[k].transform.rotation.eulerAngles.z);
                                if (angles[k] == 45 || angles[k] == 135 || angles[k] == 225 || angles[k] == 315)
                                {
                                    walls[k].transform.localScale = new Vector3(walls[k].transform.localScale.x, walls[k].transform.localScale.y, 1.5f);
                                }
                                walls[k].GetComponent<Building>().temp = true;
                            }
                        }
                        if (Input.GetMouseButtonUp(0))
                        {
                            for(int a = 0; a < walls.ToArray().Length; a++)
                            {
                                if (p.money > walls[a].GetComponent<Building>().price)
                                {
                                    walls[a].GetComponent<Building>().temp = false;
                                    Digging.Dig(walls[a].transform.position, terrain, radius, diminution, 20, walls[a].transform.rotation.eulerAngles.y);
                                    walls[a].transform.position = new Vector3(walls[a].transform.position.x, walls[a].transform.position.y - diminution, walls[a].transform.position.z);
                                    p.money -= walls[a].GetComponent<Building>().price;
                                    foreach (MeshRenderer r in walls[a].GetComponentsInChildren<MeshRenderer>())
                                    {
                                        List<Material> colors = new List<Material>();
                                        foreach (Material k in r.materials)
                                        {
                                            Material mat = new Material(Shader.Find("Standard"));
                                            mat.mainTexture = k.mainTexture;
                                            mat.SetColor("_Color", new Color(mat.color.r, mat.color.g, mat.color.b, 2));
                                            colors.Add(mat);
                                        }
                                        r.materials = colors.ToArray();
                                    }
                                    activeSite = Instantiate(constructionSite, walls[a].transform.position, walls[a].transform.rotation);
                                    activeSite.transform.localScale = new Vector3(0.5f*activeSite.transform.localScale.x, 0.5f * activeSite.transform.localScale.y, 0.5f * activeSite.transform.localScale.z);
                                    activeSite.transform.position = new Vector3(activeSite.transform.position.x, activeSite.transform.position.y + diminution, activeSite.transform.position.z);
                                    activeSite.GetComponent<ConstructionSite>().Building = Instantiate(walls[a], walls[a].transform.position, walls[a].transform.rotation);
                                    Destroy(walls[a]);
                                    activeSite.GetComponent<ConstructionSite>().Building.SetActive(false);
                                    activeSite.GetComponent<ConstructionSite>().duration = activeSite.GetComponent<ConstructionSite>().Building.GetComponent<Building>().buildTime;
                                    activeSite.GetComponent<ConstructionSite>().can = can;
                                    activeSite.GetComponent<ConstructionSite>().text = text;
                                }
                                else
                                {
                                    Destroy(walls[a]);
                                }
                            }
                            walls.Clear();
                            leftHeld = 0;
                            isBuilding = false;
                            Destroy(activebuilding);
                        }
                    }
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        activebuilding.transform.rotation = Quaternion.Euler(activebuilding.transform.rotation.eulerAngles.x, activebuilding.transform.rotation.eulerAngles.y + 90, activebuilding.transform.rotation.eulerAngles.z);
                    }
                }
                if (!activebuilding.GetComponent<Building>().isGrid)
                {
                    if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && terrain.GetComponent<TerrainCollider>().Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity) && leftHeld == 0)
                    {
                        activebuilding.transform.position = hit.point;
                    }
                    if (Input.GetMouseButton(0))
                    {
                        leftHeld += Time.deltaTime;
                        if (leftHeld >= 0.7f && !rotating)
                        {
                            rotating = true;
                        }
                    }
                    if (rotating)
                    {
                        activebuilding.transform.rotation = Quaternion.Euler(activebuilding.transform.rotation.eulerAngles.x, activebuilding.transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * rotationMultiplier * Time.deltaTime, activebuilding.transform.rotation.eulerAngles.z);
                    }
                }
                if (Input.GetMouseButtonUp(0) && leftHeld > 0)
                {
                    if (!rotating && leftHeld <= 0.7f)
                    {
                        foreach (MeshRenderer r in activebuilding.GetComponentsInChildren<MeshRenderer>())
                        {
                            List<Material> colors = new List<Material>();
                            foreach (Material k in r.materials)
                            {
                                Material mat = new Material(Shader.Find("Standard"));
                                mat.mainTexture = k.mainTexture;
                                mat.SetColor("_Color", new Color(mat.color.r, mat.color.g, mat.color.b, 2));
                                colors.Add(mat);
                            }
                            r.materials = colors.ToArray();
                        }
                        activeSite = Instantiate(constructionSite, activebuilding.transform.position, activebuilding.transform.rotation);
                        activeSite.GetComponent<ConstructionSite>().Building = Instantiate(activebuilding, activebuilding.transform.position, activebuilding.transform.rotation);
                        Destroy(activebuilding);
                        activeSite.GetComponent<ConstructionSite>().Building.SetActive(false);
                        activeSite.GetComponent<ConstructionSite>().duration = activeSite.GetComponent<ConstructionSite>().Building.GetComponent<Building>().buildTime;
                        activeSite.GetComponent<ConstructionSite>().can = can;
                        activeSite.GetComponent<ConstructionSite>().text = text;
                        leftHeld = 0;
                        isBuilding = false;
                    }
                    else
                    {
                        rotating = false;
                        leftHeld = 0;
                    }
                }
            }
        }
    }
}
