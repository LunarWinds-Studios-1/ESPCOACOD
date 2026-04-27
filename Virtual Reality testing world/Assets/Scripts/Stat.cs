using UnityEngine;

public class Stat
{
    private float value;
    private string text;

    public bool newHighscore = false;

    public Stat(float value, string text)
    {
        this.value = value;
        this.text = text;
    }

    public float GetValue()
    {
        return value;
    }

    public void SetValue(float value)
    {
        this.value = value;
    }

    public string GetText()
    {
        return text;
    }

    public void SetText(string text)
    {
        this.text = text;
    }

    public void IncreaseStat(float value)
    {
        this.value += value;
    }
}
