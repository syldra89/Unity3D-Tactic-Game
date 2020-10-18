using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private SOPlayer playerData;

    [SerializeField]
    private float playerMovement;
    public GameObject moveTile;
    
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = playerData.playerMovement;
    }

    // Update is called once per frame
    void Update()
    {
        InputCheck();
    }

    private void InputCheck() {

        //Activa o desactiva el rango de movimiento
        if (Input.GetKeyDown(KeyCode.Space)) {
            GenerateWalkablePath();
        }  
    }

    private void OnMouseDown()
    {
        Debug.Log("asdasd");
    }

    private void GenerateWalkablePath() {
        //Creo una variable con +1 para que genere la cantidad correcta de tiles
        int tilesToGenerate = (int)playerMovement + 1;
        Vector3 walkablePath;
        //Genero un camino para todos los valores que esten en el radio de tilesToGenerate y no este en (x,y)=0
        for (float x = -tilesToGenerate; x < tilesToGenerate; x++) {
            for (float y = -tilesToGenerate; y < tilesToGenerate; y++) {
                
                if ( ((Mathf.Abs(x) + Mathf.Abs(y)) < tilesToGenerate) && (x != 0 || y != 0)) {
                    walkablePath = new Vector3(x, y);
                    var myTile = Instantiate(moveTile, transform.position + walkablePath, moveTile.transform.rotation);
                    myTile.transform.parent = gameObject.transform;
                }
            }
        }
    }
}
