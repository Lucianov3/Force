using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditorScript : MonoBehaviour
{
    public Vector2 gizmoPosition;
    public float depth = 0;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector3(gizmoPosition.x, gizmoPosition.y, depth), new Vector3(1, 1, 1));
    }
}