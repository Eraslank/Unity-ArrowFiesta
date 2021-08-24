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
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(true);
        IsAvailable = false;
        DOVirtual.Float(0, id, .5f, (val) => { position = val; SetPos(); });
    }

    public void Pool()
    {
        DOVirtual.Float(position, 0, .5f, (val) => { position = val; SetPos(); }).OnComplete(() =>
        {
            IsAvailable = true;
            gameObject.SetActive(false);
        });
    }
    public void SetPos()
    {
        var x = 0f;
        var z = 0f;

        try
        {
            x = position * unit * Mathf.Cos(position / freq);
            z = position * unit * Mathf.Sin(position / freq);
            transform.localPosition = new Vector3(x, 0, z);
        }
        catch
        {
            transform.localPosition = Vector3.zero;
        }
    }

    public void Merge(bool pool)
    {
        if (pool)
            Pool();
        return;
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
