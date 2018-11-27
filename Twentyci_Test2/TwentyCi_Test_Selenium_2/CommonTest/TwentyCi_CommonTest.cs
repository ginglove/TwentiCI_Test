using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwentyCi_Test_Selenium_2.Utilities;
using TwentyCi_Test_Selenium_2.JsonObjects;

namespace TwentyCi_Test_Selenium_2.CommonTest
{
    public class TwentyCi_CommonTest
    {
        public TwentyCi_Utilities t = new TwentyCi_Utilities();
        public UtilitiesCommon u = new UtilitiesCommon();
        public void TwentyCi_Test_2_Start(Json_TwentyCi j)
        {
            t.RightMove_StartFunction(j);
        }
    }
}
