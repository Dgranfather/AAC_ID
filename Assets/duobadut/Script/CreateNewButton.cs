using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.Android;

[RequireComponent(typeof(AudioSource))]
public class CreateNewButton : MonoBehaviour
{
    //gambar
    private Texture2D choosenImg;
    public Image displayImg;

    //nama tombol
    private string userInput;
    public TMP_InputField namaInputField;

    //kategori dropdown
    public TMP_Dropdown dropdown;
    public GameObject parentObject;

    //audio user
    //public AudioSource audioSource;
    //private AudioClip selectedClip;
    private AudioSource audioSource;
    private AudioClip recordedClip;
    private bool onRecord;
    [SerializeField] private GameObject micImg, stopImg, playRecordBtn;

    //objPrefabs
    public GameObject buttonPrefab;
    public Transform[] contentFieldPanel;

    public SaveManager theSaveManager;
    void Start()
    {
        userInput = null;
        choosenImg = null;
        micImg.SetActive(true);
        stopImg.SetActive(false);
        playRecordBtn.SetActive(false);
        onRecord = false;
        recordedClip = null;

        audioSource = GetComponent<AudioSource>();

        dropdown.onValueChanged.AddListener(KategoriValueChanged);

        // Clear the options in the dropdown menu
        dropdown.ClearOptions();

        // Get the direct children of the parent object
        Transform[] children = new Transform[parentObject.transform.childCount];
        for (int i = 0; i < parentObject.transform.childCount; i++)
        {
            children[i] = parentObject.transform.GetChild(i);
        }

        // Add each child as an option in the dropdown menu
        List<string> options = new List<string>();
        foreach (Transform child in children)
        {
            options.Add(child.name);
        }
        dropdown.AddOptions(options);
    }

    public void SelectPics()
    {
        GetSelectedImage();
    }

    private Texture2D GetSelectedImage()
    {
        Texture2D selectedImage = null;

        // Open the gallery to select an image
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            if (path != null)
            {
                // Load the selected image as a Texture2D
                selectedImage = NativeGallery.LoadImageAtPath(path);
                displayImg.sprite = Sprite.Create(selectedImage, new Rect(0, 0, selectedImage.width, selectedImage.height), new Vector2(0.5f, 0.5f));
                choosenImg = selectedImage;
            }
        }, "Select an image", "image/*");

        // If gallery permission is denied, return null
        if (permission != NativeGallery.Permission.Granted)
        {
            Debug.LogWarning("Gallery permission is denied.");
            return null;
        }

        // Return the selected image (which could be null if no image was selected)
        return selectedImage;
    }

    public void SetNamaTombol()
    {
        userInput = namaInputField.text;
    }

    public void KategoriValueChanged(int index)
    {
        //on kategori change
    }

    //public void OpenFilePicker()
    //{
    //    // Request permission to access external storage
    //    if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
    //    {
    //        Permission.RequestUserPermission(Permission.ExternalStorageRead);
    //    }
    //    else
    //    {
    //        // Open the file picker
    //        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
    //        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
    //        intentObject.Call<AndroidJavaObject>("setType", "audio/*");
    //        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_OPEN_DOCUMENT"));
    //        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    //        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
    //        currentActivity.Call("startActivityForResult", intentObject, 0);
    //    }
    //}

    //public void OnActivityResult(string result)
    //{
    //    // When the user selects a file from the file picker, this method will be called with the URI of the selected file
    //    if (result != null)
    //    {
    //        StartCoroutine(LoadAudioClip(result));
    //    }
    //}

    //private IEnumerator LoadAudioClip(string uri)
    //{
    //    // Load the selected audio clip and save it to the selectedClip variable
    //    UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(uri, AudioType.WAV);
    //    yield return www.SendWebRequest();
    //    selectedClip = DownloadHandlerAudioClip.GetContent(www);
    //    audioSource.clip = selectedClip;
    //    audioSource.Play();
    //}

    //public void PlayAudioUser()
    //{
    //    audioSource.clip = selectedClip;
    //    audioSource.Play();
    //}

    public void StartRecording()
    {
        onRecord = true;
        audioSource.clip = Microphone.Start(null, true, 10, 44100);
    }

    public void StopRecording()
    {
        Microphone.End(null);
        recordedClip = audioSource.clip;
        onRecord = false;
    }

    public void RecorderBtn()
    {
        if(!onRecord)
        {
            StartRecording();
            micImg.SetActive(false);
            stopImg.SetActive(true);
            playRecordBtn.SetActive(false);
        }
        else
        {
            StopRecording();
            micImg.SetActive(true);
            stopImg.SetActive(false);
            playRecordBtn.SetActive(true);
        }
    }

    public void PlayRecordedClip()
    {
        audioSource.clip = recordedClip;
        audioSource.Play();
    }

    public void CreateButton()
    {
        if (userInput != null && choosenImg != null /*&& recordedClip != null*/)
        {
            int categoryIndex = dropdown.value;

            GameObject newButton = Instantiate(buttonPrefab, contentFieldPanel[categoryIndex].transform);

            // Set the image component's source image to the selected image
            Image imageComponent = newButton.GetComponentInChildren<Image>();
            imageComponent.sprite = Sprite.Create(choosenImg, new Rect(0, 0, choosenImg.width, choosenImg.height), new Vector2(0.5f, 0.5f));

            // Set the text component's text field to the entered name
            TextMeshProUGUI textComponent = newButton.GetComponentInChildren<TextMeshProUGUI>();
            textComponent.text = userInput;

            // Set the audio source component's audio clip to the selected sound
            //AudioSource audioSourceComponent = newButton.GetComponent<AudioSource>();
            //audioSourceComponent.clip = recordedClip;

            theSaveManager.SaveButtonData(newButton);

            ResetCreateData();
        }
    }


    private void ResetCreateData()
    {
        userInput = null;
        choosenImg = null;
        micImg.SetActive(true);
        stopImg.SetActive(false);
        playRecordBtn.SetActive(false);
        onRecord = false;
        recordedClip = null;

        displayImg.sprite = null;
        namaInputField.text = "";
    }
}
