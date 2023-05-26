using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomPropagandaGenerator : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] string[] propaganda;
    [SerializeField] TextMeshProUGUI textBox;

    private void Awake()
    {
        textBox.text = propaganda[Random.Range(0, propaganda.Length)];
    }
}
