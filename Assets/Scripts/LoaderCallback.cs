using System.Collections;
using System.Linq;
using UnityEngine;

public class LoaderCallback : MonoBehaviour {

    public GameObject loading1;
    public GameObject loading2;

    private bool isFirstUpdate = true;

    public string risposta;

    public string nexRole = "Ti chiami Nex, sei un ragazzino coraggioso. Il Maestro Orwin è il tuo mentore e ti ha cresciuto con affetto. Parla in prima persona senza aggiungere commenti, pensieri o descrizioni. Non superare le 30 parole.";
    public string orwinRole = "Ti chiami Orwin, sei un vecchio fabbro e maestro di Nex. Parla in prima persona senza aggiungere commenti, pensieri o descrizioni. Non superare le 30 parole.";
    //public string spiritoRole = "Sei lo Spirito della Roccia, parli solo in enigmi e messaggi criptici.";

    public string spiritoRole =
    "Sei lo Spirito della Roccia, un'entità antica che parla solo per enigmi e messaggi criptici. " +
    "Ogni tuo messaggio deve evocare una sequenza di parole senza mai nominarle direttamente. " +
    "Adatta la complessità dei tuoi enigmi al livello di difficoltà richiesto: " +
    "- Facile: indizi chiari e immagini semplici. " +
    "- Medio: metafore poetiche e legami meno diretti. " +
    "- Difficile: enigmi astratti e simbolici, interpretabili solo da menti attente.";

    string userPrompt;

    private int completedResponses = 0;
    private int totalResponses = 0;

    private void Update() {
        if (isFirstUpdate) {
            isFirstUpdate = false;

            StartCoroutine(DoWorkBeforeContinue());

            //StartCoroutine(FakeDialogue());
            //StartCoroutine(TestDialogue());
        }
    }

