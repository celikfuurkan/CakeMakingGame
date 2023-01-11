using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MyChoose : MonoBehaviour
{
    [SerializeField] GameObject win_10, istenenPasta, yapilanPasta, paket, pastaSayisi;

    [SerializeField] GameObject _boxKare, _underKare, _topKare, _decorationKare;
    [SerializeField] GameObject _boxKalp, _underKalp, _topKalp, _decorationKalp;
    [SerializeField] GameObject _boxYuvarlak, _underYuvarlak, _topYuvarlak, _decorationYuvarlak;

    [SerializeField] GameObject pastaPufParticle, pastaPufPlace, paketPuf;

    [SerializeField] List<GameObject> box, under, top, decoration, buttonKutular;
    [SerializeField] List<GameObject> destroyClone;

    [SerializeField] GameObject objPaket;
    [SerializeField] Transform posPaket;
    
    [SerializeField] GameObject yarat, atici, aticiShadow;

    private GameObject paketss, objLastPaket;
    private int paketCount;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void AddAll(int deger)
    {
        ButtonBugControl(deger);
        MakePasta.MP.myChooseNumber.Add(deger);
        StartCoroutine(Anim());
    }

    public void ButtonBugControl(int deger)
    {
        var _count = MakePasta.MP.myChooseNumber.Count;
        switch (_count)
        {
            case 0:
                for (int i = 0; i < buttonKutular[0].transform.childCount; i++)
                    buttonKutular[0].transform.GetChild(i).GetComponent<Button>().interactable = false;
                break;
            case 1:
                for (int i = 0; i < buttonKutular[deger + 1].transform.childCount; i++)
                    buttonKutular[deger + 1].transform.GetChild(i).GetComponent<Button>().interactable = false;
                break;
            case 2:
                for (int i = 0; i < buttonKutular[deger + 4].transform.childCount; i++)
                    buttonKutular[deger + 4].transform.GetChild(i).GetComponent<Button>().interactable = false;
                break;
            case 3:
                for (int i = 0; i < buttonKutular[deger + 6].transform.childCount; i++)
                    buttonKutular[deger + 6].transform.GetChild(i).GetComponent<Button>().interactable = false;
                break;
            default:
                break;
        }
    }

    public IEnumerator Anim()
    {
        yield return new WaitForSeconds(0.7f);
        var GO = GameObject.Find("Anim_icin");
        var count = MakePasta.MP.myChooseNumber.Count;
        Vector2 AA = GO.transform.position;

        switch (count)
        {
            case 1:
                buttonKutular[0].transform.DOMove(AA, 0.3f);
                break;
            case 2:
                for (int i = 1; i < 4; i++)
                    buttonKutular[i].transform.DOMove(AA, 0.3f);
                break;
            case 3:
                for (int i = 4; i < 7; i++)
                    buttonKutular[i].transform.DOMove(AA, 0.3f);
                break;
            case 4:
                for (int i = 7; i < 10; i++)
                    buttonKutular[i].transform.DOMove(AA, 0.3f);
                break;
            default:
                break;
        }
        StartCoroutine(Anim_2());
    }

    public IEnumerator Anim_2()
    {
        var GO = GameObject.Find("Anim_icin_2");
        Vector2 AA = GO.transform.position;
        var count = MakePasta.MP.myChooseNumber.Count;
        var number = MakePasta.MP.myChooseNumber[0];

        switch (count)
        {
            case 1:
                buttonKutular[number + 1].transform.DOMove(AA, 0.3f);
                break;
            case 2:
                buttonKutular[number + 4].transform.DOMove(AA, 0.3f);
                break;
            case 3:
                buttonKutular[number + 7].transform.DOMove(AA, 0.3f);
                break;
            case 4:
                buttonKutular[0].transform.DOMove(AA, 0.7f).SetEase(Ease.Linear);

                objLastPaket = Instantiate(objPaket, posPaket.position, Quaternion.identity, transform.parent);
                objLastPaket.transform.DOMove(yapilanPasta.transform.position, 0.3f).SetEase(Ease.Linear);
                yield return new WaitForSeconds(0.3f);
                //Instantiate(pasta_Puf_Particle, pasta_Puf_Place.transform.position, Quaternion.identity, transform.parent);
                objLastPaket.transform.DOPunchScale(new Vector3(-0.2f, 0.2f, 0), 0.2f, 0, 1f);

                for (int i = 0; i < destroyClone.Count; i++)
                {
                    Destroy(destroyClone[i]);
                }
                destroyClone.Clear();

                yield return new WaitForSeconds(0.2f);
                //Instantiate(paket_Puf, pasta_Sayisi.transform.GetChild(count).transform.position, Quaternion.identity, transform.parent);
                objLastPaket.transform.DOMove(pastaSayisi.transform.GetChild(paketCount).position, 0.3f);
                objLastPaket.transform.DOScale(new Vector3(0.18f, 0.18f, 0), 0.2f);

                paketCount++;

                if (paketCount == 10)
                {
                    PlayerPrefs.SetInt("Coin", 100);
                    paketCount = 0;
                    Debug.Log("10 tane pasta yapýldý ve burada ekstra ödül verilecek");
                    yield return new WaitForSeconds(0.7f);
                    Scene scene = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(scene.name);
                }
                else
                {
                    yield return new WaitForSeconds(0.3f);
                    MakePasta.MP.NextNewCake();
                    for (int i = 0; i < buttonKutular.Count; i++)
                    {
                        for (int k = 0; k < buttonKutular[i].transform.childCount; k++)
                        {
                            buttonKutular[i].transform.GetChild(k).transform.GetComponent<Button>().interactable = true;
                        }
                    }
                }
                break;
            default:
                break;
        }
    }

    public void ChooseAll(string str)
    {
        if (str == "Kare" || str == "Kalp" || str == "Yuvarlak")
        {
            var GO_1 = GameObject.Find(str + "_Kalýp");
            StartCoroutine(OrtakFonks1(GO_1));

            if (str == "Kare")
                paketss.transform.DOMove(_boxKare.transform.position, 0.3f).SetEase(Ease.Linear);
            else if (str == "Kalp")
                paketss.transform.DOMove(_boxKalp.transform.position, 0.3f).SetEase(Ease.Linear);
            else if (str == "Yuvarlak")
                paketss.transform.DOMove(_boxYuvarlak.transform.position, 0.3f).SetEase(Ease.Linear);

            StartCoroutine(OrtakFonks2());
        }
        else
        {
            var GO = GameObject.Find(str);
            StartCoroutine(OrtakFonks1(GO));
        }

        if (str == "Kare_Sarý_Under" || str == "Kare_Pembe_Under" || str == "Kare_Kahve_Under")
            paketss.transform.DOMove(_underKare.transform.position, 0.3f).SetEase(Ease.Linear);
        else if (str == "Kalp_Sarý_Under" || str == "Kalp_Pembe_Under" || str == "Kalp_Kahve_Under")
            paketss.transform.DOMove(_underKalp.transform.position, 0.3f).SetEase(Ease.Linear);
        else if (str == "Yuvarlak_Sarý_Under" || str == "Yuvarlak_Pembe_Under" || str == "Yuvarlak_Kahve_Under")
            paketss.transform.DOMove(_underYuvarlak.transform.position, 0.3f).SetEase(Ease.Linear);

        else if (str == "Kare_Sarý_Top" || str == "Kare_Pembe_Top" || str == "Kare_Kahve_Top")
            paketss.transform.DOMove(_topKare.transform.position, 0.3f).SetEase(Ease.Linear);
        else if (str == "Kalp_Sarý_Top" || str == "Kalp_Pembe_Top" || str == "Kalp_Kahve_Top")
            paketss.transform.DOMove(_topKalp.transform.position, 0.3f).SetEase(Ease.Linear);
        else if (str == "Yuvarlak_Sarý_Top" || str == "Yuvarlak_Pembe_Top" || str == "Yuvarlak_Kahve_Top")
            paketss.transform.DOMove(_topYuvarlak.transform.position, 0.3f).SetEase(Ease.Linear);

        else if (str == "Kare_Sus_Seker" || str == "Kare_Sus_Cikolata" || str == "Kare_Sus_Top" || str == "Kare_Sus_Cilek")
            paketss.transform.DOMove(_decorationKare.transform.position, 0.3f).SetEase(Ease.Linear);
        else if (str == "Kalp_Sus_Seker" || str == "Kalp_Sus_Cikolata" || str == "Kalp_Sus_Top" || str == "Kalp_Sus_Cilek")
            paketss.transform.DOMove(_decorationKalp.transform.position, 0.3f).SetEase(Ease.Linear);
        else if (str == "Yuvarlak_Sus_Seker" || str == "Yuvarlak_Sus_Cikolata" || str == "Yuvarlak_Sus_Top" || str == "Yuvarlak_Sus_Cilek")
            paketss.transform.DOMove(_decorationYuvarlak.transform.position, 0.3f).SetEase(Ease.Linear);

        StartCoroutine(OrtakFonks2());
    }

    private IEnumerator OrtakFonks1(GameObject GO)
    {
        paketss = Instantiate(GO, yarat.transform.position, Quaternion.identity, GameObject.Find("Yarat").transform);
        destroyClone.Add(paketss);
        paketss.GetComponent<RectTransform>().localScale = new Vector2(0.05f, 1f);
        atici.transform.DOScaleY(1.1f, 0.2f);
        aticiShadow.transform.DOScaleY(1.1f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        atici.transform.DOScaleY(1f, 0.2f);
        aticiShadow.transform.DOScaleY(1f, 0.2f);
    }

    private IEnumerator OrtakFonks2()
    {
        paketss.transform.DOScaleY(1, 0.3f);
        yield return new WaitForSeconds(0.3f);
        paketss.transform.DOScaleX(1, 0.3f);
        yield return new WaitForSeconds(0.3f);
        paketss.transform.DOPunchScale(new Vector3(-0.2f, 0.2f, 0), 0.2f, 0, 1f);
    }
}
