using UnityEngine;

public class DrawLine : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] LayerMask collisionLayer;

    void Update()
    {
        RaycastHit2D _hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, collisionLayer);

        if (_hit.collider != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, _hit.point);
        }
        else
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + Vector3.down * 1000);
        }
    }
}
