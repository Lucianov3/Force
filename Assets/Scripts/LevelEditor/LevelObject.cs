using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New LevelObject", menuName = "LevelObject", order = 0)]
public class LevelObject : ScriptableObject
{
    public GameObject Object;
    public Sprite Image;
    public string Name;
    public int Id;
    public int Width;
    public int Height;
    public bool CanBeRotated;
    public bool CanBePlacedMultipleTimes;
    public bool HasChannel;
    public bool CanOnlyBePlacedOnTop;
    public bool CanOnlyBePlacedOnBottom;
    public bool IsUpsideDown;
}