using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectImage : MonoBehaviour
{
    private Texture2D choosenImg;
    public Image displayImg;
    public void SelectPics()
    {
        choosenImg = GetSelectedImage();

        displayImg.sprite = Sprite.Create(choosenImg, new Rect(0, 0, choosenImg.width, choosenImg.height), new Vector2(0.5f, 0.5f));
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
}
