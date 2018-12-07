using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class ListContent : MonoBehaviour
{
    public ThemeSetter themeSetter;
    public Saving saving;
    public ContentItem contentItem;
    public CharacterAdder characterAdder;
    public List<ContentItem> items;

    void Start()
    {
        Reload();

        int turn = saving.saveFile.turn;
        if (items.Count > 0) items[turn].outline.SetActive(true);
    }

    public ContentItem CreateItem(string character, string stat)
    {
        ContentItem newItem = Instantiate(contentItem, transform);
        newItem.saving = saving;
        newItem.characterAdder = characterAdder;

        themeSetter.AddImage(newItem.image);
        newItem.characterText.text = character;
        themeSetter.AddText(newItem.characterText);
        newItem.statText.text = stat;
        themeSetter.AddText(newItem.statText);

        themeSetter.AddHeavyImage(newItem.outlineImage);

        themeSetter.AddImage(newItem.editImage);
        themeSetter.AddButton(newItem.editButton);
        themeSetter.AddText(newItem.editText);

        themeSetter.AddImage(newItem.deleteImage);
        themeSetter.AddButton(newItem.deleteButton);
        themeSetter.AddText(newItem.deleteText);

        items.Add(newItem);

        return newItem;
    }

    public void AddItem(string character, string stat)
    {
        CleanList();

        ContentItem newItem = CreateItem(character, stat);

        saving.saveFile.data.Add(newItem.characterText.text, Convert.ToInt32(newItem.statText.text));
        saving.Save();

        SortList();
    }

    public void Reload()
    {
        foreach (KeyValuePair<string, int> entry in saving.saveFile.data)
        {
            CreateItem(entry.Key, entry.Value.ToString());
        }

        SortList();
    }

    public void ClearAll()
    {
        saving.saveFile.data = new Dictionary<string, int>();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        items.Clear();

        saving.Save();
    }

    public void SortList()
    {
        CleanList();

        saving.saveFile.data = saving.saveFile.data.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

        int i = 0;
        foreach (KeyValuePair<string, int> entry in saving.saveFile.data)
        {
            items[i].characterText.text = entry.Key;
            items[i].statText.text = entry.Value.ToString();
            i++;
        }
    }

    public void CleanList()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null )
            {
                items.RemoveAt(i);
                i--;
            }
        }
    }

    public void NextTurn()
    {
        if (items.Count > 0)
        {
            int turn = saving.saveFile.turn;
            items[turn].outline.SetActive(false);
            turn++;
            if (turn >= items.Count) turn = 0;
            items[turn].outline.SetActive(true);
            saving.saveFile.turn = turn;
            saving.Save();
        }
    }

    public void PreviousTurn()
    {
        if (items.Count > 0)
        {
            int turn = saving.saveFile.turn;
            items[turn].outline.SetActive(false);
            turn--;
            if (turn < 0) turn = items.Count - 1;
            items[turn].outline.SetActive(true);
            saving.saveFile.turn = turn;
            saving.Save();
        }
    }
}