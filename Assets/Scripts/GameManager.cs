using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] public GameObject alien;

    [SerializeField] private int maxAliensOnScreen;
    [SerializeField] private int totalAliens;
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;
    [SerializeField] private int aliensPerSpawn;

    private int aliensOnScreen = 0;
    private float generatedSpawnTime = 0;
    private float currentSpawnTime = 0;

    public GameObject upgradePrefab;
    public Gun gun;
    public float upgradeMaxTimeSpawn = 7.5f;

    private bool spawnedUpgrade = false;
    private float actualUpgradeTime = 0;
    private float currentUpgradeTime = 0;

    public GameObject deathFloor;

    public void AlienDestroyed()
    {
        aliensOnScreen -= 1;
        totalAliens -= 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        actualUpgradeTime = Random.Range(upgradeMaxTimeSpawn - 3.0f, upgradeMaxTimeSpawn);
        actualUpgradeTime = Mathf.Abs(actualUpgradeTime);
    }

    // Update is called once per frame
    void Update()
    {
        currentSpawnTime += Time.deltaTime;

        currentUpgradeTime += Time.deltaTime;

        if (player == null)
        {
            return;
        }

        if (currentUpgradeTime > actualUpgradeTime)
        {
            
            if (!spawnedUpgrade)
            {
                
                int randomNumber = Random.Range(0, spawnPoints.Length - 1);
                GameObject spawnLocation = spawnPoints[randomNumber];
                
                GameObject upgrade = Instantiate(upgradePrefab) as GameObject;
                Upgrade upgradeScript = upgrade.GetComponent<Upgrade>();
                upgradeScript.gun = gun;
                upgrade.transform.position = spawnLocation.transform.position;

                spawnedUpgrade = true;

                //SoundManager.Instance.PlayOneShot(SoundManager.Instance.powerUpAppear);
            }
        }

        if (currentSpawnTime > generatedSpawnTime)
        {
            currentSpawnTime = 0;
        }
       
        generatedSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);

        if(aliensPerSpawn > 0 && aliensOnScreen < totalAliens)
        {
            List<int> previousSpawnLocations = new List<int>();

            if (aliensPerSpawn > spawnPoints.Length)
            {
                aliensPerSpawn = spawnPoints.Length - 1;
            }
            aliensPerSpawn = (aliensPerSpawn > totalAliens) ? aliensPerSpawn - totalAliens : aliensPerSpawn;

            for (int i = 0; i < aliensPerSpawn; i++)
            {
                if (aliensOnScreen < maxAliensOnScreen)
                {
                    aliensOnScreen += 1;

                    int spawnPoint = -1;

                    while (spawnPoint == -1)
                    {
                        int randomNumber = Random.Range(0, spawnPoints.Length - 1);
                        if (!previousSpawnLocations.Contains(randomNumber))
                        {
                            previousSpawnLocations.Add(randomNumber);
                            spawnPoint = randomNumber;
                        }
                    }

                    GameObject spawnLocation = spawnPoints[spawnPoint];
                    GameObject newAlien = Instantiate(alien) as GameObject;

                    newAlien.transform.position = spawnLocation.transform.position;

                    Alien alienScript = newAlien.GetComponent<Alien>();
                    alienScript.target = player.transform;

                    Vector3 targetRotation = new Vector3(player.transform.position.x, newAlien.transform.position.y, player.transform.position.z);
                    newAlien.transform.LookAt(targetRotation);

                    alienScript.OnDestroy.AddListener(AlienDestroyed);

                    alienScript.GetDeathParticles().SetDeathFloor(deathFloor);
                }
            }
        }

        
    }
}
