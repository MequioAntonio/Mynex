using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessaggioManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI testoMessaggio;
    [SerializeField] private Image immagineCornice;

    [TextArea(3, 10)]
    [SerializeField] private string[] messaggi;
    [SerializeField] private Sprite[] immagini;

    private int progressoDialogo = 0;
    private int[] indiciMessaggi;
    private int[] indiciImmagini;
    public List<string> messaggiLLM;

    private float tempoUltimoTocco = 0f;
    private float ritardoTocco = 0.3f; // Tempo minimo tra un tocco e l'altro

    private void Start() {
        messaggiLLM = new List<string>();
    }

    private void Update() {
        if (!gameObject.activeSelf) return;
        if (Time.time - tempoUltimoTocco < ritardoTocco) return;

        if (Input.GetMouseButtonDown(0) ||
            (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) {
            MostraDialogoLLM();
            tempoUltimoTocco = Time.time;
        }
    }

    public void MostraMessaggio(int indiceMessaggio, int indiceImmagine) {
        testoMessaggio.text = messaggi[indiceMessaggio];
        immagineCornice.sprite = immagini[indiceImmagine];

        gameObject.SetActive(true);
    }

    public void NascondiMessaggio() {
        gameObject.SetActive(false);
    }

    public void MostraDialogo() {
        if (progressoDialogo >= indiciMessaggi.Length) {
            NascondiMessaggio();
            progressoDialogo = 0;
        }
        else {
            MostraMessaggio(indiciMessaggi[progressoDialogo], indiciImmagini[progressoDialogo]);
            progressoDialogo++;
        }
    }

    public void MostraMessaggioLLM(string messaggioLLM, int indiceImmagine) {
        testoMessaggio.text = messaggioLLM;
        immagineCornice.sprite = immagini[indiceImmagine];

        gameObject.SetActive(true);
    }

    public void MostraDialogoLLM() {
        if (progressoDialogo >= messaggiLLM.Count) {
            NascondiMessaggio();
            progressoDialogo = 0;
            messaggiLLM.Clear();
        }
        else {
            MostraMessaggioLLM(messaggiLLM[progressoDialogo], indiciImmagini[progressoDialogo]);
            progressoDialogo++;
        }
    }

    public void setIndiciLLM(int[] indiciImmagini) {
        this.indiciImmagini = indiciImmagini;
    }

    public void setIndici(int[] indiciMessaggi, int[] indiciImmagini) {
        this.indiciMessaggi = indiciMessaggi;
        this.indiciImmagini = indiciImmagini;
    }

    public int GetProgressoDialogo() {
        return progressoDialogo;
    }
}