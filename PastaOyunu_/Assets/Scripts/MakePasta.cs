using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MakePasta : MonoBehaviour
{
    public static MakePasta MP;

    [SerializeField] List<int> createdNumber;

    [SerializeField] List<Sprite> box, underCream, topCream, cakeDecoration;

    [SerializeField] GameObject _box;
    [SerializeField] GameObject yapilanPastaKare, yapilanPastaKalp, yapilanPastaYuvarlak;
    [SerializeField] GameObject _under, _top;
    [SerializeField] GameObject _decorationKare, _decorationKalp, _decorationYuvarlak;

    public List<int> myChooseNumber;

    private int boxRandomNumber, underCreamRandomNumber, topCreamRandomNumber, cakeDecoRndNumber;
    private int count = 0;

    private void Awake()
    {
        MP=this;
    }

    void Start()
    {
        NewCake();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void NewCake()
    {
        BoxRandom();
        YapilanPastaAyarlama();
        UnderCreamRandom();
        TopCreamRandom();
        CakeDecorationRandom();
        PutSprite();
        AddList();
    }

    private void BoxRandom()
    {
        boxRandomNumber = Random.Range(0, 3);
    }

    private void YapilanPastaAyarlama() 
    {
        switch (boxRandomNumber)
        {
            case 0:
                yapilanPastaKalp.SetActive(false);
                yapilanPastaYuvarlak.SetActive(false);
                break;
            case 1:
                yapilanPastaKare.SetActive(false);
                yapilanPastaYuvarlak.SetActive(false);
                break;
            case 2:
                yapilanPastaKare.SetActive(false);
                yapilanPastaKalp.SetActive(false);
                break;
            default:
                break;
        }
    }

    private void UnderCreamRandom()
    {  
        if (boxRandomNumber==0)
            underCreamRandomNumber = Random.Range(0, 3);
        else if (boxRandomNumber == 1)
            underCreamRandomNumber = Random.Range(3, 6);
        else if (boxRandomNumber == 2)
            underCreamRandomNumber = Random.Range(6, 9);
    }

    private void TopCreamRandom()
    {
        if (boxRandomNumber == 0)
            topCreamRandomNumber = Random.Range(0, 3);
        if (boxRandomNumber == 1)
            topCreamRandomNumber = Random.Range(3, 6);
        if (boxRandomNumber == 2)
            topCreamRandomNumber = Random.Range(6, 9);
    }

    private void CakeDecorationRandom()
    {
        cakeDecoRndNumber = Random.Range(0, 4);
    }

    private void PutSprite()
    {
        _box.GetComponent<Image>().sprite = box[boxRandomNumber];
        _under.GetComponent<Image>().sprite = underCream[underCreamRandomNumber];
        _top.GetComponent<Image>().sprite = topCream[topCreamRandomNumber];

        if (boxRandomNumber == 0)
        {
            _decorationKalp.SetActive(false);
            _decorationYuvarlak.SetActive(false);
            _decorationKare.GetComponent<Image>().sprite = cakeDecoration[cakeDecoRndNumber];
        }
        else if (boxRandomNumber == 1)
        {
            _decorationKare.SetActive(false);
            _decorationYuvarlak.SetActive(false);
            _decorationKalp.GetComponent<Image>().sprite = cakeDecoration[cakeDecoRndNumber];
        }
        else if (boxRandomNumber == 2)
        {
            _decorationKare.SetActive(false);
            _decorationKalp.SetActive(false);
            _decorationYuvarlak.GetComponent<Image>().sprite = cakeDecoration[cakeDecoRndNumber];
        }
    }

    private void AddList()
    {
        createdNumber.Add(boxRandomNumber);
        if (boxRandomNumber == 1)
        {
            underCreamRandomNumber -= 3;
            topCreamRandomNumber -= 3;
        }
        else if (boxRandomNumber == 2)
        {
            underCreamRandomNumber -= 6;
            topCreamRandomNumber -= 6;
        }
        createdNumber.Add(underCreamRandomNumber);
        createdNumber.Add(topCreamRandomNumber);
        createdNumber.Add(cakeDecoRndNumber);
    }

    public void NextNewCake()
    {
        _decorationKare.SetActive(true);
        _decorationKalp.SetActive(true);
        _decorationYuvarlak.SetActive(true);

        yapilanPastaKare.SetActive(true);
        yapilanPastaKalp.SetActive(true);
        yapilanPastaYuvarlak.SetActive(true);

        if (myChooseNumber.Count==createdNumber.Count)
        {
            WinControl();
            createdNumber.Clear();
            myChooseNumber.Clear();

            NewCake();
        }
    }

    public void WinControl()
    {
        for (int i = 0; i < createdNumber.Count; i++)
            if (createdNumber[i]==myChooseNumber[i])
                count++;

        if (count == createdNumber.Count)
            Debug.Log("4 SEÇENEK DOÐRU 40 ALTIN");
        else if (count == createdNumber.Count - 1)
            Debug.Log("3 SEÇENEK DOÐRU 30 ALTIN");
        else if (count == createdNumber.Count - 2)
            Debug.Log("2 SEÇENEK DOÐRU 20 ALTIN");
        else if (count == createdNumber.Count - 3)
            Debug.Log("1 SEÇENEK DOÐRU 10 ALTIN");
        else if (count == createdNumber.Count - 4)
            Debug.Log("0 SEÇENEK DOÐRU 0 ALTIN");

        count = 0;
    }
}
