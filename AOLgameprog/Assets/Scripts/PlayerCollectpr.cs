using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    public int totalCropsCollected = 0;
    public int carrotCount = 0;
    public int beetCount = 0;

    public LogMessage logMessage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Carrot"))
        {
            carrotCount++;
            totalCropsCollected++;
            logMessage.message = $"Picked up Carrot - Owned {carrotCount}";
            logMessage.ShowLog();
            Destroy(other.gameObject);
            Debug.Log($"Crop harvested: {totalCropsCollected}");
        }
        else if (other.CompareTag("Beet"))
        {
            beetCount++;
            totalCropsCollected++;
            logMessage.message = $"Picked up Beet - Owned {beetCount}";
            logMessage.ShowLog();
            Destroy(other.gameObject);
            Debug.Log($"Crop harvested: {totalCropsCollected}");
        }
    }
}