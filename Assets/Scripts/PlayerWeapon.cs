using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerWeapon : MonoBehaviour {
    //Movement Variables
    [SerializeField] private float maxTravelDistance = 0.0f;
    [SerializeField] private float maxTravelTime = 0.0f;

    //Detection Variables
    private bool isDetectingEnemies = false;
    [SerializeField] private Vector3 detectionSize;
    [SerializeField] private LayerMask enemiesLayerMask;
    private Collider2D[] enemiesDetected;
    private int enemiesHit = 0;

    public void Shoot() {

        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0.0f;
        Vector3 shootingVector = new Vector3(worldPosition.x - gameObject.transform.position.x, worldPosition.y - gameObject.transform.position.y);
        shootingVector.Normalize();
        Vector3 shootingPath = shootingVector * maxTravelDistance;
        float cursorAngle = Mathf.Atan2(shootingVector.x, shootingVector.y) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, cursorAngle);
        gameObject.transform.parent = null;
        Vector3 initialPosition = gameObject.transform.position;
        float travelTime = 0.0f;
        print("shootingPath: " + shootingPath);//<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-
        print("shootingPath Length: " + shootingPath.magnitude);//<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-
        Vector3 offsetVector = gameObject.transform.position;
        offsetVector.x += (detectionSize.x / 2);
        DetectAllEnemiesInPath(offsetVector, cursorAngle);
        StartCoroutine(Move(travelTime, initialPosition, shootingPath));
    }

    private void DetectAllEnemiesInPath(Vector3 offsetVector, float cursorAngle) {
        enemiesDetected = Physics2D.OverlapBoxAll(offsetVector, detectionSize, cursorAngle);
    }

    private IEnumerator Move(float travelTime, Vector3 initialPosition, Vector3 shootingPath) {
        isDetectingEnemies = true;
        enemiesHit = 0;

        while(travelTime < maxTravelTime && isDetectingEnemies) {
            travelTime += Time.deltaTime;
            float lerpFactor = travelTime / maxTravelTime;
            gameObject.transform.position = Vector3.Lerp(initialPosition, initialPosition + shootingPath, lerpFactor);
            yield return null;
        }
        isDetectingEnemies = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(isDetectingEnemies && other.gameObject.CompareTag("Enemy")) {
            enemiesHit++;
            print("Enemies hit: " + enemiesHit);
            other.GetComponent<EnemyController>().Die();
            if(enemiesHit >= enemiesDetected.Length) {
                isDetectingEnemies = false;
                Bounce();
            }
        }
    }

    private void Bounce() {
        print("bounce");
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Vector3 offsetVector = detectionSize / 2;
        Vector3 startingPoint = new Vector3(transform.position.x + offsetVector.x, transform.position.y, transform.position.z);
        Gizmos.DrawWireCube(startingPoint, detectionSize);
    }
}