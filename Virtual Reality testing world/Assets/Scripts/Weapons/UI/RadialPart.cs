using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RadialPart : MonoBehaviour
{
    Button button;
    Image image;

    [SerializeField] TextMeshProUGUI weaponNameDisplay;
    [SerializeField] RectTransform display;

    public string weaponName = "Unnamed weapon";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
    }

    public void SetAngles(Vector3 eulerAngles, int numOfSegments)
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        float sliceSize = 360 / numOfSegments;
        float angle = sliceSize / 2 + image.rectTransform.eulerAngles.z - sliceSize;
        display.eulerAngles = new Vector3(eulerAngles.x, eulerAngles.y, angle);
        //weaponNameDisplay.rectTransform.eulerAngles = -display.eulerAngles;
        weaponNameDisplay.text = weaponName;
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
