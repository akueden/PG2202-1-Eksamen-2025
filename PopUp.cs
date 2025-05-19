using System.Collections;
using UnityEngine;
using TMPro;

public class ChestPopup : MonoBehaviour
{
    [Header("Belønning i kisten")]
    public int rewardAmount = 10;

    [Header("Referanser til UI")]
    public GameObject popupPanel;         // Panelet som inneholder belønningen
    public TextMeshProUGUI popupText;     // TMP-teksten med belønningen
    public GameObject pressEText;         // Text-objektet "Trykk E for å åpne"

    [Header("Tidsinnstillinger")]
    public float displayDuration = 2f;    // Hvor lenge popup vises

    private bool isOpened = false;
    private bool playerInRange = false;   // Er spilleren innenfor kistens område?

    void Start()
    {
        // Skjul begge UI-elementene ved spillstart
        if (pressEText != null) pressEText.SetActive(false);
        if (popupPanel != null)  popupPanel.SetActive(false);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpened)
        {
            playerInRange = true;
            if (pressEText != null) pressEText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (pressEText != null) pressEText.SetActive(false);
        }
    }

    void Update()
    {

        if (playerInRange && !isOpened && Input.GetKeyDown(KeyCode.E))
        {
            isOpened = true;
            if (pressEText != null) pressEText.SetActive(false);
            ShowPopup();
        }
    }

    void ShowPopup()
    {
        if (popupPanel != null) popupText.text = $"Du fikk {rewardAmount} bananer lead opp";
        if (popupPanel != null) popupPanel.SetActive(true);
        StartCoroutine(HideAfterDelay());
    }

    IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        if (popupPanel != null) popupPanel.SetActive(false);
    }
}

