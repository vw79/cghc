using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGizmos : MonoBehaviour
{
    public Transform groundCheckPoint;
    public Vector2 groundCheckSize = new Vector2(0.49f, 0.03f);
    public Transform frontWallCheckPoint;
    public Transform backWallCheckPoint;
    public Vector2 wallCheckSize = new Vector2(0.5f, 1f);

    private void OnDrawGizmos()
    {
        if (groundCheckPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(groundCheckPoint.position, groundCheckSize);
        }

        if (frontWallCheckPoint != null && backWallCheckPoint != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(frontWallCheckPoint.position, wallCheckSize);
            Gizmos.DrawWireCube(backWallCheckPoint.position, wallCheckSize);
        }
    }
}
