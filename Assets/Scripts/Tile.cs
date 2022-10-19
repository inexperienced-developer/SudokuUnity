using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public int value;
    public bool defaultVisible;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text valueText;
    public bool Solved { get; private set; }

    public void DefaultVisible(bool value)
    {
        Solved = value;
        defaultVisible = value;
        valueText.gameObject.SetActive(value);
    }

    public void SelectTile(bool value)
    {
        image.color = value ? Color.green : Color.white;
    }

    public void TryValue(int value)
    {
        valueText.text = value.ToString();
        valueText.gameObject.SetActive(true);
        bool correct = value == this.value;
        image.color = correct ? Color.white : Color.red;
        Solved = correct;
    }

    public void SetColor(Color color)
    {
        image.color = color;
    }
}
