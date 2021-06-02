using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotTrajectoryRenderer : MonoBehaviour
{
    [SerializeField] private float distanceFromCenter = 1f;
    [SerializeField] private ContactFilter2D contactFilter2D;

    private LineRenderer lineRenderer;
    private bool canShowing = false;
    private List<Vector3> trajectoryPoints = new List<Vector3>();
    private void Start()
    {
        if (TryGetComponent<LineRenderer>(out lineRenderer))
        {
            Color tankColor = GetComponent<SpriteRenderer>().color;
            canShowing = true;
            lineRenderer.startColor = tankColor;
            lineRenderer.endColor = new Color(tankColor.r, tankColor.g, tankColor.b, 0.6f);
        }
        else
            Debug.LogError("Line renderer not found");
    }
    private void Update()
    {
        if (canShowing == true)
            ShowTrajectory(gameObject.transform.up * distanceFromCenter + gameObject.transform.position, gameObject.transform.up);
    }
    private void ShowTrajectory(Vector3 origin, Vector3 diration)
    {
        trajectoryPoints.Clear();
        trajectoryPoints.Add(origin);

        RaycastHit2D[] raycastHits = new RaycastHit2D[1];
        Physics2D.Raycast(origin, diration, contactFilter2D, raycastHits);
        trajectoryPoints.Add(raycastHits[0].point);

        lineRenderer.SetPositions(trajectoryPoints.ToArray());
    }
    public void Disable()
    {
        lineRenderer.enabled = false;
        canShowing = false;
    }
}
