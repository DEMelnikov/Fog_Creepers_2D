using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private GameObject _runePrefab;
    [SerializeField] private GameObject _heroPrefab;
    [SerializeField] private GameObject _defaultEmemy;
    [SerializeField] private int _heroesLimit = 3;
    [SerializeField] private int _enemiesAtStart = 1;
    [SerializeField] private int _runesLimit = 3;

    //[SerializeField] private GameObject test;
    //private List<GameObject> _runes;

    //private int _runeRandomPositionLimitX = 9;
    //private int _runeRandomPositionLimitYMax = 7;
    //private int _runeRandomPositionLimitYMin = -3;
    //private Camera _camera;
    private LayerMask layerMaskCheckCollisions;

    //private int _heroLowestPosition = -5;

    //private Random _random = new Random();

    private SpawnZone heroesSpawnZone = new SpawnZone(-8f, -6f, 1f, 10f);
    private SpawnZone runesSpawnZone = new SpawnZone(-8f, -1f, 6f, 14f);
    private SpawnZone[] enemySpawnZones = { new SpawnZone(-8,-1,6f,1f),
        new SpawnZone(-8f, 4f, 1f, 14f),
        new SpawnZone(7f, -1f, 6f, 1f)};
        

    private void Awake()
    {
        //SpawnZone heroesSpawnZone = new SpawnZone(-8f,-6f,1f,10f);

        layerMaskCheckCollisions = 3;

        Vector3 randomPosition = new Vector3 (0,0,0);

        for (int i = 0; i < _runesLimit; i++)
        {
            Instantiate(_runePrefab, GenerateRandomVector3EmptyGround(runesSpawnZone), Quaternion.identity).name = "Rune";
        }

        for (int i = 0; i < _heroesLimit; i++)
        {
            GameObject newHero = Instantiate(_heroPrefab, GenerateRandomVector3EmptyGround(heroesSpawnZone), Quaternion.identity);
            newHero.name = "Hero";
        }

        GameObject mageHero = Instantiate(_heroPrefab, GenerateRandomVector3EmptyGround(heroesSpawnZone), Quaternion.identity);
        mageHero.GetComponentInChildren<HeroAI>().SetMage();
        mageHero.name = "Mage";


        for (int i = 0; i < _enemiesAtStart; i++)
        {
            int zoneIndex = Random.Range(0, enemySpawnZones.Length);
            print("ZI " +zoneIndex );
            GameObject enemy = Instantiate(_defaultEmemy, GenerateRandomVector3EmptyGround(enemySpawnZones[zoneIndex]), Quaternion.identity);
            enemy.name = "Enemy";
        }

    }

    void Start()
    {
        //Vector3 randomPosition = new Vector3(2, 2, 0);
        //if (IsGround(randomPosition))
        //{
        //    print("Ground");
        //}
    }

    void Update()
    {
        
    }

    private Vector3 GenerateRandomVector3EmptyGround(SpawnZone spawnZone)

    {
        Vector3 randomPosition = new Vector3(0, 0, 0);

       // do
        //{
            randomPosition.x = Random.Range(spawnZone.X, spawnZone.X+spawnZone.Width);
            randomPosition.y = Random.Range(spawnZone.Y, spawnZone.Y+spawnZone.Height);
            randomPosition.z = 0;

        //Physics2D.OverlapCircle(randomPosition, 130f, layerMaskCheckCollisions);
        //print(Physics2D.OverlapCircle(randomPosition,100f, layerMaskCheckCollisions));
        //Gizmos.DrawWireSphere(randomPosition,100f);
       // Instantiate(test, randomPosition, Quaternion.identity);
        //} while (IsGround(randomPosition)==false);s

        return randomPosition;
    }

    private class SpawnZone
    {
        public float Y { get; }
        public float X { get; }
        public float Height { get; }
        public float Width { get; }

        public SpawnZone(float x, float y, float height, float width)
        {
            X = x;
            Y = y;
            Height = height;
            Width = width;
        }
    }
    //private bool IsGround(Vector3 position)
    //{
    //    Instantiate(test,position,Quaternion.identity);
    //    if (Physics.CheckSphere(position, 0.5f))
    //    {
    //        Debug.Log("Generator Collision");
    //        return true;
    //    }

    //    return false;
    //}
}
