using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public string dialogueText = "Hiiii~";
    public GameObject dialogueBoxPrefab;

    private GameObject dialogueBoxInstance;
    private bool playerNearby = false;

    private CharMovement playerMove;
    private RandomWander npcWander;   // 👈 NEW
    public string npcName = "NPC";  // ini bisa di-set di Inspector per NPC
    public string extraNote = "extra";

    CameraSwitch cam;  // kalau kamu pakai camera switch

    void Start()
    {
        playerMove = FindObjectOfType<CharMovement>();
        npcWander = GetComponent<RandomWander>(); // 👈 NPC freeze

        cam = FindObjectOfType<CameraSwitch>();
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (dialogueBoxInstance == null) ShowDialogue();
            else HideDialogue();
        }
        if (dialogueBoxInstance != null && Input.GetKeyDown(KeyCode.Escape))
        {
            HideDialogue();
        }
    }

    void ShowDialogue()
    {
        // Spawn UI
        dialogueBoxInstance = Instantiate(dialogueBoxPrefab, FindObjectOfType<Canvas>().transform);
        TMPro.TextMeshProUGUI headerText = dialogueBoxInstance.transform.Find("HeaderText").GetComponent<TMPro.TextMeshProUGUI>();
        headerText.text = npcName;
        TMPro.TextMeshProUGUI extraText = dialogueBoxInstance.transform.Find("ExtraTMP").GetComponent<TMPro.TextMeshProUGUI>();
        extraText.text = extraNote;
        TMPro.TextMeshProUGUI textUI = dialogueBoxInstance.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        textUI.text = dialogueText;

        // Freeze player
        if (playerMove != null) playerMove.enabled = false;

        // Freeze NPC
        if (npcWander != null) npcWander.enabled = false;

        // Zoom in
        if (cam != null) cam.ZoomIn();
    }

    void HideDialogue()
    {
        if (dialogueBoxInstance != null)
            Destroy(dialogueBoxInstance);

        // Unfreeze player
        if (playerMove != null) playerMove.enabled = true;

        // Unfreeze NPC
        if (npcWander != null) npcWander.enabled = true;

        // Zoom out
        if (cam != null) cam.ZoomOut();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNearby = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            HideDialogue();
        }
    }
}