using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class ListingItem : MonoBehaviour
{
    [SerializeField] private List<string> wordList = new List<string>(); // List of words
    [SerializeField] private TextMeshProUGUI displayText; // UI Text to display the selected word
    [SerializeField] private UnityEvent onSelectionChanged; // Event when selection changes
    [SerializeField] private List<Button> buttons; // List of buttons to control

    private int currentIndex = 0;
    private Dictionary<string, Button> wordButtonMap = new Dictionary<string, Button>();

    private void Start()
    {
        // Map words to buttons
        for (int i = 0; i < Mathf.Min(wordList.Count, buttons.Count); i++)
        {
            wordButtonMap[wordList[i]] = buttons[i];
        }

        UpdateDisplay();
    }

    // Move selection to the left
    public void MoveLeft()
    {
        if (wordList.Count == 0) return;

        currentIndex--;
        if (currentIndex < 0)
            currentIndex = wordList.Count - 1;

        UpdateDisplay();
    }

    // Move selection to the right
    public void MoveRight()
    {
        if (wordList.Count == 0) return;

        currentIndex++;
        if (currentIndex >= wordList.Count)
            currentIndex = 0;

        UpdateDisplay();
    }

    // Update the display text and activate the correct button
    private void UpdateDisplay()
    {
        if (displayText != null && wordList.Count > 0)
        {
            string currentWord = wordList[currentIndex];
            displayText.text = currentWord;

            // Disable all buttons first
            foreach (var button in buttons)
            {
                button.gameObject.SetActive(false);
            }

            // Enable the button linked to the current word
            if (wordButtonMap.ContainsKey(currentWord))
            {
                wordButtonMap[currentWord].gameObject.SetActive(true);
            }
        }

        // Trigger event when selection changes
        onSelectionChanged?.Invoke();
    }
}