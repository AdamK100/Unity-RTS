using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
public class CameraScript : MonoBehaviour
{
    public float maxHeight;
    public float minHeight;
    public Terrain map;
    //Speeds in a units/second
    public float ScrollSpeed;
    public float zoomSpeed;
    public static bool scrolling = false;
    RaycastHit hit;
    RaycastHit hit1;
    RaycastHit hit2;
    float rightMouseHeld = 0;
    Vector3 oldPos = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Limits
        Vector3 zoomVector = new Vector3(0, 0, 0);
        Vector3 corner1 = new Vector3(0,0,0);
        Vector3 corner2 = new Vector3(0,0,0);
        Vector3 top = new Vector3(0,0,0);
        if (map.GetComponent<TerrainCollider>().Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
        {
            zoomVector= transform.position - hit.point;
        }
        if (map.GetComponent<TerrainCollider>().Raycast(Camera.main.ScreenPointToRay(new Vector3(1, 1, 0)), out hit1 , Mathf.Infinity)){
            corner1 = hit1.point;
        }
        if (map.GetComponent<TerrainCollider>().Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width, 1, 0)), out hit1, Mathf.Infinity))
        {
            corner2 = hit1.point;
        }
        if (map.GetComponent<TerrainCollider>().Raycast(Camera.main.ScreenPointToRay(new Vector3(1, Screen.height, 0)), out hit2, Mathf.Infinity))
        {
            top = hit2.point;
        }

        // Controls
        if (Input.GetMouseButton(1))
        {
            rightMouseHeld += Time.deltaTime;
            if (!scrolling && rightMouseHeld > 0.5f)
            { 
                scrolling = true;
                ScrollSpeed *= 2;
                oldPos = Input.mousePosition;
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            rightMouseHeld = 0;

            if (scrolling)
            {
                ScrollSpeed /= 2;
                scrolling = false;
            }
        }
        if (!Input.GetMouseButton(1))
        {
            oldPos = Input.mousePosition;
        }
        //Scrolling

        Vector3 mvt = new Vector3(0,0,0);
        if ((Input.mousePosition.x <= 1 || oldPos.x > Input.mousePosition.x + Screen.width / 15) && corner1.x > ScrollSpeed * Time.deltaTime)
        { 
            mvt = new Vector3(mvt.x - ScrollSpeed * Time.deltaTime, mvt.y, mvt.z);
        }
        if((Input.mousePosition.x >= Screen.width - 1 || oldPos.x < Input.mousePosition.x - Screen.width/15) && corner2.x < map.terrainData.size.x - ScrollSpeed * Time.deltaTime)
        {
            mvt = new Vector3(mvt.x + ScrollSpeed * Time.deltaTime, mvt.y, mvt.z);
        }
        if((Input.mousePosition.y <= 1 || oldPos.y > Input.mousePosition.y + Screen.height / 10) && transform.position.z > ScrollSpeed * Time.deltaTime)
        {
            mvt = new Vector3(mvt.x, mvt.y, mvt.z - ScrollSpeed * Time.deltaTime);
        }
        if((Input.mousePosition.y >= Screen.height - 1 || oldPos.y < Input.mousePosition.y - Screen.height / 10) && top.z < map.terrainData.size.z - ScrollSpeed * Time.deltaTime)
        {
            mvt = new Vector3(mvt.x, mvt.y, mvt.z + ScrollSpeed * Time.deltaTime);
        }
        transform.Translate(mvt, Space.World);
        //Zooming
        Vector3 a = new Vector3(0, 0, 0);
        if ((Input.GetAxis("Mouse ScrollWheel") < 0 && top != a && corner2 != a && corner1 != a && transform.position.y < maxHeight - Mathf.Abs(zoomVector.y * zoomSpeed * -Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime)) || (Input.GetAxis("Mouse ScrollWheel") > 0 && transform.position.y > minHeight + Mathf.Abs(zoomVector.y * zoomSpeed * -Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime)))
        {
            transform.Translate(zoomVector * zoomSpeed * -Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime, Space.World);
        }
    }
}
