using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

//Seteo todos los estados posibles de la funcion A*
public enum TileType {
    Start,
    Goal,
    Walkable,
    Obstacle,
    Path
}

public class AStar : MonoBehaviour
{
    //Variables
    private TileType tileType;

    [SerializeField]
    private Tilemap tilemap;
    [SerializeField]
    private Tile[] tiles;
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private LayerMask layerMask;

    private Vector3Int startPos, goalPos;
    private Node currentNode;

    private HashSet<Node> openList;
    private HashSet<Node> closedList;
    private Stack<Vector3Int> path;

    //Diccionario que contiene todos los nodos que tengo
    private Dictionary<Vector3Int, Node> allNodes = new Dictionary<Vector3Int, Node>();

    //Si quiero setear una rule para dibujar agua....  // public RuleTile obstacle;

    void Start()
    {

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {

            //Detecto lo que clickeo con el puntero del mouse
            RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, layerMask);

            //Si el click colisiona con algo...
            if (hit.collider != null) {

                //Guardo la posicion del mouse en una variable
                Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                //Convierto la posicion a un Vector3Int para que sea mas facil de manipular en el tilemap
                Vector3Int clickPos = tilemap.WorldToCell(mouseWorldPos);
                //Genero una pared en la posicion del mouse
                ChangeTile(clickPos);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space)) {

            Algorithm();
        }
    }

    //Inicializo las variables
    private void Initialize() {
        currentNode = GetNode(startPos);
        openList = new HashSet<Node>();
        closedList = new HashSet<Node>();
        //Agregar start a la open list
        openList.Add(currentNode);
    }

    //Es el algoritmo completo del pathfinding con debugger integrado...
    private void Algorithm()
    {
        if (currentNode == null)
        {
            Initialize();
        }

        while (openList.Count > 0 && path == null) {
            List<Node> neighbors = FindNeighbors(currentNode.Position);
            ExamineNeighbors(neighbors, currentNode);
            UpdateCurrentTile(ref currentNode);

            path = GeneratePath(currentNode);
        }

        //Debugger
        AStarDebug.Instance.CreateTiles(openList, closedList, allNodes, startPos, goalPos, path);
    }

    /*----------------------------------------------------------------------------------------------------------------------------------------------------*/
    /*----------------------------------------------------------------------------------------------------------------------------------------------------*/
    /*----------------------------------------------------------------------------------------------------------------------------------------------------*/
    /*----------------------------------------------------------------------------------------------------------------------------------------------------*/


    //Genero un nodo para encontrar los vecinos
    private Node GetNode(Vector3Int position) {
        //Si un nodo existe lo encuentro y lo devuelvo
        if (allNodes.ContainsKey(position))
        {
            return allNodes[position];
        }
        //Si no existe lo creo y lo devuelvo 
        else {
            Node node = new Node(position);
            allNodes.Add(position, node);
            return node;
        }
    }

    //Genero los vecinos para buscarlos
    private List<Node> FindNeighbors(Vector3Int parentPosition) {
        List<Node> neighbors = new List<Node>();

        //Recorro los vecinos del nodo
        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++)
            {
                //Guardo la posicion del vecino
                Vector3Int neighborPos = new Vector3Int(parentPosition.x - x, parentPosition.y - y, parentPosition.z);
                //Dejar solo (x != 0 || y != 0) si quiero movimientos en diagonal del personaje
                if ( (x != 0 || y != 0) && ( x != 1 || y != 1 ) && (x != 1 || y != -1) && (x != -1 || y != 1) && (x != -1 || y != -1)) {
                    if (neighborPos != startPos && tilemap.GetTile(neighborPos) && (tilemap.GetTile(neighborPos) != tiles[3])) {
                        //Genero un nodo con la posicon del vecino
                        Node neighbor = GetNode(neighborPos);
                        neighbors.Add(neighbor);
                    }

                }
            }
        }

        return neighbors;
    }

    //Agrega los vecinos a la openlist
    private void ExamineNeighbors(List<Node> neighbors, Node current) {

        for (int i = 0; i < neighbors.Count; i++) {

            Node neighbor = neighbors[i];

            if (!DiagonalConnection(current, neighbor)) {
                continue;
            }

            int gScore = DetermineGScore(neighbors[i].Position, current.Position);

            //Si esta en la openlist verificar si tiene un puntaje menor y calcular los nuevos valores
            if (openList.Contains(neighbor))
            {
                if (current.G + gScore < neighbor.G) {
                    CalcValues(current, neighbors[i], gScore);
                }
            //Si no esta en la closedlist calcular valores y agregarlo a la openlist 
            }else if(!closedList.Contains(neighbor))
            {
                CalcValues(current, neighbors[i], gScore);
                openList.Add(neighbor);
            }
            
        }
    }

    //Calcula los valores de cada tile
    private void CalcValues(Node parent, Node neighbor, int cost) {
        neighbor.Parent = parent;

        neighbor.G = parent.G + cost;
        neighbor.H = ( (Math.Abs((neighbor.Position.x - goalPos.x)) + Math.Abs((neighbor.Position.y - goalPos.y)) ) * 10);
        neighbor.F = neighbor.G + neighbor.H;
    }
    //Determina los valores de G(x)
    private int DetermineGScore(Vector3Int neighbor, Vector3Int current) {
        int gScore = 0;
        int x = current.x - neighbor.x;
        int y = current.y - neighbor.y;

        if (Math.Abs(x - y) % 2 == 1)
        {
            gScore = 10;
        }
        else {
            gScore = 14;
        }

        return gScore;
    }

    private void UpdateCurrentTile(ref Node current) {
        openList.Remove(current);
        closedList.Add(current);

        if (openList.Count > 0) {
            current = openList.OrderBy(x => x.F).First();
        }
    }

    //Cambia el tipo de tile al clickear los botones
    public void ChangeTileType(TileButton button) {
        tileType = button.TileType;
    }

    //Cambiar el tile segun el tipo elegido por el boton
    private void ChangeTile(Vector3Int clickPos) {
        /*
        if (tileType == TileType.Obstacle) {
            tilemap.SetTile(clickPos, obstacle);
        }else ... */

        if (tileType == TileType.Start) {
            startPos = clickPos;
        } else if (tileType == TileType.Goal) {
            goalPos = clickPos;
        }

        tilemap.SetTile(clickPos, tiles[(int)tileType]);
    }

    //Para chequear si estan conectados diagonalmente los tiles
    private bool DiagonalConnection(Node current, Node neighbor) {
        Vector3Int direction = current.Position - neighbor.Position;
        Vector3Int first = new Vector3Int(current.Position.x + (direction.x * -1), current.Position.y, current.Position.z);
        Vector3Int second = new Vector3Int(current.Position.x, current.Position.y + (direction.y *-1), current.Position.z);

        //Si la posicion contiene un tile de pared...
        if ( (tilemap.GetTile(first) == tiles[3]) || (tilemap.GetTile(second) == tiles[3])) {
            return false;
        }

        return true;
    }

    private Stack<Vector3Int> GeneratePath(Node current) {
        if (current.Position == goalPos) {
            Stack<Vector3Int> finalPath = new Stack<Vector3Int>();

            while (current.Position != startPos) {
                finalPath.Push(current.Position);
                current = current.Parent;
            }

            return finalPath;
        }
        return null;
    }
}
