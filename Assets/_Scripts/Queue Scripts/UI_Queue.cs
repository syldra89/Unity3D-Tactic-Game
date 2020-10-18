using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Queue : MonoBehaviour
{
    [SerializeField] private Stack<string> queueList;
    
    // Start is called before the first frame update
    void Start()
    {
        queueList.Push("A");
        queueList.Push("B");
        queueList.Push("C");
        queueList.Push("D");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