    private IEnumerator DoWorkBeforeContinue() {

        int numero = Random.Range(1, 3); // genera 1 o 2
        if(numero == 1) {
            loading1.SetActive(false);
            loading2.SetActive(true);
        }
        else {
            loading1.SetActive(true);
            loading2.SetActive(false);
        }

        if (Loader.targetScene == Loader.Scene.SampleScene) {

            completedResponses = 0;
            totalResponses = 9;

            LLMManager.loaderAnswers = new string[9];

            userPrompt = "Urli la frase 'Maestro Orwiin' ripetutamente chiedendoti dove si sarà cacciato il Maestro Orwin" +
                "Ecco alcuni esempi:\n" +
                "Maestro Orwin! Maestro Orwiiiin! Dove sarà mai?\n" +
                "Maestro Orwin! Dov'è finito il Maestro Orwin?";
            StartCoroutine(LLMManager.GetNPCResponse(
                nexRole,
                userPrompt,
                (response) => {
                    risposta = "Nex: \n\"" + response + "\"";
                    LLMManager.loaderAnswers[0] = risposta;
                    completedResponses++;
                    UpdateProgress(totalResponses);
                }
            ));
            userPrompt = "Vedi un robot davanti a te che distrugge oggetti. Ti chiedi come sia possibile che ci sia un robot qui." +
                "Ecco alcuni esempi:\n" +
                "Quello è un robot! Ma come è possibile?\n" +
                "Cos'è quella creatura? Un robot?! Ma come?";
            StartCoroutine(LLMManager.GetNPCResponse(
                nexRole,
                userPrompt,
                (response) => {
                    risposta = "Nex: \n\"" + response + "\"";
                    LLMManager.loaderAnswers[1] = risposta;
                    completedResponses++;
                    UpdateProgress(totalResponses);
                }
            ));
            userPrompt = "Sei sorpreso di vedere Nex, il tuo allievo, arrivare qui sano e salvo. Poi gli chiedi come sia possibile che la sua connessione alle lame funzioni già così bene" +
                "Ecco alcuni esempi:\n" +
                "Nex? Mio caro, sei arrivato fin qui! La tua connessione alle lame funziona già così bene?\n" +
                "Nex! Sei riuscito a raggiungermi! Hai già sviluppato una connessione così potente con le lame!";
            StartCoroutine(LLMManager.GetNPCResponse(
                orwinRole,
                userPrompt,
                (response) => {
                    risposta = "Orwin: \n\"" + response + "\"";
                    LLMManager.loaderAnswers[2] = risposta;
                    completedResponses++;
                    UpdateProgress(totalResponses);
                }
            ));
            userPrompt = "Rispondi a Orwin dicendogli che non sai cosa stia succedendo ma senti un nuovo potere dentro di te" +
                "Ecco alcuni esempi:\n" +
                "Sì! Non so bene cosa stia succedendo... sento un nuovo potere dentro di me...\n" +
                "Proprio così, non so nemmeno io cosa mi stia accadendo, ma sento un nuovo tipo di potere...";
            StartCoroutine(LLMManager.GetNPCResponse(
                nexRole,
                userPrompt,
                (response) => {
                    risposta = "Nex: \n\"" + response + "\"";
                    LLMManager.loaderAnswers[3] = risposta;
                    completedResponses++;
                    UpdateProgress(totalResponses);
                }
            ));
            userPrompt = "Spieghi a Nex che la Frattura è la causa dei suoi nuovi poteri e sta portando i robot qui, e che presto queste terre saranno invase" +
                "Ecco alcuni esempi:\n" +
                "È la Frattura... Sta diventanto più instabile e sta portando i robot qui! Presto queste terre saranno invase...\n" +
                "Lo vedo, è a causa della Frattura. È sempre più instabile e sta portando i robot qui! Presto saremo invasi.";
            StartCoroutine(LLMManager.GetNPCResponse(
                orwinRole,
                userPrompt,
                (response) => {
                    risposta = "Orwin: \n\"" + response + "\"";
                    LLMManager.loaderAnswers[4] = risposta;
                    completedResponses++;
                    UpdateProgress(totalResponses);
                }
            ));
            userPrompt = "Dì a Orwin di rientrare nella casa poichè stanno arrivando altri robot" +
                "Ecco alcuni esempi:\n" +
                "Ne stanno arrivando altri! Maestro, rimanga qui dentro!\n" +
                "Stanno arrivando altri robot! Maestro, torni nella casa!";
            StartCoroutine(LLMManager.GetNPCResponse(
                nexRole,
                userPrompt,
                (response) => {
                    risposta = "Nex: \n\"" + response + "\"";
                    LLMManager.loaderAnswers[5] = risposta;
                    completedResponses++;
                    UpdateProgress(totalResponses);
                }
            ));
            userPrompt = "I robot sono tanti e non ti senti più molto bene, lo dici ad alta voce in maniera stanca" +
                "Ecco alcuni esempi:\n" +
                "Sono davvero in tanti... E io non mi sento più così bene...\n" +
                "Sono in troppi... E non mi sento più molto in forma...";
            StartCoroutine(LLMManager.GetNPCResponse(
                nexRole,
                userPrompt,
                (response) => {
                    risposta = "Nex: \n\"" + response + "\"";
                    LLMManager.loaderAnswers[6] = risposta;
                    completedResponses++;
                    UpdateProgress(totalResponses);
                }
            ));
            userPrompt = "Chiami Nex per farlo venire da te in fretta" +
                "Ecco alcuni esempi:\n" +
                "Nex! Vieni qui!\n" +
                "Nex! Torna da me, veloce!";
            StartCoroutine(LLMManager.GetNPCResponse(
                orwinRole,
                userPrompt,
                (response) => {
                    risposta = "Orwin: \n\"" + response + "\"";
                    LLMManager.loaderAnswers[7] = risposta;
                    completedResponses++;
                    UpdateProgress(totalResponses);
                }
            ));
            userPrompt = "Dici a Nex che dovete correre, e in fretta." +
                "Ecco alcuni esempi:\n" +
                "Nex, dobbiamo andarcene, ora!\n" +
                "Nex, dobbiamo correre, e in fretta!";
            StartCoroutine(LLMManager.GetNPCResponse(
                orwinRole,
                userPrompt,
                (response) => {
                    risposta = "Orwin: \n\"" + response + "\"";
                    LLMManager.loaderAnswers[8] = risposta;
                    completedResponses++;
                    UpdateProgress(totalResponses);
                }
            ));



            /*LLMManager.loaderAnswers[0] = "Nex: \n";
            LLMManager.loaderAnswers[1] = "Nex: \n";
            LLMManager.loaderAnswers[2] = "Orwin: \n";
            LLMManager.loaderAnswers[3] = "Nex: \n";
            LLMManager.loaderAnswers[4] = "Orwin: \n";
            LLMManager.loaderAnswers[5] = "Nex: \n";
            LLMManager.loaderAnswers[6] = "Nex: \n";
            LLMManager.loaderAnswers[7] = "Orwin: \n";
            LLMManager.loaderAnswers[8] = "Orwin: \n";*/



            yield return new WaitUntil(() =>
                LLMManager.loaderAnswers.All(answer => !string.IsNullOrEmpty(answer))
            );

            for (int i = 0; i < 9; i++) {
                Debug.Log("Frase " + i + ": " + LLMManager.loaderAnswers[i] + "\n");
            }
        }
        else if (Loader.targetScene == Loader.Scene.Cave && !Loader.caveGiaCaricata) {

            completedResponses = 0;
            totalResponses = 1;

            Loader.caveGiaCaricata = true;
            LLMManager.loaderAnswers = new string[1];

            userPrompt = "Dici a Nex che dovete usare i 3 sacri Elisir di Lumenfall se volete respingere l'invasione. E che Nex deve trovare i passaggi, risolvere gli enigmi e poi tornare qui." +
                "Ecco alcuni esempi:\n" +
                "Nex, per respingere l'invasione dobbiamo trovare i 3 sacri Elisir di Lumenfall. Devi trovare i passaggi, risolvere gli enigmi e poi tornare qui!\n" +
                "Nex, dobbiamo assolutamente ottenere i 3 sacri Elisir di Lumenfall se vogliamo respingere l'invasione. Trova i passaggi, risolvi gli enigmi e poi torna da me!";
            StartCoroutine(LLMManager.GetNPCResponse(
                orwinRole,
                userPrompt,
                (response) => {
                    risposta = "Orwin: \n\"" + response + "\"";
                    LLMManager.loaderAnswers[0] = risposta;
                    completedResponses++;
                    UpdateProgress(totalResponses);
                }
            ));



            //LLMManager.loaderAnswers[0] = "Orwin: \n";



            yield return new WaitUntil(() =>
                LLMManager.loaderAnswers[0] != null
            );

        }
        else if (Loader.targetScene == Loader.Scene.Door_1) {

            completedResponses = 0;
            totalResponses = 1;

            LLMManager.loaderAnswers = new string[1];

            //Player.Instance.mescolaColori();
            Loader.mescolaColori();

            //userPrompt = "Crea un messaggio criptico che faccia venire in mente 4 colori in questo ordine: " + Loader.colori[0] + ", " + Loader.colori[1] + ", " + Loader.colori[2] + ", " + Loader.colori[3] + ". Mantienilo sotto le 30 parole.";
            string userPrompt =
            "Devi creare un messaggio criptico che faccia venire in mente 4 colori nell’ordine indicato. " +
            "Non nominare mai direttamente i colori. " +
            "Usa al massimo 30 parole.\n\n" +

            "Ecco alcuni esempi:\n\n" +
            "Difficoltà: Facile\n" +
            "Colori: rosso, verde, blu, giallo\n" +
            "Messaggio: 'Dal fuoco nasce la foglia, poi il cielo e infine il sole.'\n\n" +

            "Difficoltà: Media\n" +
            "Colori: verde, rosso, blu, giallo\n" +
            "Messaggio: 'Tra boschi ardenti e mari silenziosi, l’alba si posa leggera.'\n\n" +

            "Difficoltà: Difficile\n" +
            "Colori: blu, giallo, rosso, verde\n" +
            "Messaggio: 'Nel silenzio profondo del mare, un lampo d’oro risveglia la vita nascosta nella pietra.'\n\n" +

            "Ora genera un nuovo messaggio per:\n" +
            "Difficoltà: " + Loader.difficulty + "\n" +
            "Colori: " + Loader.colori[0] + ", " + Loader.colori[1] + ", " + Loader.colori[2] + ", " + Loader.colori[3] + "\n" +
            "Messaggio:";
            StartCoroutine(LLMManager.GetNPCResponse(
                spiritoRole,
                userPrompt,
                (response) => {
                    risposta = "Spirito della Roccia: \n\"" + response + "\"";
                    LLMManager.loaderAnswers[0] = risposta;
                    completedResponses++;
                    UpdateProgress(totalResponses);
                }
            ));



            //LLMManager.loaderAnswers[0] = "Spirito della Roccia: \n";



            yield return new WaitUntil(() =>
                LLMManager.loaderAnswers[0] != null
            );

        }
        else if (Loader.targetScene == Loader.Scene.Door_2) {

            completedResponses = 0;
            totalResponses = 1;

            LLMManager.loaderAnswers = new string[1];

            //Player.Instance.mescolaTotems();
            Loader.mescolaTotems();

            //userPrompt = "Crea un messaggio criptico che faccia venire in mente 4 oggetti in questo ordine: " + Loader.totems[0] + ", " + Loader.totems[1] + ", " + Loader.totems[2] + ", " + Loader.totems[3] + ". Mantienilo sotto le 30 parole.";
            string userPrompt =
            "Devi creare un messaggio criptico che faccia venire in mente 4 oggetti nell’ordine indicato. " +
            "Non nominare mai direttamente gli oggetti. " +
            "Usa al massimo 30 parole.\n\n" +

            "Ecco alcuni esempi:\n\n" +
            "Difficoltà: Facile\n" +
            "Oggetti: cuore, porta, corona, cervo\n" +
            "Messaggio: 'L’amore apre i passaggi, dona regni e guida chi ascolta la foresta.'\n\n" +

            "Difficoltà: Media\n" +
            "Oggetti: porta, cervo, cuore, corona\n" +
            "Messaggio: 'Varca il legno silente, segui le orme, e il battito nascosto ti renderà sovrano.'\n\n" +

            "Difficoltà: Difficile\n" +
            "Oggetti: corona, cuore, cervo, porta\n" +
            "Messaggio: 'L’onore pesa sul petto, la selva risponde, ma solo chi entra trova il vero sentiero.'\n\n" +

            "Ora genera un nuovo messaggio per:\n" +
            "Difficoltà: " + Loader.difficulty + "\n" +
            "Oggetti: " + Loader.totems[0] + ", " + Loader.totems[1] + ", " + Loader.totems[2] + ", " + Loader.totems[3] + "\n" +
            "Messaggio:";
            StartCoroutine(LLMManager.GetNPCResponse(
                spiritoRole,
                userPrompt,
                (response) => {
                    risposta = "Spirito della Roccia: \n\"" + response + "\"";
                    LLMManager.loaderAnswers[0] = risposta;
                    completedResponses++;
                    UpdateProgress(totalResponses);
                }
            ));



            //LLMManager.loaderAnswers[0] = "Spirito della Roccia: \n";



            yield return new WaitUntil(() =>
                LLMManager.loaderAnswers[0] != null
            );

        }
        else if (Loader.targetScene == Loader.Scene.Door_3) {

            completedResponses = 0;
            totalResponses = 1;

            LLMManager.loaderAnswers = new string[1];

            //Player.Instance.mescolaElementi();
            Loader.mescolaElementi();

            //userPrompt = "Crea un messaggio criptico che faccia venire in mente 4 elementi in questo ordine: " + Loader.elementi[0] + ", " + Loader.elementi[1] + ", " + Loader.elementi[2] + ", " + Loader.elementi[3] + ". Mantienilo sotto le 30 parole.";
            string userPrompt =
            "Devi creare un messaggio criptico che faccia venire in mente 4 elementi nell’ordine indicato. " +
            "Non nominare mai direttamente gli elementi. " +
            "Usa al massimo 30 parole.\n\n" +

            "Ecco alcuni esempi:\n\n" +
            "Difficoltà: Facile\n" +
            "Elementi: acqua, fuoco, terra, vento\n" +
            "Messaggio: 'Dal fiume nasce la fiamma, che nutre il suolo e danza tra le brezze.'\n\n" +

            "Difficoltà: Media\n" +
            "Elementi: fuoco, vento, acqua, terra\n" +
            "Messaggio: 'L’ardore corre nel cielo, si spegne tra le onde e dorme nel grembo del mondo.'\n\n" +

            "Difficoltà: Difficile\n" +
            "Elementi: vento, terra, fuoco, acqua\n" +
            "Messaggio: 'Il respiro plasma la pietra, l’incendio la purifica, il mare infine la accoglie.'\n\n" +

            "Ora genera un nuovo messaggio per:\n" +
            "Difficoltà: " + Loader.difficulty + "\n" +
            "Elementi: " + Loader.elementi[0] + ", " + Loader.elementi[1] + ", " + Loader.elementi[2] + ", " + Loader.elementi[3] + "\n" +
            "Messaggio:";
            StartCoroutine(LLMManager.GetNPCResponse(
                spiritoRole,
                userPrompt,
                (response) => {
                    risposta = "Spirito della Roccia: \n\"" + response + "\"";
                    LLMManager.loaderAnswers[0] = risposta;
                    completedResponses++;
                    UpdateProgress(totalResponses);
                }
            ));



            //LLMManager.loaderAnswers[0] = "Spirito della Roccia: \n";



            yield return new WaitUntil(() =>
                LLMManager.loaderAnswers[0] != null
            );

        }

        Loader.LoaderCallback();
    }

