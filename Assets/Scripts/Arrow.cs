using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Arrow : MonoBehaviour, IPoolable
{
    public int id;
    public float position;
    public float unit;
    public float freq;

    public bool IsAvailable { get; set; }

    public void DePool()
    {
        IsAvailable = false;
        DOVirtual.Float(0, id, .5f, (val) => { position = val; SetPos(); }).OnComplete(() =>
        {
            gameObject.SetActive(true);
        });
    }

    public void Pool()
    {
        IsAvailable = true;
        transform.localPosition = Vector3.zero;
        DOVirtual.Float(position, 0, .5f, (val) => { position = val; SetPos(); }).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
    public void SetPos()
    {
        var x = position * unit * Mathf.Cos(position / freq);
        var z = position * unit * Mathf.Sin(position / freq);
        transform.localPosition = new Vector3(x, 0, z);
    }

    public void Merge()
    {
        DOVirtual.Float(position, 0, 5f, (val) =>
        {
            position = val;
            SetPos();
        }).OnComplete(Pool);
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.localRotation = Quaternion.identity;
        transform.DOLocalRotate(new Vector3(0, 360, 0), 1f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
