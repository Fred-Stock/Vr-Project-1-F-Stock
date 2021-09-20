using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{

    private Queue<GameObject> bolts;


    public void PushBolt(GameObject newBolt)
    {
        bolts.Enqueue(newBolt);
    }

    public void PopBolt()
    {
        bolts.Dequeue();
    }



}
