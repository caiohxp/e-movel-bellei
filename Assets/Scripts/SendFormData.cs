using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class SendFormData : MonoBehaviour
{
    public TMP_Dropdown enterpriseDropdown;
    public TMP_InputField nameInput;
    public TMP_InputField emailInput;
    public TMP_InputField phoneInput;
    public TMP_InputField commentInput;
    public Button btnSubmit;
    public Button btnClean;
    public TextMeshProUGUI successMessage; // Text component to show success message
    public TextMeshProUGUI errorMessage;   // Text component to show error message

    private string formUrl = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSdxA7U5jZFYXbGmfBim_m6ECtXSCotMi5jVGoP74Q9_PfjClg/formResponse";

    public void Start()
    {
        btnSubmit.onClick.AddListener(() => SubmitFeedback());
        btnClean.onClick.AddListener(() => CleanForm());
        successMessage.gameObject.SetActive(false); // Initially hide the success message
        errorMessage.gameObject.SetActive(false);   // Initially hide the error message
    }

    private void SubmitFeedback()
    {
        string enterprise = enterpriseDropdown.options[enterpriseDropdown.value].text;
        string name = nameInput.text;
        string email = emailInput.text;
        string phone = phoneInput.text;
        string comment = commentInput.text;

        if (string.IsNullOrWhiteSpace(enterprise) || string.IsNullOrWhiteSpace(name) || 
            string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phone) || 
            string.IsNullOrWhiteSpace(comment))
        {
            StartCoroutine(ShowErrorMessage("Todos os campos devem ser preenchidos."));
            return;
        }

        StartCoroutine(Post(enterprise, name, email, phone, comment));
    }

    private void CleanForm()
    {
        nameInput.text = "";
        emailInput.text = "";
        phoneInput.text = "";
        commentInput.text = "";
    }

    private IEnumerator Post(string enterprise, string name, string email, string phone, string comment)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.1809063416", enterprise);
        form.AddField("entry.1531581476", name);
        form.AddField("entry.718958483", email);
        form.AddField("entry.560174319", phone);
        form.AddField("entry.1868292946", comment);

        using (UnityWebRequest www = UnityWebRequest.Post(formUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Feedback submitted successfully.");
                successMessage.gameObject.SetActive(true); // Show success message
                CleanForm();
                yield return new WaitForSeconds(3); // Wait for 3 seconds
                successMessage.gameObject.SetActive(false); // Hide success message
            }
            else
            {
                Debug.LogError("Error in feedback submission: " + www.error);
                StartCoroutine(ShowErrorMessage("Erro ao enviar o feedback: " + www.error));
            }
        }
    }

    private IEnumerator ShowErrorMessage(string message)
    {
        errorMessage.text = message;
        errorMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(3); // Show error message for 3 seconds
        errorMessage.gameObject.SetActive(false);
    }
}
