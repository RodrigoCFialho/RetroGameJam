using System.Collections;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour 
{
    //Components
    private Rigidbody2D weaponRigidbody = null;
    private Animator weaponAnimator = null;

    //Movement Variables
    [SerializeField] private float maxTravelDistance = 5f;
    [SerializeField] private float maxTravelTime = 0.1f;

    //Detection Variables
    private bool isDetectingEnemies = false;
    [SerializeField] private Vector2 detectionSize;
    [SerializeField] private LayerMask enemiesLayerMask;
    private RaycastHit2D[] enemiesDetected;
    private int enemiesHit = 0;

    //Bounce Variables
    [SerializeField] private Vector2 bounceDistanceVector;
    [SerializeField] private float maxBounceTime;

    //Sound
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AudioClip[] audioClips;

    private void Awake() 
    {
        weaponAnimator = GetComponent<Animator>();
        weaponRigidbody = GetComponent<Rigidbody2D>();
    }

    public void Shoot(GameObject player) 
    {
        this.transform.position = player.transform.position;
        gameObject.SetActive(true);

        //get shooting angle
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 shootingVector = new Vector2(worldPosition.x - gameObject.transform.position.x,
            worldPosition.y - gameObject.transform.position.y).normalized;

        //shooting angle using the max range of the throw
        Vector2 shootingPath = shootingVector * maxTravelDistance;

        Vector2 initialPosition = gameObject.transform.position;
        float travelTime = 0.0f;
        DetectAllEnemiesInPath(shootingVector);
        StartCoroutine(Move(travelTime, initialPosition, shootingPath));
    }

    private void DetectAllEnemiesInPath(Vector2 shootingVector) 
    {
        enemiesDetected = Physics2D.RaycastAll(transform.position, shootingVector, maxTravelDistance);
    }

    private IEnumerator Move(float travelTime, Vector2 initialPosition, Vector2 shootingPath) 
    {
        isDetectingEnemies = true;
        enemiesHit = 2;//Deteta sempre 2 a mais, n�o sei porqu�

        while(travelTime < maxTravelTime && isDetectingEnemies) 
        {
            travelTime += Time.deltaTime;
            float lerpFactor = travelTime / maxTravelTime;
            gameObject.transform.position = Vector3.Lerp(initialPosition, initialPosition + shootingPath, lerpFactor);
            yield return null;
        }
        isDetectingEnemies = false;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(isDetectingEnemies && other.gameObject.CompareTag("Enemy")) 
        {
            enemiesHit++;
            //            if (enemiesHit == 3)
            //           {
            playerAudioSource.clip = audioClips[0];
            playerAudioSource.Play();
            //            }

            other.GetComponent<EnemyController>().WasHit();
            if(enemiesHit >= enemiesDetected.Length) {
                isDetectingEnemies = false;
                Bounce();
            }
        }
    }

    private void Bounce() 
    {
        playerAudioSource.clip = audioClips[1];
        playerAudioSource.Play();
        weaponRigidbody.velocity = Vector2.zero;
        weaponAnimator.SetTrigger("Bounce");
        StartCoroutine(GoUp());
    }

    private IEnumerator GoUp() 
    {
        float bounceTime = 0.0f;
        Vector2 initialBouncePosition = transform.position;

        while(bounceTime < maxBounceTime) {
            bounceTime += Time.deltaTime;
            float lerpFactor = bounceTime / maxBounceTime;
            gameObject.transform.position = Vector2.Lerp(initialBouncePosition, initialBouncePosition + bounceDistanceVector, lerpFactor);
            yield return null;
        }
    }

    private void GoDown()//Called by animation event
    {
        StartCoroutine(ResetPositionAfterBounce());
    }

    private IEnumerator ResetPositionAfterBounce() 
    {
        float bounceTime = 0.0f;
        Vector2 initialBouncePosition = transform.position;

        while(bounceTime < maxBounceTime) {
            bounceTime += Time.deltaTime;
            float lerpFactor = bounceTime / maxBounceTime;
            gameObject.transform.position = 
                Vector2.Lerp(initialBouncePosition, initialBouncePosition - bounceDistanceVector, lerpFactor);
            yield return null;
        }
    }

    public int GetEnemiesHit() 
    {
        return enemiesHit - 2;
    }
}