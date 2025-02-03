using UnityEngine;
using TMPro;

public class DisplayDate : MonoBehaviour
{
    [SerializeField] private WorldTime worldTime;
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        worldTime.CurrentDayChanged += OnCurrentDayChanged;
        worldTime.SeasonChanged += OnSeasonChanged;
        worldTime.YearChanged += OnYearChanged;
        UpdateDate();
    }

    private void OnDestroy()
    {
        worldTime.CurrentDayChanged -= OnCurrentDayChanged;
        worldTime.SeasonChanged -= OnSeasonChanged;
        worldTime.YearChanged -= OnYearChanged;
    }

    private void UpdateDate()
    {
        int day = worldTime.CurrentDay;
        var month = worldTime.CurrentSeason;
        int year = worldTime.Year;

        if (month == 0)
        {
            month = Season.Spring;
        }

        text.SetText(day.ToString() + " d. " + month.ToString() + " " + year.ToString() + " Year");
    }

    private void OnCurrentDayChanged(object sender, int day)
    {
        UpdateDate();
    }

    private void OnSeasonChanged(object sender, Season season)
    {
        UpdateDate();
    }

    private void OnYearChanged(object sender, int year)
    {
        UpdateDate();
    }
}
