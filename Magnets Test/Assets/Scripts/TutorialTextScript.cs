using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialTextScript : MonoBehaviour
{
    [SerializeField] private string onePlayerText;
    [SerializeField] private string twoPlayerText;

    // Use this for initialization
    private void Start()
    {
        GetComponent<TextMeshProUGUI>().text = Controls.TwoPlayerMode ? twoPlayerText : onePlayerText;
    }
}