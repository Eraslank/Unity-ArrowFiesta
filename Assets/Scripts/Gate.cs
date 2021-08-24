using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gate : MonoBehaviour
{
    [SerializeField] MeshRenderer gateRenderer;

    [SerializeField] Text numberText;
    [SerializeField] Material positive, negative;

    public EOperation operation;
    public short number;

    public void Initialize(EOperation operation, short number)
    {
        this.operation = operation;
        this.number = number;

        numberText.text = operation.Symbol() + number.ToString();
        gateRenderer.material = operation.GetMaterial(positive, negative);
    }


    private void OnTriggerEnter(Collider other)
    {
        var arrowController = other.gameObject.GetComponentInParent<ArrowController>();
        if (arrowController)
        {
            arrowController.ChangeArrowCount(operation.Calculate(arrowController.arrowCount, number));
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void OnValidate()
    {
        Initialize(operation, number);
    }

}
