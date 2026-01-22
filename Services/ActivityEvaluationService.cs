public class ActivityEvaluationService
{
    private readonly List<Activity> _activities = new()
    {
        new Activity
        {
            Name = "Пробежка",
            MinTemperature = 5,
            MaxTemperature = 25,
            AllowRain = false,
            AllowLightRain = false,
            MaxWindSpeed = 10
        },
        new Activity
        {
            Name = "Прогулка",
            MinTemperature = -10,
            MaxTemperature = 30,
            AllowRain = true,
            AllowLightRain = true,
            MaxWindSpeed = 20
        },
        new Activity
        {
            Name = "Велопрогулка",
            MinTemperature = 10,
            MaxTemperature = 28,
            AllowRain = false,
            AllowLightRain = true,
            MaxWindSpeed = 12
        }
    };

    // Получить все активности
    public IEnumerable<Activity> GetActivities() => _activities;

    // Оценка одной активности по погоде
    public ActivityEvaluationResult Evaluate(Activity activity, WeatherData weather)
    {
        if (weather.Temperature < activity.MinTemperature ||
            weather.Temperature > activity.MaxTemperature)
        {
            return NotSuitable(activity, "Сильное отклонение температуры");
        }

        if (weather.WindSpeed > activity.MaxWindSpeed)
        {
            return NotSuitable(activity, "Слишком ветренно");
        }

        if (weather.IsRaining && !activity.AllowRain)
        {
            if (activity.AllowLightRain)
            {
                return Acceptable(activity, "Возможен дискомфорт от дождя");
            }

            return NotSuitable(activity, "Дождь не подходит");
        }

        return Suitable(activity, "Подходящие погодные условия");
    }

    // Методы для удобства
    private ActivityEvaluationResult Suitable(Activity a, string reason) =>
        new() { ActivityName = a.Name, Suitability = ActivitySuitability.Suitable, Reason = reason };

    private ActivityEvaluationResult Acceptable(Activity a, string reason) =>
        new() { ActivityName = a.Name, Suitability = ActivitySuitability.Acceptable, Reason = reason };

    private ActivityEvaluationResult NotSuitable(Activity a, string reason) =>
        new() { ActivityName = a.Name, Suitability = ActivitySuitability.NotSuitable, Reason = reason };

    // Добавление новой активности
    public bool AddActivity(Activity activity)
    {
        if (activity == null || string.IsNullOrWhiteSpace(activity.Name))
            return false; // не добавляем пустое имя

        if (_activities.Any(a => a.Name.Equals(activity.Name, StringComparison.OrdinalIgnoreCase)))
            return false;

        _activities.Add(activity);
        return true;
    }


    // Обновление существующей активности
    public bool UpdateActivity(string oldName, Activity updatedActivity)
    {
        if (updatedActivity == null || string.IsNullOrWhiteSpace(updatedActivity.Name))
            return false; // не обновляем пустым именем

        var activity = _activities.FirstOrDefault(a => a.Name.Equals(oldName, StringComparison.OrdinalIgnoreCase));
        if (activity == null) return false;

        activity.Name = updatedActivity.Name;
        activity.MinTemperature = updatedActivity.MinTemperature;
        activity.MaxTemperature = updatedActivity.MaxTemperature;
        activity.AllowRain = updatedActivity.AllowRain;
        activity.AllowLightRain = updatedActivity.AllowLightRain;
        activity.MaxWindSpeed = updatedActivity.MaxWindSpeed;

        return true;
    }


    // Удаление активности
    public bool DeleteActivity(string name)
    {
        var activity = _activities.FirstOrDefault(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (activity == null) return false;

        _activities.Remove(activity);
        return true;
    }
}
