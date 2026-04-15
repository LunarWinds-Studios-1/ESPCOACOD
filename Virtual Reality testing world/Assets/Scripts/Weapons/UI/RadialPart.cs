using UnityEngine;
using UnityEngine.UI;

public class RadialPart : MonoBehaviour
{
    Button button;
    Image image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
    }

    public void SetSelected(bool selected)
    {
        if (selected)
        {
            image.color = button.colors.selectedColor;
        } else
        {
            image.color = button.colors.normalColor;
        }
    }
}
