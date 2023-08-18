using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 35.0f;
    [HideInInspector] public const float PLATFORM_X_SPACING = 3.0f;
    [HideInInspector] public const float PLATFORM_Y_SPACING = 1.5f;

    [SerializeField] private Player player;

    [SerializeField] private GameObject levelStart;
    [SerializeField] private List<GameObject> platformTypes;

    public List<GameObject> activePlatforms = new List<GameObject>();

    private int platformCount = 35;
    private GameObject lastPlatform;

    // Start is called before the first frame update
    void Awake()
    {
        // makes the starting platform the last known platform position and adds it to the active platforms list
        lastPlatform = levelStart;
        activePlatforms.Add(levelStart);

        // spawns the amount of platforms set above to make them appear offscreen
        for (int i = activePlatforms.Count ; i < platformCount ; i++){
            SpawnPlatform();
        }

        // starts the player facing the right way initially so they dont have an immediate fail (unless they rotate first)
        if (activePlatforms[1].transform.position.x > 0){
            player.Flip();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, lastPlatform.transform.position) < PLAYER_DISTANCE_SPAWN_LEVEL_PART){
            RearragePlatforms();
        }
    }

    private void SpawnPlatform()
    {
        // spawn in a new platform from the platform Types list and places it in the last platforms position initally
        GameObject newPlatform = Instantiate(platformTypes[Random.Range(0, platformTypes.Count)], lastPlatform.transform.position, Quaternion.identity);

        // modifies the position of the spawned platform to left/right and then up from the last platform in the list
        newPlatform.transform.position = NewPosition(newPlatform.transform);

        // adds the newly spawned platform to the last known platform position and to the active Platforms list
        lastPlatform = newPlatform.gameObject;
        activePlatforms.Add(newPlatform.gameObject);

    }

    private void RearragePlatforms()
    {
        // selects the oldest platform on the list (first>last) and removes it from the list
        GameObject movingPlatform = activePlatforms[0];
        activePlatforms.RemoveAt(activePlatforms.IndexOf(movingPlatform));

        // modifies the position of the moving platform to left/right and then up from the last platform in the list
        movingPlatform.transform.position = NewPosition(lastPlatform.transform);

        // re-adds the newly moved platform to the last known platform position and to the active Platforms list
        lastPlatform = movingPlatform;
        activePlatforms.Add(movingPlatform);
    }

    private Vector3 NewPosition(Transform platform)
    {
        Vector3 newPosition = platform.transform.position;

        // modifies the position of the spawned platform to left/right and then up
        if (Random.Range(0, 2) == 0){
            newPosition.x -= PLATFORM_X_SPACING;
        }
        else{
            newPosition.x += PLATFORM_X_SPACING;
        }

        newPosition.y += PLATFORM_Y_SPACING;
        return newPosition;
    }
}
