using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ShotTrajectoryRenderer : MonoBehaviour
{
    [SerializeField] private float distanceFromCenter = 1f;
    [SerializeField] private float length = 5f;
    [SerializeField] private ContactFilter2D contactFilter;

    private LineRenderer lineRenderer;

    private void Awake() => lineRenderer = GetComponent<LineRenderer>();

    private void OnDisable() => lineRenderer.enabled = false;

    private void OnEnable() => lineRenderer.enabled = true;
    
    private void Start()
    {
        Color tankColor = GetComponent<SpriteRenderer>().color;
        lineRenderer.startColor = tankColor;
        lineRenderer.endColor = new Color(tankColor.r, tankColor.g, tankColor.b, 0.6f);
    }

    private void Update()
    {
        Transform tr = transform;
        DrawTrajectory(gameObject.transform.up * distanceFromCenter + tr.position, tr.up);
    }
    
    private void DrawTrajectory(Vector3 origin, Vector3 direction)
    {
        List<Vector3> trajectoryPoints = new List<Vector3>();
        List<RaycastHit2D> contactsResult = new List<RaycastHit2D>();
        trajectoryPoints.Add(origin);

        float lengthRemainder = length;
        int reflectionsLimit = 20;
        
        while (lengthRemainder > 0 && reflectionsLimit > 0)
        {
            reflectionsLimit--;
            
            Physics2D.Raycast(origin + direction * 0.01f, direction, contactFilter, contactsResult);
            var hit = contactsResult[0];
            Vector2 contactPoint = hit.point;
            Vector2 previousContactPoint = trajectoryPoints[trajectoryPoints.Count - 1];
            float distance = Vector2.Distance(previousContactPoint, contactPoint);

            if (distance > lengthRemainder) 
            {
                contactPoint = origin + direction * lengthRemainder;
                trajectoryPoints.Add(contactPoint);
                break;
            }

            lengthRemainder -= distance;
            trajectoryPoints.Add(contactPoint);
            origin = contactPoint;
            direction = Vector3.Reflect(direction, hit.normal);

            if (hit.rigidbody.gameObject.TryGetComponent<PlayerController>(out var player))
            {
                break;
            }
        }

        lineRenderer.positionCount = trajectoryPoints.Count;
        lineRenderer.SetPositions(trajectoryPoints.ToArray());
    }
}
