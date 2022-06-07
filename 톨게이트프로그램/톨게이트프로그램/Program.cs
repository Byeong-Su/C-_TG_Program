using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 톨게이트프로그램
{
    static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new 톨게이트());
        }
    }
}

/*
[STAThread]
기본적으로 MTA(멀티쓰레드아파트먼트)인 .net에
단일쓰레드아파트먼트 기반인 COM객체를 사용할 경우 명시해줘야함
*/