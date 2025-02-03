using System;
using System.Collections;
using UnityEngine;

public class WorldTime : MonoBehaviour
{
    public event EventHandler<TimeSpan> WorldTimeChanged;
    public event EventHandler<int> DayCountChanged;
    public event EventHandler<int> CurrentDayChanged;
    public event EventHandler<Season> SeasonChanged;
    public event EventHandler<int> YearChanged;

    [SerializeField] private int dayLength;
    [SerializeField] private int dayCount;
    [SerializeField] private int currentDay;
    [SerializeField] private int currentMonth;
    [SerializeField] private int currentYear;
    [SerializeField] private int daysInMonth;
    [SerializeField] private int minutesInDay = 1440;

    private TimeSpan currentTime;
    private float minuteLength => (float)dayLength / minutesInDay;
    private Season currentSeason;

    public int DayCount => dayCount;
    public int CurrentDay => currentDay;
    public Season CurrentSeason => currentSeason;
    public int Year => currentYear;
    public int MinutesInDay => minutesInDay;
    private void Awake()
    {
        currentSeason = (Season)currentMonth;
    }

    private void Start()
    {
        StartCoroutine(AddMinute());
    }

    private IEnumerator AddMinute()
    {
        currentTime += TimeSpan.FromMinutes(1);
        WorldTimeChanged?.Invoke(this, currentTime);

        if (currentTime.TotalMinutes % TimeConstant.MinutesInDay == 0)
        {
            dayCount++;
            DayCountChanged?.Invoke(this, dayCount);

            if (dayCount % daysInMonth == 0)
            {
                currentMonth++;
                if (currentMonth > 4)
                {
                    currentMonth = 1;
                    currentYear++;
                    YearChanged?.Invoke(this, currentYear);
                }
                currentSeason = (Season)currentMonth;
                SeasonChanged?.Invoke(this, currentSeason);

                currentDay = 1;
                CurrentDayChanged?.Invoke(this, currentDay);
            }
            else
            {
                currentDay++;
                CurrentDayChanged?.Invoke(this, currentDay);
            }
        }

        yield return new WaitForSeconds(minuteLength);
        StartCoroutine(AddMinute());
    }

    public int GetCurrentDayCount()
    {
        return dayCount;
    }

}
public enum Season
{
    Spring = 1,
    Fall = 2,
    Winter = 3,
    Summer = 4
}
