using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponInteraction : MonoBehaviour
{
    public List<Weapon> weapons = new List<Weapon>();
    Weapon selectedWeapon;

    [SerializeField] InputActionReference activateWeapon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        activateWeapon.action.performed += OnActivateWeapon;
        activateWeapon.action.canceled += OnReleaseWeapon;
        SetSelectedWeapon(0);
        Debug.Log("Initialiazed");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSelectedWeapon(int index)
    {
        selectedWeapon = weapons[index];
        selectedWeapon.gameObject.SetActive(true);

        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i] != selectedWeapon)
            {
                weapons[i].gameObject.SetActive(false);
            }
        }
    }

    public void OnActivateWeapon(InputAction.CallbackContext context)
    {
        selectedWeapon.ActivateWeapon();
        Debug.Log("Input");
    }

    public void OnReleaseWeapon(InputAction.CallbackContext context)
    {
        selectedWeapon.ReleaseWeapon();
    }
}
