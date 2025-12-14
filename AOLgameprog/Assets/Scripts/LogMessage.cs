using UnityEngine;
using TMPro;

public class LogMessage : MonoBehaviour
{
    public GameObject logPanelPrefab;      // prefab panel
    public string message = "Event happened!";

    private GameObject logInstance;
    private CharMovement playerMove;

    void Start()
    {
        playerMove = FindObjectOfType<CharMovement>();
    }

    public void ShowLog()
    {
        if (logInstance != null) return; // cuma 1 log active

        // spawn panel
        logInstance = Instantiate(logPanelPrefab, FindObjectOfType<Canvas>().transform);
        TMPro.TextMeshProUGUI textUI = logInstance.GetComponentInChildren<TextMeshProUGUI>();
        textUI.text = message;

        // freeze player
        if (playerMove != null) playerMove.enabled = false;
    }

    void Update()
    {
        if (logInstance != null && Input.GetKeyDown(KeyCode.Escape))
        {
            HideLog();
        }
    }

    public void HideLog()
    {
        if (logInstance != null)
            Destroy(logInstance);

        // unfreeze player
        if (playerMove != null) playerMove.enabled = true;
    }
}