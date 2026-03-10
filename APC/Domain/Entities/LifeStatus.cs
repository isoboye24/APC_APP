using System;

namespace APC.Domain.Entities
{
    public class LifeStatus
    {
        public DateTime DeadDate { get; private set; }

        public LifeStatus(DateTime deadDate)
        {
            SetDeadDate(deadDate);
        }

        private void SetDeadDate(DateTime date)
        {
            if (date.Year < 2000 || date.Year > DateTime.Now.Year + 1)
                throw new ArgumentException("Invalid year. Choose from 2000 to " + DateTime.Now.Year);

            DeadDate = date;
        }

        public void UpdateDeadDate(DateTime newDate)
        {
            SetDeadDate(newDate);
        }
    }

}
