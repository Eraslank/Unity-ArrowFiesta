using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gate : MonoBehaviour
{

    [SerializeField] Text numberText;

    public EOperation operation;
    public short number;

    public void Initialize(EOperation operation, short number)
    {
        this.operation = operation;
        this.number = number;

        numberText.text = operation.Symbol() + number.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        var arrowController = other.gameObject.GetComponent<ArrowController>();
        if (arrowController)
            arrowController.arrowCount = operation.Calculate(arrowController.arrowCount, number);
    }

    private void OnValidate()
    {
        Initialize(operation, number);
    }
}
