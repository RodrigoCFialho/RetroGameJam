using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class PlayerShooting : MonoBehaviour 
{
    //Shooting Objects
    [SerializeField] private PlayerWeapon weaponScript;

    //Shooting Variables
    private bool playerHasWeapon = true;
    private bool canPickWeapon = false;

    //Healing Variables
    [SerializeField] private int healPerEnemy = 0;

    //Sound
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private List<AudioClip> audioClips;

    private Health healthScript;

    [SerializeField]
    private RuntimeAnimatorController withWeaponController;

    [SerializeField]
    private RuntimeAnimatorController withoutWeaponController;

    private Animator myAnimator;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        healthScript = GetComponent<Health>();
    }

    public void EnableShootingEvent() 
    {
        if (playerHasWeapon) 
        {
            myAnimator.SetBool("IsAttacking", true);
        }
    }
    
    public void Shoot() //called by Animation Event
    {
        //play audio
        playerAudioSource.clip = audioClips[0];
        playerAudioSource.Play();

        //bools
        canPickWeapon = false;
        playerHasWeapon = false;

        //enable shoot
        weaponScript.Shoot(gameObject);

        // start useless coroutine -> remove later ;-;
        StartCoroutine(CanPickWeapon());

        myAnimator.SetBool("IsAttacking", false);
        myAnimator.runtimeAnimatorController = withoutWeaponController;
    }

    private IEnumerator CanPickWeapon() // esta coroutine é useless é dar um ganda remove nisto ;-;
    {
        yield return new WaitForSeconds(.5f);//Este valor tem de ser sempre igual � dura��o da anima��o do bounce

        AudioClip weaponDropSFX = audioClips[1];
        playerAudioSource.PlayOneShot(weaponDropSFX);

        canPickWeapon = true;
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if (!playerHasWeapon && canPickWeapon && other.gameObject.CompareTag("Weapon")) 
        {
            //pickup weapon
            playerHasWeapon = true;
            weaponScript.gameObject.SetActive(false);
            myAnimator.runtimeAnimatorController = withWeaponController;

            //health regen
            int enemiesHit = weaponScript.GetEnemiesHit();

            if (enemiesHit > 0) 
            {
                AudioClip clipToPlay = audioClips[2];
                playerAudioSource.PlayOneShot(clipToPlay);
            }
            else
            {
                int healtToRegen = healPerEnemy * enemiesHit;
                healthScript.RegenHP(healtToRegen);
            }
        }
    }
}