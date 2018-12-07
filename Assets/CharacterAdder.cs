using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CharacterAdder : MonoBehaviour
{
    public Saving saving;
    public ListContent listContent;
    public Text title;
    public Text error;
    public InputField characterText;
    public InputField statText;

    public Text editCharacter;
    public Text editStat;
    public bool editing;

    public void Open()
    {
        editing = false;
        title.text = "Add a Character";
        gameObject.SetActive(true);
        characterText.text = "";
        statText.text = "";
    }

    public void OpenToEdit(Text character, Text stat)
    {
        editing = true;
        title.text = "Edit this Character";
        editCharacter = character;
        editStat = stat;
        gameObject.SetActive(true);
        characterText.text = editCharacter.text;
        statText.text = editStat.text;
    }

    public void Accept()
    {
        if (characterText.text != "" && statText.text != "")
        {
            if (editing)
            {
                if (characterText.text == editCharacter.text)
                {
                    saving.saveFile.data[editCharacter.text] = Convert.ToInt32(statText.text);

                    editCharacter.text = characterText.text;
                    editStat.text = statText.text;

                    gameObject.SetActive(false);
                    error.text = "";

                    saving.Save();

                    listContent.SortList();
                }
                else
                {
                    bool found = false;

                    foreach (KeyValuePair<string, int> entry in saving.saveFile.data)
                    {
                        if (entry.Key == characterText.text) found = true;
                    }

                    if (!found)
                    {
                        saving.saveFile.data.Remove(editCharacter.text);
                        saving.saveFile.data.Add(characterText.text, Convert.ToInt32(statText.text));
                        saving.Save();

                        editCharacter.text = characterText.text;
                        editStat.text = statText.text;

                        gameObject.SetActive(false);
                        error.text = "";

                        saving.Save();

                        listContent.SortList();
                    }
                    else
                    {
                        error.text = "A character with that name already exists!";
                    }
                }
            }
            else
            {
                bool found = false;

                foreach (KeyValuePair<string, int> entry in saving.saveFile.data)
                {
                    if (entry.Key == characterText.text) found = true;
                }

                if (!found)
                {
                    listContent.AddItem(characterText.text, statText.text);

                    gameObject.SetActive(false);
                    error.text = "";
                }
                else
                {
                    error.text = "A character with that name already exists!";
                }
            }
        }
        else
        {
            error.text = "Please fill out both fields!";
        }
        
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
        error.text = "";
    }
}
