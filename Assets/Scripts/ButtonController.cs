using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button myButton; // Сама кнопка
    public Text buttonText; // Текст на кнопці
    private bool isClicked = false; // Перевірка натискання

    private string originalText = "Вітаю, любий друже!\n Дякую, що завітав у цю гру і якщо ті в неї хочешь пограть,\n давай розберемося як грати" +
        "\n Гра починаеться з того що ти з'являешься у лісі,\n бо тебе отровили їжею і ті втратив свідомість.\n З самого початку ти маешь будиночок в якому можна перебувати." +
        "\n Таких будиночків буде 6 і в якомусь із них може бут ніж,\n завдяки якому ти зможешь вбивати гулей які нападатимуть на тебе.\n Щоб премогти гуля потрібен або ніж, або шприц" +
        "\n Після того як ти перемагаешь гуля з нього випадає ключ який наносить урон босу,\n але босс буде з'являтися вже після того,\n як ти вбив всіх гулів і якщо ти підібрав ключ " +
        "раніше ніж потрібно,\n то це буде зайвий ключ. Мета гри - перемогти босса"; // Початковий текст

    private void Start()
    {
        // Встановлюємо початковий текст
        buttonText.text = originalText;

        // Додаємо слухач подій для кнопки
        myButton.onClick.AddListener(ToggleText);
    }

    private void ToggleText()
    {
        if (isClicked)
        {
            buttonText.text = originalText; // Повертаємо старий текст
        }


        isClicked = !isClicked; // Змінюємо стан
    }
}
