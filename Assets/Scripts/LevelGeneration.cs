using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelGeneration : MonoBehaviour
{
    public int levelLength = 0;
    [SerializeField] List<GameObject> roadPrefabs;

    Road head,current;
    private void Start()
    {
        ArrowController aC = ArrowController.Instance;
        List<WayPoint> pos = new List<WayPoint>();
        for (int i = 0; i < levelLength; i++)
        {
            var road = Instantiate(roadPrefabs.RandomItem()).GetComponent<Road>();
            if (head == null)
                head = road;
            current?.AttachToTail(road);
            current = road;
        }
        aC.transform.position = head.transform.position + new Vector3(0, 2f, 0);
        current = head;
        while (current != null)
        {
            foreach (var w in current.wayPoints)
            {
                pos.Add(w);
            }
            current = current.tail;
        }
        aC.SetPath(pos);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
