using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UchebPrackt326.Compnent
{
    public partial class ClientService
    {
        public string CollorEnd
        {
            get
            {
                if ((StartTime - DateTime.Now )< TimeSpan.FromHours(5))
                {
                    return $"#FFE7FABF";
                }
                else
                {
                    return "";
                }

            }

        }
        public string TimeStart
        {
            get
            {
                var time = StartTime - DateTime.Now;
                return $"Начало в {StartTime.ToString("F")}.  осталось: {time.ToString(@"hh\:mm")}";
            }
        }
    }
}
