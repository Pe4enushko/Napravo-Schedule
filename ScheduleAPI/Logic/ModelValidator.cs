using ScheduleAPI.Models;

namespace ScheduleAPI.Logic
{
    /// <summary>
    /// Ensures that validated model can be sent to db without errors
    /// </summary>
    public static class ModelValidator
    {
        public static bool Validate(Notification model)
        {
            return model.IdStudent != null
                && model.Header != null
                && model.Message != null
                && model.Moment != null
                && model.IsChecked != null;
        }
    }
}
