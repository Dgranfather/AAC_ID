using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private string saveFileName = "buttonData.json";

    private Dictionary<string, object> buttonData = new Dictionary<string, object>();
    //private List<Dictionary<string, object>> buttonDataList = new List<Dictionary<string, object>>();

    public void SaveButtonData(GameObject buttonToSave)
    {
        buttonData.Clear();

        // Save button text
        TextMeshProUGUI buttonText = buttonToSave.GetComponentInChildren<TextMeshProUGUI>();
        buttonData.Add("text", buttonText.text);

        // Save button image
        Image buttonImage = buttonToSave.GetComponentInChildren<Image>();
        buttonData.Add("image", buttonImage.sprite.name);

        // Save button position
        buttonData.Add("position", buttonToSave.transform.position);

        // Save button scale
        buttonData.Add("scale", buttonToSave.transform.localScale);

        // Save button rotation
        buttonData.Add("rotation", buttonToSave.transform.rotation.eulerAngles);

        // Convert dictionary to JSON string
        string jsonData = JsonUtility.ToJson(buttonData);

        // Write JSON string to file
        string saveFilePath = Application.persistentDataPath + "/" + saveFileName;
        try
        {
            File.WriteAllText(saveFilePath, jsonData);
            Debug.Log("Button data saved to " + saveFilePath);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error saving button data: " + e.Message);
        }
        Debug.Log(jsonData);
    }

    public void LoadButtonData(GameObject buttonToSave)
    {
        // Check if file exists
        string saveFilePath = Application.persistentDataPath + "/" + saveFileName;
        if (!File.Exists(saveFilePath))
        {
            Debug.Log("Save file does not exist");
            return;
        }

        // Read JSON string from file
        string jsonData = File.ReadAllText(saveFilePath);

        // Convert JSON string to dictionary
        buttonData = JsonUtility.FromJson<Dictionary<string, object>>(jsonData);

        // Set button text
        if (buttonData.ContainsKey("text"))
        {
            TextMeshProUGUI buttonText = buttonToSave.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = (string)buttonData["text"];
        }

        // Set button image
        if (buttonData.ContainsKey("image"))
        {
            string imageName = (string)buttonData["image"];
            Sprite buttonSprite = Resources.Load<Sprite>(imageName);
            Image buttonImage = buttonToSave.GetComponentInChildren<Image>();
            buttonImage.sprite = buttonSprite;
        }

        // Set button position
        if (buttonData.ContainsKey("position"))
        {
            buttonToSave.transform.position = (Vector3)buttonData["position"];
        }

        // Set button scale
        if (buttonData.ContainsKey("scale"))
        {
            buttonToSave.transform.localScale = (Vector3)buttonData["scale"];
        }

        // Set button rotation
        if (buttonData.ContainsKey("rotation"))
        {
            Vector3 rotation = (Vector3)buttonData["rotation"];
            Quaternion quaternion = Quaternion.Euler(rotation);
            buttonToSave.transform.rotation = quaternion;
        }

        Debug.Log("Button data loaded from " + saveFilePath);
    }

    //public void SaveButtonData(GameObject buttonToSave)
    //{
    //    Dictionary<string, object> buttonData = new Dictionary<string, object>();

    //    // Save button text
    //    TextMeshProUGUI buttonText = buttonToSave.GetComponentInChildren<TextMeshProUGUI>();
    //    buttonData.Add("text", buttonText.text);

    //    //// Save button image
    //    Image buttonImage = buttonToSave.GetComponentInChildren<Image>();
    //    buttonData.Add("image", buttonImage.sprite.name);

    //    // Save button position
    //    buttonData.Add("position", buttonToSave.transform.position);

    //    // Save button scale
    //    buttonData.Add("scale", buttonToSave.transform.localScale);

    //    // Save button rotation
    //    buttonData.Add("rotation", buttonToSave.transform.rotation.eulerAngles);

    //    // Add button data to the list
    //    buttonDataList.Add(buttonData);

    //    // Convert list to JSON string
    //    string jsonData = JsonUtility.ToJson(buttonDataList);

    //    // Write JSON string to file
    //    string saveFilePath = Application.persistentDataPath + "/" + saveFileName;
    //    try
    //    {
    //        File.WriteAllText(saveFilePath, jsonData);
    //        Debug.Log("Button data saved to " + saveFilePath);
    //    }
    //    catch (System.Exception e)
    //    {
    //        Debug.LogError("Error saving button data: " + e.Message);
    //    }
    //    Debug.Log(jsonData);
    //}
}
