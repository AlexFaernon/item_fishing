using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private Transform level1Tutorial;
    [SerializeField] private Transform level2Tutorial;
    [SerializeField] private GameObject upgrades;
    [SerializeField] private GameObject researches;
    [SerializeField] private StoryWindow story;

    public static bool TutorialEnabled = true;

    private void Start()
    {
        if (!TutorialEnabled) return;
        
        switch (LoadedData.LevelNumber)
        {
            case 1:
                StartCoroutine(Level1());
                break;
            case 2:
                StartCoroutine(Level2());
                break;
            case 3:
                StartCoroutine(Level3());
                break;
        }
    }

    private IEnumerator Level1()
    {
        yield return new WaitUntil(() => GameMode.Mode == Mode.Player);
        
        story.gameObject.SetActive(true);
        story.SetStory(false, "О боже… Хорошо что выжил хотя бы. Но где я черт возьми? Надо связаться с командованием и вызвать помощь.");
        yield return new WaitUntil(() => story.IsStoryContinued);
        story.SetStory(false, "Прием. На связи инженер номер 479225. Грузовой корабль потерпел крушение, мой спасательный челнок упал в неизвестную водную среду, высылаю координаты, прием");
        yield return new WaitUntil(() => story.IsStoryContinued);
        story.SetStory(true, "Прием, принято. Высылаю спасательную команду, вам необходимо дождаться когда они соберут оставшихся выживших и останки корабля, пожалуйста ожидайте и будьте на связи.");
        yield return new WaitUntil(() => story.IsStoryContinued);
        story.SetStory(true, "Не паникуйте, не пытайтесь выбраться наружу челнока, это может быть небезопасно. В нем должно быть достаточно сухпайков и питьевой воды чтобы могли дождаться спасения");
        yield return new WaitUntil(() => story.IsStoryContinued);
        story.SetStory(false, "Значит придется ждать чертовски долго… Ладно, пока надо осмотреть корабль, смотрю его все же потрепало от падения.");
        yield return new WaitUntil(() => story.IsStoryContinued);
        story.gameObject.SetActive(false);
        
        level1Tutorial.GetChild(0).gameObject.SetActive(true);
        yield return new WaitUntil(() => GameMode.Mode == Mode.Fishing);
        level1Tutorial.GetChild(0).gameObject.SetActive(false);

        level1Tutorial.GetChild(1).gameObject.SetActive(true);
        yield return new WaitUntil(() => GameMode.Mode == Mode.Player);
        level1Tutorial.GetChild(1).gameObject.SetActive(false);

        level1Tutorial.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(4);
        level1Tutorial.GetChild(2).gameObject.SetActive(false);

        level1Tutorial.GetChild(3).gameObject.SetActive(true);
        var wall = Ship.Walls.First(wall => wall.Side == Side.Up);
        yield return new WaitUntil(() => wall.Health == wall.wallClass.MaxHealth);
        level1Tutorial.GetChild(3).gameObject.SetActive(false);

        level1Tutorial.GetChild(4).gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(4);
        level1Tutorial.GetChild(4).gameObject.SetActive(false);

        level1Tutorial.GetChild(5).gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(4);
        level1Tutorial.GetChild(5).gameObject.SetActive(false);
    }

    private IEnumerator Level2()
    {
        yield return new WaitUntil(() => GameMode.Mode == Mode.Player);

        level2Tutorial.GetChild(0).gameObject.SetActive(true);
        yield return new WaitUntil(() => GameMode.Mode == Mode.Fishing);
        level2Tutorial.GetChild(0).gameObject.SetActive(false);
        
        story.gameObject.SetActive(true);
        story.SetStory(false, "О, это куски оборудования с корабля! Нужно их собрать, думаю я смогу сделать из них что-нибудь полезное.");
        yield return new WaitUntil(() => story.IsStoryContinued);
        story.gameObject.SetActive(false);

        yield return new WaitUntil(() => Resources.Electronics.Count > 0);
        
        story.gameObject.SetActive(true);
        story.SetStory(false, "Надо бы посмотреть в верстаке что я могу с ними сделать.");
        yield return new WaitUntil(() => story.IsStoryContinued);
        story.gameObject.SetActive(false);
        
        level2Tutorial.GetChild(1).gameObject.SetActive(true);
        yield return new WaitUntil(() => GameMode.Mode == Mode.Player);
        level2Tutorial.GetChild(1).gameObject.SetActive(false);
    
        level2Tutorial.GetChild(2).gameObject.SetActive(true);
        yield return new WaitUntil(() => upgrades.activeSelf);
        level2Tutorial.GetChild(2).gameObject.SetActive(false);

        level2Tutorial.GetChild(3).gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(3);
        level2Tutorial.GetChild(3).gameObject.SetActive(false);

        level2Tutorial.GetChild(4).gameObject.SetActive(true);
        yield return new WaitUntil(() => researches.activeSelf);
        level2Tutorial.GetChild(4).gameObject.SetActive(false);
        
        level2Tutorial.GetChild(5).gameObject.SetActive(true);
        yield return new WaitUntil(() => Research.TurretsResearch);
        level2Tutorial.GetChild(5).gameObject.SetActive(false);
        
        level2Tutorial.GetChild(6).gameObject.SetActive(true);
        yield return new WaitUntil(() => !researches.activeSelf && !upgrades.activeSelf);
        level2Tutorial.GetChild(6).gameObject.SetActive(false);

        level2Tutorial.GetChild(7).gameObject.SetActive(true);
        yield return new WaitUntil(() => Ship.Turrets.Any(turret => turret.IsInstalled));
        level2Tutorial.GetChild(7).gameObject.SetActive(false);
        
        level2Tutorial.GetChild(8).gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(4);
        level2Tutorial.GetChild(8).gameObject.SetActive(false);
    }

    private IEnumerator Level3()
    {
        yield return new WaitUntil(() => GameMode.Mode == Mode.Player);
        
        story.gameObject.SetActive(true);
        story.SetStory(false, "Я заметил что эти монстры начинают атаковать после этого странного звука, и уплывают когда он звучит снова.");
        yield return new WaitUntil(() => story.IsStoryContinued);
        story.SetStory(false, "Не думаю что смогу перебить всех этих тварей, мне надо просто переждать эти нападки.");
        yield return new WaitUntil(() => story.IsStoryContinued);
        story.gameObject.SetActive(false);
    }
}
