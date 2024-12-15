using System.Collections;
using UnityEditor.Animations;
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

    //Healing Variables
    [SerializeField] private int healPerEnemy = 0;

    [SerializeField]
    private AnimatorController withWeaponController;

    [SerializeField]
    private AnimatorController withoutWeaponController;

    private Animator myAnimator;

    private void Awake() {
        myAnimator = GetComponent<Animator>();
    }

    public void EnableShootingEvent()
    {
        if(playerHasWeapon) {
            myAnimator.SetBool("IsAttacking", true);
        }
    }

    public void Shoot() 
    {
        canPickWeapon = false;
        playerHasWeapon = false;

        weapon.SetActive(true);
        weapon.transform.position = this.transform.position;

        weapon.GetComponent<PlayerWeapon>().Shoot();
        StartCoroutine(CanPickWeapon());

        myAnimator.SetBool("IsAttacking", false);
        myAnimator.runtimeAnimatorController = withoutWeaponController;
    }

    private IEnumerator CanPickWeapon()
    {
        yield return new WaitForSeconds(.5f);//Este valor tem de ser sempre igual à duração da animação do bounce
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
            int enemiesHit = weapon.GetComponent<PlayerWeapon>().GetEnemiesHit();
            int healtToRegen = healPerEnemy * enemiesHit;
            gameObject.GetComponent<HP_Manager>().RegenHP(healtToRegen);
            weapon.SetActive(false);
            myAnimator.runtimeAnimatorController = withWeaponController;
        }
    }
}