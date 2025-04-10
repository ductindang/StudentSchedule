namespace StudentManageFrontEnd.Models.Enum
{
    public enum WeekDay
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
        Sunday = 7
    }

    public static class WeekDayExtensions
    {
        public static string GetDisplayName(this WeekDay day)
        {
            switch (day)
            {
                case WeekDay.Monday: return "Thứ 2";
                case WeekDay.Tuesday: return "Thứ 3";
                case WeekDay.Wednesday: return "Thứ 4";
                case WeekDay.Thursday: return "Thứ 5";
                case WeekDay.Friday: return "Thứ 6";
                case WeekDay.Saturday: return "Thứ 7";
                case WeekDay.Sunday: return "Chủ Nhật";
                default: return string.Empty;
            }
        }
    }
}
