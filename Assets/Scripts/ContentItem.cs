using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentItem : MonoBehaviour
{
    public Saving saving;

    public CharacterAdder characterAdder;

    public GameObject outline;
    public Image outlineImage;

    public Image image;
    public Text characterText;
    public Text statText;

    public Image editImage;
    public Button editButton;
    public Text editText;

    public Image deleteImage;
    public Button deleteButton;
    public Text deleteText;

    public void DeleteSelf()
    {
        saving.saveFile.data.Remove(characterText.text);
        saving.Save();
        Destroy(gameObject);
    }

    public void EditSelf()
    {
        characterAdder.OpenToEdit(characterText, statText);
    }
}
