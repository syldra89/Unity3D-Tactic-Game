using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Clase con los atributos de los objetos que van a ir en la lista characterList
public class CharacterList
{
    public string CharName;
    public int CharSpeed;
    public int CurrSpeedCount;

    public CharacterList(string charName, int charSpeed, int currSpeedCount) {
        CharName = charName;
        CharSpeed = charSpeed;
        CurrSpeedCount = currSpeedCount;
    }
}

public class QueueTest : MonoBehaviour
{
    //Cola con los turnos
    public Stack<CharacterList> queue = new Stack<CharacterList>();

    [SerializeField, Range(0,40)]
    private int speedTick = 20;
    private int maxValue;
    private int maxValueIndex;

    //Prefabs de los personajes
    public List<GameObject> characters = new List<GameObject>();

    //Lista con las variables de los personajes
    public List<CharacterList> characterList = new List<CharacterList>();
    

    // Start is called before the first frame update
    void Start()
    {
        InitializeVariables();
    }

    // Update is called once per frame
    void Update()
    {
        GenerateQueue();
        
    }

    void InitializeVariables() {

        //Cada personaje que tenga en la lista me va a agregar a la lista de variables de los personajes
        foreach (GameObject c in characters) {
            characterList.Add(new CharacterList(c.GetComponent<TestChar>().CharacterName, c.GetComponent<TestChar>().CharacterSpeed, c.GetComponent<TestChar>().CharacterSpeedCount));
        }
        
    }

    void GenerateQueue() {

        if (Input.GetKeyDown(KeyCode.G)) {

            CalculateScores();
            foreach (var q in queue)
            {
                Debug.Log(q.CharName + " " + queue.Count);
            }
        }
        
    }

    void CalculateScores() {

        

        if (queue == null || queue.Count < 17)
        {
            
            //Ordeno la lista
            SortBySpeedCount();

            //debug
            foreach (var c in characterList) {
                Debug.Log("Lista 1 . Char: "+c.CharName + " / SpeedCount " + c.CurrSpeedCount);
            }

            //Pongo al primer elemento de la lista como ganador e inicializo los max values
            
            maxValue = characterList[0].CurrSpeedCount;
            maxValueIndex = 0;

            //Chequeo si el personaje siguiente tiene el speedcount ganador (maxvalue) para agregarlo a la queue
            for (int i = 0; i < characterList.Count; i++)
            {
                //Si la velocidad del elemento es igual al max value
                if (characterList[i].CurrSpeedCount == maxValue)
                {
                    queue.Push(characterList[i]);
                    maxValueIndex = i;
                }
            }

            //Aumento en 20 el speedcount de todos los characters
            GenerateSpeedTicks();

            //Reseteo los valores para los ganadores, agarrar el valor principal para compararlo con el resto...
            Debug.Log(maxValueIndex);
            
            for (int i = 0; i < maxValueIndex+1; i++)
            {
                characterList[i].CurrSpeedCount = characterList[i].CharSpeed;
                Debug.Log("Ganador: " + characterList[i].CharName);

            }


            //LLamo a la funcion para calcular de nuevo
            CalculateScores();
        }

        
    }

    //Aumento en 20 los tick de currspeedcount
    void GenerateSpeedTicks()
    {
        foreach (var c in characterList)
        {
            c.CurrSpeedCount += 20;
        }

    }

    //Para ordenar la lista de menor a mayor
    void SortBySpeedCount() {
        characterList.Sort((y, x) => x.CurrSpeedCount.CompareTo(y.CurrSpeedCount));   
    }

}
