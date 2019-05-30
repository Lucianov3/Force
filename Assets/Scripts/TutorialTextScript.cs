using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTextScript : MonoBehaviour
{
    [SerializeField] private Sprite onePlayerSprite;
    [SerializeField] private Sprite twoPlayerSprite;

    // Use this for initialization
    private void Start()
    {
        GetComponent<Image>().sprite = Controls.TwoPlayerMode ? twoPlayerSprite : onePlayerSprite;
    }
}