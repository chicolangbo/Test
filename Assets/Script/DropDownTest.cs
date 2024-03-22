using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System;
using System.Linq;

public class Data
{
    public string Name { get; set; }
    public int Count { get; set; }
    public Data(string name, int count)
    {
        Name = name;
        Count = count;
    }
}

public class DataComparer : IComparer<Data>
{
    public int Compare(Data x, Data y)
    {
        Debug.Log("Compare");
        // 선택된 횟수가 많은 순서대로 정렬
        return y.Count.CompareTo(x.Count);
    }
}

public class DropDownTest : MonoBehaviour, IObserver
{
    public SortedSet<Data> nameList = new SortedSet<Data>(new DataComparer());
    public TMP_InputField nameInput; // 검색어 컴포넌트
    public GameObject nameCell; // 프리팹
    public GameObject nameCellParent; // 컨텐츠박스
    public GameObject scrollView; // 드롭박스
    public Button loginButton;

    private void Start()
    {
        DataSet();
        scrollView.SetActive(false);
        nameInput.onSelect.AddListener(ActivateNameListWindow);
        nameInput.onDeselect.AddListener(UnActivateNameListWindow);
        nameInput.onValueChanged.AddListener(CheckNameSimilarity);
        loginButton.onClick.AddListener(UpdateNameCount);
    }

    private void Update()
    {
    }

    public void DataSet()
    {
        Data data1 = new Data("김민지", 10);
        Data data2 = new Data("박은정", 9);
        Data data3 = new Data("김민규", 8);
        Data data4 = new Data("박라현", 7);
        Data data5 = new Data("최승희", 6);

        nameList.Add(data1);
        nameList.Add(data2);
        nameList.Add(data3);
        nameList.Add(data4);
        nameList.Add(data5);

        // cell 생성, 자동 정렬
        foreach (var data in nameList)
        {
            var cell = Instantiate(nameCell, nameCellParent.transform);
            cell.GetComponentInChildren<TextMeshProUGUI>().text = data.Name;
        }
    }

    public void ActivateNameListWindow(string text)
    {
        if(!scrollView.activeSelf)
        {
            scrollView.SetActive(true);
        }
    }

    public void UnActivateNameListWindow(string text)
    {
        if(scrollView.activeSelf)
        {
            scrollView.SetActive(false);
        }
    }

    public void CheckNameSimilarity(string text)
    {
        Debug.Log("value changed");
        foreach(Transform cell in nameCellParent.transform)
        {
            var cellText = cell.GetComponentInChildren<TextMeshProUGUI>();
            if(cellText != null)
            {
                // text가 cell에 포함되어 있는지 조사
                if (cellText.text.Contains(text.ToLower()))
                {
                    cell.gameObject.SetActive(true);
                }
                else
                {
                    cell.gameObject.SetActive(false);
                }
            }
        }
    }

    public void OnButtonClicked(string buttonText)
    {
        Debug.Log("OnButtonClicked");
        nameInput.text = buttonText;
    }

    public void UpdateNameCount()
    {
        foreach(var name in nameList)
        {
            if(name.Name.Equals(nameInput.text))
            {
                name.Count++;
                Debug.Log($"UpdateNameCount: {name.Name}, {name.Count} / 첫번째는 {nameList.First().Name}");
                break;
            }
        }

        foreach(Transform nameCell in nameCellParent.transform)
        {
            Destroy(nameCell.gameObject);

        }
        foreach (var data in nameList)
        {
            var cell = Instantiate(nameCell, nameCellParent.transform);
            cell.GetComponentInChildren<TextMeshProUGUI>().text = data.Name;
        }

    }
}
