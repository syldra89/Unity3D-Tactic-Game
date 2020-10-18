using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChar : MonoBehaviour
{
    [SerializeField]
    private string characterName;
    public string CharacterName { get => characterName; }

    [SerializeField]
    private int characterSpeed;
    public int CharacterSpeed { get => characterSpeed; }

    [SerializeField]
    private int characterSpeedCount;
    public int CharacterSpeedCount { get => characterSpeedCount; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
