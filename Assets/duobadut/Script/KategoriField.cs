using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KategoriField : MonoBehaviour
{
    public GameObject[] fieldPanel;

    public void BukaKategori(int id)
    {
        for(int i = 0; i < fieldPanel.Length; i++)
        {
            if (i == id)
            {
                fieldPanel[i].SetActive(true);
            }
            else
            {
                fieldPanel[i].SetActive(false);
            }
        }
    }
}
