using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSpawner : MonoBehaviour
{
    [SerializeField] GameObject gatePrefab;
    WeightedRandomBag<bool?> gateBag;
    void Start()
    {
        gateBag = new WeightedRandomBag<bool?>();
        gateBag.AddEntry(true, 3);
        gateBag.AddEntry(false, 2);
        gateBag.AddEntry(null, 2);

        var positive = gateBag.GetRandom();

        if(positive.HasValue && Physics.Raycast(transform.position, -transform.up, out RaycastHit hit,5))
        {
            var pos = hit.point;
            var gate = Instantiate(gatePrefab, transform.parent).GetComponent<Gate>();
            var operation = EOperationExtension.GetRandomOperation(positive.Value, out short number);
            gate.Initialize(operation, number);
            gate.transform.position = pos;
            gate.transform.eulerAngles = transform.eulerAngles;
        }


        Destroy(gameObject);

    }
}
