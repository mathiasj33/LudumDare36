using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;

public class TextIntro : MonoBehaviour
{
    private Text quoteText;
    private Text storyText;
    private Text buttonText;
    private char[] quoteContent;

    private AudioSource textSound;

    void Start()
    {
        quoteText = GameObject.Find("QuoteText").GetComponent<Text>();
        storyText = GameObject.Find("StoryText").GetComponent<Text>();
        buttonText = GameObject.Find("Button").GetComponent<Text>();
        quoteContent = quoteText.text.ToCharArray();
        quoteText.text = "";

        textSound = GameObject.Find("Sounds").GetComponents<AudioSource>()[0];

        StartCoroutine("ShowQuoteText");
    }

    private IEnumerator ShowQuoteText()
    {
        yield return new WaitForSeconds(1f);
        StringBuilder builder = new StringBuilder();
        builder.Append("_");
        for (int i = 0; i < quoteContent.Length; i++)
        {
            textSound.Play();
            builder.Remove(i, 1);

            builder.Append(quoteContent[i]);
            builder.Append("_");

            quoteText.text = builder.ToString();

            float wait = .08f;  //.08f
            switch ((int)quoteContent[i])
            {
                case CharConstants.Comma:
                    wait = .5f;
                    break;
                case CharConstants.LineBreak:
                    wait = 1f;
                    break;
            }
            yield return new WaitForSeconds(wait);
        }

        yield return new WaitForSeconds(1.5f);

        GameObject.Find("Sounds").GetComponents<AudioSource>()[1].Play();

        StartCoroutine("ShowStoryText");
    }

    private IEnumerator ShowStoryText()
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(FadeOut(quoteText));
        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeIn(storyText));
        StartCoroutine(FadeIn(buttonText));
    }

    private IEnumerator FadeOut(Text text)
    {
        float a = 1;
        Color color = text.color;
        while (a > 0)
        {
            a -= .05f;
            text.color = new Color(color.r, color.g, color.b, a);
            yield return null;
        }
    }

    private IEnumerator FadeIn(Text text)
    {
        float a = 0;
        Color color = text.color;
        while (a < 1)
        {
            a += .05f;
            text.color = new Color(color.r, color.g, color.b, a);
            yield return null;
        }
    }
}
