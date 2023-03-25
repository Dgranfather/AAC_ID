using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowScreenManager : MonoBehaviour
{
    public Transform showScreenPlace;
    public static Transform ShowScreen { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        ShowScreen = showScreenPlace;
    }
}
