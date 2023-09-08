using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameManager gameManager;

    [Space(10)]

    [SerializeField] private string seed;
    [SerializeField] private bool useSeed;

    public float PLATFORM_X_SIZE { get; private set; } = 1.0f;
    public float PLATFORM_Y_SIZE { get; private set; } = 0.5f;
    public float PLATFORM_PADDING { get; private set; } = 0.1f;

    // the smaller the number, the closer the player can be to the top of the staircase without rearranging
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 12.5f;

    [SerializeField] private GameObject levelStart;
    [SerializeField] private List<GameObject> platformTypes;

    private List<GameObject> activePlatforms = new List<GameObject>();

    private int platformCount = 35;
    private GameObject lastPlatform;


    // Start is called before the first frame update
    void Awake()
    {
        // determines if the level is to be generated using a seed or have it randomly generate one
        UseSeededGeneration();

        // Initializes the starting platform creation of the level
        InitalizePlatforms();

        // starts the player facing the right way initially so they dont have an immediate fail (unless they rotate first)
        if (activePlatforms[1].transform.position.x > 0){
            gameManager.player.Flip();
        }
    }


    // Update is called once per frame
    void Update()
    {
        // if the player position is less than the desired spacing, move the first platform up
        if (Vector3.Distance(gameManager.player.transform.position, lastPlatform.transform.position) < PLAYER_DISTANCE_SPAWN_LEVEL_PART){
            RearragePlatform();
        }
    }


    // determines if the level is to be generated using a seed or have it randomly generate one
    private void UseSeededGeneration()
    {
        // if a seed is provided and we want to generate the leel via a seed, then activiate the seed
        if (seed != "" && useSeed)
        {
            Random.InitState(int.Parse(seed));
        }
        // randomly generates a seed from 1-1000000000 if no seed is given
        else
        {
            int randSeed = Random.Range(1, 1000000000);
            Random.InitState(randSeed);

            seed = randSeed.ToString();
            Debug.Log(seed);

        }
    }


    // Initializes the starting platform creation of the level
    private void InitalizePlatforms()
    {
        // makes the starting platform the last known platform position and adds it to the active platforms list
        lastPlatform = levelStart;
        activePlatforms.Add(levelStart);

        // Spawn the set amount of platforms set by the platformCount variable
        for (int i = activePlatforms.Count; i < platformCount; i++)
        {
            NewPlatform();
        }
    }


    //
    private void NewPlatform()
    {
        // spawn in a new platform prefab from the platform types list and places it in the last platforms position
        GameObject newPlatform = Instantiate(platformTypes[Random.Range(0, platformTypes.Count)], lastPlatform.transform.position, Quaternion.identity);

        // modifies the position of the spawned platform to left/right and then up from the last platform in the list
        newPlatform.transform.position = NewPlatformPosition(newPlatform.transform);

        // adds the newly spawned platform to the last known platform position and to the active Platforms list
        lastPlatform = newPlatform.gameObject;
        activePlatforms.Add(newPlatform.gameObject);

    }


    // rearrages the platform from the oldest(first) on the list back to the newest(last) on the list, then giving it a new position
    private void RearragePlatform()
    {
        // selects the oldest platform on the list (first>last) and removes it from the list
        GameObject movingPlatform = activePlatforms[0];
        activePlatforms.RemoveAt(activePlatforms.IndexOf(movingPlatform));

        // modifies the position of the moving platform to left/right and then up from the last platform in the list
        movingPlatform.transform.position = NewPlatformPosition(lastPlatform.transform);

        // re-adds the newly moved platform to the last known platform position and to the active Platforms list
        lastPlatform = movingPlatform;
        activePlatforms.Add(movingPlatform);
    }


    // rules set to decide the platforms new position
    private Vector3 NewPlatformPosition(Transform platform)
    {
        int _randNum = Random.Range(1, 101);
        Vector3 _newPlatformPosition = platform.transform.position;

        if (activePlatforms.Count < 2)
        {
            if (_randNum <= 50)
                _newPlatformPosition.x -= (PLATFORM_X_SIZE + PLATFORM_PADDING);
            else
                _newPlatformPosition.x += (PLATFORM_X_SIZE + PLATFORM_PADDING);
        }

        // tries to give a slightly higher priority to keeping stairs going in the same direction
        // checking if the last platform is to the right of the platform before it
        else
        {
            float _lastPlatPos = lastPlatform.transform.position.x;
            float _prevPlatPos = activePlatforms[activePlatforms.Count - 2].transform.position.x;

            if (_lastPlatPos > _prevPlatPos)
            {
                if (_randNum <= 65)
                    _newPlatformPosition.x += (PLATFORM_X_SIZE + PLATFORM_PADDING);
                else
                    _newPlatformPosition.x -= (PLATFORM_X_SIZE + PLATFORM_PADDING);
            }
            else
            {
                if (_randNum <= 65)
                    _newPlatformPosition.x -= (PLATFORM_X_SIZE + PLATFORM_PADDING);
                else
                    _newPlatformPosition.x += (PLATFORM_X_SIZE + PLATFORM_PADDING);
            }
        }

        _newPlatformPosition.y += (PLATFORM_Y_SIZE + PLATFORM_PADDING);
        return _newPlatformPosition;
    }
}
