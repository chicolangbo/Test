using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameDropButton : MonoBehaviour
{
    private Button button;
    private readonly List<IObserver> observers = new List<IObserver>();

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(()=> { NotifyObservers(); }); // �̶� ĸó�ż� observers �ٲ�� ���� �� �Ǵ� �� �ƴ���?
    }

    private void Start()
    {
        //������ ���
        var obj = FindObjectOfType<DropDownTest>();
        RegisterObserver(obj);
    }

    public void RegisterObserver(IObserver observer)
    {
        if(!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    public void RemoveObserver(IObserver observer)
    {
        if(observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }

    public void NotifyObservers()
    {
        var buttonText = GetComponentInChildren<TextMeshProUGUI>().text;
        Debug.Log($"NotifyObservers : {observers.Count} / name : {buttonText}");
        foreach(var observer in observers)
        {
            observer.OnButtonClicked(buttonText);
        }
    }
}

public interface IObserver
{
    void OnButtonClicked(string buttonText);
}