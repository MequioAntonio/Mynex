using Newtonsoft.Json;
using System.Collections;
using System.Linq;
using System.Text;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Networking;

public static class LLMManager {
    public static string[] loaderAnswers = new string[9];

    private static string apiKey = "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";
    private static string apiUrl = "https://openrouter.ai/api/v1/chat/completions";

    public static IEnumerator GetNPCResponse(string npcGuidelines, string userPrompt, System.Action<string> callback, int maxRetries = 3) {
        int attempt = 0;
        bool success = false;
        string finalResponse = "Errore: nessuna risposta valida dopo i tentativi.";

        while (attempt < maxRetries && !success) {
            attempt++;
            Debug.Log($"Tentativo {attempt} di {maxRetries}...");

            var requestBody = new {
                model = "deepseek/deepseek-chat-v3.1:free", //il migliore, alcune volte da errori
                //model = "openai/gpt-oss-120b:free", //non funziona più
                //model = "x-ai/grok-4-fast:free", //non funziona più
                //model = "openai/gpt-oss-20b:free", //brutto tbh
                //model = "google/gemma-3n-e2b-it:free", //non funziona
                //model = "deepseek/deepseek-r1-0528-qwen3-8b:free", //TERRIBILE
                //model = "alibaba/tongyi-deepresearch-30b-a3b:free", //TERRIBILE
                //model = "meituan/longcat-flash-chat:free", //rate limited upstream error
                //model = "nvidia/nemotron-nano-9b-v2:free", //TERRIBILE
                //model = "z-ai/glm-4.5-air:free", //PIU O MENO DECENTE
                //model = "qwen/qwen3-coder:free",
                //model = "qwen/qwen3-235b-a22b:free",
                //model = "meta-llama/llama-4-maverick:free",

                messages = new object[]
                {
                    new { role = "system", content = npcGuidelines },
                    new { role = "user", content = userPrompt }
                }
            };

            string jsonBody = JsonConvert.SerializeObject(requestBody);

            using (UnityWebRequest request = new UnityWebRequest(apiUrl, "POST")) {
                byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();

                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Authorization", "Bearer " + apiKey);
                request.SetRequestHeader("HTTP-Referer", "http://localhost");
                request.SetRequestHeader("X-Title", "UnityNPCDemo");

                yield return request.SendWebRequest();

                Debug.Log($"Status Code: {request.responseCode}");

                if (request.result == UnityWebRequest.Result.Success) {
                    string responseJson = request.downloadHandler.text;
                    Debug.Log($"Raw response: {responseJson}");

                    try {
                        var response = JsonConvert.DeserializeObject<ChatResponse>(responseJson);

                        if (response != null && response.choices != null && response.choices.Length > 0) {
                            finalResponse = CleanResponse(response.choices[0].message.content.Trim());
                            success = true;
                            Debug.Log("Risposta valida ricevuta dal modello.");
                        }
                        else {
                            Debug.LogWarning("Nessuna risposta valida dal modello (choices vuote o null).");
                        }
                    }
                    catch (System.Exception e) {
                        Debug.LogError($"Parsing JSON fallito: {e.Message}");
                    }
                }
                else {
                    Debug.LogError($"Errore API: {request.error}, Body: {request.downloadHandler.text}");
                }
            }

            if (!success) {
                Debug.Log("Attendo 1s prima di riprovare...");
                yield return new WaitForSeconds(1f);
            }
        }

        if (!success) {
            Debug.LogError("Falliti tutti i tentativi, ritorno messaggio di errore.");
        }

        callback?.Invoke(finalResponse);
    }

    private static string CleanResponse(string raw) {
        if (string.IsNullOrEmpty(raw)) return raw;

        return raw
            .Replace("<｜begin▁of▁sentence｜>", "")
            .Replace("<begin_of_sentence>", "")
            .Replace("<begin of sentence>", "")
            .Replace("<end of sentence>", "")
            .Replace("<bos>", "")
            .Replace("<eos>", "")
            .Trim();
    }
}

[System.Serializable]
public class ChatResponse {
    public Choice[] choices;
}

[System.Serializable]
public class Choice {
    public Message message;
}

[System.Serializable]
public class Message {
    public string role;
    public string content;
}