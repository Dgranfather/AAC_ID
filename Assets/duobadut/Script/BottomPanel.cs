using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomPanel : MonoBehaviour
{
    public GameObject createButtonPanel;
    public GameObject contentShowScreen;

    public void OpenCreateButtonPanel()
    {
        createButtonPanel.SetActive(true);
    }

    public void CloseCreateButtonPanel()
    {
        createButtonPanel.SetActive(false);
    }

    public void ClearShowScreen()
    {
        foreach(Transform child in contentShowScreen.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
