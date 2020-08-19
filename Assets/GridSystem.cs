using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class GridSystem : MonoBehaviour
{
    public Terrain terrain;
    public static GridSquare[] myGrid;
    public float terrainHeight;
    public float subdivisions;
    public static List<float> angles = new List<float>();
    // Start is called before the first frame update
    void Start()
    {
        myGrid = Grid(terrain, subdivisions);
    }

    // Update is called once per frame
    void Update()
    {
        //To Show the Grid, press and hold I
        if (Input.GetKey(KeyCode.I))
        {
            foreach (GridSquare sq in myGrid)
            {
                Debug.DrawLine(sq.firstPoint, sq.fourthPoint, Color.green);
                Debug.DrawLine(sq.firstPoint, sq.secondPoint, Color.green);
                Debug.DrawLine(sq.secondPoint, sq.thirdPoint, Color.green);
                Debug.DrawLine(sq.thirdPoint, sq.fourthPoint, Color.green);
            }
        }
    }
    public GridSquare[] Grid(Terrain t, float subdividision)
    {
        List<GridSquare> squares = new List<GridSquare>();
        for (int k = 0; k < subdividision; k++)
        {
            for (int j = 0; j < subdividision; j++)
            {
                squares.Add(new GridSquare(new Vector3( k * t.terrainData.size.x / subdividision, terrainHeight, j * t.terrainData.size.x / subdividision), t.terrainData.size.x / subdividision));
            }
        }
        return squares.ToArray();
    }
    public static Vector3 SnapToGrid(Vector3 hitpoint, GridSquare[] grid)
    {
        List<float> distances = new List<float>();
        foreach(GridSquare sq in grid)
        {
            distances.Add(Vector3.Distance(hitpoint, sq.center));
        }
        return grid[distances.IndexOf(distances.Min())].center;

    }
    public static Vector3[] drawPath(Vector3 center1, Vector3 center2, float rotation)
    {
        angles.Clear();
        List<Vector3> pathpoints = new List<Vector3>();
        float gridsize = Vector3.Distance(myGrid[0].fourthPoint, myGrid[0].firstPoint);
        angles = new List<float>();
        if (center1.x != center2.x && center1.z == center2.z) 
        {
            if(center2.x > center1.x)
            {
                for(int k = 0; k <= Mathf.RoundToInt((center2.x - center1.x)/gridsize); k++)
                {
                    pathpoints.Add(new Vector3((center1.x + k * gridsize), center1.y, center1.z));
                    angles.Add(90 * Mathf.Sign(rotation));
                }
            }
            if (center2.x < center1.x)
            {
                for (int k = 0; k <= Mathf.RoundToInt((center1.x - center2.x) / gridsize); k++)
                {
                    pathpoints.Add(new Vector3((center1.x - k * gridsize), center1.y, center1.z));
                    angles.Add(90 * Mathf.Sign(rotation));
                }
            }
        }
        if (center1.x == center2.x && center1.z != center2.z)
        {
            if (center2.z > center1.z)
            {
                for (int k = 0; k <= ((center2.z - center1.z) / gridsize); k++)
                {
                    pathpoints.Add(new Vector3(center1.x, center1.y, center1.z + (k * gridsize)));
                    angles.Add(90 - (90 * Mathf.Sign(rotation)));
                }
            }
            if (center2.z < center1.z)
            {
                for (int k = 0; k <= Mathf.RoundToInt((center1.z - center2.z) / gridsize); k++)
                {
                    pathpoints.Add(new Vector3(center1.x, center1.y, center1.z - k * gridsize));
                    angles.Add(90 - (90 * Mathf.Sign(rotation)));
                }
            }
        }
        if (center1.x < center2.x && center1.z < center2.z)
        {
            if((int)(center2.x - center1.x) > (int)(center2.z - center1.z))
            {
                for (int k = 0; k <= Mathf.Round((center2.x - center1.x) / gridsize); k++)
                {
                    pathpoints.Add(new Vector3((center1.x + k * gridsize), center1.y, center1.z));
                    angles.Add(90 * Mathf.Sign(rotation));
                }
                for (int a = 0; a <= Mathf.Round((center2.z - center1.z) / gridsize); a++)
                {
                    pathpoints.Add(new Vector3(center2.x, center1.y, center1.z + a * gridsize));
                    angles.Add(90 - (90 * Mathf.Sign(rotation)));
                }
            }
            if ((int)(center2.x - center1.x) < (int)(center2.z - center1.z))
            {                 
                for (int a = 0; a <= Mathf.Round((center2.z - center1.z) / gridsize); a++)
                {
                    pathpoints.Add(new Vector3(center1.x, center1.y, center1.z + a * gridsize));
                    angles.Add(90 - (90 * Mathf.Sign(rotation)));
                }
                for (int k = 0; k <= Mathf.Round((center2.x - center1.x) / gridsize); k++)
                {
                    pathpoints.Add(new Vector3((center1.x + k * gridsize), center1.y, center2.z));
                    angles.Add(90 * Mathf.Sign(rotation));
                }
            }
            if ((int)(center2.x - center1.x) == (int)(center2.z - center1.z))
            {                
                for (int a = 0; a <= Mathf.Round((center2.z - center1.z) / gridsize); a++)
                {
                    pathpoints.Add(new Vector3(center1.x + a * gridsize, center1.y, center1.z + a * gridsize));
                    angles.Add(45);
                }
            }
        }
        if (center1.x > center2.x && center1.z > center2.z)
        {
            if (Mathf.RoundToInt(center1.x - center2.x) > Mathf.RoundToInt(center1.z - center2.z))
            {
                for (int k = 0; k <= Mathf.RoundToInt((center1.x - center2.x) / gridsize); k++)
                {
                    pathpoints.Add(new Vector3((center1.x - k * gridsize), center1.y, center1.z));
                    angles.Add(90 * Mathf.Sign(rotation));
                }
                for (int a = 0; a <= Mathf.RoundToInt((center1.z - center2.z) / gridsize); a++)
                {
                    pathpoints.Add(new Vector3(center2.x, center1.y, center1.z - a * gridsize));
                    angles.Add(90 - (90 * Mathf.Sign(rotation)));
                }
            }
            if (Mathf.RoundToInt(center1.x - center2.x) < Mathf.RoundToInt(center1.z - center2.z))
            {                
                for (int a = 0; a <= Mathf.RoundToInt((center1.z - center2.z) / gridsize); a++)
                {
                    pathpoints.Add(new Vector3(center1.x, center1.y, center1.z - a * gridsize));
                    angles.Add(90 - (90 * Mathf.Sign(rotation)));
                }
                for (int k = 0; k <= Mathf.RoundToInt((center1.x - center2.x) / gridsize); k++)
                {
                    pathpoints.Add(new Vector3((center1.x - k * gridsize), center1.y, center2.z));
                    angles.Add(90 * Mathf.Sign(rotation));
                }
            }
            if (Mathf.RoundToInt(center1.x - center2.x) == Mathf.RoundToInt(center1.z - center2.z))
            {
                for (int a = 0; a <= Mathf.RoundToInt((center1.z - center2.z) / gridsize); a++)
                {
                    pathpoints.Add(new Vector3(center1.x - a * gridsize, center1.y, center1.z - a * gridsize));
                    angles.Add(225);
                }
            }

        }
        if (center1.x > center2.x && center1.z < center2.z)
        {
            if ((int)(center1.x - center2.x) > (int)(center2.z - center1.z))
            {
                for (int k = 0; k <= Mathf.RoundToInt((center1.x - center2.x) / gridsize); k++)
                {
                    pathpoints.Add(new Vector3((center1.x - k * gridsize), center1.y, center1.z));
                    angles.Add(90 * Mathf.Sign(rotation));
                }
                for (int a = 0; a <= Mathf.RoundToInt((center2.z - center1.z) / gridsize); a++)
                {
                    pathpoints.Add(new Vector3(center2.x, center1.y, center1.z + a * gridsize));
                    angles.Add(90 - (90 * Mathf.Sign(rotation)));
                }
            }
            if ((int)(center1.x - center2.x) < (int)(center2.z - center1.z))
            {
                for (int a = 0; a <= Mathf.RoundToInt((center2.z - center1.z) / gridsize); a++)
                {
                    pathpoints.Add(new Vector3(center1.x, center1.y, center1.z + a * gridsize));
                    angles.Add(90 - (90 * Mathf.Sign(rotation)));
                }
                for (int k = 0; k <= Mathf.RoundToInt((center1.x - center2.x) / gridsize); k++)
                {
                    pathpoints.Add(new Vector3((center1.x - k * gridsize), center1.y, center2.z));
                    angles.Add(90 * Mathf.Sign(rotation));
                }
            }
            if ((int)(center1.x - center2.x) == (int)(center2.z - center1.z))
            {
                for (int k = 0; k <= Mathf.RoundToInt((center1.x - center2.x) / gridsize); k++)
                {
                    pathpoints.Add(new Vector3((center1.x - k * gridsize), center1.y, center1.z + k * gridsize));
                    angles.Add(135);
                }
            }
        }
        if (center1.x < center2.x && center1.z > center2.z)
        {
            if ((int)(center2.x - center1.x) > (int)(center1.z - center2.z))
            {
                for (int k = 0; k <= Mathf.RoundToInt((center2.x - center1.x) / gridsize); k++)
                {
                    pathpoints.Add(new Vector3((center1.x + k * gridsize), center1.y, center1.z));
                    angles.Add(90 * Mathf.Sign(rotation));
                }
                for (int a = 0; a <= Mathf.RoundToInt((center1.z - center2.z) / gridsize); a++)
                {
                    pathpoints.Add(new Vector3(center2.x, center1.y, center1.z - a * gridsize));
                    angles.Add(90 - (90 * Mathf.Sign(rotation)));
                }
            }
            if ((int)(center2.x - center1.x) < (int)(center1.z - center2.z))
            {
                for (int a = 0; a <= Mathf.RoundToInt((center1.z - center2.z) / gridsize); a++)
                {
                    pathpoints.Add(new Vector3(center1.x, center1.y, center1.z - a * gridsize));
                    angles.Add(90 - (90 * Mathf.Sign(rotation)));
                }
                for (int k = 0; k <= Mathf.RoundToInt((center2.x - center1.x) / gridsize); k++)
                {
                    pathpoints.Add(new Vector3((center1.x + k * gridsize), center1.y, center2.z));
                    angles.Add(90 * Mathf.Sign(rotation));
                }
            }
            if ((int)(center2.x - center1.x) == (int)(center1.z - center2.z))
            {
                for (int k = 0; k <= Mathf.RoundToInt((center2.x - center1.x) / gridsize); k++)
                {
                    pathpoints.Add(new Vector3((center1.x + k * gridsize), center1.y, center1.z - k * gridsize));
                    angles.Add(315);
                }
            }
        }
        return pathpoints.ToArray();
    }
}
//A Square in the Grid

public class GridSquare
{
    public Vector3 firstPoint { get; set; }
    public Vector3 secondPoint { get; set; }
    public Vector3 thirdPoint { get; set; }
    public Vector3 fourthPoint { get; set; }
    public Vector3 center { get; }
    public GridSquare(Vector3 a, float side)
    {
        firstPoint = a;
        secondPoint = new Vector3(a.x, a.y, a.z + side);
        thirdPoint = new Vector3(a.x + side, a.y, a.z + side);
        fourthPoint = new Vector3(a.x + side, a.y, a.z);
        center = (firstPoint + thirdPoint) / 2;
    }
}