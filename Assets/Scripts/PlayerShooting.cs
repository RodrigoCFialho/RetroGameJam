using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    //Shooting Objects
    [SerializeField] private GameObject weapon;

    //Shooting Variables
    private bool playerHasWeapon = true;
    private bool canPickWeapon = false;

    public void EnableShootingEvent()
    {
        if(playerHasWeapon)
        {
            canPickWeapon = false;
            playerHasWeapon = false;
            weapon.SetActive(true);
            weapon.transform.position = this.transform.position;
            weapon.GetComponent<PlayerWeapon>().Shoot();
            StartCoroutine(CanPickWeapon());
        }
    }

    private IEnumerator CanPickWeapon()
    {
        yield return new WaitForSeconds(.025f);
        canPickWeapon = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!playerHasWeapon && canPickWeapon && other.gameObject.CompareTag("Weapon"))
        {
            other.transform.position = gameObject.transform.position;
            other.transform.rotation = Quaternion.identity;
            other.transform.parent = gameObject.transform;
            playerHasWeapon = true;
            weapon.SetActive(false);
        }
    }
}