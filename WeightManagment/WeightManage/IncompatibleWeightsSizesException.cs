using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WeightManagment.WeightManage
{
    public class IncompatibleWeightsSizesException:Exception
    {
        private readonly string defaultMessage = "The weights with such sizes are incompatible";

        private readonly string additionalMessage;


        public IncompatibleWeightsSizesException()
        {
            this.additionalMessage = string.Empty;
        }
        public IncompatibleWeightsSizesException(string message)
        {
            this.additionalMessage = message;
        }

        public override string Message => this.defaultMessage +"\t"+ this.additionalMessage;
    }
}
