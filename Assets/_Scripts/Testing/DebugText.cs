using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugText : MonoBehaviour
{
    [SerializeField]
    private RectTransform _arrow;
    [SerializeField]
    private TextMeshProUGUI fText, gText, hText, pText;

    public RectTransform Arrow { get => _arrow; set => _arrow = value; }
    public TextMeshProUGUI FText { get => fText; set => fText = value; }
    public TextMeshProUGUI GText { get => gText; set => gText = value; }
    public TextMeshProUGUI HText { get => hText; set => hText = value; }
    public TextMeshProUGUI PText { get => pText; set => pText = value; }
}