    private IEnumerator FakeDialogue() {

        int numero = Random.Range(1, 3); // genera 1 o 2
        if (numero == 1) {
            loading1.SetActive(false);
            loading2.SetActive(true);
        }
        else {
            loading1.SetActive(true);
            loading2.SetActive(false);
        }

        if (Loader.targetScene == Loader.Scene.SampleScene) {

            completedResponses = 0;
            totalResponses = 9;

            LLMManager.loaderAnswers = new string[9];

            LLMManager.loaderAnswers[0] = "Nex: \n\"Maestro Orwin! Maestro Orwiiiin! Dove sarà mai?\"";
            LLMManager.loaderAnswers[1] = "Nex: \n\"Quello è un robot! Ma come è possibile?\"";
            LLMManager.loaderAnswers[2] = "Orwin: \n\"Nex? Mio caro, sei arrivato fin qui! La tua connessione alle lame funziona già così bene?\"";
            LLMManager.loaderAnswers[3] = "Nex: \n\"Sì! Non so bene cosa stia succedendo... sento un nuovo potere dentro di me...\"";
            LLMManager.loaderAnswers[4] = "Orwin: \n\"È la Frattura... Sta diventanto più instabile e sta portando i robot qui! Presto queste terre saranno invase...\"";
            LLMManager.loaderAnswers[5] = "Nex: \n\"Ne stanno arrivando altri! Maestro, rimanga qui dentro!\"";
            LLMManager.loaderAnswers[6] = "Nex: \n\"Sono davvero in tanti... E io non mi sento più così bene...\"";
            LLMManager.loaderAnswers[7] = "Orwin: \n\"Nex! Vieni qui!\"";
            LLMManager.loaderAnswers[8] = "Orwin: \n\"Nex, dobbiamo andarcene, ora!\"";

            completedResponses = 9;

            yield return new WaitUntil(() =>
                LLMManager.loaderAnswers.All(answer => !string.IsNullOrEmpty(answer))
            );

            for (int i = 0; i < 9; i++) {
                Debug.Log("Frase " + i + ": " + LLMManager.loaderAnswers[i] + "\n");
            }
        }
        else if (Loader.targetScene == Loader.Scene.Cave && !Loader.caveGiaCaricata) {

            completedResponses = 0;
            totalResponses = 1;

            Loader.caveGiaCaricata = true;
            LLMManager.loaderAnswers = new string[1];

            LLMManager.loaderAnswers[0] = "Orwin: \n\"Nex, per respingere l'invasione dobbiamo trovare i 3 sacri Elisir di Lumenfall. Devi trovare i passaggi, risolvere gli enigmi e poi tornare qui!\"";

            completedResponses = 1;

            yield return new WaitUntil(() =>
                LLMManager.loaderAnswers[0] != null
            );

        }
        else if (Loader.targetScene == Loader.Scene.Door_1) {

            completedResponses = 0;
            totalResponses = 1;

            LLMManager.loaderAnswers = new string[1];

            //Player.Instance.mescolaColori();
            //Loader.mescolaColori();
            Loader.colori[0] = "Blu";
            Loader.colori[1] = "Verde";
            Loader.colori[2] = "Giallo";
            Loader.colori[3] = "Rosso";

            LLMManager.loaderAnswers[0] = "Spirito della Roccia: \n\"Come il mare che culla una foresta, il sole che sorge e infiamma la sera.\"";

            completedResponses = 1;

            yield return new WaitUntil(() =>
                LLMManager.loaderAnswers[0] != null
            );

        }
        else if (Loader.targetScene == Loader.Scene.Door_2) {

            completedResponses = 0;
            totalResponses = 1;

            LLMManager.loaderAnswers = new string[1];

            //Player.Instance.mescolaTotems();
            //Loader.mescolaTotems();
            Loader.totems[0] = "Cuore";
            Loader.totems[1] = "Porta";
            Loader.totems[2] = "Cervo";
            Loader.totems[3] = "Corona";

            LLMManager.loaderAnswers[0] = "Spirito della Roccia: \n\"Nel petto batte un segreto, oltre il legno si schiude, e chi segue le tracce nella selva trova la chioma d'oro.\"";

            completedResponses = 1;

            yield return new WaitUntil(() =>
                LLMManager.loaderAnswers[0] != null
            );

        }
        else if (Loader.targetScene == Loader.Scene.Door_3) {

            completedResponses = 0;
            totalResponses = 1;

            LLMManager.loaderAnswers = new string[1];

            //Player.Instance.mescolaElementi();
            //Loader.mescolaElementi();
            Loader.elementi[0] = "Acqua";
            Loader.elementi[1] = "Terra";
            Loader.elementi[2] = "Fuoco";
            Loader.elementi[3] = "Vento";

            LLMManager.loaderAnswers[0] = "Spirito della Roccia: \n\"Onde nutrono il suolo, che scalda l'aria e muove le fronde.\"";

            completedResponses = 1;

            yield return new WaitUntil(() =>
                LLMManager.loaderAnswers[0] != null
            );

        }

        Loader.LoaderCallback();
    }

