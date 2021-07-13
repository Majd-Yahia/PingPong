using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWallSpawner : MonoBehaviour
{

    public GameObject wallPrefab;

    // Range of (x,y) for wall to spawn in.
    private float horizontalMax = 200f;
    private float horizontalMin = -200f;
    private float verticalMax = 200f;
    private float verticalMin = -200f;
    private float minHeight = 80f;
    private float maxHeight = 150f;
    private Vector2 vectorError;
    private Vector2 position;

    public void SpawnWall(float wait)
    {
        position = new Vector2(GetRandomFloatHorizontel(), GetRandomFloatVertical());

        // subtract vectorError from position.y to find the y distance between old and new wall.
        float yError = Mathf.Abs(vectorError.y - position.y);
        float height = GetRandomFloatInRnage();

        // fix error in position.
        if (vectorError != Vector2.zero)
        {
            if (yError < 100)
            {
                if ((position.y > 0) && (position.y + 150f) >= 300)
                {
                    position.y = 300f;
                }
                else if ((position.y < 0) && (position.y + 150f) <= -300)
                {
                    position.y = -300f;
                }
                else
                {
                    position.y += 150f;
                }
            }
        }

        // Vector error is the position for wall 1.
        vectorError = position;

        // spawn the wall.
        GameObject wall = Instantiate(wallPrefab, position, Quaternion.identity);

        // Change the height of the wall.
        wall.transform.localScale = new Vector3(10, height, 2);

        // Attach a script to auto destroy the wall.
        wall.AddComponent<DestroyWall>();
        wall.GetComponent<DestroyWall>().waitUntilDestroy = wait;
    }

    private float GetRandomFloatHorizontel()
    {
        return Random.Range(horizontalMin, horizontalMax);
    }
    private float GetRandomFloatVertical()
    {
        return Random.Range(verticalMin, verticalMax);
    }




    private float GetRandomFloatInRnage()
    {
        return Random.Range(minHeight, maxHeight);
    }


}
