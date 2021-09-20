using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{

    [SerializeField] private float lifeTime;
    [SerializeField] private float timeAlive;

    public void Update()
    {
        timeAlive += Time.deltaTime;

        if(timeAlive > lifeTime)
        {
            Delete();
        }
    }

    private void OnEnable()
    {
        Debug.Log(gameObject.name);
        GameObject.Find("GameManager").GetComponent<GameData>().PushBolt(gameObject);
    }

    public void Delete()
    {
        GameObject.Find("GameManager").GetComponent<GameData>().PopBolt();
        Destroy(gameObject);
    }

}
