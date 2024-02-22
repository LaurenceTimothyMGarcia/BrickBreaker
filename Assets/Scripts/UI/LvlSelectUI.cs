using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LvlSelectUI : MonoBehaviour
{
    [SerializeField] private BrickLevelCollection collection;

    private Button currentButton;

    void Start()
    {
        // Get all buttons in the scene
        Button[] buttons = this.gameObject.GetComponentsInChildren<Button>();

        // Loop through each button
        foreach (Button button in buttons)
        {
            // Get the Text component of the button if it exists
            TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();

            // Add an onClick listener to each button
            if (buttonText != null)
            {
                string text = buttonText.text;
                button.onClick.AddListener(() => SelectLvl(text));
            }
        }
    }

    public void SelectLvl(string buttonText)
    {
        collection.SelectedLvl = buttonText;
        SceneManager.LoadScene("SampleScene");
    }
}
