using UnityEngine;

public class RoadSocketGizmo : MonoBehaviour
{
    public Color gizmoColor = Color.green;
    public float arrowLength = 2f;

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, 0.3f);

        // Draw forward arrow
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * arrowLength);
    }
}