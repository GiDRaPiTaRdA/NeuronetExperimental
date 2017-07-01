using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightManagment.WeightManage
{
    public class IncompatibleWeightsSizesException:Exception
    {
        private string defaultMessage = "The weights with such sizes are incompatible";

        private string additionalMessage = "";


        public IncompatibleWeightsSizesException()
        {
            additionalMessage = "";
        }
        public IncompatibleWeightsSizesException(string message)
        {
            additionalMessage = message;
        }

        public override string Message => defaultMessage +"\t"+ additionalMessage;
    }
}
