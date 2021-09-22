using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
  
    private Queue<GameObject> bolts = new Queue<GameObject>(); 
    public  LinkedList<GameObject> targets = new LinkedList<GameObject>();
    public TargetManager targetManager;
    public int score = 0;

    /// <summary>
    /// Adds new bolt to game data
    /// </summary>
    /// <param name="newBolt"></param>
    public void PushBolt(GameObject newBolt)
    {
        bolts.Enqueue(newBolt);
    }

    /// <summary>
    /// Removes the oldest Bolt
    /// Technically this could cause a slight issue if the user shoots really fast at two seperate targets
    /// But the bolts move so fast it would be very difficult to do and it would be barely noticible for the player
    /// so the extra performance of the queue seems worth it
    /// </summary>
    public void PopBolt()
    {
        bolts.Dequeue();
    }

    /// <summary>
    /// Adds new target to game data
    /// </summary>
    /// <param name="newTarget"></param>
    public void AddTarget(GameObject newTarget)
    {
        targets.AddFirst(newTarget);
    }

    /// <summary>
    /// Removes target from game data, updates player score, and adds the target's spawn location back to the list of valid locations
    /// </summary>
    /// <param name="shotTarget"></param>
    public void TargetHit(GameObject shotTarget)
    {
        targets.Remove(shotTarget);
        score++;

        targetManager.spawnLocations.Add(shotTarget.GetComponent<Target>().spawnedLocation);
    }

    /// <summary>
    /// deletes all targets from the scene
    /// </summary>
    public void ClearTargets()
    {
        while(targets.Count > 0)
        {
            targetManager.spawnLocations.Add(targets.First.Value.GetComponent<Target>().spawnedLocation);
            Destroy(targets.First.Value);
            targets.RemoveFirst();
        }
    }

}
