using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCharacterButton : MonoBehaviour
{
    [SerializeField]
    private string _characterName;

    public string CharacterName {
        get => _characterName;
    }

    [SerializeField]
    private int _characterSpeed;

    public int CharacterSpeed {
        get => _characterSpeed;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
