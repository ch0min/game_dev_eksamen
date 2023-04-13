using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitchScript : MonoBehaviour
{

    public GameObject pistol;
    public GameObject shotgun;
    public GameObject assaultRiffle;
    // Start is called before the first frame update
    void Start()
    {
        pistol.SetActive(false);
        shotgun.SetActive(false);
        assaultRiffle.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            pistol.SetActive(true);
            shotgun.SetActive(false);
            assaultRiffle.SetActive(false);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            pistol.SetActive(false);
            shotgun.SetActive(true);
            assaultRiffle.SetActive(false);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            pistol.SetActive(false);
            shotgun.SetActive(false);
            assaultRiffle.SetActive(true);
        }

    }
}
