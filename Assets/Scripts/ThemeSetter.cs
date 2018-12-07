using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeSetter : MonoBehaviour
{
    public Saving saving;

    [Header("For Images")]
    public Sprite sprite;
    public Color blank;

    [Header("Colors")]
    public Color light;
    public Color medium;
    public Color heavy;
    public Color textColor;
    public Color extreme;

    [Header("Themes")]
    public int chosenTheme;
    public List<Theme> availableThemes;

    [Header("Pool")]
    public List<Text> text;
    public List<Image> images;
    public List<Image> heavyImages;
    public List<Image> extremeImages;
    public List<Button> buttons;
    public List<Scrollbar> scrollbars;
    public List<InputField> inputFields;

    void Start()
    {
        chosenTheme = saving.saveFile.theme;

        if (availableThemes.Count >= chosenTheme) SetTheme(chosenTheme);
        else if (availableThemes.Count > 0 ) SetTheme(0);
    }

   
    public void SetTheme(int theme)
    {
        chosenTheme = theme;

        sprite = availableThemes[chosenTheme].sprite;
        light = availableThemes[chosenTheme].light;
        medium = availableThemes[chosenTheme].medium;
        heavy = availableThemes[chosenTheme].heavy;
        textColor = availableThemes[chosenTheme].textColor;
        extreme = availableThemes[chosenTheme].extreme;

        for (int i = 0; i < text.Count; i++)
        {
            if (text[i] == null)
            {
                text.RemoveAt(i);
                i--;
            }
            else text[i].color = textColor;
        }

        for (int i = 0; i < images.Count; i++)
        {
            if (images[i] == null)
            {
                images.RemoveAt(i);
                i--;
            }
            else
            {
                images[i].sprite = sprite;
                images[i].color = light;
            }
        }

        for (int i = 0; i < heavyImages.Count; i++)
        {
            if (heavyImages[i] == null)
            {
                heavyImages.RemoveAt(i);
                i--;
            }
            else
            {
                heavyImages[i].sprite = sprite;
                heavyImages[i].color = heavy;
            }
        }

        for (int i = 0; i < extremeImages.Count; i++)
        {
            if (extremeImages[i] == null)
            {
                extremeImages.RemoveAt(i);
                i--;
            }
            else
            {
                extremeImages[i].sprite = sprite;
                extremeImages[i].color = extreme;
            }
        }

        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i] == null)
            {
                buttons.RemoveAt(i);
                i--;
            }
            else
            {
                ColorBlock cb = buttons[i].colors;
                cb.normalColor = medium;
                cb.highlightedColor = heavy;
                cb.pressedColor = extreme;
                cb.disabledColor = heavy;

                buttons[i].colors = cb;
            }
        }

        for (int i = 0; i < scrollbars.Count; i++)
        {
            if (scrollbars[i] == null)
            {
                scrollbars.RemoveAt(i);
                i--;
            }
            else
            {
                ColorBlock cb = scrollbars[i].colors;
                cb.normalColor = light;
                cb.highlightedColor = medium;
                cb.pressedColor = heavy;
                cb.disabledColor = heavy;

                scrollbars[i].colors = cb;
            }
        }

        for (int i = 0; i < inputFields.Count; i++)
        {
            if (inputFields[i] == null)
            {
                inputFields.RemoveAt(i);
                i--;
            }
            else
            {
                ColorBlock cb = inputFields[i].colors;
                cb.normalColor = blank;
                cb.highlightedColor = medium;
                cb.pressedColor = heavy;
                cb.disabledColor = heavy;

                inputFields[i].colors = cb;
            }
        }

        saving.saveFile.theme = chosenTheme;
        saving.Save();
    }

    public void IncrementTheme()
    {
        chosenTheme++;
        if (chosenTheme >= availableThemes.Count) chosenTheme = 0;

        SetTheme(chosenTheme);
    }

    public void AddImage(Image image)
    {
        image.sprite = sprite;
        image.color = light;

        images.Add(image);
    }

    public void AddHeavyImage(Image image)
    {
        image.sprite = sprite;
        image.color = heavy;

        heavyImages.Add(image);
    }

    public void AddExtremeImage(Image image)
    {
        image.sprite = sprite;
        image.color = extreme;

        extremeImages.Add(image);
    }

    public void AddText(Text t)
    {
        t.color = textColor;

        text.Add(t);
    }

    public void AddButton(Button button)
    {
        ColorBlock cb = button.colors;
        cb.normalColor = light;
        cb.highlightedColor = medium;
        cb.pressedColor = heavy;
        cb.disabledColor = heavy;
        button.colors = cb;

        buttons.Add(button);
    }
}
