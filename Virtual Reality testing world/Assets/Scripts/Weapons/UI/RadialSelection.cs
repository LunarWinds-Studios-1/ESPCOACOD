using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
public class RadialSelection : MonoBehaviour
{
    [SerializeField] GameObject radialPart;
    [Range(2, 10)] [SerializeField] int numberOfSegments;
    [SerializeField] float margins = 10f;

    Transform menuTransform;

    List<GameObject> parts = new List<GameObject>();

    [SerializeField] GameObject cursor;
    [SerializeField] InputActionReference rightStick;

    [SerializeField] WeaponInteraction weaponInteraction;
    float maxDistanceOfCursor = 86;

    int selectedIndex = -1;



    bool menuActive = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuTransform = transform;
        //SpawnRadialParts(numberOfSegments);
        rightStick.action.performed += SetCursorLocation;
        rightStick.action.canceled += ResetCursorLocation;

        numberOfSegments = weaponInteraction.weapons.Count;
    }

    // Update is called once per frame
    void Update()
    {
        cursor.GetComponent<Image>().enabled = menuActive;
        //Debug.Log(selectedIndex);
    }


    public void SetCursorLocation(InputAction.CallbackContext context)
    {
        Vector2 position = context.ReadValue<Vector2>().normalized * maxDistanceOfCursor;
        cursor.GetComponent<RectTransform>().anchoredPosition = position;
        if (!menuActive)
        {
            SpawnRadialParts(numberOfSegments);
            menuActive = true;
        }
        SetSelectedPart();
    }

    public static float FindDegree(float y, float x)
    {
        float value = (float)(Mathf.Atan2(y, x) *Mathf.Rad2Deg ) - 90;
        if (value < 0) value += 360f;
        if (value > 360) value -= 360;

        return value;
    }

    public void SetSelectedPart()
    {
        float angle;
        
        angle = FindDegree(cursor.GetComponent<Image>().rectTransform.anchoredPosition.y, cursor.GetComponent<Image>().rectTransform.anchoredPosition.x) + 360 / numberOfSegments;
        
        if (angle > 360)
        {
            angle -= 360;
        }

        selectedIndex = (int)angle * numberOfSegments / 360 ;
        SetButtonColors();

    }

    public void SetButtonColors()
    {
        for (int i = 0; i < numberOfSegments; i++)
        {
            bool selected = false;
            if (i == selectedIndex)
            {
                selected = true;
                Debug.Log("You are hovering over " + i);
            }

            parts[i]?.GetComponent<RadialPart>().SetSelected(selected);
        }
    }

    public void ResetCursorLocation(InputAction.CallbackContext context)
    {
        weaponInteraction.SetSelectedWeapon(selectedIndex);
        cursor.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        selectedIndex = -1;
        SpawnRadialParts(0);
        menuActive = false;
        
    }

    public void SpawnRadialParts(int num)
    {
        foreach (GameObject part in parts)
        {
            Destroy(part);
        }

        parts.Clear();

        for (int i = 0; i < num; i++)
        {
            float angle = i * 360 / num - margins / 2;
            Vector3 radialPartEulerAngle = new Vector3(menuTransform.eulerAngles.x, menuTransform.eulerAngles.y, menuTransform.eulerAngles.z + angle);

            GameObject part = Instantiate(radialPart, menuTransform);
            part.transform.position = menuTransform.position;
            part.transform.eulerAngles = radialPartEulerAngle;

            part.GetComponent<Image>().fillAmount = (1 / (float)num) - (margins / 360);

            parts.Add(part);
        }
        if (num > 0)
        {
            cursor.transform.parent = parts[parts.Count - 1].transform;
            cursor.transform.parent = gameObject.transform;
        }
    }
}
