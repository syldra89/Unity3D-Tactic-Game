using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AStarDebug : MonoBehaviour
{
    private static AStarDebug _instance;
    public static AStarDebug Instance {
        get {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AStarDebug>();
            }
            return _instance;
        }       
    }

    [SerializeField]
    private Grid grid;

    [SerializeField]
    private Tile debugTile;

    [SerializeField]
    private Tilemap tilemap;

    [SerializeField]
    private Color openColor, closeColor, pathColor, currentColor, startColor, goalColor;

    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private GameObject debugTextPrefab;

    private List<GameObject> debugGameObjects = new List<GameObject>();

    public void CreateTiles(HashSet<Node> openList, HashSet<Node> closedList, Dictionary<Vector3Int, Node> allNodes, Vector3Int start, Vector3Int goal, Stack<Vector3Int> path = null)
    {
        foreach (Node node in openList) {
            ColorTile(node.Position, openColor);
        }

        foreach (Node node in closedList) {
            ColorTile(node.Position, closeColor);
        }

        foreach (KeyValuePair<Vector3Int, Node> node in allNodes) {
            float offset = 0.5f;
            if (node.Value.Parent != null) {
                GameObject myGameObject = Instantiate(debugTextPrefab, canvas.transform);
                myGameObject.transform.position = grid.CellToWorld(node.Key) + new Vector3(offset,offset,0);
                debugGameObjects.Add(myGameObject);
                GenerateDebugText(node.Value, myGameObject.GetComponent<DebugText>());
            }
        }

        if (path != null) {
            foreach (Vector3Int pos in path) {
                if (pos != start && pos != goal) {
                    ColorTile(pos, pathColor);
                }
            }
        }

        ColorTile(start, startColor);
        ColorTile(goal, goalColor);
    }

    public void GenerateDebugText(Node node, DebugText debugText) {

        debugText.FText.text = $"F:{node.F}";
        debugText.GText.text = $"G:{node.G}";
        debugText.HText.text = $"H:{node.H}";
        debugText.PText.text = $"P:{node.Position.x},{node.Position.y}";

        if (node.Parent.Position.x < node.Position.x && node.Parent.Position.y == node.Position.y)
        {
            debugText.Arrow.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
        }
        else if (node.Parent.Position.x < node.Position.x && node.Parent.Position.y > node.Position.y)
        {
            debugText.Arrow.localRotation = Quaternion.Euler(new Vector3(0, 0, 135));
        }
        else if (node.Parent.Position.x < node.Position.x && node.Parent.Position.y < node.Position.y)
        {
            debugText.Arrow.localRotation = Quaternion.Euler(new Vector3(0, 0, 225));
        }
        else if (node.Parent.Position.x > node.Position.x && node.Parent.Position.y == node.Position.y)
        {
            debugText.Arrow.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else if (node.Parent.Position.x > node.Position.x && node.Parent.Position.y > node.Position.y)
        {
            debugText.Arrow.localRotation = Quaternion.Euler(new Vector3(0, 0, 45));
        }
        else if (node.Parent.Position.x > node.Position.x && node.Parent.Position.y < node.Position.y)
        {
            debugText.Arrow.localRotation = Quaternion.Euler(new Vector3(0, 0, -45));
        }
        else if (node.Parent.Position.x == node.Position.x && node.Parent.Position.y > node.Position.y)
        {
            debugText.Arrow.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }
        else if (node.Parent.Position.x == node.Position.x && node.Parent.Position.y < node.Position.y)
        {
            debugText.Arrow.localRotation = Quaternion.Euler(new Vector3(0, 0, 270));
        }
        
    }

    public void ColorTile(Vector3Int position, Color color) {
        tilemap.SetTile(position, debugTile);
        tilemap.SetTileFlags(position, TileFlags.None);
        tilemap.SetColor(position, color);
    }
}
