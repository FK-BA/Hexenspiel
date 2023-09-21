using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveButton : MonoBehaviour
{
    public GameObject canvasToClose;
    public GameObject canvasToActivate;
    public TMP_InputField inputFieldToSelect;

    public Button saveButton;

    void Start()
    {
        // Attach a click event listener to the saveButton
        saveButton.onClick.AddListener(OnSaveButtonClick);
    }

    void OnSaveButtonClick()
    {
        // Deactivate the canvas and activate the second one
        canvasToClose.SetActive(false);
        canvasToActivate.SetActive(true);

        //Change Inputfield
        inputFieldToSelect.placeholder.GetComponent<TextMeshProUGUI>().text = "Versuchsperson";

        // Select the TMP_InputField
        inputFieldToSelect.Select();
    }
}