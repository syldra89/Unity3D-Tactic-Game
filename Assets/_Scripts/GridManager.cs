using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private int rows = 0;
    [SerializeField]
    private int cols = 0;
    [SerializeField]
    private float cellSize = 1;

    // Start is called before the first frame update
    void Start()
    {
        for (int col = 0; col < cols; col++) {
            for (int row = 0; row < rows; row++)
            {
                SpawnGameObject(row, col);
                Debug.DrawLine(new Vector3(col, row), new Vector3(col, row + 1), Color.white, 100f);
                Debug.DrawLine(new Vector3(col, row), new Vector3(col + 1, row), Color.white, 100f);
            }
        }
        Debug.DrawLine(new Vector3(0, rows), new Vector3(cols, rows), Color.white, 100f);
        Debug.DrawLine(new Vector3(cols, 0), new Vector3(cols, rows), Color.white, 100f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject SpawnGameObject(int row, int col) {
        GameObject gridObject = new GameObject("Grid Object [ F: "+row+" , "+" C: "+col+" ] ");
        Transform transform = gridObject.transform;
        transform.position = new Vector2( col + 0.5f, row +0.5f);
        gridObject.transform.parent = gameObject.transform;
        return gridObject;
    }
}
