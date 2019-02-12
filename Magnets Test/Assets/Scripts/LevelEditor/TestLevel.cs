using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevel : MonoBehaviour
{
    public static Level TestLvl;

    private void Start()
    {
        GameManager.LoadLevel(TestLvl);
    }
}