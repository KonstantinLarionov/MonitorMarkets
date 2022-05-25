using System;

namespace MonitorMarkets.Application.Extensions
{
    public class ServerTimeHelper
    {
        public ServerTimeHelper(Func<DateTime> get_utc, TimeSpan utc_offset)
        {
            GetUtc = get_utc;
            UtcOffset = utc_offset;
        }

        readonly Func<DateTime> GetUtc = null;

        /// <summary>
        /// Часовой пояс сервера
        /// </summary>
        public TimeSpan UtcOffset { get; set; } = TimeSpan.Zero;

        /// <summary>
        /// Расхождение локального и серверного времени (в одном часовом поясе)
        /// </summary>
        public TimeSpan ShiftOffset { get; set; } = TimeSpan.Zero;

        /// <summary>
        /// Время сервера: UTC + сдвиг по часовому поясу + сдвиг по расхождению (опционально)
        /// </summary>
        /// <returns></returns>
        public DateTimeOffset ServerTime(bool add_shift=true)
        {
            //TODO:
            //
            //- (?) DateTimeKind kind = UtcOffset.Equals(TimeZoneInfo.Utc.BaseUtcOffset) ? DateTimeKind.Utc : DateTimeKind.Unspecified;

            var utc = add_shift? GetUtc().Add(ShiftOffset): GetUtc();

            var offset = UtcOffset;

            return new DateTimeOffset(DateTime.SpecifyKind(utc.Add(offset), DateTimeKind.Unspecified), offset);
        }

        #region [unix]

        readonly DateTime m_UnixTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public DateTime UnixTime => m_UnixTime;

        public long ToUnixMilliseconds(DateTime dt)
        {
            return (long)(dt - m_UnixTime).TotalMilliseconds;
        }

        public DateTime FromUnixMilliseconds(long ms)
        {
            return DateTime.SpecifyKind(m_UnixTime.Add(TimeSpan.FromMilliseconds(ms)), DateTimeKind.Unspecified);
        }

        #endregion
    }
}