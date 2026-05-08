using UnityEngine;

public class HarpoonAnimationHandler : MonoBehaviour
{
    public HarpoonGun gun;
    

    public void OnAnimationStart()
    {
        gun.OnAnimationStart();
    }

    public void OnAnimationEnd()
    {

        gun.OnAnimationEnd(); 
    }

}