    private void UpdateProgress(int totalResponses) {
        float progress = Mathf.Clamp01((float)completedResponses / totalResponses);
        LoadingUI.Instance.SetProgress(progress);
    }

    public IEnumerator TestDialogue() {
        string[] testLoaderAnswers = new string[13];
        string nexRole = "Ti chiami Nex, sei un ragazzino coraggioso. Il Maestro Orwin è il tuo mentore e ti ha cresciuto con affetto. Parla in prima persona senza aggiungere commenti, pensieri o descrizioni. Non superare le 30 parole.";
        string orwinRole = "Ti chiami Orwin, sei un vecchio fabbro e maestro di Nex. Parla in prima persona senza aggiungere commenti, pensieri o descrizioni. Non superare le 30 parole.";
        string spiritoRole = "Sei lo Spirito della Roccia, parli solo in enigmi e messaggi criptici.";
        string risposta;
        string userPrompt;

        userPrompt = "Urli la frase 'Maestro Orwiin' ripetutamente chiedendoti dove si sarà cacciato il Maestro Orwin";
        StartCoroutine(LLMManager.GetNPCResponse(
            nexRole,
            userPrompt,
            (response) => {
                risposta = "Nex: \n" + response;
                LLMManager.loaderAnswers[0] = risposta;
            }
        ));
        userPrompt = "Vedi un robot davanti a te che distrugge oggetti. Ti chiedi come sia possibile che ci sia un robot qui.";
        StartCoroutine(LLMManager.GetNPCResponse(
            nexRole,
            userPrompt,
            (response) => {
                risposta = "Nex: \n" + response;
                LLMManager.loaderAnswers[1] = risposta;
            }
        ));
        userPrompt = "Sei sorpreso di vedere Nex, il tuo allievo, arrivare qui sano e salvo. Poi gli chiedi come sia possibile che la sua connessione alle lame funzioni già così bene";
        StartCoroutine(LLMManager.GetNPCResponse(
            orwinRole,
            userPrompt,
            (response) => {
                risposta = "Orwin: \n" + response;
                LLMManager.loaderAnswers[2] = risposta;
            }
        ));
        userPrompt = "Rispondi a Orwin dicendogli che non sai cosa stia succedendo ma senti un nuovo potere dentro di te";
        StartCoroutine(LLMManager.GetNPCResponse(
            nexRole,
            userPrompt,
            (response) => {
                risposta = "Nex: \n" + response;
                LLMManager.loaderAnswers[3] = risposta;
            }
        ));
        userPrompt = "Spieghi a Nex che la Frattura è la causa dei suoi nuovi poteri e sta portando i robot qui, e che presto queste terre saranno invase";
        StartCoroutine(LLMManager.GetNPCResponse(
            orwinRole,
            userPrompt,
            (response) => {
                risposta = "Orwin: \n" + response;
                LLMManager.loaderAnswers[4] = risposta;
            }
        ));
        userPrompt = "Dì a Orwin di rientrare nella casa poichè stanno arrivando altri robot";
        StartCoroutine(LLMManager.GetNPCResponse(
            nexRole,
            userPrompt,
            (response) => {
                risposta = "Nex: \n" + response;
                LLMManager.loaderAnswers[5] = risposta;
            }
        ));
        userPrompt = "Non ti senti più molto bene, lo dici ad alta voce in maniera stanca";
        StartCoroutine(LLMManager.GetNPCResponse(
            nexRole,
            userPrompt,
            (response) => {
                risposta = "Nex: \n" + response;
                LLMManager.loaderAnswers[6] = risposta;
            }
        ));
        userPrompt = "Chiami Nex in modo spaventato per farlo venire da te in fretta";
        StartCoroutine(LLMManager.GetNPCResponse(
            orwinRole,
            userPrompt,
            (response) => {
                risposta = "Orwin: \n" + response;
                LLMManager.loaderAnswers[7] = risposta;
            }
        ));
        userPrompt = "Dici a Nex che dovete correre, e in fretta. Sta arrivando una legione di robot!";
        StartCoroutine(LLMManager.GetNPCResponse(
            orwinRole,
            userPrompt,
            (response) => {
                risposta = "Orwin: \n" + response;
                LLMManager.loaderAnswers[8] = risposta;
            }
        ));
        userPrompt = "Dici a Nex che dovete usare i 3 sacri Elisir di Lumenfall se volete respingere l'invasione. E che Nex deve trovare i passaggi, risolvere gli enigmi e poi tornare qui.";
        StartCoroutine(LLMManager.GetNPCResponse(
            orwinRole,
            userPrompt,
            (response) => {
                risposta = "Orwin: \n" + response;
                LLMManager.loaderAnswers[0] = risposta;
            }
        ));
        userPrompt = "Crea un messaggio criptico che faccia venire in mente 4 colori in questo ordine: Rosso, Giallo, Blu, Verde. Mantienilo sotto le 30 parole.";
        StartCoroutine(LLMManager.GetNPCResponse(
            spiritoRole,
            userPrompt,
            (response) => {
                risposta = "Spirito della Roccia: \n" + response;
                LLMManager.loaderAnswers[0] = risposta;
            }
        ));
        userPrompt = "Crea un messaggio criptico che faccia venire in mente 4 oggetti in questo ordine: Cervo, Cuore, Porta, Corona. Mantienilo sotto le 30 parole.";
        StartCoroutine(LLMManager.GetNPCResponse(
            spiritoRole,
            userPrompt,
            (response) => {
                risposta = "Spirito della Roccia: \n" + response;
                LLMManager.loaderAnswers[0] = risposta;
            }
        ));
        userPrompt = "Crea un messaggio criptico che faccia venire in mente 4 elementi in questo ordine: Terra, Acqua, Fuoco, Vento. Mantienilo sotto le 30 parole.";
        StartCoroutine(LLMManager.GetNPCResponse(
            spiritoRole,
            userPrompt,
            (response) => {
                risposta = "Spirito della Roccia: \n" + response;
                LLMManager.loaderAnswers[0] = risposta;
            }
        ));

        yield return new WaitUntil(() =>
            LLMManager.loaderAnswers.All(answer => !string.IsNullOrEmpty(answer))
        );

        for (int i = 0; i < 13; i++) {
            Debug.Log("Frase " + i + ": " + LLMManager.loaderAnswers[i] + "\n");
        }

    }
}
