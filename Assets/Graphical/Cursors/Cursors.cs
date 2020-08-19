using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursors : MonoBehaviour
{
    RaycastHit mouseHit;
    public Texture2D Idle;
    public Texture2D Build;
    public Texture2D Move;
    public Texture2D Select;
    public Texture2D Mine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (UI.rotating)
        {
            Cursor.visible = false;
        }
        if (!UI.rotating)
        {
            Cursor.visible = true;
        }
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out mouseHit, Mathf.Infinity))
        {
            if (UI.isBuilding)
            {
                Cursor.SetCursor(Build, new Vector2(0, 0), CursorMode.Auto);
            }
            else
            {
                if (Selection.selected.ToArray().Length == 0)
                {
                    Cursor.SetCursor(Idle, new Vector2(0, 0), CursorMode.Auto);
                }
                if (mouseHit.collider.GetComponent<Terrain>() != null && Selection.selected.ToArray().Length != 0)
                {
                    Cursor.SetCursor(Move, new Vector2(Move.width / 2, Move.height / 2), CursorMode.Auto);
                }
                if (mouseHit.collider.GetComponent<Tank>() != null || mouseHit.collider.GetComponent<Building>() != null)
                {
                    Cursor.SetCursor(Select, new Vector2(Select.width / 2, Select.height / 2), CursorMode.Auto);
                }
                if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    Cursor.SetCursor(Idle, new Vector2(0, 0), CursorMode.Auto);
                }
            }
        }
    }
}
