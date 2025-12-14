using UnityEngine;
using Unity.Cinemachine;

public class MapTransition : MonoBehaviour
{
    [Header("Teleport Settings")]
    public Transform destination;                  // Tempat player teleport
    [SerializeField] private PolygonCollider2D mapBoundary;  // Collider untuk confiner
    public CameraSwitch camSwitch;                // Script untuk switch camera

    private CinemachineConfiner2D confiner;

    private void Awake()
    {
        confiner = FindObjectOfType<CinemachineConfiner2D>();
        if (confiner == null)
            Debug.LogError("CinemachineConfiner2D not found in the scene!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // Teleport player langsung
        other.transform.position = destination.position;

        // Switch camera
        if (camSwitch != null)
            camSwitch.ZoomIn();

        // Update confiner
        if (confiner != null && mapBoundary != null)
        {
            confiner.BoundingShape2D = mapBoundary;
            confiner.InvalidateBoundingShapeCache();
        }
    }
}