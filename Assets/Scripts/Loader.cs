using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader {

    public static string difficulty = "Medio";

    public static bool Door1Clear = false;
    public static bool Door2Clear = false;
    public static bool Door3Clear = false;

    public static string[] colori = { "Rosso", "Giallo", "Verde", "Blu" };
    public static string[] totems = { "Cuore", "Cervo", "Corona", "Porta" };
    public static string[] elementi = { "Acqua", "Fuoco", "Terra", "Vento" };

    public static float[] statoPlayerCave = { 0f, 100f, 1f }; // 0: prima volta, 1: salute, 2: pozioni
    public static string fraseCave;
    public static float[] statoPlayerDoor1 = { 0f, 100f, 1f, 0f, 0f, 0f }; // 0: prima volta, 1: salute, 2: pozioni, 3: elisir1, 4: elisir2, 5: elisir3
    //public static string fraseDoor1;
    public static float[] statoPlayerDoor2 = { 0f, 100f, 1f, 0f, 0f, 0f };
    //public static string fraseDoor2;
    public static float[] statoPlayerDoor3 = { 0f, 100f, 1f, 0f, 0f, 0f };
    //public static string fraseDoor3;

    public static bool caveGiaCaricata = false;

    public enum Scene {
        MainMenuScene,
        SampleScene,
        LoadingScene,
        Cave,
        Door_1,
        Door_2,
        Door_3

    }

    public static Scene targetScene;

    public static void Load(Scene targetScene) {
        Loader.targetScene = targetScene;

        SceneManager.LoadScene(Scene.LoadingScene.ToString());

    }

    public static void LoaderCallback() {

        Debug.Log("LOADDO LA SCENE " + targetScene);

        SceneManager.LoadScene(targetScene.ToString());
    }

    public static void mescolaColori() {
        colori = colori.OrderBy(c => Random.value).ToArray();
    }
    public static void mescolaTotems() {
        totems = totems.OrderBy(c => Random.value).ToArray();
    }
    public static void mescolaElementi() {
        elementi = elementi.OrderBy(c => Random.value).ToArray();
    }

}